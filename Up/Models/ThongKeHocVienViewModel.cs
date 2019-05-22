namespace Up.Models
{
    using System.Collections.Generic;

    public class ThongKeHocVienViewModel
    {
        public List<int> GiaoTiep { get; set; }
        public List<int> ThieuNhi { get; set; }
        public List<int> QuocTe { get; set; }
    }

    public class ThongKeModel
    {
        public string Label { get; set; }
        public int Data { get; set; }

    }
}
