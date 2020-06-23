namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Data.Entities;
    using Up.Enums;
    using Up.Models;

    public class ThongKeService : IThongKeService
    {
        private readonly ApplicationDbContext _context;

        public ThongKeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ThongKeModel>> GetHocVienGiaoTiepAsync()
        {
            try
            {
                //tinh tong hoc vien theo tháng
                var thongke = await _context.ThongKeHocVienTheoThangs
                    .FirstOrDefaultAsync(x => x.LoaiHocVien == (byte)LoaiHocVienEnums.GiaoTiep && x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year);
                if (thongke == null)
                {
                    ThongKeHocVienTheoThang thongKeHocVien = new ThongKeHocVienTheoThang
                    {
                        Date = DateTime.Now,
                        LoaiHocVien = (byte)LoaiHocVienEnums.GiaoTiep,
                        ThongKeHocVienTheoThangId = new Guid(),
                        SoLuong = await _context.HocVien_LopHocs
                            .Where(x => x.LopHoc.KhoaHocId == LoaiKhoaHocEnums.GiaoTiep.ToId())
                            .Where(x => x.HocVien.IsDisabled == false)
                            .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == x.LopHoc.LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc > DateTime.Now)))
                            .Select(x => x.HocVienId)
                            .Distinct()
                            .CountAsync()
                    };
                    await _context.ThongKeHocVienTheoThangs.AddAsync(thongKeHocVien);
                }
                else
                {
                    var soLuong = await _context.HocVien_LopHocs
                            .Where(x => x.LopHoc.KhoaHocId == LoaiKhoaHocEnums.GiaoTiep.ToId())
                            .Where(x => x.HocVien.IsDisabled == false)
                            .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == x.LopHoc.LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc > DateTime.Now)))
                            .Select(x => x.HocVienId)
                            .Distinct()
                            .CountAsync();
                    if (thongke.SoLuong != soLuong)
                    {
                        thongke.SoLuong = soLuong;
                    }
                }
                await _context.SaveChangesAsync();

                var giaoTiep = await _context.ThongKeHocVienTheoThangs
                .Where(x => x.LoaiHocVien == (byte)LoaiHocVienEnums.GiaoTiep && x.Date.Year == DateTime.Now.Year)
                .OrderBy(x => x.Date)

                .Select(g => new ThongKeModel
                {
                    Data = g.SoLuong,
                    Date = g.Date
                })
                .ToListAsync();

                return giaoTiep;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<ThongKeModel>> GetHocVienThieuNhiAsync()
        {
            try
            {
                //tinh tong hoc vien theo tháng
                var thongke = await _context.ThongKeHocVienTheoThangs
                    .FirstOrDefaultAsync(x => x.LoaiHocVien == (byte)LoaiHocVienEnums.ThieuNhi && x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year);
                if (thongke == null)
                {
                    ThongKeHocVienTheoThang thongKeHocVien = new ThongKeHocVienTheoThang
                    {
                        Date = DateTime.Now,
                        LoaiHocVien = (byte)LoaiHocVienEnums.ThieuNhi,
                        ThongKeHocVienTheoThangId = new Guid(),
                        SoLuong = await _context.HocVien_LopHocs
                            .Where(x => x.LopHoc.KhoaHocId == LoaiKhoaHocEnums.ThieuNhi.ToId())
                            .Where(x => x.HocVien.IsDisabled == false)
                            .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == x.LopHoc.LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc > DateTime.Now)))
                            .Select(x => x.HocVienId)
                            .Distinct()
                            .CountAsync()
                    };
                    await _context.ThongKeHocVienTheoThangs.AddAsync(thongKeHocVien);
                }
                else
                {
                    var soLuong = await _context.HocVien_LopHocs
                            .Where(x => x.LopHoc.KhoaHocId == LoaiKhoaHocEnums.ThieuNhi.ToId())
                            .Where(x => x.HocVien.IsDisabled == false)
                            .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == x.LopHoc.LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc > DateTime.Now)))
                            .Select(x => x.HocVienId)
                            .Distinct()
                            .CountAsync();
                    if (thongke.SoLuong != soLuong)
                    {
                        thongke.SoLuong = soLuong;
                    }
                }
                await _context.SaveChangesAsync();

                var thieuNhi = await _context.ThongKeHocVienTheoThangs
                .Where(x => x.LoaiHocVien == (byte)LoaiHocVienEnums.ThieuNhi && x.Date.Year == DateTime.Now.Year)
                .OrderBy(x => x.Date)

                .Select(g => new ThongKeModel
                {
                    Data = g.SoLuong,
                    Date = g.Date
                })
                .ToListAsync();

                return thieuNhi;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<ThongKeModel>> GetHocVienCCQuocTeAsync()
        {
            try
            {
                var thongke = await _context.ThongKeHocVienTheoThangs
                    .FirstOrDefaultAsync(x => x.LoaiHocVien == (byte)LoaiHocVienEnums.QuocTe && x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year);
                if (thongke == null)
                {
                    ThongKeHocVienTheoThang thongKeHocVien = new ThongKeHocVienTheoThang
                    {
                        Date = DateTime.Now,
                        LoaiHocVien = (byte)LoaiHocVienEnums.QuocTe,
                        ThongKeHocVienTheoThangId = new Guid(),
                        SoLuong = await _context.HocVien_LopHocs
                            .Where(x => x.LopHoc.KhoaHocId != LoaiKhoaHocEnums.GiaoTiep.ToId() && x.LopHoc.KhoaHocId != LoaiKhoaHocEnums.ThieuNhi.ToId())
                            .Where(x => x.HocVien.IsDisabled == false)
                            .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == x.LopHoc.LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc > DateTime.Now)))
                            .Select(x => x.HocVienId)
                            .Distinct()
                            .CountAsync()
                    };
                    await _context.ThongKeHocVienTheoThangs.AddAsync(thongKeHocVien);
                }
                else
                {
                    var soLuong = await _context.HocVien_LopHocs
                        .Where(x => x.LopHoc.KhoaHocId != LoaiKhoaHocEnums.GiaoTiep.ToId() && x.LopHoc.KhoaHocId != LoaiKhoaHocEnums.ThieuNhi.ToId())
                        .Where(x => x.HocVien.IsDisabled == false)
                        .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == x.LopHoc.LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc > DateTime.Now)))
                        .Select(x => x.HocVienId)
                            .Distinct()
                            .CountAsync();
                    if (thongke.SoLuong != soLuong)
                    {
                        thongke.SoLuong = soLuong;
                    }
                }
                await _context.SaveChangesAsync();

                var quocTe = await _context.ThongKeHocVienTheoThangs
                .Where(x => x.LoaiHocVien == (byte)LoaiHocVienEnums.QuocTe && x.Date.Year == DateTime.Now.Year)
                .OrderBy(x => x.Date)

                .Select(g => new ThongKeModel
                {
                    Data = g.SoLuong,
                    Date = g.Date
                })
                .ToListAsync();

                return quocTe;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<ThongKeModel>> GetGiaoVienFullTimeAsync()
        {
            try
            {
                //tinh tong giao vien theo tháng
                var thongke = await _context.ThongKeGiaoVienTheoThangs
                    .FirstOrDefaultAsync(x => x.LoaiGiaoVien == (byte)LoaiGiaoVienEnums.FullTime && x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year);
                if (thongke == null)
                {
                    ThongKeGiaoVienTheoThang thongKeHocVien = new ThongKeGiaoVienTheoThang
                    {
                        Date = DateTime.Now,
                        LoaiGiaoVien = (byte)LoaiGiaoVienEnums.FullTime,
                        ThongKeGiaoVienTheoThangId = new Guid(),
                        SoLuong = await _context.GiaoViens
                            .Where(x => x.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && x.NhanVien_ViTris.Any(m => m.CheDoId == LoaiCheDoEnums.FullTime.ToId()) && x.IsDisabled == false).CountAsync()
                    };
                    await _context.ThongKeGiaoVienTheoThangs.AddAsync(thongKeHocVien);
                }
                else
                {
                    var soLuong = await _context.GiaoViens
                            .Where(x => x.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && x.NhanVien_ViTris.Any(m => m.CheDoId == LoaiCheDoEnums.FullTime.ToId()) && x.IsDisabled == false).CountAsync();
                    if (thongke.SoLuong != soLuong)
                    {
                        thongke.SoLuong = soLuong;
                    }
                }
                await _context.SaveChangesAsync();

                var fullTime = await _context.ThongKeGiaoVienTheoThangs
                .Where(x => x.LoaiGiaoVien == (byte)LoaiGiaoVienEnums.FullTime && x.Date.Year == DateTime.Now.Year)
                .OrderBy(x => x.Date)

                .Select(g => new ThongKeModel
                {
                    Data = g.SoLuong,
                    Date = g.Date
                })
                .ToListAsync();

                return fullTime;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<ThongKeModel>> GetGiaoVienPartTimeAsync()
        {
            try
            {
                //tinh tong giao vien theo tháng
                var thongke = await _context.ThongKeGiaoVienTheoThangs
                    .FirstOrDefaultAsync(x => x.LoaiGiaoVien == (byte)LoaiGiaoVienEnums.PartTime && x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year);
                if (thongke == null)
                {
                    ThongKeGiaoVienTheoThang thongKeHocVien = new ThongKeGiaoVienTheoThang
                    {
                        Date = DateTime.Now,
                        LoaiGiaoVien = (byte)LoaiGiaoVienEnums.PartTime,
                        ThongKeGiaoVienTheoThangId = new Guid(),
                        SoLuong = await _context.GiaoViens
                            .Where(x => x.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && x.NhanVien_ViTris.Any(m => m.CheDoId == LoaiCheDoEnums.PartTime.ToId()) && x.IsDisabled == false).CountAsync()
                    };
                    await _context.ThongKeGiaoVienTheoThangs.AddAsync(thongKeHocVien);
                }
                else
                {
                    var soLuong = await _context.GiaoViens
                            .Where(x => x.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && x.NhanVien_ViTris.Any(m => m.CheDoId == LoaiCheDoEnums.PartTime.ToId()) && x.IsDisabled == false).CountAsync();
                    if (thongke.SoLuong != soLuong)
                    {
                        thongke.SoLuong = soLuong;
                    }
                }
                await _context.SaveChangesAsync();

                var partTime = await _context.ThongKeGiaoVienTheoThangs
                .Where(x => x.LoaiGiaoVien == (byte)LoaiGiaoVienEnums.PartTime && x.Date.Year == DateTime.Now.Year)
                .OrderBy(x => x.Date)

                .Select(g => new ThongKeModel
                {
                    Data = g.SoLuong,
                    Date = g.Date
                })
                .ToListAsync();

                return partTime;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        //public async Task<List<GiaoVienViewModel>> GetGiaoVienNuocNgoaiAsync()
        //{
        //    try
        //    {
        //        var nuocNgoai = await _context.GiaoViens
        //        .Where(x => x.LoaiGiaoVienId == LoaiGiaoVienEnums.GVNN.ToId() && x.IsDisabled == false)
        //        .Where(x => x.CreatedDate.Year == DateTime.Now.Year)
        //        .OrderBy(x => x.CreatedDate)

        //        .Select(g => new GiaoVienViewModel
        //        {
        //            GiaoVienId = g.GiaoVienId,
        //            CreatedDate_Date = g.CreatedDate
        //        })
        //        .ToListAsync();

        //        return nuocNgoai;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception(exception.Message);
        //    }
        //}

        public async Task<int> GetTongGiaoVienAsync()
        {
            try
            {
                return await _context.GiaoViens
                    .Where(x => x.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && x.IsDisabled == false)
                    .AsNoTracking()
                    .CountAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<int> GetTongHocVienAsync()
        {
            try
            {
                return await _context.HocVien_LopHocs
                            .Where(x => x.HocVien.IsDisabled == false)
                            .AsNoTracking()
                            .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == x.LopHoc.LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc > DateTime.Now)))
                            .Select(x => x.HocVienId)
                    .Distinct()
                    .CountAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<ThongKe_DoanhThuHocPhiViewModel>> GetDoanhThuHocPhiAsync()
        {
            try
            {
                var doanhThu = await _context.ThongKe_DoanhThuHocPhis
                .Where(x => x.NgayDong.Year == DateTime.Now.Year && x.DaDong == true)
                .OrderBy(x => x.NgayDong)
                .AsNoTracking()
                .Select(g => new ThongKe_DoanhThuHocPhiViewModel
                {
                    HocPhi = g.HocPhi,
                    NgayDong = g.NgayDong
                })
                .ToListAsync();

                return doanhThu;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<NoViewModel>> GetNoAsync()
        {
            try
            {
                var no = await _context.HocVien_Nos
                .Where(x => x.NgayNo.Year == DateTime.Now.Year && x.HocVien.IsDisabled == false)
                .OrderBy(x => x.NgayNo)
                .AsNoTracking()
                .Select(g => new NoViewModel
                {
                    TienNo = g.TienNo,
                    NgayNo_Date = g.NgayNo
                })
                .ToListAsync();

                return no;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<double> GetTongDoanhThuAsync()
        {
            try
            {
                return Math.Round(await _context.ThongKe_DoanhThuHocPhis.Where(x => x.DaDong == true).AsNoTracking().Select(x => x.HocPhi).SumAsync(), 0);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<ThongKe_ChiPhiViewModel>> GetChiPhiAsync()
        {
            try
            {
                var chiPhi = await _context.ThongKe_ChiPhis
                .Where(x => x.NgayChiPhi.Year == DateTime.Now.Year && x.DaLuu == true)
                .OrderBy(x => x.NgayChiPhi)
                .AsNoTracking()
                .Select(g => new ThongKe_ChiPhiViewModel
                {
                    ChiPhi = g.ChiPhi,
                    NgayChiPhi = g.NgayChiPhi
                })
                .ToListAsync();

                return chiPhi;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<double> GetTongChiPhiAsync()
        {
            try
            {
                return Math.Round(await _context.ThongKe_ChiPhis.Where(x => x.DaLuu == true).AsNoTracking().Select(x => x.ChiPhi).SumAsync(), 0);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<HocVienOffHon3NgayViewModel>> GetHocVienOffHon3NgayAsync()
        {
            var model = await _context.HocVien_NgayHocs
                .Where(x => x.HocVien.IsDisabled == false)
                .Where(x => x.NgayKetThuc == null || x.NgayKetThuc.Value >= DateTime.Now)
                .Select(x => new
                {
                    HocVien = x.HocVien.FullName,
                    LopHoc_DiemDanh = x.LopHoc.LopHoc_DiemDanhs
                                        .Where(p => p.LopHoc.IsCanceled == false && p.LopHoc.IsDisabled == false && p.LopHoc.IsGraduated == false)
                                        .OrderByDescending(p => p.NgayDiemDanh)
                                        .Where(p => p.IsOff == false && p.IsDuocNghi != true && p.HocVienId == x.HocVienId && p.LopHocId == x.LopHocId)
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
                NgayHocCuoi = x.LopHoc_DiemDanh[0].NgayDiemDanh.ToString("dd/MM/yyyy"),
                TenHocVien = x.HocVien
            })
                .ToList();
        }
    }
}
