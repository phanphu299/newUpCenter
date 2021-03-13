using System.Collections.Generic;
namespace Up.Models
{
    public class CreateHocPhiTronGoiInput
    {
        public double HocPhi { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<HocPhiTronGoi_LopHocViewModel> LopHocList { get; set; }
    }
}
