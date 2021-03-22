using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IBienLaiRepository
    {
        Task<string> GetLastestMaBienLaiAsync();

        Task CreateBienLaiAsync(CreateBienLaiInputModel input, string loggedEmployee);

        Task<bool> IsExistMaBienLaiAsync(string maBienLai);
    }
}
