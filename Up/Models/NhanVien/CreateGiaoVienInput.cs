using System;
using System.Collections.Generic;

namespace Up.Models
{
    public class CreateGiaoVienInput
    {
        public List<LoaiNhanVien_CheDoViewModel> LoaiNhanVien_CheDo { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double TeachingRate { get; set; }
        public double TutoringRate { get; set; }
        public double BasicSalary { get; set; }
        public string FacebookAccount { get; set; }
        public string DiaChi { get; set; }
        public string InitialName { get; set; }
        public string CMND { get; set; }
        public double MucHoaHong { get; set; }
        public Guid NgayLamViecId { get; set; }
        public string NgayLamViec { get; set; }
        public string NgayBatDau { get; set; }
        public DateTime NgayBatDauDate { get; set; }
        public string NgayKetThuc { get; set; }
        public DateTime NgayKetThucDate { get; set; }
        public string NganHang { get; set; }
    }
}
