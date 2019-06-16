
namespace Up.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
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

        public async Task<bool> ActiveAsync(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                throw new Exception("Tài khoản không tồn tại!");
            }

            user.LockoutEnd = null;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new System.Exception("Lỗi khi kích hoạt tài khoản!");
            }
            return true;
        }

        public async Task<AccountInfo> AddRolesToUserAsync(string UserId, List<string> RoleIds)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var newRoles = await _context.Roles.Where(x => RoleIds.Contains(x.Id)).Select(x => x.Name).ToListAsync();

            if (user == null)
            {
                throw new Exception("Tài khoản không tồn tại!");
            }

            var oldRoleIds = _context.UserRoles.Where(x => x.UserId == UserId)
                                    .Select(x => x.RoleId)
                                    .ToList();

            if(oldRoleIds.Any())
            {
                var oldRoles = await _context.Roles.Where(x => oldRoleIds.Contains(x.Id)).Select(x => x.Name).ToListAsync();
                await _userManager.RemoveFromRolesAsync(user, oldRoles);
            }

            await _userManager.AddToRolesAsync(user, newRoles);

            return new AccountInfo {
                Email = user.Email,
                Id = user.Id,
                Roles = _userManager.GetRolesAsync(user).Result,
                RoleIds = _context.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId).ToList()
            };
        }

        public async Task<bool> ChangePasswordAsync(string UserId, string NewPassword = "M@tkhau@123")
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                throw new Exception("Tài khoản không tồn tại!");
            }
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, NewPassword);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Lỗi khi reset!");
            }
            return true;
        }

        public async Task<bool> CreateNewUserAsync(string Email)
        {
            try
            {
                if (await _userManager.FindByEmailAsync(Email) == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = Email,
                        Email = Email
                    };

                    IdentityResult result = _userManager.CreateAsync(user, "M@tkhau@123").Result;
                    return result.Succeeded;
                }
                else
                {
                    throw new Exception("Tài khoản đã tồn tại !!!");
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> DisableAsync(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                throw new System.Exception("Tài khoản không tồn tại!");
            }
            DateTime dateTime = new DateTime(9999, 12, 20);
            user.LockoutEnd = new System.DateTimeOffset(dateTime);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new System.Exception("Lỗi khi khóa tài khoản!");
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

        public async Task<List<RoleViewModel>> GetAllRolesAsync()
        {
            return await _context.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Role = x.Name
            }).ToListAsync();
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
                    Roles = _userManager.GetRolesAsync(item).Result,
                    RoleIds = _context.UserRoles.Where(x => x.UserId == item.Id).Select(x => x.RoleId).ToList()
                });
            }

            return userList;
        }
    }
}
