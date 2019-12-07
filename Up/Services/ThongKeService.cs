namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
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
                if(thongke == null)
                {
                    ThongKeHocVienTheoThang thongKeHocVien = new ThongKeHocVienTheoThang {
                        Date = DateTime.Now,
                        LoaiHocVien = (byte)LoaiHocVienEnums.GiaoTiep,
                        ThongKeHocVienTheoThangId = new Guid(),
                        SoLuong = await _context.HocViens
                            .Where(x => x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId == LoaiKhoaHocEnums.GiaoTiep.ToId()) && x.IsDisabled == false && !x.HocVien_NgayHocs.Any(p => p.NgayKetThuc != null)).CountAsync()
                    };
                    await _context.ThongKeHocVienTheoThangs.AddAsync(thongKeHocVien);
                }
                else
                {
                    var soLuong = await _context.HocViens
                            .Where(x => x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId == LoaiKhoaHocEnums.GiaoTiep.ToId()) && x.IsDisabled == false && !x.HocVien_NgayHocs.Any(p => p.NgayKetThuc != null)).CountAsync();
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
            catch(Exception exception)
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
                        SoLuong = await _context.HocViens
                            .Where(x => x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId == LoaiKhoaHocEnums.ThieuNhi.ToId()) && x.IsDisabled == false && !x.HocVien_NgayHocs.Any(p => p.NgayKetThuc != null)).CountAsync()
                    };
                    await _context.ThongKeHocVienTheoThangs.AddAsync(thongKeHocVien);
                }
                else
                {
                    var soLuong = await _context.HocViens
                            .Where(x => x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId == LoaiKhoaHocEnums.ThieuNhi.ToId()) && x.IsDisabled == false && !x.HocVien_NgayHocs.Any(p => p.NgayKetThuc != null)).CountAsync();
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
                //tinh tong hoc vien theo tháng
                var thongke = await _context.ThongKeHocVienTheoThangs
                    .FirstOrDefaultAsync(x => x.LoaiHocVien == (byte)LoaiHocVienEnums.QuocTe && x.Date.Month == DateTime.Now.Month && x.Date.Year == DateTime.Now.Year);
                if (thongke == null)
                {
                    ThongKeHocVienTheoThang thongKeHocVien = new ThongKeHocVienTheoThang
                    {
                        Date = DateTime.Now,
                        LoaiHocVien = (byte)LoaiHocVienEnums.QuocTe,
                        ThongKeHocVienTheoThangId = new Guid(),
                        SoLuong = await _context.HocViens
                            .Where(x => (!x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId == LoaiKhoaHocEnums.GiaoTiep.ToId()) && !x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId == LoaiKhoaHocEnums.ThieuNhi.ToId())) && x.IsDisabled == false && !x.HocVien_NgayHocs.Any(p => p.NgayKetThuc != null)).CountAsync()
                    };
                    await _context.ThongKeHocVienTheoThangs.AddAsync(thongKeHocVien);
                }
                else
                {
                    var soLuong = await _context.HocViens
                            .Where(x => (!x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId == LoaiKhoaHocEnums.GiaoTiep.ToId()) && !x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId == LoaiKhoaHocEnums.ThieuNhi.ToId())) && x.IsDisabled == false && !x.HocVien_NgayHocs.Any(p => p.NgayKetThuc != null)).CountAsync();
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
                return await _context.GiaoViens.Where(x => x.NhanVien_ViTris.Any(m => m.ViTriId == LoaiNhanVienEnums.GiaoVien.ToId()) && x.IsDisabled == false).CountAsync();
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
                return await _context.HocViens.Where(x => x.IsDisabled == false && !x.HocVien_NgayHocs.Any(p => p.NgayKetThuc != null)).CountAsync();
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
                .Where(x => x.NgayNo.Year == DateTime.Now.Year)
                .OrderBy(x => x.NgayNo)

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
                return Math.Round(await _context.ThongKe_DoanhThuHocPhis.Where(x => x.DaDong == true).Select(x => x.HocPhi).SumAsync(), 0);
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
                .Where(x => x.NgayChiPhi.Year == DateTime.Now.Year  && x.DaLuu == true)
                .OrderBy(x => x.NgayChiPhi)

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
                return Math.Round(await _context.ThongKe_ChiPhis.Where(x => x.DaLuu == true).Select(x => x.ChiPhi).SumAsync(), 0);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
