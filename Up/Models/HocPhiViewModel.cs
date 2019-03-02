using System;

namespace Up.Models
{
    public class HocPhiViewModel
    {
        public Guid HocPhiId { get; set; }
        public double Gia { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
