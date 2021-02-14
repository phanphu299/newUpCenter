using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IChiPhiCoDinhRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<List<ChiPhiCoDinhViewModel>> GetChiPhiCoDinhAsync();

        Task<ChiPhiCoDinhViewModel> GetChiPhiCoDinhDetailAsync(Guid id);

        Task<bool> DeleteChiPhiCoDinhAsync(Guid id, string loggedEmployee);

        Task<Guid> CreateChiPhiCoDinhAsync(double gia, string name, string loggedEmployee);

        Task<Guid> UpdateChiPhiCoDinhAsync(Guid id, double gia, string name, string loggedEmployee);
    }
}
