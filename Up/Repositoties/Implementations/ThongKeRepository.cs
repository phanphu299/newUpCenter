using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Converters;
using Up.Data;
using Up.Data.Entities;
using Up.Enums;
using Up.Extensions;
using Up.Models;

namespace Up.Repositoties
{
    public class ThongKeRepository : BaseRepository, IThongKeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public ThongKeRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task<List<ThongKe_ChiPhiViewModel>> GetChiPhiAsync()
        {
            return await _context.ThongKe_ChiPhis
                .Where(x => x.NgayChiPhi.Year == DateTime.Now.Year && x.DaLuu)
                .OrderBy(x => x.NgayChiPhi)
                .AsNoTracking()
                .Select(g => new ThongKe_ChiPhiViewModel
                {
                    ChiPhi = g.ChiPhi,
                    NgayChiPhi = g.NgayChiPhi
                })
                .ToListAsync();
        }

        public async Task<List<ThongKe_DoanhThuHocPhiViewModel>> GetDoanhThuHocPhiAsync()
        {
            return await _context.ThongKe_DoanhThuHocPhis
                .Where(x => x.NgayDong.Year == DateTime.Now.Year && x.DaDong)
                .OrderBy(x => x.NgayDong)
                .AsNoTracking()
                .Select(g => new ThongKe_DoanhThuHocPhiViewModel
                {
                    HocPhi = g.HocPhi,
                    NgayDong = g.NgayDong
                })
                .ToListAsync();
        }

        public async Task<List<ThongKe_DoanhThuHocPhiViewModel>> GetDoanhThuHocPhiTronGoiAsync()
        {
            return await _context.HocPhiTronGois
                .Where(x => !x.IsRemoved && x.FromDate.Year == DateTime.Now.Year && !x.IsDisabled && (DateTime.Now.Year < x.ToDate.Year || (DateTime.Now.Year == x.ToDate.Year && DateTime.Now.Month <= x.ToDate.Month)))
                .OrderBy(x => x.FromDate)
                .AsNoTracking()
                .Select(g => new ThongKe_DoanhThuHocPhiViewModel
                {
                    HocPhi = g.HocPhi,
                    NgayDong = g.FromDate
                })
                .ToListAsync();
        }

        public async Task<List<HocVienOffHon3NgayViewModel>> GetHocVienOffHon3NgayAsync()
        {
            var model = await _context.HocVien_NgayHocs
                .Where(x => !x.HocVien.IsDisabled)
                .Where(x => x.NgayKetThuc == null)
                .Select(x => new
                {
                    HocVien = x.HocVien.FullName,
                    LopHoc_DiemDanh = x.LopHoc.LopHoc_DiemDanhs
                                        .Where(p => !p.LopHoc.IsCanceled && !p.LopHoc.IsDisabled && !p.LopHoc.IsGraduated)
                                        .OrderByDescending(p => p.NgayDiemDanh)
                                        .Where(p => !p.IsOff && p.IsDuocNghi != true && p.HocVienId == x.HocVienId && p.LopHocId == x.LopHocId)
                                        .Where(p => p.LopHoc.HocVien_LopHocs.Any(m => m.LopHocId == x.LopHocId && m.HocVienId == x.HocVienId))
                                        .Select(p => new
                                        {
                                            LopHoc = p.LopHoc.Name,
                                            NgayDiemDanh = p.NgayDiemDanh
                                        })
                                        .Take(1)
                                        .ToList()
                })
                .AsNoTracking()
                .ToListAsync();

            var list = model.Where(x => x.LopHoc_DiemDanh.Any(p => (DateTime.Now - p.NgayDiemDanh).TotalDays >= 8));

            return list.Select(x => new HocVienOffHon3NgayViewModel
            {
                TenLop = x.LopHoc_DiemDanh[0].LopHoc,
                NgayHocCuoi = x.LopHoc_DiemDanh[0].NgayDiemDanh.ToClearDate(),
                TenHocVien = x.HocVien
            })
            .ToList();
        }

        public async Task<List<NoViewModel>> GetNoAsync()
        {
            return await _context.HocVien_Nos
                .Where(x => x.NgayNo.Year == DateTime.Now.Year && !x.HocVien.IsDisabled)
                .OrderBy(x => x.NgayNo)
                .AsNoTracking()
                .Select(g => new NoViewModel
                {
                    TienNo = g.TienNo,
                    NgayNo_Date = g.NgayNo
                })
                .ToListAsync();
        }

        public async Task<List<ThongKeModel>> GetThongKeGiaoVienAsync(LoaiCheDoEnums loaiCheDo)
        {
            return await _context.ThongKeGiaoVienTheoThangs
                .Where(x => x.LoaiGiaoVien == (byte)loaiCheDo && x.Date.Year == DateTime.Now.Year)
                .OrderBy(x => x.Date)
                .Select(g => new ThongKeModel
                {
                    Data = g.SoLuong,
                    Date = g.Date
                })
                .ToListAsync();
        }

        public async Task<List<ThongKeModel>> GetThongKeHocVienAsync(LoaiHocVienEnums loaiHocVien)
        {
            return await _context.ThongKeHocVienTheoThangs
                                .Where(x => x.LoaiHocVien == (byte)loaiHocVien && x.Date.Year == DateTime.Now.Year)
                                .OrderBy(x => x.Date)
                                .Select(g => new ThongKeModel
                                {
                                    Data = g.SoLuong,
                                    Date = g.Date
                                })
                                .ToListAsync();
        }

