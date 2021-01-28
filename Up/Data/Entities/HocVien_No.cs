
namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class HocVien_No : BaseEntity, IRemovable
    {
        public Guid HocVien_NoId { get; set; }
        public Guid HocVienId { get; set; }
        public Guid LopHocId { get; set; }
        public double TienNo { get; set; }
        public DateTime NgayNo { get; set; }
        public bool IsDisabled { get; set; }

        [ForeignKey("LopHocId")]
        public LopHoc LopHoc { get; set; }
        [ForeignKey("HocVienId")]
        public HocVien HocVien { get; set; }
    }
}
