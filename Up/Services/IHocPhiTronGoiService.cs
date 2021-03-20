
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IHocPhiTronGoiService
    {
        Task<List<HocPhiTronGoiViewModel>> GetHocPhiTronGoiAsync();
        Task<bool> CreateHocPhiTronGoiAsync(CreateHocPhiTronGoiInputModel input, string loggedEmployee);
        Task<HocPhiTronGoiViewModel> UpdateHocPhiTronGoiAsync(UpdateHocPhiTronGoiInputModel input, string loggedEmployee);
        Task<bool> ToggleHocPhiTronGoiAsync(Guid id, string loggedEmployee);
        Task<bool> DeleteHocPhiTronGoiAsync(Guid id, string loggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);

        Task<bool> CanContributeHocPhiTronGoiAsync(ClaimsPrincipal user);

        Task<bool> CheckIsDisable();
    }
}
