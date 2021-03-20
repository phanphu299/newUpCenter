using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Up.Data;
using Up.Enums;

namespace Up.Extensions
{
    public class Read_HocPhiTronGoi : BasePolicy, IActionFilter
    {
        public Read_HocPhiTronGoi(UserManager<IdentityUser> userManager, ApplicationDbContext context)
            : base(context, userManager)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            bool canAccess = CanAccessAsync(context.HttpContext.User, (int)QuyenEnums.Read_HocPhiTronGoi, (int)QuyenEnums.Contribute_HocPhiTronGoi).Result;

            if (!canAccess)
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("~/Identity/Account/AccessDenied");
            return;
        }
    }
}
