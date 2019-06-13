
namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ThongKe_DoanhThuHocPhi
    {
        public Guid ThongKe_DoanhThuHocPhiId { get; set; }
        public Guid HocVienId { get; set; }
        public Guid LopHocId { get; set; }
        public double HocPhi { get; set; }
        public DateTime NgayDong { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        [ForeignKey("LopHocId")]
        public LopHoc LopHoc { get; set; }
        [ForeignKey("HocVienId")]
        public HocVien HocVien { get; set; }
    }
}
