using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Converters;
using Up.Data;
using Up.Models;

namespace Up.Repositoties
{
    public class HocPhiRepository : BaseRepository, IHocPhiRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public HocPhiRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateHocPhiAsync(CreateHocPhiInputModel input, string loggedEmployee)
        {
            var hocPhi = _entityConverter.ToEntityHocPhi(input, loggedEmployee);

            _context.HocPhis.Add(hocPhi);
            await _context.SaveChangesAsync();

            return hocPhi.HocPhiId;
        }

        public async Task<bool> DeleteHocPhiAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.HocPhis
                                    .Where(x => x.HocPhiId == id)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<HocPhiViewModel>> GetHocPhiAsync()
        {
            var hocPhis = await _context.HocPhis
                .Where(x => !x.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return hocPhis.Select(hocPhi => _entityConverter.ToHocPhiViewModel(hocPhi)).ToList();
        }

        public async Task<HocPhiViewModel> GetHocPhiDetailAsync(Guid id)
        {
            var hocPhi = await _context.HocPhis.FirstOrDefaultAsync(x => x.HocPhiId == id);
            return _entityConverter.ToHocPhiViewModel(hocPhi);
        }

        public int TinhSoNgayHocVienVoSauAsync(int year, int month, DateTime ngayBatDau, Guid lopHocId)
        {
            var item = _context.LopHocs
                                    .Include(x => x.NgayHoc)
                                    .Where(x => x.LopHocId == lopHocId)
                                    .AsNoTracking()
                                    .FirstOrDefault();

            var ngayHoc = item.NgayHoc.Name.Split('-');
            int tongNgayHoc = 0;

            foreach (string el in ngayHoc)
            {
                switch (el.Trim())
                {
                    case "2":
                        tongNgayHoc += Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Monday, ngayBatDau).Count();
                        break;
                    case "3":
                        tongNgayHoc += Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Tuesday, ngayBatDau).Count();
                        break;
                    case "4":
                        tongNgayHoc += Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Wednesday, ngayBatDau).Count();
                        break;
                    case "5":
                        tongNgayHoc += Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Thursday, ngayBatDau).Count();
                        break;
                    case "6":
                        tongNgayHoc += Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Friday, ngayBatDau).Count();
                        break;
                    case "7":
                        tongNgayHoc += Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Saturday, ngayBatDau).Count();
                        break;
                    default:
                        tongNgayHoc += Helpers.DaysInMonthWithStartDate(year, month, DayOfWeek.Sunday, ngayBatDau).Count();
                        break;
                }
            }

            return tongNgayHoc;
        }

        public async Task<Guid> UpdateHocPhiAsync(UpdateHocPhiInputModel input, string loggedEmployee)
        {
            var item = await _context.HocPhis
                                    .Where(x => x.HocPhiId == input.HocPhiId)
                                    .SingleOrDefaultAsync();

            _entityConverter.MappingEntityHocPhi(input, item, loggedEmployee);
            await _context.SaveChangesAsync();

            return item.HocPhiId;
        }

        public async Task<int> TinhSoNgayDuocChoNghiAsync(Guid lopHocId, int month, int year)
        {
            if (month == 1)
            {
                month = 12;
                year--;
            }
            else
            {
                month--;
            }

            var ngayChoNghi = await _context.LopHoc_DiemDanhs
                                            .Where(x =>
                                                x.LopHocId == lopHocId &&
                                                x.IsDuocNghi == true &&
                                                x.NgayDiemDanh.Month == month &&
                                                x.NgayDiemDanh.Year == year)
                                            .GroupBy(x => x.NgayDiemDanh)
                                            .Select(m => new
                                            {
                                                m.Key
                                            })
                                            .AsNoTracking()
                                            .ToListAsync();
            return ngayChoNghi.Count();
        }

        public async Task<double?> GetHocPhiCuAsync(Guid lopHocId, int month, int year)
        {
            var hocPhi = await _context.LopHoc_HocPhis
                .Include(x => x.HocPhi)
                .AsNoTracking().
                FirstOrDefaultAsync(x => x.Thang == month && x.Nam == year && x.LopHocId == lopHocId);
            return hocPhi?.HocPhi?.Gia;
        }

        public IQueryable<Data.Entities.HocVien_LopHoc> GetHocVien_LopHocsEntity(Guid lopHocId, int month, int year)
        {
            return _context.HocVien_LopHocs
                                    .Include(x => x.LopHoc)
                                    .Include(x => x.HocVien.HocVien_NgayHocs)
                                    .Include(x => x.HocVien.LopHoc_DiemDanhs)
                                    .Include(x => x.HocVien.HocVien_Nos)
                                    .Include(x => x.HocVien.ThongKe_DoanhThuHocPhis)
                                    .ThenInclude(x => x.ThongKe_DoanhThuHocPhi_TaiLieus)
                                    .ThenInclude(x => x.Sach)
                                    .Where(x => x.LopHocId == lopHocId && !x.HocVien.IsDisabled)
                                    .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == lopHocId && (m.NgayKetThuc == null || (m.NgayKetThuc.Value.Month >= month && m.NgayKetThuc.Value.Year == year) || m.NgayKetThuc.Value.Year > year)))
                                    .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == lopHocId && (m.NgayBatDau.Month <= month && m.NgayBatDau.Year == year) || m.NgayBatDau.Year < year));
        }

        public bool IsTronGoi(Guid hocVienId, Guid lopHocId, int month, int year)
        {
            var item = _context.HocPhiTronGois
                .Where(x => x.HocVienId == hocVienId && !x.IsDisabled && !x.IsRemoved)
                .SelectMany(x => x.HocPhiTronGoi_LopHocs)
                .Where(x => x.LopHocId == lopHocId &&
                            (year < x.ToDate.Year || (year == x.ToDate.Year && month <= x.ToDate.Month)) &&
                            (year > x.FromDate.Year || (year == x.FromDate.Year && month >= x.FromDate.Month)))
                .FirstOrDefault();

            return item != null;
        }

        public int TinhSoNgayHocTronGoi(Guid hocVienId, Guid lopHocId, int month, int year, DateTime ngayBatDauHoc)
        {
            var item = _context.LopHocs
                                    .Include(x => x.NgayHoc)
                                    .Where(x => x.LopHocId == lopHocId)
                                    .AsNoTracking()
                                    .SingleOrDefault();

            var ngayHoc = item.NgayHoc.Name.Split('-');
            List<int> tongNgayHoc = new List<int>();

            foreach (string el in ngayHoc)
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

            var ngayHocPhi = _context.HocPhiTronGois
                .Where(x => x.HocVienId == hocVienId)
                .SelectMany(x => x.HocPhiTronGoi_LopHocs)
                .FirstOrDefault(x => x.LopHocId == lopHocId);

            if (ngayHocPhi == null || 
                (ngayHocPhi.FromDate == ngayBatDauHoc && ngayHocPhi.FromDate.Month == month && ngayHocPhi.FromDate.Year == year))
                return 0;

            int soNgayTinhHocPhi = 0;

            if (ngayHocPhi.FromDate.Year == year && ngayHocPhi.FromDate.Month < month && (ngayHocPhi.ToDate.Year > year || ngayHocPhi.ToDate.Year == year && ngayHocPhi.ToDate.Month > month))
                return 0;

            if (ngayHocPhi.FromDate.Year == year && ngayHocPhi.FromDate.Month == month)
            {
                foreach (var ngay in tongNgayHoc)
                {
                    if (ngay < ngayHocPhi.FromDate.Day)
                        soNgayTinhHocPhi++;
                }
            }

            if (ngayHocPhi.ToDate.Year == year && ngayHocPhi.ToDate.Month == month)
            {
                foreach (var ngay in tongNgayHoc)
                {
                    if (ngay > ngayHocPhi.ToDate.Day)
                        soNgayTinhHocPhi++;
                }
            }

            return soNgayTinhHocPhi;
        }
    }
}
