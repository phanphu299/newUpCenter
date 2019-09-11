


namespace Up.Models
{
    using System.Collections.Generic;
    public class QuyenViewModel
    {
        public int QuyenId { get; set; }
        public string Name { get; set; }
        public bool IsTrue { get; set; }
    }

    public class AddQuyenToRoleViewModel
    {
        public string RoleId { get; set; }
        public List<QuyenViewModel> QuyenList { get; set; }
    }
}
