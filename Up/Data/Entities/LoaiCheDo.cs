
namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class LoaiCheDo : BaseEntity, IRemovable
    {
        public Guid LoaiCheDoId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }

        //GIAO VIEN LA NHAN VIEN
        public ICollection<NhanVien_ViTri> NhanVien_ViTris { get; set; }
    }
}
