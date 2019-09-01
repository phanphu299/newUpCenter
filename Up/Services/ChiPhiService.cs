
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
            var items = _context.ThongKe_ChiPhis.Where(x => x.NgayChiPhi.Month == month && x.NgayChiPhi.Year == year);
            
            if(items.Any())
            {
                return new TinhChiPhiViewModel
                {
                    ChiPhiList = await _context.ThongKe_ChiPhis
                                        .Where(x => x.NgayChiPhi.Month == month && x.NgayChiPhi.Year == year)
                                        .Select(x => new ChiPhiModel
                                        {
                                            Bonus = x.Bonus,
                                            Minus = x.Minus,
                                            ChiPhiMoi = x.ChiPhi,
                                            NhanVienId = x.NhanVienId,
                                            ChiPhiCoDinhId = x.ChiPhiCoDinhId,
                                            SoGioKem = x.SoGioKem,
                                            SoGioDay = x.SoGioDay,
                                            SoHocVien = x.SoHocVien,
                                            DaLuu = x.DaLuu,
                                            Name = x.NhanVienId != null ? x.NhanVien.Name : x.ChiPhiCoDinh.Name,
                                            Salary_Expense = x.NhanVienId != null ? x.NhanVien.BasicSalary : x.ChiPhiCoDinh.Gia,
                                            TeachingRate = x.NhanVienId != null ? x.NhanVien.TeachingRate : 0,
                                            TutoringRate = x.NhanVienId != null ? x.NhanVien.TutoringRate : 0,
                                            MucHoaHong = x.NhanVienId != null ? x.NhanVien.MucHoaHong : 0,
                                            LoaiChiPhi = x.NhanVienId == null ? 3 : (x.NhanVien.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && x.NhanVien.NhanVien_ViTris.Count > 1) ? 4 : (x.NhanVien.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && x.NhanVien.NhanVien_ViTris.Count == 1) ? 1 : 2
                                        })
                                        .ToListAsync()
                };
            }
            else
            {
                var giaoVien = await _context
                .GiaoViens.Where(x => x.IsDisabled == false)
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
                    ChiPhiMoi = x.BasicSalary,
                    NhanVienId = x.GiaoVienId
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
                        ChiPhiMoi = x.Gia,
                        ChiPhiCoDinhId = x.ChiPhiCoDinhId
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
}
