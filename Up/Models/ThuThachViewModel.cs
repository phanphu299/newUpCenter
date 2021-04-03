using System;

namespace Up.Models
{
    public class ThuThachViewModel : BaseViewModel
    {
        public Guid ThuThachId { get; set; }

        public string Name { get; set; }

        public Guid KhoaHocId { get; set; }

        public string TenKhoaHoc { get; set; }

        public int SoCauHoi { get; set; }

        public int ThoiGianLamBai { get; set; }

        public int MinGrade { get; set; }
    }
}
