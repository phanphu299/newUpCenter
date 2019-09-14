
namespace Up.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Services;

    [Authorize]
    public class HocPhiController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHocPhiService _hocPhiService;
        private readonly ILopHocService _lopHocService;
        private readonly IThongKe_DoanhThuHocPhiService _thongKe_DoanhThuHocPhiService;
        private readonly INoService _noService;

        public HocPhiController(UserManager<IdentityUser> userManager, IHocPhiService hocPhiService, ILopHocService lopHocService, IThongKe_DoanhThuHocPhiService thongKe_DoanhThuHocPhiService, INoService noService)
        {
            _userManager = userManager;
            _hocPhiService = hocPhiService;
            _lopHocService = lopHocService;
            _thongKe_DoanhThuHocPhiService = thongKe_DoanhThuHocPhiService;
            _noService = noService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTinhHocPhiAsync(Guid LopHocId, int Month, int Year, double HocPhi)
        {
            var model = await _hocPhiService.TinhHocPhiAsync(LopHocId, Month, Year, HocPhi);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> LuuNhap_HocPhiAsync([FromBody]Models.LuuNhap_ThongKe_HocPhiViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                DateTime _ngayDong = new DateTime(model.models[0].year, model.models[0].month, 1);
                foreach(var item in model.models)
                {
                    var sachIds = item.GiaSach != null ? item.GiaSach.Select(x => x.SachId).ToArray() : new Guid[0];
                    await _thongKe_DoanhThuHocPhiService.ThemThongKe_DoanhThuHocPhiAsync(item.LopHocId, item.HocVienId,
                    item.HocPhiMoi, _ngayDong, item.Bonus, item.Minus, item.KhuyenMai, item.GhiChu, sachIds, false, false, currentUser.Email);
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Lưu Nháp Doanh Thu thành công !!!",
                    Result = true
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
        public async Task<IActionResult> LuuDoanhThu_HocPhiAsync([FromBody]Models.ThongKe_DoanhThuHocPhiViewModel model)
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
                DateTime _ngayDong = new DateTime(model.year, model.month, 1);
                var successful = await _thongKe_DoanhThuHocPhiService.ThemThongKe_DoanhThuHocPhiAsync(model.LopHocId, model.HocVienId,
                    model.HocPhi, _ngayDong, model.Bonus, model.Minus, model.KhuyenMai, model.GhiChu, model.SachIds, true, false, currentUser.Email);
                if (successful == false)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Lưu Doanh Thu lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Lưu Doanh Thu thành công !!!",
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
        public async Task<IActionResult> LuuNo_HocPhiAsync([FromBody]Models.ThongKe_DoanhThuHocPhiViewModel model)
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
                DateTime _ngayNo = new DateTime(model.year, model.month, 1);
                await _thongKe_DoanhThuHocPhiService.ThemThongKe_DoanhThuHocPhiAsync(model.LopHocId, model.HocVienId,
                    model.HocPhi, _ngayNo, model.Bonus, model.Minus, model.KhuyenMai, model.GhiChu, model.SachIds, false, true, currentUser.Email);

                var successful = await _noService.ThemHocVien_NoAsync(model.LopHocId, model.HocVienId,
                    model.HocPhi, _ngayNo, currentUser.Email);
                if (successful == false)
                {
                    return Json(new Models.ResultModel
                    {
                        Status = "Failed",
                        Message = "Lưu Nợ lỗi !!!"
                    });
                }

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Lưu Nợ thành công !!!",
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
        public IActionResult Export([FromBody]Models.TinhHocPhiViewModel model)
        {
            var stream = GenerateExcelFile(model);
            string excelName = $"UserList.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        private System.IO.MemoryStream GenerateExcelFile(Models.TinhHocPhiViewModel model)
        {
            var stream = new System.IO.MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                string lopHocName = _lopHocService.GetLopHocByIdAsync(model.LopHocId).Result.Name;
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Hoc Phi " + lopHocName);
                int totalRows = model.HocVienList.Count;

                worksheet.Cells["A1:L1"].Merge = true;
                worksheet.Cells["A1:L1"].Value = "DANH SÁCH ĐÓNG HỌC PHÍ " + lopHocName;
                worksheet.Cells["A1:L1"].Style.Font.Bold = true;
                worksheet.Cells["A1:L1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells["A2:L2"].Merge = true;
                worksheet.Cells["A2:L2"].Value = "T" + model.month + "/" + model.year;
                worksheet.Cells["A2:L2"].Style.Font.Bold = true;
                worksheet.Cells["A2:L2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A2:L2"].Style.Font.Color.SetColor(Color.Red);

                worksheet.Cells[3, 1].Value = "No";
                worksheet.Cells[3, 2].Value = "Tên";
                worksheet.Cells[3, 3].Value = "Khoảng trừ\r\ntháng trước";
                worksheet.Cells[3, 3].Style.WrapText = true;
                worksheet.Cells[3, 4].Value = "Nợ tháng trước";
                worksheet.Cells[3, 4].Style.WrapText = true;
                worksheet.Cells[3, 5].Value = "Học phí tháng này";
                worksheet.Cells[3, 5].Style.WrapText = true;
                worksheet.Cells[3, 6].Value = "Mua tài liệu";
                worksheet.Cells[3, 6].Style.WrapText = true;
                worksheet.Cells[3, 7].Value = "Khuyến mãi";
                worksheet.Cells[3, 7].Style.WrapText = true;
                worksheet.Cells[3, 8].Value = "Bonus";
                worksheet.Cells[3, 9].Value = "Khoảng trừ khác";
                worksheet.Cells[3, 9].Style.WrapText = true;
                worksheet.Cells[3, 10].Value = "Tổng";
                worksheet.Cells[3, 11].Value = "Chữ ký";
                worksheet.Cells[3, 12].Value = "Ghi Chú";
                worksheet.Cells["A3:L3"].Style.Font.Bold = true;
                worksheet.Cells["A3:L3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A3:L3"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                var modelCells = worksheet.Cells["A3"];
                string modelRange = "A3:L" + (totalRows + 3);
                var modelTable = worksheet.Cells[modelRange];

                

                // Assign borders
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                for (int i = 0; i < totalRows; i++)
                {
                    worksheet.Cells[i + 4, 1].Value = i + 1;
                    worksheet.Cells[i + 4, 2].Value = model.HocVienList[i].FullName;
                    worksheet.Cells[i + 4, 3].Value = model.HocVienList[i].HocPhiBuHocVienVaoSau;
                    worksheet.Cells[i + 4, 4].Value = model.HocVienList[i].TienNo;
                    worksheet.Cells[i + 4, 5].Value = model.HocVienList[i].HocPhiFixed;

                    if(model.HocVienList[i].GiaSach != null && model.HocVienList[i].GiaSach.Length > 0)
                    {
                        double giaSach = 0;
                        foreach(var item in model.HocVienList[i].GiaSach)
                        {
                            giaSach += item.Gia;
                        }
                        worksheet.Cells[i + 4, 6].Value = giaSach;
                    }
                    
                    worksheet.Cells[i + 4, 7].Value = model.HocVienList[i].KhuyenMai + "%";
                    worksheet.Cells[i + 4, 8].Value = model.HocVienList[i].Bonus;
                    worksheet.Cells[i + 4, 9].Value = model.HocVienList[i].Minus;
                    worksheet.Cells[i + 4, 10].Value = model.HocVienList[i].HocPhiMoi;
                    
                    if(model.HocVienList[i].DaDongHocPhi)
                    {
                        worksheet.Cells[i + 4, 12].Value = model.HocVienList[i].GhiChu + " - ĐÃ ĐÓNG HP";
                    }
                    else
                    {
                        worksheet.Cells[i + 4, 12].Value = model.HocVienList[i].GhiChu;
                    }
                }

                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                worksheet.Cells.AutoFitColumns();
                worksheet.Column(11).Width = 14;
                worksheet.Column(12).Width = 14;

                worksheet.Column(1).Width = 3;
                worksheet.Column(7).Width = 5;
                worksheet.Column(9).Width = 8;
                worksheet.Column(4).Width = 8;
                worksheet.Column(5).Width = 8;
                worksheet.Column(6).Width = 6;

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }

        [HttpGet]
        public async Task<IActionResult> UndoAsync(Guid LopHocId, Guid HocVienId, int Month, int Year)
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
                await _thongKe_DoanhThuHocPhiService.Undo_DoanhThuAsync(LopHocId, HocVienId, Month, Year, currentUser.Email);
                await _noService.Undo_NoAsync(LopHocId, HocVienId, Month, Year, currentUser.Email);

                return Json(new Models.ResultModel
                {
                    Status = "OK",
                    Message = "Undo thành công !!!",
                    Result = true
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