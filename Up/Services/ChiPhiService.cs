
namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Models;

    public class ChiPhiService : IChiPhiService
    {
        private readonly ApplicationDbContext _context;

        public ChiPhiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TinhChiPhiViewModel> TinhChiPhiAsync()
        {
            var giaoVien = await _context
                .GiaoViens.Where(x => x.IsDisabled == false)
                .Select(x => new ChiPhiModel
                {
                    Name = x.Name,
                    Salary_Expense = x.BasicSalary,
                    TeachingRate = x.TeachingRate,
                    TutoringRate = x.TutoringRate,
                    Bonus = 0,
                    LoaiChiPhi = 1,
                    ChiPhiMoi = x.BasicSalary
                })
                .ToListAsync();

            var nhanVien = await _context.NhanVienKhacs
                .Where(x => x.IsDisabled == false)
                .Select(x => new ChiPhiModel
                {
                    Salary_Expense = x.BasicSalary,
                    Name = x.Name,
                    LoaiChiPhi = 2,
                    Bonus = 0,
                    ChiPhiMoi = x.BasicSalary
                })
                .ToListAsync();

            var chiPhi = await _context.ChiPhiCoDinhs
                .Where(x => x.IsDisabled == false)
                .Select(x => new ChiPhiModel
                {
                    Name = x.Name,
                    Salary_Expense = x.Gia,
                    Bonus = 0,
                    LoaiChiPhi = 3,
                    ChiPhiMoi = x.Gia
                })
                .ToListAsync();

            List<ChiPhiModel> model = new List<ChiPhiModel>();
            model.AddRange(giaoVien);
            model.AddRange(nhanVien);
            model.AddRange(chiPhi);

            return new TinhChiPhiViewModel
            {
                ChiPhiList = model
            };
        }
    }
}
