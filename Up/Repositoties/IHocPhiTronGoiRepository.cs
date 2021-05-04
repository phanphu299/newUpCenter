using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IHocPhiTronGoiRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<List<HocPhiTronGoiViewModel>> GetHocPhiTronGoiAsync();

        Task<HocPhiTronGoiViewModel> GetHocPhiTronGoiDetailAsync(Guid id);

        Task<bool> CheckIsDisable();

        Task<bool> CreateHocPhiTronGoiAsync(CreateHocPhiTronGoiInputModel input, string loggedEmployee);

        Task<bool> ToggleHocPhiTronGoiAsync(Guid id, string loggedEmployee);

        Task<bool> DeleteHocPhiTronGoiAsync(Guid id, string loggedEmployee);

        Task<Guid> UpdateHocPhiTronGoiAsync(UpdateHocPhiTronGoiInputModel input, string loggedEmployee);

        Task<List<HocPhiTronGoiViewModel>> GetHocPhiTronGoiAsync(Guid hocVienId, Guid lopHocId);
    }
}
