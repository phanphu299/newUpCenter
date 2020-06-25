﻿
namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class HocPhiTronGoi
    {
        public Guid HocPhiTronGoiId { get; set; }
        public Guid HocVienId { get; set; }
        public double HocPhi { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        [ForeignKey("HocVienId")]
        public HocVien HocVien { get; set; }
    }
}