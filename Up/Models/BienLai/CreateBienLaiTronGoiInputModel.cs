using System;

namespace Up.Models
{
    public class CreateBienLaiTronGoiInputModel
    {
        public Guid HocVienId { get; set; }
        public double HocPhi { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
