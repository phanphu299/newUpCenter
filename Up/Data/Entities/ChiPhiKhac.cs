
namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class ChiPhiKhac : BaseEntity, IRemovable
    {
        public Guid ChiPhiKhacId { get; set; }
        public string Name { get; set; }
        public double Gia { get; set; }
        public DateTime NgayChiPhi { get; set; }
        public bool IsDisabled { get; set; }

        public ICollection<ThongKe_ChiPhi> ThongKe_ChiPhis { get; set; }
    }
}
