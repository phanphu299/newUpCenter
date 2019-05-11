namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IDiemDanhService
    {
        Task<List<HocVienViewModel>> GetHocVienByLopHoc(Guid LopHocId);
        Task<bool> DiemDanhTungHocVienAsync(Guid LopHocId, Guid HocVienId, bool isOff, DateTime NgayDiemDanh, string LoggedEmployee);
        Task<bool> DiemDanhTatCaAsync(Guid LopHocId, bool isOff, DateTime NgayDiemDanh, string LoggedEmployee);
        Task<bool> DuocNghi(Guid LopHocId, DateTime NgayDiemDanh, string LoggedEmployee);
    }
}
