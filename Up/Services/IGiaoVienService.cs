
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IGiaoVienService
    {
        Task<List<GiaoVienViewModel>> GetGiaoVienAsync();
        Task<List<GiaoVienViewModel>> GetGiaoVienOnlyAsync();
        Task<GiaoVienViewModel> CreateGiaoVienAsync(List<LoaiNhanVien_CheDoViewModel> LoaiNhanVien_CheDo, string Name, string Phone, double TeachingRate, double TutoringRate,
            double BasicSalary, string FacebookAccount, string DiaChi, string InitialName, string CMND, double HoaHong, string LoggedEmployee);
        Task<GiaoVienViewModel> UpdateGiaoVienAsync(List<LoaiNhanVien_CheDoViewModel> LoaiNhanVien_CheDo, Guid GiaoVienId, string Name, string Phone, double TeachingRate, double TutoringRate,
            double BasicSalary, string FacebookAccount, string DiaChi, string InitialName, string CMND, double HoaHong, string LoggedEmployee);
        Task<bool> DeleteGiaoVienAsync(Guid GiaoVienId, string LoggedEmployee);
    }
}
