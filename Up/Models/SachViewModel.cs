namespace Up.Models
{
    using System;

    public class SachViewModel
    {
        public Guid SachId { get; set; }
        public string Name { get; set; }
        public double Gia { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDisabled { get; set; }
    }
}
