using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IQuanHeRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<List<QuanHeViewModel>> GetQuanHeAsync();

        Task<QuanHeViewModel> GetQuanHeDetailAsync(Guid id);

        Task<Guid> CreateQuanHeAsync(string name, string loggedEmployee);

        Task<bool> UpdateQuanHeAsync(Guid id, string name, string loggedEmployee);

        Task<bool> DeleteQuanHeAsync(Guid id, string loggedEmployee);
    }
}
