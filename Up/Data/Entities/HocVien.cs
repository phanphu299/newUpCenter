﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Up.Data.Entities
{
    public class HocVien
    {
        public Guid HocVienId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string FacebookAccount { get; set; }
        public string ParentFullName { get; set; }
        public string ParentPhone { get; set; }
        public Guid QuanHeId { get; set; }
        public string ParentFacebookAccount { get; set; }
        public DateTime NgaySinh { get; set; }
        public string EnglishName { get; set; }
        public bool IsAppend { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        [ForeignKey("QuanHeId")]
        public QuanHe QuanHe { get; set; }
    }
}
