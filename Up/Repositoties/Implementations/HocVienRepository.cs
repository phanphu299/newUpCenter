using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Converters;
using Up.Data;
using Up.Data.Entities;
using Up.Models;

namespace Up.Repositoties
{
    public class HocVienRepository : BaseRepository, IHocVienRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;

        public HocVienRepository(
            ApplicationDbContext context,
            EntityConverter entityConverter,
            UserManager<IdentityUser> userManager)
            : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateHocVienAsync(CreateHocVienInputModel input, string loggedEmployee)
        {
            HocVien hocVien = _entityConverter.ToEntityHocVien(input, loggedEmployee);
            hocVien.Trigram = await GenerateTrigramAsync(input.FullName);

            _context.HocViens.Add(hocVien);

            foreach (var item in input.LopHoc_NgayHocList)
            {
                var hocVien_LopHoc = new HocVien_LopHoc
                {
                    HocVienId = hocVien.HocVienId,
                    LopHocId = item.LopHoc.LopHocId,
                    CreatedBy = loggedEmployee
                };

                var hocVien_NgayHoc = new HocVien_NgayHoc
                {
                    HocVienId = hocVien.HocVienId,
                    LopHocId = item.LopHoc.LopHocId,
                    NgayBatDau = Convert.ToDateTime(item.NgayHoc, System.Globalization.CultureInfo.InvariantCulture),
                    NgayKetThuc = null,
                    CreatedBy = loggedEmployee
                };

                _context.HocVien_LopHocs.Add(hocVien_LopHoc);
                _context.HocVien_NgayHocs.Add(hocVien_NgayHoc);
            }

            await _context.SaveChangesAsync();

            return hocVien.HocVienId;
        }

        public async Task<List<HocVienViewModel>> GetHocVienAsync()
        {
            var hocViens = await _context.HocViens
                                        .Where(x => !x.IsDisabled)
                                        .Include(x => x.QuanHe)
                                        .Include(x => x.HocVien_LopHocs)
                                        .ThenInclude(x => x.LopHoc)
                                        .Include(x => x.HocVien_NgayHocs)
                                        .Include(x => x.ChallengeResults)
                                        .ThenInclude(x => x.ThuThach)
                                        .AsNoTracking()
                                        .ToListAsync();

            return hocViens
                .Select(hocvien => _entityConverter.ToHocVienViewModel(hocvien))
                .ToList();
        }

        public async Task<List<HocVienLightViewModel>> GetHocVienByNameAsync(string name)
        {
            var hocViens = await _context.HocViens
                .Where(x => !x.IsDisabled && x.FullName.ToLower().Contains(name.ToLower()))
                .AsNoTracking()
                .ToListAsync();

            return hocViens
                .Select(hocVien => _entityConverter.ToHocVienLightViewModel(hocVien))
                .ToList();
        }

        public async Task<HocVienLightViewModel> GetHocVienByTrigramAsync(string trigram)
        {
            var hocVien = await _context.HocViens
                .Where(x => !x.IsDisabled && x.Trigram.ToLower().Equals(trigram.ToLower()))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (hocVien == null) return null;

            return _entityConverter.ToHocVienLightViewModel(hocVien);
        }

        public async Task<HocVienViewModel> GetHocVienDetailAsync(Guid id)
        {
            var hocVien = await _context.HocViens
                                        .Include(x => x.QuanHe)
                                        .Include(x => x.HocVien_LopHocs)
                                        .ThenInclude(x => x.LopHoc)
                                        .Include(x => x.HocVien_NgayHocs)
                                        .Include(x => x.ChallengeResults)
                                        .ThenInclude(x => x.ThuThach)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.HocVienId == id);

