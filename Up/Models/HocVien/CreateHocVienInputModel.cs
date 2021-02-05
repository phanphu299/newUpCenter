using System.Collections.Generic;

namespace Up.Models
{
    public class CreateHocVienInputModel : CreateHocVienInput
    {
        public CreateHocVienInputModel()
        {
            LopHoc_NgayHocList = new List<LopHoc_NgayHocViewModel>();
        }

        public IList<LopHoc_NgayHocViewModel> LopHoc_NgayHocList { get; set; }
    }
}
