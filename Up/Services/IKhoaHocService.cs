using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface IKhoaHocService
    {
        Task<List<KhoaHocViewModel>> GetKhoaHocAsync();

        Task<KhoaHocViewModel> CreateKhoaHocAsync(string name, string loggedEmployee);

        Task<bool> UpdateKhoaHocAsync(Guid id, string name, string loggedEmployee);

        Task<bool> DeleteKhoaHocAsync(Guid id, string loggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);
    }
}
