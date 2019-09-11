
namespace Up.Data.Entities
{
    using System.Collections.Generic;

    public class Quyen
    {
        public int QuyenId { get; set; }
        public string Name { get; set; }

        public ICollection<Quyen_Role> Quyen_Roles { get; set; }
    }
}
