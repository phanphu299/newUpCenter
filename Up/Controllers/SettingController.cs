
namespace Up.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
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
        private readonly IQuyenService _quyenService;

        public SettingController(UserManager<IdentityUser> userManager, ISettingService settingService, IQuyenService quyenService)
        {
            _userManager = userManager;
            _settingService = settingService;
            _quyenService = quyenService;
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
        public async Task<IActionResult> GetAccountByRoleNameAsync(string RoleName)
        {
            var everyOne = await _settingService.GetAllUsersByRoleNameAsync(RoleName);
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
                    return Json(new ResultModel
                    {
                        Status = "Failed",
                        Message = "Reset lỗi !!!"
                    });
                }

                return Json(new ResultModel
                {
                    Status = "OK",
                    Message = "Reset thành công !!!"
                });
            }
            catch (Exception exception)
            {
                return Json(new ResultModel
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
                    return Json(new ResultModel
                    {
                        Status = "Failed",
                        Message = "Kích hoạt tài khoản lỗi !!!"
                    });
                }

                return Json(new ResultModel
                {
                    Status = "OK",
                    Message = "Kích hoạt tài khoản thành công !!!"
                });
            }
            catch (Exception exception)
            {
                return Json(new ResultModel
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
                    return Json(new ResultModel
                    {
                        Status = "Failed",
                        Message = "Khóa tài khoản lỗi !!!"
                    });
                }

                return Json(new ResultModel
                {
                    Status = "OK",
                    Message = "Khóa tài khoản thành công !!!"
                });
            }
            catch (Exception exception)
            {
                return Json(new ResultModel
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
                    return Json(new ResultModel
                    {
                        Status = "Failed",
                        Message = "Cập nhật lỗi !!!"
                    });
                }

                return Json(new ResultModel
                {
                    Status = "OK",
                    Message = "Cập nhật thành công !!!",
                    Result = successful
                });
            }
            catch (Exception exception)
            {
                return Json(new ResultModel
                {
                    Status = "Failed",
                    Message = exception.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateNewUserAsync(string Email)
        {
            if (string.IsNullOrWhiteSpace(Email))
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
                var successful = await _settingService.CreateNewUserAsync(Email);
                if (successful == null)
                {
                    return Json(new ResultModel
                    {
                        Status = "Failed",
                        Message = "Tạo tài khoản lỗi !!!"
                    });
                }

                return Json(new ResultModel
                {
                    Status = "OK",
                    Message = "Tạo tài khoản thành công !!!",
                    Result = successful
                });
            }
            catch (Exception exception)
            {
                return Json(new ResultModel
                {
                    Status = "Failed",
                    Message = exception.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUserAsync(string Id)
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
                var successful = await _settingService.RemoveUserAsync(Id);
                if (!successful)
                {
                    return Json(new ResultModel
                    {
                        Status = "Failed",
                        Message = "Xóa tài khoản lỗi !!!"
                    });
                }

                return Json(new ResultModel
                {
                    Status = "OK",
                    Message = "Xóa tài khoản thành công !!!",
                });
            }
            catch (Exception exception)
            {
                return Json(new ResultModel
                {
                    Status = "Failed",
                    Message = exception.Message
                });
            }
        }

        public async Task<IActionResult> RoleIndexAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateNewRoleAsync(string Name)
        {
            foreach(char item in Name)
            {
                if(item == ' ')
                {
                    return Json(new ResultModel
                    {
                        Status = "Failed",
                        Message = "Tên role phải viết liền không dấu !!!"
                    });
                }
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                return RedirectToAction("RoleIndexAsync");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("RoleIndexAsync");
            }

            try
            {
                var successful = await _settingService.CreateNewRoleAsync(Name);
                if (successful == null)
                {
                    return Json(new ResultModel
                    {
                        Status = "Failed",
                        Message = "Tạo role lỗi !!!"
                    });
                }

                return Json(new ResultModel
                {
                    Status = "OK",
                    Message = "Tạo role thành công !!!",
                    Result = successful
                });
            }
            catch (Exception exception)
            {
                return Json(new ResultModel
                {
                    Status = "Failed",
                    Message = exception.Message
                });
            }
        }

        public async Task<IActionResult> QuyenIndexAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetQuyenAsync()
        {
            var model = await _quyenService.GetAllAsync();
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetQuyenByRoleIdAsync(string RoleId)
        {
            var model = await _quyenService.GetAllByRoleIdAsync(RoleId);
            return Json(model);
        }

        [HttpPut]
        public async Task<IActionResult> AddQuyenToRoleAsync([FromBody]AddQuyenToRoleViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.RoleId))
            {
                return RedirectToAction("RoleIndexAsync");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("RoleIndexAsync");
            }

            try
            {
                var successful = await _quyenService.AddQuyenToRoleAsync(model);
                if (!successful)
                {
                    return Json(new ResultModel
                    {
                        Status = "Failed",
                        Message = "Cập nhật lỗi !!!"
                    });
                }

                return Json(new ResultModel
                {
                    Status = "OK",
                    Message = "Cập nhật thành công !!!",
                });
            }
            catch (Exception exception)
            {
                return Json(new ResultModel
                {
                    Status = "Failed",
                    Message = exception.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditRoleAsync(string RoleId, string Name)
        {
            if (string.IsNullOrWhiteSpace(RoleId))
            {
                return RedirectToAction("RoleIndexAsync");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("RoleIndexAsync");
            }

            try
            {
                var successful = await _settingService.EditRolesAsync(RoleId, Name);
                if (!successful)
                {
                    return Json(new ResultModel
                    {
                        Status = "Failed",
                        Message = "Cập nhật lỗi !!!"
                    });
                }

                return Json(new ResultModel
                {
                    Status = "OK",
                    Message = "Cập nhật thành công !!!",
                });
            }
            catch (Exception exception)
            {
                return Json(new ResultModel
                {
                    Status = "Failed",
                    Message = exception.Message
                });
            }
        }
    }
}