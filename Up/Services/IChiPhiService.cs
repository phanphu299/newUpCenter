
namespace Up.Services
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IChiPhiService
    {
        Task<TinhChiPhiViewModel> TinhChiPhiAsync(int month, int year);
        Task<bool> CanContributeAsync(ClaimsPrincipal user);
    }
}
