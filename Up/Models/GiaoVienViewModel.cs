namespace Up.Models
{
    using System;
    using System.Collections.Generic;

    public class GiaoVienViewModel : BaseViewModel
    {
        public Guid GiaoVienId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double TeachingRate { get; set; }
        public double TutoringRate { get; set; }
        public double BasicSalary { get; set; }
        public string FacebookAccount { get; set; }
        public string DiaChi { get; set; }
        public string InitialName { get; set; }
        public string CMND { get; set; }
        public DateTime CreatedDate_Date { get; set; }
        public List<LoaiNhanVien_CheDoViewModel> LoaiNhanVien_CheDo { get; set; }
        public double MucHoaHong { get; set; }
        public Guid NgayLamViecId { get; set; }
        public string NgayLamViec { get; set; }
        public string NgayBatDau { get; set; }
        public string NgayKetThuc { get; set; }
        public string NganHang { get; set; }
    }

    public class LoaiNhanVien_CheDoViewModel
    {
        public LoaiGiaoVienViewModel LoaiGiaoVien { get; set; }
        public LoaiCheDoViewModel LoaiCheDo { get; set; }
    }
}
