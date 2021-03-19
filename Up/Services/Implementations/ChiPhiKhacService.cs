
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Enums;
    using Up.Models;
    using Up.Repositoties;

    public class ChiPhiKhacService : IChiPhiKhacService
    {
        private readonly IChiPhiKhacRepository _chiPhiKhacRepository;

        public ChiPhiKhacService(IChiPhiKhacRepository chiPhiKhacRepository)
        {
            _chiPhiKhacRepository = chiPhiKhacRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _chiPhiKhacRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_ChiPhiKhac);
            return canContribute;
        }

        public async Task<ChiPhiKhacViewModel> CreateChiPhiKhacAsync(CreateChiPhiKhacInputModel input, string loggedEmployee)
        {
            var result = await _chiPhiKhacRepository.CreateChiPhiKhacAsync(input, loggedEmployee);
            return await _chiPhiKhacRepository.GetChiPhiKhacDetailAsync(result);
        }

        public async Task<bool> DeleteChiPhiKhacAsync(Guid id, string loggedEmployee)
        {
            return await _chiPhiKhacRepository.DeleteChiPhiKhacAsync(id, loggedEmployee);
        }

        public async Task<List<ChiPhiKhacViewModel>> GetChiPhiKhacAsync()
        {
            return await _chiPhiKhacRepository.GetChiPhiKhacAsync();
        }

        public async Task<ChiPhiKhacViewModel> UpdateChiPhiKhacAsync(UpdateChiPhiKhacInputModel input, string loggedEmployee)
        {
            var result = await _chiPhiKhacRepository.UpdateChiPhiKhacAsync(input, loggedEmployee);
            return await _chiPhiKhacRepository.GetChiPhiKhacDetailAsync(result);
        }
    }
}
