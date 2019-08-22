
namespace Up.Services
{
    using System;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IThongKe_ChiPhiService
    {
        Task<bool> ThemThongKe_ChiPhiAsync(ThongKe_ChiPhiViewModel[] Model, DateTime NgayChiPhi, string LoggedEmployee);
    }
}
