namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class QuanHe
    {
        public Guid QuanHeId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<HocVien> HocViens { get; set; }
    }
}
