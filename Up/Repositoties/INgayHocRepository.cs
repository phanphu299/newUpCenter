using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface INgayHocRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<HocVien_NgayHocViewModel> GetHocVien_NgayHocByHocVienAsync(Guid hocVienId, Guid lopHocId);

        Task<bool> CreateUpdateHocVien_NgayHocAsync(HocVien_NgayHocInputModel input, string loggedEmployee);

        Task<List<NgayHocViewModel>> GetNgayHocAsync();

        Task<Guid> CreateNgayHocAsync(string name, string loggedEmployee);

        Task<NgayHocViewModel> GetNgayHocDetailAsync(Guid id);

        Task<bool> UpdateNgayHocAsync(NgayHocViewModel input, string loggedEmployee);

        Task<bool> DeleteNgayHocAsync(Guid ngayHocId, string loggedEmployee);
    }
}
