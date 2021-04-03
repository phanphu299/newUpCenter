namespace Up.Models
{
    using System;

    public class SachViewModel : BaseViewModel
    {
        public Guid SachId { get; set; }
        public string Name { get; set; }
        public double Gia { get; set; }
        public bool IsDisabled { get; set; }
    }
}
