namespace Up.Models
{
    using System;
    using System.Collections.Generic;

    public class LopHoc_DiemDanhViewModel : BaseViewModel
    {
        public Guid LopHoc_DiemDanhId { get; set; }
        public Guid LopHocId { get; set; }
        public string LopHoc { get; set; }
        public Guid HocVienId { get; set; }
        public string HocVien { get; set; }
        public bool IsOff { get; set; }
        public bool? IsDuocNghi { get; set; }
        public string NgayDiemDanh { get; set; }
        public List<string> NgayDiemDanhs { get; set; }

        public List<Guid> HocVienIds { get; set; }
    }
}
