
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
        Task<NgayLamViecViewModel> CreateNgayLamViecAsync(string name, string loggedEmployee);
        Task<bool> UpdateNgayLamViecAsync(Guid id, string name, string loggedEmployee);
        Task<bool> DeleteNgayLamViecAsync(Guid id, string loggedEmployee);
        Task<bool> CanContributeAsync(ClaimsPrincipal user);
    }
}
