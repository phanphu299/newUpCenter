
namespace Up.Models
{
    using System;

    public class LoaiGiaoVienViewModel : BaseViewModel
    {
        public Guid LoaiGiaoVienId { get; set; }
        public string Name { get; set; }
        public byte? Order { get; set; }
    }
}
