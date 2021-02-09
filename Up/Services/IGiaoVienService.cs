
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IGiaoVienService
    {
        Task<List<GiaoVienViewModel>> GetAllNhanVienAsync();
        Task<List<GiaoVienViewModel>> GetGiaoVienOnlyAsync();
        Task<GiaoVienViewModel> CreateGiaoVienAsync(CreateGiaoVienInputModel input, string loggedEmployee);
        Task<GiaoVienViewModel> UpdateGiaoVienAsync(UpdateGiaoVienInputModel input, string loggedEmployee);
        Task<bool> DeleteGiaoVienAsync(Guid id, string loggedEmployee);
        Task<bool> CanContributeAsync(ClaimsPrincipal user);
    }
}
