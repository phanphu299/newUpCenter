using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface IThuThachService
    {
        Task<List<ThuThachViewModel>> GetThuThachAsync();

        Task<ThuThachViewModel> CreateThuThachAsync(CreateThuThachInputModel input, string loggedEmployee);

        Task<ThuThachViewModel> UpdateThuThachAsync(UpdateThuThachInputModel input, string loggedEmployee);

        Task<bool> DeleteThuThachAsync(Guid id, string loggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);
    }
}
