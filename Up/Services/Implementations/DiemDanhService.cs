namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Data.Entities;
    using Up.Enums;
    using Up.Models;
    using Up.Repositoties;

    public class DiemDanhService : IDiemDanhService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDiemDanhRepository _diemDanhRepository;

        public DiemDanhService(ApplicationDbContext context, IDiemDanhRepository diemDanhRepository)
        {
            _context = context;
            _diemDanhRepository = diemDanhRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _diemDanhRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_DiemDanh);
            return canContribute;
        }

        public async Task<bool> CanContributeExportAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _diemDanhRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_DiemDanh_Export);
            return canContribute;
        }

        public async Task<bool> DiemDanhTatCaAsync(DiemDanhHocVienInput input, string loggedEmployee)
        {
            if (input.LopHocId == null || input.IsOff == null || input.NgayDiemDanh == null)
                throw new Exception("Lỗi khi Điểm Danh!!!");

            if (await _diemDanhRepository.CheckDaDiemDanhAsync(input.LopHocId, input.NgayDiemDanh))
                throw new Exception($"Lớp học đã được điểm danh ngày {input.NgayDiemDanh.ToShortDateString()}");

            return await _diemDanhRepository.DiemDanhTatCaAsync(input, loggedEmployee);
        }

        public async Task<bool> DiemDanhTungHocVienAsync(DiemDanhHocVienInput input, string loggedEmployee)
        {
            if (input.LopHocId == null || input.HocVienId == null || input.IsOff == null || input.NgayDiemDanh == null)
                throw new Exception("Lỗi khi Điểm Danh!!!");

            return await _diemDanhRepository.DiemDanhTungHocVienAsync(input, loggedEmployee);
        }

        public async Task<bool> DuocNghi(Guid LopHocId, DateTime NgayDiemDanh, string LoggedEmployee)
        {
            if (LopHocId == null || NgayDiemDanh == null)
                throw new Exception("Lỗi khi Cho Lớp Học Nghỉ!!!");

            var isExisting = await _context.LopHoc_DiemDanhs
                                    .Where(x => x.LopHocId == LopHocId && x.NgayDiemDanh == NgayDiemDanh)
                                    .ToListAsync();
            if (isExisting.Any())
                _context.LopHoc_DiemDanhs.RemoveRange(isExisting);
            //throw new Exception("Lớp học đã được điểm danh ngày " + NgayDiemDanh.ToShortDateString());

            var hocViens = await _context.HocVien_LopHocs
                                .Include(x => x.LopHoc)
                                .Include(x => x.HocVien.HocVien_NgayHocs)
                                .Where(x => x.LopHocId == LopHocId && x.HocVien.IsDisabled == false)
                                .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc.Value >= NgayDiemDanh)))
                                .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayBatDau <= NgayDiemDanh)))
                                .Select(x => new HocVienViewModel
                                {
                                    HocVienId = x.HocVienId,
                                })
                                .ToListAsync();

            foreach (var item in hocViens)
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

        public async Task<List<DiemDanhViewModel>> GetDiemDanhByHocVienAndLopHoc(Guid hocVienId, Guid lopHocId)
        {
            if (hocVienId == null)
                throw new Exception("Không tìm thấy Học Viên!");

            if (lopHocId == null)
                throw new Exception("Không tìm thấy Lớp Học!");

            return await _diemDanhRepository.GetDiemDanhByHocVienAndLopHoc(hocVienId, lopHocId);
        }

        public async Task<List<DiemDanhViewModel>> GetDiemDanhByLopHoc(Guid lopHocId, int month, int year)
        {
            if (lopHocId == null)
                throw new Exception("Không tìm thấy Lớp Học!");

            return await _diemDanhRepository.GetDiemDanhByLopHoc(lopHocId, month, year);
        }

        public async Task<List<HocVienViewModel>> GetHocVienByLopHoc(Guid lopHocId)
        {
            if (lopHocId == null)
                throw new Exception("Không tìm thấy Lớp Học!");

            return await _diemDanhRepository.GetHocVienByLopHoc(lopHocId);
        }

        public async Task<bool> SaveHocVienHoanTac(Guid LopHocId, List<Guid> HocVienIds, List<DateTime> NgayDiemDanhs, string LoggedEmployee)
        {
            try
            {
                if (LopHocId == null || !NgayDiemDanhs.Any())
                    throw new Exception("Lỗi khi Hoan Tác!!!");

                foreach (DateTime NgayDiemDanh in NgayDiemDanhs)
                {
                    var isExisting = await _context.LopHoc_DiemDanhs
                                          .Where(x => x.LopHocId == LopHocId && x.NgayDiemDanh == NgayDiemDanh && HocVienIds.Contains(x.HocVienId))
                                          .ToListAsync();
                    if (isExisting.Any())
                        _context.LopHoc_DiemDanhs.RemoveRange(isExisting);
                }

                var saveResult = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SaveHocVienOff(Guid LopHocId, List<Guid> HocVienIds, List<DateTime> NgayDiemDanhs, string LoggedEmployee)
        {
            try
            {
                if (LopHocId == null || !NgayDiemDanhs.Any())
                    throw new Exception("Lỗi khi Cho Lớp Học Nghỉ!!!");

                foreach (DateTime NgayDiemDanh in NgayDiemDanhs)
                {
                    var isExisting = await _context.LopHoc_DiemDanhs
                                        .Where(x => x.LopHocId == LopHocId && x.NgayDiemDanh == NgayDiemDanh && HocVienIds.Contains(x.HocVienId))
                                        .ToListAsync();
                    if (isExisting.Any())
                        _context.LopHoc_DiemDanhs.RemoveRange(isExisting);
                    //throw new Exception("Lớp học đã được điểm danh ngày " + NgayDiemDanh.ToShortDateString());

                    var hocViens = await _context.HocVien_LopHocs
                                        .Include(x => x.LopHoc)
                                        .Include(x => x.HocVien.HocVien_NgayHocs)
                                        .Where(x => x.LopHocId == LopHocId && x.HocVien.IsDisabled == false)
                                        .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc.Value >= NgayDiemDanh)))
                                        .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayBatDau <= NgayDiemDanh)))
                                        .Where(x => HocVienIds.Contains(x.HocVienId))
                                        .Select(x => new HocVienViewModel
                                        {
                                            HocVienId = x.HocVienId,
                                        })
                                        .ToListAsync();

                    foreach (var item in hocViens)
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
                }

                var saveResult = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<int>> SoNgayHocAsync(Guid lopHocId, int month, int year)
        {
            return await _diemDanhRepository.SoNgayHocAsync(lopHocId, month, year);
        }

        public async Task<bool> UndoDuocNghi(Guid LopHocId, DateTime NgayDiemDanh, string LoggedEmployee)
        {
            if (LopHocId == null || NgayDiemDanh == null)
                throw new Exception("Lỗi khi Cho Lớp Học Nghỉ!!!");

            var items = await _context.LopHoc_DiemDanhs
                                    .Where(x => x.LopHocId == LopHocId && x.NgayDiemDanh == NgayDiemDanh)
                                    .ToListAsync();
            if (!items.Any())
                throw new Exception("Lớp học chưa được Cho nghỉ ngày " + NgayDiemDanh.ToShortDateString());

            //foreach (var item in items)
            //{
            //    item.IsOff = false;
            //    item.IsDuocNghi = false;
            //    item.UpdatedBy = LoggedEmployee;
            //    item.UpdatedDate = DateTime.Now;
            //}
            _context.RemoveRange(items);

            var saveResult = await _context.SaveChangesAsync();
            return true;
        }
    }
}
