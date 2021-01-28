using System;
using System.Collections.Generic;

namespace Up.Models
{
    public class UpdateHocVienInputModel : CreateHocVienInput
    {
        public UpdateHocVienInputModel()
        {
            LopHoc_NgayHocList = new List<LopHoc_NgayHocViewModel>();
        }

        public Guid HocVienId { get; set; }
        public Guid[] LopHocIds { get; set; }
        public IList<LopHoc_NgayHocViewModel> LopHoc_NgayHocList { get; set; }
    }
}
