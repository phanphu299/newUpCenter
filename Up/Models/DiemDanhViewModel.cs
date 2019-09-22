
namespace Up.Models
{
    using System;

    public class DiemDanhViewModel
    {
        public Guid HocVienId { get; set; }
        public string NgayDiemDanh { get; set; }
        public bool IsOff { get; set; }
        public bool? IsDuocNghi { get; set; }
        public string HocVien { get; set; }

        public DateTime NgayDiemDanh_Date { get; set; }
        public int Day { get; set; }

        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
    }
}
