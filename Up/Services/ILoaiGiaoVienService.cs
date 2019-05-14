
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface ILoaiGiaoVienService
    {
        Task<List<LoaiGiaoVienViewModel>> GetLoaiGiaoVienAsync();
        Task<LoaiGiaoVienViewModel> CreateLoaiGiaoVienAsync(string Name, string LoggedEmployee);
        Task<bool> UpdateLoaiGiaoVienAsync(Guid LoaiGiaoVienId, string Name, string LoggedEmployee);
        Task<bool> DeleteLoaiGiaoVienAsync(Guid LoaiGiaoVienId, string LoggedEmployee);
    }
}
