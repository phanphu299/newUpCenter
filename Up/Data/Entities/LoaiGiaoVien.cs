namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;
    //GIAO VIEN LA NHAN VIEN
    public class LoaiGiaoVien : BaseEntity, IRemovable
    {
        public Guid LoaiGiaoVienId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public byte? Order { get; set; }

        public ICollection<NhanVien_ViTri> NhanVien_ViTris { get; set; }
    }
}
