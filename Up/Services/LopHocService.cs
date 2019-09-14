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

    public class LopHocService : ILopHocService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public LopHocService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_LopHoc)
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

        public async Task<LopHocViewModel> CreateLopHocAsync(string Name, Guid KhoaHocId, Guid NgayHocId, Guid GioHocId, DateTime NgayKhaiGiang, string LoggedEmployee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name) || KhoaHocId == null || NgayHocId == null || GioHocId == null || NgayKhaiGiang == null)
                    throw new Exception("Tên Lớp Học, Khóa Học, Ngày Học, Giờ Học, Ngày Khai Giảng không được để trống !!!");

                LopHoc lopHoc = new LopHoc();
                lopHoc.Name = Name;
                lopHoc.KhoaHocId = KhoaHocId;
                lopHoc.NgayKhaiGiang = NgayKhaiGiang;
                lopHoc.NgayHocId = NgayHocId;
                lopHoc.GioHocId = GioHocId;
                lopHoc.CreatedBy = LoggedEmployee;
                lopHoc.CreatedDate = DateTime.Now;

                _context.LopHocs.Add(lopHoc);
                await _context.SaveChangesAsync();

                return new LopHocViewModel
                {
                    LopHocId = lopHoc.LopHocId,
                    GioHocId = lopHoc.GioHocId,
                    NgayHocId = lopHoc.NgayHocId,
                    NgayHoc = _context.NgayHocs.FindAsync(lopHoc.NgayHocId).Result.Name,
                    Name = lopHoc.Name,
                    NgayKhaiGiang = lopHoc.NgayKhaiGiang.ToString("dd/MM/yyyy"),
                    KhoaHocId = lopHoc.KhoaHocId,
                    CreatedBy = lopHoc.CreatedBy,
                    GioHocFrom = _context.GioHocs.FindAsync(lopHoc.GioHocId).Result.From,
                    GioHocTo = _context.GioHocs.FindAsync(lopHoc.GioHocId).Result.To,
                    CreatedDate = lopHoc.CreatedDate.ToString("dd/MM/yyyy"),
                    IsCanceled = lopHoc.IsCanceled,
                    IsGraduated = lopHoc.IsGraduated,
                    KhoaHoc = _context.KhoaHocs.FindAsync(lopHoc.KhoaHocId).Result.Name,
                    NgayKetThuc = lopHoc.NgayKetThuc != null ? ((DateTime)lopHoc.NgayKetThuc).ToString("dd/MM/yyyy") : ""
                };
            }
            catch (Exception exception)
            {
                throw new Exception("Lỗi khi tạo mới: " + exception.Message);
            }
        }

        public async Task<bool> DeleteLopHocAsync(Guid LopHocId, string LoggedEmployee)
        {
            try
            {
                var item = await _context.LopHocs
                                    .Where(x => x.LopHocId == LopHocId)
                                    .SingleOrDefaultAsync();

                if (item == null)
                    throw new Exception("Không tìm thấy Lớp Học !!!");

                var _lopHoc_HocVien = await _context.HocVien_LopHocs
                                                .Where(x => x.LopHocId == item.LopHocId)
                                                .ToListAsync();

                if (_lopHoc_HocVien.Any())
                    throw new Exception("Vẫn còn học viên theo học Lớp Học này !!!");

                item.IsDisabled = true;
                item.UpdatedBy = LoggedEmployee;
                item.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Lỗi khi xóa : " + exception.Message);
            }
        }

        public async Task<List<LopHocViewModel>> GetAvailableLopHocAsync()
        {
            return await _context.LopHocs
                .Where(x => x.IsDisabled == false && x.IsCanceled == false && x.IsGraduated == false)
                .Select(x => new LopHocViewModel
                {
                    Name = x.Name,
                    LopHocId = x.LopHocId
                })
                .ToListAsync();
        }

        public async Task<List<LopHocViewModel>> GetGraduatedAndCanceledLopHocAsync()
        {
            return await _context.LopHocs
                .Where(x => x.IsDisabled == false && (x.IsCanceled == true || x.IsGraduated == true))
                .Select(x => new LopHocViewModel
                {
                    Name = x.Name,
                    LopHocId = x.LopHocId
                })
                .ToListAsync();
        }

        public async Task<List<LopHocViewModel>> GetLopHocAsync()
        {
            return await _context.LopHocs
                .Where(x => x.IsDisabled == false)
                .Select(x => new LopHocViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    GioHocId = x.GioHocId,
                    Name = x.Name,
                    KhoaHocId = x.KhoaHocId,
                    KhoaHoc = x.KhoaHoc.Name,
                    IsGraduated = x.IsGraduated,
                    GioHocFrom = x.GioHoc.From,
                    GioHocTo = x.GioHoc.To,
                    IsCanceled = x.IsCanceled,
                    LopHocId = x.LopHocId,
                    NgayHocId = x.NgayHocId,
                    NgayHoc = x.NgayHoc.Name,
                    NgayKhaiGiang = x.NgayKhaiGiang.ToString("dd/MM/yyyy"),
                    NgayKetThuc = x.NgayKetThuc != null ? ((DateTime)x.NgayKetThuc).ToString("dd/MM/yyyy") : "",
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<List<LopHocViewModel>> GetLopHocByHocVienIdAsync(Guid HocVienId)
        {
            return await _context.LopHocs
                .Where(x => x.HocVien_LopHocs.Any(m => m.HocVienId == HocVienId))
                .Select(x => new LopHocViewModel
                {
                    Name = x.Name,
                    LopHocId = x.LopHocId
                })
                .ToListAsync();
        }

        public async Task<LopHocViewModel> GetLopHocByIdAsync(Guid LopHocId)
        {
            return await _context.LopHocs.Where(x => x.LopHocId == LopHocId)
                                        .Select(x => new LopHocViewModel
                                        {
                                            LopHocId = x.LopHocId,
                                            Name = x.Name,
                                        })
                                        .FirstOrDefaultAsync();
        }

        public async Task<bool> ToggleHuyLopAsync(Guid LopHocId, string LoggedEmployee)
        {
            try
            {
                var item = await _context.LopHocs
                                         .Where(x => x.LopHocId == LopHocId)
                                         .SingleOrDefaultAsync();
                if (item == null)
                    throw new Exception("Không tìm thấy Lớp Học !!!");

                item.IsCanceled = !item.IsCanceled;
                item.UpdatedBy = LoggedEmployee;
                item.UpdatedDate = DateTime.Now;

                var _hocVien_NgayHoc = await _context.HocVien_NgayHocs
                                                .Where(x => x.LopHocId == item.LopHocId)
                                                .ToListAsync();

                if (item.IsCanceled == true)
                {
                    foreach (var ngayHoc in _hocVien_NgayHoc)
                    {
                        ngayHoc.NgayKetThuc = DateTime.Now;
                    }
                }
                else
                {
                    foreach (var ngayHoc in _hocVien_NgayHoc)
                    {
                        ngayHoc.NgayKetThuc = null;
                    }
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception exception)
            {
                throw new Exception("Cập nhật lỗi: " + exception.Message);
            }
        }

        public async Task<bool> ToggleTotNghiepAsync(Guid LopHocId, string LoggedEmployee)
        {
            try
            {
                var item = await _context.LopHocs
                                         .Where(x => x.LopHocId == LopHocId)
                                         .SingleOrDefaultAsync();
                if (item == null)
                    throw new Exception("Không tìm thấy Lớp Học !!!");

                item.IsGraduated = !item.IsGraduated;
                item.UpdatedBy = LoggedEmployee;
                item.UpdatedDate = DateTime.Now;

                var _hocVien_NgayHoc = await _context.HocVien_NgayHocs
                                                .Where(x => x.LopHocId == item.LopHocId)
                                                .ToListAsync();

                if(item.IsGraduated == true)
                {
                    foreach (var ngayHoc in _hocVien_NgayHoc)
                    {
                        ngayHoc.NgayKetThuc = DateTime.Now;
                    }
                }
                else
                {
                    foreach (var ngayHoc in _hocVien_NgayHoc)
                    {
                        ngayHoc.NgayKetThuc = null;
                    }
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Cập nhật lỗi: " + exception.Message);
            }
        }

        public async Task<LopHocViewModel> UpdateLopHocAsync(Guid LopHocId, string Name, Guid KhoaHocId, Guid NgayHocId,
            Guid GioHocId, DateTime NgayKhaiGiang, DateTime? NgayKetThuc, string LoggedEmployee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name) || KhoaHocId == null || NgayHocId == null || GioHocId == null || NgayKhaiGiang == null)
                    throw new Exception("Tên Lớp Học, Khóa Học, Ngày Học, Giờ Học, Ngày Khai Giảng không được để trống !!!");

                var item = await _context.LopHocs
                                        .Where(x => x.LopHocId == LopHocId)
                                        .SingleOrDefaultAsync();

                if (item == null)
                    throw new Exception("Không tìm thấy Lớp Học !!!");

                item.Name = Name;
                item.KhoaHocId = KhoaHocId;
                item.NgayHocId = NgayHocId;
                item.GioHocId = GioHocId;
                item.NgayKhaiGiang = NgayKhaiGiang;
                item.UpdatedBy = LoggedEmployee;
                item.UpdatedDate = DateTime.Now;

                if (NgayKetThuc != null)
                    item.NgayKetThuc = NgayKetThuc;

                await _context.SaveChangesAsync();
                return new LopHocViewModel
                {
                    LopHocId = item.LopHocId,
                    GioHocId = item.GioHocId,
                    NgayHocId = item.NgayHocId,
                    NgayHoc = _context.NgayHocs.FindAsync(item.NgayHocId).Result.Name,
                    Name = item.Name,
                    NgayKhaiGiang = item.NgayKhaiGiang.ToString("dd/MM/yyyy"),
                    KhoaHocId = item.KhoaHocId,
                    CreatedBy = item.CreatedBy,
                    GioHocFrom = _context.GioHocs.FindAsync(item.GioHocId).Result.From,
                    GioHocTo = _context.GioHocs.FindAsync(item.GioHocId).Result.To,
                    CreatedDate = item.CreatedDate.ToString("dd/MM/yyyy"),
                    IsCanceled = item.IsCanceled,
                    IsGraduated = item.IsGraduated,
                    KhoaHoc = _context.KhoaHocs.FindAsync(item.KhoaHocId).Result.Name,
                    NgayKetThuc = item.NgayKetThuc != null ? ((DateTime)item.NgayKetThuc).ToString("dd/MM/yyyy") : ""
                };
            }
            catch (Exception exception)
            {
                throw new Exception("Cập nhật lỗi: " + exception.Message);
            }
        }
    }
}
