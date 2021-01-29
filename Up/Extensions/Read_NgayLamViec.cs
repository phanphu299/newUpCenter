
namespace Up.Extensions
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Up.Data;
    using Up.Enums;

    public class Read_NgayLamViec : BasePolicy, IActionFilter
    {
        public Read_NgayLamViec(UserManager<IdentityUser> userManager, ApplicationDbContext context)
            : base(context, userManager)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            bool canAccess = CanAccessAsync(context.HttpContext.User, (int)QuyenEnums.Read_NgayLamViec, (int)QuyenEnums.Contribute_NgayLamViec).Result;

            if (!canAccess)
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult("~/Identity/Account/AccessDenied");
            return;
        }
    }
}
