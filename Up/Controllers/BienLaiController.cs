using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Up.Converters;
using Up.Services;

namespace Up.Controllers
{
    [Authorize]
    public class BienLaiController : Controller
    {
        private readonly IBienLaiService _bienLaiService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Converter _converter;

        public BienLaiController(
            IBienLaiService bienLaiService, 
            UserManager<IdentityUser> userManager, 
            Converter converter)
        {
            _bienLaiService = bienLaiService;
            _userManager = userManager;
            _converter = converter;
        }

        [HttpGet]
        public async Task<IActionResult> GetBienLaiAsync()
        {
            var model = await _bienLaiService.GetBienLaiAsync();
            return Json(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBienLaiAsync([FromBody] Models.BienLaiViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var successful = await _bienLaiService.DeleteBienLaiAsync(model.BienLaiId, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Xóa thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Xóa nhật lỗi !!!", false));
        }
    }
}
