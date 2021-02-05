

namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;
    public interface IGioHocService
    {
        Task<List<GioHocViewModel>> GetGioHocAsync();
        Task<GioHocViewModel> CreateGioHocAsync(CreateGioHocInputModel input, string loggedEmployee);
        Task<GioHocViewModel> UpdateGioHocAsync(UpdateGioHocInputModel input, string loggedEmployee);
        Task<bool> DeleteGioHocAsync(Guid id, string loggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);
    }
}
