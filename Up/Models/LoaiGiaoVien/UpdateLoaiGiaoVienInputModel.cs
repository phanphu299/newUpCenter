using System;

namespace Up.Models
{
    public class UpdateLoaiGiaoVienInputModel : CreateLoaiGiaoVienInput
    {
        public Guid LoaiGiaoVienId { get; set; }
    }
}
