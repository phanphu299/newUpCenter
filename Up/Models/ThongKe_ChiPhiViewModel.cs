
namespace Up.Models
{
    using System;
    using System.Collections.Generic;

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

        public Guid? ChiPhiCoDinhId { get; set; }
        public Guid? NhanVienId { get; set; }
        public double ChiPhiMoi { get; set; }

        public bool DaLuu { get; set; }
    }

    public class Add_ThongKe_ChiPhiViewModel
    {
        public ThongKe_ChiPhiViewModel[] models { get; set; }
    }
}
