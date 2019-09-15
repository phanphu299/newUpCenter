
namespace Up.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Extensions;
    using Up.Services;

    [Authorize]
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
        public async Task<IActionResult> LuuChiPhiAsync([FromBody]Models.Add_ThongKe_ChiPhiViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                DateTime _ngayChiPhi = new DateTime(model.models[0].year, model.models[0].month, 1);
                foreach(var item in model.models)
                {
                    item.DaLuu = true;
                }
                var successful = await _thongKe_ChiPhiService.ThemThongKe_ChiPhiAsync(model.models, _ngayChiPhi, currentUser.Email);
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
                    //Result = successful
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
        public async Task<IActionResult> LuuNhapChiPhiAsync([FromBody]Models.Add_ThongKe_ChiPhiViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                DateTime _ngayChiPhi = new DateTime(model.models[0].year, model.models[0].month, 1);
                foreach (var item in model.models)
                {
                    item.DaLuu = false;
                }
                var successful = await _thongKe_ChiPhiService.ThemThongKe_ChiPhiAsync(model.models, _ngayChiPhi, currentUser.Email);
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
                    //Result = successful
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