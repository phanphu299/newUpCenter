
namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ThongKe_DoanhThuHocPhi_TaiLieu
    {
        public Guid ThongKe_DoanhThuHocPhi_TaiLieuId { get; set; }
        public Guid ThongKe_DoanhThuHocPhiId { get; set; }
        public Guid SachId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        [ForeignKey("ThongKe_DoanhThuHocPhiId")]
        public ThongKe_DoanhThuHocPhi ThongKe_DoanhThuHocPhi { get; set; }
        [ForeignKey("SachId")]
        public Sach Sach { get; set; }
    }
}
