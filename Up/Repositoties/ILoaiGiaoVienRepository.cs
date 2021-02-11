using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface ILoaiGiaoVienRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<List<LoaiGiaoVienViewModel>> GetLoaiGiaoVienAsync();

        Task<LoaiGiaoVienViewModel> GetLoaiGiaoVienDetailAsync(Guid id);

        Task<Guid> CreateLoaiGiaoVienAsync(CreateLoaiGiaoVienInputModel input, string loggedEmployee);

        Task<bool> UpdateLoaiGiaoVienAsync(UpdateLoaiGiaoVienInputModel input, string loggedEmployee);

        Task<IList<Guid>> GetNhanVienIdsAsync(Guid id);

        Task<IList<Guid>> GetNhanVienIdsByLoaiCheDoAsync(Guid id);

        Task<bool> DeleteLoaiGiaoVienAsync(Guid id, string loggedEmployee);
    }
}
