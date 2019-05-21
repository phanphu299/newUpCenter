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

    public class KhoaHocService : IKhoaHocService
    {
        private readonly ApplicationDbContext _context;

        public KhoaHocService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<KhoaHocViewModel> CreateKhoaHocAsync(string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Khóa Học không được để trống !!!");

            KhoaHoc khoaHoc = new KhoaHoc();
            khoaHoc.KhoaHocId = new Guid();
            khoaHoc.Name = Name;
            khoaHoc.CreatedBy = LoggedEmployee;
            khoaHoc.CreatedDate = DateTime.Now;

            _context.KhoaHocs.Add(khoaHoc);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Khóa Học !!!");
            return new KhoaHocViewModel { KhoaHocId = khoaHoc.KhoaHocId, Name = khoaHoc.Name, CreatedBy = khoaHoc.CreatedBy, CreatedDate = khoaHoc.CreatedDate.ToString("dd/MM/yyyy") };
        }

        public async Task<bool> DeleteKhoaHocAsync(Guid KhoaHocId, string LoggedEmployee)
        {
            var lopHoc = await _context.LopHocs.Where(x => x.KhoaHocId == KhoaHocId).ToListAsync();
            if (lopHoc.Any())
                throw new Exception("Hãy xóa những lớp học thuộc khóa học này trước !!!");

            var item = await _context.KhoaHocs
                                    .FindAsync(KhoaHocId);

            if (item == null)
                throw new Exception("Không tìm thấy Khóa Học !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<KhoaHocViewModel>> GetKhoaHocAsync()
        {
            return await _context.KhoaHocs
                .Where(x => x.IsDisabled == false)
                .Select(x => new KhoaHocViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    KhoaHocId = x.KhoaHocId,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateKhoaHocAsync(Guid KhoaHocId, string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Khóa Học không được để trống !!!");

            var item = await _context.KhoaHocs
                                    .Where(x => x.KhoaHocId == KhoaHocId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Khóa Học !!!");

            item.Name = Name;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
