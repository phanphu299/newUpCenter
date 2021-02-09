namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Enums;
    using Up.Models;
    using Up.Repositoties;

    public class GiaoVienService : IGiaoVienService
    {
        private readonly IGiaoVienRepository _giaoVienRepository;

        public GiaoVienService(IGiaoVienRepository giaoVienRepository)
        {
            _giaoVienRepository = giaoVienRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _giaoVienRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_NhanVien);
            return canContribute;
        }

        public async Task<GiaoVienViewModel> CreateGiaoVienAsync(CreateGiaoVienInputModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.Name) ||
                    string.IsNullOrWhiteSpace(input.Phone) ||
                    string.IsNullOrWhiteSpace(input.DiaChi) ||
                    string.IsNullOrWhiteSpace(input.InitialName) ||
                    string.IsNullOrWhiteSpace(input.CMND))
                throw new Exception("Tên Nhân Viên, SĐT, Địa Chỉ, Initial Name, CMND không được để trống !!!");

            var result = await _giaoVienRepository.CreateGiaoVienAsync(input, loggedEmployee);
            return await _giaoVienRepository.GetNhanVienDetailAsync(result);
        }

        public async Task<bool> DeleteGiaoVienAsync(Guid id, string loggedEmployee)
        {
            return await _giaoVienRepository.DeleteGiaoVienAsync(id, loggedEmployee);
        }

        public async Task<List<GiaoVienViewModel>> GetAllNhanVienAsync()
        {
            return await _giaoVienRepository.GetAllNhanVienAsync();
        }

        public async Task<List<GiaoVienViewModel>> GetGiaoVienOnlyAsync()
        {
            return await _giaoVienRepository.GetGiaoVienOnlyAsync();
        }

        public async Task<GiaoVienViewModel> UpdateGiaoVienAsync(UpdateGiaoVienInputModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.Name) ||
                    string.IsNullOrWhiteSpace(input.Phone) ||
                    string.IsNullOrWhiteSpace(input.DiaChi) ||
                    string.IsNullOrWhiteSpace(input.InitialName) ||
                    string.IsNullOrWhiteSpace(input.CMND))
                throw new Exception("Tên Giáo Viên, SĐT, Địa Chỉ, Initial Name, CMND không được để trống !!!");

            var result = await _giaoVienRepository.UpdateGiaoVienAsync(input, loggedEmployee);
            return await _giaoVienRepository.GetNhanVienDetailAsync(result);
        }
    }
}
