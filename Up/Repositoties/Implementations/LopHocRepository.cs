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
    public class LopHocRepository : BaseRepository, ILopHocRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;

        public LopHocRepository(
            ApplicationDbContext context,
            EntityConverter entityConverter,
            UserManager<IdentityUser> userManager)
            : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateLopHocAsync(CreateLopHocInputModel input, string loggedEmployee)
        {
            var lopHoc = _entityConverter.ToEntityLopHoc(input, loggedEmployee);

            _context.LopHocs.Add(lopHoc);
            await _context.SaveChangesAsync();

            return lopHoc.LopHocId;
        }

        public async Task<bool> DeleteLopHocAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.LopHocs
                                    .Where(x => x.LopHocId == id)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var result = await _context.SaveChangesAsync();
            return result == 1;
        }

        public async Task<List<LopHocViewModel>> GetAvailableLopHocAsync(int? thang = null, int? nam = null)
        {
            return await _context.LopHocs
                .Include(x => x.LopHoc_HocPhis)
                .Where(x => !x.IsDisabled && !x.IsCanceled && !x.IsGraduated)
                .OrderBy(x => x.Name)
                .Select(x => new LopHocViewModel
                {
                    Name = x.Name,
                    LopHocId = x.LopHocId,
                    HocPhi = !x.LopHoc_HocPhis.Any(m => m.Nam == nam && m.Thang == thang) ? null :
                    new HocPhiViewModel
                    {
                        HocPhiId = x.LopHoc_HocPhis.FirstOrDefault(m => m.Nam == nam && m.Thang == thang).HocPhiId,
                        Gia = x.LopHoc_HocPhis.FirstOrDefault(m => m.Nam == nam && m.Thang == thang).HocPhi.Gia
                    }
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<LopHocViewModel>> GetGraduatedAndCanceledLopHocAsync()
        {
            return await _context.LopHocs
                .Where(x => !x.IsDisabled && (x.IsCanceled || x.IsGraduated))
                .Select(x => new LopHocViewModel
                {
                    Name = x.Name,
                    LopHocId = x.LopHocId
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IList<Guid>> GetHocVienIdAsync(Guid id)
        {
            return await _context.HocVien_LopHocs
                                        .Where(x => x.LopHocId == id)
                                        .Select(x => x.HocVienId)
                                        .ToListAsync();
        }

        public async Task<List<LopHocViewModel>> GetLopHocAsync()
        {
            var lopHocs = await _context.LopHocs
                .Include(x => x.GioHoc)
                .Include(x => x.KhoaHoc)
                .Include(x => x.NgayHoc)
                .Where(x => !x.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return lopHocs.Select(lopHoc => _entityConverter.ToLopHocViewModel(lopHoc)).ToList();
        }

        public async Task<List<LopHocViewModel>> GetLopHocByHocVienIdAsync(Guid hocVienId)
        {
            return await _context.LopHocs
                .Where(x => x.HocVien_LopHocs.Any(m => m.HocVienId == hocVienId))
                .Select(x => new LopHocViewModel
                {
                    Name = x.Name,
                    LopHocId = x.LopHocId
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<LopHocViewModel> GetLopHocDetailAsync(Guid id)
        {
            var lopHoc = await _context.LopHocs
                .Select(x => new LopHocViewModel
                {
                    LopHocId = x.LopHocId,
                    Name = x.Name,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return lopHoc;
        }

        public async Task<bool> ToggleHuyLopAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.LopHocs
                                         .Where(x => x.LopHocId == id)
                                         .SingleOrDefaultAsync();

            item.IsCanceled = !item.IsCanceled;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var _hocVien_NgayHoc = await _context.HocVien_NgayHocs
                                            .Where(x => x.LopHocId == id && x.NgayKetThuc == null)
                                            .ToListAsync();

            if (item.IsCanceled == true)
            {
                foreach (var ngayHoc in _hocVien_NgayHoc)
                {
                    ngayHoc.NgayKetThuc = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ToggleTotNghiepAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.LopHocs
                                         .Where(x => x.LopHocId == id)
                                         .SingleOrDefaultAsync();

            item.IsGraduated = !item.IsGraduated;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var _hocVien_NgayHoc = await _context.HocVien_NgayHocs
                                            .Where(x => x.LopHocId == id && x.NgayKetThuc == null)
                                            .ToListAsync();

            if (item.IsGraduated == true)
            {
                foreach (var ngayHoc in _hocVien_NgayHoc)
                {
                    ngayHoc.NgayKetThuc = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateHocPhiLopHocAsync(Guid lopHocId, Guid hocPhiId, int thang, int nam, string loggedEmployee)
        {
            var item = await _context.LopHoc_HocPhis
                                        .Where(x => x.LopHocId == lopHocId && x.Thang == thang && x.Nam == nam)
                                        .SingleOrDefaultAsync();

            if (item == null)
            {
                var thongKe = _entityConverter.ToEntityHocPhi(lopHocId, hocPhiId, thang, nam);
                await _context.LopHoc_HocPhis.AddAsync(thongKe);
            }
            else
            {
                item.HocPhiId = hocPhiId;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Guid> UpdateLopHocAsync(UpdateLopHocInputModel input, string loggedEmployee)
        {
            var item = await _context.LopHocs
                                    .Where(x => x.LopHocId == input.LopHocId)
                                    .SingleOrDefaultAsync();

            _entityConverter.MappingEntityLopHoc(input, item, loggedEmployee);

            await _context.SaveChangesAsync();
            return item.LopHocId;
        }
    }
}
