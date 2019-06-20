
namespace Up.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Up.Services;

    public class NhanVienKhacController : Controller
    {
        private readonly INhanVienKhacService _nhanVienKhacService;
        private readonly UserManager<IdentityUser> _userManager;

        public NhanVienKhacController(INhanVienKhacService nhanVienKhacService, UserManager<IdentityUser> userManager)
        {
            _nhanVienKhacService = nhanVienKhacService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetNhanVienAsync()
        {
            var model = await _nhanVienKhacService.GetNhanVienAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNhanVienAsync([FromBody]Models.NhanVienKhacViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                var successful = await _nhanVienKhacService.CreateNhanVienAsync(model.Name, model.Phone, model.BasicSalary, model.FacebookAccount, model.DiaChi, model.CMND, currentUser.Email);
                if (successful == null)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Thêm mới lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Thêm mới thành công !!!",
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

        [HttpDelete]
        public async Task<IActionResult> DeleteNhanVienAsync([FromBody]Models.NhanVienKhacViewModel model)
        {
            if (model.NhanVienKhacId == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                var successful = await _nhanVienKhacService.DeleteNhanVienAsync(model.NhanVienKhacId, currentUser.Email);
                if (!successful)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Xóa lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Xóa thành công !!!"
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

        [HttpPut]
        public async Task<IActionResult> UpdateNhanVienAsync([FromBody]Models.NhanVienKhacViewModel model)
        {
            if (model.NhanVienKhacId == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                var successful = await _nhanVienKhacService.UpdateNhanVienAsync(model.NhanVienKhacId, model.Name, model.Phone,
                    model.BasicSalary, model.FacebookAccount, model.DiaChi, model.CMND, currentUser.Email);
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