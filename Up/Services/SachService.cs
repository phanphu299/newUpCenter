
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

    public class SachService : ISachService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SachService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_TaiLieu)
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

        public async Task<SachViewModel> CreateSachAsync(string Name, double Gia, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Tài Liệu không được để trống !!!");

            Sach sach = new Sach();
            sach.SachId = new Guid();
            sach.Name = Name;
            sach.Gia = Gia;
            sach.CreatedBy = LoggedEmployee;
            sach.CreatedDate = DateTime.Now;

            _context.Sachs.Add(sach);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Tài Liệu !!!");

            return new SachViewModel {
                SachId = sach.SachId,
                Name = sach.Name,
                Gia = sach.Gia,
                CreatedBy = sach.CreatedBy,
                CreatedDate = sach.CreatedDate.ToString("dd/MM/yyyy"),
            };
        }

        public async Task<bool> DeleteSachAsync(Guid SachId, string LoggedEmployee)
        {
            var item = await _context.Sachs
                                    .Where(x => x.SachId == SachId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Khóa Học !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<SachViewModel>> GetSachAsync()
        {
            return await _context.Sachs
                //.Where(x => x.IsDisabled == false)
                .OrderBy(x => x.Name)
                .Select(x => new SachViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    SachId = x.SachId,
                    Name = x.Name,
                    Gia = x.Gia,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : "",
                    IsDisabled = x.IsDisabled,
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateSachAsync(Guid SachId, string Name, double Gia, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Tài Liệu không được để trống !!!");

            var item = await _context.Sachs
                                    .Where(x => x.SachId == SachId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Tài Liệu !!!");

            item.Name = Name;
            item.Gia = Gia;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
