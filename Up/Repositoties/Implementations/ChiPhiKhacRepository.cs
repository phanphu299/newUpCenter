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
    public class ChiPhiKhacRepository : BaseRepository, IChiPhiKhacRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public ChiPhiKhacRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<Guid> CreateChiPhiKhacAsync(CreateChiPhiKhacInputModel input, string loggedEmployee)
        {
            var chiPhi = _entityConverter.ToEntityChiPhiKhac(input, loggedEmployee);
            _context.ChiPhiKhacs.Add(chiPhi);

            var thongKe = new ThongKe_ChiPhi
            {
                NgayChiPhi = input.NgayChiPhi,
                CreatedBy = loggedEmployee,
                ChiPhi = input.Gia,
                ChiPhiKhacId = chiPhi.ChiPhiKhacId,
                DaLuu = true
            };
            _context.ThongKe_ChiPhis.Add(thongKe);

            await _context.SaveChangesAsync();
            return chiPhi.ChiPhiKhacId;
        }

        public async Task<bool> DeleteChiPhiKhacAsync(Guid id, string loggedEmployee)
        {
            var item = await _context.ChiPhiKhacs
                                    .Where(x => x.ChiPhiKhacId == id)
                                    .SingleOrDefaultAsync();

            item.IsDisabled = true;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var thongKe_ChiPhi = await _context.ThongKe_ChiPhis.FirstOrDefaultAsync(x => x.ChiPhiKhacId == item.ChiPhiKhacId);
            _context.ThongKe_ChiPhis.Remove(thongKe_ChiPhi);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ChiPhiKhacViewModel>> GetChiPhiKhacAsync()
        {
            var chiPhis = await _context.ChiPhiKhacs
                .Where(x => !x.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return chiPhis.Select(chiPhi => _entityConverter.ToChiPhiKhacViewModel(chiPhi)).ToList();
        }

        public async Task<ChiPhiKhacViewModel> GetChiPhiKhacDetailAsync(Guid id)
        {
            var chiPhi = await _context.ChiPhiKhacs.FirstOrDefaultAsync(x => x.ChiPhiKhacId == id);
            return _entityConverter.ToChiPhiKhacViewModel(chiPhi);
        }

        public async Task<Guid> UpdateChiPhiKhacAsync(UpdateChiPhiKhacInputModel input, string loggedEmployee)
        {
            var item = await _context.ChiPhiKhacs
                                    .Where(x => x.ChiPhiKhacId == input.ChiPhiKhacId)
                                    .SingleOrDefaultAsync();

            _entityConverter.MappingEntityChiPhiKhac(input, item, loggedEmployee);

            var thongKe_ChiPhi = await _context.ThongKe_ChiPhis.FirstOrDefaultAsync(x => x.ChiPhiKhacId == item.ChiPhiKhacId);
            thongKe_ChiPhi.NgayChiPhi = input.NgayChiPhi;
            thongKe_ChiPhi.ChiPhi = input.Gia;

            await _context.SaveChangesAsync();
            return item.ChiPhiKhacId;
        }
    }
}
