using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Enums;
using Up.Models;
using Up.Repositoties;

namespace Up.Services
{
    public class HocVienService : IHocVienService
    {
        private readonly IHocVienRepository _hocVienRepository;

        public HocVienService(IHocVienRepository hocVienRepository)
        {
            _hocVienRepository = hocVienRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            return await _hocVienRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_HocVien);
        }

        public async Task<bool> AddToUnavailableClassAsync(List<Guid> lopHocIds, Guid hocVienId, string loggedEmployee)
        {
            return await _hocVienRepository.AddToUnavailableClassAsync(lopHocIds, hocVienId, loggedEmployee);
        }

        public async Task<HocVienViewModel> CreateHocVienAsync(CreateHocVienInputModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.FullName))
                throw new Exception("Tên Học Viên không được để trống !!!");

            var result = await _hocVienRepository.CreateHocVienAsync(input, loggedEmployee);
            return await _hocVienRepository.GetHocVienDetailAsync(result);
        }

        public async Task<HocVienViewModel> ImportHocVienAsync(ImportHocVienInputModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.FullName))
                throw new Exception("Tên Học Viên không được để trống !!!");

            var result = await _hocVienRepository.ImportHocVienAsync(input, loggedEmployee);
            return await _hocVienRepository.GetHocVienDetailAsync(result);
        }

        public async Task<bool> DeleteHocVienAsync(Guid hocVienId, string loggedEmployee)
        {
            return await _hocVienRepository.DeleteHocVienAsync(hocVienId, loggedEmployee);
        }

        public async Task<List<HocVienViewModel>> GetAllHocVienAsync()
        {
            return await _hocVienRepository.GetAllHocVienAsync();
        }

        public async Task<List<HocVienViewModel>> GetHocVienAsync()
        {
            return await _hocVienRepository.GetHocVienAsync();
        }

        public async Task<HocVienViewModel> UpdateHocVienAsync(UpdateHocVienInputModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.FullName))
                throw new Exception("Tên Học Viên không được để trống !!!");

            var result = await _hocVienRepository.UpdateHocVienAsync(input, loggedEmployee);
            return await _hocVienRepository.GetHocVienDetailAsync(result);
        }

        public async Task<List<HocVienLightViewModel>> GetHocVienByNameAsync(string name)
        {
            return await _hocVienRepository.GetHocVienByNameAsync(name);
        }

        public async Task<HocVienViewModel> GetHocVienDetailAsync(Guid id)
        {
            return await _hocVienRepository.GetHocVienDetailAsync(id);
        }
    }
}
