using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface INgayLamViecRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<List<NgayLamViecViewModel>> GetNgayLamViecAsync();

        Task<NgayLamViecViewModel> GetNgayLamViecDetailAsync(Guid id);

        Task<Guid> CreateNgayLamViecAsync(string name, string loggedEmployee);

        Task<bool> DeleteNgayLamViecAsync(Guid id, string loggedEmployee);

        Task<bool> UpdateNgayLamViecAsync(Guid id, string name, string loggedEmployee);
    }
}
