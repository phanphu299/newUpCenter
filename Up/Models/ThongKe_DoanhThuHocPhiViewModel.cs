
namespace Up.Models
{
    using System;

    public class ThongKe_DoanhThuHocPhiViewModel
    {
        public Guid ThongKe_DoanhThuHocPhiId { get; set; }
        public Guid LopHocId { get; set; }
        public string LopHoc { get; set; }
        public Guid HocVienId { get; set; }
        public string HocVien { get; set; }
        public double HocPhi { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate_Date { get; set; }
    }
}
