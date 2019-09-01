

namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class NhanVien_ViTri
    {
        public Guid NhanVien_ViTriId { get; set; }
        public Guid NhanVienId { get; set; }
        public Guid ViTriId { get; set; }
        public Guid CheDoId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        [ForeignKey("NhanVienId")]
        public GiaoVien NhanVien { get; set; }
        [ForeignKey("ViTriId")]
        public LoaiGiaoVien ViTri { get; set; }
        [ForeignKey("CheDoId")]
        public LoaiCheDo CheDo { get; set; }
    }
}
