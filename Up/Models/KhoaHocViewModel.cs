using System;

namespace Up.Models
{
    public class KhoaHocViewModel
    {
        public Guid KhoaHocId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
