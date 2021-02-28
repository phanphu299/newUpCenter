
namespace Up.Services
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IChiPhiService
    {
        Task<TinhChiPhiViewModel> TinhChiPhiAsync(int month, int year);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);

        Task<bool> ThemThongKe_ChiPhiAsync(ThongKe_ChiPhiViewModel[] model, DateTime ngayChiPhi, string loggedEmployee);
    }
}
