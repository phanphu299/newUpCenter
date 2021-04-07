using System;
using System.Collections.Generic;

namespace Up.Models
{
    public class ImportCauHoiInputModel
    {
        public Guid ThuThachId { get; set; }
        public List<CreateCauHoiInputModel> CauHois { get; set; } = new List<CreateCauHoiInputModel>();
    }
}
