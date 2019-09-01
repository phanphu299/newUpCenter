
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

    public class LoaiCheDoService : ILoaiCheDoService
    {
        private readonly ApplicationDbContext _context;

        public LoaiCheDoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoaiCheDoViewModel> CreateLoaiCheDoAsync(string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Loại Chế Độ không được để trống !!!");

            LoaiCheDo loaiCheDo = new LoaiCheDo();
            loaiCheDo.LoaiCheDoId = new Guid();
            loaiCheDo.Name = Name;
            loaiCheDo.CreatedBy = LoggedEmployee;
            loaiCheDo.CreatedDate = DateTime.Now;

            _context.LoaiCheDos.Add(loaiCheDo);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Loại Chế Độ !!!");
            return new LoaiCheDoViewModel { LoaiCheDoId = loaiCheDo.LoaiCheDoId, Name = loaiCheDo.Name, CreatedBy = loaiCheDo.CreatedBy, CreatedDate = loaiCheDo.CreatedDate.ToString("dd/MM/yyyy") };
        }

        public async Task<bool> DeleteLoaiCheDoAsync(Guid LoaiCheDoId, string LoggedEmployee)
        {
            var giaoVien = await _context.GiaoViens.Where(x => x.NhanVien_ViTris.Any(m => m.CheDoId == LoaiCheDoId)).ToListAsync();
            if (giaoVien.Any())
                throw new Exception("Hãy xóa những nhân viên thuộc loại này trước !!!");

            var item = await _context.LoaiCheDos
                                    .Where(x => x.LoaiCheDoId == LoaiCheDoId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Loại Chế Độ !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<LoaiCheDoViewModel>> GetLoaiCheDoAsync()
        {
            return await _context.LoaiCheDos
                .Where(x => x.IsDisabled == false)
                .Select(x => new LoaiCheDoViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    LoaiCheDoId = x.LoaiCheDoId,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateLoaiCheDoAsync(Guid LoaiCheDoId, string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Loại Chế Độ không được để trống !!!");

            var item = await _context.LoaiCheDos
                                    .Where(x => x.LoaiCheDoId == LoaiCheDoId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Loại Chế Độ !!!");

            item.Name = Name;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
