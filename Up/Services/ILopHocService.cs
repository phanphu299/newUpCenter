namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface ILopHocService
    {
        Task<List<LopHocViewModel>> GetLopHocAsync();
        Task<LopHocViewModel> GetLopHocByIdAsync(Guid LopHocId);
        Task<List<LopHocViewModel>> GetAvailableLopHocAsync();
        Task<List<LopHocViewModel>> GetGraduatedAndCanceledLopHocAsync();
        Task<List<LopHocViewModel>> GetLopHocByHocVienIdAsync(Guid HocVienId);

        Task<LopHocViewModel> CreateLopHocAsync(string Name, Guid KhoaHocId, Guid NgayHocId,
            Guid GioHocId, DateTime NgayKhaiGiang, string LoggedEmployee);

        Task<LopHocViewModel> UpdateLopHocAsync(Guid LopHocId, string Name, Guid KhoaHocId,
            Guid NgayHocId, Guid GioHocId, DateTime NgayKhaiGiang,
            DateTime? NgayKetThuc, string LoggedEmployee);

        Task<bool> ToggleHuyLopAsync(Guid LopHocId, string LoggedEmployee);

        Task<bool> ToggleTotNghiepAsync(Guid LopHocId, string LoggedEmployee);

        Task<bool> DeleteLopHocAsync(Guid LopHocId, string LoggedEmployee);
    }
}
