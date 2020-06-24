
namespace Up.Models
{
    using System;

    public class ThongKe_ChiPhiViewModel
    {
        public Guid ThongKe_ChiPhiId { get; set; }
        public double ChiPhi { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime NgayChiPhi { get; set; }

        public int month { get; set; }
        public int year { get; set; }

        public double Bonus { get; set; }
        public double Minus { get; set; }
        public double SoGioDay { get; set; }
        public double SoGioKem { get; set; }
        public double SoHocVien { get; set; }

        public Guid? ChiPhiCoDinhId { get; set; }
        public Guid? ChiPhiKhacId { get; set; }
        public Guid? NhanVienId { get; set; }
        public double ChiPhiMoi { get; set; }

        public string NgayLamViec { get; set; }
        public int SoNgayLam { get; set; }
        public int SoNgayLamVoSau { get; set; }

        public double SoNgayNghi { get; set; }
        public double DailySalary { get; set; }
        public double Salary_Expense { get; set; }
        public double TeachingRate { get; set; }
        public double TutoringRate { get; set; }
        public double MucHoaHong { get; set; }
        public bool DaLuu { get; set; }
        public string GhiChu { get; set; }
    }

    public class Add_ThongKe_ChiPhiViewModel
    {
        public ThongKe_ChiPhiViewModel[] models { get; set; }
    }
}
