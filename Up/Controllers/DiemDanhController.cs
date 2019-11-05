namespace Up.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Up.Services;
    using System.Threading.Tasks;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using OfficeOpenXml.Style;
    using System.Drawing;
    using Up.Models;
    using OfficeOpenXml;
    using Microsoft.AspNetCore.Authorization;
    using Up.Extensions;

    [Authorize]
    public class DiemDanhController : Controller
    {
        private readonly IDiemDanhService _diemDanhService;
        private readonly ILopHocService _lopHocService;
        private readonly IHocPhiService _hocPhiService;
        private readonly UserManager<IdentityUser> _userManager;

        public DiemDanhController(IDiemDanhService diemDanhService, ILopHocService lopHocService, IHocPhiService hocPhiService, UserManager<IdentityUser> userManager)
        {
            _diemDanhService = diemDanhService;
            _lopHocService = lopHocService;
            _hocPhiService = hocPhiService;
            _userManager = userManager;
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
            List<ThongKeModel> model = new List<ThongKeModel>();
            var soNgayHoc = await _hocPhiService.SoNgayHocAsync(LopHocId, month, year);
            //if (list.Where(x => x.NgayDiemDanh_Date.Month == month && x.NgayDiemDanh_Date.Year == year).Any())
            //{
                model = list
                        .Where(x => x.NgayDiemDanh_Date.Month == month && x.NgayDiemDanh_Date.Year == year)
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
                            ThongKeDiemDanh = x.Select(m => new ThongKeDiemDanhModel
                            {
                                Dates = m.NgayDiemDanh_Date,
                                DuocNghi = m.IsDuocNghi,
                                IsOff = m.IsOff,
                                Day = m.NgayDiemDanh_Date.Day
                            }).ToList()
                        }).ToList();

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
            //}
            //else if (list.Where(x => (x.NgayDiemDanh_Date.Month < month && x.NgayDiemDanh_Date.Year == year) || x.NgayDiemDanh_Date.Year < year).Any())
            //{
            //    model = list
            //            .GroupBy(x => x.HocVien).Select(x => new ThongKeModel
            //            {
            //                Label = x.Key,
            //                HocVienId = x.Select(m => m.HocVienId).First(),
            //                NgayBatDau_Day = x.Select(m => m.NgayBatDau).First().Day,
            //                NgayBatDau_Month = x.Select(m => m.NgayBatDau).First().Month,
            //                NgayBatDau_Year = x.Select(m => m.NgayBatDau).First().Year,
            //                NgayKetThuc_Day = x.Select(m => m.NgayKetThuc).FirstOrDefault() != null ? x.Select(m => m.NgayKetThuc).FirstOrDefault().Value.Day : 0,
            //                NgayKetThuc_Month = x.Select(m => m.NgayKetThuc).FirstOrDefault() != null ? x.Select(m => m.NgayKetThuc).FirstOrDefault().Value.Month : 0,
            //                NgayKetThuc_Year = x.Select(m => m.NgayKetThuc).FirstOrDefault() != null ? x.Select(m => m.NgayKetThuc).FirstOrDefault().Value.Year : 0,
            //                ThongKeDiemDanh = x.Select(m => new ThongKeDiemDanhModel
            //                {
            //                    Dates = m.NgayDiemDanh_Date,
            //                    DuocNghi = m.IsDuocNghi,
            //                    IsOff = m.IsOff,
            //                    Day = m.NgayDiemDanh_Date.Day
            //                }).ToList()
            //            }).ToList();

            //    foreach (var hocVien in model)
            //    {
            //        List<ThongKeDiemDanhModel> diemDanhModel = new List<ThongKeDiemDanhModel>();
            //        foreach (int ngayHoc in soNgayHoc)
            //        {
            //            diemDanhModel.Add(new ThongKeDiemDanhModel
            //            {
            //                //phai~ dao~ isOff de ko sinh loi v-switch
            //                DuocNghi = false,
            //                IsOff = false,
            //                Day = ngayHoc,
            //                Dates = new DateTime(year, month, ngayHoc)
            //            });
            //        }

            //        hocVien.ThongKeDiemDanh = diemDanhModel;
            //    }
            //}
                        
            return Json(
                model
                );
        }

        [HttpGet]
        public async Task<IActionResult> DiemDanhTungHocVienNewAsync(Guid HocVienId, Guid LopHocId, int day, int month, int year, bool IsOff)
        {
            if (LopHocId == Guid.Empty || HocVienId == Guid.Empty)
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
                DateTime _ngayDiemDanh = new DateTime(year, month, day);

                var successful = await _diemDanhService.DiemDanhTungHocVienAsync(LopHocId, HocVienId,
                    !IsOff, _ngayDiemDanh, currentUser.Email);
                if (successful == false)
                {
                    return Json(new ResultModel
                    {
                        Status = "Failed",
                        Message = "Điểm Danh lỗi !!!"
                    });
                }

                return Json(new ResultModel
                {
                    Status = "OK",
                    Message = "Điểm Danh thành công !!!",
                    Result = successful
                });
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

        [HttpPost]
        public async Task<IActionResult> DiemDanhTungHocVienAsync([FromBody]Models.LopHoc_DiemDanhViewModel model)
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

            try
            {
                DateTime _ngayDiemDanh = Convert.ToDateTime(model.NgayDiemDanh, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _diemDanhService.DiemDanhTungHocVienAsync(model.LopHocId, model.HocVienId,
                    model.IsOff, _ngayDiemDanh, currentUser.Email);
                if (successful == false)
                {
                    return Json(new ResultModel
                    {
                        Status = "Failed",
                        Message = "Điểm Danh lỗi !!!"
                    });
                }

                return Json(new ResultModel
                {
                    Status = "OK",
                    Message = "Điểm Danh thành công !!!",
                    Result = successful
                });
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

        [HttpPost]
        public async Task<IActionResult> DiemDanhTatCaAsync([FromBody]Models.LopHoc_DiemDanhViewModel model)
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

            try
            {
                DateTime _ngayDiemDanh = Convert.ToDateTime(model.NgayDiemDanh, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _diemDanhService.DiemDanhTatCaAsync(model.LopHocId,
                    model.IsOff, _ngayDiemDanh, currentUser.Email);
                if (successful == false)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Điểm Danh lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Điểm Danh thành công !!!",
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
        public async Task<IActionResult> LopNghiAsync([FromBody]Models.LopHoc_DiemDanhViewModel model)
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

            try
            {
                DateTime _ngayDiemDanh = Convert.ToDateTime(model.NgayDiemDanh, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _diemDanhService.DuocNghi(model.LopHocId, _ngayDiemDanh, currentUser.Email);
                if (successful == false)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Điểm Danh lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Cho Lớp Nghỉ thành công !!!",
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
        public async Task<IActionResult> UndoLopNghiAsync([FromBody]Models.LopHoc_DiemDanhViewModel model)
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

            try
            {
                DateTime _ngayDiemDanh = Convert.ToDateTime(model.NgayDiemDanh, System.Globalization.CultureInfo.InvariantCulture);

                var successful = await _diemDanhService.UndoDuocNghi(model.LopHocId, _ngayDiemDanh, currentUser.Email);
                if (successful == false)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Undo lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Undo thành công !!!",
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
            var model = await _hocPhiService.SoNgayHocAsync(LopHocId, month, year);
            return Json(model);
        }

        private String Number2String(int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c.ToString();
        }

        private System.IO.MemoryStream GenerateExcelFile(List<Models.DiemDanhViewModel> model, int month, int year, Guid LopHocId)
        {
            var stream = new System.IO.MemoryStream();
            using (OfficeOpenXml.ExcelPackage package = new OfficeOpenXml.ExcelPackage(stream))
            {
                string lopHocName = _lopHocService.GetLopHocByIdAsync(LopHocId).Result.Name;
                OfficeOpenXml.ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Diem Danh " + lopHocName);
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

                var soNgayHoc = _hocPhiService.SoNgayHocAsync(LopHocId, month, year).Result;
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