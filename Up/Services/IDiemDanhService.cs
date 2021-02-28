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
        Task<bool> DuocNghi(Guid LopHocId, DateTime NgayDiemDanh, string LoggedEmployee);
        Task<List<DiemDanhViewModel>> GetDiemDanhByHocVienAndLopHoc(Guid hocVienId, Guid lopHocId);
        Task<List<DiemDanhViewModel>> GetDiemDanhByLopHoc(Guid lopHocId, int month, int year);
        Task<bool> UndoDuocNghi(Guid LopHocId, DateTime NgayDiemDanh, string LoggedEmployee);
        Task<bool> SaveHocVienOff(Guid LopHocId, List<Guid> HocVienIds, List<DateTime> NgayDiemDanhs, string LoggedEmployee);
        Task<bool> SaveHocVienHoanTac(Guid LopHocId, List<Guid> HocVienIds, List<DateTime> NgayDiemDanhs, string LoggedEmployee);

        Task<List<int>> SoNgayHocAsync(Guid lopHocId, int month, int year);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);
        Task<bool> CanContributeExportAsync(ClaimsPrincipal user);
    }
}
