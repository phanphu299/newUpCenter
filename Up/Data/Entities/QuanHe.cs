namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class QuanHe : BaseEntity, IRemovable
    {
        public Guid QuanHeId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }

        public ICollection<HocVien> HocViens { get; set; }
    }
}
