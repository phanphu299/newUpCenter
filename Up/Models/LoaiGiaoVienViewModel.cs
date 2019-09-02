﻿
namespace Up.Models
{
    using System;

    public class LoaiGiaoVienViewModel
    {
        public Guid LoaiGiaoVienId { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public byte? Order { get; set; }
    }
}
