namespace Up.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Up.Services;
    using System.Threading.Tasks;
    using System;

    public class DiemDanhController : Controller
    {
        private readonly IDiemDanhService _diemDanhService;
        private readonly UserManager<IdentityUser> _userManager;

        public DiemDanhController(IDiemDanhService diemDanhService, UserManager<IdentityUser> userManager)
        {
            _diemDanhService = diemDanhService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetHocVienByLopHocAsync(Guid LopHocId)
        {
            var model = await _diemDanhService.GetHocVienByLopHoc(LopHocId);
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetDiemDanhByHocVienAndLopHocAsync(Guid HocVienId, Guid LopHocId)
        {
            var model = await _diemDanhService.GetDiemDanhByHocVienAndLopHoc(HocVienId, LopHocId);
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetDiemDanhByLopHocAsync(Guid LopHocId)
        {
            var model = await _diemDanhService.GetDiemDanhByLopHoc(LopHocId);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> DiemDanhTungHocVienAsync([FromBody]Models.LopHoc_DiemDanhViewModel model)
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
                DateTime _ngayDiemDanh = Convert.ToDateTime(model.NgayDiemDanh, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _diemDanhService.DiemDanhTungHocVienAsync(model.LopHocId, model.HocVienId,
                    model.IsOff, _ngayDiemDanh, currentUser.Email);
                if (successful == false)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Điểm Danh lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Điểm Danh thành công !!!",
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
        public async Task<IActionResult> DiemDanhTatCaAsync([FromBody]Models.LopHoc_DiemDanhViewModel model)
        {
            if (model.LopHocId == Guid.Empty)
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
                DateTime _ngayDiemDanh = Convert.ToDateTime(model.NgayDiemDanh, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _diemDanhService.DiemDanhTatCaAsync(model.LopHocId,
                    model.IsOff, _ngayDiemDanh, currentUser.Email);
                if (successful == false)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Điểm Danh lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Điểm Danh thành công !!!",
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
        public async Task<IActionResult> LopNghiAsync([FromBody]Models.LopHoc_DiemDanhViewModel model)
        {
            if (model.LopHocId == Guid.Empty)
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
                DateTime _ngayDiemDanh = Convert.ToDateTime(model.NgayDiemDanh, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _diemDanhService.DuocNghi(model.LopHocId, _ngayDiemDanh, currentUser.Email);
                if (successful == false)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Điểm Danh lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Cho Lớp Nghỉ thành công !!!",
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