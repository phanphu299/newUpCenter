using System;

namespace Up.Models
{
    public class ResultInputModel
    {
        public Guid HocVienId { get; set; }

        public Guid ThuThachId { get; set; }

        public int LanThi { get; set; }

        public bool IsPass { get; set; }

        public int Score { get; set; }
    }
}
