
namespace Up.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Quyen_Role
    {
        public Guid Quyen_RoleId { get; set; }
        public int QuyenId { get; set; }
        public string RoleId { get; set; }

        [ForeignKey("QuyenId")]
        public Quyen Quyen { get; set; }
    }
}
