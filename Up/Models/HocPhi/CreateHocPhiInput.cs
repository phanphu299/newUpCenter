using System;

namespace Up.Models
{
    public class CreateHocPhiInput
    {
        public double Gia { get; set; }
        public string GhiChu { get; set; }
        public string NgayApDung { get; set; }
        public DateTime NgayApDungDate { get; set; }
    }
}
