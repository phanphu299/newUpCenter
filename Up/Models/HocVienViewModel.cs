namespace Up.Models
{
    using System;
    using System.Collections.Generic;

    public class HocVienViewModel
    {
        public Guid HocVienId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string FacebookAccount { get; set; }
        public string ParentFullName { get; set; }
        public string ParentPhone { get; set; }
        public Guid? QuanHeId { get; set; }
        public string QuanHe { get; set; }
        public string ParentFacebookAccount { get; set; }
        public string NgaySinh { get; set; }
        public string EnglishName { get; set; }
        public bool IsAppend { get; set; }
        public bool IsDisabled { get; set; }
        public List<LopHocViewModel> LopHocList { get; set; }
        public Guid[] LopHocIds { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate_Date { get; set; }

        public double TienNo { get; set; }
        public int SoNgayHoc { get; set; }
        public double HocPhiMoi { get; set; }
        public string NgayBatDauHoc { get; set; }
        public double HocPhiBuHocVienVaoSau { get; set; }
        public int KhuyenMai { get; set; } = 0;
        public double[] GiaSach { get; set; }
        public double HocPhiFixed { get; set; }
        public double[] LastGiaSach { get; set; }
    }
}
