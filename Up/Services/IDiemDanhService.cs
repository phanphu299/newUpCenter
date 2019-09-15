namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IDiemDanhService
    {
        Task<List<HocVienViewModel>> GetHocVienByLopHoc(Guid LopHocId);
        Task<bool> DiemDanhTungHocVienAsync(Guid LopHocId, Guid HocVienId, bool isOff, DateTime NgayDiemDanh, string LoggedEmployee);
        Task<bool> DiemDanhTatCaAsync(Guid LopHocId, bool isOff, DateTime NgayDiemDanh, string LoggedEmployee);
        Task<bool> DuocNghi(Guid LopHocId, DateTime NgayDiemDanh, string LoggedEmployee);
        Task<List<DiemDanhViewModel>> GetDiemDanhByHocVienAndLopHoc(Guid HocVienId, Guid LopHocId);
        Task<List<DiemDanhViewModel>> GetDiemDanhByLopHoc(Guid LopHocId, int month, int year);
        Task<bool> UndoDuocNghi(Guid LopHocId, DateTime NgayDiemDanh, string LoggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal User);
        Task<bool> CanContributeExportAsync(ClaimsPrincipal User);
    }
}
