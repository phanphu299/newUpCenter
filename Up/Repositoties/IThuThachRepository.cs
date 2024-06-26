﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;

namespace Up.Repositoties
{
    public interface IThuThachRepository
    {
        Task<List<ThuThachViewModel>> GetThuThachAsync();

        Task<List<ThuThachViewModel>> GetThuThachByKhoaHocIdsAsync(IList<Guid> khoaHocIds);

        Task<Guid> CreateThuThachAsync(CreateThuThachInputModel input, string loggedEmployee);

        Task<Guid> UpdateThuThachAsync(UpdateThuThachInputModel input, string loggedEmployee);

        Task<ThuThachViewModel> GetThuThachDetailAsync(Guid id);

        Task<bool> DeleteThuThachAsync(Guid id, string loggedEmployee);

        Task LuuKetQuaAsync(ResultInputModel input);

        Task<int> GetLatestLanThi(Guid hocVienId, Guid thuThachId);

        Task<bool> CanContributeAsync(ClaimsPrincipal user, int right);
    }
}
