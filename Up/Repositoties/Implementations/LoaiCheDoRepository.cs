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
    public class LoaiCheDoRepository : BaseRepository, ILoaiCheDoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public LoaiCheDoRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateLoaiCheDoAsync(string name, string loggedEmployee)
        {
            var loaiCheDo = new LoaiCheDo 
            {
                Name = name,
                CreatedBy = loggedEmployee
            };
            _context.LoaiCheDos.Add(loaiCheDo);

            await _context.SaveChangesAsync();
            return loaiCheDo.LoaiCheDoId;
        }

        public async Task<bool> DeleteLoaiCheDoAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.LoaiCheDos
                                    .Where(x => x.LoaiCheDoId == id)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<LoaiCheDoViewModel>> GetLoaiCheDoAsync()
        {
            var loaiCDs = await _context.LoaiCheDos
                .Where(x => !x.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return loaiCDs.Select(cheDo => _entityConverter.ToLoaiCheDoViewModel(cheDo)).ToList();
        }

        public async Task<LoaiCheDoViewModel> GetLoaiCheDoDetailAsync(Guid id)
        {
            var cheDo = await _context.LoaiCheDos.FirstOrDefaultAsync(x => x.LoaiCheDoId == id);
            return _entityConverter.ToLoaiCheDoViewModel(cheDo);
        }

        public async Task<bool> UpdateLoaiCheDoAsync(Guid id, string name, string loggedEmployee)
        {
            var item = await _context.LoaiCheDos
                                    .Where(x => x.LoaiCheDoId == id)
                                    .SingleOrDefaultAsync();

            item.Name = name;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
