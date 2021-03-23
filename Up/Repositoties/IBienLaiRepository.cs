using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IBienLaiRepository
    {
        Task<string> GetLastestMaBienLaiAsync();

        Task CreateBienLaiAsync(CreateBienLaiInputModel input, string loggedEmployee);

        Task<bool> IsExistMaBienLaiAsync(string maBienLai);

        Task<List<BienLaiViewModel>> GetBienLaiAsync();

        Task<bool> DeleteBienLaiAsync(Guid id, string loggedEmployee);
    }
}
