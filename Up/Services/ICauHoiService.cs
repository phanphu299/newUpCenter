using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Services
{
    public interface ICauHoiService
    {
        Task<bool> CanContributeAsync(ClaimsPrincipal user);

        Task<CauHoiViewModel> CreateCauHoiAsync(CreateCauHoiInputModel input, string loggedEmployee);
        
        Task<List<CauHoiViewModel>> ImportCauHoiAsync(ImportCauHoiInputModel input, string loggedEmployee);

        Task<List<CauHoiViewModel>> GetCauHoiAsync();

        Task<List<CauHoiViewModel>> GetCauHoiAsync(IList<Guid> ids);

        Task<bool> DeleteCauHoiAsync(Guid id, string loggedEmployee);
    }
}
