using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Up.Data.Entities
{
    public class ChallengeResult : BaseEntity
    {
        public Guid ChallengeResultId { get; set; }

        public Guid HocVienId { get; set; }

        public Guid ThuThachId { get; set; }

        public int LanThi { get; set; }

        public bool IsPass { get; set; }

        public int Score { get; set; }

        [ForeignKey("HocVienId")]
        public HocVien HocVien { get; set; }

        [ForeignKey("ThuThachId")]
        public ThuThach ThuThach { get; set; }
    }
}
