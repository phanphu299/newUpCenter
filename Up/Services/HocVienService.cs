using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Data;
using Up.Data.Entities;
using Up.Models;

namespace Up.Services
{
    public class HocVienService : IHocVienService
    {
        private readonly ApplicationDbContext _context;

        public HocVienService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HocVienViewModel> CreateHocVienAsync(string FullName, string Phone, string FacebookAccount, 
            string ParentFullName, string ParentPhone, string ParentFacebookAccount, Guid QuanHeId, 
            string EnglishName, DateTime NgaySinh, bool IsAppend, string LoggedEmployee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FullName) || QuanHeId == null || string.IsNullOrWhiteSpace(Phone) ||
                    string.IsNullOrWhiteSpace(FacebookAccount) || string.IsNullOrWhiteSpace(ParentFullName) || string.IsNullOrWhiteSpace(ParentPhone) ||
                    string.IsNullOrWhiteSpace(ParentFacebookAccount) || string.IsNullOrWhiteSpace(EnglishName) || NgaySinh == null)
                    throw new Exception("Tên Học Viên, Phone, Fb, Tên Phụ Huynh, Phone Của Phụ Huynh, Fb Của Phụ Huynh, Tên Tiếng Anh, Ngày Sinh, Quan Hệ " +
                        "không được để trống !!!");

                HocVien hocVien = new HocVien();
                hocVien.FullName = FullName;
                hocVien.Phone = Phone;
                hocVien.FacebookAccount = FacebookAccount;
                hocVien.ParentFullName = ParentFullName;
                hocVien.ParentPhone = ParentPhone;
                hocVien.ParentFacebookAccount = ParentFacebookAccount;
                hocVien.QuanHeId = QuanHeId;
                hocVien.EnglishName = EnglishName;
                hocVien.NgaySinh = NgaySinh;
                hocVien.IsAppend = IsAppend;
                hocVien.CreatedBy = LoggedEmployee;
                hocVien.CreatedDate = DateTime.Now;

                _context.HocViens.Add(hocVien);

                await _context.SaveChangesAsync();

                return new HocVienViewModel
                {
                    FullName = hocVien.FullName,
                    IsAppend = hocVien.IsAppend,
                    NgaySinh = hocVien.NgaySinh.ToString("dd/MM/yyyy"),
                    EnglishName = hocVien.EnglishName,
                    QuanHeId = hocVien.QuanHeId,
                    ParentFacebookAccount = hocVien.ParentFacebookAccount,
                    CreatedBy = hocVien.CreatedBy,
                    CreatedDate = hocVien.CreatedDate.ToString("dd/MM/yyyy"),
                    FacebookAccount = hocVien.FacebookAccount,
                    IsDisabled = hocVien.IsDisabled,
                    ParentFullName = hocVien.ParentFullName,
                    ParentPhone = hocVien.ParentPhone,
                    Phone = hocVien.Phone,
                    HocVienId = hocVien.HocVienId,
                    QuanHe = _context.QuanHes.Find(hocVien.QuanHeId).Name,
                };
            }
            catch (Exception exception)
            {
                throw new Exception("Lỗi khi tạo mới: " + exception.Message);
            }
        }

        public Task<bool> DeleteHocVienAsync(Guid HocVienId, string LoggedEmployee)
        {
            throw new NotImplementedException();
        }

        public Task<List<HocVienViewModel>> GetHocVienAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateHocVienAsync(Guid HocVienId, string Name, string LoggedEmployee)
        {
            throw new NotImplementedException();
        }
    }
}
