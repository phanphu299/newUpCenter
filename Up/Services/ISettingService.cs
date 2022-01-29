
namespace Up.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface ISettingService
    {
        Task<List<AccountInfo>> GetAdminsAsync();
        Task<List<AccountInfo>> GetAllUsersAsync();
        Task<List<AccountInfo>> GetAllUsersByRoleNameAsync(string RoleName);
        Task<bool> ChangePasswordAsync(string UserId, string NewPassword = Constants.Default_Reset_Password);
        Task<bool> DisableAsync(string UserId);
        Task<bool> ActiveAsync(string UserId);
        Task<bool> RemoveUserAsync(string UserId);
        Task<List<RoleViewModel>> GetAllRolesAsync();
        Task<bool> EditRolesAsync(string RoleId, string Name);
        Task<AccountInfo> AddRolesToUserAsync(string UserId, List<string> RoleIds);
        Task<AccountInfo> CreateNewUserAsync(string Email);
        Task<RoleViewModel> CreateNewRoleAsync(string Name);
    }
}
