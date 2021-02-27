using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Converters;
using Up.Data;
using Up.Enums;
using Up.Models;

namespace Up.Repositoties
{
    public class ChiPhiRepository : BaseRepository, IChiPhiRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public ChiPhiRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<TinhChiPhiViewModel> TinhChiPhiAsync(int month, int year)
        {
            var giaoVienList = await _context.GiaoViens
                .Include(x => x.NgayLamViec)
                .Include(x => x.ThongKe_ChiPhis)
                .Include(x => x.NhanVien_ViTris)
                    .ThenInclude(x => x.ViTri)
                .Where(x => !x.IsDisabled)
                .Where(x => (x.NgayBatDau.Month <= month && x.NgayBatDau.Year == year) || x.NgayBatDau.Year < year)
                .Where(x => (x.NgayKetThuc == null || (x.NgayKetThuc.Value.Month >= month && x.NgayKetThuc.Value.Year == year) || x.NgayKetThuc.Value.Year > year))
                .AsNoTracking()
                .ToListAsync();

            var giaoVien = giaoVienList
                .OrderBy(x => x.NhanVien_ViTris.OrderBy(m => m.ViTri.Order).First().ViTri.Order)
                .Select(nhanVien =>
                {
                    var thongKeData = nhanVien
                                            .ThongKe_ChiPhis
                                            .FirstOrDefault(m => m.NgayChiPhi.Month == month &&
                                                                 m.NgayChiPhi.Year == year &&
                                                                 m.NhanVienId == nhanVien.GiaoVienId);

                    var salary_Expense = thongKeData?.Salary_Expense ?? nhanVien.BasicSalary;
                    var teachingRate = thongKeData?.TeachingRate ?? nhanVien.TeachingRate;
                    var tutoringRate = thongKeData?.TutoringRate ?? nhanVien.TutoringRate;
                    var hoaHong = thongKeData?.MucHoaHong ?? nhanVien.MucHoaHong;
                    var bonus = thongKeData?.Bonus ?? 0;
                    var minus = thongKeData?.Minus ?? 0;
                    var loaiChiPhi = (nhanVien.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && nhanVien.NhanVien_ViTris.Count > 1) ?
                                4 : (nhanVien.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && nhanVien.NhanVien_ViTris.Count == 1) ?
                                1 : 2;
                    var chiPhiMoi = thongKeData?.ChiPhi ?? nhanVien.BasicSalary;
                    var soGioDay = thongKeData?.SoGioDay ?? 0;
                    var soGioKem = thongKeData?.SoGioKem ?? 0;
                    var soHocVien = thongKeData?.SoHocVien ?? 0;
                    bool daLuu = thongKeData?.DaLuu ?? false;
                    var ngayLamViec = thongKeData?.NgayLamViec ?? nhanVien.NgayLamViec.Name;
                    var soNgayLam = thongKeData?.SoNgayLam ?? TongNgayLam(nhanVien.NgayLamViec.Name, month, year, null);

                    var soNgayLamVoSau = thongKeData?.SoNgayLamVoSau ??
                            ((nhanVien.NgayBatDau.Month == month &&
                             nhanVien.NgayBatDau.Year == year &&
                             nhanVien.NgayKetThuc != null &&
                             nhanVien.NgayKetThuc.Value.Month == month &&
                             nhanVien.NgayKetThuc.Value.Year == year)
                             ?
                             TongNgayLamVoSau(nhanVien.NgayLamViec.Name, month, year, nhanVien.NgayBatDau, nhanVien.NgayKetThuc) :
                             (nhanVien.NgayBatDau.Month == month && nhanVien.NgayBatDau.Year == year)
                             ?
                             TongNgayLamVoSau(nhanVien.NgayLamViec.Name, month, year, nhanVien.NgayBatDau, null) :
                             TongNgayLam(nhanVien.NgayLamViec.Name, month, year, nhanVien.NgayKetThuc));

                    var dailySalary = thongKeData?.DailySalary ?? /*(Math.Ceiling(*/(nhanVien.BasicSalary / TongNgayLam(nhanVien.NgayLamViec.Name, month, year, null)) /*/ 10000) * 10000)*/;
                    var soNgayNghi = thongKeData?.SoNgayNghi ?? 0;
                    var ghiChu = thongKeData?.GhiChu ?? string.Empty;

                    if (soNgayLam > soNgayLamVoSau)
                    {
                        salary_Expense = /*(Math.Ceiling(*/(dailySalary * soNgayLamVoSau)/* / 10000) * 10000)*/;
                        chiPhiMoi = salary_Expense;
                    }
                    chiPhiMoi = (Math.Ceiling(chiPhiMoi / 10000) * 10000);

                    return _entityConverter.ToChiPhiModel(
                        nhanVien,
                        salary_Expense,
                        teachingRate,
                        tutoringRate,
                        hoaHong,
                        bonus,
                        minus,
                        loaiChiPhi,
                        chiPhiMoi,
                        soGioDay,
                        soGioKem,
                        soHocVien,
                        daLuu,
                        ngayLamViec,
                        soNgayLam,
                        soNgayLamVoSau,
                        dailySalary,
                        soNgayNghi,
                        ghiChu);
                })
                .ToList();

