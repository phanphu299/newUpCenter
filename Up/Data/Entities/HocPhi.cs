namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class HocPhi
    {
        public Guid HocPhiId { get; set; }
        public double Gia { get; set; }
        public bool IsDisabled { get; set; }
        public string GhiChu { get; set; }
        public DateTime? NgayApDung { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<LopHoc_HocPhi> LopHoc_HocPhis { get; set; }
    }
}
