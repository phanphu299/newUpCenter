using System;

namespace Up.Models
{
    public class HocVienLightViewModel
    {
        public Guid HocVienId { get; set; }
        public string FullName { get; set; }
        public string NgaySinh { get; set; }
        public bool IsDisabled { get; set; }
        public string Phone { get; set; }
    }
}
