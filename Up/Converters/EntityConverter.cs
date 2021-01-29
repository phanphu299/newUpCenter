using System;
using System.Linq;
using Up.Data.Entities;
using Up.Extensions;
using Up.Models;

namespace Up.Converters
{
    public class EntityConverter
    {
        public HocVien ToEntityHocVien<T>(T input, string loggedEmployee) where T : CreateHocVienInput
        {
            return new HocVien
            {
                FullName = input.FullName,
                Phone = input.Phone,
                OtherPhone = input.OtherPhone,
                FacebookAccount = input.FacebookAccount,
                ParentPhone = input.ParentPhone,
                ParentFullName = input.ParentFullName,
                QuanHeId = input.QuanHeId,
                EnglishName = input.EnglishName,
                NgaySinh = input.NgaySinhDate,
                CreatedBy = loggedEmployee
            };
        }

        public HocVienViewModel ToHocVienViewModel(HocVien hocVien)
        {
            return new HocVienViewModel
            {
                FullName = hocVien.FullName,
                NgaySinh = hocVien.NgaySinh?.ToClearDate() ?? string.Empty,
                EnglishName = hocVien.EnglishName,
                QuanHeId = hocVien.QuanHeId,
                CreatedBy = hocVien.CreatedBy,
                CreatedDate = hocVien.CreatedDate.ToClearDate(),
                FacebookAccount = hocVien.FacebookAccount,
                IsDisabled = hocVien.IsDisabled,
                ParentFullName = hocVien.ParentFullName,
                ParentPhone = hocVien.ParentPhone,
                Phone = hocVien.Phone,
                OtherPhone = hocVien.OtherPhone,
                HocVienId = hocVien.HocVienId,
                QuanHe = hocVien.QuanHe?.Name ?? string.Empty,
                LopHocList = hocVien.HocVien_LopHocs
                                    .Select(x => new LopHocViewModel
                                    {
                                        LopHocId = x.LopHocId,
                                        Name = x.LopHoc.Name,
                                        IsCanceled = x.LopHoc.IsCanceled,
                                        IsGraduated = x.LopHoc.IsGraduated
                                    })
                                    .ToList(),
                LopHoc_NgayHocList = hocVien.HocVien_LopHocs
                                    .Select(x => new LopHoc_NgayHocViewModel
                                    {
                                        LopHoc = new LopHocViewModel
                                        {
                                            LopHocId = x.LopHocId,
                                            Name = x.LopHoc.Name
                                        },
                                        NgayHoc = hocVien.HocVien_NgayHocs.FirstOrDefault(m => m.LopHocId == x.LopHocId)?.NgayBatDau.ToEditionDate() ?? string.Empty
                                    })
                                    .ToList()
            };
        }

        public HocVienLightViewModel ToHocVienLightViewModel(HocVien hocVien)
        {
            return new HocVienLightViewModel
            {
                FullName = hocVien.FullName,
                NgaySinh = hocVien.NgaySinh?.ToClearDate() ?? string.Empty,
                IsDisabled = hocVien.IsDisabled,
                Phone = hocVien.Phone,
                HocVienId = hocVien.HocVienId,
            };
        }

        public HocVien_NgayHocViewModel ToHocVien_NgayHocViewModel(HocVien_NgayHoc hocVien_NgayHoc)
        {
            return new HocVien_NgayHocViewModel
            {
                NgayBatDau = hocVien_NgayHoc.NgayBatDau.ToClearDate(),
                NgayKetThuc = hocVien_NgayHoc.NgayKetThuc?.ToClearDate() ?? string.Empty,
                HocVienId = hocVien_NgayHoc.HocVienId,
                LopHocId = hocVien_NgayHoc.LopHocId
            };
        }

        public NgayHocViewModel ToNgayHocViewModel(NgayHoc ngayHoc)
        {
            return new NgayHocViewModel
            {
                CreatedBy = ngayHoc.CreatedBy,
                CreatedDate = ngayHoc.CreatedDate.ToClearDate(),
                NgayHocId = ngayHoc.NgayHocId,
                Name = ngayHoc.Name,
                UpdatedBy = ngayHoc.UpdatedBy,
                UpdatedDate = ngayHoc.UpdatedDate?.ToClearDate() ?? string.Empty
            };
        }

        public void MappingEntityHocVien(UpdateHocVienInputModel input, HocVien item, string loggedEmployee)
        {
            item.FullName = input.FullName;
            item.QuanHeId = input.QuanHeId;
            item.Phone = input.Phone;
            item.OtherPhone = input.OtherPhone;
            item.FacebookAccount = input.FacebookAccount;
            item.ParentFullName = input.ParentFullName;
            item.ParentPhone = input.ParentPhone;
            item.EnglishName = input.EnglishName;
            item.NgaySinh = input.NgaySinhDate;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;
        }
    }
}
