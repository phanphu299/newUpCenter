
namespace Up.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Enums;
    using Up.Models;

    public class ChiPhiService : IChiPhiService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ChiPhiService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_TinhLuong)
                .Select(x => x.RoleId).ToList();

            var allRoles = _context.Roles.Where(x => quyen_roles.Contains(x.Id)).Select(x => x.Name);

            bool canContribute = false;

            foreach (string role in roles)
            {
                if (allRoles.Contains(role))
                {
                    canContribute = true;
                    break;
                }
            }
            return canContribute;
        }

        private static IEnumerable<int> DaysInMonth(int year, int month, DayOfWeek dow)
        {
            DateTime monthStart = new DateTime(year, month, 1);
            return Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => monthStart.AddDays(day))
                .Where(date => date.DayOfWeek == dow)
                .Select(date => date.Day);
        }

        private static IEnumerable<int> DaysInMonthWithStartDate(int year, int month, DayOfWeek dow, DateTime StartDate)
        {
            DateTime monthStart = new DateTime(year, month, 1);
            return Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => monthStart.AddDays(day))
                .Where(date => date.DayOfWeek == dow && date.Day >= StartDate.Day)
                .Select(date => date.Day);
        }

        private int TongNgayLam(string NgayLam, int month, int year, DateTime? NgayKetThuc)
        {
            var _ngayLam = NgayLam.Split('-');
            List<int> tongNgayHoc = new List<int>();

            foreach (string el in _ngayLam)
            {
                switch (el.Trim())
                {
                    case "2":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Monday));
                        break;
                    case "3":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Tuesday));
                        break;
                    case "4":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Wednesday));
                        break;
                    case "5":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Thursday));
                        break;
                    case "6":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Friday));
                        break;
                    case "7":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Saturday));
                        break;
                    default:
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Sunday));
                        break;
                }
            }

            if (NgayKetThuc != null && NgayKetThuc.Value.Month == month && NgayKetThuc.Value.Year == year)
                tongNgayHoc = tongNgayHoc.Where(x => x <= NgayKetThuc.Value.Day).ToList();

            return tongNgayHoc.Count;
        }

        private int TongNgayLamVoSau(string NgayLam, int month, int year, DateTime NgayBatDau, DateTime? NgayKetThuc)
        {
            var _ngayLam = NgayLam.Split('-');
            List<int> tongNgayHoc = new List<int>();

            foreach (string el in _ngayLam)
            {
                switch (el.Trim())
                {
                    case "2":
                        tongNgayHoc.AddRange(DaysInMonthWithStartDate(year, month, DayOfWeek.Monday, NgayBatDau));
                        break;
                    case "3":
                        tongNgayHoc.AddRange(DaysInMonthWithStartDate(year, month, DayOfWeek.Tuesday, NgayBatDau));
                        break;
                    case "4":
                        tongNgayHoc.AddRange(DaysInMonthWithStartDate(year, month, DayOfWeek.Wednesday, NgayBatDau));
                        break;
                    case "5":
                        tongNgayHoc.AddRange(DaysInMonthWithStartDate(year, month, DayOfWeek.Thursday, NgayBatDau));
                        break;
                    case "6":
                        tongNgayHoc.AddRange(DaysInMonthWithStartDate(year, month, DayOfWeek.Friday, NgayBatDau));
                        break;
                    case "7":
                        tongNgayHoc.AddRange(DaysInMonthWithStartDate(year, month, DayOfWeek.Saturday, NgayBatDau));
                        break;
                    default:
                        tongNgayHoc.AddRange(DaysInMonthWithStartDate(year, month, DayOfWeek.Sunday, NgayBatDau));
                        break;
                }
            }

            if (NgayKetThuc != null && NgayKetThuc.Value.Month == month && NgayKetThuc.Value.Year == year)
                tongNgayHoc = tongNgayHoc.Where(x => x <= NgayKetThuc.Value.Day).ToList();


            return tongNgayHoc.Count;
        }

        public async Task<TinhChiPhiViewModel> TinhChiPhiAsync(int month, int year)
        {
            var giaoVien = await _context.GiaoViens
            .Where(x => x.IsDisabled == false && x.NgayBatDau.Month <= month && x.NgayBatDau.Year <= year && (x.NgayKetThuc == null || (x.NgayKetThuc.Value.Month >= month && x.NgayKetThuc.Value.Year >= year)))
            .OrderBy(x => x.NhanVien_ViTris.OrderBy(m => m.ViTri.Order).First().ViTri.Order)
            .Select(x => new ChiPhiModel
            {
                Name = x.Name,
                Salary_Expense = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).Salary_Expense : x.BasicSalary,
                TeachingRate = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).TeachingRate : x.TeachingRate,
                TutoringRate = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).TutoringRate : x.TutoringRate,
                MucHoaHong = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).MucHoaHong : x.MucHoaHong,
                Bonus = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).Bonus : 0,
                Minus = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).Minus : 0,
                LoaiChiPhi = (x.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && x.NhanVien_ViTris.Count > 1) ? 4 : (x.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && x.NhanVien_ViTris.Count == 1) ? 1 : 2,
                ChiPhiMoi = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).ChiPhi : x.BasicSalary,
                NhanVienId = x.GiaoVienId,
                SoGioDay = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).SoGioDay : 0,
                SoGioKem = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).SoGioKem : 0,
                SoHocVien = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).SoHocVien : 0,
                DaLuu = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).DaLuu : false,
                NgayLamViec = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).NgayLamViec : x.NgayLamViec.Name,
                SoNgayLam = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).SoNgayLam : TongNgayLam(x.NgayLamViec.Name, month, year, null),
                SoNgayLamVoSau = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).SoNgayLamVoSau : (x.NgayBatDau.Month == month && x.NgayBatDau.Year == year && x.NgayKetThuc != null && x.NgayKetThuc.Value.Month == month && x.NgayKetThuc.Value.Year == year) ? TongNgayLamVoSau(x.NgayLamViec.Name, month, year, x.NgayBatDau, x.NgayKetThuc) : (x.NgayBatDau.Month == month && x.NgayBatDau.Year == year) ? TongNgayLamVoSau(x.NgayLamViec.Name, month, year, x.NgayBatDau, null) : TongNgayLam(x.NgayLamViec.Name, month, year, x.NgayKetThuc),
                DailySalary = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).DailySalary : (Math.Ceiling((x.BasicSalary / TongNgayLam(x.NgayLamViec.Name, month, year, null)) / 10000) * 10000),
                SoNgayNghi = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).SoNgayNghi : 0
            })
            .ToListAsync();

            foreach(var item in giaoVien)
            {
                if(item.SoNgayLam > item.SoNgayLamVoSau)
                {
                    item.Salary_Expense = (Math.Ceiling((item.DailySalary * item.SoNgayLamVoSau) / 10000) * 10000);
                    item.ChiPhiMoi = item.Salary_Expense;
                }
            }

            var chiPhi = await _context.ChiPhiCoDinhs
                .Where(x => x.IsDisabled == false)
                .Select(x => new ChiPhiModel
                {
                    Name = x.Name,
                    Salary_Expense = x.Gia,
                    Bonus = 0,
                    Minus = 0,
                    LoaiChiPhi = 3,
                    ChiPhiMoi = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.ChiPhiCoDinhId == x.ChiPhiCoDinhId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.ChiPhiCoDinhId == x.ChiPhiCoDinhId).ChiPhi : x.Gia,
                    ChiPhiCoDinhId = x.ChiPhiCoDinhId,
                    DaLuu = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.ChiPhiCoDinhId == x.ChiPhiCoDinhId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.ChiPhiCoDinhId == x.ChiPhiCoDinhId).DaLuu : false,
                })
                .ToListAsync();

            List<ChiPhiModel> model = new List<ChiPhiModel>();
            model.AddRange(giaoVien);
            model.AddRange(chiPhi);

            return new TinhChiPhiViewModel
            {
                ChiPhiList = model
            };

        }
    }
}
