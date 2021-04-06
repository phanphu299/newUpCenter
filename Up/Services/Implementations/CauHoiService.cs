using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;
using Up.Repositoties;

namespace Up.Services
{
    public class CauHoiService : ICauHoiService
    {
        private readonly ICauHoiRepository _cauHoiRepository;

        public CauHoiService(ICauHoiRepository cauHoiRepository)
        {
            _cauHoiRepository = cauHoiRepository;
        }

        public Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public async Task<CauHoiViewModel> CreateCauHoiAsync(CreateCauHoiInputModel input, string loggedEmployee)
        {
            var result = await _cauHoiRepository.CreateCauHoiAsync(input, loggedEmployee);
            return await _cauHoiRepository.GetCauHoiDetailAsync(result);
        }

        public async Task<List<CauHoiViewModel>> GetCauHoiAsync()
        {
            return await _cauHoiRepository.GetCauHoiAsync();
        }
    }
}
