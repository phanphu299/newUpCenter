
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IChiPhiKhacService
    {
        Task<List<ChiPhiKhacViewModel>> GetChiPhiKhacAsync();
        Task<ChiPhiKhacViewModel> CreateChiPhiKhacAsync(string Name, double Gia, DateTime NgayChiPhi, string LoggedEmployee);
        Task<ChiPhiKhacViewModel> UpdateChiPhiKhacAsync(Guid ChiPhiKhacId, string Name, double Gia, DateTime NgayChiPhi, string LoggedEmployee);
        Task<bool> DeleteChiPhiKhacAsync(Guid ChiPhiKhacId, string LoggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal User);
    }
}
