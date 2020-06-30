
namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class HocPhiTronGoi_LopHoc
    {
        public Guid HocPhiTronGoi_LopHocId { get; set; }
        public Guid HocPhiTronGoiId { get; set; }
        public Guid LopHocId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        [ForeignKey("HocPhiTronGoiId")]
        public HocPhiTronGoi HocPhiTronGoi { get; set; }

        [ForeignKey("LopHocId")]
        public LopHoc LopHoc { get; set; }
    }
}
