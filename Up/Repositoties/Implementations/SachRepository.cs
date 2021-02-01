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
    public class SachRepository : BaseRepository, ISachRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public SachRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateSachAsync(CreateSachInputModel input, string loggedEmployee)
        {
            var sach = _entityConverter.ToEntitySach(input, loggedEmployee);
            _context.Sachs.Add(sach);

            await _context.SaveChangesAsync();
            return sach.SachId;
        }

        public async Task<bool> DeleteSachAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.Sachs
                                    .Where(x => x.SachId == id)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<IList<SachViewModel>> GetSachAsync()
        {
            var sachs = await _context.Sachs
                //.Where(x => !x.IsDisabled)
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .ToListAsync();

            return sachs.Select(sach => _entityConverter.ToSachViewModel(sach)).ToList();
        }

        public async Task<SachViewModel> GetSachDetailAsync(Guid id)
        {
            var sach = await _context.Sachs.FirstOrDefaultAsync(x => x.SachId == id);

            return _entityConverter.ToSachViewModel(sach);
        }

        public async Task<bool> UpdateSachAsync(UpdateSachInputModel input, string loggedEmployee)
        {
            var item = await _context.Sachs
                                    .Where(x => x.SachId == input.SachId)
                                    .SingleOrDefaultAsync();

            _entityConverter.MappingEntitySach(input, item, loggedEmployee);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
