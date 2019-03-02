using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface IHocPhiService
    {
        Task<List<HocPhiViewModel>> GetHocPhiAsync();
        Task<HocPhiViewModel> CreateHocPhiAsync(double Gia, string LoggedEmployee);
        Task<bool> UpdateHocPhiAsync(Guid HocPhiId, double Gia, string LoggedEmployee);
        Task<bool> DeleteHocPhiAsync(Guid HocPhiId, string LoggedEmployee);
        Task<bool> IsCanDeleteAsync(Guid HocPhiId);
    }
}
