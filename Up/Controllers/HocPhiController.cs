
namespace Up.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Up.Services;

    public class HocPhiController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHocPhiService _hocPhiService;
        private readonly IThongKe_DoanhThuHocPhiService _thongKe_DoanhThuHocPhiService;
        private readonly INoService _noService;

        public HocPhiController(UserManager<IdentityUser> userManager, IHocPhiService hocPhiService, IThongKe_DoanhThuHocPhiService thongKe_DoanhThuHocPhiService, INoService noService)
        {
            _userManager = userManager;
            _hocPhiService = hocPhiService;
            _thongKe_DoanhThuHocPhiService = thongKe_DoanhThuHocPhiService;
            _noService = noService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTinhHocPhiAsync(Guid LopHocId, int Month, int Year)
        {
            var model = await _hocPhiService.TinhHocPhiAsync(LopHocId, Month, Year);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> LuuDoanhThu_HocPhiAsync([FromBody]Models.ThongKe_DoanhThuHocPhiViewModel model)
        {
            if (model.LopHocId == Guid.Empty || model.HocVienId == Guid.Empty)
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
                DateTime _ngayDong = new DateTime(model.year, model.month, 1);
                var successful = await _thongKe_DoanhThuHocPhiService.ThemThongKe_DoanhThuHocPhiAsync(model.LopHocId, model.HocVienId,
                    model.HocPhi, _ngayDong, currentUser.Email);
                if (successful == false)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Lưu Doanh Thu lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Lưu Doanh Thu thành công !!!",
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

        [HttpPost]
        public async Task<IActionResult> LuuNo_HocPhiAsync([FromBody]Models.NoViewModel model)
        {
            if (model.LopHocId == Guid.Empty || model.HocVienId == Guid.Empty)
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
                DateTime _ngayNo = new DateTime(model.year, model.month, 1);
                var successful = await _noService.ThemHocVien_NoAsync(model.LopHocId, model.HocVienId,
                    model.TienNo, _ngayNo, currentUser.Email);
                if (successful == false)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Lưu Nợ lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Lưu Nợ thành công !!!",
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