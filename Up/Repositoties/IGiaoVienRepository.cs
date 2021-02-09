using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IGiaoVienRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<List<GiaoVienViewModel>> GetAllNhanVienAsync();

        Task<List<GiaoVienViewModel>> GetGiaoVienOnlyAsync();

        Task<GiaoVienViewModel> GetNhanVienDetailAsync(Guid id);

        Task<bool> DeleteGiaoVienAsync(Guid id, string loggedEmployee);

        Task<IList<Guid>> GetGiaoVienIdsByNgayLamViec(Guid ngayLamViecId);

        Task<Guid> CreateGiaoVienAsync(CreateGiaoVienInputModel input, string loggedEmployee);

        Task<Guid> UpdateGiaoVienAsync(UpdateGiaoVienInputModel input, string loggedEmployee);
    }
}
