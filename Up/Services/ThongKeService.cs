namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Enums;
    using Up.Models;

    public class ThongKeService : IThongKeService
    {
        private readonly ApplicationDbContext _context;

        public ThongKeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<HocVienViewModel>> GetHocVienGiaoTiepAsync()
        {
            try
            {
                var giaoTiep = await _context.HocViens
                .Where(x => x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId == LoaiKhoaHocEnums.GiaoTiep.ToId()) && x.IsDisabled == false)
                .Where(x => x.CreatedDate.Year == DateTime.Now.Year)
                .OrderBy(x => x.CreatedDate)
                
                .Select(g => new HocVienViewModel
                {
                    HocVienId = g.HocVienId,
                    CreatedDate_Date = g.CreatedDate
                })
                .ToListAsync();

                return giaoTiep;
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<HocVienViewModel>> GetHocVienThieuNhiAsync()
        {
            try
            {
                var thieuNhi = await _context.HocViens
                .Where(x => x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId == LoaiKhoaHocEnums.ThieuNhi.ToId()) && x.IsDisabled == false)
                .Where(x => x.CreatedDate.Year == DateTime.Now.Year)
                .OrderBy(x => x.CreatedDate)

                .Select(g => new HocVienViewModel
                {
                    HocVienId = g.HocVienId,
                    CreatedDate_Date = g.CreatedDate
                })
                .ToListAsync();

                return thieuNhi;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<HocVienViewModel>> GetHocVienCCQuocTeAsync()
        {
            try
            {
                var quocTe = await _context.HocViens
                .Where(x => (x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId != LoaiKhoaHocEnums.GiaoTiep.ToId()) || x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId != LoaiKhoaHocEnums.ThieuNhi.ToId())) && x.IsDisabled == false)
                .Where(x => x.CreatedDate.Year == DateTime.Now.Year)
                .OrderBy(x => x.CreatedDate)

                .Select(g => new HocVienViewModel
                {
                    HocVienId = g.HocVienId,
                    CreatedDate_Date = g.CreatedDate
                })
                .ToListAsync();

                return quocTe;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<GiaoVienViewModel>> GetGiaoVienFullTimeAsync()
        {
            try
            {
                var fullTime = await _context.GiaoViens
                .Where(x => x.LoaiGiaoVienId == LoaiGiaoVienEnums.FullTime.ToId() && x.IsDisabled == false)
                .Where(x => x.CreatedDate.Year == DateTime.Now.Year)
                .OrderBy(x => x.CreatedDate)

                .Select(g => new GiaoVienViewModel
                {
                    GiaoVienId = g.GiaoVienId,
                    CreatedDate_Date = g.CreatedDate
                })
                .ToListAsync();

                return fullTime;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<GiaoVienViewModel>> GetGiaoVienPartTimeAsync()
        {
            try
            {
                var partTime = await _context.GiaoViens
                .Where(x => x.LoaiGiaoVienId == LoaiGiaoVienEnums.PartTime.ToId() && x.IsDisabled == false)
                .Where(x => x.CreatedDate.Year == DateTime.Now.Year)
                .OrderBy(x => x.CreatedDate)

                .Select(g => new GiaoVienViewModel
                {
                    GiaoVienId = g.GiaoVienId,
                    CreatedDate_Date = g.CreatedDate
                })
                .ToListAsync();

                return partTime;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<GiaoVienViewModel>> GetGiaoVienNuocNgoaiAsync()
        {
            try
            {
                var nuocNgoai = await _context.GiaoViens
                .Where(x => x.LoaiGiaoVienId == LoaiGiaoVienEnums.GVNN.ToId() && x.IsDisabled == false)
                .Where(x => x.CreatedDate.Year == DateTime.Now.Year)
                .OrderBy(x => x.CreatedDate)

                .Select(g => new GiaoVienViewModel
                {
                    GiaoVienId = g.GiaoVienId,
                    CreatedDate_Date = g.CreatedDate
                })
                .ToListAsync();

                return nuocNgoai;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<int> GetTongGiaoVienAsync()
        {
            try
            {
                return await _context.GiaoViens.Where(x => x.IsDisabled == false).CountAsync();
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
                return await _context.HocViens.Where(x => x.IsDisabled == false).CountAsync();
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
                .Where(x => x.NgayDong.Year == DateTime.Now.Year)
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
                return Math.Round(await _context.ThongKe_DoanhThuHocPhis.Select(x => x.HocPhi).SumAsync(), 0);
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
                .Where(x => x.NgayChiPhi.Year == DateTime.Now.Year)
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
                return Math.Round(await _context.ThongKe_ChiPhis.Select(x => x.ChiPhi).SumAsync(), 0);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
