
namespace Up.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Data.Entities;
    using Up.Enums;
    using Up.Models;

    public class HocPhiTronGoiService : IHocPhiTronGoiService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HocPhiTronGoiService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = await _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_TinhHocPhi)
                .Select(x => x.RoleId).AsNoTracking().ToListAsync();

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

        public async Task<bool> CreateHocPhiTronGoiAsync(Guid HocVienId, double HocPhi, DateTime FromDate, DateTime ToDate, string LoggedEmployee)
        {
            try
            {
                HocPhiTronGoi hocPhi = new HocPhiTronGoi();
                hocPhi.HocPhiTronGoiId = new Guid();
                hocPhi.HocVienId = HocVienId;
                hocPhi.CreatedBy = LoggedEmployee;
                hocPhi.CreatedDate = DateTime.Now;
                hocPhi.HocPhi = HocPhi;
                hocPhi.FromDate = FromDate;
                hocPhi.ToDate = ToDate;

                _context.HocPhiTronGois.Add(hocPhi);

                var saveResult = await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ToggleHocPhiTronGoiAsync(Guid HocPhiTronGoiId, string LoggedEmployee)
        {
            var item = await _context.HocPhiTronGois
                                    .Where(x => x.HocPhiTronGoiId == HocPhiTronGoiId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Học Phí !!!");

            item.IsDisabled = !item.IsDisabled;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<HocPhiTronGoiViewModel>> GetHocPhiTronGoiAsync()
        {
            return await _context.HocPhiTronGois
                .Where(x => x.IsRemoved == false)
                .Include(x => x.HocVien)
                .Select(x => new HocPhiTronGoiViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    HocPhiTronGoiId = x.HocPhiTronGoiId,
                    Name = x.HocVien.FullName,
                    HocPhi = x.HocPhi,
                    IsDisabled = x.IsDisabled,
                    FromDate = x.FromDate.ToString("dd/MM/yyyy"),
                    ToDate = x.ToDate.ToString("dd/MM/yyyy"),
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : "",
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<HocPhiTronGoiViewModel> UpdateHocPhiTronGoiAsync(Guid HocPhiTronGoiId, double HocPhi, DateTime FromDate, DateTime ToDate, string LoggedEmployee)
        {
            var item = await _context.HocPhiTronGois
                                    .Include(x => x.HocVien)
                                    .Where(x => x.HocPhiTronGoiId == HocPhiTronGoiId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Học Phí !!!");

            item.HocPhi = HocPhi;
            item.FromDate = FromDate;
            item.ToDate = ToDate;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return new HocPhiTronGoiViewModel
            {
                Name = item.HocVien.FullName,
                CreatedBy = item.CreatedBy,
                HocPhiTronGoiId = item.HocPhiTronGoiId,
                UpdatedBy = item.UpdatedBy,
                HocPhi = item.HocPhi,
                FromDate = item.FromDate.ToString("dd/MM/yyyy"),
                ToDate = item.ToDate.ToString("dd/MM/yyyy"),
                CreatedDate = item.CreatedDate != null ? ((DateTime)item.CreatedDate).ToString("dd/MM/yyyy") : "",
                UpdatedDate = item.UpdatedDate != null ? ((DateTime)item.UpdatedDate).ToString("dd/MM/yyyy") : "",
                IsDisabled = item.IsDisabled
            };
        }

        public async Task<bool> CheckIsDisable()
        {
            try
            {
                var item = await _context.HocPhiTronGois
                                    .Where(x => x.IsDisabled == false && (DateTime.Now.Year > x.ToDate.Year || (DateTime.Now.Year == x.ToDate.Year && DateTime.Now.Month > x.ToDate.Month)))
                                    .ToListAsync();
                foreach (var hocPhi in item)
                {
                    hocPhi.IsDisabled = true;
                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteHocPhiTronGoiAsync(Guid HocPhiTronGoiId, string LoggedEmployee)
        {
            var item = await _context.HocPhiTronGois
                                    .Where(x => x.HocPhiTronGoiId == HocPhiTronGoiId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Học Phí !!!");

            item.IsRemoved = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
