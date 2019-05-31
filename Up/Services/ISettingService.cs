
namespace Up.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface ISettingService
    {
        Task<List<AccountInfo>> GetAdminsAsync();
        Task<List<AccountInfo>> GetAllUsersAsync();
    }
}
