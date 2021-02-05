using System;

namespace Up.Models
{
    public class UpdateGioHocInputModel : CreateGioHocInput
    {
        public Guid GioHocId { get; set; }
    }
}
