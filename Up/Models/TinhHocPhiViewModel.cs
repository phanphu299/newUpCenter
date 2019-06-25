
namespace Up.Models
{
    using System;
    using System.Collections.Generic;
    public class TinhHocPhiViewModel
    {
        public int SoNgayHoc { get; set; }
        public int SoNgayDuocNghi { get; set; }
        public double HocPhiMoiNgay { get; set; }
        public double HocPhi { get; set; }
        public List<HocVienViewModel> HocVienList { get; set; }
        public Guid LopHocId { get; set; }

        public int month { get; set; }
        public int year { get; set; }
    }
}
