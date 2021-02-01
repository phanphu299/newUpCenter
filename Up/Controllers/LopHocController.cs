
namespace Up.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Up.Extensions;
    using Up.Models;
    using Up.Services;

    [Authorize]
    public class LopHocController : Controller
    {
        private readonly ILopHocService _lopHocService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Converters.Converter _converter;

        public LopHocController(ILopHocService lopHocService, UserManager<IdentityUser> userManager, Converters.Converter converter)
        {
            _lopHocService = lopHocService;
            _userManager = userManager;
            _converter = converter;
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
        public async Task<IActionResult> GetAvailableLopHocWithTimeAsync(int thang, int nam)
        {
            var model = await _lopHocService.GetAvailableLopHocAsync(thang, nam);
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetGraduatedAndCanceledLopHocAsync()
        {
            var model = await _lopHocService.GetGraduatedAndCanceledLopHocAsync();
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetLopHocByHocVienIdAsync(Guid hocVienId)
        {
            var model = await _lopHocService.GetLopHocByHocVienIdAsync(hocVienId);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLopHocAsync([FromBody] CreateLopHocInputModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            model.NgayKhaiGiangDate = _converter.ToDateTime(model.NgayKhaiGiang);

            var successful = await _lopHocService.CreateLopHocAsync(model, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Thêm mới lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Thêm mới thành công !!!", true, successful));
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

            var successful = await _lopHocService.DeleteLopHocAsync(model.LopHocId, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Xóa thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Xóa lỗi !!!", false));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLopHocAsync([FromBody] UpdateLopHocInputModel model)
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

            model.NgayKhaiGiangDate = _converter.ToDateTime(model.NgayKhaiGiang);
            if (!string.IsNullOrWhiteSpace(model.NgayKetThuc))
                model.NgayKetThucDate = _converter.ToDateTime(model.NgayKetThuc);

            var successful = await _lopHocService.UpdateLopHocAsync(model, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Cập nhật lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Cập nhật thành công !!!", true, successful));
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

            var successful = await _lopHocService.ToggleTotNghiepAsync(model.LopHocId, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Cập nhật thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Cập nhật lỗi !!!", false));
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

            var successful = await _lopHocService.ToggleHuyLopAsync(model.LopHocId, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Cập nhật thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Cập nhật lỗi !!!", false));
        }
    }
}