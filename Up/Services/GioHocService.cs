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

    public class GioHocService: IGioHocService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GioHocService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {

            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_GioHoc)
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

        public async Task<GioHocViewModel> CreateGioHocAsync(string From, string To, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(From) || string.IsNullOrWhiteSpace(To))
                throw new Exception("Giờ Học không được để trống !!!");

            GioHoc gioHoc = new GioHoc();
            gioHoc.GioHocId = new Guid();
            gioHoc.From = From;
            gioHoc.To = To;
            gioHoc.CreatedBy = LoggedEmployee;
            gioHoc.CreatedDate = DateTime.Now;

            _context.GioHocs.Add(gioHoc);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Giờ Học !!!");

            return new GioHocViewModel {
                GioHocId = gioHoc.GioHocId,
                To = gioHoc.To,
                From = gioHoc.From,
                CreatedBy = gioHoc.CreatedBy,
                CreatedDate = gioHoc.CreatedDate.ToString("dd/MM/yyyy"),
            };
        }

        public async Task<bool> DeleteGioHocAsync(Guid GioHocId, string LoggedEmployee)
        {
            var lopHoc = await _context.LopHocs.Where(x => x.GioHocId == GioHocId).ToListAsync();
            if (lopHoc.Any())
                throw new Exception("Hãy xóa những lớp học thuộc giờ học này trước !!!");

            var item = await _context.GioHocs
                                    .Where(x => x.GioHocId == GioHocId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Giờ Học !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<GioHocViewModel>> GetGioHocAsync()
        {
            return await _context.GioHocs
                .Where(x => x.IsDisabled == false)
                .Select(x => new GioHocViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    GioHocId = x.GioHocId,
                    From = x.From,
                    To = x.To,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : "",
                })
                .ToListAsync();
        }

        public async Task<GioHocViewModel> UpdateGioHocAsync(Guid GioHocId, string From, string To, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(From) || string.IsNullOrWhiteSpace(To))
                throw new Exception("Giờ Học không được để trống !!!");

            var item = await _context.GioHocs
                                    .Where(x => x.GioHocId == GioHocId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Giờ Học !!!");

            item.From = From;
            item.To = To;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();

            return new GioHocViewModel
            {
                From = item.From,
                To = item.To,
                CreatedBy = item.CreatedBy,
                UpdatedBy = item.UpdatedBy,
                GioHocId = item.GioHocId,
                UpdatedDate = item.UpdatedDate != null ? ((DateTime)item.UpdatedDate).ToString("dd/MM/yyyy") : "",
                CreatedDate = item.CreatedDate != null ? ((DateTime)item.CreatedDate).ToString("dd/MM/yyyy") : "",
            };
        }
    }
}
