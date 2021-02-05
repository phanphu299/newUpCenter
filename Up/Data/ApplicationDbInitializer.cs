using Microsoft.AspNetCore.Identity;

namespace Up.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("huynhquan.nguyen@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "huynhquan.nguyen@gmail.com",
                    Email = "huynhquan.nguyen@gmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssword@123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.Admin).Wait();
                }
            }
            else
            {
                var isAdmin = userManager.IsInRoleAsync(userManager.FindByEmailAsync("huynhquan.nguyen@gmail.com").Result, Constants.Admin);
                if (!isAdmin.Result)
                {
                    userManager.AddToRoleAsync(userManager.FindByEmailAsync("huynhquan.nguyen@gmail.com").Result, Constants.Admin).Wait();
                }

            }
        }
    }
}
