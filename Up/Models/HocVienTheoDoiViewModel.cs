using System;

namespace Up.Models
{
    public class HocVienTheoDoiViewModel
    {
        public Guid NoteId { get; set; }

        public Guid HocVienId { get; set; }

        public string TenHocVien { get; set; }

        public string Trigram { get; set; }

        public string GhiChu { get; set; }
    }
}
