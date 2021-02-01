
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Enums;
    using Up.Models;
    using Up.Repositoties;

    public class SachService : ISachService
    {
        private readonly ISachRepository _sachRepository;

        public SachService(ISachRepository sachRepository)
        {
            _sachRepository = sachRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _sachRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_TaiLieu);

            return canContribute;
        }

        public async Task<SachViewModel> CreateSachAsync(CreateSachInputModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
                throw new Exception("Tên Tài Liệu không được để trống !!!");

            var result = await _sachRepository.CreateSachAsync(input, loggedEmployee);
            return await _sachRepository.GetSachDetailAsync(result);
        }

        public async Task<bool> DeleteSachAsync(Guid id, string loggedEmployee)
        {
            return await _sachRepository.DeleteSachAsync(id, loggedEmployee);
        }

        public async Task<IList<SachViewModel>> GetSachAsync()
        {
            return await _sachRepository.GetSachAsync();
        }

        public async Task<bool> UpdateSachAsync(UpdateSachInputModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
                throw new Exception("Tên Tài Liệu không được để trống !!!");

            return await _sachRepository.UpdateSachAsync(input, loggedEmployee);
        }
    }
}
