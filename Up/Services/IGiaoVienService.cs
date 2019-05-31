
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IGiaoVienService
    {
        Task<List<GiaoVienViewModel>> GetGiaoVienAsync();
        Task<GiaoVienViewModel> CreateGiaoVienAsync(string Name, Guid LoaiGiaoVienId, string Phone, double TeachingRate, double TutoringRate,
            double BasicSalary, string FacebookAccount, string DiaChi, string InitialName, string CMND, string LoggedEmployee);
        Task<GiaoVienViewModel> UpdateGiaoVienAsync(Guid GiaoVienId, string Name, Guid LoaiGiaoVienId, string Phone, double TeachingRate, double TutoringRate,
            double BasicSalary, string FacebookAccount, string DiaChi, string InitialName, string CMND, string LoggedEmployee);
        Task<bool> DeleteGiaoVienAsync(Guid GiaoVienId, string LoggedEmployee);
    }
}
