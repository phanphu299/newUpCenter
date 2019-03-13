using System;

namespace Up.Models
{
    public class HocVienViewModel
    {
        public Guid HocVienId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string FacebookAccount { get; set; }
        public string ParentFullName { get; set; }
        public string ParentPhone { get; set; }
        public Guid QuanHeId { get; set; }
        public string QuanHe { get; set; }
        public string ParentFacebookAccount { get; set; }
        public string NgaySinh { get; set; }
        public string EnglishName { get; set; }
        public bool IsAppend { get; set; }
        public bool IsDisabled { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
