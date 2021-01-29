using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Data;

namespace Up.Extensions
{
    public class BasePolicy
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BasePolicy(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanAccessAsync(ClaimsPrincipal user, int readRight, int contributeRigth)
        {
            var curUser = await _userManager.GetUserAsync(user);

            var roles = await _userManager.GetRolesAsync(curUser);

            var quyen_roles = await _context.Quyen_Roles
                .Where(x => x.QuyenId == readRight || x.QuyenId == contributeRigth)
                .Select(x => x.RoleId)
                .ToListAsync();

            var allRoles = _context.Roles.Where(x => quyen_roles.Contains(x.Id)).Select(x => x.Name).AsNoTracking();

            bool canAccess = allRoles.Any(x => roles.Contains(x));

            //foreach (string role in roles)
            //{
            //    if (allRoles.Contains(role))
            //    {
            //        canAccess = true;
            //        break;
            //    }
            //}
            return canAccess;
        }
    }
}
