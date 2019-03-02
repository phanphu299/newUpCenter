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

        [HttpPost]
        public async Task<IActionResult> CreateLopHocAsync([FromBody]Models.LopHocViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            DateTime _ngayKhaiGiang = Convert.ToDateTime(model.NgayKhaiGiang, System.Globalization.CultureInfo.InvariantCulture);

            var successful = await _lopHocService.CreateLopHocAsync(model.Name, model.KhoaHocId, model.NgayHocId, model.GioHocId, _ngayKhaiGiang, currentUser.Email);
            if (successful == null)
            {
                return BadRequest("Thêm mới lỗi !!!");
            }

            return Json(successful);
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
            if (!successful)
            {
                return BadRequest("Xóa lỗi !!!");
            }

            return Ok(200);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLopHocAsync([FromBody]Models.LopHocViewModel model)
        {
            if (model.LopHocId == Guid.Empty)
            {
                return RedirectToAction("LopHocIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("LopHocIndex");
            }

            DateTime _ngayKhaiGiang = Convert.ToDateTime(model.NgayKhaiGiang, System.Globalization.CultureInfo.InvariantCulture);
            DateTime? _ngayKetThuc = null;
            if (!string.IsNullOrWhiteSpace(model.NgayKetThuc))
                _ngayKetThuc = Convert.ToDateTime(model.NgayKetThuc, System.Globalization.CultureInfo.InvariantCulture);

            var successful = await _lopHocService.UpdateLopHocAsync(model.LopHocId, model.Name, model.KhoaHocId, model.NgayHocId, model.GioHocId, _ngayKhaiGiang, _ngayKetThuc, model.IsCanceled, model.IsGraduated, currentUser.Email);
            if (successful == null)
            {
                return Json("Cập nhật lỗi !!!");
            }

            return Json(successful);
        }
    }
}