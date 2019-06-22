
namespace Up.Services
{
    using System;
    using System.Threading.Tasks;

    public interface IThongKe_ChiPhiService
    {
        Task<bool> ThemThongKe_ChiPhiAsync(double ChiPhi, DateTime NgayChiPhi, string LoggedEmployee);
    }
}
