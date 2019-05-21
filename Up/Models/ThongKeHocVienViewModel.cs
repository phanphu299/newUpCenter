namespace Up.Models
{
    using System.Collections.Generic;

    public class ThongKeHocVienViewModel
    {
        public List<int> GiaoTiep { get; set; }
        public List<int> ThieuNhi { get; set; }
        public List<int> QuocTe { get; set; }
    }

    public class ThongKeHocVienModel
    {
        public string Label { get; set; }
        public string BackgroundColor { get; set; }
        public int Data { get; set; }

    }
}
