
namespace Up.Models
{
    using System;

    public class HocPhiViewModel : BaseViewModel
    {
        public Guid HocPhiId { get; set; }
        public double Gia { get; set; }
        public string GhiChu { get; set; }
        public string NgayApDung { get; set; }
    }
}
