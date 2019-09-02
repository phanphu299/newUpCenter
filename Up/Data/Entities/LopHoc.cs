﻿namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LopHoc
    {
        public Guid LopHocId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsGraduated { get; set; }
        public bool IsCanceled { get; set; }
        public Guid KhoaHocId { get; set; }
        public Guid NgayHocId { get; set; }
        public Guid GioHocId { get; set; }
        public Guid HocPhiId { get; set; }
        public DateTime NgayKhaiGiang { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        [ForeignKey("KhoaHocId")]
        public KhoaHoc KhoaHoc { get; set; }
        [ForeignKey("GioHocId")]
        public GioHoc GioHoc { get; set; }
        [ForeignKey("NgayHocId")]
        public NgayHoc NgayHoc { get; set; }
        [ForeignKey("HocPhiId")]
        public HocPhi HocPhi { get; set; }
        public ICollection<LopHoc_DiemDanh> LopHoc_DiemDanhs { get; set; }
        public ICollection<HocVien_LopHoc> HocVien_LopHocs { get; set; }
        public ICollection<HocVien_NgayHoc> HocVien_NgayHocs { get; set; }
        public ICollection<HocVien_No> HocVien_Nos { get; set; }
        public ICollection<ThongKe_DoanhThuHocPhi> ThongKe_DoanhThuHocPhis { get; set; }
    }
}
