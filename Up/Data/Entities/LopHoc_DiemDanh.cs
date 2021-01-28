namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LopHoc_DiemDanh : BaseEntity
    {
        public Guid LopHoc_DiemDanhId { get; set; }
        public Guid LopHocId { get; set; }
        public Guid HocVienId { get; set; }
        public bool IsOff { get; set; }
        public DateTime NgayDiemDanh { get; set; }
        public bool? IsDuocNghi { get; set; }

        [ForeignKey("LopHocId")]
        public LopHoc LopHoc { get; set; }
        [ForeignKey("HocVienId")]
        public HocVien HocVien { get; set; }
    }
}
