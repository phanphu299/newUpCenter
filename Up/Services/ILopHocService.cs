using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface ILopHocService
    {
        Task<List<LopHocViewModel>> GetLopHocAsync();
        Task<LopHocViewModel> CreateLopHocAsync(string Name, Guid KhoaHocId, Guid NgayHocId,
            Guid GioHocId, Guid HocPhiId, DateTime NgayKhaiGiang, Guid[] SachIds, Guid GiaoVienId, string LoggedEmployee);

        Task<LopHocViewModel> UpdateLopHocAsync(Guid LopHocId, string Name, Guid KhoaHocId,
            Guid NgayHocId, Guid GioHocId, Guid HocPhiId, DateTime NgayKhaiGiang,
            DateTime? NgayKetThuc, bool HuyLop, bool TotNghiep, Guid[] SachIds, Guid GiaoVienId, string LoggedEmployee);

        Task<bool> DeleteLopHocAsync(Guid LopHocId, string LoggedEmployee);
    }
}
