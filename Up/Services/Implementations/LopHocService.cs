namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Enums;
    using Up.Models;
    using Up.Repositoties;

    public class LopHocService : ILopHocService
    {
        private readonly ILopHocRepository _lopHocRepository;

        public LopHocService(ILopHocRepository lopHocRepository)
        {
            _lopHocRepository = lopHocRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            bool canContribute = await _lopHocRepository.CanContributeAsync(User, (int)QuyenEnums.Contribute_LopHoc);

            return canContribute;
        }

        public async Task<LopHocViewModel> CreateLopHocAsync(CreateLopHocInputModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.Name) || input.KhoaHocId == null || input.NgayHocId == null || input.GioHocId == null || input.NgayKhaiGiangDate == null)
                throw new Exception("Tên Lớp Học, Khóa Học, Ngày Học, Giờ Học, Ngày Khai Giảng không được để trống !!!");

            var result = await _lopHocRepository.CreateLopHocAsync(input, loggedEmployee);
            return await _lopHocRepository.GetLopHocDetailAsync(result);
        }

        public async Task<bool> DeleteLopHocAsync(Guid id, string loggedEmployee)
        {
            var hocVienIds = await _lopHocRepository.GetHocVienIdAsync(id);

            if (hocVienIds.Any())
                throw new Exception("Vẫn còn học viên theo học Lớp Học này !!!");

            return await _lopHocRepository.DeleteLopHocAsync(id, loggedEmployee);
        }

        public async Task<List<LopHocViewModel>> GetAvailableLopHocAsync(int? thang = null, int? nam = null)
        {
            return await _lopHocRepository.GetAvailableLopHocAsync(thang, nam);
        }

        public async Task<List<LopHocViewModel>> GetGraduatedAndCanceledLopHocAsync()
        {
            return await _lopHocRepository.GetGraduatedAndCanceledLopHocAsync();
        }

        public async Task<List<LopHocViewModel>> GetLopHocAsync()
        {
            return await _lopHocRepository.GetLopHocAsync();
        }

        public async Task<List<LopHocViewModel>> GetLopHocByHocVienIdAsync(Guid hocVienId)
        {
            return await _lopHocRepository.GetLopHocByHocVienIdAsync(hocVienId);
        }

        public async Task<LopHocViewModel> GetLopHocDetailAsync(Guid id)
        {
            return await _lopHocRepository.GetLopHocDetailAsync(id);
        }

        public async Task<bool> ToggleHuyLopAsync(Guid id, string loggedEmployee)
        {
            return await _lopHocRepository.ToggleHuyLopAsync(id, loggedEmployee);
        }

        public async Task<bool> ToggleTotNghiepAsync(Guid id, string loggedEmployee)
        {
            return await _lopHocRepository.ToggleTotNghiepAsync(id, loggedEmployee);
        }

        public async Task<bool> UpdateHocPhiLopHocAsync(Guid lopHocId, Guid hocPhiId, int thang, int nam, string loggedEmployee)
        {
            return await _lopHocRepository.UpdateHocPhiLopHocAsync(lopHocId, hocPhiId, thang, nam, loggedEmployee);
        }

        public async Task<LopHocViewModel> UpdateLopHocAsync(UpdateLopHocInputModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.Name) || input.KhoaHocId == null || input.NgayHocId == null || input.GioHocId == null || input.NgayKhaiGiang == null)
                throw new Exception("Tên Lớp Học, Khóa Học, Ngày Học, Giờ Học, Ngày Khai Giảng không được để trống !!!");

            var result = await _lopHocRepository.UpdateLopHocAsync(input, loggedEmployee);
            return await _lopHocRepository.GetLopHocDetailAsync(result);
        }
    }
}
