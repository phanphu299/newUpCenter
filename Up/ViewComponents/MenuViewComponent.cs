
namespace Up.ViewComponents
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Enums;
    using Up.Models;

    public class MenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MenuViewComponent(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(ClaimsPrincipal CurrentUser)
        {
            var CurUser = await _userManager.GetUserAsync(CurrentUser);

            var roles = await _userManager.GetRolesAsync(CurUser);

            if(roles.Any(x => x == Constants.Admin))
            {
                return View("Menu", new ListQuyenViewModel
                {
                    CanViewLopHoc = true,
                    CanViewTaiLieu = true,
                    CanViewKhoaHoc = true,
                    CanViewNgayHoc = true,
                    CanViewHocPhi = true,
                    CanViewGioHoc = true,
                    CanViewHocVien = true,
                    CanViewQuanHe = true,
                    CanViewVitri = true,
                    CanViewCheDo = true,
                    CanViewNgayLamViec = true,
                    CanViewNhanVien = true,
                    CanViewChiPhiCoDinh = true,
                    CanViewNo = true,
                    CanViewTinhHocPhi = true,
                    CanViewTinhLuong = true,
                    CanViewDiemDanh = true,
                    CanViewDiemDanh_Export = true
                });
            }
            else
            {
                var quyen_rolesLopHoc = await _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Read_LopHoc || x.QuyenId == (int)QuyenEnums.Contribute_LopHoc)
                .Select(x => x.RoleId).ToListAsync();

                var allRolesLopHoc = _context.Roles.Where(x => quyen_rolesLopHoc.Contains(x.Id)).Select(x => x.Name);

                bool canViewLopHoc = false;

                foreach (string role in roles)
                {
                    if (allRolesLopHoc.Contains(role))
                    {
                        canViewLopHoc = true;
                        break;
                    }
                }

                var quyen_rolesTaiLieu = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_TaiLieu || x.QuyenId == (int)QuyenEnums.Contribute_TaiLieu)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesTaiLieu = _context.Roles.Where(x => quyen_rolesTaiLieu.Contains(x.Id)).Select(x => x.Name);

                bool canViewTaiLieu = false;

                foreach (string role in roles)
                {
                    if (allRolesTaiLieu.Contains(role))
                    {
                        canViewTaiLieu = true;
                        break;
                    }
                }

                var quyen_rolesKhoaHoc = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_KhoaHoc || x.QuyenId == (int)QuyenEnums.Contribute_KhoaHoc)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesKhoaHoc = _context.Roles.Where(x => quyen_rolesKhoaHoc.Contains(x.Id)).Select(x => x.Name);

                bool canViewKhoaHoc = false;

                foreach (string role in roles)
                {
                    if (allRolesKhoaHoc.Contains(role))
                    {
                        canViewKhoaHoc = true;
                        break;
                    }
                }

                var quyen_rolesNgayHoc = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_NgayHoc || x.QuyenId == (int)QuyenEnums.Contribute_NgayHoc)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesNgayHoc = _context.Roles.Where(x => quyen_rolesNgayHoc.Contains(x.Id)).Select(x => x.Name);

                bool canViewNgayHoc = false;

                foreach (string role in roles)
                {
                    if (allRolesNgayHoc.Contains(role))
                    {
                        canViewNgayHoc = true;
                        break;
                    }
                }

                var quyen_rolesHocPhi = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_HocPhi || x.QuyenId == (int)QuyenEnums.Contribute_HocPhi)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesHocPhi = _context.Roles.Where(x => quyen_rolesHocPhi.Contains(x.Id)).Select(x => x.Name);

                bool canViewHocPhi = false;

                foreach (string role in roles)
                {
                    if (allRolesHocPhi.Contains(role))
                    {
                        canViewHocPhi = true;
                        break;
                    }
                }

                var quyen_rolesGioHoc = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_GioHoc || x.QuyenId == (int)QuyenEnums.Contribute_GioHoc)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesGioHoc = _context.Roles.Where(x => quyen_rolesGioHoc.Contains(x.Id)).Select(x => x.Name);

                bool canViewGioHoc = false;

                foreach (string role in roles)
                {
                    if (allRolesGioHoc.Contains(role))
                    {
                        canViewGioHoc = true;
                        break;
                    }
                }

                var quyen_rolesHocVien = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_HocVien || x.QuyenId == (int)QuyenEnums.Contribute_HocVien)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesHocVien = _context.Roles.Where(x => quyen_rolesHocVien.Contains(x.Id)).Select(x => x.Name);

                bool canViewHocVien = false;

                foreach (string role in roles)
                {
                    if (allRolesHocVien.Contains(role))
                    {
                        canViewHocVien = true;
                        break;
                    }
                }

                var quyen_rolesQuanHe = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_QuanHe || x.QuyenId == (int)QuyenEnums.Contribute_QuanHe)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesQuanHe = _context.Roles.Where(x => quyen_rolesQuanHe.Contains(x.Id)).Select(x => x.Name);

                bool canViewQuanHe = false;

                foreach (string role in roles)
                {
                    if (allRolesQuanHe.Contains(role))
                    {
                        canViewQuanHe = true;
                        break;
                    }
                }

                var quyen_rolesVitri = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_ViTriCongViec || x.QuyenId == (int)QuyenEnums.Contribute_ViTriCongViec)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesVitri = _context.Roles.Where(x => quyen_rolesVitri.Contains(x.Id)).Select(x => x.Name);

                bool canViewVitri = false;

                foreach (string role in roles)
                {
                    if (allRolesVitri.Contains(role))
                    {
                        canViewVitri = true;
                        break;
                    }
                }

                var quyen_rolesCheDo = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_CheDoHopTac || x.QuyenId == (int)QuyenEnums.Contribute_CheDoHopTac)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesCheDo = _context.Roles.Where(x => quyen_rolesCheDo.Contains(x.Id)).Select(x => x.Name);

                bool canViewCheDo = false;

                foreach (string role in roles)
                {
                    if (allRolesCheDo.Contains(role))
                    {
                        canViewCheDo = true;
                        break;
                    }
                }

                var quyen_rolesNgayLamViec = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_NgayLamViec || x.QuyenId == (int)QuyenEnums.Contribute_NgayLamViec)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesNgayLamViec = _context.Roles.Where(x => quyen_rolesNgayLamViec.Contains(x.Id)).Select(x => x.Name);

                bool canViewNgayLamViec = false;

                foreach (string role in roles)
                {
                    if (allRolesNgayLamViec.Contains(role))
                    {
                        canViewNgayLamViec = true;
                        break;
                    }
                }

                var quyen_rolesNhanVien = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_NhanVien || x.QuyenId == (int)QuyenEnums.Contribute_NhanVien)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesNhanVien = _context.Roles.Where(x => quyen_rolesNhanVien.Contains(x.Id)).Select(x => x.Name);

                bool canViewNhanVien = false;

                foreach (string role in roles)
                {
                    if (allRolesNhanVien.Contains(role))
                    {
                        canViewNhanVien = true;
                        break;
                    }
                }

                var quyen_rolesChiPhi = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_ChiPhiCoDinh || x.QuyenId == (int)QuyenEnums.Contribute_ChiPhiCoDinh)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesChiPhi = _context.Roles.Where(x => quyen_rolesChiPhi.Contains(x.Id)).Select(x => x.Name);

                bool canViewChiPhi = false;

                foreach (string role in roles)
                {
                    if (allRolesChiPhi.Contains(role))
                    {
                        canViewChiPhi = true;
                        break;
                    }
                }

                var quyen_rolesNo = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_No || x.QuyenId == (int)QuyenEnums.Contribute_No)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesNo = _context.Roles.Where(x => quyen_rolesNo.Contains(x.Id)).Select(x => x.Name);

                bool canViewNo = false;

                foreach (string role in roles)
                {
                    if (allRolesNo.Contains(role))
                    {
                        canViewNo = true;
                        break;
                    }
                }

                var quyen_rolesTinhHocPhi = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_TinhHocPhi || x.QuyenId == (int)QuyenEnums.Contribute_TinhHocPhi)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesTinhHocPhi = _context.Roles.Where(x => quyen_rolesTinhHocPhi.Contains(x.Id)).Select(x => x.Name);

                bool canViewTinhHocPhi = false;

                foreach (string role in roles)
                {
                    if (allRolesTinhHocPhi.Contains(role))
                    {
                        canViewTinhHocPhi = true;
                        break;
                    }
                }

                var quyen_rolesTinhLuong = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_TinhLuong || x.QuyenId == (int)QuyenEnums.Contribute_TinhLuong)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesTinhLuong = _context.Roles.Where(x => quyen_rolesTinhLuong.Contains(x.Id)).Select(x => x.Name);

                bool canViewTinhLuong = false;

                foreach (string role in roles)
                {
                    if (allRolesTinhLuong.Contains(role))
                    {
                        canViewTinhLuong = true;
                        break;
                    }
                }

                var quyen_rolesDiemDanh = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_DiemDanh || x.QuyenId == (int)QuyenEnums.Contribute_DiemDanh)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesDiemDanh = _context.Roles.Where(x => quyen_rolesDiemDanh.Contains(x.Id)).Select(x => x.Name);

                bool canViewDiemDanh = false;

                foreach (string role in roles)
                {
                    if (allRolesDiemDanh.Contains(role))
                    {
                        canViewDiemDanh = true;
                        break;
                    }
                }

                var quyen_rolesDiemDanh_Export = await _context.Quyen_Roles
                    .Where(x => x.QuyenId == (int)QuyenEnums.Read_DiemDanh_Export || x.QuyenId == (int)QuyenEnums.Contribute_DiemDanh_Export)
                    .Select(x => x.RoleId).ToListAsync();

                var allRolesDiemDanh_Export = _context.Roles.Where(x => quyen_rolesDiemDanh_Export.Contains(x.Id)).Select(x => x.Name);

                bool canViewDiemDanh_Export = false;

                foreach (string role in roles)
                {
                    if (allRolesDiemDanh_Export.Contains(role))
                    {
                        canViewDiemDanh_Export = true;
                        break;
                    }
                }

                return View("Menu", new ListQuyenViewModel
                {
                    CanViewLopHoc = canViewLopHoc,
                    CanViewTaiLieu = canViewTaiLieu,
                    CanViewKhoaHoc = canViewKhoaHoc,
                    CanViewNgayHoc = canViewNgayHoc,
                    CanViewHocPhi = canViewHocPhi,
                    CanViewGioHoc = canViewGioHoc,
                    CanViewHocVien = canViewHocVien,
                    CanViewQuanHe = canViewQuanHe,
                    CanViewVitri = canViewVitri,
                    CanViewCheDo = canViewCheDo,
                    CanViewNgayLamViec = canViewNgayLamViec,
                    CanViewNhanVien = canViewNhanVien,
                    CanViewChiPhiCoDinh = canViewChiPhi,
                    CanViewNo = canViewNo,
                    CanViewTinhHocPhi = canViewTinhHocPhi,
                    CanViewTinhLuong = canViewTinhLuong,
                    CanViewDiemDanh = canViewDiemDanh,
                    CanViewDiemDanh_Export = canViewDiemDanh_Export
                });
            }
        }
    }
}
