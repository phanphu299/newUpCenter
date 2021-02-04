using System;

namespace Up.Models
{
    public class TinhHocPhiInputModel
    {
        public Guid LopHocId { get; set; }

        public Guid HocPhiId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public double HocPhi { get; set; }
    }
}
