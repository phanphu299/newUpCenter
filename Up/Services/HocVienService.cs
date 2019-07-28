using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Data;
using Up.Data.Entities;
using Up.Models;

namespace Up.Services
{
    public class HocVienService : IHocVienService
    {
        private readonly ApplicationDbContext _context;

        public HocVienService(ApplicationDbContext context)
        {
            _context = context;
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
            catch(Exception exception)
            {
                throw new Exception("Lỗi thêm học viên vào lớp học cũ: " + exception.Message);
            }
        }

        public async Task<HocVienViewModel> CreateHocVienAsync(string FullName, string Phone, string FacebookAccount, 
            string ParentFullName, Guid? QuanHeId, 
            string EnglishName, DateTime? NgaySinh, Guid[] LopHocIds, string LoggedEmployee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Phone))
                    throw new Exception("Tên Học Viên, Phone " +
                        "không được để trống !!!");

                HocVien hocVien = new HocVien();
                hocVien.FullName = FullName;
                hocVien.Phone = Phone;
                hocVien.FacebookAccount = FacebookAccount;
                hocVien.ParentFullName = ParentFullName;
                hocVien.QuanHeId = QuanHeId;
                hocVien.EnglishName = EnglishName;
                hocVien.NgaySinh = NgaySinh;
                hocVien.CreatedBy = LoggedEmployee;
                hocVien.CreatedDate = DateTime.Now;

                _context.HocViens.Add(hocVien);

                if (LopHocIds.Length > 0)
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
                    Phone = hocVien.Phone,
                    HocVienId = hocVien.HocVienId,
                    QuanHe = _context.QuanHes.FindAsync(hocVien.QuanHeId).Result == null ? "" : _context.QuanHes.FindAsync(hocVien.QuanHeId).Result.Name,
                    LopHocList = await _context.HocVien_LopHocs.Where(x => x.HocVienId == hocVien.HocVienId).Select(x => new LopHocViewModel
                    {
                        LopHocId = x.LopHocId,
                        Name = x.LopHoc.Name,
                        IsCanceled = x.LopHoc.IsCanceled,
                        IsGraduated = x.LopHoc.IsGraduated
                    }).ToListAsync(),
                    LopHocIds = LopHocIds
                };
            }
            catch (Exception exception)
            {
                throw new Exception("Lỗi khi tạo mới: " + exception.Message);
            }
        }

        public async Task<bool> DeleteHocVienAsync(Guid HocVienId, string LoggedEmployee)
        {
            //var hocVien = await _context.HocViens.Where(x => x.HocVienId == HocVienId).ToListAsync();
            //if (lopHoc.Any())
            //    throw new Exception("Hãy xóa những lớp học có học phí này trước !!!");

            var item = await _context.HocViens
                                    .Where(x => x.HocVienId == HocVienId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Học Viên !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<HocVienViewModel>> GetAllHocVienAsync()
        {
            return await _context.HocViens
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
                    Phone = x.Phone,
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
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<HocVienViewModel>> GetHocVienAsync()
        {
            return await _context.HocViens
                .Where(x => x.IsDisabled == false)
                .Select(x => new HocVienViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    EnglishName  = x.EnglishName,
                    FacebookAccount = x.FacebookAccount,
                    FullName = x.FullName,
                    HocVienId = x.HocVienId,
                    IsDisabled = x.IsDisabled,
                    NgaySinh = x.NgaySinh == null ? "" : x.NgaySinh.Value.ToString("dd/MM/yyyy"),
                    ParentFullName = x.ParentFullName,
                    Phone = x.Phone,
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
                    }).ToList()
                })
                .ToListAsync();
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

        public async Task<HocVienViewModel> UpdateHocVienAsync(Guid HocVienId, string FullName, string Phone, string FacebookAccount,
           string ParentFullName, Guid? QuanHeId, string EnglishName,
           DateTime? NgaySinh, Guid[] LopHocIds, string LoggedEmployee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Phone))
                    throw new Exception("Tên Học Viên, SĐT không được để trống !!!");

                var item = await _context.HocViens
                                        .Where(x => x.HocVienId == HocVienId)
                                        .SingleOrDefaultAsync();

                if (item == null)
                    throw new Exception("Không tìm thấy Học Viên!!!");

                item.FullName = FullName;
                item.QuanHeId = QuanHeId;
                item.Phone = Phone;
                item.FacebookAccount = FacebookAccount;
                item.ParentFullName = ParentFullName;
                item.EnglishName = EnglishName;
                item.NgaySinh = NgaySinh;
                item.UpdatedBy = LoggedEmployee;
                item.UpdatedDate = DateTime.Now;

                var _hocVien_LopHoc = await _context.HocVien_LopHocs
                                                    .Where(x => x.HocVienId == item.HocVienId && x.LopHoc.IsCanceled == false && x.LopHoc.IsDisabled == false && x.LopHoc.IsGraduated == false)
                                                    .ToListAsync();

                if (_hocVien_LopHoc.Any())
                    _context.HocVien_LopHocs.RemoveRange(_hocVien_LopHoc);

                if (LopHocIds.Length > 0)
                {
                    foreach (Guid lophoc in LopHocIds)
                    {
                        HocVien_LopHoc hocVien_LopHoc = new HocVien_LopHoc();
                        hocVien_LopHoc.HocVienId = new Guid();
                        hocVien_LopHoc.HocVienId = item.HocVienId;
                        hocVien_LopHoc.LopHocId = lophoc;
                        hocVien_LopHoc.CreatedBy = LoggedEmployee;
                        hocVien_LopHoc.CreatedDate = DateTime.Now;

                        _context.HocVien_LopHocs.Add(hocVien_LopHoc);
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
                    FacebookAccount = item.FacebookAccount,
                    Phone = item.Phone,
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
                                        }).ToListAsync()
                };
            }
            catch (Exception exception)
            {
                throw new Exception("Cập nhật lỗi: " + exception.Message);
            }
        }
    }
}
