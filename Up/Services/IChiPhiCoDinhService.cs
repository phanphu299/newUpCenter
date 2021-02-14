namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IChiPhiCoDinhService
    {
        Task<List<ChiPhiCoDinhViewModel>> GetChiPhiCoDinhAsync();

        Task<ChiPhiCoDinhViewModel> CreateChiPhiCoDinhAsync(double gia, string name, string loggedEmployee);

        Task<ChiPhiCoDinhViewModel> UpdateChiPhiCoDinhAsync(Guid id, double gia, string name, string loggedEmployee);

        Task<bool> DeleteChiPhiCoDinhAsync(Guid id, string loggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);
    }
}
