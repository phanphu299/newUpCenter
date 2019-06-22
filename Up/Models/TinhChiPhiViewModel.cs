namespace Up.Models
{
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
        public double Bonus { get; set; }
        public double TeachingRate { get; set; }
        public double TutoringRate { get; set; }
        public double SoGioDay { get; set; }
        public double SoGioKem { get; set; }
        public double ChiPhiMoi { get; set; }
    }
}
