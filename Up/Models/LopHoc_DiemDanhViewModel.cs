﻿namespace Up.Models
{
    using System;

    public class LopHoc_DiemDanhViewModel
    {
        public Guid LopHoc_DiemDanhId { get; set; }
        public Guid LopHocId { get; set; }
        public string LopHoc { get; set; }
        public Guid HocVienId { get; set; }
        public string HocVien { get; set; }
        public bool IsOff { get; set; }
        public string NgayDiemDanh { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
