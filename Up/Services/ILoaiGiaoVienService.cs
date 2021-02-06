
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface ILoaiGiaoVienService
    {
        Task<List<LoaiGiaoVienViewModel>> GetLoaiGiaoVienAsync();
        Task<LoaiGiaoVienViewModel> CreateLoaiGiaoVienAsync(CreateLoaiGiaoVienInputModel input, string loggedEmployee);
        Task<bool> UpdateLoaiGiaoVienAsync(UpdateLoaiGiaoVienInputModel input, string loggedEmployee);
        Task<bool> DeleteLoaiGiaoVienAsync(Guid id, string loggedEmployee);
        Task<bool> CanContributeAsync(ClaimsPrincipal user);
    }
}
