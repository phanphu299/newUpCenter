﻿namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class LoaiGiaoVien
    {
        public Guid LoaiGiaoVienId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<GiaoVien> GiaoViens { get; set; }
    }
}