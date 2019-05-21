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
                .Where(x => x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId == LoaiKhoaHocEnums.GiaoTiep.ToId()))
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
                var giaoTiep = await _context.HocViens
                .Where(x => x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId == LoaiKhoaHocEnums.ThieuNhi.ToId()))
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
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<List<HocVienViewModel>> GetHocVienCCQuocTeAsync()
        {
            try
            {
                var giaoTiep = await _context.HocViens
                .Where(x => x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId != LoaiKhoaHocEnums.GiaoTiep.ToId()) || x.HocVien_LopHocs.Any(p => p.LopHoc.KhoaHocId != LoaiKhoaHocEnums.ThieuNhi.ToId()))
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
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
