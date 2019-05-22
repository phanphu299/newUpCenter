namespace Up.Models
{
    using System;
    public class GiaoVienViewModel
    {
        public Guid GiaoVienId { get; set; }
        public string Name { get; set; }
        public Guid LoaiGiaoVienId { get; set; }
        public string LoaiGiaoVien { get; set; }
        public string Phone { get; set; }
        public double TeachingRate { get; set; }
        public double TutoringRate { get; set; }
        public double BasicSalary { get; set; }
        public string FacebookAccount { get; set; }
        public string DiaChi { get; set; }
        public string InitialName { get; set; }
        public string CMND { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate_Date { get; set; }
    }
}
