using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface IHocVienService
    {
        Task<List<HocVienViewModel>> GetHocVienAsync();
        Task<HocVienViewModel> CreateHocVienAsync(string FullName, string Phone, string FacebookAccount,
           string ParentFullName, string ParentPhone, string ParentFacebookAccount, Guid QuanHeId, string EnglishName,
           DateTime NgaySinh, bool IsAppend, string LoggedEmployee);
        Task<bool> UpdateHocVienAsync(Guid HocVienId, string Name, string LoggedEmployee);
        Task<bool> DeleteHocVienAsync(Guid HocVienId, string LoggedEmployee);
    }
}