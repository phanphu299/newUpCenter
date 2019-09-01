namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;
    //GIAO VIEN LA NHAN VIEN
    public class LoaiGiaoVien
    {
        public Guid LoaiGiaoVienId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<NhanVien_ViTri> NhanVien_ViTris { get; set; }
    }
}
