
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Enums;
    using Up.Models;
    using Up.Repositoties;

    public class ChiPhiCoDinhService : IChiPhiCoDinhService
    {
        private readonly IChiPhiCoDinhRepository _chiPhiCoDinhRepository;

        public ChiPhiCoDinhService(IChiPhiCoDinhRepository chiPhiCoDinhRepository)
        {
            _chiPhiCoDinhRepository = chiPhiCoDinhRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _chiPhiCoDinhRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_ChiPhiCoDinh);
            return canContribute;
        }

        public async Task<ChiPhiCoDinhViewModel> CreateChiPhiCoDinhAsync(double gia, string name, string loggedEmployee)
        {
            var result = await _chiPhiCoDinhRepository.CreateChiPhiCoDinhAsync(gia, name, loggedEmployee);
            return await _chiPhiCoDinhRepository.GetChiPhiCoDinhDetailAsync(result);
        }

        public async Task<bool> DeleteChiPhiCoDinhAsync(Guid id, string loggedEmployee)
        {
            return await _chiPhiCoDinhRepository.DeleteChiPhiCoDinhAsync(id, loggedEmployee);
        }

        public async Task<List<ChiPhiCoDinhViewModel>> GetChiPhiCoDinhAsync()
        {
            return await _chiPhiCoDinhRepository.GetChiPhiCoDinhAsync();
        }

        public async Task<ChiPhiCoDinhViewModel> UpdateChiPhiCoDinhAsync(Guid id, double gia, string name, string loggedEmployee)
        {
            var result = await _chiPhiCoDinhRepository.UpdateChiPhiCoDinhAsync(id, gia, name, loggedEmployee);
            return await _chiPhiCoDinhRepository.GetChiPhiCoDinhDetailAsync(result);
        }
    }
}
