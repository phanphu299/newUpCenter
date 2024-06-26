﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface ICauHoiRepository
    {
        Task<Guid> CreateCauHoiAsync(CreateCauHoiInputModel input, string loggedEmployee);

        Task<List<CauHoiViewModel>> GetCauHoiAsync();

        Task<List<CauHoiViewModel>> GetCauHoiAsync(Guid thuThachId, int stt);

        Task<List<CauHoiViewModel>> GetCauHoiAsync(Guid thuThachId);

        Task<List<CauHoiViewModel>> GetCauHoiAsync(IList<Guid> ids);

        Task<CauHoiViewModel> GetCauHoiDetailAsync(Guid id);

        Task<bool> DeleteCauHoiAsync(Guid id, string loggedEmployee);

        Task<List<Guid>> ImportCauHoiAsync(ImportCauHoiInputModel input, string loggedEmployee);

        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);
    }
}
