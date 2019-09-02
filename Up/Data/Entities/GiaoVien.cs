namespace Up.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    //GIAO VIEN LA NHAN VIEN
    public class GiaoVien
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
        public bool IsDisabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public double MucHoaHong { get; set; }
        //GIAO VIEN LA NHAN VIEN
        
        public ICollection<ThongKe_ChiPhi> ThongKe_ChiPhis { get; set; }
        public ICollection<NhanVien_ViTri> NhanVien_ViTris { get; set; }
    }
}
