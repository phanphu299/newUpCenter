namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Data.Entities;
    using Up.Models;

    public class GioHocService: IGioHocService
    {
        private readonly ApplicationDbContext _context;

        public GioHocService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GioHocViewModel> CreateGioHocAsync(string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Giờ Học không được để trống !!!");

            GioHoc gioHoc = new GioHoc();
            gioHoc.GioHocId = new Guid();
            gioHoc.Name = Name;
            gioHoc.CreatedBy = LoggedEmployee;
            gioHoc.CreatedDate = DateTime.Now;

            _context.GioHocs.Add(gioHoc);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Giờ Học !!!");
            return new GioHocViewModel { GioHocId = gioHoc.GioHocId, Name = gioHoc.Name, CreatedBy = gioHoc.CreatedBy, CreatedDate = gioHoc.CreatedDate.ToString("dd/MM/yyyy") };
        }

        public async Task<bool> DeleteGioHocAsync(Guid GioHocId, string LoggedEmployee)
        {
            var lopHoc = await _context.LopHocs.Where(x => x.GioHocId == GioHocId).ToListAsync();
            if (lopHoc.Any())
                throw new Exception("Hãy xóa những lớp học thuộc giờ học này trước !!!");

            var item = await _context.GioHocs
                                    .Where(x => x.GioHocId == GioHocId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Giờ Học !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<GioHocViewModel>> GetGioHocAsync()
        {
            return await _context.GioHocs
                .Where(x => x.IsDisabled == false)
                .Select(x => new GioHocViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    GioHocId = x.GioHocId,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateGioHocAsync(Guid GioHocId, string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Giờ Học không được để trống !!!");

            var item = await _context.GioHocs
                                    .Where(x => x.GioHocId == GioHocId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Giờ Học !!!");

            item.Name = Name;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
