
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

    public class NhanVienKhacService : INhanVienKhacService
    {
        private readonly ApplicationDbContext _context;

        public NhanVienKhacService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<NhanVienKhacViewModel> CreateNhanVienAsync(string Name, string Phone, double BasicSalary, string FacebookAccount, string DiaChi, string CMND, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(FacebookAccount) || string.IsNullOrWhiteSpace(DiaChi) ||  string.IsNullOrWhiteSpace(CMND))
                throw new Exception("Tên, SĐT, FB, Địa Chỉ, CMND không được để trống !!!");

            NhanVienKhac nhanVien = new NhanVienKhac {
                BasicSalary = BasicSalary,
                CMND = CMND,
                CreatedBy = LoggedEmployee,
                CreatedDate = DateTime.Now,
                DiaChi = DiaChi,
                FacebookAccount = FacebookAccount,
                Name = Name,
                NhanVienKhacId = new Guid(),
                Phone = Phone
            };

            _context.NhanVienKhacs.Add(nhanVien);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Giáo Viên !!!");
            return new NhanVienKhacViewModel
            {
                Name = nhanVien.Name,
                BasicSalary = nhanVien.BasicSalary,
                CMND = nhanVien.CMND,
                DiaChi = nhanVien.DiaChi,
                FacebookAccount = nhanVien.FacebookAccount,
                Phone = nhanVien.Phone,
                CreatedBy = nhanVien.CreatedBy,
                CreatedDate = nhanVien.CreatedDate.ToString("dd/MM/yyyy")
            };
        }

        public async Task<bool> DeleteNhanVienAsync(Guid NhanVienKhacId, string LoggedEmployee)
        {
            var item = await _context.NhanVienKhacs
                                    .FindAsync(NhanVienKhacId);

            if (item == null)
                throw new Exception("Không tìm thấy Nhân Viên !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<NhanVienKhacViewModel>> GetNhanVienAsync()
        {
            return await _context.NhanVienKhacs
                .Where(x => x.IsDisabled == false)
                .Select(x => new NhanVienKhacViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    NhanVienKhacId = x.NhanVienKhacId,
                    Phone = x.Phone,
                    FacebookAccount = x.FacebookAccount,
                    DiaChi = x.DiaChi,
                    CMND = x.CMND,
                    BasicSalary = x.BasicSalary,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<NhanVienKhacViewModel> UpdateNhanVienAsync(Guid NhanVienKhacId, string Name, string Phone, double BasicSalary, string FacebookAccount, string DiaChi, string CMND, string LoggedEmployee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(FacebookAccount) || string.IsNullOrWhiteSpace(DiaChi) || string.IsNullOrWhiteSpace(CMND))
                    throw new Exception("Tên, SĐT, FB, Địa Chỉ, CMND không được để trống !!!");

                var item = await _context.NhanVienKhacs
                                        .Where(x => x.NhanVienKhacId == NhanVienKhacId)
                                        .SingleOrDefaultAsync();

                if (item == null)
                    throw new Exception("Không tìm thấy Nhân Viên!!!");

                item.Name = Name;
                item.Phone = Phone;
                item.FacebookAccount = FacebookAccount;
                item.DiaChi = DiaChi;
                item.CMND = CMND;
                item.BasicSalary = BasicSalary;
                item.UpdatedBy = LoggedEmployee;
                item.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                return new NhanVienKhacViewModel
                {
                    NhanVienKhacId = item.NhanVienKhacId,
                    BasicSalary = item.BasicSalary,
                    CMND = item.CMND,
                    DiaChi = item.DiaChi,
                    Name = item.Name,
                    FacebookAccount = item.FacebookAccount,
                    Phone = item.Phone,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate?.ToString("dd/MM/yyyy"),
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate.ToString("dd/MM/yyyy"),
                };
            }
            catch (Exception exception)
            {
                throw new Exception("Cập nhật lỗi: " + exception.Message);
            }
        }
    }
}
