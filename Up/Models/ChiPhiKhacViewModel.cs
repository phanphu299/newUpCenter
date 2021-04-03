
namespace Up.Models
{
    using System;

    public class ChiPhiKhacViewModel : BaseViewModel
    {
        public Guid ChiPhiKhacId { get; set; }
        public string Name { get; set; }
        public double Gia { get; set; }
        public DateTime NgayChiPhi { get; set; }
        public string _NgayChiPhi { get; set; }
    }
}
