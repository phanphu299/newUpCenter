﻿
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface INgayHocService
    {
        Task<List<NgayHocViewModel>> GetNgayHocAsync();
        Task<NgayHocViewModel> CreateNgayHocAsync(string Name, string LoggedEmployee);
        Task<bool> UpdateNgayHocAsync(Guid NgayHocId, string Name, string LoggedEmployee);
        Task<bool> DeleteNgayHocAsync(Guid NgayHocId, string LoggedEmployee);

        Task<HocVien_NgayHocViewModel> GetHocVien_NgayHocByHocVienAsync(Guid HocVienId, Guid LopHocId);
        Task<bool> CreateUpdateHocVien_NgayHocAsync(Guid HocVienId, Guid LopHocId, DateTime NgayBatDau, DateTime? NgayKetThuc, string LoggedEmployee);
    }
}
