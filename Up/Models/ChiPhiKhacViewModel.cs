
namespace Up.Models
{
    using System;

    public class ChiPhiKhacViewModel
    {
        public Guid ChiPhiKhacId { get; set; }
        public string Name { get; set; }
        public double Gia { get; set; }
        public DateTime NgayChiPhi { get; set; }
        public string _NgayChiPhi { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
