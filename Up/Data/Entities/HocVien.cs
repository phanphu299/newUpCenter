﻿
namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class HocVien : BaseEntity, IRemovable
    {
        public Guid HocVienId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string OtherPhone { get; set; }
        public string FacebookAccount { get; set; }
        public string ParentFullName { get; set; }
        public string ParentPhone { get; set; }
        public Guid? QuanHeId { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string EnglishName { get; set; }
        public bool IsDisabled { get; set; }

        public string CMND { get; set; }

        public string Notes { get; set; }
        public string DiaChi { get; set; }

        public string Trigram { get; set; }

        [ForeignKey("QuanHeId")]
        public QuanHe QuanHe { get; set; }
        public ICollection<LopHoc_DiemDanh> LopHoc_DiemDanhs { get; set; }
        public ICollection<HocVien_LopHoc> HocVien_LopHocs { get; set; }
        public ICollection<HocVien_NgayHoc> HocVien_NgayHocs { get; set; }
        public ICollection<HocVien_No> HocVien_Nos { get; set; }
        public ICollection<ThongKe_DoanhThuHocPhi> ThongKe_DoanhThuHocPhis { get; set; }
        public ICollection<HocPhiTronGoi> HocPhiTronGois { get; set; }
        public ICollection<ChallengeResult> ChallengeResults { get; set; }
    }
}
