
namespace Up.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Services;

    public class HocPhiController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHocPhiService _hocPhiService;

        public HocPhiController(UserManager<IdentityUser> userManager, IHocPhiService hocPhiService)
        {
            _userManager = userManager;
            _hocPhiService = hocPhiService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTongNgayHocAsync(Guid LopHocId, int Month, int Year)
        {
            int model = await _hocPhiService.TinhSoNgayHocAsync(LopHocId, Month, Month);
            return Json(model);
        }
    }
}