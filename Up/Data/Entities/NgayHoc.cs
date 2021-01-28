namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class NgayHoc : BaseEntity, IRemovable
    {
        public Guid NgayHocId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }

        public ICollection<LopHoc> LopHocs { get; set; }
    }
}
