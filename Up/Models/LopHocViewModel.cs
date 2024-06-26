﻿
namespace Up.Models
{
    using System;

    public class LopHocViewModel : BaseViewModel
    {
        public Guid LopHocId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsGraduated { get; set; }
        public bool IsCanceled { get; set; }
        public Guid KhoaHocId { get; set; }
        public string KhoaHoc { get; set; }
        public Guid NgayHocId { get; set; }
        public string NgayHoc { get; set; }
        public Guid GioHocId { get; set; }
        public string GioHocFrom { get; set; }
        public string GioHocTo { get; set; }
        public string NgayKhaiGiang { get; set; }
        public string NgayKetThuc { get; set; }
        public bool HocVienNghi { get; set; }
        public HocPhiViewModel HocPhi { get; set; }
    }
}
