﻿
namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ThongKe_ChiPhi : BaseEntity, IRemovable
    {
        public Guid ThongKe_ChiPhiId { get; set; }
        public double ChiPhi { get; set; }
        public DateTime NgayChiPhi { get; set; }
        public bool IsDisabled { get; set; }
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
        public Guid? ChiPhiKhacId { get; set; }
        public Guid? NhanVienId { get; set; }
        public bool DaLuu { get; set; }
        public double Salary_Expense { get; set; }
        public double TeachingRate { get; set; }
        public double TutoringRate { get; set; }
        public double MucHoaHong { get; set; }
        public string GhiChu { get; set; }

        [ForeignKey("ChiPhiCoDinhId")]
        public ChiPhiCoDinh ChiPhiCoDinh { get; set; }
        [ForeignKey("NhanVienId")]
        public GiaoVien NhanVien { get; set; }
        [ForeignKey("ChiPhiKhacId")]
        public ChiPhiKhac ChiPhiKhac { get; set; }
    }
}
