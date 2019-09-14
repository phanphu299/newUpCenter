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
        Task<QuanHeViewModel> CreateQuanHeAsync(string Name, string LoggedEmployee);
        Task<bool> UpdateQuanHeAsync(Guid QuanHeId, string Name, string LoggedEmployee);
        Task<bool> DeleteQuanHeAsync(Guid QuanHeId, string LoggedEmployee);
        Task<bool> CanContributeAsync(ClaimsPrincipal User);
    }
}
