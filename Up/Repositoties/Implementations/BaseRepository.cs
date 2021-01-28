using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Data;
using Up.Enums;

namespace Up.Repositoties
{
    public class BaseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BaseRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user, int right)
        {
            var curUser = await _userManager.GetUserAsync(user);

            var roles = await _userManager.GetRolesAsync(curUser);

            var quyen_roles = await _context.Quyen_Roles
                .Where(x => x.QuyenId == right)
                .Select(x => x.RoleId)
                .AsNoTracking()
                .ToListAsync();

            var allRoles = _context.Roles.Where(x => quyen_roles.Contains(x.Id)).Select(x => x.Name).AsNoTracking();

            bool canContribute = allRoles.Any(x => roles.Contains(x));

            //foreach (string role in roles)
            //{
            //    if (allRoles.Contains(role))
            //    {
            //        canContribute = true;
            //        break;
            //    }
            //}
            return canContribute;
        }
    }
}
