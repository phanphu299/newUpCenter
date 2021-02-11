using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface ILoaiCheDoRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<List<LoaiCheDoViewModel>> GetLoaiCheDoAsync();

        Task<LoaiCheDoViewModel> GetLoaiCheDoDetailAsync(Guid id);

        Task<Guid> CreateLoaiCheDoAsync(string name, string loggedEmployee);

        Task<bool> UpdateLoaiCheDoAsync(Guid id, string name, string loggedEmployee);

        Task<bool> DeleteLoaiCheDoAsync(Guid id, string loggedEmployee);
    }
}
