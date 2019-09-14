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

    public class LoaiGiaoVienService : ILoaiGiaoVienService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public LoaiGiaoVienService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_ViTriCongViec)
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

        public async Task<LoaiGiaoVienViewModel> CreateLoaiGiaoVienAsync(string Name, byte Order, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Loại Nhân Viên không được để trống !!!");

            LoaiGiaoVien loaiGiaoVien = new LoaiGiaoVien();
            loaiGiaoVien.LoaiGiaoVienId = new Guid();
            loaiGiaoVien.Name = Name;
            loaiGiaoVien.CreatedBy = LoggedEmployee;
            loaiGiaoVien.CreatedDate = DateTime.Now;
            loaiGiaoVien.Order = Order;

            _context.LoaiGiaoViens.Add(loaiGiaoVien);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Loại Nhân Viên !!!");
            return new LoaiGiaoVienViewModel { LoaiGiaoVienId = loaiGiaoVien.LoaiGiaoVienId, Order = loaiGiaoVien.Order, Name = loaiGiaoVien.Name, CreatedBy = loaiGiaoVien.CreatedBy, CreatedDate = loaiGiaoVien.CreatedDate.ToString("dd/MM/yyyy") };
        }

        public async Task<bool> DeleteLoaiGiaoVienAsync(Guid LoaiGiaoVienId, string LoggedEmployee)
        {
            var giaoVien = await _context.GiaoViens.Where(x => x.NhanVien_ViTris.Any(m => m.ViTriId == LoaiGiaoVienId)).ToListAsync();
            if (giaoVien.Any())
                throw new Exception("Hãy xóa những nhân viên thuộc loại này trước !!!");

            var item = await _context.LoaiGiaoViens
                                    .Where(x => x.LoaiGiaoVienId == LoaiGiaoVienId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Loại nhân viên !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<LoaiGiaoVienViewModel>> GetLoaiGiaoVienAsync()
        {
            return await _context.LoaiGiaoViens
                .Where(x => x.IsDisabled == false)
                .Select(x => new LoaiGiaoVienViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    LoaiGiaoVienId = x.LoaiGiaoVienId,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : "",
                    Order = x.Order
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateLoaiGiaoVienAsync(Guid LoaiGiaoVienId, string Name, byte Order, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Loại Nhân Viên không được để trống !!!");

            var item = await _context.LoaiGiaoViens
                                    .Where(x => x.LoaiGiaoVienId == LoaiGiaoVienId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Loại Nhân Viên !!!");

            item.Name = Name;
            item.Order = Order;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
