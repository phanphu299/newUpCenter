
namespace Up.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Models;
    using Up.Services;

    [Authorize(Roles = Constants.Admin)]
    public class SettingController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISettingService _settingService;

        public SettingController(UserManager<IdentityUser> userManager, ISettingService settingService)
        {
            _userManager = userManager;
            _settingService = settingService;
        }

        public async Task<IActionResult> AccountIndexAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountAsync()
        {
            var model = new ManageUsersViewModel
            {
                Administrators = _settingService.GetAdminsAsync().Result,
                Everyone = _settingService.GetAllUsersAsync().Result
            };

            return Json(model);
        }
    }
}