
namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ThongKe_ChiPhi
    {
        public Guid ThongKe_ChiPhiId { get; set; }
        public double ChiPhi { get; set; }
        public DateTime NgayChiPhi { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public double Bonus { get; set; }
        public double Minus { get; set; }
        public double SoGioDay { get; set; }
        public double SoGioKem { get; set; }
        public double SoHocVien { get; set; }
        public double SoNgayNghi { get; set; }
        public double DailySalary { get; set; }
        public string NgayLamViec { get; set; }
        public int SoNgayLam { get; set; }
        public int SoNgayLamVoSau { get; set; }

        public Guid? ChiPhiCoDinhId { get; set; }
        public Guid? NhanVienId { get; set; }
        public bool DaLuu { get; set; }

        public double Salary_Expense { get; set; }
        public double TeachingRate { get; set; }
        public double TutoringRate { get; set; }
        public double MucHoaHong { get; set; }

        [ForeignKey("ChiPhiCoDinhId")]
        public ChiPhiCoDinh ChiPhiCoDinh { get; set; }
        [ForeignKey("NhanVienId")]
        public GiaoVien NhanVien { get; set; }
    }
}
