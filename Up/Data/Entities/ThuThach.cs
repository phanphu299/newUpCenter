using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Up.Data.Entities
{
    public class ThuThach : BaseEntity, IRemovable
    {
        public Guid ThuThachId { get; set; }

        public Guid KhoaHocId { get; set; }

        public string Name { get; set; }

        public int SoCauHoi { get; set; }

        public int ThoiGianLamBai { get; set; }

        public int MinGrade { get; set; }

        public bool IsDisabled { get; set; }

        [ForeignKey("KhoaHocId")]
        public KhoaHoc KhoaHoc { get; set; }

        public ICollection<CauHoi> CauHois { get; set; }

        public ICollection<ChallengeResult> ChallengeResults { get; set; }
    }
}
