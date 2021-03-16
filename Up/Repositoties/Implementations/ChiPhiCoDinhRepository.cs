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
    public class ChiPhiCoDinhRepository : BaseRepository, IChiPhiCoDinhRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public ChiPhiCoDinhRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateChiPhiCoDinhAsync(double gia, string name, string loggedEmployee)
        {
            var chiPhi = new ChiPhiCoDinh
            {
                Gia = gia,
                Name = name,
                CreatedBy = loggedEmployee
            };
            _context.ChiPhiCoDinhs.Add(chiPhi);

            await _context.SaveChangesAsync();
            return chiPhi.ChiPhiCoDinhId;
        }

        public async Task<bool> DeleteChiPhiCoDinhAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.ChiPhiCoDinhs
                                    .Where(x => x.ChiPhiCoDinhId == id)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<ChiPhiCoDinhViewModel>> GetChiPhiCoDinhAsync()
        {
            var chiPhis = await _context.ChiPhiCoDinhs
                .Where(x => !x.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return chiPhis.Select(chiPhi => _entityConverter.ToChiPhiCoDinhViewModel(chiPhi)).ToList();
        }

        public async Task<ChiPhiCoDinhViewModel> GetChiPhiCoDinhDetailAsync(Guid id)
        {
            var chiPhi = await _context.ChiPhiCoDinhs.FirstOrDefaultAsync(x => x.ChiPhiCoDinhId == id);

            return _entityConverter.ToChiPhiCoDinhViewModel(chiPhi);
        }

        public async Task<Guid> UpdateChiPhiCoDinhAsync(Guid id, double gia, string name, string loggedEmployee)
        {
            var item = await _context.ChiPhiCoDinhs
                                    .Where(x => x.ChiPhiCoDinhId == id)
                                    .SingleOrDefaultAsync();

            item.Gia = gia;
            item.Name = name;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return item.ChiPhiCoDinhId;
        }
    }
}
