namespace Up.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OfficeOpenXml.Style;
    using System;
    using System.Drawing;
    using System.Linq;
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

        [HttpGet]
        public IActionResult Export()
        {
            var stream = GenerateExcelFile();
            string excelName = $"UserList.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        private System.IO.MemoryStream GenerateExcelFile()
        {
            var stream = new System.IO.MemoryStream();
            using (OfficeOpenXml.ExcelPackage package = new OfficeOpenXml.ExcelPackage(stream))
            {
                var hocVien = _hocVienService.GetAllHocVienAsync().Result;
                OfficeOpenXml.ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Hoc Vien");
                int totalRows = hocVien.Count;

                worksheet.Cells["A1:D1"].Merge = true;
                worksheet.Cells["A1:D1"].Value = "DANH SÁCH HỌC VIÊN";
                worksheet.Cells["A1:D1"].Style.Font.Bold = true;
                worksheet.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[2, 1].Value = "Firsname";
                worksheet.Cells[2, 2].Value = "Mobile Phone";
                worksheet.Cells[2, 3].Value = "Middle name";
                worksheet.Cells[2, 4].Value = "Last name";
                
                worksheet.Cells["A2:D2"].Style.Font.Bold = true;
                worksheet.Cells["A2:D2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A2:D2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                var modelCells = worksheet.Cells["A2"];
                string modelRange = "A2:D" + (totalRows + 2);
                var modelTable = worksheet.Cells[modelRange];



                // Assign borders
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                for (int i = 0; i < totalRows; i++)
                {
                    worksheet.Cells[i + 3, 1].Value = hocVien[i].FullName;
                    worksheet.Cells[i + 3, 2].Value = string.IsNullOrWhiteSpace(hocVien[i].ParentPhone) ? hocVien[i].Phone : hocVien[i].ParentPhone;
                    string lopHoc = "";
                    if (hocVien[i].IsDisabled || hocVien[i].LopHocList.Any(x => x.IsDisabled || x.IsGraduated || x.IsCanceled))
                        lopHoc = "BL - " + String.Join(", ", hocVien[i].LopHocList.Select(x => x.Name).ToArray());
                    else
                        lopHoc = String.Join(", ", hocVien[i].LopHocList.Select(x => x.Name).ToArray());
                    worksheet.Cells[i + 3, 3].Value = lopHoc;
                    worksheet.Cells[i + 3, 4].Value = hocVien[i].QuanHe + " " + hocVien[i].ParentFullName;
                }

                worksheet.Cells.AutoFitColumns();

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }
    }
}