namespace Up.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Up.Services;

    public class GiaoVienController : Controller
    {
        private readonly IGiaoVienService _giaoVienService;
        private readonly UserManager<IdentityUser> _userManager;

        public GiaoVienController(IGiaoVienService giaoVienService, UserManager<IdentityUser> userManager)
        {
            _giaoVienService = giaoVienService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetGiaoVienAsync()
        {
            var model = await _giaoVienService.GetGiaoVienAsync();
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetGiaoVienOnlyAsync()
        {
            var model = await _giaoVienService.GetGiaoVienOnlyAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGiaoVienAsync([FromBody]Models.GiaoVienViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                DateTime _ngayBatDau = Convert.ToDateTime(model.NgayBatDau, System.Globalization.CultureInfo.InvariantCulture);
                DateTime? _ngayKetThuc = null;
                if (!string.IsNullOrWhiteSpace(model.NgayKetThuc))
                    _ngayKetThuc = Convert.ToDateTime(model.NgayKetThuc, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _giaoVienService.CreateGiaoVienAsync(model.LoaiNhanVien_CheDo, model.Name, model.Phone, model.TeachingRate, model.TutoringRate,
                    model.BasicSalary, model.FacebookAccount, model.DiaChi, model.InitialName, model.CMND, model.MucHoaHong, model.NgayLamViecId, _ngayBatDau, _ngayKetThuc,
                    currentUser.Email);
                if (successful == null)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Thêm mới lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Thêm mới thành công !!!",
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

        [HttpDelete]
        public async Task<IActionResult> DeleteGiaoVienAsync([FromBody]Models.GiaoVienViewModel model)
        {
            if (model.GiaoVienId == Guid.Empty)
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
                var successful = await _giaoVienService.DeleteGiaoVienAsync(model.GiaoVienId, currentUser.Email);
                if (!successful)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Xóa lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Xóa thành công !!!"
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

        [HttpPut]
        public async Task<IActionResult> UpdateGiaoVienAsync([FromBody]Models.GiaoVienViewModel model)
        {
            if (model.GiaoVienId == Guid.Empty)
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
                DateTime _ngayBatDau = Convert.ToDateTime(model.NgayBatDau, System.Globalization.CultureInfo.InvariantCulture);
                DateTime? _ngayKetThuc = null;
                if(!string.IsNullOrWhiteSpace(model.NgayKetThuc))
                    _ngayKetThuc = Convert.ToDateTime(model.NgayKetThuc, System.Globalization.CultureInfo.InvariantCulture);
                var successful = await _giaoVienService.UpdateGiaoVienAsync(model.LoaiNhanVien_CheDo, model.GiaoVienId, model.Name, model.Phone, model.TeachingRate, model.TutoringRate,
                    model.BasicSalary, model.FacebookAccount, model.DiaChi, model.InitialName,
                    model.CMND, model.MucHoaHong, model.NgayLamViecId, _ngayBatDau, _ngayKetThuc, currentUser.Email);
                if (successful == null)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Cập nhật lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Cập nhật thành công !!!",
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