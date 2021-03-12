
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IThongKe_DoanhThuHocPhiService
    {
        Task<bool> ThemThongKe_DoanhThuHocPhiAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee);
        Task<bool> Undo_DoanhThuAsync(Guid LopHocId, Guid HocVienId, int Month, int Year, string LoggedEmployee);
    }
}
