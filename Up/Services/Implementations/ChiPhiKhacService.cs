
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

    public class ChiPhiKhacService : IChiPhiKhacService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ChiPhiKhacService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
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

        public async Task<ChiPhiKhacViewModel> CreateChiPhiKhacAsync(string Name, double Gia, DateTime NgayChiPhi, string LoggedEmployee)
        {
            try
            {
                ChiPhiKhac chiPhi = new ChiPhiKhac();
                chiPhi.ChiPhiKhacId = new Guid();
                chiPhi.Name = Name;
                chiPhi.CreatedBy = LoggedEmployee;
                chiPhi.CreatedDate = DateTime.Now;
                chiPhi.Gia = Gia;
                chiPhi.NgayChiPhi = NgayChiPhi;

                _context.ChiPhiKhacs.Add(chiPhi);

                ThongKe_ChiPhi thongKe = new ThongKe_ChiPhi
                {
                    ThongKe_ChiPhiId = new Guid(),
                    NgayChiPhi = NgayChiPhi,
                    CreatedBy = LoggedEmployee,
                    CreatedDate = DateTime.Now,
                    ChiPhi = Gia,
                    ChiPhiKhacId = chiPhi.ChiPhiKhacId,
                    DaLuu = true
                };
                await _context.ThongKe_ChiPhis.AddAsync(thongKe);

                var saveResult = await _context.SaveChangesAsync();
                if (saveResult != 2)
                    throw new Exception("Lỗi khi lưu Chi Phí !!!");
                return new ChiPhiKhacViewModel
                {
                    ChiPhiKhacId = chiPhi.ChiPhiKhacId,
                    Name = chiPhi.Name,
                    Gia = chiPhi.Gia,
                    _NgayChiPhi = chiPhi.NgayChiPhi.ToString("dd/MM/yyyy"),
                    CreatedBy = chiPhi.CreatedBy,
                    CreatedDate = chiPhi.CreatedDate.ToString("dd/MM/yyyy"),
                    NgayChiPhi = chiPhi.NgayChiPhi,
                };
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteChiPhiKhacAsync(Guid ChiPhiKhacId, string LoggedEmployee)
        {
            var item = await _context.ChiPhiKhacs
                                    .Where(x => x.ChiPhiKhacId == ChiPhiKhacId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Chi Phí !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var thongKe_ChiPhi = await _context.ThongKe_ChiPhis.FirstOrDefaultAsync(x => x.ChiPhiKhacId == item.ChiPhiKhacId);

            if(thongKe_ChiPhi != null)
                _context.ThongKe_ChiPhis.Remove(thongKe_ChiPhi);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 2;
        }

        public async Task<List<ChiPhiKhacViewModel>> GetChiPhiKhacAsync()
        {
            return await _context.ChiPhiKhacs
                .Where(x => x.IsDisabled == false)
                .Select(x => new ChiPhiKhacViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    ChiPhiKhacId = x.ChiPhiKhacId,
                    Name = x.Name,
                    Gia = x.Gia,
                    _NgayChiPhi = x.NgayChiPhi.ToString("dd/MM/yyyy"),
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : "",
                    NgayChiPhi = x.NgayChiPhi
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ChiPhiKhacViewModel> UpdateChiPhiKhacAsync(Guid ChiPhiKhacId, string Name, double Gia, DateTime NgayChiPhi, string LoggedEmployee)
        {
            var item = await _context.ChiPhiKhacs
                                    .Where(x => x.ChiPhiKhacId == ChiPhiKhacId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Chi Phí !!!");

            item.Name = Name;
            item.Gia = Gia;
            item.NgayChiPhi = NgayChiPhi;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return new ChiPhiKhacViewModel
            {
                Name = item.Name,
                CreatedBy = item.CreatedBy,
                ChiPhiKhacId = item.ChiPhiKhacId,
                UpdatedBy = item.UpdatedBy,
                Gia = item.Gia,
                _NgayChiPhi = item.NgayChiPhi.ToString("dd/MM/yyyy"),
                CreatedDate = item.CreatedDate != null ? ((DateTime)item.CreatedDate).ToString("dd/MM/yyyy") : "",
                UpdatedDate = item.UpdatedDate != null ? ((DateTime)item.UpdatedDate).ToString("dd/MM/yyyy") : "",
                NgayChiPhi = item.NgayChiPhi
            };
        }
    }
}
