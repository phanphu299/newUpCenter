using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IThuThachRepository
    {
        Task<List<ThuThachViewModel>> GetThuThachAsync();

        Task<Guid> CreateThuThachAsync(CreateThuThachInputModel input, string loggedEmployee);

        Task<Guid> UpdateThuThachAsync(UpdateThuThachInputModel input, string loggedEmployee);

        Task<ThuThachViewModel> GetThuThachDetailAsync(Guid id);

        Task<bool> DeleteThuThachAsync(Guid id, string loggedEmployee);
    }
}
