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

        public async Task<bool> DuocNghiAsync(DiemDanhHocVienInput input, string loggedEmployee)
        {
            if (input.LopHocId == null || input.NgayDiemDanh == null)
                throw new Exception("Lỗi khi Cho Lớp Học Nghỉ!!!");

            var isExistingIds = await _diemDanhRepository.GetDiemDanhByLopHocAndNgayDiemDanhAsync(input.LopHocId, input.NgayDiemDanh);
            if (isExistingIds.Any())
                await _diemDanhRepository.RemoveDiemDanhByIdsAsync(isExistingIds);

            return await _diemDanhRepository.DuocNghiAsync(input, loggedEmployee);
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

        public async Task<bool> SaveHocVienHoanTac(Guid lopHocId, List<Guid> hocVienIds, List<DateTime> ngayDiemDanhs)
        {
            if (lopHocId == null || !ngayDiemDanhs.Any())
                throw new Exception("Lỗi khi Hoàn Tác!!!");

            foreach (DateTime NgayDiemDanh in ngayDiemDanhs)
            {
                var isExistingIds = await _diemDanhRepository.GetDiemDanhByLopHocAndNgayDiemDanhAndHocVienIdsAsync(lopHocId, NgayDiemDanh, hocVienIds);
                if (isExistingIds.Any())
                    await _diemDanhRepository.RemoveDiemDanhByIdsAsync(isExistingIds);
            }

            return true;
        }

        public async Task<bool> SaveHocVienOff(Guid lopHocId, List<Guid> hocVienIds, List<DateTime> ngayDiemDanhs, string loggedEmployee)
        {
            if (lopHocId == null || !ngayDiemDanhs.Any())
                throw new Exception("Lỗi khi Cho Lớp Học Nghỉ!!!");

            return await _diemDanhRepository.SaveHocVienOff(lopHocId, hocVienIds, ngayDiemDanhs, loggedEmployee);
        }

        public async Task<List<int>> SoNgayHocAsync(Guid lopHocId, int month, int year)
        {
            return await _diemDanhRepository.SoNgayHocAsync(lopHocId, month, year);
        }

        public async Task<bool> UndoDuocNghi(DiemDanhHocVienInput input, string loggedEmployee)
        {
            if (input.LopHocId == null || input.NgayDiemDanh == null)
                throw new Exception("Lỗi khi Cho Lớp Học Nghỉ!!!");

            var items = await _diemDanhRepository.GetDiemDanhByLopHocAndNgayDiemDanhAsync(input.LopHocId, input.NgayDiemDanh);
            if (!items.Any())
                throw new Exception($"Lớp học chưa được Cho nghỉ ngày {input.NgayDiemDanh.ToShortDateString()}" );

            await _diemDanhRepository.RemoveDiemDanhByIdsAsync(items);
            return true;
        }
    }
}
