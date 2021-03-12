using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface INoRepository
    {
        Task<List<NoViewModel>> GetHocVien_No();

        Task<List<NoViewModel>> GetHocVien_NoByLopHoc(Guid lopHocId);

        Task<bool> IsExistingAsync(Guid lopHocId, Guid hocVienId, DateTime ngayNo);

        Task AddNoAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee);

        Task UpdateNoAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee);
    }
}
