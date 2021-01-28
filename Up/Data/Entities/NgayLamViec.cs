
namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class NgayLamViec : BaseEntity, IRemovable
    {
        public Guid NgayLamViecId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }

        public ICollection<GiaoVien> NhanViens { get; set; }
    }
}
