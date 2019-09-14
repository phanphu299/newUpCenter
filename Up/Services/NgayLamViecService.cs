
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

    public class NgayLamViecService : INgayLamViecService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public NgayLamViecService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_NgayLamViec)
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

        public async Task<NgayLamViecViewModel> CreateNgayLamViecAsync(string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Ngày Làm Việc không được để trống !!!");

            NgayLamViec ngayLamViec = new NgayLamViec();
            ngayLamViec.NgayLamViecId = new Guid();
            ngayLamViec.Name = Name;
            ngayLamViec.CreatedBy = LoggedEmployee;
            ngayLamViec.CreatedDate = DateTime.Now;

            _context.NgayLamViecs.Add(ngayLamViec);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Ngày Làm Việc !!!");
            return new NgayLamViecViewModel { NgayLamViecId = ngayLamViec.NgayLamViecId, Name = ngayLamViec.Name, CreatedBy = ngayLamViec.CreatedBy, CreatedDate = ngayLamViec.CreatedDate.ToString("dd/MM/yyyy") };
        }

        public async Task<bool> DeleteNgayLamViecAsync(Guid NgayLamViecId, string LoggedEmployee)
        {
            var giaoVien = await _context.GiaoViens.Where(x => x.NgayLamViecId == NgayLamViecId).ToListAsync();
            if (giaoVien.Any())
                throw new Exception("Hãy xóa những nhân viên có ngày làm việc này trước !!!");

            var item = await _context.NgayLamViecs
                                    .Where(x => x.NgayLamViecId == NgayLamViecId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Ngày làm việc !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<NgayLamViecViewModel>> GetNgayLamViecAsync()
        {
            return await _context.NgayLamViecs
                .Where(x => x.IsDisabled == false)
                .Select(x => new NgayLamViecViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    NgayLamViecId = x.NgayLamViecId,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateNgayLamViecAsync(Guid NgayLamViecId, string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên ngày làm việc không được để trống !!!");

            var item = await _context.NgayLamViecs
                                    .Where(x => x.NgayLamViecId == NgayLamViecId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Ngày làm việc !!!");

            item.Name = Name;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
