using System;

namespace Up.Data.Entities
{
    public class LopHoc
    {
        public Guid LopHocId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsGraduated { get; set; }
        public bool IsCanceled { get; set; }
        public Guid KhoaHocId { get; set; }
        public Guid NgayHocId { get; set; }
        public Guid GioHocId { get; set; }
        public Guid HocPhiId { get; set; }
        public DateTime NgayKhaiGiang { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public KhoaHoc KhoaHoc { get; set; }
        public GioHoc GioHoc { get; set; }
        public NgayHoc NgayHoc { get; set; }
        public HocPhi HocPhi { get; set; }
    }
}
