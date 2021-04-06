using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface ICauHoiRepository
    {
        Task<Guid> CreateCauHoiAsync(CreateCauHoiInputModel input, string loggedEmployee);

        Task<List<CauHoiViewModel>> GetCauHoiAsync();

        Task<CauHoiViewModel> GetCauHoiDetailAsync(Guid id);
    }
}
