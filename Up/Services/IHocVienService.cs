namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IHocVienService
    {
        Task<List<HocVienViewModel>> GetHocVienAsync();

        Task<List<HocVienViewModel>> GetAllHocVienAsync();

        Task<HocVienViewModel> CreateHocVienAsync(CreateHocVienInputModel input, string loggedEmployee);

        Task<HocVienViewModel> ImportHocVienAsync(ImportHocVienInputModel input, string loggedEmployee);

        Task<HocVienViewModel> UpdateHocVienAsync(UpdateHocVienInputModel input, string loggedEmployee);

        Task<bool> DeleteHocVienAsync(Guid hocVienId, string loggedEmployee);

        Task<bool> AddToUnavailableClassAsync(List<Guid> lopHocIds, Guid hocVienId, string loggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);

        Task<List<HocVienLightViewModel>> GetHocVienByNameAsync(string name);

        Task<HocVienLightViewModel> GetHocVienByTrigramAsync(string trigram);

        Task<HocVienViewModel> GetHocVienDetailAsync(Guid id);
    }
}