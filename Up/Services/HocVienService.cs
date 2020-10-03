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

namespace Up.Services
{
    public class HocVienService : IHocVienService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HocVienService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = await _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_HocVien)
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

        public async Task<bool> AddToUnavailableClassAsync(List<Guid> LopHocId, Guid HocVienId, string LoggedEmployee)
        {
            try
            {
                var oldHocVien_LopHoc = _context.HocVien_LopHocs
                .Where(x => LopHocId.Contains(x.LopHocId) && x.HocVienId == HocVienId);

                if (oldHocVien_LopHoc.Any())
                    _context.HocVien_LopHocs.RemoveRange(oldHocVien_LopHoc);

                foreach (var item in LopHocId)
                {
                    HocVien_LopHoc hocVien_LopHoc = new HocVien_LopHoc();
                    hocVien_LopHoc.LopHocId = item;
                    hocVien_LopHoc.HocVienId = HocVienId;
                    hocVien_LopHoc.CreatedBy = LoggedEmployee;
                    hocVien_LopHoc.CreatedDate = DateTime.Now;

                    _context.HocVien_LopHocs.Add(hocVien_LopHoc);
                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Lỗi thêm học viên vào lớp học cũ: " + exception.Message);
            }
        }

        public async Task<HocVienViewModel> CreateHocVienAsync(List<LopHoc_NgayHocViewModel> LopHocList, string FullName, string Phone, string OtherPhone, string FacebookAccount,
            string ParentFullName, string ParentPhone, Guid? QuanHeId,
            string EnglishName, DateTime? NgaySinh, Guid[] LopHocIds, string LoggedEmployee, DateTime? NgayBatDau = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FullName))
                    throw new Exception("Tên Học Viên " +
                        "không được để trống !!!");

                if (string.IsNullOrWhiteSpace(Phone))
                    Phone = "";

                HocVien hocVien = new HocVien();
                hocVien.FullName = FullName;
                hocVien.Phone = Phone;
                hocVien.OtherPhone = OtherPhone;
                hocVien.FacebookAccount = FacebookAccount;
                hocVien.ParentPhone = ParentPhone;
                hocVien.ParentFullName = ParentFullName;
                hocVien.QuanHeId = QuanHeId;
                hocVien.EnglishName = EnglishName;
                hocVien.NgaySinh = NgaySinh;
                hocVien.CreatedBy = LoggedEmployee;
                hocVien.CreatedDate = DateTime.Now;

                _context.HocViens.Add(hocVien);

                if (LopHocIds != null)
                {
                    foreach (Guid item in LopHocIds)
                    {
                        HocVien_LopHoc hocVien_LopHoc = new HocVien_LopHoc();
                        hocVien_LopHoc.HocVien_LopHocId = new Guid();
                        hocVien_LopHoc.HocVienId = hocVien.HocVienId;
                        hocVien_LopHoc.LopHocId = item;
                        hocVien_LopHoc.CreatedBy = LoggedEmployee;
                        hocVien_LopHoc.CreatedDate = DateTime.Now;

                        _context.HocVien_LopHocs.Add(hocVien_LopHoc);

                        if (NgayBatDau != null)
                        {
                            HocVien_NgayHoc HV_NgayHoc = new HocVien_NgayHoc();
                            HV_NgayHoc.HocVien_NgayHocId = new Guid();
                            HV_NgayHoc.HocVienId = hocVien.HocVienId;
                            HV_NgayHoc.LopHocId = item;
                            HV_NgayHoc.NgayBatDau = NgayBatDau.Value;
                            HV_NgayHoc.NgayKetThuc = null;
                            HV_NgayHoc.CreatedBy = LoggedEmployee;
                            HV_NgayHoc.CreatedDate = DateTime.Now;

                            _context.HocVien_NgayHocs.Add(HV_NgayHoc);
                        }
                    }
                }

                if(LopHocList.Any())
                {
                    foreach (var item in LopHocList)
                    {
                        HocVien_LopHoc hocVien_LopHoc = new HocVien_LopHoc();
                        hocVien_LopHoc.HocVien_LopHocId = new Guid();
                        hocVien_LopHoc.HocVienId = hocVien.HocVienId;
                        hocVien_LopHoc.LopHocId = item.LopHoc.LopHocId;
                        hocVien_LopHoc.CreatedBy = LoggedEmployee;
                        hocVien_LopHoc.CreatedDate = DateTime.Now;

                        _context.HocVien_LopHocs.Add(hocVien_LopHoc);

                        HocVien_NgayHoc HV_NgayHoc = new HocVien_NgayHoc();
                        HV_NgayHoc.HocVien_NgayHocId = new Guid();
                        HV_NgayHoc.HocVienId = hocVien.HocVienId;
                        HV_NgayHoc.LopHocId = item.LopHoc.LopHocId;
                        HV_NgayHoc.NgayBatDau = Convert.ToDateTime(item.NgayHoc, System.Globalization.CultureInfo.InvariantCulture); 
                        HV_NgayHoc.NgayKetThuc = null;
                        HV_NgayHoc.CreatedBy = LoggedEmployee;
                        HV_NgayHoc.CreatedDate = DateTime.Now;

                        _context.HocVien_NgayHocs.Add(HV_NgayHoc);
                    }
                }

                await _context.SaveChangesAsync();

                return new HocVienViewModel
                {
                    FullName = hocVien.FullName,
                    NgaySinh = hocVien.NgaySinh == null ? "" : hocVien.NgaySinh.Value.ToString("dd/MM/yyyy"),
                    EnglishName = hocVien.EnglishName,
                    QuanHeId = hocVien.QuanHeId,
                    CreatedBy = hocVien.CreatedBy,
                    CreatedDate = hocVien.CreatedDate.ToString("dd/MM/yyyy"),
                    FacebookAccount = hocVien.FacebookAccount,
                    IsDisabled = hocVien.IsDisabled,
                    ParentFullName = hocVien.ParentFullName,
                    ParentPhone = hocVien.ParentPhone,
                    Phone = hocVien.Phone,
                    OtherPhone = hocVien.OtherPhone,
                    HocVienId = hocVien.HocVienId,
                    QuanHe = _context.QuanHes.FindAsync(hocVien.QuanHeId).Result == null ? "" : _context.QuanHes.FindAsync(hocVien.QuanHeId).Result.Name,
                    LopHocList = await _context.HocVien_LopHocs.Where(x => x.HocVienId == hocVien.HocVienId).Select(x => new LopHocViewModel
                    {
                        LopHocId = x.LopHocId,
                        Name = x.LopHoc.Name,
                        IsCanceled = x.LopHoc.IsCanceled,
                        IsGraduated = x.LopHoc.IsGraduated
                    }).ToListAsync(),
                    LopHocIds = LopHocIds,
                    LopHoc_NgayHocList = await _context.HocVien_LopHocs
                                        .Where(x => x.HocVienId == hocVien.HocVienId)
                                        .Select(x => new LopHoc_NgayHocViewModel
                                        {
                                            LopHoc = new LopHocViewModel
                                            {
                                                LopHocId = x.LopHocId,
                                                Name = x.LopHoc.Name
                                            },
                                            NgayHoc = x.LopHoc.HocVien_NgayHocs.FirstOrDefault(m => m.HocVienId == hocVien.HocVienId) != null ? x.LopHoc.HocVien_NgayHocs.FirstOrDefault(m => m.HocVienId == hocVien.HocVienId).NgayBatDau.ToString("yyyy-MM-dd") : ""
                                        })
                                        .ToListAsync()
                };
            }
            catch (Exception exception)
            {
                throw new Exception("Lỗi khi tạo mới: " + exception.Message);
            }
        }

        public async Task<bool> DeleteHocVienAsync(Guid HocVienId, string LoggedEmployee)
        {
            try
            {
                var item = await _context.HocViens
                                    .Where(x => x.HocVienId == HocVienId)
                                    .SingleOrDefaultAsync();

                if (item == null)
                    throw new Exception("Không tìm thấy Học Viên !!!");

                item.IsDisabled = true;
                item.UpdatedBy = LoggedEmployee;
                item.UpdatedDate = DateTime.Now;

                var hocVien_LopHoc = await _context.HocVien_LopHocs.Where(x => x.HocVienId == HocVienId).ToListAsync();
                if (hocVien_LopHoc.Any())
                    _context.HocVien_LopHocs.RemoveRange(hocVien_LopHoc);

                var saveResult = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Xóa lỗi: " + exception.Message);
            }
        }

        public async Task<List<HocVienViewModel>> GetAllHocVienAsync()
        {
            return await _context.HocViens
                .Include(x => x.HocVien_LopHocs)
                .ThenInclude(x => x.LopHoc)
                .Select(x => new HocVienViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    EnglishName = x.EnglishName,
                    FacebookAccount = x.FacebookAccount,
                    FullName = x.FullName,
                    HocVienId = x.HocVienId,
                    IsDisabled = x.IsDisabled,
                    NgaySinh = x.NgaySinh == null ? "" : x.NgaySinh.Value.ToString("dd/MM/yyyy"),
                    ParentFullName = x.ParentFullName,
                    ParentPhone = x.ParentPhone,
                    Phone = x.Phone,
                    OtherPhone = x.OtherPhone,
                    QuanHe = x.QuanHe.Name,
                    QuanHeId = x.QuanHeId,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : "",
                    LopHocIds = x.HocVien_LopHocs.Select(p => p.LopHocId).ToArray(),
                    LopHocList = x.HocVien_LopHocs.Select(p => new LopHocViewModel
                    {
                        LopHocId = p.LopHocId,
                        Name = p.LopHoc.Name,
                        IsGraduated = p.LopHoc.IsGraduated,
                        IsCanceled = p.LopHoc.IsCanceled,
                        HocVienNghi = p.HocVien.HocVien_NgayHocs.FirstOrDefault(n => n.LopHocId == p.LopHocId).NgayKetThuc == null ? false : p.HocVien.HocVien_NgayHocs.FirstOrDefault(n => n.LopHocId == p.LopHocId).NgayKetThuc < DateTime.Now ? true : false
                    }).ToList(),
                    LopHoc_NgayHocList = x.HocVien_LopHocs
                                        .Select(m => new LopHoc_NgayHocViewModel
                                        {
                                            LopHoc = new LopHocViewModel
                                            {
                                                LopHocId = m.LopHocId,
                                                Name = m.LopHoc.Name
                                            },
                                            NgayHoc = m.LopHoc.HocVien_NgayHocs.FirstOrDefault(t => t.HocVienId == x.HocVienId) != null ? m.LopHoc.HocVien_NgayHocs.FirstOrDefault(t => t.HocVienId == x.HocVienId).NgayBatDau.ToString("yyyy-MM-dd") : ""
                                        })
                                        .ToList()
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<HocVienViewModel>> GetHocVienAsync()
        {
            try
            {
                return await _context.HocViens
                .Where(x => x.IsDisabled == false)
                .Include(x => x.HocVien_LopHocs)
                .ThenInclude(x => x.LopHoc)
                .Select(x => new HocVienViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    EnglishName = x.EnglishName,
                    FacebookAccount = x.FacebookAccount,
                    FullName = x.FullName,
                    HocVienId = x.HocVienId,
                    IsDisabled = x.IsDisabled,
                    NgaySinh = x.NgaySinh == null ? "" : x.NgaySinh.Value.ToString("dd/MM/yyyy"),
                    ParentFullName = x.ParentFullName,
                    ParentPhone = x.ParentPhone,
                    Phone = x.Phone,
                    OtherPhone = x.OtherPhone,
                    QuanHe = x.QuanHe.Name,
                    QuanHeId = x.QuanHeId,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : "",
                    LopHocIds = x.HocVien_LopHocs.Select(p => p.LopHocId).ToArray(),
                    LopHocList = x.HocVien_LopHocs.Select(p => new LopHocViewModel
                    {
                        LopHocId = p.LopHocId,
                        Name = p.LopHoc.Name,
                        IsGraduated = p.LopHoc.IsGraduated,
                        IsCanceled = p.LopHoc.IsCanceled
                    }).ToList(),
                    LopHoc_NgayHocList = x.HocVien_LopHocs
                                        .Select(m => new LopHoc_NgayHocViewModel
                                        {
                                            LopHoc = new LopHocViewModel
                                            {
                                                LopHocId = m.LopHocId,
                                                Name = m.LopHoc.Name
                                            },
                                            NgayHoc = m.LopHoc.HocVien_NgayHocs.FirstOrDefault(t => t.HocVienId == x.HocVienId) != null ? m.LopHoc.HocVien_NgayHocs.FirstOrDefault(t => t.HocVienId == x.HocVienId).NgayBatDau.ToString("yyyy-MM-dd") : ""
                                        })
                                        .ToList()
                })
                .AsNoTracking()
                .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ToggleChenAsync(Guid HocVienId, string LoggedEmployee)
        {
            try
            {
                var item = await _context.HocViens
                                        .Where(x => x.HocVienId == HocVienId)
                                        .SingleOrDefaultAsync();

                if (item == null)
                    throw new Exception("Không tìm thấy Học Viên!!!");

                item.UpdatedBy = LoggedEmployee;
                item.UpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Cập nhật lỗi: " + exception.Message);
            }
        }

        public async Task<HocVienViewModel> UpdateHocVienAsync(List<LopHoc_NgayHocViewModel> LopHocList, Guid HocVienId, string FullName, string Phone, string OtherPhone, string FacebookAccount,
           string ParentFullName, string ParentPhone, Guid? QuanHeId, string EnglishName,
           DateTime? NgaySinh, Guid[] LopHocIds, string LoggedEmployee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FullName))
                    throw new Exception("Tên Học Viên, SĐT không được để trống !!!");

                if (string.IsNullOrWhiteSpace(Phone))
                    Phone = "";

                var item = await _context.HocViens
                                        .Where(x => x.HocVienId == HocVienId)
                                        .SingleOrDefaultAsync();

                if (item == null)
                    throw new Exception("Không tìm thấy Học Viên!!!");

                item.FullName = FullName;
                item.QuanHeId = QuanHeId;
                item.Phone = Phone;
                item.OtherPhone = OtherPhone;
                item.FacebookAccount = FacebookAccount;
                item.ParentFullName = ParentFullName;
                item.ParentPhone = ParentPhone;
                item.EnglishName = EnglishName;
                item.NgaySinh = NgaySinh;
                item.UpdatedBy = LoggedEmployee;
                item.UpdatedDate = DateTime.Now;

                var _hocVien_LopHoc = await _context.HocVien_LopHocs
                                                    .Where(x => x.HocVienId == item.HocVienId)
                                                    .ToListAsync();

                _context.HocVien_LopHocs.RemoveRange(_hocVien_LopHoc);

                var _lopHocIds = LopHocList.Select(m => m.LopHoc.LopHocId).ToList();
                var _hocVien_NgayHoc_Dumb = await _context.HocVien_NgayHocs
                                                    .Where(x => x.HocVienId == HocVienId && !_lopHocIds.Contains(x.LopHocId))
                                                    .ToListAsync();

                _context.HocVien_NgayHocs.RemoveRange(_hocVien_NgayHoc_Dumb);

                if (LopHocList.Any())
                {
                    foreach (var itemLop in LopHocList)
                    {
                        HocVien_LopHoc hocVien_LopHoc = new HocVien_LopHoc();
                        hocVien_LopHoc.HocVien_LopHocId = new Guid();
                        hocVien_LopHoc.HocVienId = item.HocVienId;
                        hocVien_LopHoc.LopHocId = itemLop.LopHoc.LopHocId;
                        hocVien_LopHoc.CreatedBy = LoggedEmployee;
                        hocVien_LopHoc.CreatedDate = DateTime.Now;

                        _context.HocVien_LopHocs.Add(hocVien_LopHoc);

                        var _hocVien_NgayHoc = await _context.HocVien_NgayHocs
                                                    .FirstOrDefaultAsync(x => x.HocVienId == item.HocVienId && x.LopHocId == itemLop.LopHoc.LopHocId);

                        if(_hocVien_NgayHoc == null)
                        {
                            HocVien_NgayHoc HV_NgayHoc = new HocVien_NgayHoc();
                            HV_NgayHoc.HocVien_NgayHocId = new Guid();
                            HV_NgayHoc.HocVienId = item.HocVienId;
                            HV_NgayHoc.LopHocId = itemLop.LopHoc.LopHocId;
                            HV_NgayHoc.NgayBatDau = Convert.ToDateTime(itemLop.NgayHoc, System.Globalization.CultureInfo.InvariantCulture);
                            HV_NgayHoc.NgayKetThuc = null;
                            HV_NgayHoc.CreatedBy = LoggedEmployee;
                            HV_NgayHoc.CreatedDate = DateTime.Now;

                            _context.HocVien_NgayHocs.Add(HV_NgayHoc);
                        }
                        else
                            _hocVien_NgayHoc.NgayBatDau = Convert.ToDateTime(itemLop.NgayHoc, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }

                await _context.SaveChangesAsync();
                return new HocVienViewModel
                {
                    FullName = item.FullName,
                    NgaySinh = item.NgaySinh == null ? "" : item.NgaySinh.Value.ToString("dd/MM/yyyy"),
                    EnglishName = item.EnglishName,
                    HocVienId = item.HocVienId,
                    ParentFullName = item.ParentFullName,
                    ParentPhone = item.ParentPhone,
                    FacebookAccount = item.FacebookAccount,
                    Phone = item.Phone,
                    OtherPhone = item.OtherPhone,
                    QuanHeId = item.QuanHeId,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate?.ToString("dd/MM/yyyy"),
                    QuanHe = _context.QuanHes.FindAsync(item.QuanHeId).Result != null ? _context.QuanHes.FindAsync(item.QuanHeId).Result.Name : "",
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate.ToString("dd/MM/yyyy"),
                    LopHocIds = LopHocIds,
                    LopHocList = await _context.HocVien_LopHocs
                                        .Where(x => x.HocVienId == item.HocVienId)
                                        .Select(x => new LopHocViewModel
                                        {
                                            Name = x.LopHoc.Name,
                                            LopHocId = x.LopHocId,
                                            IsCanceled = x.LopHoc.IsCanceled,
                                            IsGraduated = x.LopHoc.IsGraduated
                                        }).ToListAsync(),
                    LopHoc_NgayHocList = _context.HocVien_LopHocs
                                        .Where(m => m.HocVienId == item.HocVienId)
                                        .Select(m => new LopHoc_NgayHocViewModel
                                        {
                                            LopHoc = new LopHocViewModel
                                            {
                                                LopHocId = m.LopHocId,
                                                Name = m.LopHoc.Name
                                            },
                                            NgayHoc = m.LopHoc.HocVien_NgayHocs.FirstOrDefault(t => t.HocVienId == item.HocVienId) != null ? m.LopHoc.HocVien_NgayHocs.FirstOrDefault(t => t.HocVienId == item.HocVienId).NgayBatDau.ToString("yyyy-MM-dd") : ""
                                        })
                                        .ToList()
                };
            }
            catch (Exception exception)
            {
                throw new Exception("Cập nhật lỗi: " + exception.Message);
            }
        }

        public async Task<List<HocVienViewModel>> GetHocVienByNameAsync(string name)
        {
            try
            {
                return await _context.HocViens
                .Where(x => x.IsDisabled == false && x.FullName.ToLower().Contains(name.ToLower()))
                .Select(x => new HocVienViewModel
                {
                    FullName = x.FullName,
                    HocVienId = x.HocVienId,
                    IsDisabled = x.IsDisabled,
                    NgaySinh = x.NgaySinh == null ? "" : x.NgaySinh.Value.ToString("dd/MM/yyyy"),
                    Phone = x.Phone,
                })
                .AsNoTracking()
                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
