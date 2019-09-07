namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IHocVienService
    {
        Task<List<HocVienViewModel>> GetHocVienAsync();
        Task<List<HocVienViewModel>> GetAllHocVienAsync();
        Task<HocVienViewModel> CreateHocVienAsync(List<LopHoc_NgayHocViewModel> LopHocList, string FullName, string Phone, string FacebookAccount,
           string ParentFullName, Guid? QuanHeId, string EnglishName,
           DateTime? NgaySinh, Guid[] LopHocIds, string LoggedEmployee, DateTime? NgayBatDau = null);

        Task<HocVienViewModel> UpdateHocVienAsync(List<LopHoc_NgayHocViewModel> LopHocList, Guid HocVienId, string FullName, string Phone, string FacebookAccount,
           string ParentFullName, Guid? QuanHeId, string EnglishName,
           DateTime? NgaySinh, Guid[] LopHocIds, string LoggedEmployee);

        Task<bool> DeleteHocVienAsync(Guid HocVienId, string LoggedEmployee);

        Task<bool> ToggleChenAsync(Guid HocVienId, string LoggedEmployee);

        Task<bool> AddToUnavailableClassAsync(List<Guid> LopHocId, Guid HocVienId, string LoggedEmployee);
    }
}