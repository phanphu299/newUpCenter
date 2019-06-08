
namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class HocVien_No
    {
        public Guid HocVien_NoId { get; set; }
        public Guid HocVienId { get; set; }
        public Guid LopHocId { get; set; }
        public double TienNo { get; set; }
        public DateTime NgayNo { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        [ForeignKey("LopHocId")]
        public LopHoc LopHoc { get; set; }
        [ForeignKey("HocVienId")]
        public HocVien HocVien { get; set; }
    }
}
