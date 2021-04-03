using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Up.Data.Entities
{
    public class DapAn : BaseEntity, IRemovable
    {
        public Guid DapAnId { get; set; }

        public Guid CauHoiId { get; set; }

        public string Name { get; set; }

        public bool IsTrue { get; set; }

        public bool IsDisabled { get; set; }

        [ForeignKey("CauHoiId")]
        public CauHoi CauHoi { get; set; }
    }
}
