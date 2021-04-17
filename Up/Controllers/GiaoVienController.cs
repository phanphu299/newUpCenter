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
    public class GiaoVienController : Controller
    {
        private readonly IGiaoVienService _giaoVienService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Converters.Converter _converter;

        public GiaoVienController(
            IGiaoVienService giaoVienService,
            UserManager<IdentityUser> userManager,
            Converters.Converter converter)
        {
            _giaoVienService = giaoVienService;
            _userManager = userManager;
            _converter = converter;
        }

        [ServiceFilter(typeof(Read_NhanVien))]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _giaoVienService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetGiaoVienAsync()
        {
            var model = await _giaoVienService.GetAllNhanVienAsync();
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetGiaoVienOnlyAsync()
        {
            var model = await _giaoVienService.GetGiaoVienOnlyAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGiaoVienAsync([FromBody] CreateGiaoVienInputModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            model.NgayBatDauDate = _converter.ToDateTime(model.NgayBatDau);

            if (!string.IsNullOrWhiteSpace(model.NgayKetThuc))
                model.NgayKetThucDate = _converter.ToDateTime(model.NgayKetThuc);

            var successful = await _giaoVienService.CreateGiaoVienAsync(model, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Thêm mới lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Thêm mới thành công !!!", true, successful));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGiaoVienAsync([FromBody] GiaoVienViewModel model)
        {
            if (model.GiaoVienId == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            var successful = await _giaoVienService.DeleteGiaoVienAsync(model.GiaoVienId, currentUser.Email);
            return successful ?
               Json(_converter.ToResultModel("Xóa thành công !!!", true, successful))
               :
               Json(_converter.ToResultModel("Xóa lỗi !!!", false));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGiaoVienAsync([FromBody] UpdateGiaoVienInputModel model)
        {
            if (model.GiaoVienId == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            model.NgayBatDauDate = _converter.ToDateTime(model.NgayBatDau);

            if (!string.IsNullOrWhiteSpace(model.NgayKetThuc))
                model.NgayKetThucDate = _converter.ToDateTime(model.NgayKetThuc);
            var successful = await _giaoVienService.UpdateGiaoVienAsync(model, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Cập nhật lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Cập nhật thành công !!!", true, successful));
        }
    }
}