using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IDiemDanhRepository
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);

        Task<List<HocVienViewModel>> GetHocVienByLopHoc(Guid lopHocId);

        Task<List<DiemDanhViewModel>> GetDiemDanhByHocVienAndLopHoc(Guid hocVienId, Guid lopHocId);

        Task<List<DiemDanhViewModel>> GetDiemDanhByLopHoc(Guid lopHocId, int month, int year);

        Task<List<int>> SoNgayHocAsync(Guid lopHocId, int month, int year);

        Task<bool> DiemDanhTungHocVienAsync(DiemDanhHocVienInput input, string loggedEmployee);

        Task<bool> CheckDaDiemDanhAsync(Guid lopHocId, DateTime ngayDiemDanh);

        Task<bool> DiemDanhTatCaAsync(DiemDanhHocVienInput input, string loggedEmployee);

        Task<List<Guid>> GetDiemDanhByLopHocAndNgayDiemDanhAsync(Guid lopHocId, DateTime ngayDiemDanh);

        Task<List<Guid>> GetDiemDanhByLopHocAndNgayDiemDanhAndHocVienIdsAsync(Guid lopHocId, DateTime ngayDiemDanh, IList<Guid> hocVienIds);

        Task RemoveDiemDanhByIdsAsync(IList<Guid> ids);

        Task<bool> DuocNghiAsync(DiemDanhHocVienInput input, string loggedEmployee);

        Task<bool> SaveHocVienOff(Guid lopHocId, List<Guid> hocVienIds, List<DateTime> ngayDiemDanhs, string loggedEmployee);
    }
}
