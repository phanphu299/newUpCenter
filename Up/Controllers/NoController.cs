
namespace Up.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Up.Services;

    [Authorize]
    public class NoController : Controller
    {
        private readonly INoService _noService;
        private readonly UserManager<IdentityUser> _userManager;

        public NoController(INoService noService, UserManager<IdentityUser> userManager)
        {
            _noService = noService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetNoByLopHocAsync(Guid LopHocId)
        {
            var model = await _noService.GetHocVien_NoByLopHoc(LopHocId);
            return Json(model);
        }
    }
}