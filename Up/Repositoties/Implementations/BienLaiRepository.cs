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
    public class BienLaiRepository : BaseRepository, IBienLaiRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public BienLaiRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task CreateBienLaiAsync(CreateBienLaiInputModel input, string loggedEmployee)
        {
            try
            {
                var bienLai = _entityConverter.ToEntityBienLai(input, loggedEmployee);
                await _context.BienLais.AddAsync(bienLai);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetLastestMaBienLaiAsync()
        {
            var bienLai = await _context.BienLais
                                        .OrderByDescending(x => x.CreatedDate)
                                        .FirstOrDefaultAsync();
            return bienLai?.MaBienLai ?? string.Empty;
                                
        }

        public async Task<bool> IsExistMaBienLaiAsync(string maBienLai)
        {
            return await _context.BienLais
                .AnyAsync(x => x.MaBienLai == maBienLai);
        }
    }
}
