
namespace Up.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface ISettingService
    {
        Task<List<AccountInfo>> GetAdminsAsync();
        Task<List<AccountInfo>> GetAllUsersAsync();
        Task<bool> ChangePasswordAsync(string userId, string newPassword = "M@tkhau@123");
        Task<bool> DisableAsync(string userId);
        Task<bool> ActiveAsync(string userId);
    }
}
