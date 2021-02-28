
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
    public class ChiPhiController : Controller
    {
        private readonly IChiPhiService _chiPhiService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Converters.Converter _converter;

        public ChiPhiController(
            IChiPhiService chiPhiService, 
            UserManager<IdentityUser> userManager,
            Converters.Converter converter)
        {
            _chiPhiService = chiPhiService;
            _userManager = userManager;
            _converter = converter;
        }

        [ServiceFilter(typeof(Read_TinhLuong))]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _chiPhiService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetChiPhiAsync(int month, int year)
        {
            var model = await _chiPhiService.TinhChiPhiAsync(month, year);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> LuuChiPhiAsync([FromBody] Models.Add_ThongKe_ChiPhiViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            DateTime _ngayChiPhi = new DateTime(model.models[0].year, model.models[0].month, 1);
            foreach (var item in model.models)
            {
                item.DaLuu = true;
            }
            var successful = await _chiPhiService.ThemThongKe_ChiPhiAsync(model.models, _ngayChiPhi, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Lưu Chi Phí thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Lưu Chi Phí lỗi !!!", false));
        }

        [HttpPost]
        public async Task<IActionResult> LuuNhapChiPhiAsync([FromBody] Models.Add_ThongKe_ChiPhiViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            DateTime _ngayChiPhi = new DateTime(model.models[0].year, model.models[0].month, 1);
            foreach (var item in model.models)
            {
                item.DaLuu = false;
            }
            var successful = await _chiPhiService.ThemThongKe_ChiPhiAsync(model.models, _ngayChiPhi, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Lưu Chi Phí thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Lưu Chi Phí lỗi !!!", false));
        }
    }
}