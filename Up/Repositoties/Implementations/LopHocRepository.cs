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
    public class LopHocRepository : BaseRepository, ILopHocRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;

        public LopHocRepository(
            ApplicationDbContext context,
            EntityConverter entityConverter,
            UserManager<IdentityUser> userManager)
            : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateLopHocAsync(CreateLopHocInputModel input, string loggedEmployee)
        {
            var lopHoc = _entityConverter.ToEntityLopHoc(input, loggedEmployee);

            _context.LopHocs.Add(lopHoc);
            await _context.SaveChangesAsync();

            return lopHoc.LopHocId;
        }

        public async Task<bool> DeleteLopHocAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.LopHocs
                                    .Where(x => x.LopHocId == id)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var result = await _context.SaveChangesAsync();
            return result == 1;
        }

        public async Task<int> DemSoNgayHocAsync(Guid id, int month, int year)
        {
            var item = await _context.LopHocs
                                    .Include(x => x.NgayHoc)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.LopHocId == id);

            var ngayHoc = item.NgayHoc.Name.Split('-');
            List<int> tongNgayHoc = new List<int>();

            foreach (string el in ngayHoc)
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

            return tongNgayHoc.Count;
        }

        private static IEnumerable<int> DaysInMonth(int year, int month, DayOfWeek dow)
        {
            DateTime monthStart = new DateTime(year, month, 1);
            return Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => monthStart.AddDays(day))
                .Where(date => date.DayOfWeek == dow)
                .Select(date => date.Day);
        }

        public async Task<List<LopHocViewModel>> GetAvailableLopHocAsync(int? thang = null, int? nam = null)
        {
            return await _context.LopHocs
                .Include(x => x.LopHoc_HocPhis)
                .Where(x => !x.IsDisabled && !x.IsCanceled && !x.IsGraduated)
                .OrderBy(x => x.Name)
                .Select(x => new LopHocViewModel
                {
                    Name = x.Name,
                    LopHocId = x.LopHocId,
                    HocPhi = !x.LopHoc_HocPhis.Any(m => m.Nam == nam && m.Thang == thang) ? null :
                    new HocPhiViewModel
                    {
                        HocPhiId = x.LopHoc_HocPhis.FirstOrDefault(m => m.Nam == nam && m.Thang == thang).HocPhiId,
                        Gia = x.LopHoc_HocPhis.FirstOrDefault(m => m.Nam == nam && m.Thang == thang).HocPhi.Gia
                    }
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<LopHocViewModel>> GetGraduatedAndCanceledLopHocAsync()
        {
            return await _context.LopHocs
                .Where(x => !x.IsDisabled && (x.IsCanceled || x.IsGraduated))
                .Select(x => new LopHocViewModel
                {
                    Name = x.Name,
                    LopHocId = x.LopHocId
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IList<Guid>> GetHocVienIdAsync(Guid id)
        {
            return await _context.HocVien_LopHocs
                                        .Where(x => x.LopHocId == id)
                                        .Select(x => x.HocVienId)
                                        .ToListAsync();
        }

        public async Task<List<LopHocViewModel>> GetLopHocAsync()
        {
            var lopHocs = await _context.LopHocs
                .Include(x => x.GioHoc)
                .Include(x => x.KhoaHoc)
                .Include(x => x.NgayHoc)
                .Where(x => !x.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return lopHocs.Select(lopHoc => _entityConverter.ToLopHocViewModel(lopHoc)).ToList();
        }

        public async Task<List<LopHocViewModel>> GetLopHocByHocVienIdAsync(Guid hocVienId)
        {
            return await _context.LopHocs
                .Where(x => x.HocVien_LopHocs.Any(m => m.HocVienId == hocVienId))
                .Select(x => new LopHocViewModel
                {
                    Name = x.Name,
                    LopHocId = x.LopHocId
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<LopHocViewModel> GetLopHocDetailAsync(Guid id)
        {
            var lopHoc = await _context.LopHocs
                .Include(x => x.GioHoc)
                .Include(x => x.KhoaHoc)
                .Include(x => x.NgayHoc)
                .FirstOrDefaultAsync(x => x.LopHocId == id);

            return _entityConverter.ToLopHocViewModel(lopHoc);
        }

        public async Task<bool> ToggleHuyLopAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.LopHocs
                                         .Where(x => x.LopHocId == id)
                                         .SingleOrDefaultAsync();

            item.IsCanceled = !item.IsCanceled;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var _hocVien_NgayHoc = await _context.HocVien_NgayHocs
                                            .Where(x => x.LopHocId == id && x.NgayKetThuc == null)
                                            .ToListAsync();

            if (item.IsCanceled == true)
            {
                foreach (var ngayHoc in _hocVien_NgayHoc)
                {
                    ngayHoc.NgayKetThuc = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ToggleTotNghiepAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.LopHocs
                                         .Where(x => x.LopHocId == id)
                                         .SingleOrDefaultAsync();

            item.IsGraduated = !item.IsGraduated;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var _hocVien_NgayHoc = await _context.HocVien_NgayHocs
                                            .Where(x => x.LopHocId == id && x.NgayKetThuc == null)
                                            .ToListAsync();

            if (item.IsGraduated == true)
            {
                foreach (var ngayHoc in _hocVien_NgayHoc)
                {
                    ngayHoc.NgayKetThuc = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateHocPhiLopHocAsync(TinhHocPhiInputModel input, string loggedEmployee)
        {
            var item = await _context.LopHoc_HocPhis
                                        .Where(x => x.LopHocId == input.LopHocId && x.Thang == input.Month && x.Nam == input.Year)
                                        .SingleOrDefaultAsync();

            if (item == null)
            {
                var lopHoc_HocPhi = _entityConverter.ToEntityHocPhi(input.LopHocId, input.HocPhiId, input.Month, input.Year);
                await _context.LopHoc_HocPhis.AddAsync(lopHoc_HocPhi);
            }
            else
            {
                item.HocPhiId = input.HocPhiId;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Guid> UpdateLopHocAsync(UpdateLopHocInputModel input, string loggedEmployee)
        {
            var item = await _context.LopHocs
                                    .Where(x => x.LopHocId == input.LopHocId)
                                    .SingleOrDefaultAsync();

            _entityConverter.MappingEntityLopHoc(input, item, loggedEmployee);

            await _context.SaveChangesAsync();
            return item.LopHocId;
        }

        public async Task<IList<Guid>> GetLopHocIdByGioHocAsync(Guid gioHocId)
        {
            return await _context.LopHocs
                .Where(x => x.GioHocId == gioHocId)
                .Select(x => x.LopHocId)
                .ToListAsync();
        }
    }
}
