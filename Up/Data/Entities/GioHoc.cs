namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class GioHoc
    {
        public Guid GioHocId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<LopHoc> LopHocs { get; set; }
    }
}
