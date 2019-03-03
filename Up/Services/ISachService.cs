using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface ISachService
    {
        Task<List<SachViewModel>> GetSachAsync();
        Task<SachViewModel> CreateSachAsync(string Name, double Gia, string LoggedEmployee);
        Task<bool> UpdateSachAsync(Guid SachId, string Name, double Gia, string LoggedEmployee);
        Task<bool> DeleteSachAsync(Guid SachId, string LoggedEmployee);
    }
}
