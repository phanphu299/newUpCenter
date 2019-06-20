
namespace Up.Data.Entities
{
    using System;

    public class NhanVienKhac
    {
        public Guid NhanVienKhacId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double BasicSalary { get; set; }
        public string FacebookAccount { get; set; }
        public string DiaChi { get; set; }
        public string CMND { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
