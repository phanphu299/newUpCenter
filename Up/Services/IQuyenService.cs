﻿
namespace Up.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IQuyenService
    {
        Task<List<QuyenViewModel>> GetAllAsync();
        Task<List<QuyenViewModel>> GetAllByRoleIdAsync(string roleId);
        Task<bool> AddQuyenToRoleAsync(AddQuyenToRoleViewModel model);

        Task<List<RoleViewModel>> GetRoleByQuyenIdAsync(int quyenId);
    }
}
