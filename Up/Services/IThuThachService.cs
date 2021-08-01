using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface IThuThachService
    {
        Task<List<ThuThachViewModel>> GetThuThachAsync();

        Task<ThuThachViewModel> CreateThuThachAsync(CreateThuThachInputModel input, string loggedEmployee);

        Task<ThuThachViewModel> UpdateThuThachAsync(UpdateThuThachInputModel input, string loggedEmployee);

        Task<bool> DeleteThuThachAsync(Guid id, string loggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);

        Task<bool> CanContributeExportAsync(ClaimsPrincipal user);

        Task<List<ThuThachViewModel>> GetThuThachByHocVienAsync(Guid hocVienId);

        Task<List<CauHoiViewModel>> GetCauHoiAsync(Guid thuThachId);

        Task<int> LuuKetQuaAsync(ResultInputModel input);

        Task<List<HocVienViewModel>> GetKetQuaAsync(Guid lopHocId, int month, int year);
    }
}
