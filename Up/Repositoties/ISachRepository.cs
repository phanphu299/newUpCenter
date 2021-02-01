using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface ISachRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<IList<SachViewModel>> GetSachAsync();

        Task<bool> DeleteSachAsync(Guid id, string loggedEmployee);

        Task<Guid> CreateSachAsync(CreateSachInputModel input, string loggedEmployee);

        Task<bool> UpdateSachAsync(UpdateSachInputModel input, string loggedEmployee);

        Task<SachViewModel> GetSachDetailAsync(Guid id);
    }
}
