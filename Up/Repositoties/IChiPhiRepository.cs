using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IChiPhiRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<TinhChiPhiViewModel> TinhChiPhiAsync(int month, int year);

        Task<bool> ThemThongKe_ChiPhiAsync(ThongKe_ChiPhiViewModel[] model, DateTime ngayChiPhi, string loggedEmployee);
    }
}
