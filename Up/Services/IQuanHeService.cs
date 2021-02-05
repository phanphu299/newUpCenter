namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IQuanHeService
    {
        Task<List<QuanHeViewModel>> GetQuanHeAsync();
        Task<QuanHeViewModel> CreateQuanHeAsync(string name, string loggedEmployee);
        Task<bool> UpdateQuanHeAsync(Guid id, string name, string loggedEmployee);
        Task<bool> DeleteQuanHeAsync(Guid id, string loggedEmployee);
        Task<bool> CanContributeAsync(ClaimsPrincipal user);
    }
}
