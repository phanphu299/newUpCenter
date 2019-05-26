namespace Up.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Up.Services;

    public class HocVienController : Controller
    {
        private readonly IHocVienService _hocVienService;
        private readonly INgayHocService _ngayHocService;
        private readonly UserManager<IdentityUser> _userManager;

        public HocVienController(IHocVienService hocVienService, INgayHocService ngayHocService, UserManager<IdentityUser> userManager)
        {
            _hocVienService = hocVienService;
            _ngayHocService = ngayHocService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetHocVienAsync()
        {
            var model = await _hocVienService.GetHocVienAsync();
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetHocVien_LopHocByHocVienAsync(Guid HocVienId, Guid LopHocId)
        {
            var model = await _ngayHocService.GetHocVien_NgayHocByHocVienAsync(HocVienId, LopHocId);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHocVienAsync([FromBody]Models.HocVienViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                DateTime _ngaySinh = Convert.ToDateTime(model.NgaySinh, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _hocVienService.CreateHocVienAsync(model.FullName, model.Phone, model.FacebookAccount, model.ParentFullName,
                    model.ParentPhone, model.ParentFacebookAccount, model.QuanHeId, model.EnglishName, _ngaySinh, model.IsAppend, model.LopHocIds, currentUser.Email);
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

        [HttpPost]
        public async Task<IActionResult> CreateUpdateHocVien_ngayHocAsync([FromBody]Models.HocVien_NgayHocViewModel model)
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

                var successful = await _ngayHocService.CreateUpdateHocVien_NgayHocAsync(model.HocVienId, model.LopHocId, _ngayBatDau, _ngayKetThuc, currentUser.Email);
                if (!successful)
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

        [HttpPost]
        public async Task<IActionResult> AddHocVienToLopCuAsync([FromBody]Models.HocVien_LopViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                var successful = await _hocVienService.AddToUnavailableClassAsync(model.LopHocId, model.HocVienId, currentUser.Email);
                if (!successful)
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
        public async Task<IActionResult> DeleteHocVienAsync([FromBody]Models.HocVienViewModel model)
        {
            if (model.HocVienId == Guid.Empty)
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
                var successful = await _hocVienService.DeleteHocVienAsync(model.HocVienId, currentUser.Email);
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
        public async Task<IActionResult> UpdateChenAsync([FromBody]Models.HocVienViewModel model)
        {
            if (model.HocVienId == Guid.Empty)
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
                var successful = await _hocVienService.ToggleChenAsync(model.HocVienId, currentUser.Email);
                if (!successful)
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

        [HttpPut]
        public async Task<IActionResult> UpdateHocVienAsync([FromBody]Models.HocVienViewModel model)
        {
            if (model.HocVienId == Guid.Empty)
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
                DateTime _ngaySinh = Convert.ToDateTime(model.NgaySinh, System.Globalization.CultureInfo.InvariantCulture);
                
                var successful = await _hocVienService.UpdateHocVienAsync(model.HocVienId, model.FullName, model.Phone,
                   model.FacebookAccount, model.ParentFullName, model.ParentPhone, model.ParentFacebookAccount, model.QuanHeId,
                   model.EnglishName, _ngaySinh, model.LopHocIds, currentUser.Email);
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