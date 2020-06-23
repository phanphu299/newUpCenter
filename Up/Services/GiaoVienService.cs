﻿namespace Up.Services
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

    public class GiaoVienService : IGiaoVienService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GiaoVienService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = await _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_NhanVien)
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

        public async Task<GiaoVienViewModel> CreateGiaoVienAsync(List<LoaiNhanVien_CheDoViewModel> LoaiNhanVien_CheDo, string Name, string Phone, double TeachingRate, double TutoringRate, double BasicSalary, string FacebookAccount, string DiaChi, string InitialName, string CMND, double HoaHong, Guid NgayLamViecId, DateTime NgayBatDau, DateTime? NgayKetThuc, string NganHang, string LoggedEmployee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(DiaChi) || string.IsNullOrWhiteSpace(InitialName) || string.IsNullOrWhiteSpace(CMND))
                    throw new Exception("Tên Nhân Viên, SĐT, Địa Chỉ, Initial Name, CMND không được để trống !!!");

                GiaoVien giaoVien = new GiaoVien();
                giaoVien.GiaoVienId = new Guid();
                giaoVien.Name = Name;
                giaoVien.Phone = Phone;
                giaoVien.FacebookAccount = FacebookAccount;
                giaoVien.DiaChi = DiaChi;
                giaoVien.InitialName = InitialName;
                giaoVien.CMND = CMND;
                giaoVien.TeachingRate = TeachingRate;
                giaoVien.TutoringRate = TutoringRate;
                giaoVien.BasicSalary = BasicSalary;
                giaoVien.CreatedBy = LoggedEmployee;
                giaoVien.CreatedDate = DateTime.Now;
                giaoVien.MucHoaHong = HoaHong;
                giaoVien.NgayKetThuc = NgayKetThuc;
                giaoVien.NgayLamViecId = NgayLamViecId;
                giaoVien.NgayBatDau = NgayBatDau;
                giaoVien.NganHang = NganHang;

                _context.GiaoViens.Add(giaoVien);

                foreach (var item in LoaiNhanVien_CheDo)
                {
                    NhanVien_ViTri nhanVien_ViTri = new NhanVien_ViTri
                    {
                        NhanVien_ViTriId = new Guid(),
                        NhanVienId = giaoVien.GiaoVienId,
                        CreatedBy = LoggedEmployee,
                        CreatedDate = DateTime.Now,
                        CheDoId = item.LoaiCheDo.LoaiCheDoId,
                        ViTriId = item.LoaiGiaoVien.LoaiGiaoVienId
                    };
                    await _context.NhanVien_ViTris.AddAsync(nhanVien_ViTri);
                }

                await _context.SaveChangesAsync();

                return new GiaoVienViewModel
                {
                    GiaoVienId = giaoVien.GiaoVienId,
                    Name = giaoVien.Name,
                    InitialName = giaoVien.InitialName,
                    BasicSalary = giaoVien.BasicSalary,
                    TutoringRate = giaoVien.TutoringRate,
                    TeachingRate = giaoVien.TeachingRate,
                    CMND = giaoVien.CMND,
                    DiaChi = giaoVien.DiaChi,
                    FacebookAccount = giaoVien.FacebookAccount,
                    Phone = giaoVien.Phone,
                    CreatedBy = giaoVien.CreatedBy,
                    CreatedDate = giaoVien.CreatedDate.ToString("dd/MM/yyyy"),
                    MucHoaHong = giaoVien.MucHoaHong,
                    NgayBatDau = giaoVien.NgayBatDau.ToString("dd/MM/yyyy"),
                    NgayKetThuc = giaoVien.NgayKetThuc != null ? ((DateTime)giaoVien.NgayKetThuc).ToString("dd/MM/yyyy") : "",
                    NgayLamViecId = giaoVien.NgayLamViecId,
                    NgayLamViec = _context.NgayLamViecs.FindAsync(giaoVien.NgayLamViecId).Result.Name,
                    NganHang = giaoVien.NganHang,
                    LoaiNhanVien_CheDo = await _context.NhanVien_ViTris
                                        .Where(x => x.NhanVienId == giaoVien.GiaoVienId)
                                        .Select(x => new LoaiNhanVien_CheDoViewModel
                                        {
                                            LoaiCheDo = new LoaiCheDoViewModel
                                            {
                                                LoaiCheDoId = x.CheDoId,
                                                Name = x.CheDo.Name
                                            },
                                            LoaiGiaoVien = new LoaiGiaoVienViewModel
                                            {
                                                LoaiGiaoVienId = x.ViTriId,
                                                Name = x.ViTri.Name
                                            }
                                        })
                                        .AsNoTracking()
                                        .ToListAsync()
                };
            }
            catch(Exception exception)
            {
                throw new Exception("Lỗi khi lưu Nhân Viên : " + exception.Message);
            }
        }

        public async Task<bool> DeleteGiaoVienAsync(Guid GiaoVienId, string LoggedEmployee)
        {
            var item = await _context.GiaoViens
                                    .FindAsync(GiaoVienId);

            if (item == null)
                throw new Exception("Không tìm thấy Nhân Viên !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<GiaoVienViewModel>> GetGiaoVienAsync()
        {
            return await _context.GiaoViens
                .Where(x => x.IsDisabled == false)
                .Select(x => new GiaoVienViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    GiaoVienId = x.GiaoVienId,
                    Phone = x.Phone,
                    FacebookAccount = x.FacebookAccount,
                    DiaChi = x.DiaChi,
                    CMND = x.CMND,
                    TeachingRate = x.TeachingRate,
                    TutoringRate = x.TutoringRate,
                    BasicSalary = x.BasicSalary,
                    InitialName = x.InitialName,
                    Name = x.Name,
                    MucHoaHong = x.MucHoaHong,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : "",
                    NgayBatDau = x.NgayBatDau.ToString("dd/MM/yyyy"),
                    NgayKetThuc = x.NgayKetThuc != null ? ((DateTime)x.NgayKetThuc).ToString("dd/MM/yyyy") : "",
                    NgayLamViecId = x.NgayLamViecId,
                    NgayLamViec = x.NgayLamViec.Name,
                    NganHang = x.NganHang, 
                    LoaiNhanVien_CheDo = _context.NhanVien_ViTris
                                        .Where(m => m.NhanVienId == x.GiaoVienId)
                                        .Select(m => new LoaiNhanVien_CheDoViewModel
                                        {
                                            LoaiCheDo = new LoaiCheDoViewModel
                                            {
                                                LoaiCheDoId = m.CheDoId,
                                                Name = m.CheDo.Name
                                            },
                                            LoaiGiaoVien = new LoaiGiaoVienViewModel
                                            {
                                                LoaiGiaoVienId = m.ViTriId,
                                                Name = m.ViTri.Name
                                            }
                                        })
                                        .AsNoTracking()
                                        .ToList()
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<GiaoVienViewModel>> GetGiaoVienOnlyAsync()
        {
            return await _context.GiaoViens
                .Where(x => x.IsDisabled == false && x.NhanVien_ViTris.Any(m => m.CheDoId == LoaiNhanVienEnums.GiaoVien.ToId()))
                .Select(x => new GiaoVienViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    GiaoVienId = x.GiaoVienId,
                    Phone = x.Phone,
                    FacebookAccount = x.FacebookAccount,
                    DiaChi = x.DiaChi,
                    CMND = x.CMND,
                    TeachingRate = x.TeachingRate,
                    TutoringRate = x.TutoringRate,
                    BasicSalary = x.BasicSalary,
                    InitialName = x.InitialName,
                    MucHoaHong = x.MucHoaHong,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<GiaoVienViewModel> UpdateGiaoVienAsync(List<LoaiNhanVien_CheDoViewModel> LoaiNhanVien_CheDo, Guid GiaoVienId, string Name, string Phone, double TeachingRate, double TutoringRate, double BasicSalary, string FacebookAccount, string DiaChi, string InitialName, string CMND, double HoaHong, Guid NgayLamViecId, DateTime NgayBatDau, DateTime? NgayKetThuc, string NganHang, string LoggedEmployee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(DiaChi) || string.IsNullOrWhiteSpace(InitialName) || string.IsNullOrWhiteSpace(CMND))
                    throw new Exception("Tên Giáo Viên, SĐT, Địa Chỉ, Initial Name, CMND không được để trống !!!");

                var item = await _context.GiaoViens
                                        .Where(x => x.GiaoVienId == GiaoVienId)
                                        .SingleOrDefaultAsync();

                if (item == null)
                    throw new Exception("Không tìm thấy Nhân Viên!!!");

                item.Name = Name;
                item.Phone = Phone;
                item.FacebookAccount = FacebookAccount;
                item.DiaChi = DiaChi;
                item.InitialName = InitialName;
                item.CMND = CMND;
                item.GiaoVienId = GiaoVienId;
                item.TeachingRate = TeachingRate;
                item.TutoringRate = TutoringRate;
                item.BasicSalary = BasicSalary;
                item.UpdatedBy = LoggedEmployee;
                item.UpdatedDate = DateTime.Now;
                item.MucHoaHong = HoaHong;
                item.NgayBatDau = NgayBatDau;
                item.NgayKetThuc = NgayKetThuc;
                item.NgayLamViecId = NgayLamViecId;
                item.NganHang = NganHang;

                var viTris = _context.NhanVien_ViTris.Where(x => x.NhanVienId == item.GiaoVienId);
                _context.NhanVien_ViTris.RemoveRange(viTris);

                foreach (var viTri in LoaiNhanVien_CheDo)
                {
                    NhanVien_ViTri nhanVien_ViTri = new NhanVien_ViTri
                    {
                        NhanVien_ViTriId = new Guid(),
                        NhanVienId = item.GiaoVienId,
                        CreatedBy = LoggedEmployee,
                        CreatedDate = DateTime.Now,
                        CheDoId = viTri.LoaiCheDo.LoaiCheDoId,
                        ViTriId = viTri.LoaiGiaoVien.LoaiGiaoVienId
                    };
                    await _context.NhanVien_ViTris.AddAsync(nhanVien_ViTri);
                }

                await _context.SaveChangesAsync();
                return new GiaoVienViewModel
                {
                    GiaoVienId = item.GiaoVienId,
                    BasicSalary = item.BasicSalary,
                    CMND = item.CMND,
                    DiaChi = item.DiaChi,
                    InitialName = item.InitialName,
                    Name = item.Name,
                    TeachingRate = item.TeachingRate,
                    TutoringRate = item.TutoringRate,
                    FacebookAccount = item.FacebookAccount,
                    Phone = item.Phone,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate?.ToString("dd/MM/yyyy"),
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate.ToString("dd/MM/yyyy"),
                    MucHoaHong = item.MucHoaHong,
                    NgayBatDau = item.NgayBatDau.ToString("dd/MM/yyyy"),
                    NgayKetThuc = item.NgayKetThuc != null ? ((DateTime)item.NgayKetThuc).ToString("dd/MM/yyyy") : "",
                    NgayLamViecId = item.NgayLamViecId,
                    NgayLamViec = _context.NgayLamViecs.FindAsync(item.NgayLamViecId).Result.Name,
                    NganHang = item.NganHang,
                    LoaiNhanVien_CheDo = _context.NhanVien_ViTris
                                        .Where(m => m.NhanVienId == item.GiaoVienId)
                                        .Select(m => new LoaiNhanVien_CheDoViewModel
                                        {
                                            LoaiCheDo = new LoaiCheDoViewModel
                                            {
                                                LoaiCheDoId = m.CheDoId,
                                                Name = m.CheDo.Name
                                            },
                                            LoaiGiaoVien = new LoaiGiaoVienViewModel
                                            {
                                                LoaiGiaoVienId = m.ViTriId,
                                                Name = m.ViTri.Name
                                            }
                                        })
                                        .AsNoTracking()
                                        .ToList()
                };
            }
            catch (Exception exception)
            {
                throw new Exception("Cập nhật lỗi: " + exception.Message);
            }
        }
    }
}
