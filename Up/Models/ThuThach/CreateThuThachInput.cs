using System;

namespace Up.Models
{
    public class CreateThuThachInput
    {
        public Guid KhoaHocId { get; set; }

        public string Name { get; set; }

        public int SoCauHoi { get; set; }

        public int ThoiGianLamBai { get; set; }

        public int MinGrade { get; set; }
    }
}
