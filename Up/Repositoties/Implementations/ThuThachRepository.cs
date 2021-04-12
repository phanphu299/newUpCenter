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
    public class ThuThachRepository : BaseRepository, IThuThachRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;

        public ThuThachRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateThuThachAsync(CreateThuThachInputModel input, string loggedEmployee)
        {
            var thuThach = _entityConverter.ToEntityThuThach(input, loggedEmployee);
            await _context.ThuThachs.AddAsync(thuThach);

            await _context.SaveChangesAsync();
            return thuThach.ThuThachId;
        }

        public async Task<bool> DeleteThuThachAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.ThuThachs
                                    .FindAsync(id);

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<ThuThachViewModel>> GetThuThachAsync()
        {
            var thuThachs = await _context.ThuThachs
                                        .Include(x => x.KhoaHoc)
                                        .Where(x => !x.IsDisabled)
                                        .ToListAsync();

            return thuThachs.Select(thuThach => _entityConverter.ToThuThachViewModel(thuThach)).ToList();
        }

        public async Task<List<ThuThachViewModel>> GetThuThachByKhoaHocIdsAsync(IList<Guid> khoaHocIds)
        {
            var thuThachs = await _context.ThuThachs
                                        .Include(x => x.KhoaHoc)
                                        .Where(x => !x.IsDisabled && khoaHocIds.Contains(x.KhoaHocId))
                                        .ToListAsync();

            return thuThachs.Select(thuThach => _entityConverter.ToThuThachViewModel(thuThach)).ToList();
        }

        public async Task<ThuThachViewModel> GetThuThachDetailAsync(Guid id)
        {
            var thuThach = await _context.ThuThachs
                                        .Include(x => x.KhoaHoc)
                                        .FirstOrDefaultAsync(x => x.ThuThachId == id);

            return _entityConverter.ToThuThachViewModel(thuThach);
        }

        public async Task<Guid> UpdateThuThachAsync(UpdateThuThachInputModel input, string loggedEmployee)
        {
            var item = await _context.ThuThachs
                                    .Where(x => x.ThuThachId == input.ThuThachId)
                                    .SingleOrDefaultAsync();

            _entityConverter.MappingEntityThuThach(input, item, loggedEmployee);

            await _context.SaveChangesAsync();
            return item.ThuThachId;
        }
    }
}
