
namespace Up.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IQuyenService
    {
        Task<List<QuyenViewModel>> GetAllAsync();
        Task<List<QuyenViewModel>> GetAllByRoleIdAsync(string RoleId);
        Task<bool> AddQuyenToRoleAsync(AddQuyenToRoleViewModel model);
    }
}
