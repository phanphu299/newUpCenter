using System;

namespace Up.Models
{
    public class UpdateGiaoVienInputModel : CreateGiaoVienInput
    {
        public Guid GiaoVienId { get; set; }
    }
}
