using System;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IThongKe_DoanhThuHocPhiRepository
    {
        Task<bool> IsExistingAsync(Guid hocVienId, Guid lopHocId, DateTime ngayDong);

        Task AddThongKe_DoanhThuAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee);

        Task UpdateThongKe_DoanhThuAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee);

        Task XoaDaDongThongKe_DoanhThuAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee);

    }
}
