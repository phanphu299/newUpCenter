using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface ILopHocService
    {
        Task<List<LopHocViewModel>> GetLopHocAsync();
        Task<List<LopHocViewModel>> GetAvailableLopHocAsync();
        Task<List<LopHocViewModel>> GetGraduatedAndCanceledLopHocAsync();
        Task<List<LopHocViewModel>> GetLopHocByHocVienIdAsync(Guid HocVienId);
        Task<LopHocViewModel> CreateLopHocAsync(string Name, Guid KhoaHocId, Guid NgayHocId,
            Guid GioHocId, Guid HocPhiId, DateTime NgayKhaiGiang, Guid[] SachIds, Guid GiaoVienId, string LoggedEmployee);

        Task<LopHocViewModel> UpdateLopHocAsync(Guid LopHocId, string Name, Guid KhoaHocId,
            Guid NgayHocId, Guid GioHocId, Guid HocPhiId, DateTime NgayKhaiGiang,
            DateTime? NgayKetThuc, Guid[] SachIds, Guid GiaoVienId, string LoggedEmployee);

        Task<bool> ToggleHuyLopAsync(Guid LopHocId, string LoggedEmployee);

        Task<bool> ToggleTotNghiepAsync(Guid LopHocId, string LoggedEmployee);

        Task<bool> DeleteLopHocAsync(Guid LopHocId, string LoggedEmployee);
    }
}
