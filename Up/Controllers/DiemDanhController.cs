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
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Extensions;
    using Up.Models;
    using Up.Services;

    [Authorize]
    public class DiemDanhController : Controller
    {
        private readonly IDiemDanhService _diemDanhService;
        private readonly ILopHocService _lopHocService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Converters.Converter _converter;

        public DiemDanhController(
            IDiemDanhService diemDanhService, 
            ILopHocService lopHocService, 
            UserManager<IdentityUser> userManager,
            Converters.Converter converter)
        {
            _diemDanhService = diemDanhService;
            _lopHocService = lopHocService;
            _userManager = userManager;
            _converter = converter;
        }

        [ServiceFilter(typeof(Read_DiemDanh))]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _diemDanhService.CanContributeAsync(User);
            return View();
        }

        [ServiceFilter(typeof(Read_DiemDanh_Export))]
        public async Task<IActionResult> ExportIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _diemDanhService.CanContributeExportAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetHocVienByLopHocAsync(Guid LopHocId)
        {
            var model = await _diemDanhService.GetHocVienByLopHoc(LopHocId);
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetDiemDanhByHocVienAndLopHocAsync(Guid HocVienId, Guid LopHocId, int month, int year)
        {
            var model = await _diemDanhService.GetDiemDanhByHocVienAndLopHoc(HocVienId, LopHocId);
            return Json(model.Where(x => x.NgayDiemDanh_Date.Month == month && x.NgayDiemDanh_Date.Year == year));
        }

        [HttpGet]
        public async Task<IActionResult> GetDiemDanhByLopHocAsync(Guid LopHocId, int month, int year)
        {
            var list = await _diemDanhService.GetDiemDanhByLopHoc(LopHocId, month, year);
            var soNgayHoc = await _diemDanhService.SoNgayHocAsync(LopHocId, month, year);

            var model = list
                    .GroupBy(x => x.HocVien).Select(x => new ThongKeModel
                    {
                        Label = x.Key,
                        HocVienId = x.Select(m => m.HocVienId).First(),
                        NgayBatDau_Day = x.Select(m => m.NgayBatDau).First().Day,
                        NgayBatDau_Month = x.Select(m => m.NgayBatDau).First().Month,
                        NgayBatDau_Year = x.Select(m => m.NgayBatDau).First().Year,
                        NgayKetThuc_Day = x.Select(m => m.NgayKetThuc).FirstOrDefault() != null ? x.Select(m => m.NgayKetThuc).FirstOrDefault().Value.Day : 0,
                        NgayKetThuc_Month = x.Select(m => m.NgayKetThuc).FirstOrDefault() != null ? x.Select(m => m.NgayKetThuc).FirstOrDefault().Value.Month : 0,
                        NgayKetThuc_Year = x.Select(m => m.NgayKetThuc).FirstOrDefault() != null ? x.Select(m => m.NgayKetThuc).FirstOrDefault().Value.Year : 0,
                        ThongKeDiemDanh = x.Where(m => m.NgayDiemDanh_Date.Month == month && m.NgayDiemDanh_Date.Year == year)
                        .Select(m => new ThongKeDiemDanhModel
                        {
                            Dates = m.NgayDiemDanh_Date,
                            DuocNghi = m.IsDuocNghi,
                            IsOff = m.IsOff,
                            Day = m.NgayDiemDanh_Date.Day
                        }).ToList()
                    })
                    .OrderBy(x => x.Label)
                    .ToList();

            foreach (var hocVien in model)
            {
                List<ThongKeDiemDanhModel> diemDanhModel = new List<ThongKeDiemDanhModel>();
                foreach (int ngayHoc in soNgayHoc)
                {
                    diemDanhModel.Add(new ThongKeDiemDanhModel
                    {
                        //phai~ dao~ isOff de ko sinh loi v-switch
                        DuocNghi = false,
                        IsOff = false,
                        Day = ngayHoc,
                        Dates = new DateTime(year, month, ngayHoc)
                    });
                }

                foreach (var diemDanh in hocVien.ThongKeDiemDanh)
                {
                    foreach (var item in diemDanhModel)
                    {
                        if (diemDanh.Day == item.Day)
                        {
                            //phai~ dao~ isOff de ko sinh loi v-switch
                            item.Day = diemDanh.Day;
                            item.Dates = diemDanh.Dates;
                            item.IsOff = !diemDanh.IsOff;
                            item.DuocNghi = diemDanh.DuocNghi;
                        }
                    }
                }

                hocVien.ThongKeDiemDanh = diemDanhModel;
            }
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> DiemDanhTungHocVienNewAsync(DiemDanhHocVienInput input)
        {
            if (input.LopHocId == Guid.Empty || input.HocVienId == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            input.NgayDiemDanh = new DateTime(input.Year, input.Month, input.Day);
            input.IsOff = !input.IsOff;

            var successful = await _diemDanhService.DiemDanhTungHocVienAsync(input, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Điểm Danh thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Điểm Danh lỗi !!!", false));
        }

        [HttpPost]
        public async Task<IActionResult> DiemDanhTungHocVienAsync([FromBody] LopHoc_DiemDanhViewModel model)
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

            DateTime _ngayDiemDanh = _converter.ToDateTime(model.NgayDiemDanh);
            var input = _converter.ToDiemDanhHocVienInput(model.LopHocId, model.HocVienId, model.IsOff, _ngayDiemDanh);
            var successful = await _diemDanhService.DiemDanhTungHocVienAsync(input, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Điểm Danh thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Điểm Danh lỗi !!!", false));
        }

        [HttpPost]
        public async Task<IActionResult> DiemDanhTatCaAsync([FromBody] LopHoc_DiemDanhViewModel model)
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

            DateTime _ngayDiemDanh = _converter.ToDateTime(model.NgayDiemDanh);
            var input = _converter.ToDiemDanhHocVienInput(model.LopHocId, model.HocVienId, model.IsOff, _ngayDiemDanh);
            var successful = await _diemDanhService.DiemDanhTatCaAsync(input, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Điểm Danh thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Điểm Danh lỗi !!!", false));
        }

        [HttpPost]
        public async Task<IActionResult> LopNghiAsync([FromBody] LopHoc_DiemDanhViewModel model)
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

            DateTime _ngayDiemDanh = _converter.ToDateTime(model.NgayDiemDanh);
            var input = _converter.ToDiemDanhHocVienInput(model.LopHocId, model.HocVienId, model.IsOff, _ngayDiemDanh);
            var successful = await _diemDanhService.DuocNghiAsync(input, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Điểm Danh thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Điểm Danh lỗi !!!", false));
        }

        [HttpPost]
        public async Task<IActionResult> SaveHocVienOffAsync([FromBody] LopHoc_DiemDanhViewModel model)
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

            List<DateTime> ngayDiemDanhs = new List<DateTime>();
            foreach (string item in model.NgayDiemDanhs)
            {
                ngayDiemDanhs.Add(_converter.ToDateTime(item));
            }

            var successful = await _diemDanhService.SaveHocVienOff(model.LopHocId, model.HocVienIds, ngayDiemDanhs, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Điểm Danh thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Điểm Danh lỗi !!!", false));
        }

        [HttpPost]
        public async Task<IActionResult> SaveHocVienHoanTacAsync([FromBody] LopHoc_DiemDanhViewModel model)
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

            List<DateTime> ngayDiemDanhs = new List<DateTime>();
            foreach (string item in model.NgayDiemDanhs)
            {
                ngayDiemDanhs.Add(_converter.ToDateTime(item));
            }

            var successful = await _diemDanhService.SaveHocVienHoanTac(model.LopHocId, model.HocVienIds, ngayDiemDanhs);
            return successful ?
                Json(_converter.ToResultModel("Hoàn tác thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Hoàn tác lỗi !!!", false));
        }

        [HttpPost]
        public async Task<IActionResult> UndoLopNghiAsync([FromBody] LopHoc_DiemDanhViewModel model)
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

            DateTime _ngayDiemDanh = _converter.ToDateTime(model.NgayDiemDanh);
            var input = _converter.ToDiemDanhHocVienInput(model.LopHocId, model.HocVienId, model.IsOff, _ngayDiemDanh);
            var successful = await _diemDanhService.UndoDuocNghi(input, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Undo thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Undo lỗi !!!", false));
        }

        [HttpGet]
        public async Task<IActionResult> ExportDiemDanh(Guid LopHocId, int month, int year)
        {
            var model = await _diemDanhService.GetDiemDanhByLopHoc(LopHocId, month, year);
            var stream = GenerateExcelFile(model.ToList(), month, year, LopHocId);
            string excelName = $"UserList.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [HttpGet]
        public async Task<IActionResult> GetSoNgayHoc(Guid LopHocId, int month, int year)
        {
            var model = await _diemDanhService.SoNgayHocAsync(LopHocId, month, year);
            return Json(model);
        }

        private String Number2String(int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c.ToString();
        }

        private System.IO.MemoryStream GenerateExcelFile(List<DiemDanhViewModel> model, int month, int year, Guid LopHocId)
        {
            var stream = new System.IO.MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                string lopHocName = _lopHocService.GetLopHocDetailAsync(LopHocId).Result.Name;
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Diem Danh " + lopHocName);
                var groupedModel = model.GroupBy(x => x.HocVien).Select(x => new ThongKeModel
                {
                    Label = x.Key,
                    ThongKeDiemDanh = x.Where(n => n.NgayDiemDanh_Date.Month == month && n.NgayDiemDanh_Date.Year == year).Select(m => new ThongKeDiemDanhModel
                    {
                        Dates = m.NgayDiemDanh_Date,
                        DuocNghi = m.IsDuocNghi,
                        IsOff = m.IsOff,
                    }).ToList()
                }).ToList();
                int totalRows = groupedModel.Count;

                var soNgayHoc = _diemDanhService.SoNgayHocAsync(LopHocId, month, year).Result;
                string column = Number2String(soNgayHoc.Count + 3, true);

                worksheet.Cells["A1:" + column + "1"].Merge = true;
                worksheet.Cells["A1:" + column + "1"].Value = "DANH SÁCH ĐIỂM DANH " + lopHocName;
                worksheet.Cells["A1:" + column + "1"].Style.Font.Bold = true;
                worksheet.Cells["A1:" + column + "1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells["A2:" + column + "2"].Merge = true;
                worksheet.Cells["A2:" + column + "2"].Value = "T" + month + "/" + year;
                worksheet.Cells["A2:" + column + "2"].Style.Font.Bold = true;
                worksheet.Cells["A2:" + column + "2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A2:" + column + "2"].Style.Font.Color.SetColor(Color.Red);

                worksheet.Cells[3, 1].Value = "No";
                worksheet.Cells[3, 2].Value = "Tên";


                worksheet.Cells[3, soNgayHoc.Count + 3].Value = "Ghi Chú";

                worksheet.Cells["A3:" + column + "3"].Style.Font.Bold = true;
                worksheet.Cells["A3:" + column + "3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A3:" + column + "3"].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                var modelCells = worksheet.Cells["A3"];
                string modelRange = "A3:" + column + (totalRows + 3);
                var modelTable = worksheet.Cells[modelRange];



                // Assign borders
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                for (int i = 0; i < totalRows; i++)
                {
                    worksheet.Cells[i + 4, 1].Value = i + 1;
                    worksheet.Cells[i + 4, 2].Value = groupedModel[i].Label;
                    for (int j = 0; j < soNgayHoc.Count; j++)
                    {
                        for (int z = 0; z < groupedModel[i].ThongKeDiemDanh.Count; z++)
                        {
                            var ngay = groupedModel[i].ThongKeDiemDanh[z].Dates.Day;
                            var off = groupedModel[i].ThongKeDiemDanh[z].IsOff;
                            var duocNghi = groupedModel[i].ThongKeDiemDanh[z].DuocNghi;

                            if (ngay == soNgayHoc[j] && off == false)
                                worksheet.Cells[i + 4, j + 3].Value = "x";
                            else if (ngay == soNgayHoc[j] && off == true && duocNghi == true)
                            {
                                worksheet.Cells[i + 4, j + 3].Value = "";
                                worksheet.Cells[i + 4, j + 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[i + 4, j + 3].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                            }
                            else if (ngay == soNgayHoc[j] && off == true && duocNghi == null)
                                worksheet.Cells[i + 4, j + 3].Value = "";

                            worksheet.Cells[i + 4, j + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }
                    }
                }
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                worksheet.Cells.AutoFitColumns();

                worksheet.Column(1).Width = 3;
                for (int i = 0; i < soNgayHoc.Count; i++)
                {
                    worksheet.Cells[3, i + 3].Value = soNgayHoc[i];
                    worksheet.Cells[3, i + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Column(i + 3).Width = 3;
                }
                worksheet.Column(soNgayHoc.Count + 3).Width = 40;

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }
    }
}