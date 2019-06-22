
namespace Up.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Up.Services;

    public class ChiPhiController : Controller
    {
        private readonly IChiPhiService _chiPhiService;
        private readonly UserManager<IdentityUser> _userManager;

        public ChiPhiController(IChiPhiService chiPhiService, UserManager<IdentityUser> userManager)
        {
            _chiPhiService = chiPhiService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetChiPhiAsync()
        {
            var model = await _chiPhiService.TinhChiPhiAsync();
            return Json(model);
        }
    }
}