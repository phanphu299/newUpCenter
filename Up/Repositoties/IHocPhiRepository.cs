using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IHocPhiRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<List<HocPhiViewModel>> GetHocPhiAsync();

        Task<Guid> CreateHocPhiAsync(CreateHocPhiInputModel input, string loggedEmployee);

        Task<Guid> UpdateHocPhiAsync(UpdateHocPhiInputModel input, string loggedEmployee);

        Task<HocPhiViewModel> GetHocPhiDetailAsync(Guid id);

        Task<bool> DeleteHocPhiAsync(Guid id, string loggedEmployee);
    }
}
