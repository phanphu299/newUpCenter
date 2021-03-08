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

        public async Task<int> TinhSoNgayHocVienVoSauAsync(int year, int month, DateTime ngayBatDau, Guid lopHocId)
        {
            var item = await _context.LopHocs
                                    .Include(x => x.NgayHoc)
                                    .Where(x => x.LopHocId == lopHocId)
                                    .AsNoTracking()
                                    .SingleOrDefaultAsync();

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
    }
}
