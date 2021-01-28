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

    public class NgayHocService: INgayHocService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public NgayHocService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = await _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_NgayHoc)
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

        public async Task<NgayHocViewModel> CreateNgayHocAsync(string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Ngày Học không được để trống !!!");

            NgayHoc ngayHoc = new NgayHoc();
            ngayHoc.NgayHocId = new Guid();
            ngayHoc.Name = Name;
            ngayHoc.CreatedBy = LoggedEmployee;
            ngayHoc.CreatedDate = DateTime.Now;

            _context.NgayHocs.Add(ngayHoc);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Ngày Học !!!");

            return new NgayHocViewModel {
                NgayHocId = ngayHoc.NgayHocId,
                Name = ngayHoc.Name,
                CreatedBy = ngayHoc.CreatedBy,
                CreatedDate = ngayHoc.CreatedDate.ToString("dd/MM/yyyy"),
            };
        }

        public async Task<bool> CreateUpdateHocVien_NgayHocAsync(Guid HocVienId, Guid LopHocId, DateTime NgayBatDau, DateTime? NgayKetThuc, string LoggedEmployee)
        {
            var hocVien_NgayHoc = await _context.HocVien_NgayHocs.FirstOrDefaultAsync(x => x.LopHocId == LopHocId && x.HocVienId == HocVienId);
            if (hocVien_NgayHoc != null)
            {
                hocVien_NgayHoc.NgayBatDau = NgayBatDau;
                hocVien_NgayHoc.NgayKetThuc = NgayKetThuc;
                hocVien_NgayHoc.UpdatedBy = LoggedEmployee;
                hocVien_NgayHoc.UpdatedDate = DateTime.Now;
            }
            else
            {
                HocVien_NgayHoc HV_NgayHoc = new HocVien_NgayHoc();
                HV_NgayHoc.HocVien_NgayHocId = new Guid();
                HV_NgayHoc.HocVienId = HocVienId;
                HV_NgayHoc.LopHocId = LopHocId;
                HV_NgayHoc.NgayBatDau = NgayBatDau;
                HV_NgayHoc.NgayKetThuc = NgayKetThuc;
                HV_NgayHoc.CreatedBy = LoggedEmployee;
                HV_NgayHoc.CreatedDate = DateTime.Now;

                _context.HocVien_NgayHocs.Add(HV_NgayHoc);
            }

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> DeleteNgayHocAsync(Guid NgayHocId, string LoggedEmployee)
        {
            var lopHoc = await _context.LopHocs.Where(x => x.NgayHocId == NgayHocId).ToListAsync();
            if (lopHoc.Any())
                throw new Exception("Hãy xóa những lớp học thuộc ngày học này trước !!!");

            var item = await _context.NgayHocs
                                    .Where(x => x.NgayHocId == NgayHocId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Ngày Học !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<HocVien_NgayHocViewModel> GetHocVien_NgayHocByHocVienAsync(Guid HocVienId, Guid LopHocId)
        {
            if (HocVienId == null)
                throw new Exception("Không tìm thấy Học Viên!");

            if (LopHocId == null)
                throw new Exception("Không tìm thấy Lớp Học!");

            return await _context.HocVien_NgayHocs.Where(x => x.HocVienId == HocVienId && x.LopHocId == LopHocId)
                                .Select(x => new HocVien_NgayHocViewModel
                                {
                                    NgayBatDau = x.NgayBatDau.ToString("dd/MM/yyyy"),
                                    NgayKetThuc = x.NgayKetThuc == null? "":x.NgayKetThuc.Value.ToString("dd/MM/yyyy")
                                })
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
        }

        public async Task<List<NgayHocViewModel>> GetNgayHocAsync()
        {
            return await _context.NgayHocs
                .Where(x => x.IsDisabled == false)
                .Select(x => new NgayHocViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    NgayHocId = x.NgayHocId,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : "",
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> UpdateNgayHocAsync(Guid NgayHocId, string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception("Tên Ngày Học không được để trống !!!");

            var item = await _context.NgayHocs
                                    .Where(x => x.NgayHocId == NgayHocId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Ngày Học !!!");

            item.Name = Name;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