            return _entityConverter.ToHocVienViewModel(hocVien);
        }

        public async Task<Guid> ImportHocVienAsync(ImportHocVienInputModel input, string loggedEmployee)
        {
            HocVien hocVien = _entityConverter.ToEntityHocVien(input, loggedEmployee);
            hocVien.Trigram = await GenerateTrigramAsync(input.FullName);

            _context.HocViens.Add(hocVien);

            var hocVien_LopHoc = new HocVien_LopHoc
            {
                HocVienId = hocVien.HocVienId,
                LopHocId = input.LopHocId,
                CreatedBy = loggedEmployee
            };

            if (input.NgayBatDau != null)
            {
                var hocVien_NgayHoc = new HocVien_NgayHoc
                {
                    HocVienId = hocVien.HocVienId,
                    LopHocId = input.LopHocId,
                    NgayBatDau = input.NgayBatDau.Value,
                    NgayKetThuc = null,
                    CreatedBy = loggedEmployee
                };

                _context.HocVien_LopHocs.Add(hocVien_LopHoc);
                _context.HocVien_NgayHocs.Add(hocVien_NgayHoc);
            }

            await _context.SaveChangesAsync();

            return hocVien.HocVienId;
        }

        public async Task<Guid> UpdateHocVienAsync(UpdateHocVienInputModel input, string loggedEmployee)
        {
            var item = await _context.HocViens
                                    .Where(x => x.HocVienId == input.HocVienId)
                                    .SingleOrDefaultAsync();

            _entityConverter.MappingEntityHocVien(input, item, loggedEmployee);

            var _hocVien_LopHoc = await _context.HocVien_LopHocs
                                                .Where(x => x.HocVienId == item.HocVienId)
                                                .ToListAsync();

            _context.HocVien_LopHocs.RemoveRange(_hocVien_LopHoc);

            var _lopHocIds = input.LopHoc_NgayHocList
                                .Select(m => m.LopHoc.LopHocId).ToList();
            var _hocVien_NgayHoc_Dumb = await _context.HocVien_NgayHocs
                                                .Where(x => x.HocVienId == input.HocVienId && !_lopHocIds.Contains(x.LopHocId))
                                                .ToListAsync();

            _context.HocVien_NgayHocs.RemoveRange(_hocVien_NgayHoc_Dumb);

            foreach (var itemLop in input.LopHoc_NgayHocList)
            {
                var hocVien_LopHoc = new HocVien_LopHoc
                {
                    HocVienId = item.HocVienId,
                    LopHocId = itemLop.LopHoc.LopHocId,
                    CreatedBy = loggedEmployee
                };

                _context.HocVien_LopHocs.Add(hocVien_LopHoc);

                var _hocVien_NgayHoc = await _context.HocVien_NgayHocs
                                            .FirstOrDefaultAsync(x => x.HocVienId == item.HocVienId && x.LopHocId == itemLop.LopHoc.LopHocId);

                if (_hocVien_NgayHoc == null)
                {
                    var HV_NgayHoc = new HocVien_NgayHoc
                    {
                        HocVienId = item.HocVienId,
                        LopHocId = itemLop.LopHoc.LopHocId,
                        NgayBatDau = Convert.ToDateTime(itemLop.NgayHoc, System.Globalization.CultureInfo.InvariantCulture),
                        NgayKetThuc = null,
                        CreatedBy = loggedEmployee
                    };

                    _context.HocVien_NgayHocs.Add(HV_NgayHoc);
                }
                else
                    _hocVien_NgayHoc.NgayBatDau = Convert.ToDateTime(itemLop.NgayHoc, System.Globalization.CultureInfo.InvariantCulture);
            }

            await _context.SaveChangesAsync();
            return item.HocVienId;
        }

        public async Task<bool> DeleteHocVienAsync(Guid hocVienId, string loggedEmployee)
        {
            var item = await _context.HocViens
                                    .Where(x => x.HocVienId == hocVienId)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var hocVien_LopHoc = _context.HocVien_LopHocs
                                                .Where(x => x.HocVienId == hocVienId);
            _context.HocVien_LopHocs.RemoveRange(hocVien_LopHoc);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddToUnavailableClassAsync(List<Guid> lopHocIds, Guid hocVienId, string loggedEmployee)
        {
            var oldHocVien_LopHoc = _context.HocVien_LopHocs
                                                .Where(x => lopHocIds.Contains(x.LopHocId) && x.HocVienId == hocVienId);

            if (oldHocVien_LopHoc.Any())
                _context.HocVien_LopHocs.RemoveRange(oldHocVien_LopHoc);

            foreach (var item in lopHocIds)
            {
                var hocVien_LopHoc = new HocVien_LopHoc
                {
                    LopHocId = item,
                    HocVienId = hocVienId,
                    CreatedBy = loggedEmployee
                };

                _context.HocVien_LopHocs.Add(hocVien_LopHoc);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<HocVienViewModel>> GetAllHocVienAsync()
        {
            var hocViens = await _context.HocViens
                            .Include(x => x.QuanHe)
                            .Include(x => x.HocVien_LopHocs)
                            .ThenInclude(x => x.LopHoc)
                            .Include(x => x.HocVien_NgayHocs)
                            .Include(x => x.ChallengeResults)
                            .ThenInclude(x => x.ThuThach)
                            .AsNoTracking()
                            .ToListAsync();

            return hocViens
                .Select(hocvien => _entityConverter.ToHocVienViewModel(hocvien))
                .ToList();
        }

        public async Task<IList<Guid>> GetHocVienIdByQuanHeAsync(Guid quanHeId)
        {
            return await _context.HocViens
                .Where(x => x.QuanHeId == quanHeId)
                .Select(x => x.HocVienId)
                .ToListAsync();
        }

        private async Task<string> GenerateTrigramAsync(string fullName)
        {
            var name = Helpers.ToTiengVietKhongDau(fullName.Trim())
                    .Split(' ')
                    .ToList();

            string trigram = $"{name.First()[0]}";

            if (name[name.Count - 1].StartsWith('('))
            {
                trigram += name[name.Count - 2].Length >= 2 ?
                    $"{name[name.Count - 2].Substring(0, 2)}"
                    : $"{name[name.Count - 2][0]}{name[name.Count - 2][0]}";
            }
            else
            {
                trigram += name[name.Count - 1].Length >= 2 ?
                    $"{name[name.Count - 1].Substring(0, 2)}"
                    : $"{name[name.Count - 1][0]}{name[name.Count - 1][0]}";
            }

            var latestTrigram = await _context.HocViens
                    .Where(x => x.Trigram.ToLower().Contains(trigram.ToLower()))
                    .OrderByDescending(x => x)
                    .Select(x => x.Trigram)
                    .FirstOrDefaultAsync();

            if (latestTrigram == null)
                trigram += 1.ToString("D2");
            else
            {
                var currentNumber = int.Parse(latestTrigram.Substring(3, 2));
                currentNumber++;
                trigram += currentNumber.ToString("D2");
            }

            return trigram.ToUpper();
        }

        public async Task<List<HocVienViewModel>> GetHocVienKetQuaAsync(Guid lopHocId, int month, int year)
        {
            var hocViens = await _context.HocViens
                                        .Include(x => x.HocVien_LopHocs)
                                        .ThenInclude(x => x.LopHoc)
                                        .Include(x => x.HocVien_NgayHocs)
                                        .Include(x => x.ChallengeResults)
                                        .ThenInclude(x => x.ThuThach)
                                        .Where(x => !x.IsDisabled && x.HocVien_LopHocs.Any(m => m.LopHocId == lopHocId))
                                        .Where(x => x.HocVien_NgayHocs.Any(m => m.LopHocId == lopHocId && (m.NgayKetThuc == null || (m.NgayKetThuc.Value.Month >= month && m.NgayKetThuc.Value.Year == year) || m.NgayKetThuc.Value.Year > year)))
                                        .Where(x => x.HocVien_NgayHocs.Any(m => m.LopHocId == lopHocId && (m.NgayBatDau.Month <= month && m.NgayBatDau.Year == year) || m.NgayBatDau.Year < year))
                                        .AsNoTracking()
                                        .ToListAsync();

            return hocViens
                .Select(hocvien => _entityConverter.ToHocVienKetQuaViewModel(hocvien))
                .ToList();
        }
    }
}
