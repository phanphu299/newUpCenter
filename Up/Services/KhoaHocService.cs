using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Data;
using Up.Models;

namespace Up.Services
{
    public class KhoaHocService : IKhoaHocService
    {
        private readonly ApplicationDbContext _context;

        public KhoaHocService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<KhoaHocViewModel>> GetKhoaHocAsync()
        {
            return await _context.KhoaHocs
                .Where(x => x.IsDisabled == false)
                .Select(x => new KhoaHocViewModel {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    KhoaHocId = x.KhoaHocId,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate
                })
                .ToListAsync();
        }
    }
}
