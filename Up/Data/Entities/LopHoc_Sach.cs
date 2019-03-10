using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Up.Data.Entities
{
    public class LopHoc_Sach
    {
        public Guid LopHoc_SachId { get; set; }
        public Guid LopHocId { get; set; }
        public Guid SachId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        [ForeignKey("LopHocId")]
        public LopHoc LopHoc { get; set; }
        [ForeignKey("SachId")]
        public Sach Sach { get; set; }
    }
}
