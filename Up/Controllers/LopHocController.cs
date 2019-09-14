
namespace Up.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Up.Extensions;
    using Up.Services;

    [Authorize]
    public class LopHocController : Controller
    {
        private readonly ILopHocService _lopHocService;
        private readonly UserManager<IdentityUser> _userManager;

        public LopHocController(ILopHocService lopHocService, UserManager<IdentityUser> userManager)
        {
            _lopHocService = lopHocService;
            _userManager = userManager;
        }

        [ServiceFilter(typeof(Read_LopHoc))]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _lopHocService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetLopHocAsync()
        {
            var model = await _lopHocService.GetLopHocAsync();
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableLopHocAsync()
        {
            var model = await _lopHocService.GetAvailableLopHocAsync();
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetGraduatedAndCanceledLopHocAsync()
        {
            var model = await _lopHocService.GetGraduatedAndCanceledLopHocAsync();
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetLopHocByHocVienIdAsync(Guid HocVienId)
        {
            var model = await _lopHocService.GetLopHocByHocVienIdAsync(HocVienId);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLopHocAsync([FromBody]Models.LopHocViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }
            
            try
            {
                DateTime _ngayKhaiGiang = Convert.ToDateTime(model.NgayKhaiGiang, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _lopHocService.CreateLopHocAsync(model.Name, model.KhoaHocId, model.NgayHocId, model.GioHocId, _ngayKhaiGiang, currentUser.Email);
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
        public async Task<IActionResult> DeleteLopHocAsync([FromBody]Models.LopHocViewModel model)
        {
            if (model.LopHocId == Guid.Empty)
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
                var successful = await _lopHocService.DeleteLopHocAsync(model.LopHocId, currentUser.Email);
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
        public async Task<IActionResult> UpdateLopHocAsync([FromBody]Models.LopHocViewModel model)
        {
            if (model.LopHocId == Guid.Empty)
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
                DateTime _ngayKhaiGiang = Convert.ToDateTime(model.NgayKhaiGiang, System.Globalization.CultureInfo.InvariantCulture);
                DateTime? _ngayKetThuc = null;
                if (!string.IsNullOrWhiteSpace(model.NgayKetThuc))
                    _ngayKetThuc = Convert.ToDateTime(model.NgayKetThuc, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _lopHocService.UpdateLopHocAsync(model.LopHocId, model.Name,
                    model.KhoaHocId, model.NgayHocId, model.GioHocId, _ngayKhaiGiang,
                    _ngayKetThuc, currentUser.Email);
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

        [HttpPut]
        public async Task<IActionResult> UpdateTotNghiepAsync([FromBody]Models.LopHocViewModel model)
        {
            if (model.LopHocId == Guid.Empty)
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
                var successful = await _lopHocService.ToggleTotNghiepAsync(model.LopHocId, currentUser.Email);
                if (!successful)
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

        [HttpPut]
        public async Task<IActionResult> UpdateHuyLopAsync([FromBody]Models.LopHocViewModel model)
        {
            if (model.LopHocId == Guid.Empty)
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
                var successful = await _lopHocService.ToggleHuyLopAsync(model.LopHocId, currentUser.Email);
                if (!successful)
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