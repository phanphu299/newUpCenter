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

    public class KhoaHocService : IKhoaHocService
    {
        private readonly IKhoaHocRepository _khoaHocRepository;

        public KhoaHocService(IKhoaHocRepository khoaHocRepository)
        {
            _khoaHocRepository = khoaHocRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _khoaHocRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_KhoaHoc);
            return canContribute;
        }

        public async Task<KhoaHocViewModel> CreateKhoaHocAsync(string name, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên Khóa Học không được để trống !!!");

            var result = await _khoaHocRepository.CreateKhoaHocAsync(name, loggedEmployee);
            return await _khoaHocRepository.GetKhoaHocDetailAsync(result);
        }

        public async Task<bool> DeleteKhoaHocAsync(Guid id, string loggedEmployee)
        {
            var lopHoc = await _khoaHocRepository.GetLopHocByKhoaHocIdAsync(id);
            if (lopHoc.Any())
                throw new Exception("Hãy xóa những lớp học thuộc khóa học này trước !!!");

            return await _khoaHocRepository.DeleteKhoaHocAsync(id, loggedEmployee);
        }

        public async Task<List<KhoaHocViewModel>> GetKhoaHocAsync()
        {
            return await _khoaHocRepository.GetKhoaHocAsync();
        }

        public async Task<bool> UpdateKhoaHocAsync(Guid id, string name, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên Khóa Học không được để trống !!!");

            return await _khoaHocRepository.UpdateKhoaHocAsync(id, name, loggedEmployee);
        }
    }
}
