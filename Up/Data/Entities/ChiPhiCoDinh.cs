
namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class ChiPhiCoDinh : BaseEntity, IRemovable
    {
        public Guid ChiPhiCoDinhId { get; set; }
        public double Gia { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }

        public ICollection<ThongKe_ChiPhi> ThongKe_ChiPhis { get; set; }
    }
}
