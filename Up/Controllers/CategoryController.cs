using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Up.Services;

namespace Up.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IKhoaHocService _khoaHocService;
        private readonly IGioHocService _gioHocService;
        private readonly INgayHocService _ngayHocService;
        private readonly IQuanHeService _quanHeService;
        private readonly IHocPhiService _hocPhiService;
        private readonly UserManager<IdentityUser> _userManager;

        public CategoryController(IKhoaHocService khoaHocService, IGioHocService gioHocService, INgayHocService ngayHocService, IQuanHeService quanHeService, IHocPhiService hocPhiService, UserManager<IdentityUser> userManager)
        {
            _khoaHocService = khoaHocService;
            _gioHocService = gioHocService;
            _ngayHocService = ngayHocService;
            _quanHeService = quanHeService;
            _hocPhiService = hocPhiService;
            _userManager = userManager;
        }

        public async Task<IActionResult> KhoaHocIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetKhoaHocAsync()
        {
            var model = await _khoaHocService.GetKhoaHocAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateKhoaHocAsync([FromBody]Models.KhoaHocViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("KhoaHocIndex");
            }

            var successful = await _khoaHocService.CreateKhoaHocAsync(model.Name, currentUser.Email);
            if (successful == null)
            {
                return BadRequest("Thêm mới lỗi !!!");
            }

            return Json(successful);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateKhoaHocAsync([FromBody]Models.KhoaHocViewModel model)
        {
            if (model.KhoaHocId == Guid.Empty)
            {
                return RedirectToAction("KhoaHocIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("KhoaHocIndex");
            }

            var successful = await _khoaHocService.UpdateKhoaHocAsync(model.KhoaHocId, model.Name, currentUser.Email);
            if (!successful)
            {
                return BadRequest("Cập nhật lỗi !!!");
            }

            return Ok(200);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteKhoaHocAsync([FromBody]Models.KhoaHocViewModel model)
        {
            if(await _khoaHocService.IsCanDeleteAsync(model.KhoaHocId))
            {
                return BadRequest("Hãy xóa những lớp học thuộc khóa học này trước !!!");
            }

            if (model.KhoaHocId == Guid.Empty)
            {
                return RedirectToAction("KhoaHocIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("KhoaHocIndex");
            }

            var successful = await _khoaHocService.DeleteKhoaHocAsync(model.KhoaHocId, currentUser.Email);
            if (!successful)
            {
                return BadRequest("Xóa lỗi !!!");
            }

            return Ok(200);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> QuanHeIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetQuanHeAsync()
        {
            var model = await _quanHeService.GetQuanHeAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuanHeAsync([FromBody]Models.QuanHeViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("QuanHeIndex");
            }

            var successful = await _quanHeService.CreateQuanHeAsync(model.Name, currentUser.Email);
            if (successful == null)
            {
                return BadRequest("Thêm mới lỗi !!!");
            }

            return Json(successful);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuanHeAsync([FromBody]Models.QuanHeViewModel model)
        {
            if (model.QuanHeId == Guid.Empty)
            {
                return RedirectToAction("QuanHeIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("QuanHeIndex");
            }

            var successful = await _quanHeService.UpdateQuanHeAsync(model.QuanHeId, model.Name, currentUser.Email);
            if (!successful)
            {
                return BadRequest("Cập nhật lỗi !!!");
            }

            return Ok(200);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteQuanHeAsync([FromBody]Models.QuanHeViewModel model)
        {
            if (model.QuanHeId == Guid.Empty)
            {
                return RedirectToAction("QuanHeIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("QuanHeIndex");
            }

            var successful = await _quanHeService.DeleteQuanHeAsync(model.QuanHeId, currentUser.Email);
            if (!successful)
            {
                return BadRequest("Xóa lỗi !!!");
            }

            return Ok(200);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> NgayHocIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetNgayHocAsync()
        {
            var model = await _ngayHocService.GetNgayHocAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNgayHocAsync([FromBody]Models.NgayHocViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("NgayHocIndex");
            }

            var successful = await _ngayHocService.CreateNgayHocAsync(model.Name, currentUser.Email);
            if (successful == null)
            {
                return BadRequest("Thêm mới lỗi !!!");
            }

            return Json(successful);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNgayHocAsync([FromBody]Models.NgayHocViewModel model)
        {
            if (model.NgayHocId == Guid.Empty)
            {
                return RedirectToAction("NgayHocIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("NgayHocIndex");
            }

            var successful = await _ngayHocService.UpdateNgayHocAsync(model.NgayHocId, model.Name, currentUser.Email);
            if (!successful)
            {
                return BadRequest("Cập nhật lỗi !!!");
            }

            return Ok(200);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNgayHocAsync([FromBody]Models.NgayHocViewModel model)
        {
            if (await _ngayHocService.IsCanDeleteAsync(model.NgayHocId))
            {
                return BadRequest("Hãy xóa những lớp học thuộc ngày học này trước !!!");
            }

            if (model.NgayHocId == Guid.Empty)
            {
                return RedirectToAction("NgayHocIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("NgayHocIndex");
            }

            var successful = await _ngayHocService.DeleteNgayHocAsync(model.NgayHocId, currentUser.Email);
            if (!successful)
            {
                return BadRequest("Xóa lỗi !!!");
            }

            return Ok(200);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> GioHocIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetGioHocAsync()
        {
            var model = await _gioHocService.GetGioHocAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGioHocAsync([FromBody]Models.GioHocViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("GioHocIndex");
            }

            var successful = await _gioHocService.CreateGioHocAsync(model.Name, currentUser.Email);
            if (successful == null)
            {
                return BadRequest("Thêm mới lỗi !!!");
            }

            return Json(successful);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGioHocAsync([FromBody]Models.GioHocViewModel model)
        {
            if (model.GioHocId == Guid.Empty)
            {
                return RedirectToAction("GioHocIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("GioHocIndex");
            }

            var successful = await _gioHocService.UpdateGioHocAsync(model.GioHocId, model.Name, currentUser.Email);
            if (!successful)
            {
                return BadRequest("Cập nhật lỗi !!!");
            }

            return Ok(200);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGioHocAsync([FromBody]Models.GioHocViewModel model)
        {
            if (model.GioHocId == Guid.Empty)
            {
                return RedirectToAction("GioHocIndex");
            }

            if (await _gioHocService.IsCanDeleteAsync(model.GioHocId))
            {
                return BadRequest("Hãy xóa những lớp học thuộc giờ học này trước !!!");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("GioHocIndex");
            }

            var successful = await _gioHocService.DeleteGioHocAsync(model.GioHocId, currentUser.Email);
            if (!successful)
            {
                return BadRequest("Xóa lỗi !!!");
            }

            return Ok(200);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> HocPhiIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetHocPhiAsync()
        {
            var model = await _hocPhiService.GetHocPhiAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHocPhiAsync([FromBody]Models.HocPhiViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("HocPhiIndex");
            }

            var successful = await _hocPhiService.CreateHocPhiAsync(model.Gia, currentUser.Email);
            if (successful == null)
            {
                return BadRequest("Thêm mới lỗi !!!");
            }

            return Json(successful);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHocPhiAsync([FromBody]Models.HocPhiViewModel model)
        {
            if (model.HocPhiId == Guid.Empty)
            {
                return RedirectToAction("HocPhiIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("HocPhiIndex");
            }

            var successful = await _hocPhiService.UpdateHocPhiAsync(model.HocPhiId, model.Gia, currentUser.Email);
            if (!successful)
            {
                return BadRequest("Cập nhật lỗi !!!");
            }

            return Ok(200);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHocPhiAsync([FromBody]Models.HocPhiViewModel model)
        {
            if (model.HocPhiId == Guid.Empty)
            {
                return RedirectToAction("HocPhiIndex");
            }

            //if (await _hocPhiService.IsCanDeleteAsync(model.HocPhiId))
            //{
            //    return BadRequest("Hãy xóa những lớp học có học phí này trước !!!");
            //}

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("HocPhiIndex");
            }

            var successful = await _hocPhiService.DeleteHocPhiAsync(model.HocPhiId, currentUser.Email);
            if (!successful)
            {
                return BadRequest("Xóa lỗi !!!");
            }

            return Ok(200);
        }
    }
}