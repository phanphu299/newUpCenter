using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Data.Entities;
using Up.Models;

namespace Up.Repositoties
{
    public interface IHocPhiRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<List<HocPhiViewModel>> GetHocPhiAsync();

        Task<Guid> CreateHocPhiAsync(CreateHocPhiInputModel input, string loggedEmployee);

        Task<Guid> UpdateHocPhiAsync(UpdateHocPhiInputModel input, string loggedEmployee);

        Task<HocPhiViewModel> GetHocPhiDetailAsync(Guid id);

        Task<bool> DeleteHocPhiAsync(Guid id, string loggedEmployee);

        int TinhSoNgayHocVienVoSauAsync(int year, int month, DateTime ngayBatDau, Guid lopHocId);

        Task<int> TinhSoNgayDuocChoNghiAsync(Guid lopHocId, int month, int year);

        Task<double?> GetHocPhiCuAsync(Guid lopHocId, int month, int year);

        IQueryable<HocVien_LopHoc> GetHocVien_LopHocsEntity(Guid lopHocId, int month, int year);

        bool IsTronGoi(Guid hocVienId, Guid lopHocId, int month, int year);

        int TinhSoNgayHocTronGoi(Guid hocVienId, Guid lopHocId, int month, int year);
    }
}
