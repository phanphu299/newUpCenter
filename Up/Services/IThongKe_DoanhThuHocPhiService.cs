
namespace Up.Services
{
    using System;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IThongKe_DoanhThuHocPhiService
    {
        Task<bool> ThemThongKe_DoanhThuHocPhiAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee);
        Task<bool> Undo_DoanhThuAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee);
    }
}
