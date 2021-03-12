
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface INoService
    {
        Task<List<NoViewModel>> GetHocVien_No();
        Task<List<NoViewModel>> GetHocVien_NoByLopHoc(Guid lopHocId);
        Task<bool> ThemHocVien_NoAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee);
        Task<bool> Undo_NoAsync(Guid LopHocId, Guid HocVienId, int Month, int Year, string LoggedEmployee);
    }
}
