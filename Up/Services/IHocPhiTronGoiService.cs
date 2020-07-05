
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IHocPhiTronGoiService
    {
        Task<List<HocPhiTronGoiViewModel>> GetHocPhiTronGoiAsync();
        Task<bool> CreateHocPhiTronGoiAsync(Guid HocVienId, double HocPhi, DateTime FromDate, DateTime ToDate, List<HocPhiTronGoi_LopHocViewModel> LopHocList, string LoggedEmployee);
        Task<HocPhiTronGoiViewModel> UpdateHocPhiTronGoiAsync(Guid HocPhiTronGoiId, double HocPhi, DateTime FromDate, DateTime ToDate, List<HocPhiTronGoi_LopHocViewModel> LopHocList, string LoggedEmployee);
        Task<bool> ToggleHocPhiTronGoiAsync(Guid HocPhiTronGoiId, string LoggedEmployee);
        Task<bool> DeleteHocPhiTronGoiAsync(Guid HocPhiTronGoiId, string LoggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal User);
        Task<bool> CheckIsDisable();
    }
}
