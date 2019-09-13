namespace Up.Services
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
            try
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
            catch (Exception exception)
            {
                throw new Exception("Lỗi khi gán quyền cho Role: " + exception.Message);
            }
        }

        public async Task<List<QuyenViewModel>> GetAllAsync()
        {
            return await _context.Quyens
                .Select(x => new QuyenViewModel
                {
                    QuyenId = x.QuyenId,
                    Name = x.Name,
                })
                .ToListAsync();
        }

        public async Task<List<QuyenViewModel>> GetAllByRoleIdAsync(string RoleId)
        {
            var quyenList = await _context.Quyens
                .Select(x => new QuyenViewModel
                {
                    QuyenId = x.QuyenId,
                    Name = x.Name,
                }).ToListAsync();

            var quyenByRole = _context.Quyen_Roles.Where(x => x.RoleId == RoleId).Select(x => x.QuyenId);

            foreach(QuyenViewModel item in quyenList)
            {
                if (quyenByRole.Contains(item.QuyenId))
                    item.IsTrue = true;
            }

            return quyenList;
        }
    }
}
