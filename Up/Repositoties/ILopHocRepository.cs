using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface ILopHocRepository
    {
        Task<List<LopHocViewModel>> GetLopHocAsync();

        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<Guid> CreateLopHocAsync(CreateLopHocInputModel input, string loggedEmployee);

        Task<LopHocViewModel> GetLopHocDetailAsync(Guid id);

        Task<Guid> UpdateLopHocAsync(UpdateLopHocInputModel input, string loggedEmployee);

        Task<List<LopHocViewModel>> GetAvailableLopHocAsync(int? thang = null, int? nam = null);

        Task<List<LopHocViewModel>> GetGraduatedAndCanceledLopHocAsync();

        Task<List<LopHocViewModel>> GetLopHocByHocVienIdAsync(Guid hocVienId);

        Task<bool> DeleteLopHocAsync(Guid id, string loggedEmployee);

        Task<IList<Guid>> GetHocVienIdAsync(Guid id);

        Task<bool> ToggleHuyLopAsync(Guid id, string loggedEmployee);

        Task<bool> ToggleTotNghiepAsync(Guid id, string loggedEmployee);

        Task<bool> UpdateHocPhiLopHocAsync(Guid lopHocId, Guid hocPhiId, int thang, int nam, string loggedEmployee);
    }
}
