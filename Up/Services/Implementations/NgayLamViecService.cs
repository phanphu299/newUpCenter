
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

    public class NgayLamViecService : INgayLamViecService
    {
        private readonly INgayLamViecRepository _ngayLamViecRepository;
        private readonly IGiaoVienRepository _giaoVienRepository;

        public NgayLamViecService(INgayLamViecRepository ngayLamViecRepository, IGiaoVienRepository giaoVienRepository)
        {
            _ngayLamViecRepository = ngayLamViecRepository;
            _giaoVienRepository = giaoVienRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _ngayLamViecRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_NgayLamViec);
            return canContribute;
        }

        public async Task<NgayLamViecViewModel> CreateNgayLamViecAsync(string name, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên Ngày Làm Việc không được để trống !!!");

            var result = await _ngayLamViecRepository.CreateNgayLamViecAsync(name, loggedEmployee);
            return await _ngayLamViecRepository.GetNgayLamViecDetailAsync(result);
        }

        public async Task<bool> DeleteNgayLamViecAsync(Guid id, string loggedEmployee)
        {
            var giaoVienIds = await _giaoVienRepository.GetGiaoVienIdsByNgayLamViec(id);
            if (giaoVienIds.Any())
                throw new Exception("Hãy xóa những nhân viên có ngày làm việc này trước !!!");

            return await _ngayLamViecRepository.DeleteNgayLamViecAsync(id, loggedEmployee);
        }

        public async Task<List<NgayLamViecViewModel>> GetNgayLamViecAsync()
        {
            return await _ngayLamViecRepository.GetNgayLamViecAsync();
        }

        public async Task<bool> UpdateNgayLamViecAsync(Guid id, string name, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên ngày làm việc không được để trống !!!");

            return await _ngayLamViecRepository.UpdateNgayLamViecAsync(id, name, loggedEmployee);
        }
    }
}
