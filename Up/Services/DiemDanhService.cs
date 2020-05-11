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

    public class DiemDanhService : IDiemDanhService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DiemDanhService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_DiemDanh)
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

        public async Task<bool> CanContributeExportAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_DiemDanh_Export)
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

        public async Task<bool> DiemDanhTatCaAsync(Guid LopHocId, bool isOff, DateTime NgayDiemDanh, string LoggedEmployee)
        {
            try
            {
                if (LopHocId == null || isOff == null || NgayDiemDanh == null)
                    throw new Exception("Lỗi khi Điểm Danh!!!");

                var isExisting = await _context.LopHoc_DiemDanhs
                                        .Where(x => x.LopHocId == LopHocId && x.NgayDiemDanh == NgayDiemDanh)
                                        .ToListAsync();
                if (isExisting.Any())
                    throw new Exception("Lớp học đã được điểm danh ngày " + NgayDiemDanh.ToShortDateString());

                var hocViens = GetHocVienByLopHoc(LopHocId);

                foreach (var item in hocViens.Result)
                {
                    LopHoc_DiemDanh lopHoc_DiemDanh = new LopHoc_DiemDanh();
                    lopHoc_DiemDanh.NgayDiemDanh = NgayDiemDanh;
                    lopHoc_DiemDanh.IsOff = isOff;
                    lopHoc_DiemDanh.LopHocId = LopHocId;
                    lopHoc_DiemDanh.HocVienId = item.HocVienId;
                    lopHoc_DiemDanh.CreatedBy = LoggedEmployee;
                    lopHoc_DiemDanh.CreatedDate = DateTime.Now;
                    _context.LopHoc_DiemDanhs.Add(lopHoc_DiemDanh);
                }

                var saveResult = await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public async Task<bool> DiemDanhTungHocVienAsync(Guid LopHocId, Guid HocVienId, bool isOff, DateTime NgayDiemDanh, string LoggedEmployee)
        {
            try
            {
                if (LopHocId == null || HocVienId == null || isOff == null || NgayDiemDanh == null)
                    throw new Exception("Lỗi khi Điểm Danh!!!");

                var diemDanh = await _context.LopHoc_DiemDanhs
                                        .Where(x => x.LopHocId == LopHocId && x.HocVienId == HocVienId && x.NgayDiemDanh == NgayDiemDanh)
                                        .SingleOrDefaultAsync();

                if (diemDanh != null)
                {
                    diemDanh.IsOff = isOff;
                }
                else
                {
                    LopHoc_DiemDanh lopHoc_DiemDanh = new LopHoc_DiemDanh();
                    lopHoc_DiemDanh.NgayDiemDanh = NgayDiemDanh;
                    lopHoc_DiemDanh.IsOff = isOff;
                    lopHoc_DiemDanh.LopHocId = LopHocId;
                    lopHoc_DiemDanh.HocVienId = HocVienId;
                    lopHoc_DiemDanh.CreatedBy = LoggedEmployee;
                    lopHoc_DiemDanh.CreatedDate = DateTime.Now;

                    _context.LopHoc_DiemDanhs.Add(lopHoc_DiemDanh);
                }

                var saveResult = await _context.SaveChangesAsync();
                if (saveResult != 1)
                    throw new Exception("Lỗi khi Điểm Danh!!!");
                return true;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public async Task<bool> DuocNghi(Guid LopHocId, DateTime NgayDiemDanh, string LoggedEmployee)
        {
            if (LopHocId == null || NgayDiemDanh == null)
                throw new Exception("Lỗi khi Cho Lớp Học Nghỉ!!!");

            var isExisting = await _context.LopHoc_DiemDanhs
                                    .Where(x => x.LopHocId == LopHocId && x.NgayDiemDanh == NgayDiemDanh)
                                    .ToListAsync();
            if (isExisting.Any())
                _context.LopHoc_DiemDanhs.RemoveRange(isExisting);
            //throw new Exception("Lớp học đã được điểm danh ngày " + NgayDiemDanh.ToShortDateString());

            var hocViens = await _context.HocVien_LopHocs
                                .Include(x => x.LopHoc)
                                .Include(x => x.HocVien.HocVien_NgayHocs)
                                .Where(x => x.LopHocId == LopHocId && x.HocVien.IsDisabled == false)
                                .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc.Value >= NgayDiemDanh)))
                                .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayBatDau <= NgayDiemDanh)))
                                .Select(x => new HocVienViewModel
                                {
                                    HocVienId = x.HocVienId,
                                })
                                .ToListAsync();

            foreach (var item in hocViens)
            {
                LopHoc_DiemDanh lopHoc_DiemDanh = new LopHoc_DiemDanh();
                lopHoc_DiemDanh.NgayDiemDanh = NgayDiemDanh;
                lopHoc_DiemDanh.IsOff = true;
                lopHoc_DiemDanh.IsDuocNghi = true;
                lopHoc_DiemDanh.LopHocId = LopHocId;
                lopHoc_DiemDanh.HocVienId = item.HocVienId;
                lopHoc_DiemDanh.CreatedBy = LoggedEmployee;
                lopHoc_DiemDanh.CreatedDate = DateTime.Now;
                _context.LopHoc_DiemDanhs.Add(lopHoc_DiemDanh);
            }

            var saveResult = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DiemDanhViewModel>> GetDiemDanhByHocVienAndLopHoc(Guid HocVienId, Guid LopHocId)
        {
            if (HocVienId == null)
                throw new Exception("Không tìm thấy Học Viên!");

            if (LopHocId == null)
                throw new Exception("Không tìm thấy Lớp Học!");

            return await _context.LopHoc_DiemDanhs.Where(x => x.HocVienId == HocVienId && x.LopHocId == LopHocId )
                                .OrderByDescending(x => x.NgayDiemDanh)
                                .Select(x => new DiemDanhViewModel
                                {
                                    IsDuocNghi = x.IsDuocNghi,
                                    IsOff = x.IsOff,
                                    NgayDiemDanh = x.NgayDiemDanh.ToString("dd/MM/yyyy"),
                                    NgayDiemDanh_Date = x.NgayDiemDanh,
                                    Day = x.NgayDiemDanh.Day
                                })
                                .ToListAsync();
        }

        public async Task<List<DiemDanhViewModel>> GetDiemDanhByLopHoc(Guid LopHocId, int month, int year)
        {
            if (LopHocId == null)
                throw new Exception("Không tìm thấy Lớp Học!");
            try
            {
                
                var model = await _context.HocVien_LopHocs
                                .Where(x => x.LopHocId == LopHocId)
                                .SelectMany(x => x.HocVien.LopHoc_DiemDanhs)
                                .Where(x => x.LopHocId == LopHocId)
                                .Where(x => x.HocVien.IsDisabled == false)
                                .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayKetThuc == null || (m.NgayKetThuc.Value.Month >= month && m.NgayKetThuc.Value.Year == year) || m.NgayKetThuc.Value.Year > year)))
                                .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayBatDau.Month <= month && m.NgayBatDau.Year == year) || m.NgayBatDau.Year < year))
                                .Select(x => new DiemDanhViewModel
                                {
                                    IsDuocNghi = x.IsDuocNghi,
                                    IsOff = x.IsOff,
                                    NgayDiemDanh = x.NgayDiemDanh.ToString("dd/MM/yyyy"),
                                    HocVien = x.HocVien.FullName,
                                    NgayDiemDanh_Date = x.NgayDiemDanh,
                                    HocVienId = x.HocVienId,
                                    NgayBatDau = x.LopHoc.HocVien_NgayHocs.FirstOrDefault(m => m.LopHocId == LopHocId && m.HocVienId == x.HocVienId).NgayBatDau,
                                    NgayKetThuc = x.LopHoc.HocVien_NgayHocs.FirstOrDefault(m => m.LopHocId == LopHocId && m.HocVienId == x.HocVienId).NgayKetThuc
                                })
                                //.GroupJoin(_context.LopHoc_DiemDanhs,
                                //i => i.HocVienId,
                                //p => p.HocVienId,
                                //(i, g) =>
                                //new
                                //{
                                //    i = i,
                                //    g = g
                                //})
                                //.SelectMany(
                                //temp0 => temp0.g.DefaultIfEmpty(),
                                //(temp0, cat) =>
                                //    new DiemDanhViewModel
                                //    {
                                //        IsDuocNghi = (cat == null) ? false : cat.IsDuocNghi,
                                //        IsOff = (cat == null) ? true : cat.IsOff,
                                //        NgayDiemDanh = (cat == null) ? new DateTime().ToString("dd/MM/yyyy") : cat.NgayDiemDanh.ToString("dd/MM/yyyy"),
                                //        HocVien = temp0.i.HocVien.FullName,
                                //        NgayDiemDanh_Date = (cat == null) ? new DateTime() : cat.NgayDiemDanh,
                                //        HocVienId = temp0.i.HocVienId,
                                //        NgayBatDau = temp0.i.LopHoc.HocVien_NgayHocs.FirstOrDefault(m => m.LopHocId == LopHocId && m.HocVienId == temp0.i.HocVienId).NgayBatDau,
                                //        NgayKetThuc = temp0.i.LopHoc.HocVien_NgayHocs.FirstOrDefault(m => m.LopHocId == LopHocId && m.HocVienId == temp0.i.HocVienId).NgayKetThuc
                                //    }
                                //)
                                .ToListAsync();


                return model;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<HocVienViewModel>> GetHocVienByLopHoc(Guid LopHocId)
        {
            if (LopHocId == null)
                throw new Exception("Không tìm thấy Lớp Học!");

            return await _context.HocVien_LopHocs
                                .Include(x => x.LopHoc)
                                .Include(x => x.HocVien.HocVien_NgayHocs)
                                .Where(x => x.LopHocId == LopHocId && x.HocVien.IsDisabled == false)
                                .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc.Value >= DateTime.Now)))
                                .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayBatDau <= DateTime.Now)))
                                .Select(x => new HocVienViewModel
                                {
                                    FullName = x.HocVien.FullName,
                                    EnglishName = x.HocVien.EnglishName,
                                    HocVienId = x.HocVienId,
                                })
                                .ToListAsync();
        }

        public async Task<bool> SaveHocVienHoanTac(Guid LopHocId, List<Guid> HocVienIds, List<DateTime> NgayDiemDanhs, string LoggedEmployee)
        {
            try
            {
                if (LopHocId == null || !NgayDiemDanhs.Any())
                    throw new Exception("Lỗi khi Hoan Tác!!!");

                foreach (DateTime NgayDiemDanh in NgayDiemDanhs)
                {
                    var isExisting = await _context.LopHoc_DiemDanhs
                                          .Where(x => x.LopHocId == LopHocId && x.NgayDiemDanh == NgayDiemDanh && HocVienIds.Contains(x.HocVienId))
                                          .ToListAsync();
                    if (isExisting.Any())
                        _context.LopHoc_DiemDanhs.RemoveRange(isExisting);
                }

                var saveResult = await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SaveHocVienOff(Guid LopHocId, List<Guid> HocVienIds, List<DateTime> NgayDiemDanhs, string LoggedEmployee)
        {
            try
            {
                if (LopHocId == null || !NgayDiemDanhs.Any())
                    throw new Exception("Lỗi khi Cho Lớp Học Nghỉ!!!");

                foreach (DateTime NgayDiemDanh in NgayDiemDanhs)
                {
                    var isExisting = await _context.LopHoc_DiemDanhs
                                        .Where(x => x.LopHocId == LopHocId && x.NgayDiemDanh == NgayDiemDanh && HocVienIds.Contains(x.HocVienId))
                                        .ToListAsync();
                    if (isExisting.Any())
                        _context.LopHoc_DiemDanhs.RemoveRange(isExisting);
                    //throw new Exception("Lớp học đã được điểm danh ngày " + NgayDiemDanh.ToShortDateString());

                    var hocViens = await _context.HocVien_LopHocs
                                        .Include(x => x.LopHoc)
                                        .Include(x => x.HocVien.HocVien_NgayHocs)
                                        .Where(x => x.LopHocId == LopHocId && x.HocVien.IsDisabled == false)
                                        .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayKetThuc == null || m.NgayKetThuc.Value >= NgayDiemDanh)))
                                        .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayBatDau <= NgayDiemDanh)))
                                        .Where(x => HocVienIds.Contains(x.HocVienId))
                                        .Select(x => new HocVienViewModel
                                        {
                                            HocVienId = x.HocVienId,
                                        })
                                        .ToListAsync();

                    foreach (var item in hocViens)
                    {
                        LopHoc_DiemDanh lopHoc_DiemDanh = new LopHoc_DiemDanh();
                        lopHoc_DiemDanh.NgayDiemDanh = NgayDiemDanh;
                        lopHoc_DiemDanh.IsOff = true;
                        lopHoc_DiemDanh.IsDuocNghi = true;
                        lopHoc_DiemDanh.LopHocId = LopHocId;
                        lopHoc_DiemDanh.HocVienId = item.HocVienId;
                        lopHoc_DiemDanh.CreatedBy = LoggedEmployee;
                        lopHoc_DiemDanh.CreatedDate = DateTime.Now;
                        _context.LopHoc_DiemDanhs.Add(lopHoc_DiemDanh);
                    }
                }

                var saveResult = await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UndoDuocNghi(Guid LopHocId, DateTime NgayDiemDanh, string LoggedEmployee)
        {
            if (LopHocId == null || NgayDiemDanh == null)
                throw new Exception("Lỗi khi Cho Lớp Học Nghỉ!!!");

            var items = await _context.LopHoc_DiemDanhs
                                    .Where(x => x.LopHocId == LopHocId && x.NgayDiemDanh == NgayDiemDanh)
                                    .ToListAsync();
            if (!items.Any())
                throw new Exception("Lớp học chưa được Cho nghỉ ngày " + NgayDiemDanh.ToShortDateString());

            //foreach (var item in items)
            //{
            //    item.IsOff = false;
            //    item.IsDuocNghi = false;
            //    item.UpdatedBy = LoggedEmployee;
            //    item.UpdatedDate = DateTime.Now;
            //}
            _context.RemoveRange(items);

            var saveResult = await _context.SaveChangesAsync();
            return true;
        }
    }
}
