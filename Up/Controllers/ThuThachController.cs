using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Up.Converters;
using Up.Extensions;
using Up.Models;
using Up.Services;

namespace Up.Controllers
{
    [Authorize]
    public class ThuThachController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Converter _converter;
        private readonly IThuThachService _thuThachService;
        private readonly ICauHoiService _cauHoiService;

        public ThuThachController(UserManager<IdentityUser> userManager, Converter converter, IThuThachService thuThachService, ICauHoiService cauHoiService)
        {
            _userManager = userManager;
            _converter = converter;
            _thuThachService = thuThachService;
            _cauHoiService = cauHoiService;
        }

        [ServiceFilter(typeof(Read_ThuThach))]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _thuThachService.CanContributeAsync(User);
            return View();
        }

        [ServiceFilter(typeof(Read_ThuThach_Export))]
        public async Task<IActionResult> ExportIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _thuThachService.CanContributeExportAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetThuThachAsync()
        {
            var model = await _thuThachService.GetThuThachAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateThuThachAsync([FromBody] CreateThuThachInputModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }


            var successful = await _thuThachService.CreateThuThachAsync(model, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Thêm mới lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Thêm mới thành công !!!", true, successful));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteThuThachAsync([FromBody] ThuThachViewModel model)
        {
            if (model.ThuThachId == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            var successful = await _thuThachService.DeleteThuThachAsync(model.ThuThachId, currentUser.Email);
            return successful ?
               Json(_converter.ToResultModel("Xóa thành công !!!", true, successful))
               :
               Json(_converter.ToResultModel("Xóa lỗi !!!", false));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateThuThachAsync([FromBody] UpdateThuThachInputModel model)
        {
            if (model.ThuThachId == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            var successful = await _thuThachService.UpdateThuThachAsync(model, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Cập nhật lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Cập nhật thành công !!!", true, successful));
        }

        [HttpGet]
        public async Task<IActionResult> ExportThuThach(Guid thuThachId)
        {
            var model = await _cauHoiService.GetCauHoiAsync(thuThachId);
            var stream = GenerateExcelFile(model);
            string excelName = $"UserList.xlsx";
            return File(stream, Constants.ContentType, excelName);
        }

        private System.IO.MemoryStream GenerateExcelFile(List<CauHoiViewModel> model)
        {
            var stream = new System.IO.MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                string thuThachName = model.FirstOrDefault()?.TenThuThach ?? string.Empty;
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add($"Thu Thach {thuThachName}");
                int totalRows = model.Count;

                worksheet.Cells["A1:I1"].Merge = true;
                worksheet.Cells["A1:I1"].Value = $"CÂU HỎI THỬ THÁCH - {thuThachName}";
                worksheet.Cells["A1:I1"].Style.Font.Bold = true;
                worksheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[3, 1].Value = "#";
                worksheet.Cells[3, 2].Value = "Tên";


                worksheet.Cells[3, 3].Value = "Đáp Án Đúng";
                worksheet.Cells[3, 4].Value = "Đáp Án A";
                worksheet.Cells[3, 5].Value = "Đáp Án B";
                worksheet.Cells[3, 6].Value = "Đáp Án C";
                worksheet.Cells[3, 7].Value = "Đáp Án D";
                worksheet.Cells[3, 8].Value = "Đáp Án E";
                worksheet.Cells[3, 9].Value = "Đáp Án F";

                worksheet.Cells["A3:I3"].Style.Font.Bold = true;
                worksheet.Cells["A3:I3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A3:I3"].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                var modelCells = worksheet.Cells["A3"];
                string modelRange = "A3:I" + (totalRows + 3);
                var modelTable = worksheet.Cells[modelRange];

                // Assign borders
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                for (int i = 0; i < totalRows; i++)
                {
                    worksheet.Cells[i + 4, 1].Value = model[i].STT;
                    worksheet.Cells[i + 4, 2].Value = model[i].Name;
                    worksheet.Cells[i + 4, 3].Value = model[i].DapAns.FirstOrDefault(x => x.IsTrue).Name;
                    int index = 4;
                    foreach(var item in model[i].DapAns)
                    {
                        worksheet.Cells[i + 4, index].Value = item.Name;
                        index++;
                    }
                }
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                worksheet.Cells.AutoFitColumns();

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }
    }
}
