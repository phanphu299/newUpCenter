
namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class Sach : BaseEntity, IRemovable
    {
        public Guid SachId { get; set; }
        public string Name { get; set; }
        public double Gia { get; set; }
        public bool IsDisabled { get; set; }

        public ICollection<ThongKe_DoanhThuHocPhi_TaiLieu> ThongKe_DoanhThuHocPhi_TaiLieus { get; set; }
    }
}
