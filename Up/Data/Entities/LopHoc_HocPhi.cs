
namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LopHoc_HocPhi
    {
        public Guid LopHoc_HocPhiId { get; set; }
        public Guid LopHocId { get; set; }
        public Guid HocPhiId { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }

        [ForeignKey("LopHocId")]
        public LopHoc LopHoc { get; set; }
        [ForeignKey("HocPhiId")]
        public HocPhi HocPhi { get; set; }
    }
}
