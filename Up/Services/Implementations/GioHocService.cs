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

    public class GioHocService: IGioHocService
    {
        private readonly IGioHocRepository _gioHocRepository;
        private readonly ILopHocRepository _lopHocRepository;

        public GioHocService(IGioHocRepository gioHocRepository, ILopHocRepository lopHocRepository)
        {
            _gioHocRepository = gioHocRepository;
            _lopHocRepository = lopHocRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _gioHocRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_GioHoc);
            return canContribute;
        }

        public async Task<GioHocViewModel> CreateGioHocAsync(CreateGioHocInputModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.From) || string.IsNullOrWhiteSpace(input.To))
                throw new Exception("Giờ Học không được để trống !!!");

            var result = await _gioHocRepository.CreateGioHocAsync(input, loggedEmployee);

            return await _gioHocRepository.GetGioHocDetailAsync(result);
        }

        public async Task<bool> DeleteGioHocAsync(Guid id, string loggedEmployee)
        {
            var lopHocIds = await _lopHocRepository.GetLopHocIdByGioHocAsync(id);
            if (lopHocIds.Any())
                throw new Exception("Hãy xóa những lớp học thuộc giờ học này trước !!!");

            return await _gioHocRepository.DeleteGioHocAsync(id, loggedEmployee);
        }

        public async Task<List<GioHocViewModel>> GetGioHocAsync()
        {
            return await _gioHocRepository.GetGioHocAsync();
        }

        public async Task<GioHocViewModel> UpdateGioHocAsync(UpdateGioHocInputModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.From) || string.IsNullOrWhiteSpace(input.To))
                throw new Exception("Giờ Học không được để trống !!!");

            var result = await _gioHocRepository.UpdateGioHocAsync(input, loggedEmployee);

            return await _gioHocRepository.GetGioHocDetailAsync(result);
        }
    }
}
