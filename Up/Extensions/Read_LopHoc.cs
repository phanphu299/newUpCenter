

namespace Up.Extensions
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Enums;

    public class Read_LopHoc : IActionFilter
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public Read_LopHoc(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var CurUser = _userManager.GetUserAsync(context.HttpContext.User).Result;

            var roles = _userManager.GetRolesAsync(CurUser).Result;

            var quyen_roles = _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Read_LopHoc || x.QuyenId == (int)QuyenEnums.Contribute_LopHoc)
                .Select(x => x.RoleId).ToList();

            var allRoles = _context.Roles.Where(x => quyen_roles.Contains(x.Id)).Select(x => x.Name);

            bool canAccess = false;

            foreach (string role in roles)
            {
                if (allRoles.Contains(role))
                {
                    canAccess = true;
                    break;
                }
            }

            if (!canAccess)
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("~/Identity/Account/AccessDenied");
            return;
        }
    }
}
