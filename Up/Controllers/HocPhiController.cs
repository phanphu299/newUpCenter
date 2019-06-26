
namespace Up.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OfficeOpenXml.Style;
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using Up.Services;

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
        public async Task<IActionResult> GetTinhHocPhiAsync(Guid LopHocId, int Month, int Year)
        {
            var model = await _hocPhiService.TinhHocPhiAsync(LopHocId, Month, Year);
            return Json(model);
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
                    model.HocPhi, _ngayDong, currentUser.Email);
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
        public async Task<IActionResult> LuuNo_HocPhiAsync([FromBody]Models.NoViewModel model)
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
                var successful = await _noService.ThemHocVien_NoAsync(model.LopHocId, model.HocVienId,
                    model.TienNo, _ngayNo, currentUser.Email);
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
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        private System.IO.MemoryStream GenerateExcelFile(Models.TinhHocPhiViewModel model)
        {
            var stream = new System.IO.MemoryStream();
            using (OfficeOpenXml.ExcelPackage package = new OfficeOpenXml.ExcelPackage(stream))
            {
                OfficeOpenXml.ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Customer");
                int totalRows = model.HocVienList.Count;

                worksheet.Cells["A1:J1"].Merge = true;
                worksheet.Cells["A1:J1"].Value = "DANH SÁCH ĐÓNG HỌC PHÍ " + _lopHocService.GetLopHocByIdAsync(model.LopHocId).Result.Name;
                worksheet.Cells["A1:J1"].Style.Font.Bold = true;
                worksheet.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells["A2:J2"].Merge = true;
                worksheet.Cells["A2:J2"].Value = "T" + model.month + "/" + model.year;
                worksheet.Cells["A2:J2"].Style.Font.Bold = true;
                worksheet.Cells["A2:J2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A2:J2"].Style.Font.Color.SetColor(Color.Red);

                worksheet.Cells[3, 1].Value = "No";
                worksheet.Cells[3, 2].Value = "Tên";
                worksheet.Cells[3, 3].Value = "Khoảng trừ tháng trước";
                worksheet.Cells[3, 4].Value = "Nợ tháng trước";
                worksheet.Cells[3, 5].Value = "Học phí tháng này";
                worksheet.Cells[3, 6].Value = "Mua sách";
                worksheet.Cells[3, 7].Value = "Khuyến mãi";
                worksheet.Cells[3, 8].Value = "Tổng";
                worksheet.Cells[3, 9].Value = "Chữ ký";
                worksheet.Cells[3, 10].Value = "Ghi Chú";

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
                        foreach(double item in model.HocVienList[i].GiaSach)
                        {
                            giaSach += item;
                        }
                        worksheet.Cells[i + 4, 6].Value = giaSach;
                    }
                    
                    worksheet.Cells[i + 4, 7].Value = model.HocVienList[i].KhuyenMai + "%";
                    worksheet.Cells[i + 4, 8].Value = model.HocVienList[i].HocPhiMoi;
                }

                worksheet.Cells.AutoFitColumns();

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }
    }
}