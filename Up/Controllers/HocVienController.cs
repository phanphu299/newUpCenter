namespace Up.Controllers
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
        private readonly Converters.Converter _converter;

        public HocVienController(
            IHocVienService hocVienService, 
            INgayHocService ngayHocService, 
            IQuanHeService quanHeService,
            UserManager<IdentityUser> userManager, 
            Converters.Converter converter)
        {
            _hocVienService = hocVienService;
            _ngayHocService = ngayHocService;
            _quanHeService = quanHeService;
            _userManager = userManager;
            _converter = converter;
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
        public async Task<IActionResult> GetHocVienByNameAsync(string name)
        {
            var model = await _hocVienService.GetHocVienByNameAsync(name);
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetHocVien_LopHocByHocVienAsync(Guid hocVienId, Guid lopHocId)
        {
            var model = await _ngayHocService.GetHocVien_NgayHocByHocVienAsync(hocVienId, lopHocId);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHocVienAsync([FromBody] CreateHocVienInputModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            if (!string.IsNullOrWhiteSpace(model.NgaySinh) || model.NgaySinh != "")
                model.NgaySinhDate = _converter.ToDateTime(model.NgaySinh);

            var successful = await _hocVienService.CreateHocVienAsync(model, currentUser.Email);

            return successful == null ? 
                Json(_converter.ToResultModel("Thêm mới lỗi !!!", false)) 
                : 
                Json(_converter.ToResultModel("Thêm mới thành công !!!", true, successful));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateHocVien_ngayHocAsync([FromBody] HocVien_NgayHocInputModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            model.NgayBatDauDate = _converter.ToDateTime(model.NgayBatDau);

            if (!string.IsNullOrWhiteSpace(model.NgayKetThuc))
                model.NgayKetThucDate = _converter.ToDateTime(model.NgayKetThuc);

            var successful = await _ngayHocService.CreateUpdateHocVien_NgayHocAsync(model, currentUser.Email);

            return successful ?
                Json(_converter.ToResultModel("Thêm mới thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Thêm mới lỗi !!!", false));
        }

        [HttpPost]
        public async Task<IActionResult> AddHocVienToLopCuAsync([FromBody] HocVien_LopViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            var successful = await _hocVienService.AddToUnavailableClassAsync(model.LopHocId, model.HocVienId, currentUser.Email);

            return successful ?
                Json(_converter.ToResultModel("Thêm mới thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Thêm mới lỗi !!!", false));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteHocVienAsync([FromBody] UpdateHocVienInputModel model)
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

            var successful = await _hocVienService.DeleteHocVienAsync(model.HocVienId, currentUser.Email);

            return successful ? 
                Json(_converter.ToResultModel("Xóa thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Xóa lỗi !!!", false));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHocVienAsync([FromBody] UpdateHocVienInputModel model)
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

            if (!string.IsNullOrWhiteSpace(model.NgaySinh) || model.NgaySinh != "")
                model.NgaySinhDate = _converter.ToDateTime(model.NgaySinh);

            var successful = await _hocVienService.UpdateHocVienAsync(model, currentUser.Email);

            return successful == null ?
                Json(_converter.ToResultModel("Cập nhật lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Cập nhật thành công !!!", true, successful));
        }

        [HttpGet]
        public IActionResult Export()
        {
            var stream = GenerateExcelFile();
            string excelName = $"UserList.xlsx";
            return File(stream, Constants.ContentType, excelName);
        }

        private MemoryStream GenerateExcelFile()
        {
            var stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var hocVien = _hocVienService.GetAllHocVienAsync().Result;
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Hoc Vien");
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
                            lopHoc += $"{item.Name} ";
                        }

                        foreach (var item in itemHocVien.LopHocList.Where(x => x.IsDisabled || x.IsGraduated || x.IsCanceled || x.HocVienNghi))
                        {
                            lopHoc += $"BL-{item.Name.Substring(2)}-{item.Name.Substring(0, 2)} ";
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
                        worksheet.Cells[i + 2, 6].Value = itemHocVien.FacebookAccount;

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
                        worksheet.Cells[i + 2, 6].Value = itemHocVien.FacebookAccount;

                        i++;
                    }
                }

                worksheet.Cells[1, 1].Value = "First Name";
                worksheet.Cells[1, 2].Value = "Mobile Phone";
                worksheet.Cells[1, 3].Value = "Other Phone";
                worksheet.Cells[1, 4].Value = "Middle Name";
                worksheet.Cells[1, 5].Value = "Last Name";
                worksheet.Cells[1, 6].Value = "E-mail Address";

                worksheet.Cells["A1:F1"].Style.Font.Bold = true;
                worksheet.Cells["A1:F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                var modelCells = worksheet.Cells["A1"];
                string modelRange = "A1:F" + (totalRows + 1 + phuHuynhRows);
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
            return File(stream, Constants.ContentType, excelName);
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
                worksheet.Cells[2, 5].Value = "Email";
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
        public async Task<IActionResult> Import([FromBody] FileUploadModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            string extension = model.Name.Substring(model.Name.IndexOf('.'));
            if (extension != ".xlsx")
                return Json(_converter.ToResultModel("File import phải là excel .xlsx !!!", false));

            using (var stream = new MemoryStream(Convert.FromBase64String(model.File.Substring(model.File.IndexOf(',') + 1))))
            {
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    Guid lopHocId = new Guid(worksheet.Cells[1, 13].Value.ToString().Trim());
                    List<HocVienViewModel> hocViens = new List<HocVienViewModel>();

                    for (int row = 3; row <= rowCount; row++)
                    {
                        if (worksheet.Cells[row, 1].Value != null)
                        {
                            var input = _converter.ToImportHocVien(worksheet, row);
                            if (worksheet.Cells[row, 6].Value != null)
                                input.NgaySinhDate = _converter.ToDateTime(worksheet.Cells[row, 6].Value.ToString().Trim() + " 00:00:00");

                            if (worksheet.Cells[row, 10].Value != null)
                                input.NgayBatDau = _converter.ToDateTime(worksheet.Cells[row, 10].Value.ToString().Trim() + " 00:00:00");
                            input.LopHocId = lopHocId;

                            var successful = await _hocVienService.ImportHocVienAsync(input, currentUser.Email);

                            if (successful != null)
                                hocViens.Add(successful);
                        }
                    }
                    return Json(_converter.ToResultModel("Import thành công các học viên " + String.Join(", ", hocViens.Select(x => x.FullName).ToArray()), true, hocViens));
                }
            }
        }
    }
}