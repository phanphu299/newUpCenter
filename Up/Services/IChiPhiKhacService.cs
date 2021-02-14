
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IChiPhiKhacService
    {
        Task<List<ChiPhiKhacViewModel>> GetChiPhiKhacAsync();
        Task<ChiPhiKhacViewModel> CreateChiPhiKhacAsync(CreateChiPhiKhacInputModel input, string loggedEmployee);
        Task<ChiPhiKhacViewModel> UpdateChiPhiKhacAsync(UpdateChiPhiKhacInputModel input, string loggedEmployee);
        Task<bool> DeleteChiPhiKhacAsync(Guid id, string loggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);
    }
}
