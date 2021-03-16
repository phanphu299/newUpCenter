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
    public class NgayLamViecRepository : BaseRepository, INgayLamViecRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public NgayLamViecRepository(
             ApplicationDbContext context,
             EntityConverter entityConverter,
             UserManager<IdentityUser> userManager)
             : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateNgayLamViecAsync(string name, string loggedEmployee)
        {
            var ngayLamViec = new NgayLamViec
            {
                Name = name,
                CreatedBy = loggedEmployee
            };

            _context.NgayLamViecs.Add(ngayLamViec);

            await _context.SaveChangesAsync();
            return ngayLamViec.NgayLamViecId;
        }

        public async Task<bool> DeleteNgayLamViecAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.NgayLamViecs
                                    .Where(x => x.NgayLamViecId == id)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<NgayLamViecViewModel>> GetNgayLamViecAsync()
        {
            var ngayLVs = await _context.NgayLamViecs
                .Where(x => !x.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return ngayLVs.Select(ngayLv => _entityConverter.ToNgayLamViecViewModel(ngayLv)).ToList();
        }

        public async Task<NgayLamViecViewModel> GetNgayLamViecDetailAsync(Guid id)
        {
            var ngayLv = await _context.NgayLamViecs.FirstOrDefaultAsync(x => x.NgayLamViecId == id);

            return _entityConverter.ToNgayLamViecViewModel(ngayLv);
        }

        public async Task<bool> UpdateNgayLamViecAsync(Guid id, string name, string loggedEmployee)
        {
            var item = await _context.NgayLamViecs
                                    .Where(x => x.NgayLamViecId == id)
                                    .SingleOrDefaultAsync();

            item.Name = name;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
