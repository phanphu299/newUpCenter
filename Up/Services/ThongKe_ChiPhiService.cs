
namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Data.Entities;
    using Up.Models;

    public class ThongKe_ChiPhiService : IThongKe_ChiPhiService
    {
        private readonly ApplicationDbContext _context;

        public ThongKe_ChiPhiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ThemThongKe_ChiPhiAsync(ThongKe_ChiPhiViewModel[] Model, DateTime NgayChiPhi, string LoggedEmployee)
        {
            var items = _context.ThongKe_ChiPhis
                .Where(x => x.NgayChiPhi.Month == NgayChiPhi.Month && x.NgayChiPhi.Year == NgayChiPhi.Year);

            try
            {
                _context.RemoveRange(items);

                foreach (var item in Model)
                {
                    ThongKe_ChiPhi thongKe = new ThongKe_ChiPhi
                    {
                        ThongKe_ChiPhiId = new Guid(),
                        NgayChiPhi = NgayChiPhi,
                        CreatedBy = LoggedEmployee,
                        CreatedDate = DateTime.Now,
                        ChiPhi = item.ChiPhiMoi,
                        Bonus = item.Bonus,
                        Minus = item.Minus,
                        SoGioDay = item.SoGioDay,
                        SoGioKem = item.SoGioKem,
                        ChiPhiCoDinhId = item.ChiPhiCoDinhId,
                        NhanVienId = item.NhanVienId
                    };
                    await _context.ThongKe_ChiPhis.AddAsync(thongKe);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exeption)
            {
                throw new Exception("Lỗi khi lưu chi phí : " + exeption.Message);
            }
        }
    }
}
