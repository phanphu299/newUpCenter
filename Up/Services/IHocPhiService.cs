
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IHocPhiService
    {
        Task<List<HocPhiViewModel>> GetHocPhiAsync();

        Task<HocPhiViewModel> CreateHocPhiAsync(CreateHocPhiInputModel input, string loggedEmployee);

        Task<HocPhiViewModel> UpdateHocPhiAsync(UpdateHocPhiInputModel input, string loggedEmployee);

        Task<bool> DeleteHocPhiAsync(Guid id, string loggedEmployee);

        Task<TinhHocPhiViewModel> TinhHocPhiAsync(TinhHocPhiInputModel input);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);

        Task<bool> CanContributeTinhHocPhiAsync(ClaimsPrincipal user);
    }
}
