namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class GioHoc : BaseEntity, IRemovable
    {
        public Guid GioHocId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool IsDisabled { get; set; }

        public ICollection<LopHoc> LopHocs { get; set; }
    }
}
