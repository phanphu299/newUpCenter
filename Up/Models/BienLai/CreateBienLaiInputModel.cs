using System;

namespace Up.Models
{
    public class CreateBienLaiInputModel
    {
        public Guid LopHocId { get; set; }
        public Guid HocVienId { get; set; }
        public double HocPhi { get; set; }
        public string ThangHocPhi { get; set; }
        public string MaBienLai { get; set; }
    }
}
