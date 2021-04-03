
namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class KhoaHoc : BaseEntity, IRemovable
    {
        public Guid KhoaHocId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }

        public ICollection<LopHoc> LopHocs { get; set; }
        public ICollection<ThuThach> ThuThachs { get; set; }
    }
}
