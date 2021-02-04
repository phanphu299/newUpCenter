namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface ILopHocService
    {
        Task<List<LopHocViewModel>> GetLopHocAsync();

        Task<LopHocViewModel> GetLopHocDetailAsync(Guid id);

        Task<List<LopHocViewModel>> GetAvailableLopHocAsync(int? thang = null, int? nam = null);

        Task<List<LopHocViewModel>> GetGraduatedAndCanceledLopHocAsync();

        Task<List<LopHocViewModel>> GetLopHocByHocVienIdAsync(Guid hocVienId);

        Task<LopHocViewModel> CreateLopHocAsync(CreateLopHocInputModel input, string loggedEmployee);

        Task<LopHocViewModel> UpdateLopHocAsync(UpdateLopHocInputModel input, string loggedEmployee);

        Task<bool> ToggleHuyLopAsync(Guid id, string loggedEmployee);

        Task<bool> ToggleTotNghiepAsync(Guid id, string loggedEmployee);

        Task<bool> DeleteLopHocAsync(Guid id, string loggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal user);

        Task<bool> UpdateHocPhiLopHocAsync(TinhHocPhiInputModel input, string loggedEmployee);
    }
}
