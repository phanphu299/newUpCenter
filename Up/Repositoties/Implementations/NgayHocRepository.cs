using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Converters;
using Up.Data;
using Up.Data.Entities;
using Up.Models;

namespace Up.Repositoties
{
    public class NgayHocRepository : BaseRepository, INgayHocRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;

        public NgayHocRepository(
            ApplicationDbContext context,
            EntityConverter entityConverter,
            UserManager<IdentityUser> userManager)
            : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateNgayHocAsync(string name, string loggedEmployee)
        {
            var ngayHoc = new NgayHoc
            {
                Name = name,
                CreatedBy = loggedEmployee
            };

            _context.NgayHocs.Add(ngayHoc);

            await _context.SaveChangesAsync();

            return ngayHoc.NgayHocId;
        }

        public async Task<bool> CreateUpdateHocVien_NgayHocAsync(HocVien_NgayHocInputModel input, string loggedEmployee)
        {
            var hocVien_NgayHoc = await _context.HocVien_NgayHocs
                .FirstOrDefaultAsync(x => x.LopHocId == input.LopHocId && x.HocVienId == input.HocVienId);
            if (hocVien_NgayHoc != null)
            {
                hocVien_NgayHoc.NgayBatDau = input.NgayBatDauDate;
                hocVien_NgayHoc.NgayKetThuc = input.NgayKetThucDate;
                hocVien_NgayHoc.UpdatedBy = loggedEmployee;
                hocVien_NgayHoc.UpdatedDate = DateTime.Now;
            }
            else
            {
                var HV_NgayHoc = new HocVien_NgayHoc 
                {
                    HocVienId = input.HocVienId,
                    LopHocId = input.LopHocId,
                    NgayBatDau = input.NgayBatDauDate,
                    NgayKetThuc = input.NgayKetThucDate,
                    CreatedBy = loggedEmployee
                };

                _context.HocVien_NgayHocs.Add(HV_NgayHoc);
            }

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> DeleteNgayHocAsync(Guid ngayHocId, string loggedEmployee)
        {
            var item = await _context.NgayHocs
                                    .Where(x => x.NgayHocId == ngayHocId)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<HocVien_NgayHocViewModel> GetHocVien_NgayHocByHocVienAsync(Guid hocVienId, Guid lopHocId)
        {
            var ngayHoc = await _context.HocVien_NgayHocs
                                        .FirstOrDefaultAsync(x => x.HocVienId == hocVienId && x.LopHocId == lopHocId);

            return _entityConverter.ToHocVien_NgayHocViewModel(ngayHoc);
        }

        public async Task<List<NgayHocViewModel>> GetNgayHocAsync()
        {
            var ngayHocs = await _context.NgayHocs
                .Where(x => x.IsDisabled == false)
                .AsNoTracking()
                .ToListAsync();

            return ngayHocs.Select(ngayHoc => _entityConverter.ToNgayHocViewModel(ngayHoc)).ToList();
        }

        public async Task<NgayHocViewModel> GetNgayHocDetailAsync(Guid id)
        {
            var ngayHoc = await _context.NgayHocs.FirstOrDefaultAsync(x => x.NgayHocId == id);

            return _entityConverter.ToNgayHocViewModel(ngayHoc);
        }

        public async Task<bool> UpdateNgayHocAsync(NgayHocViewModel input, string loggedEmployee)
        {
            var item = await _context.NgayHocs
                                    .Where(x => x.NgayHocId == input.NgayHocId)
                                    .SingleOrDefaultAsync();

            item.Name = input.Name;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
