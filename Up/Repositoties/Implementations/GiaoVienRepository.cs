using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Converters;
using Up.Data;
using Up.Data.Entities;
using Up.Enums;
using Up.Models;

namespace Up.Repositoties
{
    public class GiaoVienRepository : BaseRepository, IGiaoVienRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public GiaoVienRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateGiaoVienAsync(CreateGiaoVienInputModel input, string loggedEmployee)
        {
            var giaoVien = _entityConverter.ToEntityGiaoVien(input, loggedEmployee);

            _context.GiaoViens.Add(giaoVien);

            foreach (var item in input.LoaiNhanVien_CheDo)
            {
                var nhanVien_ViTri = new NhanVien_ViTri
                {
                    NhanVienId = giaoVien.GiaoVienId,
                    CreatedBy = loggedEmployee,
                    CheDoId = item.LoaiCheDo.LoaiCheDoId,
                    ViTriId = item.LoaiGiaoVien.LoaiGiaoVienId
                };
                await _context.NhanVien_ViTris.AddAsync(nhanVien_ViTri);
            }

            await _context.SaveChangesAsync();
            return giaoVien.GiaoVienId;
        }

        public async Task<bool> DeleteGiaoVienAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.GiaoViens
                                    .FindAsync(id);

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<GiaoVienViewModel>> GetAllNhanVienAsync()
        {
            var nhanViens = await _context.GiaoViens
                .Include(x => x.NgayLamViec)
                .Include(x => x.NhanVien_ViTris)
                    .ThenInclude(x => x.CheDo)
                .Include(x => x.NhanVien_ViTris)
                    .ThenInclude(x => x.ViTri)
                .Where(x => !x.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return nhanViens.Select(nhanVien => _entityConverter.ToGiaoVienViewModel(nhanVien)).ToList();
        }

        public async Task<IList<Guid>> GetGiaoVienIdsByNgayLamViec(Guid ngayLamViecId)
        {
            return await _context.GiaoViens
                .Where(x => x.NgayLamViecId == ngayLamViecId)
                .Select(x => x.GiaoVienId)
                .ToListAsync();
        }

        public async Task<List<GiaoVienViewModel>> GetGiaoVienOnlyAsync()
        {
            var giaoViens = await _context.GiaoViens
                .Include(x => x.NgayLamViec)
                .Include(x => x.NhanVien_ViTris)
                    .ThenInclude(x => x.CheDo)
                .Include(x => x.NhanVien_ViTris)
                    .ThenInclude(x => x.ViTri)
                .Where(x => !x.IsDisabled &&
                            x.NhanVien_ViTris.Any(m => m.CheDoId == LoaiNhanVienEnums.GiaoVien.ToId()))
                .AsNoTracking()
                .ToListAsync();

            return giaoViens.Select(giaoVien => _entityConverter.ToGiaoVienViewModel(giaoVien)).ToList();
        }

        public async Task<GiaoVienViewModel> GetNhanVienDetailAsync(Guid id)
        {
            var nhanVien = await _context.GiaoViens
                .Include(x => x.NgayLamViec)
                .Include(x => x.NhanVien_ViTris)
                    .ThenInclude(x => x.CheDo)
                .Include(x => x.NhanVien_ViTris)
                    .ThenInclude(x => x.ViTri)
                .FirstOrDefaultAsync(x => x.GiaoVienId == id);

            return _entityConverter.ToGiaoVienViewModel(nhanVien);
        }

        public async Task<Guid> UpdateGiaoVienAsync(UpdateGiaoVienInputModel input, string loggedEmployee)
        {
            var item = await _context.GiaoViens
                                        .Where(x => x.GiaoVienId == input.GiaoVienId)
                                        .SingleOrDefaultAsync();

            _entityConverter.MappingEntityGiaoVien(input, item, loggedEmployee);

            var viTris = _context.NhanVien_ViTris.Where(x => x.NhanVienId == item.GiaoVienId);
            _context.NhanVien_ViTris.RemoveRange(viTris);

            foreach (var viTri in input.LoaiNhanVien_CheDo)
            {
                var nhanVien_ViTri = new NhanVien_ViTri
                {
                    NhanVienId = item.GiaoVienId,
                    CreatedBy = loggedEmployee,
                    CheDoId = viTri.LoaiCheDo.LoaiCheDoId,
                    ViTriId = viTri.LoaiGiaoVien.LoaiGiaoVienId
                };
                await _context.NhanVien_ViTris.AddAsync(nhanVien_ViTri);
            }

            await _context.SaveChangesAsync();
            return item.GiaoVienId;
        }
    }
}
