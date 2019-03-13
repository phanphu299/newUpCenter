﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Data;
using Up.Data.Entities;
using Up.Models;

namespace Up.Services
{
    public class LopHocService : ILopHocService
    {
        private readonly ApplicationDbContext _context;

        public LopHocService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LopHocViewModel> CreateLopHocAsync(string Name, Guid KhoaHocId, Guid NgayHocId, Guid GioHocId, Guid HocPhiId, DateTime NgayKhaiGiang, Guid[] SachIds, string LoggedEmployee)
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
                lopHoc.HocPhiId = HocPhiId;
                lopHoc.CreatedBy = LoggedEmployee;
                lopHoc.CreatedDate = DateTime.Now;

                _context.LopHocs.Add(lopHoc);

                if (SachIds.Length > 0)
                {
                    foreach (Guid item in SachIds)
                    {
                        LopHoc_Sach lopHoc_Sach = new LopHoc_Sach();
                        lopHoc_Sach.LopHoc_SachId = new Guid();
                        lopHoc_Sach.LopHocId = lopHoc.LopHocId;
                        lopHoc_Sach.SachId = item;
                        lopHoc_Sach.CreatedBy = LoggedEmployee;
                        lopHoc_Sach.CreatedDate = DateTime.Now;

                        _context.LopHoc_Sachs.Add(lopHoc_Sach);
                    }
                }
                await _context.SaveChangesAsync();

                return new LopHocViewModel
                {
                    LopHocId = lopHoc.LopHocId,
                    GioHocId = lopHoc.GioHocId,
                    NgayHocId = lopHoc.NgayHocId,
                    NgayHoc = _context.NgayHocs.Find(lopHoc.NgayHocId).Name,
                    Name = lopHoc.Name,
                    NgayKhaiGiang = lopHoc.NgayKhaiGiang.ToString("dd/MM/yyyy"),
                    KhoaHocId = lopHoc.KhoaHocId,
                    CreatedBy = lopHoc.CreatedBy,
                    GioHoc = _context.GioHocs.Find(lopHoc.GioHocId).Name,
                    CreatedDate = lopHoc.CreatedDate.ToString("dd/MM/yyyy"),
                    IsCanceled = lopHoc.IsCanceled,
                    IsGraduated = lopHoc.IsGraduated,
                    HocPhiId = lopHoc.HocPhiId,
                    HocPhi = _context.HocPhis.Find(lopHoc.HocPhiId).Gia,
                    KhoaHoc = _context.KhoaHocs.Find(lopHoc.KhoaHocId).Name,
                    NgayKetThuc = lopHoc.NgayKetThuc != null ? ((DateTime)lopHoc.NgayKetThuc).ToString("dd/MM/yyyy") : "",
                    SachList = _context.LopHoc_Sachs.Where(x => x.LopHocId == lopHoc.LopHocId).Select(x => new SachViewModel
                    {
                        SachId = x.SachId,
                        Gia = x.Sach.Gia,
                        Name = x.Sach.Name
                    }).ToList(),
                    SachIds = SachIds
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

                var _lopHoc_Sach = await _context.LopHoc_Sachs
                                                .Where(x => x.LopHocId == item.LopHocId)
                                                .ToListAsync();

                if (_lopHoc_Sach.Any())
                    _context.LopHoc_Sachs.RemoveRange(_lopHoc_Sach);

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
                    GioHoc = x.GioHoc.Name,
                    IsCanceled = x.IsCanceled,
                    LopHocId = x.LopHocId,
                    NgayHocId = x.NgayHocId,
                    NgayHoc = x.NgayHoc.Name,
                    HocPhiId = x.HocPhiId,
                    HocPhi = x.HocPhi.Gia,
                    NgayKhaiGiang = x.NgayKhaiGiang.ToString("dd/MM/yyyy"),
                    NgayKetThuc = x.NgayKetThuc != null ? ((DateTime)x.NgayKetThuc).ToString("dd/MM/yyyy") : "",
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : "",
                    SachIds = x.LopHoc_Sachs.Select(p => p.SachId).ToArray(),
                    SachList = x.LopHoc_Sachs.Select(p => new SachViewModel
                    {
                        SachId = p.SachId,
                        Gia = p.Sach.Gia,
                        Name = p.Sach.Name
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<LopHocViewModel> UpdateLopHocAsync(Guid LopHocId, string Name, Guid KhoaHocId, Guid NgayHocId,
            Guid GioHocId, Guid HocPhiId, DateTime NgayKhaiGiang, DateTime? NgayKetThuc,
            bool HuyLop, bool TotNghiep, Guid[] SachIds, string LoggedEmployee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name) || KhoaHocId == null || NgayHocId == null || GioHocId == null || NgayKhaiGiang == null || HocPhiId == null)
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
                item.IsCanceled = HuyLop;
                item.IsGraduated = TotNghiep;
                item.HocPhiId = HocPhiId;

                if (NgayKetThuc != null)
                    item.NgayKetThuc = NgayKetThuc;

                var _lopHoc_Sach = await _context.LopHoc_Sachs
                                                    .Where(x => x.LopHocId == item.LopHocId)
                                                    .ToListAsync();

                if (_lopHoc_Sach.Any())
                    _context.LopHoc_Sachs.RemoveRange(_lopHoc_Sach);

                if (SachIds.Length > 0)
                {
                    foreach (Guid sach in SachIds)
                    {
                        LopHoc_Sach lopHoc_Sach = new LopHoc_Sach();
                        lopHoc_Sach.LopHoc_SachId = new Guid();
                        lopHoc_Sach.LopHocId = item.LopHocId;
                        lopHoc_Sach.SachId = sach;
                        lopHoc_Sach.CreatedBy = LoggedEmployee;
                        lopHoc_Sach.CreatedDate = DateTime.Now;

                        _context.LopHoc_Sachs.Add(lopHoc_Sach);
                    }
                }

                await _context.SaveChangesAsync();
                return new LopHocViewModel
                {
                    LopHocId = item.LopHocId,
                    GioHocId = item.GioHocId,
                    NgayHocId = item.NgayHocId,
                    NgayHoc = _context.NgayHocs.Find(item.NgayHocId).Name,
                    Name = item.Name,
                    NgayKhaiGiang = item.NgayKhaiGiang.ToString("dd/MM/yyyy"),
                    KhoaHocId = item.KhoaHocId,
                    CreatedBy = item.CreatedBy,
                    GioHoc = _context.GioHocs.Find(item.GioHocId).Name,
                    HocPhiId = item.HocPhiId,
                    HocPhi = _context.HocPhis.Find(item.HocPhiId).Gia,
                    CreatedDate = item.CreatedDate.ToString("dd/MM/yyyy"),
                    IsCanceled = item.IsCanceled,
                    IsGraduated = item.IsGraduated,
                    KhoaHoc = _context.KhoaHocs.Find(item.KhoaHocId).Name,
                    SachIds = SachIds,
                    NgayKetThuc = item.NgayKetThuc != null ? ((DateTime)item.NgayKetThuc).ToString("dd/MM/yyyy") : "",
                    SachList = item.LopHoc_Sachs.Select(x => new SachViewModel {
                        Gia = x.Sach.Gia,
                        Name = x.Sach.Name,
                        SachId = x.SachId
                    }).ToList()
                };
            }
            catch (Exception exception)
            {
                throw new Exception("Cập nhật lỗi: " + exception.Message);
            }
        }
    }
}
