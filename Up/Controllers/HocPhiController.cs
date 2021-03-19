
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
    using Up.Extensions;
    using Up.Models;
    using Up.Services;

    [Authorize]
    public class HocPhiController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHocPhiService _hocPhiService;
        private readonly ILopHocService _lopHocService;
        private readonly IThongKe_DoanhThuHocPhiService _thongKe_DoanhThuHocPhiService;
        private readonly INoService _noService;
        private readonly IHocPhiTronGoiService _hocPhiTronGoiService;
        private readonly Converters.Converter _converter;

        public HocPhiController(
            UserManager<IdentityUser> userManager,
            IHocPhiTronGoiService hocPhiTronGoiService,
            IHocPhiService hocPhiService,
            ILopHocService lopHocService,
            IThongKe_DoanhThuHocPhiService thongKe_DoanhThuHocPhiService,
            INoService noService,
            Converters.Converter converter)
        {
            _userManager = userManager;
            _hocPhiService = hocPhiService;
            _lopHocService = lopHocService;
            _thongKe_DoanhThuHocPhiService = thongKe_DoanhThuHocPhiService;
            _noService = noService;
            _hocPhiTronGoiService = hocPhiTronGoiService;
            _converter = converter;
        }

        [ServiceFilter(typeof(Read_TinhHocPhi))]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _hocPhiService.CanContributeTinhHocPhiAsync(User);
            return View();
        }

        [ServiceFilter(typeof(Read_TinhHocPhi))]
        public async Task<IActionResult> HocPhiTronGoiIndex()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewBag.CanContribute = await _hocPhiTronGoiService.CanContributeAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetHocPhiTronGoiAsync()
        {
            var model = await _hocPhiTronGoiService.GetHocPhiTronGoiAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHocPhiTronGoiAsync([FromBody] CreateHocPhiTronGoiInputModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            await _hocPhiTronGoiService.CreateHocPhiTronGoiAsync(model, currentUser.Email);
            return Json(_converter.ToResultModel("Lưu Học Phí thành công !!!", true, true));
        }

        [HttpPut]
        public async Task<IActionResult> ToggleHocPhiTronGoiAsync([FromBody] HocPhiTronGoiViewModel model)
        {
            if (model.HocPhiTronGoiId == Guid.Empty)
            {
                return RedirectToAction("HocPhiTronGoiIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("HocPhiTronGoiIndex");
            }

            var successful = await _hocPhiTronGoiService.ToggleHocPhiTronGoiAsync(model.HocPhiTronGoiId, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Cập nhật thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Cập nhật lỗi !!!", false));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHocPhiTronGoiAsync([FromBody] HocPhiTronGoiViewModel model)
        {
            if (model.HocPhiTronGoiId == Guid.Empty)
            {
                return RedirectToAction("HocPhiTronGoiIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("HocPhiTronGoiIndex");
            }

            var successful = await _hocPhiTronGoiService.DeleteHocPhiTronGoiAsync(model.HocPhiTronGoiId, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Xóa thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Xóa lỗi !!!", false));
        }

        [HttpGet]
        public async Task<IActionResult> CheckHocPhiTronGoiAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("HocPhiTronGoiIndex");
            }

            var successful = await _hocPhiTronGoiService.CheckIsDisable();
            return successful ?
                Json(_converter.ToResultModel("Cập nhật thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Cập nhật lỗi !!!", false));
        }

        public async Task<IActionResult> UpdateHocPhiTronGoiAsync([FromBody] UpdateHocPhiTronGoiInputModel model)
        {
            if (model.HocPhiTronGoiId == Guid.Empty)
            {
                return RedirectToAction("HocPhiTronGoiIndex");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("HocPhiTronGoiIndex");
            }

            var successful = await _hocPhiTronGoiService.UpdateHocPhiTronGoiAsync(model, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Cập nhật lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Cập nhật thành công !!!", true, successful));
        }

        [HttpGet]
        public async Task<IActionResult> GetTinhHocPhiAsync(TinhHocPhiInputModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            var updateHocPhi = await _lopHocService.UpdateHocPhiLopHocAsync(model, currentUser.Email);
            var modeal = await _hocPhiService.TinhHocPhiAsync(model);
            return Json(modeal);
        }

        [HttpPost]
        public async Task<IActionResult> LuuNhap_HocPhiAsync([FromBody] LuuNhap_ThongKe_HocPhiInputModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Index");
            }

            DateTime _ngayDong = new DateTime(model.models[0].year, model.models[0].month, 1);
            foreach (var item in model.models)
            {
                item.SachIds = item.GiaSach != null ? item.GiaSach.Select(x => x.SachId).ToArray() : new Guid[0];
                item.NgayDong = _ngayDong;
                item.DaDong = false;
                item.DaNo = false;

                await _thongKe_DoanhThuHocPhiService.ThemThongKe_DoanhThuHocPhiAsync(item, currentUser.Email);
            }

            return Json(_converter.ToResultModel("Lưu Nháp Doanh Thu thành công !!!", true));
        }

        [HttpPost]
        public async Task<IActionResult> LuuDoanhThu_HocPhiAsync([FromBody] ThongKe_DoanhThuHocPhiInputModel model)
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

            model.NgayDong = new DateTime(model.year, model.month, 1);
            model.DaDong = true;
            model.DaNo = false;

            var successful = await _thongKe_DoanhThuHocPhiService.ThemThongKe_DoanhThuHocPhiAsync(model, currentUser.Email);
            return successful ?
               Json(_converter.ToResultModel("Lưu Doanh Thu thành công !!!", true, successful))
               :
               Json(_converter.ToResultModel("Lưu Doanh Thu lỗi !!!", false));
        }

        [HttpPost]
        public async Task<IActionResult> LuuNo_HocPhiAsync([FromBody] ThongKe_DoanhThuHocPhiInputModel model)
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

            model.NgayDong = new DateTime(model.year, model.month, 1);
            model.DaDong = false;
            model.DaNo = true;
            await _thongKe_DoanhThuHocPhiService.ThemThongKe_DoanhThuHocPhiAsync(model, currentUser.Email);

            var successful = await _noService.ThemHocVien_NoAsync(model, currentUser.Email);
            return successful ?
               Json(_converter.ToResultModel("Lưu Nợ thành công !!!", true, successful))
               :
               Json(_converter.ToResultModel("Lưu Nợ Thu lỗi !!!", false));
        }

        [HttpPut]
        public IActionResult Export([FromBody] TinhHocPhiViewModel model)
        {
            var stream = GenerateExcelFile(model);
            string excelName = $"UserList.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        private System.IO.MemoryStream GenerateExcelFile(TinhHocPhiViewModel model)
        {
            var stream = new System.IO.MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                string lopHocName = _lopHocService.GetLopHocDetailAsync(model.LopHocId).Result.Name;
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

                worksheet.Cells["A3:B3"].Merge = true;
                worksheet.Cells["A3:C3"].Style.Font.Bold = true;
                worksheet.Cells["A3:C3"].Style.Font.Color.SetColor(Color.Red);
                worksheet.Cells["A3:B3"].Value = "(1) Học phí tháng của lớp: ";
                worksheet.Cells[3, 3].Value = model.HocPhi;

                worksheet.Cells["A4:B4"].Merge = true;
                worksheet.Cells["A4:C4"].Style.Font.Bold = true;
                worksheet.Cells["A4:C4"].Style.Font.Color.SetColor(Color.Red);
                worksheet.Cells["A4:B4"].Value = "(2) Tổng số ngày học trong tháng: ";
                worksheet.Cells[4, 3].Value = model.SoNgayHoc;

                worksheet.Cells[5, 1].Value = "No";
                worksheet.Cells[5, 2].Value = "Tên";
                worksheet.Cells[5, 3].Value = "(3) Số ngày tham gia chính thức";
                worksheet.Cells[5, 3].Style.WrapText = true;
                worksheet.Cells[5, 4].Value = "(4) Học phí trước khuyến học (1)/(2)x(3)";
                worksheet.Cells[5, 4].Style.WrapText = true;
                worksheet.Cells[5, 5].Value = "(5) Khuyến học";
                worksheet.Cells[5, 5].Style.WrapText = true;
                worksheet.Cells[5, 6].Value = "(6)Học phí sau khuyến học (4)-(5)";
                worksheet.Cells[5, 6].Style.WrapText = true;
                worksheet.Cells[5, 7].Value = "(7) Học phí trả lại do được nghỉ tháng trước";
                worksheet.Cells[5, 7].Style.WrapText = true;
                worksheet.Cells[5, 8].Value = "(8) Nợ hoặc học phí còn dư";
                worksheet.Cells[5, 8].Style.WrapText = true;
                worksheet.Cells[5, 9].Value = "(9) Tài liệu mới";
                worksheet.Cells[5, 9].Style.WrapText = true;
                worksheet.Cells[5, 10].Value = "(10) Khoảng cộng khác";
                worksheet.Cells[5, 10].Style.WrapText = true;
                worksheet.Cells[5, 11].Value = "(11) Khoảng trừ khác";
                worksheet.Cells[5, 11].Style.WrapText = true;
                worksheet.Cells[5, 12].Value = "Tổng (Đã được làm tròn) (6)-(7)+(8)+(9)+(10)-(11)";
                worksheet.Cells[5, 12].Style.WrapText = true;
                worksheet.Cells[5, 13].Value = "Chữ ký";
                worksheet.Cells[5, 14].Value = "Ghi Chú";
                worksheet.Cells["A5:N5"].Style.Font.Bold = true;
                worksheet.Cells["A5:N5"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A5:N5"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet.Cells["A5:N5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A5:N5"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                var modelCells = worksheet.Cells["A5"];
                string modelRange = "A5:N" + (totalRows + 5);
                var modelTable = worksheet.Cells[modelRange];

                string modelRange2 = "C6:N" + (totalRows + 5);
                var modelTable2 = worksheet.Cells[modelRange2];
                modelTable2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Assign borders
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                for (int i = 0; i < totalRows; i++)
                {
                    worksheet.Cells[i + 6, 1].Value = i + 1;
                    worksheet.Cells[i + 6, 2].Value = model.HocVienList[i].FullName;
                    worksheet.Cells[i + 6, 3].Value = model.HocVienList[i].SoNgayHoc;
                    worksheet.Cells[i + 6, 4].Value = model.HocVienList[i].HocPhiFixed;
                    worksheet.Cells[i + 6, 5].Value = $"{model.HocVienList[i].KhuyenMai}%";
                    worksheet.Cells[i + 6, 6].Value = model.HocVienList[i].HocPhiSauKhuyenMai;
                    worksheet.Cells[i + 6, 7].Value = model.HocVienList[i].HocPhiBuHocVienVaoSau;
                    worksheet.Cells[i + 6, 8].Value = model.HocVienList[i].TienNo;

                    if (model.HocVienList[i].GiaSach != null && model.HocVienList[i].GiaSach.Length > 0)
                    {
                        double giaSach = 0;
                        string sachcc = string.Empty;
                        foreach (var item in model.HocVienList[i].GiaSach)
                        {
                            giaSach += item.Gia;
                            sachcc += $"{item.Name} - {item.Gia}\r\n";
                        }
                        worksheet.Cells[i + 6, 9].Value = sachcc;
                        worksheet.Cells[i + 6, 9].Style.WrapText = true;
                    }

                    worksheet.Cells[i + 6, 10].Value = model.HocVienList[i].Bonus;
                    worksheet.Cells[i + 6, 11].Value = model.HocVienList[i].Minus;
                    worksheet.Cells[i + 6, 12].Value = model.HocVienList[i].HocPhiMoi;

                    if (model.HocVienList[i].DaDongHocPhi)
                    {
                        worksheet.Cells[i + 6, 13].Value = "- ĐÃ ĐÓNG HP";
                    }

                    worksheet.Cells[i + 6, 14].Value = model.HocVienList[i].GhiChu;
                }

                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                worksheet.Cells.AutoFitColumns();
                worksheet.Column(11).Width = 14;
                worksheet.Column(12).Width = 14;

                worksheet.Column(1).Width = 3;
                worksheet.Column(7).Width = 8;
                worksheet.Column(8).Width = 9;
                worksheet.Column(9).Width = 9;
                worksheet.Column(4).Width = 8;
                worksheet.Column(5).Width = 11;
                worksheet.Column(6).Width = 9;
                worksheet.Column(9).Width = 35;

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }

        [HttpGet]
        public async Task<IActionResult> UndoAsync(ThongKe_DoanhThuHocPhiInputModel model)
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

            await _thongKe_DoanhThuHocPhiService.Undo_DoanhThuAsync(model, currentUser.Email);
            await _noService.Undo_NoAsync(model, currentUser.Email);

            return Json(_converter.ToResultModel("Undo thành công !!!", true, true));
        }
    }
}