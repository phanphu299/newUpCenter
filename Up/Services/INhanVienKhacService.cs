
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface INhanVienKhacService
    {
        Task<List<NhanVienKhacViewModel>> GetNhanVienAsync();
        Task<NhanVienKhacViewModel> CreateNhanVienAsync(string Name, string Phone, double BasicSalary, string FacebookAccount, string DiaChi, string CMND, string LoggedEmployee);
        Task<NhanVienKhacViewModel> UpdateNhanVienAsync(Guid NhanVienKhacId, string Name, string Phone, double BasicSalary, string FacebookAccount, string DiaChi, string CMND, string LoggedEmployee);
        Task<bool> DeleteNhanVienAsync(Guid NhanVienKhacId, string LoggedEmployee);
    }
}
