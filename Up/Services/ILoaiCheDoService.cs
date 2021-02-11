namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface ILoaiCheDoService
    {
        Task<List<LoaiCheDoViewModel>> GetLoaiCheDoAsync();
        Task<LoaiCheDoViewModel> CreateLoaiCheDoAsync(string name, string loggedEmployee);
        Task<bool> UpdateLoaiCheDoAsync(Guid id, string name, string loggedEmployee);
        Task<bool> DeleteLoaiCheDoAsync(Guid id, string loggedEmployee);
        Task<bool> CanContributeAsync(ClaimsPrincipal user);
    }
}
