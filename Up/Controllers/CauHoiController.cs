using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Up.Converters;
using Up.Models;
using Up.Services;

namespace Up.Controllers
{
    [Authorize]
    public class CauHoiController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Converter _converter;
        private readonly ICauHoiService _cauHoiService;

        public CauHoiController(UserManager<IdentityUser> userManager, Converter converter, ICauHoiService cauHoiService)
        {
            _userManager = userManager;
            _converter = converter;
            _cauHoiService = cauHoiService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCauHoiAsync()
        {
            var model = await _cauHoiService.GetCauHoiAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCauHoiAsync([FromBody] CreateCauHoiInputModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }


            var successful = await _cauHoiService.CreateCauHoiAsync(model, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Thêm mới lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Thêm mới thành công !!!", true, successful));
        }
    }
}
