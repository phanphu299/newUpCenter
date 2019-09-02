
namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Enums;
    using Up.Models;

    public class ChiPhiService : IChiPhiService
    {
        private readonly ApplicationDbContext _context;

        public ChiPhiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TinhChiPhiViewModel> TinhChiPhiAsync(int month, int year)
        {
            var giaoVien = await _context
            .GiaoViens.Where(x => x.IsDisabled == false && x.NgayBatDau.Month <= month && x.NgayBatDau.Year <= year)
            .OrderBy(x => x.NhanVien_ViTris.OrderBy(m => m.ViTri.Order).First().ViTri.Order)
            .Select(x => new ChiPhiModel
            {
                Name = x.Name,
                Salary_Expense = x.BasicSalary,
                TeachingRate = x.TeachingRate,
                TutoringRate = x.TutoringRate,
                MucHoaHong = x.MucHoaHong,
                Bonus = 0,
                Minus = 0,
                LoaiChiPhi = (x.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && x.NhanVien_ViTris.Count > 1) ? 4 : (x.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && x.NhanVien_ViTris.Count == 1) ? 1 : 2,
                ChiPhiMoi = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).ChiPhi : x.BasicSalary,
                NhanVienId = x.GiaoVienId,
                SoGioDay = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).SoGioDay : 0,
                SoGioKem = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).SoGioKem : 0,
                SoHocVien = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).SoHocVien : 0,
                DaLuu = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.NhanVienId == x.GiaoVienId).DaLuu : false,
            })
            .ToListAsync();

            var chiPhi = await _context.ChiPhiCoDinhs
                .Where(x => x.IsDisabled == false)
                .Select(x => new ChiPhiModel
                {
                    Name = x.Name,
                    Salary_Expense = x.Gia,
                    Bonus = 0,
                    Minus = 0,
                    LoaiChiPhi = 3,
                    ChiPhiMoi = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.ChiPhiCoDinhId == x.ChiPhiCoDinhId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.ChiPhiCoDinhId == x.ChiPhiCoDinhId).ChiPhi : x.Gia,
                    ChiPhiCoDinhId = x.ChiPhiCoDinhId,
                    DaLuu = x.ThongKe_ChiPhis.Any(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.ChiPhiCoDinhId == x.ChiPhiCoDinhId) ? x.ThongKe_ChiPhis.FirstOrDefault(m => m.NgayChiPhi.Month == month && m.NgayChiPhi.Year == year && m.ChiPhiCoDinhId == x.ChiPhiCoDinhId).DaLuu : false,
                })
                .ToListAsync();

            List<ChiPhiModel> model = new List<ChiPhiModel>();
            model.AddRange(giaoVien);
            model.AddRange(chiPhi);

            return new TinhChiPhiViewModel
            {
                ChiPhiList = model
            };

        }
    }
}
