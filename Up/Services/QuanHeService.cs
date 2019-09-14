namespace Up.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Data.Entities;
    using Up.Enums;
    using Up.Models;
    public class QuanHeService : IQuanHeService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public QuanHeService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_QuanHe)
                .Select(x => x.RoleId).ToList();

            var allRoles = _context.Roles.Where(x => quyen_roles.Contains(x.Id)).Select(x => x.Name);

            bool canContribute = false;

            foreach (string role in roles)
            {
                if (allRoles.Contains(role))
                {
                    canContribute = true;
                    break;
                }
            }

            return canContribute;
        }

        public async Task<QuanHeViewModel> CreateQuanHeAsync(string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Quan Hệ không được để trống !!!");

            QuanHe quanHe = new QuanHe();
            quanHe.QuanHeId = new Guid();
            quanHe.Name = Name;
            quanHe.CreatedBy = LoggedEmployee;
            quanHe.CreatedDate = DateTime.Now;

            _context.QuanHes.Add(quanHe);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Quan Hệ !!!");
            return new QuanHeViewModel { QuanHeId = quanHe.QuanHeId, Name = quanHe.Name, CreatedBy = quanHe.CreatedBy, CreatedDate = quanHe.CreatedDate.ToString("dd/MM/yyyy") };
        }

        public async Task<bool> DeleteQuanHeAsync(Guid QuanHeId, string LoggedEmployee)
        {
            var hocVien = await _context.HocViens.Where(x => x.QuanHeId == QuanHeId).ToListAsync();
            if (hocVien.Any())
                throw new Exception("Hãy xóa những học viên có quan hệ này trước !!!");

            var item = await _context.QuanHes
                                    .Where(x => x.QuanHeId == QuanHeId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Quan Hệ !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<QuanHeViewModel>> GetQuanHeAsync()
        {
            return await _context.QuanHes
                .Where(x => x.IsDisabled == false)
                .Select(x => new QuanHeViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    QuanHeId = x.QuanHeId,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateQuanHeAsync(Guid QuanHeId, string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Quan Hệ không được để trống !!!");

            var item = await _context.QuanHes
                                    .Where(x => x.QuanHeId == QuanHeId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Quan Hệ !!!");

            item.Name = Name;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
