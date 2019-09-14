

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
        Task<LoaiCheDoViewModel> CreateLoaiCheDoAsync(string Name, string LoggedEmployee);
        Task<bool> UpdateLoaiCheDoAsync(Guid LoaiCheDoId, string Name, string LoggedEmployee);
        Task<bool> DeleteLoaiCheDoAsync(Guid LoaiCheDoId, string LoggedEmployee);
        Task<bool> CanContributeAsync(ClaimsPrincipal User);
    }
}
