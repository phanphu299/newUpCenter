namespace Up.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ManageUsersViewModel
    {
        public IdentityUser[] Administrators { get; set; }

        public IdentityUser[] Everyone { get; set; }
    }
}
