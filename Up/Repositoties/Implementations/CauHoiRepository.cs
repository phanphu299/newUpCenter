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
    public class CauHoiRepository : BaseRepository, ICauHoiRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;

        public CauHoiRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateCauHoiAsync(CreateCauHoiInputModel input, string loggedEmployee)
        {
            var cauHoi = _entityConverter.ToEntityCauHoi(input, loggedEmployee);
            await _context.CauHois.AddAsync(cauHoi);

            await _context.SaveChangesAsync();
            return cauHoi.CauHoiId;
        }

        public async Task<bool> DeleteCauHoiAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.CauHois
                                    .FindAsync(id);

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<CauHoiViewModel>> GetCauHoiAsync()
        {
            var cauHois = await _context.CauHois
                                        .Include(x => x.ThuThach)
                                        .Include(x => x.DapAns)
                                        .Where(x => !x.IsDisabled)
                                        .ToListAsync();

            return cauHois.Select(cauHoi => _entityConverter.ToCauHoiViewModel(cauHoi)).ToList();
        }

        public async Task<List<CauHoiViewModel>> GetCauHoiAsync(IList<Guid> ids)
        {
            var cauHois = await _context.CauHois
                                        .Include(x => x.ThuThach)
                                        .Include(x => x.DapAns)
                                        .Where(x => !x.IsDisabled && ids.Contains(x.CauHoiId))
                                        .ToListAsync();

            return cauHois.Select(cauHoi => _entityConverter.ToCauHoiViewModel(cauHoi)).ToList();
        }

        public async Task<List<CauHoiViewModel>> GetCauHoiAsync(Guid thuThachId, int stt)
        {
            var cauHois = await _context.CauHois
                                        .Include(x => x.ThuThach)
                                        .Include(x => x.DapAns)
                                        .Where(x => !x.IsDisabled && x.ThuThachId == thuThachId && x.STT == stt)
                                        .ToListAsync();

            return cauHois.Select(cauHoi => _entityConverter.ToCauHoiViewModel(cauHoi)).ToList();
        }

        public async Task<CauHoiViewModel> GetCauHoiDetailAsync(Guid id)
        {
            var cauHoi = await _context.CauHois
                                        .Include(x => x.ThuThach)
                                        .Include(x => x.DapAns)
                                        .FirstOrDefaultAsync(x => x.CauHoiId == id);

            return _entityConverter.ToCauHoiViewModel(cauHoi);
        }

        public async Task<List<Guid>> ImportCauHoiAsync(ImportCauHoiInputModel input, string loggedEmployee)
        {
            var cauHois = _entityConverter.ToEntityCauHoiList(input, loggedEmployee);
            await _context.CauHois.AddRangeAsync(cauHois);

            await _context.SaveChangesAsync();
            return cauHois.Select(x => x.CauHoiId).ToList();
        }
    }
}
