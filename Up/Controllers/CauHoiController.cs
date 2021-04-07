using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Up.Converters;
using Up.Models;
using Up.Services;

namespace Up.Controllers
{
    [Authorize]
    public class CauHoiController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Converter _converter;
        private readonly ICauHoiService _cauHoiService;

        public CauHoiController(UserManager<IdentityUser> userManager, Converter converter, ICauHoiService cauHoiService)
        {
            _userManager = userManager;
            _converter = converter;
            _cauHoiService = cauHoiService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCauHoiAsync()
        {
            var model = await _cauHoiService.GetCauHoiAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCauHoiAsync([FromBody] CreateCauHoiInputModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }


            var successful = await _cauHoiService.CreateCauHoiAsync(model, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Thêm mới lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Thêm mới thành công !!!", true, successful));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCauHoiAsync([FromBody] CauHoiViewModel model)
        {
            if (model.CauHoiId == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            var successful = await _cauHoiService.DeleteCauHoiAsync(model.CauHoiId, currentUser.Email);
            return successful ?
               Json(_converter.ToResultModel("Xóa thành công !!!", true, successful))
               :
               Json(_converter.ToResultModel("Xóa lỗi !!!", false));
        }

        [HttpGet]
        public IActionResult ExportTemplate(Guid thuThachId, string thuThachName, int soCauHoi)
        {
            var stream = GenerateTemplateExcelFile(thuThachId, thuThachName, soCauHoi);
            string excelName = $"UserList.xlsx";
            return File(stream, Constants.ContentType, excelName);
        }

        private MemoryStream GenerateTemplateExcelFile(Guid thuThachId, string thuThachName, int soCauHoi)
        {
            var stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("MauImportHocVien");

                worksheet.Cells["A1:G1"].Merge = true;
                worksheet.Cells["A1:G1"].Value = "MẪU IMPORT CÂU HỎI";
                worksheet.Cells["A1:G1"].Style.Font.Bold = true;
                worksheet.Cells["A1:G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[1, 9].Value = "ID";
                worksheet.Cells[1, 9].Style.Font.Bold = true;
                worksheet.Cells[1, 10].Value = thuThachId;
                worksheet.Cells[2, 9].Value = "Thử Thách";
                worksheet.Cells[2, 9].Style.Font.Bold = true;
                worksheet.Cells[2, 10].Value = thuThachName;
                worksheet.Cells[3, 9].Value = "Số Câu Hỏi";
                worksheet.Cells[3, 9].Style.Font.Bold = true;
                worksheet.Cells[3, 10].Value = soCauHoi;
                worksheet.Cells[4, 10].Value = "** cột Câu Hỏi Số không được vượt quá Số Câu Hỏi";
                worksheet.Cells[4, 10].Style.Font.Bold = true;

                worksheet.Cells["I1:J2"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["I1:J2"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["I1:J2"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["I1:J2"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                worksheet.Cells[2, 1].Value = "Câu Hỏi Số";
                worksheet.Cells[2, 2].Value = "Nội Dung Câu Hỏi";
                worksheet.Cells[2, 3].Value = "Đáp án a";
                worksheet.Cells[2, 4].Value = "Đáp án b";
                worksheet.Cells[2, 5].Value = "Đáp án c";
                worksheet.Cells[2, 6].Value = "Đáp án d";
                worksheet.Cells[2, 7].Value = "Đáp án đúng";

                worksheet.Cells["A2:G2"].Style.Font.Bold = true;
                worksheet.Cells["A2:G2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A2:G2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                var modelCells = worksheet.Cells["A2"];
                string modelRange = "A2:G50";
                var modelTable = worksheet.Cells[modelRange];



                //// Assign borders
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

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

                    var input = new ImportCauHoiInputModel
                    {
                        ThuThachId = new Guid(worksheet.Cells[1, 10].Value.ToString().Trim())
                    };

                    for (int row = 3; row <= rowCount; row++)
                    {
                        if (worksheet.Cells[row, 1].Value != null)
                        {
                            var cauHoi = _converter.ToImportCauHoi(worksheet, row);
                            input.CauHois.Add(cauHoi);
                        }
                    }

                    var successful = await _cauHoiService.ImportCauHoiAsync(input, currentUser.Email);
                    return successful == null ?
                        Json(_converter.ToResultModel("Import lỗi !!!", false))
                        :
                        Json(_converter.ToResultModel("Import thành công !!!", true, successful));
                }
            }
        }
    }
}
