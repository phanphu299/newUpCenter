using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IChiPhiKhacRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<List<ChiPhiKhacViewModel>> GetChiPhiKhacAsync();

        Task<ChiPhiKhacViewModel> GetChiPhiKhacDetailAsync(Guid id);

        Task<bool> DeleteChiPhiKhacAsync(Guid id, string loggedEmployee);

        Task<Guid> CreateChiPhiKhacAsync(CreateChiPhiKhacInputModel input, string loggedEmployee);

        Task<Guid> UpdateChiPhiKhacAsync(UpdateChiPhiKhacInputModel input, string loggedEmployee);
    }
}
