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
    public class LoaiGiaoVienRepository : BaseRepository, ILoaiGiaoVienRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public LoaiGiaoVienRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateLoaiGiaoVienAsync(CreateLoaiGiaoVienInputModel input, string loggedEmployee)
        {
            var loaiGV = _entityConverter.ToEntityLoaiGiaoVien(input, loggedEmployee);

            _context.LoaiGiaoViens.Add(loaiGV);

            await _context.SaveChangesAsync();
            return loaiGV.LoaiGiaoVienId;
        }

        public async Task<bool> DeleteLoaiGiaoVienAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.LoaiGiaoViens
                                    .Where(x => x.LoaiGiaoVienId == id)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<LoaiGiaoVienViewModel>> GetLoaiGiaoVienAsync()
        {
            var loaiGVs = await _context.LoaiGiaoViens
                .Where(x => !x.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return loaiGVs.Select(loaiGV => _entityConverter.ToLoaiGiaoVienViewModel(loaiGV)).ToList();
        }

        public async Task<LoaiGiaoVienViewModel> GetLoaiGiaoVienDetailAsync(Guid id)
        {
            var loaiGV = await _context.LoaiGiaoViens.FirstOrDefaultAsync(x => x.LoaiGiaoVienId == id);

            return _entityConverter.ToLoaiGiaoVienViewModel(loaiGV);
        }

        public async Task<IList<Guid>> GetNhanVienIdsAsync(Guid id)
        {
            return await _context.NhanVien_ViTris
                .Where(m => m.ViTriId == id)
                .Select(x => x.NhanVienId)
                .ToListAsync();
        }

        public async Task<IList<Guid>> GetNhanVienIdsByLoaiCheDoAsync(Guid id)
        {
            return await _context.NhanVien_ViTris
                .Where(m => m.CheDoId == id)
                .Select(x => x.NhanVienId)
                .ToListAsync();
        }

        public async Task<bool> UpdateLoaiGiaoVienAsync(UpdateLoaiGiaoVienInputModel input, string loggedEmployee)
        {
            var item = await _context.LoaiGiaoViens
                                    .Where(x => x.LoaiGiaoVienId == input.LoaiGiaoVienId)
                                    .SingleOrDefaultAsync();

            _entityConverter.MappingEntityLoaiGiaoVien(input, item, loggedEmployee);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
