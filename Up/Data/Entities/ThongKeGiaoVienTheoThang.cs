

namespace Up.Data.Entities
{
    using System;
    public class ThongKeGiaoVienTheoThang
    {
        public Guid ThongKeGiaoVienTheoThangId { get; set; }
        public int SoLuong { get; set; }
        public byte LoaiGiaoVien { get; set; }
        public DateTime Date { get; set; }
    }
}
