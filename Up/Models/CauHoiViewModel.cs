using System;
using System.Collections.Generic;

namespace Up.Models
{
    public class CauHoiViewModel : BaseViewModel
    {
        public Guid CauHoiId { get; set; }

        public string Name { get; set; }

        public int STT { get; set; }

        public Guid ThuThachId { get; set; }

        public string TenThuThach { get; set; }

        public IList<DapAnModel> DapAns { get; set; }
    }
}
