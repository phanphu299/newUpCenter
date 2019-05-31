
namespace Up.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
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
            var admin = await _settingService.GetAdminsAsync();
            var everyOne = await _settingService.GetAllUsersAsync();
            var model = new ManageUsersViewModel
            {
                Administrators = admin,
                Everyone = everyOne
            };

            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> ResetMatKhauAsync(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return RedirectToAction("AccountIndexAsync");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("AccountIndexAsync");
            }

            try
            {
                var successful = await _settingService.ChangePasswordAsync(Id);
                if (!successful)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Reset lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Reset thành công !!!"
                });
            }
            catch (Exception exception)
            {
                return Json(new Models.ResultModel
                {
                    Status = "Failed",
                    Message = exception.Message
                });
            }
        }
    }
}