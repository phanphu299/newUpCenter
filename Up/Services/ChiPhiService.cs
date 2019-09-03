
namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Enums;
    using Up.Models;

    public class ChiPhiService : IChiPhiService
    {
        private readonly ApplicationDbContext _context;

        public ChiPhiService(ApplicationDbContext context)
        {
            _context = context;
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

        private int TongNgayLam(string NgayLam, int month, int year)
        {
            var _ngayLam = NgayLam.Split('-');
            int tong = 0;

            foreach (string el in _ngayLam)
            {
                switch (el.Trim())
                {
                    case "2":
                        tong += DaysInMonth(year, month, DayOfWeek.Monday).Count();
                        break;
                    case "3":
                        tong += DaysInMonth(year, month, DayOfWeek.Tuesday).Count();
                        break;
                    case "4":
                        tong += DaysInMonth(year, month, DayOfWeek.Wednesday).Count();
                        break;
                    case "5":
                        tong += DaysInMonth(year, month, DayOfWeek.Thursday).Count();
                        break;
                    case "6":
                        tong += DaysInMonth(year, month, DayOfWeek.Friday).Count();
                        break;
                    case "7":
                        tong += DaysInMonth(year, month, DayOfWeek.Saturday).Count();
                        break;
                    default:
                        tong += DaysInMonth(year, month, DayOfWeek.Sunday).Count();
                        break;
                }
            }

            return tong;
        }

        private int TongNgayLamVoSau(string NgayLam, int month, int year, DateTime NgayBatDau)
        {
            var _ngayLam = NgayLam.Split('-');
            int tongNgayHoc = 0;

            foreach (string el in _ngayLam)
            {
                switch (el.Trim())
                {
                    case "2":
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Monday, NgayBatDau).Count();
                        break;
                    case "3":
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Tuesday, NgayBatDau).Count();
                        break;
                    case "4":
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Wednesday, NgayBatDau).Count();
                        break;
                    case "5":
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Thursday, NgayBatDau).Count();
                        break;
                    case "6":
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Friday, NgayBatDau).Count();
                        break;
                    case "7":
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Saturday, NgayBatDau).Count();
                        break;
                    default:
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Sunday, NgayBatDau).Count();
                        break;
                }
            }

            return tongNgayHoc;
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
                SoNgayLam = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).SoNgayLam : TongNgayLam(x.NgayLamViec.Name, month, year),
                SoNgayLamVoSau = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).SoNgayLamVoSau : (x.NgayBatDau.Month == month && x.NgayBatDau.Year == year) ? TongNgayLamVoSau(x.NgayLamViec.Name, month, year, x.NgayBatDau) : TongNgayLam(x.NgayLamViec.Name, month, year),
                DailySalary = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).DailySalary : (Math.Ceiling((x.BasicSalary / TongNgayLam(x.NgayLamViec.Name, month, year)) / 10000) * 10000),
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
