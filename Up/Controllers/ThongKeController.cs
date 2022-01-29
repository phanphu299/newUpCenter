namespace Up.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Converters;
    using Up.Models;
    using Up.Services;

    [Authorize]
    public class ThongKeController : Controller
    {
        private readonly IThongKeService _thongKeService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Converters.Converter _converter;

        public ThongKeController(IThongKeService thongKeService, UserManager<IdentityUser> userManager, Converter converter)
        {
            _thongKeService = thongKeService;
            _userManager = userManager;
            _converter = converter;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetThongKeHocVienAsync()
        {
            var giaoTiep = await _thongKeService.GetHocVienGiaoTiepAsync();

            var listGiaoTiep = Enumerable.Repeat(0.0, 12).ToList();
            for (int i = 0; i < listGiaoTiep.Count; i++)
            {
                var item = giaoTiep.FirstOrDefault(x => x.Date.Month == (i + 1));
                listGiaoTiep[i] = item == null ? 0.0 : item.Data;
            }

            var thieuNhi = await _thongKeService.GetHocVienThieuNhiAsync();

            var listThieuNhi = Enumerable.Repeat(0.0, 12).ToList();
            for (int i = 0; i < listThieuNhi.Count; i++)
            {
                var item = thieuNhi.FirstOrDefault(x => x.Date.Month == (i + 1));
                listThieuNhi[i] = item == null ? 0.0 : item.Data;
            }

            var quocTe = await _thongKeService.GetHocVienCCQuocTeAsync();
            var listQuocTe = Enumerable.Repeat(0.0, 12).ToList();
            for (int i = 0; i < listQuocTe.Count; i++)
            {
                var item = quocTe.FirstOrDefault(x => x.Date.Month == (i + 1));
                listQuocTe[i] = item == null ? 0.0 : item.Data;
            }

            return Json(new ThongKeHocVienViewModel
            {
                GiaoTiep = listGiaoTiep,
                QuocTe = listQuocTe,
                ThieuNhi = listThieuNhi
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetThongKeGiaoVienAsync()
        {
            var fullTime = await _thongKeService.GetGiaoVienFullTimeAsync();

            var listFullTime = Enumerable.Repeat(0.0, 12).ToList();

            for (int i = 0; i < listFullTime.Count; i++)
            {
                var item = fullTime.FirstOrDefault(x => x.Date.Month == (i + 1));
                listFullTime[i] = item == null ? 0.0 : item.Data;
            }


            var partTime = await _thongKeService.GetGiaoVienPartTimeAsync();

            var listPartTime = Enumerable.Repeat(0.0, 12).ToList();
            for (int i = 0; i < listPartTime.Count; i++)
            {
                var item = partTime.FirstOrDefault(x => x.Date.Month == (i + 1));
                listPartTime[i] = item == null ? 0.0 : item.Data;
            }

            return Json(new ThongKeGiaoVienViewModel
            {
                FullTime = listFullTime,
                PartTime = listPartTime
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetTongGiaoVienVaHocVienAsync()
        {
            return Json(new ThongKeTongViewModel
            {
                HocVien = await _thongKeService.GetTongHocVienAsync(),
                GiaoVien = await _thongKeService.GetTongGiaoVienAsync(),
                DoanhThu = await _thongKeService.GetTongDoanhThuAsync(),
                ChiPhi = await _thongKeService.GetTongChiPhiAsync()
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetThongKeDoanhThu_HocPhiAsync()
        {
            var doanhThu = (await _thongKeService.GetDoanhThuHocPhiAsync())
                .GroupBy(p => p.NgayDong.Month)
                                .Select(g => new ThongKeModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Sum(x => x.HocPhi)
                                });

            var doanhThuTronGoi = (await _thongKeService.GetDoanhThuHocPhiTronGoiAsync())
                .GroupBy(p => p.NgayDong.Month)
                                .Select(g => new ThongKeModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Sum(x => x.HocPhi)
                                }).ToArray();

            var listDoanhThu = Enumerable.Repeat(0.0, 12).ToList();
            foreach (var item in doanhThu)
            {
                listDoanhThu[int.Parse(item.Label) - 1] = item.Data;
                foreach (var tronGoi in doanhThuTronGoi)
                {
                    if (item.Label == tronGoi.Label)
                        listDoanhThu[int.Parse(item.Label) - 1] += tronGoi.Data;
                }
            }

            var chiPhi = (await _thongKeService.GetChiPhiAsync())
                .GroupBy(p => p.NgayChiPhi.Month)
                                .Select(g => new ThongKeModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Sum(x => x.ChiPhi)
                                });

            var listChiPhi = Enumerable.Repeat(0.0, 12).ToList();
            foreach (var item in chiPhi)
            {
                listChiPhi[int.Parse(item.Label) - 1] = item.Data;
            }

            return Json(new
            {
                DoanhThu = listDoanhThu,
                ChiPhi = listChiPhi,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetThongKeChiPhiAsync()
        {
            var chiPhi = (await _thongKeService.GetChiPhiAsync())
                .GroupBy(p => p.NgayChiPhi.Month)
                                .Select(g => new ThongKeModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Sum(x => x.ChiPhi)
                                });

            var listChiPhi = Enumerable.Repeat(0.0, 12).ToList();
            foreach (var item in chiPhi)
            {
                listChiPhi[int.Parse(item.Label) - 1] = item.Data;
            }

            return Json(listChiPhi);
        }

        [HttpGet]
        public async Task<IActionResult> GetNoAsync()
        {
            var no = (await _thongKeService.GetNoAsync())
                .GroupBy(p => p.NgayNo_Date.Month)
                                .Select(g => new ThongKeModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Sum(x => x.TienNo)
                                });

            var listNo = Enumerable.Repeat(0.0, 12).ToList();
            foreach (var item in no)
            {
                listNo[int.Parse(item.Label) - 1] = item.Data;
            }

            return Json(listNo);
        }

        [HttpGet]
        public async Task<IActionResult> GetHocVienNghiHon3NgayAsync()
        {
            var model = await _thongKeService.GetHocVienOffHon3NgayAsync();
            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetHocVienTheoDoiAsync()
        {
            var model = await _thongKeService.GetHocVienTheoDoiAsync();
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHocVienTheoDoiAsync([FromBody] HocVienTheoDoiViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var successful = await _thongKeService.CreateHocVienTheoDoiAsync(model.HocVienId, model.GhiChu, currentUser.Email);
            return successful == null ?
                Json(_converter.ToResultModel("Thêm mới lỗi !!!", false))
                :
                Json(_converter.ToResultModel("Thêm mới thành công !!!", true, successful));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHocVienTheoDoiAsync([FromBody] HocVienTheoDoiViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var successful = await _thongKeService.UpdateHocVienTheoDoiAsync(model.NoteId, model.GhiChu, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Cập nhật thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Cập nhật lỗi !!!", false));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHocVienTheoDoiAsync([FromBody] HocVienTheoDoiViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var successful = await _thongKeService.DeleteHocVienTheoDoiAsync(model.NoteId, currentUser.Email);
            return successful ?
                Json(_converter.ToResultModel("Xóa thành công !!!", true, successful))
                :
                Json(_converter.ToResultModel("Xóa lỗi !!!", false));
        }
    }
}