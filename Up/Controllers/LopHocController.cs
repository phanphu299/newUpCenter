using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Up.Services;

namespace Up.Controllers
{
    public class LopHocController : Controller
    {
        private readonly ILopHocService _lopHocService;
        private readonly UserManager<IdentityUser> _userManager;

        public LopHocController(ILopHocService lopHocService, UserManager<IdentityUser> userManager)
        {
            _lopHocService = lopHocService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
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

                var successful = await _lopHocService.CreateLopHocAsync(model.Name, model.KhoaHocId, model.NgayHocId, model.GioHocId, model.HocPhiId, _ngayKhaiGiang, model.SachIds, model.GiaoVienId, currentUser.Email);
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
                    model.KhoaHocId, model.NgayHocId, model.GioHocId, model.HocPhiId, _ngayKhaiGiang,
                    _ngayKetThuc, model.IsCanceled, model.IsGraduated, model.SachIds, model.GiaoVienId, currentUser.Email);
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