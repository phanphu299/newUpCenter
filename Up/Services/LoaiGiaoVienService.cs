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

    public class LoaiGiaoVienService : ILoaiGiaoVienService
    {
        private readonly ApplicationDbContext _context;

        public LoaiGiaoVienService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoaiGiaoVienViewModel> CreateLoaiGiaoVienAsync(string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Loại GIáo Viên không được để trống !!!");

            LoaiGiaoVien loaiGiaoVien = new LoaiGiaoVien();
            loaiGiaoVien.LoaiGiaoVienId = new Guid();
            loaiGiaoVien.Name = Name;
            loaiGiaoVien.CreatedBy = LoggedEmployee;
            loaiGiaoVien.CreatedDate = DateTime.Now;

            _context.LoaiGiaoViens.Add(loaiGiaoVien);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Loại Giáo Viên !!!");
            return new LoaiGiaoVienViewModel { LoaiGiaoVienId = loaiGiaoVien.LoaiGiaoVienId, Name = loaiGiaoVien.Name, CreatedBy = loaiGiaoVien.CreatedBy, CreatedDate = loaiGiaoVien.CreatedDate.ToString("dd/MM/yyyy") };
        }

        public async Task<bool> DeleteLoaiGiaoVienAsync(Guid LoaiGiaoVienId, string LoggedEmployee)
        {
            var giaoVien = await _context.GiaoViens.Where(x => x.LoaiGiaoVienId == LoaiGiaoVienId).ToListAsync();
            if (giaoVien.Any())
                throw new Exception("Hãy xóa những giáo viên thuộc loại này trước !!!");

            var item = await _context.LoaiGiaoViens
                                    .Where(x => x.LoaiGiaoVienId == LoaiGiaoVienId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Loại giáo viên !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<LoaiGiaoVienViewModel>> GetLoaiGiaoVienAsync()
        {
            return await _context.LoaiGiaoViens
                .Where(x => x.IsDisabled == false)
                .Select(x => new LoaiGiaoVienViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    LoaiGiaoVienId = x.LoaiGiaoVienId,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateLoaiGiaoVienAsync(Guid LoaiGiaoVienId, string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Loại Giáo Viên không được để trống !!!");

            var item = await _context.LoaiGiaoViens
                                    .Where(x => x.LoaiGiaoVienId == LoaiGiaoVienId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Loại Giáo Viên !!!");

            item.Name = Name;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
