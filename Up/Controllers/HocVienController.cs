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
        public async Task<IActionResult> Export()
        {
            var stream = await GenerateExcelFileAsync();
            string excelName = $"UserList.xlsx";
            return File(stream, Constants.ContentType, excelName);
        }

        private async Task<MemoryStream> GenerateExcelFileAsync()
        {
            var stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var hocVien = await _hocVienService.GetAllHocVienAsync();
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
                        worksheet.Cells[i + 2, 1].Value = itemHocVien.Notes;
                        worksheet.Cells[i + 2, 2].Value = itemHocVien.DiaChi;
                        var challenges = itemHocVien.PassedChallenge.ToList();
                        if (itemHocVien.PassedChallenge != null && challenges.Count > 0)
                        {
                            string thuThach = string.Empty;
                            for (int j = 0; j < challenges.Count; j++)
                            {
                                if (j != challenges.Count - 1)
                                    thuThach += $"{challenges[j]}\r\n";
                                else
                                    thuThach += $"{challenges[j]}";
                            }

                            worksheet.Cells[i + 2, 3].Value = thuThach;
                            worksheet.Cells[i + 2, 3].Style.WrapText = true;
                        }

                        worksheet.Cells[i + 2, 4].Value = itemHocVien.FullName;
                        worksheet.Cells[i + 2, 5].Value = itemHocVien.Phone;
                        worksheet.Cells[i + 2, 6].Value = itemHocVien.OtherPhone;

                        worksheet.Cells[i + 2, 7].Value = lopHoc;
                        //worksheet.Cells[i + 2, 8].Value = hocVien[i].QuanHe + " " + hocVien[i].ParentFullName;
                        worksheet.Cells[i + 2, 9].Value = itemHocVien.FacebookAccount;
                        worksheet.Cells[i + 2, 10].Value = itemHocVien.NgaySinh;
                        worksheet.Cells[i + 2, 11].Value = itemHocVien.Trigram;

                        i++;

                    }

                    if (!string.IsNullOrWhiteSpace(itemHocVien.ParentFullName) && !string.IsNullOrWhiteSpace(itemHocVien.ParentPhone))
                    {
                        phuHuynhRows++;

                        worksheet.Cells[i + 2, 1].Value = itemHocVien.Notes;
                        worksheet.Cells[i + 2, 2].Value = itemHocVien.DiaChi;
                        var challenges = itemHocVien.PassedChallenge.ToList();
                        if (itemHocVien.PassedChallenge != null && challenges.Count > 0)
                        {
                            string thuThach = string.Empty;
                            for (int j = 0; j < challenges.Count; j++)
                            {
                                if (j != challenges.Count - 1)
                                    thuThach += $"{challenges[j]}\r\n";
                                else
                                    thuThach += $"{challenges[j]}";
                            }

                            worksheet.Cells[i + 2, 3].Value = thuThach;
                            worksheet.Cells[i + 2, 3].Style.WrapText = true;
                        }

                        worksheet.Cells[i + 2, 4].Value = itemHocVien.FullName;
                        worksheet.Cells[i + 2, 5].Value = itemHocVien.ParentPhone;
                        worksheet.Cells[i + 2, 6].Value = "";
                        worksheet.Cells[i + 2, 7].Value = lopHoc;
                        worksheet.Cells[i + 2, 8].Value = itemHocVien.QuanHe + " " + itemHocVien.ParentFullName;
                        worksheet.Cells[i + 2, 9].Value = itemHocVien.FacebookAccount;
                        worksheet.Cells[i + 2, 10].Value = itemHocVien.NgaySinh;
                        worksheet.Cells[i + 2, 11].Value = itemHocVien.Trigram;

                        i++;
                    }
                }

                worksheet.Cells[1, 1].Value = "Notes";
                worksheet.Cells[1, 2].Value = "Địa chỉ";
                worksheet.Cells[1, 3].Value = "Thử thách đã hoàn thành";
                worksheet.Cells[1, 4].Value = "First Name";
                worksheet.Cells[1, 5].Value = "Mobile Phone";
                worksheet.Cells[1, 6].Value = "Other Phone";
                worksheet.Cells[1, 7].Value = "Middle Name";
                worksheet.Cells[1, 8].Value = "Last Name";
                worksheet.Cells[1, 9].Value = "E-mail Address";
                worksheet.Cells[1, 10].Value = "Birthday";
                worksheet.Cells[1, 11].Value = "Trigram";

                worksheet.Cells["A1:K1"].Style.Font.Bold = true;
                worksheet.Cells["A1:K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A1:K1"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                var modelCells = worksheet.Cells["A1"];
                string modelRange = "A1:K" + (totalRows + 1 + phuHuynhRows);
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
        public async Task<IActionResult> ExportTemplate(Guid LopHocId)
        {
            var stream = await GenerateTemplateExcelFileAsync(LopHocId);
            string excelName = $"UserList.xlsx";
            return File(stream, Constants.ContentType, excelName);
        }

        private async Task<MemoryStream> GenerateTemplateExcelFileAsync(Guid LopHocId)
        {
            var stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("MauImportHocVien");

                worksheet.Cells["A1:I1"].Merge = true;
                worksheet.Cells["A1:I1"].Value = "MẪU IMPORT HỌC VIÊN";
                worksheet.Cells["A1:I1"].Style.Font.Bold = true;
                worksheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[1, 15].Value = "Lớp Học";
                worksheet.Cells[1, 15].Style.Font.Bold = true;
                worksheet.Cells[1, 16].Value = LopHocId;
                worksheet.Cells["O1:P1"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["O1:P1"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["O1:P1"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["O1:P1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[5, 15].Value = "ID Quan Hệ";
                worksheet.Cells[5, 15].Style.Font.Bold = true;
                worksheet.Cells[5, 16].Value = "Quan Hệ";
                worksheet.Cells[5, 16].Style.Font.Bold = true;

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
                worksheet.Cells[2, 11].Value = "CMND";
                worksheet.Cells[2, 12].Value = "Địa Chỉ";
                worksheet.Cells[2, 13].Value = "Notes";

                worksheet.Cells["A2:M2"].Style.Font.Bold = true;
                worksheet.Cells["A2:M2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A2:M2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                var modelCells = worksheet.Cells["A2"];
                string modelRange = "A2:M22";
                var modelTable = worksheet.Cells[modelRange];



                //// Assign borders
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                var quanHe = await _quanHeService.GetQuanHeAsync();
                int totalRowsQuanHe = quanHe.Count;

                var modelCellsQuanHe = worksheet.Cells["O5"];
                string modelRangeQuanHe = "O5:P" + (totalRowsQuanHe + 5);
                var modelTableQuanHe = worksheet.Cells[modelRangeQuanHe];

                // Assign borders
                modelTableQuanHe.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTableQuanHe.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTableQuanHe.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTableQuanHe.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                for (int i = 0; i < totalRowsQuanHe; i++)
                {
                    worksheet.Cells[i + 6, 15].Value = quanHe[i].QuanHeId;
                    worksheet.Cells[i + 6, 16].Value = quanHe[i].Name;
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
                    Guid lopHocId = new Guid(worksheet.Cells[1, 16].Value.ToString().Trim());
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