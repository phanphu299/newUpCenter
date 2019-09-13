
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface INgayHocService
    {
        Task<List<NgayHocViewModel>> GetNgayHocAsync(ClaimsPrincipal User);
        Task<NgayHocViewModel> CreateNgayHocAsync(string Name, string LoggedEmployee, ClaimsPrincipal User);
        Task<bool> UpdateNgayHocAsync(Guid NgayHocId, string Name, string LoggedEmployee);
        Task<bool> DeleteNgayHocAsync(Guid NgayHocId, string LoggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal User);

        Task<HocVien_NgayHocViewModel> GetHocVien_NgayHocByHocVienAsync(Guid HocVienId, Guid LopHocId);
        Task<bool> CreateUpdateHocVien_NgayHocAsync(Guid HocVienId, Guid LopHocId, DateTime NgayBatDau, DateTime? NgayKetThuc, string LoggedEmployee);
    }
}
