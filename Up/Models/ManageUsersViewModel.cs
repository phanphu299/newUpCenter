namespace Up.Models
{
    using System.Collections.Generic;

    public class ManageUsersViewModel
    {
        public List<AccountInfo> Administrators { get; set; }

        public List<AccountInfo> Everyone { get; set; }
    }

    public class AccountInfo
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<string> RoleIds { get; set; }
    }
}
