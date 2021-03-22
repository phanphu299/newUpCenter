using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Up.Data.Entities
{
    public class BienLai : BaseEntity, IRemovable
    {
        public Guid BienLaiId { get; set; }

        public Guid HocVienId { get; set; }

        public Nullable<Guid> LopHocId { get; set; }

        public double HocPhi { get; set; }

        public string ThangHocPhi { get; set; }

        public string MaBienLai { get; set; }

        public bool IsDisabled { get; set; }

        [ForeignKey("HocVienId")]
        public HocVien HocVien { get; set; }
    }
}
