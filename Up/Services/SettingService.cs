
namespace Up.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Models;

    public class SettingService : ISettingService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SettingService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> ChangePasswordAsync(string userId, string newPassword = "M@tkhau@123")
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new System.Exception("Tài khoản không tồn tại!");
            }
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new System.Exception("Lỗi khi reset!");
            }
            return true;
        }

        public async Task<List<AccountInfo>> GetAdminsAsync()
        {
            var admins = (await _userManager
                .GetUsersInRoleAsync(Constants.Admin))
                .ToArray();

            var adminList = new List<AccountInfo>();
            foreach(var item in admins)
            {
                adminList.Add(new AccountInfo
                {
                    Email = item.Email,
                    Id = item.Id,
                    Roles = _userManager.GetRolesAsync(item).Result
                });
            }

            return adminList;
        }

        public async Task<List<AccountInfo>> GetAllUsersAsync()
        {
            var everyone = await _userManager.Users
                .ToArrayAsync();

            var userList = new List<AccountInfo>();
            foreach (var item in everyone)
            {
                userList.Add(new AccountInfo
                {
                    Email = item.Email,
                    Id = item.Id,
                    Roles = _userManager.GetRolesAsync(item).Result
                });
            }

            return userList;
        }
    }
}
