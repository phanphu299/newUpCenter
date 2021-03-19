using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Up.Data;
using Up.Enums;

namespace Up.Extensions
{
    public class Read_ChiPhiKhac : BasePolicy, IActionFilter
    {
        public Read_ChiPhiKhac(UserManager<IdentityUser> userManager, ApplicationDbContext context)
            : base(context, userManager)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            bool canAccess = CanAccessAsync(context.HttpContext.User, (int)QuyenEnums.Read_ChiPhiKhac, (int)QuyenEnums.Contribute_ChiPhiKhac).Result;

            if (!canAccess)
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("~/Identity/Account/AccessDenied");
            return;
        }
    }
}
