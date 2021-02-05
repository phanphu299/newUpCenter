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
    public class GioHocRepository : BaseRepository, IGioHocRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public GioHocRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateGioHocAsync(CreateGioHocInputModel input, string loggedEmployee)
        {
            var gioHoc = _entityConverter.ToEntityGioHoc(input, loggedEmployee);

            _context.GioHocs.Add(gioHoc);

            await _context.SaveChangesAsync();
            return gioHoc.GioHocId;
        }

        public async Task<bool> DeleteGioHocAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.GioHocs
                                    .Where(x => x.GioHocId == id)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<GioHocViewModel>> GetGioHocAsync()
        {
            var gioHocs = await _context.GioHocs
                .Where(x => !x.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return gioHocs.Select(gioHoc => _entityConverter.ToGioHocViewModel(gioHoc)).ToList();
        }

        public async Task<GioHocViewModel> GetGioHocDetailAsync(Guid id)
        {
            var gioHoc = await _context.GioHocs.FirstOrDefaultAsync(x => x.GioHocId == id);
            return _entityConverter.ToGioHocViewModel(gioHoc);
        }

        public async Task<Guid> UpdateGioHocAsync(UpdateGioHocInputModel input, string loggedEmployee)
        {
            var item = await _context.GioHocs
                                    .Where(x => x.GioHocId == input.GioHocId)
                                    .SingleOrDefaultAsync();

            _entityConverter.MappingEntityGioHoc(input, item, loggedEmployee);

            await _context.SaveChangesAsync();
            return item.GioHocId;
        }
    }
}
