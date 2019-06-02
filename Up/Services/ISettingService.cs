
namespace Up.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface ISettingService
    {
        Task<List<AccountInfo>> GetAdminsAsync();
        Task<List<AccountInfo>> GetAllUsersAsync();
        Task<bool> ChangePasswordAsync(string UserId, string NewPassword = "M@tkhau@123");
        Task<bool> DisableAsync(string UserId);
        Task<bool> ActiveAsync(string UserId);
        Task<List<RoleViewModel>> GetAllRolesAsync();
        Task<AccountInfo> AddRolesToUserAsync(string UserId, List<string> RoleIds);
    }
}
