
namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ThongKe_DoanhThuHocPhi : BaseEntity
    {
        public Guid ThongKe_DoanhThuHocPhiId { get; set; }
        public Guid HocVienId { get; set; }
        public Guid LopHocId { get; set; }
        public double HocPhi { get; set; }
        public DateTime NgayDong { get; set; }
        public double Bonus { get; set; }
        public double Minus { get; set; }
        public int KhuyenMai { get; set; }
        public string GhiChu { get; set; }
        public bool DaDong { get; set; }
        public bool DaNo { get; set; }
        public bool TronGoi { get; set; }

        [ForeignKey("LopHocId")]
        public LopHoc LopHoc { get; set; }
        [ForeignKey("HocVienId")]
        public HocVien HocVien { get; set; }
        public ICollection<ThongKe_DoanhThuHocPhi_TaiLieu> ThongKe_DoanhThuHocPhi_TaiLieus { get; set; }
    }
}
