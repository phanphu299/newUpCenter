using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface INgayHocService
    {
        Task<List<NgayHocViewModel>> GetNgayHocAsync();
        Task<NgayHocViewModel> CreateNgayHocAsync(string Name, string LoggedEmployee);
        Task<bool> UpdateNgayHocAsync(Guid NgayHocId, string Name, string LoggedEmployee);
        Task<bool> DeleteNgayHocAsync(Guid NgayHocId, string LoggedEmployee);
        Task<bool> IsCanDeleteAsync(Guid NgayHocId);
    }
}
