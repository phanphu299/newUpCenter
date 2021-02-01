namespace Up.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Up.Extensions;
    using Up.Models;
    using Up.Services;

    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IKhoaHocService _khoaHocService;
        private readonly IGioHocService _gioHocService;
        private readonly INgayHocService _ngayHocService;
        private readonly IQuanHeService _quanHeService;
        private readonly IHocPhiService _hocPhiService;
        private readonly ISachService _sachService;
        private readonly ILoaiGiaoVienService _loaiGiaoVienService;
        private readonly ILoaiCheDoService _loaiCheDoService;
        private readonly IChiPhiCoDinhService _chiPhiCoDinhService;
        private readonly INgayLamViecService _ngayLamViecService;
        private readonly IChiPhiKhacService _chiPhiKhacService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Converters.Converter _converter;

        public CategoryController(
            IChiPhiKhacService chiPhiKhacService,
            IKhoaHocService khoaHocService, 
            IGioHocService gioHocService, 
            INgayHocService ngayHocService,
            IQuanHeService quanHeService,
            IHocPhiService hocPhiService, 
            ISachService sachService,
            ILoaiGiaoVienService loaiGiaoVienService,
            ILoaiCheDoService loaiCheDoService, 
            IChiPhiCoDinhService chiPhiCoDinhService,
            INgayLamViecService ngayLamViecService, 
            UserManager<IdentityUser> userManager,
            Converters.Converter converter)
        {
            _chiPhiKhacService = chiPhiKhacService;
            _khoaHocService = khoaHocService;
            _gioHocService = gioHocService;
            _ngayHocService = ngayHocService;
            _quanHeService = quanHeService;
            _hocPhiService = hocPhiService;
            _sachService = sachService;
            _loaiGiaoVienService = loaiGiaoVienService;
            _loaiCheDoService = loaiCheDoService;
            _chiPhiCoDinhService = chiPhiCoDinhService;
            _ngayLamViecService = ngayLamViecService;
            _userManager = userManager;
            _converter = converter;
        }

        [ServiceFilter(typeof(Read_KhoaHoc))]
        public async Task<IActionResult> KhoaHocIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _khoaHocService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetKhoaHocAsync()
        {
            var model = await _khoaHocService.GetKhoaHocAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateKhoaHocAsync([FromBody]Models.KhoaHocViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("KhoaHocIndex");
            }

            try {
                var successful = await _khoaHocService.CreateKhoaHocAsync(model.Name, currentUser.Email);
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

        [HttpPut]
        public async Task<IActionResult> UpdateKhoaHocAsync([FromBody]Models.KhoaHocViewModel model)
        {
            if (model.KhoaHocId == Guid.Empty)
            {
                return RedirectToAction("KhoaHocIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("KhoaHocIndex");
            }

            try
            {
                var successful = await _khoaHocService.UpdateKhoaHocAsync(model.KhoaHocId, model.Name, currentUser.Email);
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
                    Message = "Cập nhật thành công !!!"
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
        public async Task<IActionResult> DeleteKhoaHocAsync([FromBody]Models.KhoaHocViewModel model)
        {
            if (model.KhoaHocId == Guid.Empty)
            {
                return RedirectToAction("KhoaHocIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("KhoaHocIndex");
            }

            try
            {
                var successful = await _khoaHocService.DeleteKhoaHocAsync(model.KhoaHocId, currentUser.Email);
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        [ServiceFilter(typeof(Read_QuanHe))]
        public async Task<IActionResult> QuanHeIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _quanHeService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetQuanHeAsync()
        {
            var model = await _quanHeService.GetQuanHeAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuanHeAsync([FromBody]Models.QuanHeViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("QuanHeIndex");
            }

            try
            {
                var successful = await _quanHeService.CreateQuanHeAsync(model.Name, currentUser.Email);
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

        [HttpPut]
        public async Task<IActionResult> UpdateQuanHeAsync([FromBody]Models.QuanHeViewModel model)
        {
            if (model.QuanHeId == Guid.Empty)
            {
                return RedirectToAction("QuanHeIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("QuanHeIndex");
            }

            try
            {
                var successful = await _quanHeService.UpdateQuanHeAsync(model.QuanHeId, model.Name, currentUser.Email);
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
                    Message = "Cập nhật thành công !!!"
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
        public async Task<IActionResult> DeleteQuanHeAsync([FromBody]Models.QuanHeViewModel model)
        {
            if (model.QuanHeId == Guid.Empty)
            {
                return RedirectToAction("QuanHeIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("QuanHeIndex");
            }
            
            try
            {
                var successful = await _quanHeService.DeleteQuanHeAsync(model.QuanHeId, currentUser.Email);
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        [ServiceFilter(typeof(Read_NgayHoc))]
        public async Task<IActionResult> NgayHocIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _ngayHocService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetNgayHocAsync()
        {
            var model = await _ngayHocService.GetNgayHocAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNgayHocAsync([FromBody]Models.NgayHocViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("NgayHocIndex");
            }

            var successful = await _ngayHocService.CreateNgayHocAsync(model.Name, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Thêm mới lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Thêm mới thành công !!!", true, successful));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNgayHocAsync([FromBody]Models.NgayHocViewModel model)
        {
            if (model.NgayHocId == Guid.Empty)
            {
                return RedirectToAction("NgayHocIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("NgayHocIndex");
            }

            var successful = await _ngayHocService.UpdateNgayHocAsync(model, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Cập nhật thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Cập nhật lỗi !!!", false));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNgayHocAsync([FromBody]Models.NgayHocViewModel model)
        {
            if (model.NgayHocId == Guid.Empty)
            {
                return RedirectToAction("NgayHocIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("NgayHocIndex");
            }

            var successful = await _ngayHocService.DeleteNgayHocAsync(model.NgayHocId, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Xóa thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Xóa lỗi !!!", false));
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        [ServiceFilter(typeof(Read_GioHoc))]
        public async Task<IActionResult> GioHocIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _gioHocService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetGioHocAsync()
        {
            var model = await _gioHocService.GetGioHocAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGioHocAsync([FromBody]Models.GioHocViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("GioHocIndex");
            }

            try
            {
                var successful = await _gioHocService.CreateGioHocAsync(model.From, model.To, currentUser.Email);
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

        [HttpPut]
        public async Task<IActionResult> UpdateGioHocAsync([FromBody]Models.GioHocViewModel model)
        {
            if (model.GioHocId == Guid.Empty)
            {
                return RedirectToAction("GioHocIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("GioHocIndex");
            }

            try
            {
                var successful = await _gioHocService.UpdateGioHocAsync(model.GioHocId, model.From, model.To, currentUser.Email);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteGioHocAsync([FromBody]Models.GioHocViewModel model)
        {
            if (model.GioHocId == Guid.Empty)
            {
                return RedirectToAction("GioHocIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("GioHocIndex");
            }

            try
            {
                var successful = await _gioHocService.DeleteGioHocAsync(model.GioHocId, currentUser.Email);
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        [ServiceFilter(typeof(Read_HocPhi))]
        public async Task<IActionResult> HocPhiIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _hocPhiService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetHocPhiAsync()
        {
            var model = await _hocPhiService.GetHocPhiAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHocPhiAsync([FromBody]Models.HocPhiViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("HocPhiIndex");
            }

            try
            {
                DateTime _ngayApDung = Convert.ToDateTime(model.NgayApDung, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _hocPhiService.CreateHocPhiAsync(model.Gia, model.GhiChu, _ngayApDung, currentUser.Email);
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

        [HttpPut]
        public async Task<IActionResult> UpdateHocPhiAsync([FromBody]Models.HocPhiViewModel model)
        {
            if (model.HocPhiId == Guid.Empty)
            {
                return RedirectToAction("HocPhiIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("HocPhiIndex");
            }

            try
            {
                DateTime _ngayApDung = Convert.ToDateTime(model.NgayApDung, System.Globalization.CultureInfo.InvariantCulture);
                var successful = await _hocPhiService.UpdateHocPhiAsync(model.HocPhiId, model.Gia, model.GhiChu, _ngayApDung, currentUser.Email);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteHocPhiAsync([FromBody]Models.HocPhiViewModel model)
        {
            if (model.HocPhiId == Guid.Empty)
            {
                return RedirectToAction("HocPhiIndex");
            }
            
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("HocPhiIndex");
            }

            try
            {
                var successful = await _hocPhiService.DeleteHocPhiAsync(model.HocPhiId, currentUser.Email);
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        [ServiceFilter(typeof(Read_TaiLieu))]
        public async Task<IActionResult> SachIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _sachService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetSachAsync()
        {
            var model = await _sachService.GetSachAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSachAsync([FromBody] CreateSachInputModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("SachIndex");
            }

            var successful = await _sachService.CreateSachAsync(model, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Thêm mới lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Thêm mới thành công !!!", true, successful));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSachAsync([FromBody] UpdateSachInputModel model)
        {
            if (model.SachId == Guid.Empty)
            {
                return RedirectToAction("SachIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("SachIndex");
            }

            var successful = await _sachService.UpdateSachAsync(model, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Cập nhật thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Cập nhật lỗi !!!", false));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSachAsync([FromBody]Models.SachViewModel model)
        {
            if (model.SachId == Guid.Empty)
            {
                return RedirectToAction("SachIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("SachIndex");
            }

            var successful = await _sachService.DeleteSachAsync(model.SachId, currentUser.Email);
            return successful ?
               Json(_converter.ToResultModel("Xóa thành công !!!", true, successful))
               :
               Json(_converter.ToResultModel("Xóa lỗi !!!", false));
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////
        [ServiceFilter(typeof(Read_ViTriCongViec))]
        public async Task<IActionResult> LoaiGiaoVienIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _loaiGiaoVienService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetLoaiGiaoVienAsync()
        {
            var model = await _loaiGiaoVienService.GetLoaiGiaoVienAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoaiGiaoVienAsync([FromBody]Models.LoaiGiaoVienViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("LoaiGiaoVienIndex");
            }

            try
            {
                var successful = await _loaiGiaoVienService.CreateLoaiGiaoVienAsync(model.Name, (byte)model.Order, currentUser.Email);
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

        [HttpPut]
        public async Task<IActionResult> UpdateLoaiGiaoVienAsync([FromBody]Models.LoaiGiaoVienViewModel model)
        {
            if (model.LoaiGiaoVienId == Guid.Empty)
            {
                return RedirectToAction("LoaiGiaoVienIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("LoaiGiaoVienIndex");
            }

            try
            {
                var successful = await _loaiGiaoVienService.UpdateLoaiGiaoVienAsync(model.LoaiGiaoVienId, model.Name, (byte)model.Order, currentUser.Email);
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
                    Message = "Cập nhật thành công !!!"
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
        public async Task<IActionResult> DeleteLoaiGiaoVienAsync([FromBody]Models.LoaiGiaoVienViewModel model)
        {
            if (model.LoaiGiaoVienId == Guid.Empty)
            {
                return RedirectToAction("LoaiGiaoVienIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("LoaiGiaoVienIndex");
            }

            try
            {
                var successful = await _loaiGiaoVienService.DeleteLoaiGiaoVienAsync(model.LoaiGiaoVienId, currentUser.Email);
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

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        [ServiceFilter(typeof(Read_NgayLamViec))]
        public async Task<IActionResult> NgayLamViecIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _ngayLamViecService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetNgayLamViecAsync()
        {
            var model = await _ngayLamViecService.GetNgayLamViecAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNgayLamViecAsync([FromBody]Models.NgayLamViecViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("NgayLamViecIndex");
            }

            try
            {
                var successful = await _ngayLamViecService.CreateNgayLamViecAsync(model.Name, currentUser.Email);
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

        [HttpPut]
        public async Task<IActionResult> UpdateNgayLamViecAsync([FromBody]Models.NgayLamViecViewModel model)
        {
            if (model.NgayLamViecId == Guid.Empty)
            {
                return RedirectToAction("NgayLamViecIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("NgayLamViecIndex");
            }

            try
            {
                var successful = await _ngayLamViecService.UpdateNgayLamViecAsync(model.NgayLamViecId, model.Name, currentUser.Email);
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
                    Message = "Cập nhật thành công !!!"
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
        public async Task<IActionResult> DeleteNgayLamViecAsync([FromBody]Models.NgayLamViecViewModel model)
        {
            if (model.NgayLamViecId == Guid.Empty)
            {
                return RedirectToAction("NgayLamViecIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("NgayLamViecIndex");
            }

            try
            {
                var successful = await _ngayLamViecService.DeleteNgayLamViecAsync(model.NgayLamViecId, currentUser.Email);
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

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        [ServiceFilter(typeof(Read_CheDoHopTac))]
        public async Task<IActionResult> LoaiCheDoIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _loaiCheDoService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetLoaiCheDoAsync()
        {
            var model = await _loaiCheDoService.GetLoaiCheDoAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoaiCheDoAsync([FromBody]Models.LoaiCheDoViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("LoaiCheDoIndex");
            }

            try
            {
                var successful = await _loaiCheDoService.CreateLoaiCheDoAsync(model.Name, currentUser.Email);
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

        [HttpPut]
        public async Task<IActionResult> UpdateLoaiCheDoAsync([FromBody]Models.LoaiCheDoViewModel model)
        {
            if (model.LoaiCheDoId == Guid.Empty)
            {
                return RedirectToAction("LoaiCheDoIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("LoaiCheDoIndex");
            }

            try
            {
                var successful = await _loaiCheDoService.UpdateLoaiCheDoAsync(model.LoaiCheDoId, model.Name, currentUser.Email);
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
                    Message = "Cập nhật thành công !!!"
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
        public async Task<IActionResult> DeleteLoaiCheDoAsync([FromBody]Models.LoaiCheDoViewModel model)
        {
            if (model.LoaiCheDoId == Guid.Empty)
            {
                return RedirectToAction("LoaiCheDoIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("LoaiCheDoIndex");
            }

            try
            {
                var successful = await _loaiCheDoService.DeleteLoaiCheDoAsync(model.LoaiCheDoId, currentUser.Email);
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

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////
        [ServiceFilter(typeof(Read_ChiPhiCoDinh))]
        public async Task<IActionResult> StaticExpenseIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _chiPhiCoDinhService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetStaticExpenseAsync()
        {
            var model = await _chiPhiCoDinhService.GetChiPhiCoDinhAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaticExpenseAsync([FromBody]Models.ChiPhiCoDinhViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("StaticExpenseIndex");
            }

            try
            {
                var successful = await _chiPhiCoDinhService.CreateChiPhiCoDinhAsync(model.Gia, model.Name, currentUser.Email);
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

        [HttpPut]
        public async Task<IActionResult> UpdateStaticExpenseAsync([FromBody]Models.ChiPhiCoDinhViewModel model)
        {
            if (model.ChiPhiCoDinhId == Guid.Empty)
            {
                return RedirectToAction("StaticExpenseIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("StaticExpenseIndex");
            }

            try
            {
                var successful = await _chiPhiCoDinhService.UpdateChiPhiCoDinhAsync(model.ChiPhiCoDinhId, model.Gia, model.Name, currentUser.Email);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteStaticExpenseAsync([FromBody]Models.ChiPhiCoDinhViewModel model)
        {
            if (model.ChiPhiCoDinhId == Guid.Empty)
            {
                return RedirectToAction("StaticExpenseIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("StaticExpenseIndex");
            }

            try
            {
                var successful = await _chiPhiCoDinhService.DeleteChiPhiCoDinhAsync(model.ChiPhiCoDinhId, currentUser.Email);
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

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////
        [ServiceFilter(typeof(Read_ChiPhiCoDinh))]
        public async Task<IActionResult> OtherExpenseIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _chiPhiKhacService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetOtherExpenseAsync()
        {
            var model = await _chiPhiKhacService.GetChiPhiKhacAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOtherExpenseAsync([FromBody]Models.ChiPhiKhacViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("OtherExpenseIndex");
            }

            try
            {
                var successful = await _chiPhiKhacService.CreateChiPhiKhacAsync(model.Name, model.Gia, model.NgayChiPhi, currentUser.Email);
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

        [HttpPut]
        public async Task<IActionResult> UpdateOtherExpenseAsync([FromBody]Models.ChiPhiKhacViewModel model)
        {
            if (model.ChiPhiKhacId == Guid.Empty)
            {
                return RedirectToAction("OtherExpenseIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("OtherExpenseIndex");
            }

            try
            {
                var successful = await _chiPhiKhacService.UpdateChiPhiKhacAsync(model.ChiPhiKhacId, model.Name, model.Gia, model.NgayChiPhi, currentUser.Email);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteOtherExpenseAsync([FromBody]Models.ChiPhiKhacViewModel model)
        {
            if (model.ChiPhiKhacId == Guid.Empty)
            {
                return RedirectToAction("OtherExpenseIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("OtherExpenseIndex");
            }

            try
            {
                var successful = await _chiPhiKhacService.DeleteChiPhiKhacAsync(model.ChiPhiKhacId, currentUser.Email);
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
    }
}