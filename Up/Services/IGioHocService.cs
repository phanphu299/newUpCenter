using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface IGioHocService
    {
        Task<List<GioHocViewModel>> GetGioHocAsync();
        Task<GioHocViewModel> CreateGioHocAsync(string Name, string LoggedEmployee);
        Task<bool> UpdateGioHocAsync(Guid GioHocId, string Name, string LoggedEmployee);
        Task<bool> DeleteGioHocAsync(Guid GioHocId, string LoggedEmployee);
        Task<bool> IsCanDeleteAsync(Guid GioHocId);
    }
}
