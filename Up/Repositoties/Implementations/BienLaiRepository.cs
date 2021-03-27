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
        private readonly ILopHocRepository _lopHocRepository;

        public BienLaiRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager,
           ILopHocRepository lopHocRepository)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
            _lopHocRepository = lopHocRepository;
        }

        public async Task CreateBienLaiAsync(CreateBienLaiInputModel input, string loggedEmployee)
        {
            var bienLai = _entityConverter.ToEntityBienLai(input, loggedEmployee);
            await _context.BienLais.AddAsync(bienLai);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBienLaiAsync(Guid id, string loggedEmployee)
        {
            var bienLai = await _context.BienLais.FirstOrDefaultAsync(x => x.BienLaiId == id);

            bienLai.IsDisabled = true;
            bienLai.UpdatedBy = loggedEmployee;
            bienLai.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BienLaiViewModel>> GetBienLaiAsync()
        {
            var bienLais = await _context.BienLais
                .Include(x => x.HocVien)
                .Where(x => !x.IsDisabled)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            var lopHocIds = bienLais
                .Where(x => x.LopHocId != Guid.Empty)
                .Select(x => x.LopHocId.Value)
                .ToList();

            var lopHocs = await _lopHocRepository.GetLopHocByIdsAsync(lopHocIds);

            var result = bienLais.Select(bienLai =>
            {
                var lopHoc = lopHocs.FirstOrDefault(x => x.LopHocId == bienLai.LopHocId);
                return _entityConverter.ToBienLaiViewModel(bienLai, lopHoc);
            })
            .ToList();

            return result; 
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
