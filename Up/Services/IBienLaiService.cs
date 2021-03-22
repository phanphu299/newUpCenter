using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface IBienLaiService
    {
        Task<string> GenerateMaBienLaiAsync();

        Task CreateBienLaiAsync(CreateBienLaiInputModel input, string loggedEmployee);
    }
}
