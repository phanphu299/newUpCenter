
namespace Up.Data.Entities
{
    using System;

    public class ThongKe_ChiPhi
    {
        public Guid ThongKe_ChiPhiId { get; set; }
        public double ChiPhi { get; set; }
        public DateTime NgayChiPhi { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
