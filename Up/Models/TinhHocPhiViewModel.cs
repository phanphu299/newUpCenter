
namespace Up.Models
{
    using System.Collections.Generic;
    public class TinhHocPhiViewModel
    {
        public int SoNgayHoc { get; set; }
        public int SoNgayDuocNghi { get; set; }
        public double HocPhi { get; set; }
        public List<HocVienViewModel> HocVienList { get; set; }
    }
}
