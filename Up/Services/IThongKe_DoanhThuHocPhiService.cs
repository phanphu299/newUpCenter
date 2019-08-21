
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IThongKe_DoanhThuHocPhiService
    {
        Task<List<ThongKe_DoanhThuHocPhiViewModel>> GetThongKe_DoanhThuHocPhiByLopHoc(Guid LopHocId);
        Task<bool> ThemThongKe_DoanhThuHocPhiAsync(Guid LopHocId, Guid HocVienId, double HocPhi, DateTime NgayDong,
            double Bonus, double Minus, int KhuyenMai, string GhiChu, Guid[] SachIds, double No, string LoggedEmployee);
    }
}
