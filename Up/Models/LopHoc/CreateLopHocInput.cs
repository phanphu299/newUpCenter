using System;

namespace Up.Models
{
    public class CreateLopHocInput
    {
        public string Name { get; set; }
        public Guid KhoaHocId { get; set; }
        public Guid NgayHocId { get; set; }
        public Guid GioHocId { get; set; }
        public string NgayKhaiGiang { get; set; }
        public DateTime NgayKhaiGiangDate { get; set; }
    }
}
