namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IDiemDanhService
    {
        Task<List<HocVienViewModel>> GetHocVienByLopHoc(Guid lopHocId);
        Task<bool> DiemDanhTungHocVienAsync(DiemDanhHocVienInput input, string loggedEmployee);
        Task<bool> DiemDanhTatCaAsync(DiemDanhHocVienInput input, string loggedEmployee);
        Task<bool> DuocNghiAsync(DiemDanhHocVienInput input, string loggedEmployee);
        Task<List<DiemDanhViewModel>> GetDiemDanhByHocVienAndLopHoc(Guid hocVienId, Guid lopHocId);
        Task<List<DiemDanhViewModel>> GetDiemDanhByLopHoc(Guid lopHocId, int month, int year);
        Task<bool> UndoDuocNghi(DiemDanhHocVienInput input, string loggedEmployee);
        Task<bool> SaveHocVienOff(Guid lopHocId, List<Guid> hocVienIds, List<DateTime> ngayDiemDanhs, string loggedEmployee);
        Task<bool> SaveHocVienHoanTac(Guid lopHocId, List<Guid> hocVienIds, List<DateTime> ngayDiemDanhs);

        Task<List<int>> SoNgayHocAsync(Guid lopHocId, int month, int year);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);
        Task<bool> CanContributeExportAsync(ClaimsPrincipal user);
    }
}
