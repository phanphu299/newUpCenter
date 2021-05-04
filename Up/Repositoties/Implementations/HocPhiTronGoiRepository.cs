using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Converters;
using Up.Data;
using Up.Models;

namespace Up.Repositoties
{
    public class HocPhiTronGoiRepository : BaseRepository, IHocPhiTronGoiRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public HocPhiTronGoiRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<bool> CheckIsDisable()
        {
            var item = await _context.HocPhiTronGois
                                    .Where(x => !x.IsDisabled &&
                                                (DateTime.Now.Year > x.ToDate.Year || (DateTime.Now.Year == x.ToDate.Year && DateTime.Now.Month > x.ToDate.Month)))
                                    .ToListAsync();
            foreach (var hocPhi in item)
            {
                hocPhi.IsDisabled = true;
            }
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateHocPhiTronGoiAsync(CreateHocPhiTronGoiInputModel input, string loggedEmployee)
        {
            var hocPhi = _entityConverter.ToEntityHocPhiTronGoi(input, loggedEmployee);
            _context.HocPhiTronGois.Add(hocPhi);

            var hocPhi_LopHocs = _entityConverter.ToHocPhiTronGoi_LopHocList(hocPhi.HocPhiTronGoiId, input.LopHocList, loggedEmployee);
            await _context.HocPhiTronGoi_LopHocs.AddRangeAsync(hocPhi_LopHocs);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteHocPhiTronGoiAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.HocPhiTronGois
                                    .Where(x => x.HocPhiTronGoiId == id)
                                    .SingleOrDefaultAsync();

            item.IsRemoved = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var hocPhi_LopHocs = _context.HocPhiTronGoi_LopHocs
                                            .Where(x => x.HocPhiTronGoiId == item.HocPhiTronGoiId);
            _context.HocPhiTronGoi_LopHocs.RemoveRange(hocPhi_LopHocs);

            var saveResult = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<HocPhiTronGoiViewModel>> GetHocPhiTronGoiAsync()
        {
            var hocPhis = await _context.HocPhiTronGois
                .Where(x => !x.IsRemoved)
                .Include(x => x.HocVien)
                .Include(x => x.HocPhiTronGoi_LopHocs)
                .ThenInclude(x => x.LopHoc)
                .AsNoTracking()
                .ToListAsync();

            return hocPhis.Select(hocPhi => _entityConverter.ToHocPhiTronGoiViewModel(hocPhi)).ToList();
        }

        public async Task<List<HocPhiTronGoiViewModel>> GetHocPhiTronGoiAsync(Guid hocVienId, Guid lopHocId)
        {
            var hocPhis = await _context.HocPhiTronGois
                .Include(x => x.HocVien)
                .Include(x => x.HocPhiTronGoi_LopHocs)
                .ThenInclude(x => x.LopHoc)
                .Where(x => !x.IsRemoved && 
                             x.HocVienId == hocVienId && 
                             x.HocPhiTronGoi_LopHocs.Any(m => m.LopHocId == lopHocId))
                .AsNoTracking()
                .ToListAsync();

            return hocPhis.Select(hocPhi => _entityConverter.ToHocPhiTronGoiViewModel(hocPhi)).ToList();
        }

        public async Task<HocPhiTronGoiViewModel> GetHocPhiTronGoiDetailAsync(Guid id)
        {
            var hocPhi = await _context.HocPhiTronGois
                .Include(x => x.HocVien)
                .Include(x => x.HocPhiTronGoi_LopHocs)
                .ThenInclude(x => x.LopHoc)
                .FirstOrDefaultAsync(x => x.HocPhiTronGoiId == id);

            return _entityConverter.ToHocPhiTronGoiViewModel(hocPhi);
        }

        public async Task<bool> ToggleHocPhiTronGoiAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.HocPhiTronGois
                                    .Where(x => x.HocPhiTronGoiId == id)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = !item.IsDisabled;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<Guid> UpdateHocPhiTronGoiAsync(UpdateHocPhiTronGoiInputModel input, string loggedEmployee)
        {
            var item = await _context.HocPhiTronGois
                                    .FirstOrDefaultAsync(x => x.HocPhiTronGoiId == input.HocPhiTronGoiId);

            _entityConverter.MappingEntityHocPhiTronGoi(input, item, loggedEmployee);

            var oldLopHoc = _context.HocPhiTronGoi_LopHocs
                                    .Where(x => x.HocPhiTronGoiId == input.HocPhiTronGoiId);
            _context.HocPhiTronGoi_LopHocs.RemoveRange(oldLopHoc);


            var hocPhi_LopHocs = _entityConverter.ToHocPhiTronGoi_LopHocList(input.HocPhiTronGoiId, input.LopHocList, loggedEmployee);
            await _context.HocPhiTronGoi_LopHocs.AddRangeAsync(hocPhi_LopHocs);

            var saveResult = await _context.SaveChangesAsync();
            return item.HocPhiTronGoiId;
        }
    }
}
