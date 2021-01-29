using System;

namespace Up.Models
{
    public class HocVien_NgayHocInputModel
    {
        public Guid HocVienId { get; set; }
        public Guid LopHocId { get; set; }
        public string NgayBatDau { get; set; }
        public string NgayKetThuc { get; set; }
        public DateTime NgayBatDauDate { get; set; }
        public DateTime NgayKetThucDate { get; set; }
    }
}
