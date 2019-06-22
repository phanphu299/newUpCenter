
namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Data.Entities;

    public class ThongKe_ChiPhiService : IThongKe_ChiPhiService
    {
        private readonly ApplicationDbContext _context;

        public ThongKe_ChiPhiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ThemThongKe_ChiPhiAsync(double ChiPhi, DateTime NgayChiPhi, string LoggedEmployee)
        {
            var item = await _context.ThongKe_ChiPhis
                .Where(x => x.NgayChiPhi.Month == NgayChiPhi.Month && x.NgayChiPhi.Year == NgayChiPhi.Year)
                .SingleOrDefaultAsync();

            try
            {
                if (item == null)
                {
                    ThongKe_ChiPhi thongKe = new ThongKe_ChiPhi
                    {
                        ThongKe_ChiPhiId = new Guid(),
                        NgayChiPhi = NgayChiPhi,
                        CreatedBy = LoggedEmployee,
                        CreatedDate = DateTime.Now,
                        ChiPhi = ChiPhi
                    };
                    _context.ThongKe_ChiPhis.Add(thongKe);
                }
                else
                {
                    item.ChiPhi = ChiPhi;
                    item.UpdatedBy = LoggedEmployee;
                    item.UpdatedDate = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exeption)
            {
                throw new Exception("Lỗi khi lưu doanh thu : " + exeption.Message);
            }
        }
    }
}
