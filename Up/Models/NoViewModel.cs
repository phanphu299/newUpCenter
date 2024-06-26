﻿
namespace Up.Models
{
    using System;

    public class NoViewModel : BaseViewModel
    {
        public Guid HocVien_NoId { get; set; }
        public Guid LopHocId { get; set; }
        public string LopHoc { get; set; }
        public Guid HocVienId { get; set; }
        public string HocVien { get; set; }
        public double TienNo { get; set; }
        public string NgayNo { get; set; }
        public DateTime NgayNo_Date { get; set; }

        public int month { get; set; }
        public int year { get; set; }
    }
}
