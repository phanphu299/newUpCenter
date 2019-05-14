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
    public class QuanHeService : IQuanHeService
    {
        private readonly ApplicationDbContext _context;

        public QuanHeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<QuanHeViewModel> CreateQuanHeAsync(string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Quan Hệ không được để trống !!!");

            QuanHe quanHe = new QuanHe();
            quanHe.QuanHeId = new Guid();
            quanHe.Name = Name;
            quanHe.CreatedBy = LoggedEmployee;
            quanHe.CreatedDate = DateTime.Now;

            _context.QuanHes.Add(quanHe);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Quan Hệ !!!");
            return new QuanHeViewModel { QuanHeId = quanHe.QuanHeId, Name = quanHe.Name, CreatedBy = quanHe.CreatedBy, CreatedDate = quanHe.CreatedDate.ToString("dd/MM/yyyy") };
        }

        public async Task<bool> DeleteQuanHeAsync(Guid QuanHeId, string LoggedEmployee)
        {
            var hocVien = await _context.HocViens.Where(x => x.QuanHeId == QuanHeId).ToListAsync();
            if (hocVien.Any())
                throw new Exception("Hãy xóa những học viên có quan hệ này trước !!!");

            var item = await _context.QuanHes
                                    .Where(x => x.QuanHeId == QuanHeId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Quan Hệ !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<QuanHeViewModel>> GetQuanHeAsync()
        {
            return await _context.QuanHes
                .Where(x => x.IsDisabled == false)
                .Select(x => new QuanHeViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    QuanHeId = x.QuanHeId,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateQuanHeAsync(Guid QuanHeId, string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Quan Hệ không được để trống !!!");

            var item = await _context.QuanHes
                                    .Where(x => x.QuanHeId == QuanHeId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Quan Hệ !!!");

            item.Name = Name;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
