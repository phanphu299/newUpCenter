
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface INoService
    {
        Task<List<NoViewModel>> GetHocVien_No();
        Task<List<NoViewModel>> GetHocVien_NoByLopHoc(Guid LopHocId);
        Task<bool> ThemHocVien_NoAsync(Guid LopHocId, Guid HocVienId, double TienNo, DateTime NgayNo, string LoggedEmployee);
        Task<bool> XoaHocVien_NoAsync(Guid LopHocId, Guid HocVienId, DateTime NgayNo, string LoggedEmployee);
        Task<bool> Undo_NoAsync(Guid LopHocId, Guid HocVienId, int Month, int Year, string LoggedEmployee);
    }
}
