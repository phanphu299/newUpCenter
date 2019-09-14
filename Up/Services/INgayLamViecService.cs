
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface INgayLamViecService
    {
        Task<List<NgayLamViecViewModel>> GetNgayLamViecAsync();
        Task<NgayLamViecViewModel> CreateNgayLamViecAsync(string Name, string LoggedEmployee);
        Task<bool> UpdateNgayLamViecAsync(Guid NgayLamViecId, string Name, string LoggedEmployee);
        Task<bool> DeleteNgayLamViecAsync(Guid NgayLamViecId, string LoggedEmployee);
        Task<bool> CanContributeAsync(ClaimsPrincipal User);
    }
}
