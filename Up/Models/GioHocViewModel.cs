using System;

namespace Up.Models
{
    public class GioHocViewModel : BaseViewModel
    {
        public Guid GioHocId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
