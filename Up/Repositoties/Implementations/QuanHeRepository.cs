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
    public class QuanHeRepository : BaseRepository, IQuanHeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public QuanHeRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateQuanHeAsync(string name, string loggedEmployee)
        {
            var quanHe = new QuanHe
            {
                Name = name,
                CreatedBy = loggedEmployee
            };

            _context.QuanHes.Add(quanHe);

            await _context.SaveChangesAsync();
            return quanHe.QuanHeId;
        }

        public async Task<bool> DeleteQuanHeAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.QuanHes
                                    .Where(x => x.QuanHeId == id)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<QuanHeViewModel>> GetQuanHeAsync()
        {
            var quanHes = await _context.QuanHes
                .Where(x => !x.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return quanHes.Select(quanHe => _entityConverter.ToQuanHeViewModel(quanHe)).ToList();
        }

        public async Task<QuanHeViewModel> GetQuanHeDetailAsync(Guid id)
        {
            var quanHe = await _context.QuanHes.FirstOrDefaultAsync(x => x.QuanHeId == id);
            return _entityConverter.ToQuanHeViewModel(quanHe);
        }

        public async Task<bool> UpdateQuanHeAsync(Guid id, string name, string loggedEmployee)
        {
            var item = await _context.QuanHes
                                    .Where(x => x.QuanHeId == id)
                                    .SingleOrDefaultAsync();

            item.Name = name;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
