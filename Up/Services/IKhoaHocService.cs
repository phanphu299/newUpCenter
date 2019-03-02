using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface IKhoaHocService
    {
        Task<List<KhoaHocViewModel>> GetKhoaHocAsync();
        Task<KhoaHocViewModel> CreateKhoaHocAsync(string Name, string LoggedEmployee);
        Task<bool> UpdateKhoaHocAsync(Guid KhoaHocId, string Name, string LoggedEmployee);
        Task<bool> DeleteKhoaHocAsync(Guid KhoaHocId, string LoggedEmployee);
        Task<bool> IsCanDeleteAsync(Guid KhoaHocId);
    }
}
