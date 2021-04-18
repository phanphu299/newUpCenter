using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Up.Data.Entities
{
    public class Note : BaseEntity, IRemovable
    {
        public Guid NoteId { get; set; }

        public Guid HocVienId { get; set; }

        public string GhiChu { get; set; }

        public bool IsDisabled { get; set; }

        [ForeignKey("HocVienId")]
        public HocVien HocVien { get; set; }
    }
}
