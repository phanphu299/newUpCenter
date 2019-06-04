namespace Up.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Up.Services;

    public class CategoryController : Controller
    {
        private readonly IKhoaHocService _khoaHocService;
        private readonly IGioHocService _gioHocService;
        private readonly INgayHocService _ngayHocService;
        private readonly IQuanHeService _quanHeService;
        private readonly IHocPhiService _hocPhiService;
        private readonly ISachService _sachService;
        private readonly ILoaiGiaoVienService _loaiGiaoVienService;
        private readonly UserManager<IdentityUser> _userManager;

        public CategoryController(IKhoaHocService khoaHocService, IGioHocService gioHocService, INgayHocService ngayHocService, IQuanHeService quanHeService, IHocPhiService hocPhiService, ISachService sachService, ILoaiGiaoVienService loaiGiaoVienService, UserManager<IdentityUser> userManager)
        {
            _khoaHocService = khoaHocService;
            _gioHocService = gioHocService;
            _ngayHocService = ngayHocService;
            _quanHeService = quanHeService;
            _hocPhiService = hocPhiService;
            _sachService = sachService;
            _loaiGiaoVienService = loaiGiaoVienService;
            _userManager = userManager;
        }

        public async Task<IActionResult> KhoaHocIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

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
        public async Task<IActionResult> QuanHeIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

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
        public async Task<IActionResult> NgayHocIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

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
            
            try
            {
                var successful = await _ngayHocService.CreateNgayHocAsync(model.Name, currentUser.Email);
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
            
            try
            {
                var successful = await _ngayHocService.UpdateNgayHocAsync(model.NgayHocId, model.Name, currentUser.Email);
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

            try
            {
                var successful = await _ngayHocService.DeleteNgayHocAsync(model.NgayHocId, currentUser.Email);
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
        public async Task<IActionResult> GioHocIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

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
        public async Task<IActionResult> HocPhiIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

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
        public async Task<IActionResult> SachIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetSachAsync()
        {
            var model = await _sachService.GetSachAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSachAsync([FromBody]Models.SachViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("SachIndex");
            }

            try
            {
                var successful = await _sachService.CreateSachAsync(model.Name, model.Gia, currentUser.Email);
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
        public async Task<IActionResult> UpdateSachAsync([FromBody]Models.SachViewModel model)
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

            try
            {
                var successful = await _sachService.UpdateSachAsync(model.SachId, model.Name, model.Gia, currentUser.Email);
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
            
            try
            {
                var successful = await _sachService.DeleteSachAsync(model.SachId, currentUser.Email);
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
        public async Task<IActionResult> LoaiGiaoVienIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

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
                var successful = await _loaiGiaoVienService.CreateLoaiGiaoVienAsync(model.Name, currentUser.Email);
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
                var successful = await _loaiGiaoVienService.UpdateLoaiGiaoVienAsync(model.LoaiGiaoVienId, model.Name, currentUser.Email);
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
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> StaticExpenseIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetQuanHeAsync()
        //{
        //    var model = await _quanHeService.GetQuanHeAsync();
        //    return Json(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateQuanHeAsync([FromBody]Models.QuanHeViewModel model)
        //{
        //    var currentUser = await _userManager.GetUserAsync(User);
        //    if (currentUser == null)
        //    {
        //        return RedirectToAction("QuanHeIndex");
        //    }

        //    try
        //    {
        //        var successful = await _quanHeService.CreateQuanHeAsync(model.Name, currentUser.Email);
        //        if (successful == null)
        //        {
        //            return Json(new Models.ResultModel
        //            {
        //                Status = "Failed",
        //                Message = "Thêm mới lỗi !!!"
        //            });
        //        }

        //        return Json(new Models.ResultModel
        //        {
        //            Status = "OK",
        //            Message = "Thêm mới thành công !!!",
        //            Result = successful
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        return Json(new Models.ResultModel
        //        {
        //            Status = "Failed",
        //            Message = exception.Message
        //        });
        //    }
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateQuanHeAsync([FromBody]Models.QuanHeViewModel model)
        //{
        //    if (model.QuanHeId == Guid.Empty)
        //    {
        //        return RedirectToAction("QuanHeIndex");
        //    }

        //    var currentUser = await _userManager.GetUserAsync(User);
        //    if (currentUser == null)
        //    {
        //        return RedirectToAction("QuanHeIndex");
        //    }

        //    try
        //    {
        //        var successful = await _quanHeService.UpdateQuanHeAsync(model.QuanHeId, model.Name, currentUser.Email);
        //        if (!successful)
        //        {
        //            return Json(new Models.ResultModel
        //            {
        //                Status = "Failed",
        //                Message = "Cập nhật lỗi !!!"
        //            });
        //        }

        //        return Json(new Models.ResultModel
        //        {
        //            Status = "OK",
        //            Message = "Cập nhật thành công !!!"
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        return Json(new Models.ResultModel
        //        {
        //            Status = "Failed",
        //            Message = exception.Message
        //        });
        //    }
        //}

        //[HttpDelete]
        //public async Task<IActionResult> DeleteQuanHeAsync([FromBody]Models.QuanHeViewModel model)
        //{
        //    if (model.QuanHeId == Guid.Empty)
        //    {
        //        return RedirectToAction("QuanHeIndex");
        //    }

        //    var currentUser = await _userManager.GetUserAsync(User);
        //    if (currentUser == null)
        //    {
        //        return RedirectToAction("QuanHeIndex");
        //    }

        //    try
        //    {
        //        var successful = await _quanHeService.DeleteQuanHeAsync(model.QuanHeId, currentUser.Email);
        //        if (!successful)
        //        {
        //            return Json(new Models.ResultModel
        //            {
        //                Status = "Failed",
        //                Message = "Xóa lỗi !!!"
        //            });
        //        }

        //        return Json(new Models.ResultModel
        //        {
        //            Status = "OK",
        //            Message = "Xóa thành công !!!"
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        return Json(new Models.ResultModel
        //        {
        //            Status = "Failed",
        //            Message = exception.Message
        //        });
        //    }
        //}
    }
}