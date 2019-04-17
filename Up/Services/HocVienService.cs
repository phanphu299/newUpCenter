using Microsoft.EntityFrameworkCore;
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
                    QuanHe = _context.QuanHes.FindAsync(hocVien.QuanHeId).Result.Name,
                };
            }
            catch (Exception exception)
            {
                throw new Exception("Lỗi khi tạo mới: " + exception.Message);
            }
        }

        public async Task<bool> DeleteHocVienAsync(Guid HocVienId, string LoggedEmployee)
        {
            //var hocVien = await _context.HocViens.Where(x => x.HocVienId == HocVienId).ToListAsync();
            //if (lopHoc.Any())
            //    throw new Exception("Hãy xóa những lớp học có học phí này trước !!!");

            var item = await _context.HocViens
                                    .Where(x => x.HocVienId == HocVienId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Học Viên !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<HocVienViewModel>> GetHocVienAsync()
        {
            return await _context.HocViens
                .Where(x => x.IsDisabled == false)
                .Select(x => new HocVienViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    EnglishName  = x.EnglishName,
                    FacebookAccount = x.FacebookAccount,
                    FullName = x.FullName,
                    HocVienId = x.HocVienId,
                    IsAppend = x.IsAppend,
                    IsDisabled = x.IsDisabled,
                    NgaySinh = x.NgaySinh.ToString("dd/MM/yyyy"),
                    ParentFacebookAccount = x.ParentFacebookAccount,
                    ParentFullName = x.ParentFullName,
                    ParentPhone = x.ParentPhone,
                    Phone = x.Phone,
                    QuanHe = x.QuanHe.Name,
                    QuanHeId = x.QuanHeId,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<HocVienViewModel> UpdateHocVienAsync(Guid HocVienId, string FullName, string Phone, string FacebookAccount,
           string ParentFullName, string ParentPhone, string ParentFacebookAccount, Guid QuanHeId, string EnglishName,
           DateTime NgaySinh, bool IsAppend, string LoggedEmployee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FullName) || QuanHeId == null || string.IsNullOrWhiteSpace(Phone)
                    || string.IsNullOrWhiteSpace(FacebookAccount) || string.IsNullOrWhiteSpace(ParentFullName) 
                    || string.IsNullOrWhiteSpace(ParentPhone) || string.IsNullOrWhiteSpace(ParentFacebookAccount) 
                    || string.IsNullOrWhiteSpace(EnglishName) || NgaySinh == null)
                    throw new Exception("Tên Học Viên, Quan Hệ, SĐT, FB, Tên Phụ Huynh, Quan Hệ, SĐT Phụ Huynh, FB Phụ Huynh, Ngày Khai Giảng không được để trống !!!");

                var item = await _context.HocViens
                                        .Where(x => x.HocVienId == HocVienId)
                                        .SingleOrDefaultAsync();

                if (item == null)
                    throw new Exception("Không tìm thấy Học Viên!!!");

                item.FullName = FullName;
                item.QuanHeId = QuanHeId;
                item.Phone = Phone;
                item.FacebookAccount = FacebookAccount;
                item.ParentFullName = ParentFullName;
                item.ParentPhone = ParentPhone;
                item.ParentFacebookAccount = ParentFacebookAccount;
                item.EnglishName = EnglishName;
                item.NgaySinh = NgaySinh;
                item.UpdatedBy = LoggedEmployee;
                item.UpdatedDate = DateTime.Now;
                item.IsAppend = IsAppend;

                await _context.SaveChangesAsync();
                return new HocVienViewModel
                {
                    FullName = item.FullName,
                    NgaySinh = item.NgaySinh.ToString("dd/MM/yyyy"),
                    EnglishName = item.EnglishName,
                    ParentFacebookAccount = item.ParentFacebookAccount,
                    ParentPhone = item.ParentPhone,
                    IsAppend = item.IsAppend,
                    HocVienId = item.HocVienId,
                    ParentFullName = item.ParentFullName,
                    FacebookAccount = item.FacebookAccount,
                    Phone = item.Phone,
                    QuanHeId = item.QuanHeId,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate?.ToString("dd/MM/yyyy"),
                    QuanHe = _context.QuanHes.FindAsync(item.QuanHeId).Result.Name,
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
