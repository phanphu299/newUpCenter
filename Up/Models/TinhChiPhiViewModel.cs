namespace Up.Models
{
    using System;
    using System.Collections.Generic;

    public class TinhChiPhiViewModel
    {
        public TinhChiPhiViewModel()
        {
            ChiPhiList = new List<ChiPhiModel>();
        }

        public List<ChiPhiModel> ChiPhiList { get; set; }
    }

    public class ChiPhiModel
    {
        public string Name { get; set; }
        public int LoaiChiPhi { get; set; }
        public double Salary_Expense { get; set; }
        public double Bonus { get; set; } = 0;
        public double Minus { get; set; } = 0;
        public double TeachingRate { get; set; }
        public double TutoringRate { get; set; }
        public double SoGioDay { get; set; }
        public double SoGioKem { get; set; }
        public double ChiPhiMoi { get; set; }
        public Guid? ChiPhiCoDinhId { get; set; }
        public Guid? NhanVienId { get; set; }
        public bool DaLuu { get; set; }
        public double MucHoaHong { get; set; }
        public double SoHocVien { get; set; }
    }
}
