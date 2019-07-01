﻿
namespace Up.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Up.Services;

    public class ChiPhiController : Controller
    {
        private readonly IChiPhiService _chiPhiService;
        private readonly IThongKe_ChiPhiService _thongKe_ChiPhiService;
        private readonly UserManager<IdentityUser> _userManager;

        public ChiPhiController(IChiPhiService chiPhiService, IThongKe_ChiPhiService thongKe_ChiPhiService, UserManager<IdentityUser> userManager)
        {
            _chiPhiService = chiPhiService;
            _thongKe_ChiPhiService = thongKe_ChiPhiService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetChiPhiAsync()
        {
            var model = await _chiPhiService.TinhChiPhiAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> LuuChiPhiAsync([FromBody]Models.ThongKe_ChiPhiViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                DateTime _ngayChiPhi = new DateTime(model.year, model.month, 1);
                var successful = await _thongKe_ChiPhiService.ThemThongKe_ChiPhiAsync(model.ChiPhi, _ngayChiPhi, currentUser.Email);
                if (successful == false)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Lưu Chi Phí lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Lưu Chi Phí thành công !!!",
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