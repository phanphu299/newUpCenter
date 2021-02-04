using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IKhoaHocRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<List<KhoaHocViewModel>> GetKhoaHocAsync();

        Task<KhoaHocViewModel> GetKhoaHocDetailAsync(Guid id);

        Task<Guid> CreateKhoaHocAsync(string name, string loggedEmployee);

        Task<IList<Guid>> GetLopHocByKhoaHocIdAsync(Guid id);

        Task<bool> DeleteKhoaHocAsync(Guid id, string loggedEmployee);

        Task<bool> UpdateKhoaHocAsync(Guid id, string name, string loggedEmployee);
    }
}