            var chiPhiList = await _context.ChiPhiCoDinhs
                .Include(x => x.ThongKe_ChiPhis)
                .Where(x => !x.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            var chiPhi = chiPhiList
                .Select(expense =>
                {
                    var thongKeData = expense.ThongKe_ChiPhis
                                        .FirstOrDefault(m => m.NgayChiPhi.Month == month &&
                                                             m.NgayChiPhi.Year == year &&
                                                             m.ChiPhiCoDinhId == expense.ChiPhiCoDinhId);

                    var chiPhiMoi = thongKeData?.ChiPhi ?? expense.Gia;
                    var daLuu = thongKeData?.DaLuu ?? false;

                    return _entityConverter.ToChiPhiModel(expense, chiPhiMoi, daLuu);
                })
                .ToList();

            List<ChiPhiModel> model = new List<ChiPhiModel>();
            model.AddRange(giaoVien);
            model.AddRange(chiPhi);

            return new TinhChiPhiViewModel
            {
                ChiPhiList = model
            };
        }

        private int TongNgayLam(string ngayLam, int month, int year, DateTime? ngayKetThuc)
        {
            var _ngayLam = ngayLam.Split('-');
            List<int> tongNgayHoc = new List<int>();

            foreach (string el in _ngayLam)
            {
                switch (el.Trim())
                {
                    case "2":
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Monday));
                        break;
                    case "3":
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Tuesday));
                        break;
                    case "4":
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Wednesday));
                        break;
                    case "5":
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Thursday));
                        break;
                    case "6":
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Friday));
                        break;
                    case "7":
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Saturday));
                        break;
                    default:
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Sunday));
                        break;
                }
            }

            if (ngayKetThuc != null && ngayKetThuc.Value.Month == month && ngayKetThuc.Value.Year == year)
                tongNgayHoc = tongNgayHoc.Where(x => x <= ngayKetThuc.Value.Day).ToList();

            return tongNgayHoc.Count;
        }

        private int TongNgayLamVoSau(string ngayLam, int month, int year, DateTime ngayBatDau, DateTime? ngayKetThuc)
        {
            var _ngayLam = ngayLam.Split('-');
            List<int> tongNgayHoc = new List<int>();

            foreach (string el in _ngayLam)
            {
                switch (el.Trim())
                {
                    case "2":
                        tongNgayHoc.AddRange(Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Monday, ngayBatDau));
                        break;
                    case "3":
                        tongNgayHoc.AddRange(Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Tuesday, ngayBatDau));
                        break;
                    case "4":
                        tongNgayHoc.AddRange(Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Wednesday, ngayBatDau));
                        break;
                    case "5":
                        tongNgayHoc.AddRange(Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Thursday, ngayBatDau));
                        break;
                    case "6":
                        tongNgayHoc.AddRange(Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Friday, ngayBatDau));
                        break;
                    case "7":
                        tongNgayHoc.AddRange(Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Saturday, ngayBatDau));
                        break;
                    default:
                        tongNgayHoc.AddRange(Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Sunday, ngayBatDau));
                        break;
                }
            }

            if (ngayKetThuc != null && ngayKetThuc.Value.Month == month && ngayKetThuc.Value.Year == year)
                tongNgayHoc = tongNgayHoc.Where(x => x <= ngayKetThuc.Value.Day).ToList();

            return tongNgayHoc.Count;
        }
    }
}
