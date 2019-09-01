namespace Up.Models
{
    using System;

    public class NhanVien_ViTriViewModel
    {
        public Guid NhanVien_ViTriId { get; set; }
        public Guid NhanVienId { get; set; }
        public string NhanVien { get; set; }
        public Guid ViTriId { get; set; }
        public string ViTri { get; set; }
        public Guid CheDoId { get; set; }
        public string CheDo { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
