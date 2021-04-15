using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Up.Converters;
using Up.Extensions;
using Up.Models;
using Up.Services;

namespace Up.Controllers
{
    [Authorize]
    public class ThuThachController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Converter _converter;
        private readonly IThuThachService _thuThachService;

        public ThuThachController(UserManager<IdentityUser> userManager, Converter converter, IThuThachService thuThachService)
        {
            _userManager = userManager;
            _converter = converter;
            _thuThachService = thuThachService;
        }

        [ServiceFilter(typeof(Read_ThuThach))]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _thuThachService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetThuThachAsync()
        {
            var model = await _thuThachService.GetThuThachAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateThuThachAsync([FromBody] CreateThuThachInputModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }


            var successful = await _thuThachService.CreateThuThachAsync(model, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Thêm mới lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Thêm mới thành công !!!", true, successful));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteThuThachAsync([FromBody] ThuThachViewModel model)
        {
            if (model.ThuThachId == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            var successful = await _thuThachService.DeleteThuThachAsync(model.ThuThachId, currentUser.Email);
            return successful ?
               Json(_converter.ToResultModel("Xóa thành công !!!", true, successful))
               :
               Json(_converter.ToResultModel("Xóa lỗi !!!", false));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateThuThachAsync([FromBody] UpdateThuThachInputModel model)
        {
            if (model.ThuThachId == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            var successful = await _thuThachService.UpdateThuThachAsync(model, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Cập nhật lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Cập nhật thành công !!!", true, successful));
        }
    }
}
