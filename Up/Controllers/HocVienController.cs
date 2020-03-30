﻿namespace Up.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Extensions;
    using Up.Models;
    using Up.Services;

    [Authorize]
    public class HocVienController : Controller
    {
        private readonly IHocVienService _hocVienService;
        private readonly INgayHocService _ngayHocService;
        private readonly IQuanHeService _quanHeService;
        private readonly UserManager<IdentityUser> _userManager;

        public HocVienController(IHocVienService hocVienService, INgayHocService ngayHocService, IQuanHeService quanHeService, UserManager<IdentityUser> userManager)
        {
            _hocVienService = hocVienService;
            _ngayHocService = ngayHocService;
            _quanHeService = quanHeService;
            _userManager = userManager;
        }

        [ServiceFilter(typeof(Read_HocVien))]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _hocVienService.CanContributeAsync(User);
            return View();
        }

        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> ExportIndex()
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
                DateTime? _ngaySinh = null;

                if(!string.IsNullOrWhiteSpace(model.NgaySinh) || model.NgaySinh != "")
                    _ngaySinh = Convert.ToDateTime(model.NgaySinh, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _hocVienService.CreateHocVienAsync(model.LopHoc_NgayHocList, model.FullName, model.Phone, model.OtherPhone, model.FacebookAccount, model.ParentFullName, model.ParentPhone,
                    model.QuanHeId, model.EnglishName, _ngaySinh, model.LopHocIds, currentUser.Email);
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
                DateTime? _ngaySinh = null;

                if (!string.IsNullOrWhiteSpace(model.NgaySinh) || model.NgaySinh != "")
                    _ngaySinh = Convert.ToDateTime(model.NgaySinh, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _hocVienService.UpdateHocVienAsync(model.LopHoc_NgayHocList, model.HocVienId, model.FullName, model.Phone, model.OtherPhone,
                   model.FacebookAccount, model.ParentFullName, model.ParentPhone, model.QuanHeId,
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
                int totalRows = hocVien.Where(x => !string.IsNullOrWhiteSpace(x.Phone)).ToList().Count;

                int phuHuynhRows = 0;
                int i = 0;
                foreach (var itemHocVien in hocVien)
                {
                    string lopHoc = "";
                    if (itemHocVien.IsDisabled || itemHocVien.LopHocList.Any(x => x.IsDisabled || x.IsGraduated || x.IsCanceled || x.HocVienNghi))
                    {
                        if (!itemHocVien.LopHocList.Any())
                        {
                            lopHoc = "BL";
                        }

                        foreach (var item in itemHocVien.LopHocList.Where(x => !x.IsDisabled && !x.IsGraduated && !x.IsCanceled && !x.HocVienNghi))
                        {
                            lopHoc += item.Name + " ";
                        }

                        foreach (var item in itemHocVien.LopHocList.Where(x => x.IsDisabled || x.IsGraduated || x.IsCanceled || x.HocVienNghi))
                        {
                            lopHoc += "BL-" + item.Name.Substring(2) + "-" + item.Name.Substring(0, 2) + " ";
                        }
                    }
                    else
                        lopHoc = String.Join(" ", itemHocVien.LopHocList.Select(x => x.Name).ToArray());

                    if (!string.IsNullOrWhiteSpace(itemHocVien.Phone))
                    {
                        worksheet.Cells[i + 2, 1].Value = itemHocVien.FullName;
                        worksheet.Cells[i + 2, 2].Value = itemHocVien.Phone;
                        worksheet.Cells[i + 2, 3].Value = itemHocVien.OtherPhone;
                        
                        worksheet.Cells[i + 2, 4].Value = lopHoc;
                        //worksheet.Cells[i + 2, 5].Value = hocVien[i].QuanHe + " " + hocVien[i].ParentFullName;
                        i++;
                        
                    }

                    if (!string.IsNullOrWhiteSpace(itemHocVien.ParentFullName) && !string.IsNullOrWhiteSpace(itemHocVien.ParentPhone))
                    {
                        phuHuynhRows++;

                        worksheet.Cells[i + 2, 1].Value = itemHocVien.FullName;
                        worksheet.Cells[i + 2, 2].Value = itemHocVien.ParentPhone;
                        worksheet.Cells[i + 2, 3].Value = "";
                        worksheet.Cells[i + 2, 4].Value = lopHoc;
                        worksheet.Cells[i + 2, 5].Value = itemHocVien.QuanHe + " " + itemHocVien.ParentFullName;

                        i++;
                    }
                }

                worksheet.Cells[1, 1].Value = "First Name";
                worksheet.Cells[1, 2].Value = "Mobile Phone";
                worksheet.Cells[1, 3].Value = "Other Phone";
                worksheet.Cells[1, 4].Value = "Middle Name";
                worksheet.Cells[1, 5].Value = "Last Name";

                worksheet.Cells["A1:E1"].Style.Font.Bold = true;
                worksheet.Cells["A1:E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A1:E1"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                var modelCells = worksheet.Cells["A1"];
                string modelRange = "A1:E" + (totalRows + 1 + phuHuynhRows);
                var modelTable = worksheet.Cells[modelRange];



                // Assign borders
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                worksheet.Cells.AutoFitColumns();

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }

        [HttpGet]
        public IActionResult ExportTemplate(Guid LopHocId)
        {
            var stream = GenerateTemplateExcelFile(LopHocId);
            string excelName = $"UserList.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        private MemoryStream GenerateTemplateExcelFile(Guid LopHocId)
        {
            var stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("MauImportHocVien");

                worksheet.Cells["A1:I1"].Merge = true;
                worksheet.Cells["A1:I1"].Value = "MẪU IMPORT HỌC VIÊN";
                worksheet.Cells["A1:I1"].Style.Font.Bold = true;
                worksheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[1, 12].Value = "Lớp Học";
                worksheet.Cells[1, 12].Style.Font.Bold = true;
                worksheet.Cells[1, 13].Value = LopHocId;
                worksheet.Cells["L1:M1"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["L1:M1"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["L1:M1"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["L1:M1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[5, 12].Value = "ID Quan Hệ";
                worksheet.Cells[5, 12].Style.Font.Bold = true;
                worksheet.Cells[5, 13].Value = "Quan Hệ";
                worksheet.Cells[5, 13].Style.Font.Bold = true;

                worksheet.Cells[2, 1].Value = "Họ và Tên";
                worksheet.Cells[2, 2].Value = "English Name";
                worksheet.Cells[2, 3].Value = "Số Điện Thoại";
                worksheet.Cells[2, 4].Value = "Số Điện Thoại Khác";
                worksheet.Cells[2, 5].Value = "Facebook";
                worksheet.Cells[2, 6].Value = "Ngày Sinh (yyyy-mm-dd)";
                worksheet.Cells[2, 7].Value = "Người Thân";
                worksheet.Cells[2, 8].Value = "SĐT Người Thân";
                worksheet.Cells[2, 9].Value = "ID Quan Hệ";
                worksheet.Cells[2, 10].Value = "Ngày Bắt Đầu (yyyy-mm-dd)";

                worksheet.Cells["A2:J2"].Style.Font.Bold = true;
                worksheet.Cells["A2:J2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A2:J2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                var modelCells = worksheet.Cells["A2"];
                string modelRange = "A2:J22";
                var modelTable = worksheet.Cells[modelRange];



                //// Assign borders
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                var quanHe = _quanHeService.GetQuanHeAsync().Result;
                int totalRowsQuanHe = quanHe.Count;

                var modelCellsQuanHe = worksheet.Cells["L5"];
                string modelRangeQuanHe = "L5:M" + (totalRowsQuanHe + 5);
                var modelTableQuanHe = worksheet.Cells[modelRangeQuanHe];

                // Assign borders
                modelTableQuanHe.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTableQuanHe.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTableQuanHe.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTableQuanHe.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                for (int i = 0; i < totalRowsQuanHe; i++)
                {
                    worksheet.Cells[i + 6, 12].Value = quanHe[i].QuanHeId;
                    worksheet.Cells[i + 6, 13].Value = quanHe[i].Name;
                }

                worksheet.Cells.AutoFitColumns();

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }

        [HttpPost]
        public async Task<IActionResult> Import([FromBody]FileUploadModel model)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return RedirectToAction("Index");
                }

                string extension = model.Name.Substring(model.Name.IndexOf('.'));
                if (extension != ".xlsx")
                    return Json(new ResultModel
                    {
                        Status = "Failed",
                        Message = "File import phải là excel .xlsx !!!"
                    });

                using (var stream = new MemoryStream(Convert.FromBase64String(model.File.Substring(model.File.IndexOf(',') + 1))))
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;
                        Guid lopHocId = new Guid(worksheet.Cells[1, 13].Value.ToString().Trim());
                        List<Guid> lopHocIds = new List<Guid>();
                        lopHocIds.Add(lopHocId);
                        List<HocVienViewModel> hocViens = new List<HocVienViewModel>();

                        for (int row = 3; row <= rowCount; row++)
                        {
                            if(worksheet.Cells[row, 1].Value != null)
                            {
                                DateTime? _ngaySinh = null;
                                if(worksheet.Cells[row, 6].Value != null)
                                    _ngaySinh = Convert.ToDateTime(worksheet.Cells[row, 6].Value.ToString().Trim() + " 00:00:00", System.Globalization.CultureInfo.InvariantCulture);

                                DateTime? _ngayBatDau = null;
                                if (worksheet.Cells[row, 10].Value != null)
                                    _ngayBatDau = Convert.ToDateTime(worksheet.Cells[row, 10].Value.ToString().Trim() + " 00:00:00", System.Globalization.CultureInfo.InvariantCulture);

                                Guid? quanHe = null;

                                var successful = await _hocVienService.CreateHocVienAsync(
                                    new List<LopHoc_NgayHocViewModel>(),
                                    worksheet.Cells[row, 1].Value.ToString().Trim(),
                                    worksheet.Cells[row, 3].Value == null ? "" : worksheet.Cells[row, 3].Value.ToString().Trim(),
                                    worksheet.Cells[row, 4].Value == null ? "" : worksheet.Cells[row, 4].Value.ToString().Trim(),
                                    worksheet.Cells[row, 5].Value == null ? "" : worksheet.Cells[row, 5].Value.ToString().Trim(),
                                    worksheet.Cells[row, 7].Value == null ? "" : worksheet.Cells[row, 7].Value.ToString().Trim(),
                                    worksheet.Cells[row, 8].Value == null ? "" : worksheet.Cells[row, 8].Value.ToString().Trim(),
                                    worksheet.Cells[row, 9].Value == null ? quanHe : new Guid(worksheet.Cells[row, 9].Value.ToString().Trim()),
                                    worksheet.Cells[row, 2].Value == null ? "" : worksheet.Cells[row, 2].Value.ToString().Trim(),
                                    _ngaySinh,
                                    lopHocIds.ToArray(),
                                    currentUser.Email,
                                    _ngayBatDau
                                    );

                                if (successful != null)
                                    hocViens.Add(successful);
                            }
                        }
                        return Json(new ResultModel
                        {
                            Status = "OK",
                            Message = "Import thành công các học viên " + String.Join(", ", hocViens.Select(x => x.FullName).ToArray()),
                            Result = hocViens
                        });
                    }
                }
            }
            catch (Exception exception)
            {
                return Json(new ResultModel
                {
                    Status = "Failed",
                    Message = exception.Message
                });
            }
        }
    }
}