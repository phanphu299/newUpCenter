namespace Up.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Models;
    using Up.Services;

    public class ThongKeController : Controller
    {
        private readonly IThongKeService _thongKeService;
        private readonly UserManager<IdentityUser> _userManager;

        public ThongKeController(IThongKeService thongKeService, UserManager<IdentityUser> userManager)
        {
            _thongKeService = thongKeService;
            _userManager = userManager;
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
            var giaoTiep = _thongKeService.GetHocVienGiaoTiepAsync()
                .Result
                .GroupBy(p => p.CreatedDate_Date.Month)
                                .Select(g => new ThongKeModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Count(),
                                });

            var listGiaoTiep = Enumerable.Repeat(0, 12).ToList();
            foreach (var item in giaoTiep)
            {
                listGiaoTiep[int.Parse(item.Label) - 1] = item.Data;
            }

            var thieuNhi = _thongKeService.GetHocVienThieuNhiAsync()
                .Result
                .GroupBy(p => p.CreatedDate_Date.Month)
                                .Select(g => new ThongKeModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Count(),
                                });

            var listThieuNhi = Enumerable.Repeat(0, 12).ToList();
            foreach (var item in thieuNhi)
            {
                listThieuNhi[int.Parse(item.Label) - 1] = item.Data;
            }

            var quocTe = _thongKeService.GetHocVienCCQuocTeAsync()
                .Result
                .GroupBy(p => p.CreatedDate_Date.Month)
                                .Select(g => new ThongKeModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Count(),
                                });
            var listQuocTe = Enumerable.Repeat(0, 12).ToList();
            foreach (var item in quocTe)
            {
                listQuocTe[int.Parse(item.Label) - 1] = item.Data;
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
            var fullTime = _thongKeService.GetGiaoVienFullTimeAsync()
                .Result
                .GroupBy(p => p.CreatedDate_Date.Month)
                                .Select(g => new ThongKeModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Count(),
                                });

            var listFullTime = Enumerable.Repeat(0, 12).ToList();
            foreach (var item in fullTime)
            {
                listFullTime[int.Parse(item.Label) - 1] = item.Data;
            }

            var partTime = _thongKeService.GetGiaoVienPartTimeAsync()
                .Result
                .GroupBy(p => p.CreatedDate_Date.Month)
                                .Select(g => new ThongKeModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Count(),
                                });

            var listPartTime = Enumerable.Repeat(0, 12).ToList();
            foreach (var item in partTime)
            {
                listPartTime[int.Parse(item.Label) - 1] = item.Data;
            }

            var quocTe = _thongKeService.GetGiaoVienNuocNgoaiAsync()
                .Result
                .GroupBy(p => p.CreatedDate_Date.Month)
                                .Select(g => new ThongKeModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Count(),
                                });
            var listQuocTe = Enumerable.Repeat(0, 12).ToList();
            foreach (var item in quocTe)
            {
                listQuocTe[int.Parse(item.Label) - 1] = item.Data;
            }

            return Json(new ThongKeGiaoVienViewModel
            {
                FullTime = listFullTime,
                QuocTe = listQuocTe,
                PartTime = listPartTime
            });
        }
    }
}