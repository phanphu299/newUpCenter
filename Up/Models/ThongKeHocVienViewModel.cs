namespace Up.Models
{
    using System.Collections.Generic;

    public class ThongKeHocVienViewModel
    {
        public List<double> GiaoTiep { get; set; }
        public List<double> ThieuNhi { get; set; }
        public List<double> QuocTe { get; set; }
    }

    public class ThongKeModel
    {
        public string Label { get; set; }
        public double Data { get; set; }

    }
}
