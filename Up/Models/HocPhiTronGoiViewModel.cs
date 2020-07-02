
namespace Up.Models
{
    using System;
    using System.Collections.Generic;

    public class HocPhiTronGoiViewModel
    {
        public Guid HocPhiTronGoiId { get; set; }
        public Guid HocVienId { get; set; }
        public string Name { get; set; }
        public double HocPhi { get; set; }
        public bool IsDisabled { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public List<Guid> HocVienIds { get; set; }
        public List<HocPhiTronGoi_LopHocViewModel> LopHocList{ get; set; }
    }

    public class HocPhiTronGoi_LopHocViewModel
    {
        public LopHocViewModel LopHoc { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
