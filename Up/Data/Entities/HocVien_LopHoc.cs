namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class HocVien_LopHoc : BaseEntity
    {
        public Guid HocVien_LopHocId { get; set; }
        public Guid HocVienId { get; set; }
        public Guid LopHocId { get; set; }

        [ForeignKey("HocVienId")]
        public HocVien HocVien { get; set; }
        [ForeignKey("LopHocId")]
        public LopHoc LopHoc { get; set; }
    }
}
