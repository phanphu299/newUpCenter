

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
        Task<GioHocViewModel> CreateGioHocAsync(string From, string To, string LoggedEmployee);
        Task<GioHocViewModel> UpdateGioHocAsync(Guid GioHocId, string From, string To, string LoggedEmployee);
        Task<bool> DeleteGioHocAsync(Guid GioHocId, string LoggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal User);
    }
}
