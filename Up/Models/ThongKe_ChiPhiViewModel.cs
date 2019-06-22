
namespace Up.Models
{
    using System;

    public class ThongKe_ChiPhiViewModel
    {
        public Guid ThongKe_ChiPhiId { get; set; }
        public double ChiPhi { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime NgayChiPhi { get; set; }

        public int month { get; set; }
        public int year { get; set; }
    }
}
