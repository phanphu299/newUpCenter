﻿
namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class LoaiCheDo
    {
        public Guid LoaiCheDoId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        //GIAO VIEN LA NHAN VIEN
        public ICollection<NhanVien_ViTri> NhanVien_ViTris { get; set; }
    }
}
