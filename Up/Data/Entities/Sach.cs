using System;
using System.Collections.Generic;

namespace Up.Data.Entities
{
    public class Sach
    {
        public Guid SachId { get; set; }
        public string Name { get; set; }
        public double Gia { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<LopHoc_Sach> LopHoc_Sachs { get; set; }
    }
}
