
namespace Up.Models
{
    using System;
    using System.Collections.Generic;

    public class HocPhiTronGoiViewModel : BaseViewModel
    {
        public Guid HocPhiTronGoiId { get; set; }
        public Guid HocVienId { get; set; }
        public string Name { get; set; }
        public double HocPhi { get; set; }
        public bool IsDisabled { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string GhiChu { get; set; }
        public List<Guid> HocVienIds { get; set; }
        public List<HocPhiTronGoi_LopHocViewModel> LopHocList { get; set; }
    }

    public class HocPhiTronGoi_LopHocViewModel
    {
        public LopHocViewModel LopHoc { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
