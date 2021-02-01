namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface ISachService
    {
        Task<IList<SachViewModel>> GetSachAsync();

        Task<SachViewModel> CreateSachAsync(CreateSachInputModel input, string loggedEmployee);

        Task<bool> UpdateSachAsync(UpdateSachInputModel input, string loggedEmployee);

        Task<bool> DeleteSachAsync(Guid id, string loggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);
    }
}
