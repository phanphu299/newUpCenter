using System;

namespace Up.Models
{
    public class ThongKe_DoanhThuHocPhiInputModel
    {
        public Guid LopHocId { get; set; }
        public Guid HocVienId { get; set; }
        public double HocPhi { get; set; }
        public DateTime NgayDong { get; set; }
        public double Bonus { get; set; }
        public double Minus { get; set; }
        public int KhuyenMai { get; set; }
        public string GhiChu { get; set; }
        public Guid[] SachIds { get; set; }
        public SachViewModel[] GiaSach { get; set; }
        public bool DaDong { get; set; }
        public bool DaNo { get; set; }
        public bool TronGoi { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }
}
