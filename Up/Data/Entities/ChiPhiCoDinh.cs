
namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class ChiPhiCoDinh
    {
        public Guid ChiPhiCoDinhId { get; set; }
        public double Gia { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<ThongKe_ChiPhi> ThongKe_ChiPhis { get; set; }
    }
}
