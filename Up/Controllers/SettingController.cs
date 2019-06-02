
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
            var everyOne = await _settingService.GetAllUsersAsync();
            return Json(everyOne);
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

        [HttpGet]
        public async Task<IActionResult> KichHoatTaiKhoanAsync(string Id)
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
                var successful = await _settingService.ActiveAsync(Id);
                if (!successful)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Kích hoạt tài khoản lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Kích hoạt tài khoản thành công !!!"
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

        [HttpGet]
        public async Task<IActionResult> KhoaTaiKhoanAsync(string Id)
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
                var successful = await _settingService.DisableAsync(Id);
                if (!successful)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Khóa tài khoản lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Khóa tài khoản thành công !!!"
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

        [HttpGet]
        public async Task<IActionResult> GetRolesAsync()
        {
            var model = await _settingService.GetAllRolesAsync();
            return Json(model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoleAsync([FromBody]AccountInfo model)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
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
                var successful = await _settingService.AddRolesToUserAsync(model.Id, model.RoleIds.ToList());
                if (successful == null)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Cập nhật lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Cập nhật thành công !!!",
                    Result = successful
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