using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Up.Data.Entities
{
    public class CauHoi : BaseEntity, IRemovable
    {
        public Guid CauHoiId { get; set; }
        public Guid ThuThachId { get; set; }
        public bool IsDisabled { get; set; }
        public int STT { get; set; }
        public string Name { get; set; }

        [ForeignKey("ThuThachId")]
        public ThuThach ThuThach { get; set; }

        public ICollection<DapAn> DapAns { get; set; }
    }
}
