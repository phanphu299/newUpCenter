
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
        public DateTime NgayDong { get; set; }

        public double Bonus { get; set; }
        public double Minus { get; set; }
        public int KhuyenMai { get; set; }
        public string GhiChu { get; set; }
        public Guid[] SachIds { get; set; }

        public int month { get; set; }
        public int year { get; set; }
    }
}
