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

    public class DiemDanhService : IDiemDanhService
    {
        private readonly ApplicationDbContext _context;

        public DiemDanhService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DiemDanhTatCaAsync(Guid LopHocId, bool isOff, DateTime NgayDiemDanh, string LoggedEmployee)
        {
            try
            {
                if (LopHocId == null || isOff == null || NgayDiemDanh == null)
                    throw new Exception("Lỗi khi Điểm Danh!!!");

                var isExisting = _context.LopHoc_DiemDanhs
                                        .Where(x => x.LopHocId == LopHocId && x.NgayDiemDanh == NgayDiemDanh)
                                        .ToListAsync();
                if (isExisting.Result.Any())
                    throw new Exception("Lớp học đã được điểm danh ngày " + NgayDiemDanh.ToShortDateString());

                var hocViens = GetHocVienByLopHoc(LopHocId);

                foreach (var item in hocViens.Result)
                {
                    LopHoc_DiemDanh lopHoc_DiemDanh = new LopHoc_DiemDanh();
                    lopHoc_DiemDanh.NgayDiemDanh = NgayDiemDanh;
                    lopHoc_DiemDanh.IsOff = isOff;
                    lopHoc_DiemDanh.LopHocId = LopHocId;
                    lopHoc_DiemDanh.HocVienId = item.HocVienId;
                    lopHoc_DiemDanh.CreatedBy = LoggedEmployee;
                    lopHoc_DiemDanh.CreatedDate = DateTime.Now;
                    _context.LopHoc_DiemDanhs.Add(lopHoc_DiemDanh);
                }

                var saveResult = await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public async Task<bool> DiemDanhTungHocVienAsync(Guid LopHocId, Guid HocVienId, bool isOff, DateTime NgayDiemDanh, string LoggedEmployee)
        {
            try
            {
                if (LopHocId == null || HocVienId == null || isOff == null || NgayDiemDanh == null)
                    throw new Exception("Lỗi khi Điểm Danh!!!");

                var diemDanh = await _context.LopHoc_DiemDanhs
                                        .Where(x => x.LopHocId == LopHocId && x.HocVienId == HocVienId && x.NgayDiemDanh == NgayDiemDanh)
                                        .SingleOrDefaultAsync();

                if (diemDanh != null)
                {
                    diemDanh.IsOff = isOff;
                }
                else
                {
                    LopHoc_DiemDanh lopHoc_DiemDanh = new LopHoc_DiemDanh();
                    lopHoc_DiemDanh.NgayDiemDanh = NgayDiemDanh;
                    lopHoc_DiemDanh.IsOff = isOff;
                    lopHoc_DiemDanh.LopHocId = LopHocId;
                    lopHoc_DiemDanh.HocVienId = HocVienId;
                    lopHoc_DiemDanh.CreatedBy = LoggedEmployee;
                    lopHoc_DiemDanh.CreatedDate = DateTime.Now;

                    _context.LopHoc_DiemDanhs.Add(lopHoc_DiemDanh);
                }

                var saveResult = await _context.SaveChangesAsync();
                if (saveResult != 1)
                    throw new Exception("Lỗi khi Điểm Danh!!!");
                return true;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public async Task<bool> DuocNghi(Guid LopHocId, DateTime NgayDiemDanh, string LoggedEmployee)
        {
            if (LopHocId == null || NgayDiemDanh == null)
                throw new Exception("Lỗi khi Cho Lớp Học Nghỉ!!!");

            var isExisting = _context.LopHoc_DiemDanhs
                                    .Where(x => x.LopHocId == LopHocId && x.NgayDiemDanh == NgayDiemDanh)
                                    .ToListAsync();
            if (isExisting.Result.Any())
                throw new Exception("Lớp học đã được điểm danh ngày " + NgayDiemDanh.ToShortDateString());

            var hocViens = GetHocVienByLopHoc(LopHocId);

            foreach (var item in hocViens.Result)
            {
                LopHoc_DiemDanh lopHoc_DiemDanh = new LopHoc_DiemDanh();
                lopHoc_DiemDanh.NgayDiemDanh = NgayDiemDanh;
                lopHoc_DiemDanh.IsOff = true;
                lopHoc_DiemDanh.IsDuocNghi = true;
                lopHoc_DiemDanh.LopHocId = LopHocId;
                lopHoc_DiemDanh.HocVienId = item.HocVienId;
                lopHoc_DiemDanh.CreatedBy = LoggedEmployee;
                lopHoc_DiemDanh.CreatedDate = DateTime.Now;
                _context.LopHoc_DiemDanhs.Add(lopHoc_DiemDanh);
            }

            var saveResult = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DiemDanhViewModel>> GetDiemDanhByHocVienAndLopHoc(Guid HocVienId, Guid LopHocId)
        {
            if (HocVienId == null)
                throw new Exception("Không tìm thấy Học Viên!");

            if (LopHocId == null)
                throw new Exception("Không tìm thấy Lớp Học!");

            return await _context.LopHoc_DiemDanhs.Where(x => x.HocVienId == HocVienId && x.LopHocId == LopHocId )
                                .OrderByDescending(x => x.NgayDiemDanh)
                                .Select(x => new DiemDanhViewModel
                                {
                                    IsDuocNghi = x.IsDuocNghi,
                                    IsOff = x.IsOff,
                                    NgayDiemDanh = x.NgayDiemDanh.ToString("dd/MM/yyyy"),
                                    NgayDiemDanh_Date = x.NgayDiemDanh,
                                    Day = x.NgayDiemDanh.Day
                                })
                                .ToListAsync();
        }

        public async Task<List<DiemDanhViewModel>> GetDiemDanhByLopHoc(Guid LopHocId)
        {
            if (LopHocId == null)
                throw new Exception("Không tìm thấy Lớp Học!");

            return await _context.LopHoc_DiemDanhs.Where(x => x.LopHocId == LopHocId)
                                .OrderByDescending(x => x.NgayDiemDanh)
                                .Select(x => new DiemDanhViewModel
                                {
                                    IsDuocNghi = x.IsDuocNghi,
                                    IsOff = x.IsOff,
                                    NgayDiemDanh = x.NgayDiemDanh.ToString("dd/MM/yyyy"),
                                    HocVien = x.HocVien.FullName,
                                    NgayDiemDanh_Date = x.NgayDiemDanh
                                })
                                .ToListAsync();
        }

        public async Task<List<HocVienViewModel>> GetHocVienByLopHoc(Guid LopHocId)
        {
            if (LopHocId == null)
                throw new Exception("Không tìm thấy Lớp Học!");

            return await _context.HocVien_LopHocs.Where(x => x.LopHocId == LopHocId)
                                .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.NgayKetThuc == null))
                                .Select(x => new HocVienViewModel
                                {
                                    FullName = x.HocVien.FullName,
                                    EnglishName = x.HocVien.EnglishName,
                                    HocVienId = x.HocVienId,
                                })
                                .ToListAsync();
        }
    }
}
