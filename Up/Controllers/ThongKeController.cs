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
                                .Select(g => new ThongKeHocVienModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Count(),
                                });
            var listGiaoTiep = new List<int>();
            foreach(var item in giaoTiep)
            {
                listGiaoTiep.Add(item.Data);
            }

            var thieuNhi = _thongKeService.GetHocVienThieuNhiAsync()
                .Result
                .GroupBy(p => p.CreatedDate_Date.Month)
                                .Select(g => new ThongKeHocVienModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Count(),
                                });
            var listThieuNhi = new List<int>();
            foreach (var item in thieuNhi)
            {
                listThieuNhi.Add(item.Data);
            }

            var quocTe = _thongKeService.GetHocVienCCQuocTeAsync()
                .Result
                .GroupBy(p => p.CreatedDate_Date.Month)
                                .Select(g => new ThongKeHocVienModel
                                {
                                    Label = g.Key.ToString(),
                                    Data = g.Count(),
                                });
            var listQuocTe = new List<int>();
            foreach (var item in quocTe)
            {
                listQuocTe.Add(item.Data);
            }

            return Json(new ThongKeHocVienViewModel {
                GiaoTiep = listGiaoTiep,
                QuocTe = listQuocTe,
                ThieuNhi = listThieuNhi
            });
        }
    }
}