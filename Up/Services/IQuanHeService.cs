using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface IQuanHeService
    {
        Task<List<QuanHeViewModel>> GetQuanHeAsync();
        Task<QuanHeViewModel> CreateQuanHeAsync(string Name, string LoggedEmployee);
        Task<bool> UpdateQuanHeAsync(Guid QuanHeId, string Name, string LoggedEmployee);
        Task<bool> DeleteQuanHeAsync(Guid QuanHeId, string LoggedEmployee);
    }
}
