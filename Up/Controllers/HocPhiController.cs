
namespace Up.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
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
        public async Task<IActionResult> GetTinhHocPhiAsync(Guid LopHocId, int Month, int Year, int KhuyenMai, string GiaSachList)
        {
            var model = await _hocPhiService.TinhHocPhiAsync(LopHocId, Month, Year, KhuyenMai, GiaSachList);
            return Json(model);
        }
    }
}