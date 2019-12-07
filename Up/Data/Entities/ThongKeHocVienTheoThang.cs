
namespace Up.Data.Entities
{
    using System;

    public class ThongKeHocVienTheoThang
    {
        public Guid ThongKeHocVienTheoThangId { get; set; }
        public int SoLuong { get; set; }
        public byte LoaiHocVien { get; set; }
        public DateTime Date { get; set; }
    }
}
