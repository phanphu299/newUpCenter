using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface IGioHocService
    {
        Task<List<GioHocViewModel>> GetGioHocAsync();
        Task<GioHocViewModel> CreateGioHocAsync(string From, string To, string LoggedEmployee);
        Task<GioHocViewModel> UpdateGioHocAsync(Guid GioHocId, string From, string To, string LoggedEmployee);
        Task<bool> DeleteGioHocAsync(Guid GioHocId, string LoggedEmployee);
    }
}
