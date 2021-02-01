using System;

namespace Up.Models
{
    public class UpdateLopHocInputModel : CreateLopHocInput
    {
        public Guid LopHocId { get; set; }
        public string NgayKetThuc { get; set; }
        public DateTime? NgayKetThucDate { get; set; }
    }
}
