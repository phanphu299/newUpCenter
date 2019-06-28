
namespace Up.Models
{
    using System;

    public class DiemDanhViewModel
    {
        public string NgayDiemDanh { get; set; }
        public bool IsOff { get; set; }
        public bool? IsDuocNghi { get; set; }
        public string HocVien { get; set; }

        public DateTime NgayDiemDanh_Date { get; set; }
    }
}
