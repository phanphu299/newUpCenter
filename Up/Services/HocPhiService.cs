
namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Data.Entities;
    using Up.Models;

    public class HocPhiService : IHocPhiService
    {
        private readonly ApplicationDbContext _context;

        public HocPhiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HocPhiViewModel> CreateHocPhiAsync(double Gia, string GhiChu, DateTime NgayApDung, string LoggedEmployee)
        {
            HocPhi hocPhi = new HocPhi();
            hocPhi.HocPhiId = new Guid();
            hocPhi.Gia = Gia;
            hocPhi.GhiChu = GhiChu;
            hocPhi.NgayApDung = NgayApDung;
            hocPhi.CreatedBy = LoggedEmployee;
            hocPhi.CreatedDate = DateTime.Now;

            _context.HocPhis.Add(hocPhi);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Học Phí !!!");
            return new HocPhiViewModel
            {
                HocPhiId = hocPhi.HocPhiId,
                Gia = hocPhi.Gia,
                GhiChu = hocPhi.GhiChu,
                NgayApDung = hocPhi.NgayApDung?.ToString("dd/MM/yyyy"),
                CreatedBy = hocPhi.CreatedBy,
                CreatedDate = hocPhi.CreatedDate.ToString("dd/MM/yyyy")
            };
        }

        public async Task<bool> DeleteHocPhiAsync(Guid HocPhiId, string LoggedEmployee)
        {
            var lopHoc = await _context.LopHocs.Where(x => x.HocPhiId == HocPhiId).ToListAsync();
            if (lopHoc.Any())
                throw new Exception("Hãy xóa những lớp học có học phí này trước !!!");

            var item = await _context.HocPhis
                                    .Where(x => x.HocPhiId == HocPhiId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Học Phí !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<HocPhiViewModel>> GetHocPhiAsync()
        {
            return await _context.HocPhis
                .Where(x => x.IsDisabled == false)
                .Select(x => new HocPhiViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    HocPhiId = x.HocPhiId,
                    Gia = x.Gia,
                    NgayApDung = x.NgayApDung == null ? "" : ((DateTime)x.NgayApDung).ToString("dd/MM/yyyy"),
                    GhiChu = x.GhiChu,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<int> TinhSoNgayHocAsync(Guid LopHocId, int month, int year)
        {
            var item = await _context.LopHocs
                                    .Include(x => x.NgayHoc)
                                    .Where(x => x.LopHocId == LopHocId)
                                    .SingleOrDefaultAsync();

            var ngayHoc = item.NgayHoc.Name.Split('-');
            int tongNgayHoc = 0;

            foreach (string el in ngayHoc)
            {
                switch (el.Trim())
                {
                    case "2":
                        tongNgayHoc += DaysInMonth(year, month, DayOfWeek.Monday).Count();
                        break;
                    case "3":
                        tongNgayHoc += DaysInMonth(year, month, DayOfWeek.Tuesday).Count();
                        break;
                    case "4":
                        tongNgayHoc += DaysInMonth(year, month, DayOfWeek.Wednesday).Count();
                        break;
                    case "5":
                        tongNgayHoc += DaysInMonth(year, month, DayOfWeek.Thursday).Count();
                        break;
                    case "6":
                        tongNgayHoc += DaysInMonth(year, month, DayOfWeek.Friday).Count();
                        break;
                    case "7":
                        tongNgayHoc += DaysInMonth(year, month, DayOfWeek.Saturday).Count();
                        break;
                    default:
                        tongNgayHoc += DaysInMonth(year, month, DayOfWeek.Sunday).Count();
                        break;
                }
            }

            return tongNgayHoc;
        }

        private static IEnumerable<int> DaysInMonth(int year, int month, DayOfWeek dow)
        {
            DateTime monthStart = new DateTime(year, month, 1);
            return Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => monthStart.AddDays(day))
                .Where(date => date.DayOfWeek == dow)
                .Select(date => date.Day);
        }

        public async Task<HocPhiViewModel> UpdateHocPhiAsync(Guid HocPhiId, double Gia, string GhiChu, DateTime NgayApDung, string LoggedEmployee)
        {
            var item = await _context.HocPhis
                                    .Where(x => x.HocPhiId == HocPhiId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Học Phí !!!");

            item.Gia = Gia;
            item.GhiChu = GhiChu;
            item.NgayApDung = NgayApDung;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return new HocPhiViewModel
            {
                GhiChu = item.GhiChu,
                CreatedBy = item.CreatedBy,
                Gia = item.Gia,
                HocPhiId = item.HocPhiId,
                UpdatedBy = item.UpdatedBy,
                CreatedDate = item.CreatedDate != null ? ((DateTime)item.CreatedDate).ToString("dd/MM/yyyy") : "",
                UpdatedDate = item.UpdatedDate != null ? ((DateTime)item.UpdatedDate).ToString("dd/MM/yyyy") : "",
                NgayApDung = item.NgayApDung != null ? ((DateTime)item.NgayApDung).ToString("dd/MM/yyyy") : ""
            };
        }

        public async Task<int> TinhSoNgayDuocChoNghiAsync(Guid LopHocId, int month, int year)
        {
            if (month == 1)
            {
                month = 12;
                year--;
            }
            else
            {
                month--;
            }

            var ngayChoNghi = await _context.LopHoc_DiemDanhs
                                            .Where(x => x.LopHocId == LopHocId && x.IsDuocNghi == true && x.NgayDiemDanh.Month == month && x.NgayDiemDanh.Year == year)
                                            .GroupBy(x => x.NgayDiemDanh)
                                            .Select(m => new
                                            {
                                                m.Key
                                            })
                                            .ToListAsync();
            return ngayChoNghi.Count();
        }

        public async Task<TinhHocPhiViewModel> TinhHocPhiAsync(Guid LopHocId, int month, int year, int KhuyenMai, string GiaSachList)
        {
            int soNgayHoc = await TinhSoNgayHocAsync(LopHocId, month, year);
            int soNgayDuocNghi = await TinhSoNgayDuocChoNghiAsync(LopHocId, month, year);

            var item = await _context.LopHocs
                                    .Include(x => x.HocPhi)
                                    .Where(x => x.LopHocId == LopHocId)
                                    .SingleOrDefaultAsync();

            var hocPhi = item.HocPhi.Gia;

            var hocPhiMoiNgay = hocPhi / soNgayHoc;

            if (soNgayDuocNghi > 0)
                hocPhi = hocPhi - (hocPhiMoiNgay * soNgayDuocNghi);

            if (KhuyenMai > 0)
            {
                hocPhi = hocPhi - ((hocPhi * KhuyenMai) / 100);
            }

            if (!string.IsNullOrWhiteSpace(GiaSachList))
            {
                var giaSach = GiaSachList.Split(',');
                foreach (var el in giaSach)
                {
                    hocPhi += int.Parse(el);
                }
            }

            return new TinhHocPhiViewModel
            {
                SoNgayDuocNghi = soNgayDuocNghi,
                HocPhi = hocPhi,
                SoNgayHoc = soNgayHoc,
                HocVienList = await GetHocVien_No_NgayHocAsync(LopHocId, month, year, hocPhi)
            };
        }

        public async Task<List<HocVienViewModel>> GetHocVien_No_NgayHocAsync(Guid LopHocId, int month, int year, double HocPhi)
        {
            if (month == 1)
            {
                month = 12;
                year--;
            }
            else
            {
                month--;
            }
            return await _context.HocVien_LopHocs
                                    .Include(x => x.HocVien)
                                    .Where(x => x.LopHocId == LopHocId)
                                    .Select(x => new HocVienViewModel
                                    {
                                        FullName = x.HocVien.FullName,
                                        HocVienId = x.HocVienId,
                                        TienNo = x.HocVien.HocVien_Nos.Where(m => m.NgayNo.Month == month && m.NgayNo.Year == year && m.IsDisabled == false).SingleOrDefault() == null ? 0: x.HocVien.HocVien_Nos.Where(m => m.NgayNo.Month == month && m.NgayNo.Year == year).SingleOrDefault().TienNo,
                                        HocPhiMoi = x.HocVien.HocVien_Nos.Where(m => m.NgayNo.Month == month && m.NgayNo.Year == year && m.IsDisabled == false).SingleOrDefault() == null ? HocPhi : HocPhi + x.HocVien.HocVien_Nos.Where(m => m.NgayNo.Month == month && m.NgayNo.Year == year).SingleOrDefault().TienNo
                                    })
                                    .ToListAsync();
        }
    }
}
