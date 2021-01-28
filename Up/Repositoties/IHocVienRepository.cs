using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IHocVienRepository
    {
        Task<Guid> CreateHocVienAsync(CreateHocVienInputModel input, string loggedEmployee);

        Task<Guid> ImportHocVienAsync(ImportHocVienInputModel input, string loggedEmployee);

        Task<HocVienViewModel> GetHocVienDetailAsync(Guid id);

        Task<List<HocVienViewModel>> GetHocVienAsync();

        Task<List<HocVienViewModel>> GetAllHocVienAsync();

        Task<List<HocVienLightViewModel>> GetHocVienByNameAsync(string name);

        Task<Guid> UpdateHocVienAsync(UpdateHocVienInputModel input, string loggedEmployee);

        Task<bool> DeleteHocVienAsync(Guid hocVienId, string loggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<bool> AddToUnavailableClassAsync(List<Guid> lopHocIds, Guid hocVienId, string loggedEmployee);
    }
}
