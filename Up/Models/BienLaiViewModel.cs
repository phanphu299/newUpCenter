using System;

namespace Up.Models
{
    public class BienLaiViewModel
    {
        public Guid BienLaiId { get; set; }

        public string MaBienLai { get; set; }

        public string FullName { get; set; }

        public string TenLop { get; set; }

        public double HocPhi { get; set; }

        public string ThangHocPhi { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
