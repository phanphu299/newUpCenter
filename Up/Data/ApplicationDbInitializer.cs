using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Up.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync(Constants.Admin_Email).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = Constants.Admin_Email,
                    Email = Constants.Admin_Email
                };

                IdentityResult result = userManager.CreateAsync(user, Constants.Default_Password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.Admin).Wait();
                }
            }
            else
            {
                var isAdmin = userManager.IsInRoleAsync(userManager.FindByEmailAsync(Constants.Admin_Email).Result, Constants.Admin);
                if (!isAdmin.Result)
                {
                    userManager.AddToRoleAsync(userManager.FindByEmailAsync(Constants.Admin_Email).Result, Constants.Admin).Wait();
                }

            }
        }

        public static void SeedTrigram(ApplicationDbContext context)
        {
            var hocVien = context.HocViens
                .Where(x => !x.IsDisabled);

            List<string> addedList = new List<string>();
            foreach(var item in hocVien)
            {
                var name = Helpers.ToTiengVietKhongDau(item.FullName.Trim())
                    .Split(' ')
                    .ToList();
                string trigram = $"{name.First()[0]}";

                if (name[name.Count-1].StartsWith('('))
                {
                    trigram += name[name.Count - 2].Length >= 2 ?
                        $"{name[name.Count - 2].Substring(0, 2)}" 
                        : $"{name[name.Count - 2][0]}{name[name.Count - 2][0]}";
                }
                else
                {
                    trigram += name[name.Count - 1].Length >=2 ?
                        $"{name[name.Count - 1].Substring(0, 2)}"
                        : $"{name[name.Count - 1][0]}{name[name.Count - 1][0]}";
                }

                var latestTrigram = addedList.Where(x => x.ToLower().Contains(trigram.ToLower()))
                    .OrderByDescending(x => x)
                    .FirstOrDefault();

                if (latestTrigram == null)
                    trigram += 1.ToString("D2");
                else
                {
                    var currentNumber = int.Parse(latestTrigram.Substring(3, 2));
                    currentNumber++;
                    trigram += currentNumber.ToString("D2");
                }

                item.Trigram = trigram.ToUpper();
                addedList.Add(item.Trigram);
            }

            context.UpdateRange(hocVien);
            context.SaveChanges();
        }
    }
}
