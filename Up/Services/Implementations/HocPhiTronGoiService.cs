
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Enums;
    using Up.Models;
    using Up.Repositoties;

    public class HocPhiTronGoiService : IHocPhiTronGoiService
    {
        private readonly IHocPhiTronGoiRepository _hocPhiTronGoiRepository;

        public HocPhiTronGoiService(IHocPhiTronGoiRepository hocPhiTronGoiRepository)
        {
            _hocPhiTronGoiRepository = hocPhiTronGoiRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _hocPhiTronGoiRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_TinhHocPhi);
            return canContribute;
        }

        public async Task<bool> CreateHocPhiTronGoiAsync(CreateHocPhiTronGoiInputModel input, string loggedEmployee)
        {
            return await _hocPhiTronGoiRepository.CreateHocPhiTronGoiAsync(input, loggedEmployee);
        }

        public async Task<bool> ToggleHocPhiTronGoiAsync(Guid id, string loggedEmployee)
        {
            return await _hocPhiTronGoiRepository.ToggleHocPhiTronGoiAsync(id, loggedEmployee);
        }

        public async Task<List<HocPhiTronGoiViewModel>> GetHocPhiTronGoiAsync()
        {
            return await _hocPhiTronGoiRepository.GetHocPhiTronGoiAsync();
        }

        public async Task<HocPhiTronGoiViewModel> UpdateHocPhiTronGoiAsync(UpdateHocPhiTronGoiInputModel input, string loggedEmployee)
        {
            var value = await _hocPhiTronGoiRepository.UpdateHocPhiTronGoiAsync(input, loggedEmployee);
            return await _hocPhiTronGoiRepository.GetHocPhiTronGoiDetailAsync(value);
        }

        public async Task<bool> CheckIsDisable()
        {
            return await _hocPhiTronGoiRepository.CheckIsDisable();
        }

        public async Task<bool> DeleteHocPhiTronGoiAsync(Guid id, string loggedEmployee)
        {
            return await _hocPhiTronGoiRepository.DeleteHocPhiTronGoiAsync(id, loggedEmployee);
        }

        public async Task<bool> CanContributeHocPhiTronGoiAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _hocPhiTronGoiRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_HocPhiTronGoi);
            return canContribute;
        }
    }
}
