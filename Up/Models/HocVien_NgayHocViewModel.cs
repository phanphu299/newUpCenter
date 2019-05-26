namespace Up.Models
{
    using System;

    public class HocVien_NgayHocViewModel
    {
        public Guid HocVienId { get; set; }
        public Guid LopHocId { get; set; }
        public string NgayBatDau { get; set; }
        public string NgayKetThuc { get; set; }
    }
}
