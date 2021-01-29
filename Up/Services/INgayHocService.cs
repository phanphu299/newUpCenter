
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface INgayHocService
    {
        Task<List<NgayHocViewModel>> GetNgayHocAsync();
        Task<NgayHocViewModel> CreateNgayHocAsync(string name, string loggedEmployee);

        Task<bool> UpdateNgayHocAsync(NgayHocViewModel input, string loggedEmployee);

        Task<bool> DeleteNgayHocAsync(Guid ngayHocId, string loggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);

        Task<HocVien_NgayHocViewModel> GetHocVien_NgayHocByHocVienAsync(Guid hocVienId, Guid lopHocId);

        Task<bool> CreateUpdateHocVien_NgayHocAsync(HocVien_NgayHocInputModel input, string loggedEmployee);
    }
}
