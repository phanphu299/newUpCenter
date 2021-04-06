using System;
using System.Collections.Generic;

namespace Up.Models
{
    public class CreateCauHoiInput
    {
        public Guid ThuThachId { get; set; }
        public int STT { get; set; }
        public string Name { get; set; }

        public IList<DapAnModel> DapAns { get; set; }
    }
}
