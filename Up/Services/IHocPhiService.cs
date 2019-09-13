
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IHocPhiService
    {
        Task<List<HocPhiViewModel>> GetHocPhiAsync(ClaimsPrincipal User);
        Task<HocPhiViewModel> CreateHocPhiAsync(double Gia, string GhiChu, DateTime NgayApDung, string LoggedEmployee);
        Task<HocPhiViewModel> UpdateHocPhiAsync(Guid HocPhiId, double Gia, string GhiChu, DateTime NgayApDung, string LoggedEmployee);
        Task<bool> DeleteHocPhiAsync(Guid HocPhiId, string LoggedEmployee);

        Task<int> TinhSoNgayHocAsync(Guid LopHocId, int month, int year);
        Task<List<int>> SoNgayHocAsync(Guid LopHocId, int month, int year);
        Task<int> TinhSoNgayDuocChoNghiAsync(Guid LopHocId, int month, int year);
        Task<List<HocVienViewModel>> GetHocVien_No_NgayHocAsync(Guid LopHocId, int month, int year, double HocPhi, int SoNgayHoc, double HocPhiMoiNgay);
        Task<TinhHocPhiViewModel> TinhHocPhiAsync(Guid LopHocId, int month, int year, double HocPhi);
    }
}
