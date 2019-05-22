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
    }
}
