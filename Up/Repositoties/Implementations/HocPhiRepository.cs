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

        public async Task<Guid> UpdateHocPhiAsync(UpdateHocPhiInputModel input, string loggedEmployee)
        {
            var item = await _context.HocPhis
                                    .Where(x => x.HocPhiId == input.HocPhiId)
                                    .SingleOrDefaultAsync();

            _entityConverter.MappingEntityHocPhi(input, item, loggedEmployee);
            await _context.SaveChangesAsync();

            return item.HocPhiId;
        }
    }
}
