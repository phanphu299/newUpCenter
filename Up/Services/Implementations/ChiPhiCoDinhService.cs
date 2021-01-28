
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

    public class ChiPhiCoDinhService : IChiPhiCoDinhService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ChiPhiCoDinhService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = await _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_ChiPhiCoDinh)
                .Select(x => x.RoleId)
                .AsNoTracking()
                .ToListAsync();

            var allRoles = _context.Roles.Where(x => quyen_roles.Contains(x.Id)).Select(x => x.Name).AsNoTracking();

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

        public async Task<ChiPhiCoDinhViewModel> CreateChiPhiCoDinhAsync(double Gia, string Name, string LoggedEmployee)
        {
            ChiPhiCoDinh chiPhi = new ChiPhiCoDinh();
            chiPhi.ChiPhiCoDinhId = new Guid();
            chiPhi.Gia = Gia;
            chiPhi.Name = Name;
            chiPhi.CreatedBy = LoggedEmployee;
            chiPhi.CreatedDate = DateTime.Now;

            _context.ChiPhiCoDinhs.Add(chiPhi);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Chi Phí !!!");
            return new ChiPhiCoDinhViewModel
            {
                ChiPhiCoDinhId = chiPhi.ChiPhiCoDinhId,
                Gia = chiPhi.Gia,
                Name = chiPhi.Name,
                CreatedBy = chiPhi.CreatedBy,
                CreatedDate = chiPhi.CreatedDate.ToString("dd/MM/yyyy")
            };
        }

        public async Task<bool> DeleteChiPhiCoDinhAsync(Guid ChiPhiCoDinhId, string LoggedEmployee)
        {
            var item = await _context.ChiPhiCoDinhs
                                    .Where(x => x.ChiPhiCoDinhId == ChiPhiCoDinhId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Chi Phí !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<ChiPhiCoDinhViewModel>> GetChiPhiCoDinhAsync()
        {
            return await _context.ChiPhiCoDinhs
                .Where(x => x.IsDisabled == false)
                .Select(x => new ChiPhiCoDinhViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    ChiPhiCoDinhId = x.ChiPhiCoDinhId,
                    Gia = x.Gia,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ChiPhiCoDinhViewModel> UpdateChiPhiCoDinhAsync(Guid ChiPhiCoDinhId, double Gia, string Name, string LoggedEmployee)
        {
            var item = await _context.ChiPhiCoDinhs
                                    .Where(x => x.ChiPhiCoDinhId == ChiPhiCoDinhId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Chi Phí !!!");

            item.Gia = Gia;
            item.Name = Name;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return new ChiPhiCoDinhViewModel
            {
                Name = item.Name,
                CreatedBy = item.CreatedBy,
                Gia = item.Gia,
                ChiPhiCoDinhId = item.ChiPhiCoDinhId,
                UpdatedBy = item.UpdatedBy,
                CreatedDate = item.CreatedDate != null ? ((DateTime)item.CreatedDate).ToString("dd/MM/yyyy") : "",
                UpdatedDate = item.UpdatedDate != null ? ((DateTime)item.UpdatedDate).ToString("dd/MM/yyyy") : "",
            };
        }
    }
}
