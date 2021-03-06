using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Converters;
using Up.Data;
using Up.Extensions;
using Up.Models;

namespace Up.Repositoties
{
    public class DiemDanhRepository : BaseRepository, IDiemDanhRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public DiemDanhRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<bool> CheckDaDiemDanhAsync(Guid lopHocId, DateTime ngayDiemDanh)
        {
            return await _context.LopHoc_DiemDanhs
                                    .AnyAsync(x => x.LopHocId == lopHocId && x.NgayDiemDanh == ngayDiemDanh);
        }

        public async Task<bool> DiemDanhTatCaAsync(DiemDanhHocVienInput input, string loggedEmployee)
        {
            var hocViens = await GetHocVienByLopHoc(input.LopHocId);

            var diemDanhList = _entityConverter.ToLopHoc_DiemDanhList(input, hocViens, loggedEmployee);
            await _context.LopHoc_DiemDanhs.AddRangeAsync(diemDanhList);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DiemDanhTungHocVienAsync(DiemDanhHocVienInput input, string loggedEmployee)
        {
            var diemDanh = await _context.LopHoc_DiemDanhs
                                        .Where(x => x.LopHocId == input.LopHocId &&
                                                    x.HocVienId == input.HocVienId &&
                                                    x.NgayDiemDanh == input.NgayDiemDanh)
                                        .SingleOrDefaultAsync();

            if (diemDanh != null)
            {
                diemDanh.IsOff = input.IsOff;
            }
            else
            {
                var lopHoc_DiemDanh = _entityConverter.ToEntityLopHoc_DiemDanh(input, loggedEmployee);
                _context.LopHoc_DiemDanhs.Add(lopHoc_DiemDanh);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DuocNghiAsync(DiemDanhHocVienInput input, string loggedEmployee)
        {
            var hocViens = await _context.HocVien_LopHocs
                                .Include(x => x.LopHoc)
                                .Include(x => x.HocVien.HocVien_NgayHocs)
                                .Where(x => x.LopHocId == input.LopHocId && !x.HocVien.IsDisabled)
                                .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == input.LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc.Value >= input.NgayDiemDanh)))
                                .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == input.LopHocId && (m.NgayBatDau <= input.NgayDiemDanh)))
                                .Select(x => new HocVienViewModel
                                {
                                    HocVienId = x.HocVienId,
                                })
                                .ToListAsync();

            var diemDanhs = _entityConverter.ToDiemDanhDuocNghiList(input, hocViens, loggedEmployee);
            _context.LopHoc_DiemDanhs.AddRange(diemDanhs);

            var saveResult = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DiemDanhViewModel>> GetDiemDanhByHocVienAndLopHoc(Guid hocVienId, Guid lopHocId)
        {
            return await _context.LopHoc_DiemDanhs.Where(x => x.HocVienId == hocVienId && x.LopHocId == lopHocId)
                                .OrderByDescending(x => x.NgayDiemDanh)
                                .Select(x => new DiemDanhViewModel
                                {
                                    IsDuocNghi = x.IsDuocNghi,
                                    IsOff = x.IsOff,
                                    NgayDiemDanh = x.NgayDiemDanh.ToClearDate(),
                                    NgayDiemDanh_Date = x.NgayDiemDanh,
                                    Day = x.NgayDiemDanh.Day
                                })
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<List<DiemDanhViewModel>> GetDiemDanhByLopHoc(Guid lopHocId, int month, int year)
        {
            return await _context.HocVien_LopHocs
                    .Where(x => x.LopHocId == lopHocId)
                    .Where(x => !x.HocVien.IsDisabled)
                    .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == lopHocId && (m.NgayKetThuc == null || (m.NgayKetThuc.Value.Month >= month && m.NgayKetThuc.Value.Year == year) || m.NgayKetThuc.Value.Year > year)))
                    .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == lopHocId && (m.NgayBatDau.Month <= month && m.NgayBatDau.Year == year) || m.NgayBatDau.Year < year))
                    .GroupJoin(_context.LopHoc_DiemDanhs.Where(x => x.LopHocId == lopHocId),
                    i => i.HocVienId,
                    p => p.HocVienId,
                    (i, g) =>
                    new
                    {
                        i = i,
                        g = g
                    })
                    .SelectMany(
                    temp0 => temp0.g.DefaultIfEmpty(),
                    (temp0, cat) =>
                        new DiemDanhViewModel
                        {
                            IsDuocNghi = (cat == null) ? false : cat.IsDuocNghi,
                            IsOff = (cat == null) ? true : cat.IsOff,
                            NgayDiemDanh = (cat == null) ? new DateTime().ToClearDate() : cat.NgayDiemDanh.ToClearDate(),
                            HocVien = temp0.i.HocVien.FullName,
                            NgayDiemDanh_Date = (cat == null) ? new DateTime() : cat.NgayDiemDanh,
                            HocVienId = temp0.i.HocVienId,
                            NgayBatDau = temp0.i.LopHoc.HocVien_NgayHocs.FirstOrDefault(m => m.LopHocId == lopHocId && m.HocVienId == temp0.i.HocVienId).NgayBatDau,
                            NgayKetThuc = temp0.i.LopHoc.HocVien_NgayHocs.FirstOrDefault(m => m.LopHocId == lopHocId && m.HocVienId == temp0.i.HocVienId).NgayKetThuc
                        }
                    )
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<List<Guid>> GetDiemDanhByLopHocAndNgayDiemDanhAndHocVienIdsAsync(Guid lopHocId, DateTime ngayDiemDanh, IList<Guid> hocVienIds)
        {
            return await _context.LopHoc_DiemDanhs
                                        .Where(x => x.LopHocId == lopHocId && x.NgayDiemDanh == ngayDiemDanh && hocVienIds.Contains(x.HocVienId))
                                        .Select(x => x.LopHoc_DiemDanhId)
                                        .ToListAsync();
        }

        public async Task<List<Guid>> GetDiemDanhByLopHocAndNgayDiemDanhAsync(Guid lopHocId, DateTime ngayDiemDanh)
        {
            return await _context.LopHoc_DiemDanhs
                                    .Where(x => x.LopHocId == lopHocId && x.NgayDiemDanh == ngayDiemDanh)
                                    .Select(x => x.LopHoc_DiemDanhId)
                                    .ToListAsync();
        }

        public async Task<List<HocVienViewModel>> GetHocVienByLopHoc(Guid lopHocId)
        {
            return await _context.HocVien_LopHocs
                                .Include(x => x.LopHoc)
                                .Include(x => x.HocVien.HocVien_NgayHocs)
                                .Where(x => x.LopHocId == lopHocId && !x.HocVien.IsDisabled)
                                .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == lopHocId && (m.NgayKetThuc == null || m.NgayKetThuc.Value >= DateTime.Now)))
                                .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == lopHocId && (m.NgayBatDau <= DateTime.Now)))
                                .Select(x => new HocVienViewModel
                                {
                                    FullName = x.HocVien.FullName,
                                    EnglishName = x.HocVien.EnglishName,
                                    HocVienId = x.HocVienId,
                                })
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task RemoveDiemDanhByIdsAsync(IList<Guid> ids)
        {
            var diemDanhs = _context.LopHoc_DiemDanhs
                                    .Where(x => ids.Contains(x.LopHoc_DiemDanhId));

            _context.LopHoc_DiemDanhs.RemoveRange(diemDanhs);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveHocVienOff(Guid lopHocId, List<Guid> hocVienIds, List<DateTime> ngayDiemDanhs, string loggedEmployee)
        {
            foreach (DateTime NgayDiemDanh in ngayDiemDanhs)
            {
                var isExistingIds = await GetDiemDanhByLopHocAndNgayDiemDanhAndHocVienIdsAsync(lopHocId, NgayDiemDanh, hocVienIds);
                if (isExistingIds.Any())
                    await RemoveDiemDanhByIdsAsync(isExistingIds);

                var hocViens = await _context.HocVien_LopHocs
                                    .Include(x => x.LopHoc)
                                    .Include(x => x.HocVien.HocVien_NgayHocs)
                                    .Where(x => x.LopHocId == lopHocId && !x.HocVien.IsDisabled)
                                    .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == lopHocId && (m.NgayKetThuc == null || m.NgayKetThuc.Value >= NgayDiemDanh)))
                                    .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == lopHocId && (m.NgayBatDau <= NgayDiemDanh)))
                                    .Where(x => hocVienIds.Contains(x.HocVienId))
                                    .Select(x => new HocVienViewModel
                                    {
                                        HocVienId = x.HocVienId,
                                    })
                                    .ToListAsync();

                var diemDanhs = _entityConverter.ToDiemDanhSinhVienOffList(lopHocId, NgayDiemDanh, hocViens, loggedEmployee);
                _context.LopHoc_DiemDanhs.AddRange(diemDanhs);
            }
            var saveResult = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<int>> SoNgayHocAsync(Guid lopHocId, int month, int year)
        {
            var item = await _context.LopHocs
                                    .Include(x => x.NgayHoc)
                                    .Where(x => x.LopHocId == lopHocId)
                                    .AsNoTracking()
                                    .SingleOrDefaultAsync();

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

            return tongNgayHoc.OrderBy(x => x).ToList();
        }
    }
}
