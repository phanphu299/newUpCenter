
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IHocPhiService
    {
        Task<List<HocPhiViewModel>> GetHocPhiAsync();
        Task<HocPhiViewModel> CreateHocPhiAsync(double Gia, string GhiChu, DateTime NgayApDung, string LoggedEmployee);
        Task<HocPhiViewModel> UpdateHocPhiAsync(Guid HocPhiId, double Gia, string GhiChu, DateTime NgayApDung, string LoggedEmployee);
        Task<bool> DeleteHocPhiAsync(Guid HocPhiId, string LoggedEmployee);

        Task<int> TinhSoNgayHocAsync(Guid LopHocId, int month, int year );
    }
}