        public async Task<double> GetTongChiPhiAsync()
        {
            return Math.Round(await _context.ThongKe_ChiPhis
                    .Where(x => x.DaLuu && x.NgayChiPhi.Month == DateTime.Now.Month && x.NgayChiPhi.Year == DateTime.Now.Year)
                    .AsNoTracking()
                    .Select(x => x.ChiPhi)
                    .SumAsync(), 0);
        }

        public async Task<double> GetTongDoanhThuAsync()
        {
            return Math.Round(await _context.ThongKe_DoanhThuHocPhis
                    .Where(x => x.DaDong && x.NgayDong.Month == DateTime.Now.Month && x.NgayDong.Year == DateTime.Now.Year)
                    .AsNoTracking()
                    .Select(x => x.HocPhi)
                    .SumAsync(), 0) 
                    +
                    Math.Round(await _context.HocPhiTronGois
                    .Where(x => !x.IsRemoved && !x.IsDisabled && x.FromDate.Month == DateTime.Now.Month && x.FromDate.Year == DateTime.Now.Year && (DateTime.Now.Year < x.ToDate.Year || (DateTime.Now.Year == x.ToDate.Year && DateTime.Now.Month <= x.ToDate.Month)))
                    .AsNoTracking()
                    .Select(x => x.HocPhi)
                    .SumAsync(), 0);
        }

        public async Task<int> GetTongGiaoVienAsync()
        {
            return await _context.GiaoViens
                    .Where(x => x.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && !x.IsDisabled)
                    .AsNoTracking()
                    .CountAsync();
        }

        public async Task<int> GetTongHocVienAsync()
        {
            return await _context.HocVien_LopHocs
                            .Where(x => !x.HocVien.IsDisabled)
                            .AsNoTracking()
                            .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == x.LopHoc.LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc > DateTime.Now)))
                            .Select(x => x.HocVienId)
                            .Distinct()
                            .CountAsync();
        }

        public async Task TinhTongGiaoVienTheoThangAsync(LoaiCheDoEnums loaiCheDo)
        {
            var thongke = await _context.ThongKeGiaoVienTheoThangs
                    .FirstOrDefaultAsync(x => x.LoaiGiaoVien == (byte)loaiCheDo && x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year);

            var soLuong = await _context.GiaoViens
                        .Where(x => x.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && x.NhanVien_ViTris.Any(m => m.CheDoId == loaiCheDo.ToId()) && !x.IsDisabled).CountAsync();

            if (thongke == null)
            {
                ThongKeGiaoVienTheoThang thongKeHocVien = new ThongKeGiaoVienTheoThang
                {
                    Date = DateTime.Now,
                    LoaiGiaoVien = (byte)loaiCheDo,
                    ThongKeGiaoVienTheoThangId = new Guid(),
                    SoLuong = soLuong
                };
                await _context.ThongKeGiaoVienTheoThangs.AddAsync(thongKeHocVien);
            }
            else
            {
                if (thongke.SoLuong != soLuong)
                {
                    thongke.SoLuong = soLuong;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task TinhTongHocVienQuocTeTheoThangAsync()
        {
            var thongke = await _context.ThongKeHocVienTheoThangs
                    .FirstOrDefaultAsync(x => x.LoaiHocVien == (byte)LoaiHocVienEnums.QuocTe && x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year);

            var soLuong = await _context.HocVien_LopHocs
                    .Where(x => x.LopHoc.KhoaHocId != LoaiKhoaHocEnums.GiaoTiep.ToId() && x.LopHoc.KhoaHocId != LoaiKhoaHocEnums.ThieuNhi.ToId())
                    .Where(x => !x.HocVien.IsDisabled)
                    .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == x.LopHoc.LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc > DateTime.Now)))
                    .Select(x => x.HocVienId)
                    .Distinct()
                    .CountAsync();

            if (thongke == null)
            {
                ThongKeHocVienTheoThang thongKeHocVien = new ThongKeHocVienTheoThang
                {
                    Date = DateTime.Now,
                    LoaiHocVien = (byte)LoaiHocVienEnums.QuocTe,
                    ThongKeHocVienTheoThangId = new Guid(),
                    SoLuong = soLuong
                };
                await _context.ThongKeHocVienTheoThangs.AddAsync(thongKeHocVien);
            }
            else
            {
                if (thongke.SoLuong != soLuong)
                {
                    thongke.SoLuong = soLuong;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task TinhTongHocVienTheoThangAsync(LoaiHocVienEnums loaiHocVien, LoaiKhoaHocEnums loaiKhoaHoc)
        {
            var thongke = await _context.ThongKeHocVienTheoThangs
                    .FirstOrDefaultAsync(x => x.LoaiHocVien == (byte)loaiHocVien && x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year);

            var soLuong = await _context.HocVien_LopHocs
                        .Where(x => x.LopHoc.KhoaHocId == loaiKhoaHoc.ToId())
                        .Where(x => !x.HocVien.IsDisabled)
                        .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == x.LopHoc.LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc > DateTime.Now)))
                        .Select(x => x.HocVienId)
                        .Distinct()
                        .CountAsync();
            if (thongke == null)
            {
                var thongKeHocVien = new ThongKeHocVienTheoThang
                {
                    Date = DateTime.Now,
                    LoaiHocVien = (byte)LoaiHocVienEnums.ThieuNhi,
                    ThongKeHocVienTheoThangId = new Guid(),
                    SoLuong = soLuong
                };
                await _context.ThongKeHocVienTheoThangs.AddAsync(thongKeHocVien);
            }
            else
            {
                if (thongke.SoLuong != soLuong)
                {
                    thongke.SoLuong = soLuong;
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
