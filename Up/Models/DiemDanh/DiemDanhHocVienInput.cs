using System;

namespace Up.Models
{
    public class DiemDanhHocVienInput
    {
        public Guid HocVienId { get; set; }

        public Guid LopHocId { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public bool IsOff { get; set; }

        public DateTime NgayDiemDanh { get; set; }
    }
}
