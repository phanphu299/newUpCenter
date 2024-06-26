﻿namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Data.Entities;
    using Up.Models;

    public class QuyenService : IQuyenService
    {
        private readonly ApplicationDbContext _context;

        public QuyenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddQuyenToRoleAsync(AddQuyenToRoleViewModel model)
        {
            var oldQuyen = await _context.Quyen_Roles.Where(x => x.RoleId == model.RoleId).ToListAsync();
            _context.Quyen_Roles.RemoveRange(oldQuyen);

            foreach (var item in model.QuyenList)
            {
                if (item.IsTrue)
                {
                    Quyen_Role quyen_Role = new Quyen_Role
                    {
                        QuyenId = item.QuyenId,
                        RoleId = model.RoleId
                    };

                    await _context.Quyen_Roles.AddAsync(quyen_Role);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<QuyenViewModel>> GetAllAsync()
        {
            return await _context.Quyens
                .Select(x => new QuyenViewModel
                {
                    QuyenId = x.QuyenId,
                    Name = x.Name,
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<QuyenViewModel>> GetAllByRoleIdAsync(string roleId)
        {
            var quyenList = await _context.Quyens
                .Select(x => new QuyenViewModel
                {
                    QuyenId = x.QuyenId,
                    Name = x.Name,
                })
                .AsNoTracking()
                .ToListAsync();

            var quyenByRole = _context.Quyen_Roles.Where(x => x.RoleId == roleId).AsNoTracking().Select(x => x.QuyenId);

            foreach (QuyenViewModel item in quyenList)
            {
                if (quyenByRole.Contains(item.QuyenId))
                    item.IsTrue = true;
            }

            return quyenList;
        }

        public async Task<List<RoleViewModel>> GetRoleByQuyenIdAsync(int quyenId)
        {
            var quyenByRole = _context.Quyen_Roles.Where(x => x.QuyenId == quyenId).AsNoTracking().Select(x => x.RoleId);

            return await _context.Roles
                .Where(x => quyenByRole.Contains(x.Id)).Select(x => new RoleViewModel
                {
                    Id = x.Id,
                    Role = x.Name,
                })
               .AsNoTracking()
               .ToListAsync();
        }
    }
}
