
namespace Up.ViewComponents
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
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

            if (roles.Any(x => x == Constants.Admin))
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
                    CanViewDiemDanh_Export = true,
                    CanViewChiPhiKhac = true,
                    CanViewHocPhiTronGoi = true
                });
            }
            else
            {
                return View("Menu", new ListQuyenViewModel
                {
                    CanViewLopHoc = await GetQuyen(roles, QuyenEnums.Read_LopHoc, QuyenEnums.Contribute_LopHoc),
                    CanViewTaiLieu = await GetQuyen(roles, QuyenEnums.Read_TaiLieu, QuyenEnums.Contribute_TaiLieu),
                    CanViewKhoaHoc = await GetQuyen(roles, QuyenEnums.Read_KhoaHoc, QuyenEnums.Contribute_KhoaHoc),
                    CanViewNgayHoc = await GetQuyen(roles, QuyenEnums.Read_NgayHoc, QuyenEnums.Contribute_NgayHoc),
                    CanViewHocPhi = await GetQuyen(roles, QuyenEnums.Read_HocPhi, QuyenEnums.Contribute_HocPhi),
                    CanViewGioHoc = await GetQuyen(roles, QuyenEnums.Read_GioHoc, QuyenEnums.Contribute_GioHoc),
                    CanViewHocVien = await GetQuyen(roles, QuyenEnums.Read_HocVien, QuyenEnums.Contribute_HocVien),
                    CanViewQuanHe = await GetQuyen(roles, QuyenEnums.Read_QuanHe, QuyenEnums.Contribute_QuanHe),
                    CanViewVitri = await GetQuyen(roles, QuyenEnums.Read_ViTriCongViec, QuyenEnums.Contribute_ViTriCongViec),
                    CanViewCheDo = await GetQuyen(roles, QuyenEnums.Read_CheDoHopTac, QuyenEnums.Contribute_CheDoHopTac),
                    CanViewNgayLamViec = await GetQuyen(roles, QuyenEnums.Read_NgayLamViec, QuyenEnums.Contribute_NgayLamViec),
                    CanViewNhanVien = await GetQuyen(roles, QuyenEnums.Read_NhanVien, QuyenEnums.Contribute_NhanVien),
                    CanViewChiPhiCoDinh = await GetQuyen(roles, QuyenEnums.Read_ChiPhiCoDinh, QuyenEnums.Contribute_ChiPhiCoDinh),
                    CanViewNo = await GetQuyen(roles, QuyenEnums.Read_No, QuyenEnums.Contribute_No),
                    CanViewTinhHocPhi = await GetQuyen(roles, QuyenEnums.Read_TinhHocPhi, QuyenEnums.Contribute_TinhHocPhi),
                    CanViewTinhLuong = await GetQuyen(roles, QuyenEnums.Read_TinhLuong, QuyenEnums.Contribute_TinhLuong),
                    CanViewDiemDanh = await GetQuyen(roles, QuyenEnums.Read_DiemDanh, QuyenEnums.Contribute_DiemDanh),
                    CanViewDiemDanh_Export = await GetQuyen(roles, QuyenEnums.Read_DiemDanh_Export, QuyenEnums.Contribute_DiemDanh_Export),
                    CanViewChiPhiKhac = await GetQuyen(roles, QuyenEnums.Read_ChiPhiKhac, QuyenEnums.Contribute_ChiPhiKhac),
                    CanViewHocPhiTronGoi = await GetQuyen(roles, QuyenEnums.Read_HocPhiTronGoi, QuyenEnums.Contribute_HocPhiTronGoi)
                });
            }
        }

        private async Task<bool> GetQuyen(IList<string> roles, QuyenEnums quyenRead, QuyenEnums quyenContribute)
        {
            var quyen_roles = await _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)quyenRead || x.QuyenId == (int)quyenContribute)
                .Select(x => x.RoleId).ToListAsync();

            var allRoles = _context.Roles.Where(x => quyen_roles.Contains(x.Id)).Select(x => x.Name);

            bool result = false;

            foreach (string role in roles)
            {
                if (allRoles.Contains(role))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
