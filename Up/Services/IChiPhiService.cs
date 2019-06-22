
namespace Up.Services
{
    using System.Threading.Tasks;
    using Up.Models;

    public interface IChiPhiService
    {
        Task<TinhChiPhiViewModel> TinhChiPhiAsync();
    }
}
