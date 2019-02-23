using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Up.Services;

namespace Up.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IKhoaHocService _khoaHocService;
        private readonly UserManager<IdentityUser> _userManager;

        public CategoryController(IKhoaHocService khoaHocService, UserManager<IdentityUser> userManager)
        {
            _khoaHocService = khoaHocService;
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
    }
}