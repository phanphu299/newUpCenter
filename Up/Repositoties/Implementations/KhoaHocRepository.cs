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
    public class KhoaHocRepository : BaseRepository, IKhoaHocRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public KhoaHocRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateKhoaHocAsync(string name, string loggedEmployee)
        {
            var khoaHoc = new KhoaHoc
            {
                Name = name,
                CreatedBy = loggedEmployee
            };

            _context.KhoaHocs.Add(khoaHoc);

            await _context.SaveChangesAsync();

            return khoaHoc.KhoaHocId;
        }

        public async Task<bool> DeleteKhoaHocAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.KhoaHocs
                                    .FindAsync(id);

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<KhoaHocViewModel>> GetKhoaHocAsync()
        {
            var khoaHocs = await _context.KhoaHocs
                .Where(x => !x.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return khoaHocs.Select(khoaHoc => _entityConverter.ToKhoaHocViewModel(khoaHoc)).ToList();
        }

        public async Task<KhoaHocViewModel> GetKhoaHocDetailAsync(Guid id)
        {
            var khoaHoc = await _context.KhoaHocs.FirstOrDefaultAsync(x => x.KhoaHocId == id);
            return _entityConverter.ToKhoaHocViewModel(khoaHoc);
        }

        public async Task<IList<Guid>> GetLopHocByKhoaHocIdAsync(Guid id)
        {
            return await _context.LopHocs
                .Where(x => x.KhoaHocId == id)
                .Select(x => x.LopHocId)
                .ToListAsync();
        }

        public async Task<bool> UpdateKhoaHocAsync(Guid id, string name, string loggedEmployee)
        {
            var item = await _context.KhoaHocs
                                    .Where(x => x.KhoaHocId == id)
                                    .SingleOrDefaultAsync();

            item.Name = name;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
