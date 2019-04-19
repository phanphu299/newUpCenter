namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class HocVien_LopHoc
    {
        public Guid HocVien_LopHocId { get; set; }
        public Guid HocVienId { get; set; }
        public Guid LopHocId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        [ForeignKey("HocVienId")]
        public HocVien HocVien { get; set; }
        [ForeignKey("LopHocId")]
        public LopHoc LopHoc { get; set; }
    }
}
