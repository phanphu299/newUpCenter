using System;

namespace Up.Models
{
    public class CreateHocVienInput
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string OtherPhone { get; set; }
        public string FacebookAccount { get; set; }
        public string ParentFullName { get; set; }
        public string ParentPhone { get; set; }
        public Guid? QuanHeId { get; set; }
        public string NgaySinh { get; set; }
        public DateTime? NgaySinhDate { get; set; }
        public string EnglishName { get; set; }
        public DateTime? NgayBatDau { get; set; }

        public string CMND { get; set; }

        public string Notes { get; set; }
        public string DiaChi { get; set; }
    }
}
