using System;
using System.Collections.Generic;

namespace Up.Models
{
    public class LopHocViewModel
    {
        public LopHocViewModel()
        {
            SachList = new List<SachViewModel>();
        }

        public Guid LopHocId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsGraduated { get; set; }
        public bool IsCanceled { get; set; }
        public Guid KhoaHocId { get; set; }
        public string KhoaHoc { get; set; }
        public Guid NgayHocId { get; set; }
        public string NgayHoc { get; set; }
        public Guid GioHocId { get; set; }
        public string GioHoc { get; set; }
        public Guid HocPhiId { get; set; }
        public double HocPhi { get; set; }
        public string NgayKhaiGiang { get; set; }
        public string NgayKetThuc { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public List<SachViewModel> SachList{ get; set; }
        public Guid[] SachIds { get; set; }
    }
}
