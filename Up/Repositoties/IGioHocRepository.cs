using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IGioHocRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<List<GioHocViewModel>> GetGioHocAsync();

        Task<GioHocViewModel> GetGioHocDetailAsync(Guid id);

        Task<Guid> CreateGioHocAsync(CreateGioHocInputModel input, string loggedEmployee);

        Task<Guid> UpdateGioHocAsync(UpdateGioHocInputModel input, string loggedEmployee);

        Task<bool> DeleteGioHocAsync(Guid id, string loggedEmployee);
    }
}
