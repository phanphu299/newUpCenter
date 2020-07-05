
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

    public class HocPhiService : IHocPhiService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HocPhiService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                CreatedDate = hocPhi.CreatedDate.ToString("dd/MM/yyyy"),
            };
        }

        public async Task<bool> DeleteHocPhiAsync(Guid HocPhiId, string LoggedEmployee)
        {
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
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : "",
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> TinhSoNgayHocAsync(Guid LopHocId, int month, int year)
        {
            var item = await _context.LopHocs
                                    .Include(x => x.NgayHoc)
                                    .Where(x => x.LopHocId == LopHocId)
                                    .AsNoTracking()
                                    .SingleOrDefaultAsync();

            var ngayHoc = item.NgayHoc.Name.Split('-');
            List<int> tongNgayHoc = new List<int>();

            foreach (string el in ngayHoc)
            {
                switch (el.Trim())
                {
                    case "2":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Monday));
                        break;
                    case "3":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Tuesday));
                        break;
                    case "4":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Wednesday));
                        break;
                    case "5":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Thursday));
                        break;
                    case "6":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Friday));
                        break;
                    case "7":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Saturday));
                        break;
                    default:
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Sunday));
                        break;
                }
            }

            return tongNgayHoc.Count;
        }

        private static IEnumerable<int> DaysInMonth(int year, int month, DayOfWeek dow)
        {
            DateTime monthStart = new DateTime(year, month, 1);
            return Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => monthStart.AddDays(day))
                .Where(date => date.DayOfWeek == dow)
                .Select(date => date.Day);
        }

        private static IEnumerable<int> DaysInMonthWithStartDate(int year, int month, DayOfWeek dow, DateTime StartDate)
        {
            DateTime monthStart = new DateTime(year, month, 1);
            return Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => monthStart.AddDays(day))
                .Where(date => date.DayOfWeek == dow && date.Day >= StartDate.Day)
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
                NgayApDung = item.NgayApDung != null ? ((DateTime)item.NgayApDung).ToString("dd/MM/yyyy") : "",
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
                                            .AsNoTracking()
                                            .ToListAsync();
            return ngayChoNghi.Count();
        }

        private async Task<int> TinhSoNgayHocVienVoSauAsync(int year, int month, DateTime NgayBatDau, Guid LopHocId)
        {
            var item = await _context.LopHocs
                                    .Include(x => x.NgayHoc)
                                    .Where(x => x.LopHocId == LopHocId)
                                    .AsNoTracking()
                                    .SingleOrDefaultAsync();

            var ngayHoc = item.NgayHoc.Name.Split('-');
            int tongNgayHoc = 0;

            foreach (string el in ngayHoc)
            {
                switch (el.Trim())
                {
                    case "2":
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Monday, NgayBatDau).Count();
                        break;
                    case "3":
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Tuesday, NgayBatDau).Count();
                        break;
                    case "4":
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Wednesday, NgayBatDau).Count();
                        break;
                    case "5":
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Thursday, NgayBatDau).Count();
                        break;
                    case "6":
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Friday, NgayBatDau).Count();
                        break;
                    case "7":
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Saturday, NgayBatDau).Count();
                        break;
                    default:
                        tongNgayHoc += DaysInMonthWithStartDate(year, month, DayOfWeek.Sunday, NgayBatDau).Count();
                        break;
                }
            }

            return tongNgayHoc;
        }

        public async Task<TinhHocPhiViewModel> TinhHocPhiAsync(Guid LopHocId, int month, int year, double HocPhi)
        {
            //int soNgayDuocNghi = await TinhSoNgayDuocChoNghiAsync(LopHocId, month, year);
            
            int subMonth = month;
            int subYear = year;
            if (subMonth == 1)
            {
                subMonth = 12;
                subYear--;
            }
            else
            {
                subMonth--;
            }
            int soNgayHoc = await TinhSoNgayHocAsync(LopHocId, month, year);
            int soNgayHocCu = await TinhSoNgayHocAsync(LopHocId, subMonth, subYear);

            var hocPhiMoiNgay = HocPhi / soNgayHoc;

            var hocPhiCu = await _context.LopHoc_HocPhis
                .Include(x => x.HocPhi)
                .AsNoTracking().
                FirstOrDefaultAsync(x => x.Thang == subMonth && x.Nam == subYear && x.LopHocId == LopHocId);

            var hocPhiMoiNgayCu = hocPhiCu == null ? (HocPhi / soNgayHocCu) : (hocPhiCu.HocPhi.Gia / soNgayHocCu);

            return new TinhHocPhiViewModel
            {
                HocPhiMoiNgay = hocPhiMoiNgay,
                //SoNgayDuocNghi = soNgayDuocNghi,
                HocPhi = HocPhi,
                SoNgayHoc = soNgayHoc,
                HocVienList = await GetHocVien_No_NgayHocAsync(LopHocId, month, year, HocPhi, soNgayHoc, hocPhiMoiNgay, hocPhiMoiNgayCu)
            };
        }

        public async Task<List<HocVienViewModel>> GetHocVien_No_NgayHocAsync(Guid LopHocId, int month, int year, double HocPhi, int SoNgayHoc, double HocPhiMoiNgay, double HocPhiMoiNgayCu)
        {
            try
            {
                int currentMonth = month;
                int currentYear = year;
                if (month == 1)
                {
                    month = 12;
                    year--;
                }
                else
                {
                    month--;
                }

                //var ngayChoNghi = await _context.LopHoc_DiemDanhs
                //                                .Where(x => x.LopHocId == LopHocId && x.IsDuocNghi == true && x.NgayDiemDanh.Month == month && x.NgayDiemDanh.Year == year)
                //                                .GroupBy(x => x.NgayDiemDanh)
                //                                .Select(m => new
                //                                {
                //                                    m.Key
                //                                })
                //                                .ToListAsync();

                var model = await _context.HocVien_LopHocs
                                        .Include(x => x.LopHoc)
                                        .Include(x => x.HocVien.HocVien_NgayHocs)
                                        .Where(x => x.LopHocId == LopHocId && x.HocVien.IsDisabled == false)
                                        .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayKetThuc == null || (m.NgayKetThuc.Value.Month >= currentMonth && m.NgayKetThuc.Value.Year == currentYear) || m.NgayKetThuc.Value.Year > currentYear)))
                                        .Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && (m.NgayBatDau.Month <= currentMonth && m.NgayBatDau.Year == currentYear) || m.NgayBatDau.Year < currentYear))

                                        //.Where(x => x.HocVien.HocVien_NgayHocs.Any(m => m.LopHocId == LopHocId && ((m.NgayBatDau.Month <= currentMonth && m.NgayBatDau.Year == currentYear) || m.NgayBatDau.Year < currentYear) && (m.NgayKetThuc == null || (m.NgayKetThuc.Value.Month >= currentMonth && m.NgayKetThuc.Value.Year >= currentYear))))
                                        .Select(x => new HocVienViewModel
                                        {
                                            SoNgayDuocNghi = x.HocVien.LopHoc_DiemDanhs
                                                            .Where(m => m.LopHocId == LopHocId && m.IsDuocNghi == true && m.NgayDiemDanh.Month == month && m.NgayDiemDanh.Year == year)
                                                            .Select(m => m.NgayDiemDanh),
                                            NgayBatDau_Date = x.HocVien.HocVien_NgayHocs
                                                            .Where(m => m.HocVienId == x.HocVienId && m.LopHocId == LopHocId)
                                                            .FirstOrDefault().NgayBatDau,
                                            NgayKetThuc_Date = x.HocVien.HocVien_NgayHocs
                                                            .Where(m => m.HocVienId == x.HocVienId && m.LopHocId == LopHocId)
                                                            .FirstOrDefault().NgayKetThuc.Value,
                                            FullName = x.HocVien.FullName,
                                            HocVienId = x.HocVienId,
                                            NgayBatDauHoc = x.HocVien.HocVien_NgayHocs
                                                            .Where(m => m.HocVienId == x.HocVienId && m.LopHocId == LopHocId)
                                                            .FirstOrDefault().NgayBatDau.ToString("dd/MM/yyyy"),
                                            NgayKetThuc = x.HocVien.HocVien_NgayHocs
                                                            .Where(m => m.HocVienId == x.HocVienId && m.LopHocId == LopHocId)
                                                            .FirstOrDefault().NgayKetThuc == null ?
                                                            "" :
                                                            x.HocVien.HocVien_NgayHocs
                                                            .Where(m => m.HocVienId == x.HocVienId && m.LopHocId == LopHocId)
                                                            .FirstOrDefault().NgayKetThuc.Value.ToString("dd/MM/yyyy"),
                                            TienNo = x.HocVien.HocVien_Nos
                                                            .Where(m => m.IsDisabled == false && m.NgayNo.Month <= month && m.NgayNo.Year <= year)
                                                            .Any() ?
                                                            TinhNo(x.HocVien.HocVien_Nos.Where(m => m.IsDisabled == false && m.NgayNo.Month <= month && m.NgayNo.Year <= year), LopHocId) :
                                                            x.HocVien.ThongKe_DoanhThuHocPhis.FirstOrDefault(m => m.LopHocId == LopHocId && m.NgayDong.Month == month && m.NgayDong.Year == year && m.DaNo) != null ?
                                                            x.HocVien.ThongKe_DoanhThuHocPhis.FirstOrDefault(m => m.LopHocId == LopHocId && m.NgayDong.Month == month && m.NgayDong.Year == year && m.DaNo).HocPhi 
                                                            + (x.HocVien.ThongKe_DoanhThuHocPhis.OrderByDescending(m => m.NgayDong).FirstOrDefault(m => m.LopHocId != LopHocId && m.NgayDong.Month == month && m.NgayDong.Year == year).DaNo == true ? x.HocVien.ThongKe_DoanhThuHocPhis.OrderByDescending(m => m.NgayDong).FirstOrDefault(m => m.LopHocId != LopHocId && m.NgayDong.Month == month && m.NgayDong.Year == year).HocPhi : 0)
                                                            :
                                                            0
                                                                                                                        + (x.HocVien.ThongKe_DoanhThuHocPhis.OrderByDescending(m => m.NgayDong).FirstOrDefault(m => m.LopHocId != LopHocId && m.NgayDong.Month == month && m.NgayDong.Year == year).DaNo == true ? x.HocVien.ThongKe_DoanhThuHocPhis.OrderByDescending(m => m.NgayDong).FirstOrDefault(m => m.LopHocId != LopHocId && m.NgayDong.Month == month && m.NgayDong.Year == year).HocPhi : 0)
,
                                            //HocPhiMoi = (Math.Ceiling(HocPhi / 10000) * 10000),
                                            HocPhiMoi = HocPhi,
                                            DaSaveNhap = x.HocVien.ThongKe_DoanhThuHocPhis.Any(m => m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear && m.LopHocId == LopHocId),
                                            Bonus = x.HocVien.ThongKe_DoanhThuHocPhis.Any(m => m.LopHocId == LopHocId && m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear) ?
                                                            x.LopHoc.ThongKe_DoanhThuHocPhis.FirstOrDefault(m => m.LopHocId == LopHocId && m.HocVienId == x.HocVienId && m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear).Bonus
                                                            : 0,
                                            Minus = x.HocVien.ThongKe_DoanhThuHocPhis.Any(m => m.LopHocId == LopHocId && m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear) ?
                                                            x.LopHoc.ThongKe_DoanhThuHocPhis.FirstOrDefault(m => m.LopHocId == LopHocId && m.HocVienId == x.HocVienId && m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear).Minus
                                                            : 0,
                                            KhuyenMai = x.HocVien.ThongKe_DoanhThuHocPhis.Any(m => m.LopHocId == LopHocId && m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear) ?
                                                            x.LopHoc.ThongKe_DoanhThuHocPhis.FirstOrDefault(m => m.LopHocId == LopHocId && m.HocVienId == x.HocVienId && m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear).KhuyenMai
                                                            : 0,
                                            GhiChu = x.HocVien.ThongKe_DoanhThuHocPhis.Any(m => m.LopHocId == LopHocId && m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear) ?
                                                            x.LopHoc.ThongKe_DoanhThuHocPhis.FirstOrDefault(m => m.LopHocId == LopHocId && m.HocVienId == x.HocVienId && m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear).GhiChu
                                                            : "",
                                            //HocPhiFixed = (Math.Ceiling(HocPhi / 10000) * 10000),
                                            HocPhiFixed = HocPhi,

                                            GiaSach = x.HocVien.ThongKe_DoanhThuHocPhis.FirstOrDefault(m => m.LopHocId == LopHocId && m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear) == null ?
                                                        null :
                                                        x.HocVien.ThongKe_DoanhThuHocPhis.FirstOrDefault(m => m.LopHocId == LopHocId && m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear).ThongKe_DoanhThuHocPhi_TaiLieus.Any() ?
                                                        x.HocVien.ThongKe_DoanhThuHocPhis
                                                        .Where(m => m.LopHocId == LopHocId && m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear)
                                                        .SelectMany(m => m.ThongKe_DoanhThuHocPhi_TaiLieus)
                                                        .Select(t => new SachViewModel { Gia = t.Sach.Gia, SachId = t.SachId, Name = t.Sach.Name }).ToArray()
                                                        : null,
                                            DaDongHocPhi = x.HocVien.ThongKe_DoanhThuHocPhis.Any(m => m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear && m.LopHocId == LopHocId && m.DaDong == true),
                                            TronGoi = x.HocVien.ThongKe_DoanhThuHocPhis.Any(m => m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear && m.LopHocId == LopHocId && m.TronGoi == true),
                                            DaNo = x.HocVien.ThongKe_DoanhThuHocPhis.FirstOrDefault(m => m.LopHocId == LopHocId && m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear) != null ? x.HocVien.ThongKe_DoanhThuHocPhis.FirstOrDefault(m => m.LopHocId == LopHocId && m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear).DaNo : x.HocVien.HocVien_Nos.Any(m => m.NgayNo.Month == currentMonth && m.NgayNo.Year == currentYear && m.LopHocId == LopHocId && m.IsDisabled == false),
                                            KhuyenMaiThangTruoc = x.HocVien.ThongKe_DoanhThuHocPhis.Any(m => m.LopHocId == LopHocId && m.NgayDong.Month == month && m.NgayDong.Year == year) ?
                                                            x.LopHoc.ThongKe_DoanhThuHocPhis.FirstOrDefault(m => m.LopHocId == LopHocId && m.HocVienId == x.HocVienId && m.NgayDong.Month == month && m.NgayDong.Year == year).KhuyenMai
                                                            : 0,
                                        })
                                        .Where(x => x.NgayKetThuc_Date == null || (x.NgayKetThuc_Date.Value.Month >= currentMonth && x.NgayKetThuc_Date.Value.Year == currentYear) || x.NgayKetThuc_Date.Value.Year > currentYear)
                                        .Where(x => (x.NgayBatDau_Date.Month <= currentMonth && x.NgayBatDau_Date.Year == currentYear) || x.NgayBatDau_Date.Year < currentYear)
                                        .AsNoTracking()
                                        .ToListAsync();
                int index = 1;
                foreach (var item in model)
                {
                    if(IsTronGoi(item, LopHocId, currentMonth, currentYear) && !item.TronGoi)
                    {
                        var tinhSoNgayHoc = TinhSoNgayHocTronGoi(item, LopHocId, currentMonth, currentYear);
                        if (tinhSoNgayHoc == 0)
                        {
                            item.HocPhiMoi = 0;
                            item.SoNgayHoc = 0;
                            item.Bonus = 0;
                            item.LastBonus = 0;
                            item.TienNo = 0;
                            item.HocPhiFixed = 0;
                            item.KhuyenMai = 0;
                            item.KhuyenMaiThangTruoc = 0;
                            item.LastGiaSach = null;
                            item.GiaSach = null;
                            item.Minus = 0;
                            item.LastMinus = 0;
                            item.GhiChu = "Trọn gói";
                            item.TronGoi = true;
                        }
                        else
                        {
                            item.TronGoi = true;
                            item.GhiChu = "Trọn gói";
                            item.HocPhiMoi = tinhSoNgayHoc * HocPhiMoiNgay;

                            var giaSach = item.GiaSach != null ? item.GiaSach.Select(x => x.Gia).Sum() : 0;
                            item.HocPhiTruTronGoi = (item.HocPhiFixed - (tinhSoNgayHoc * HocPhiMoiNgay));

                            if (!string.IsNullOrWhiteSpace(item.NgayBatDauHoc))
                            {
                                int ngayDuocNghi = 0;
                                DateTime _ngayBatDauHoc = new DateTime(int.Parse(item.NgayBatDauHoc.Substring(6)), int.Parse(item.NgayBatDauHoc.Substring(3, 2)), int.Parse(item.NgayBatDauHoc.Substring(0, 2)));
                                foreach (var ngayNghi in item.SoNgayDuocNghi)
                                {
                                    if (ngayNghi >= _ngayBatDauHoc)
                                    {
                                        ngayDuocNghi++;
                                    }
                                }

                                var soNgayTruSauNghi = 0;
                                if (!string.IsNullOrWhiteSpace(item.NgayKetThuc))
                                {
                                    DateTime _ngayKetThuc = new DateTime(int.Parse(item.NgayKetThuc.Substring(6)), int.Parse(item.NgayKetThuc.Substring(3, 2)), int.Parse(item.NgayKetThuc.Substring(0, 2)));
                                    if (_ngayKetThuc.Month == currentMonth && _ngayKetThuc.Year == currentYear)
                                        soNgayTruSauNghi = await TinhSoNgayHocVienVoSauAsync(currentYear, currentMonth, _ngayKetThuc, LopHocId);
                                }

                                if (_ngayBatDauHoc.Month == currentMonth && _ngayBatDauHoc.Year == currentYear)
                                {
                                    var soNgayHocVienVaoSau = await TinhSoNgayHocVienVoSauAsync(currentYear, currentMonth, _ngayBatDauHoc, LopHocId);

                                    item.HocPhiBuHocVienVaoSau = (HocPhiMoiNgay * (SoNgayHoc - soNgayHocVienVaoSau)) + (HocPhiMoiNgay * soNgayTruSauNghi) + (HocPhiMoiNgayCu * ngayDuocNghi * (100 - item.KhuyenMaiThangTruoc) / 100);
                                    item.HocPhiMoi = item.HocPhiMoi - (item.HocPhiFixed * item.KhuyenMai / 100) + giaSach + item.Bonus - item.Minus + item.TienNo - item.HocPhiBuHocVienVaoSau;
                                }
                                else
                                {
                                    item.HocPhiBuHocVienVaoSau = (HocPhiMoiNgay * soNgayTruSauNghi) + (HocPhiMoiNgayCu * ngayDuocNghi * (100 - item.KhuyenMaiThangTruoc) / 100);
                                    item.HocPhiMoi = item.HocPhiMoi - (item.HocPhiFixed * item.KhuyenMai / 100) + giaSach + item.Bonus - item.Minus + item.TienNo - item.HocPhiBuHocVienVaoSau;
                                }
                            }
                            else
                            {
                                item.HocPhiMoi = item.HocPhiMoi - (item.HocPhiFixed * item.KhuyenMai / 100) + giaSach + item.Bonus - item.Minus + item.TienNo;
                            }
                            item.LastBonus = item.Bonus;
                            item.LastMinus = item.Minus;
                            item.LastGiaSach = item.GiaSach;
                        }    
                    }   
                    else
                    {
                        if(item.TronGoi)
                            item.HocPhiTruTronGoi = (item.HocPhiFixed - (TinhSoNgayHocTronGoi(item, LopHocId, currentMonth, currentYear) * HocPhiMoiNgay));

                        var giaSach = item.GiaSach != null ? item.GiaSach.Select(x => x.Gia).Sum() : 0;
                        if (!string.IsNullOrWhiteSpace(item.NgayBatDauHoc))
                        {
                            int ngayDuocNghi = 0;
                            DateTime _ngayBatDauHoc = new DateTime(int.Parse(item.NgayBatDauHoc.Substring(6)), int.Parse(item.NgayBatDauHoc.Substring(3, 2)), int.Parse(item.NgayBatDauHoc.Substring(0, 2)));
                            foreach (var ngayNghi in item.SoNgayDuocNghi)
                            {
                                if (ngayNghi >= _ngayBatDauHoc)
                                {
                                    ngayDuocNghi++;
                                }
                            }

                            var soNgayTruSauNghi = 0;
                            if (!string.IsNullOrWhiteSpace(item.NgayKetThuc))
                            {
                                DateTime _ngayKetThuc = new DateTime(int.Parse(item.NgayKetThuc.Substring(6)), int.Parse(item.NgayKetThuc.Substring(3, 2)), int.Parse(item.NgayKetThuc.Substring(0, 2)));
                                if (_ngayKetThuc.Month == currentMonth && _ngayKetThuc.Year == currentYear)
                                    soNgayTruSauNghi = await TinhSoNgayHocVienVoSauAsync(currentYear, currentMonth, _ngayKetThuc, LopHocId);
                            }


                            if (_ngayBatDauHoc.Month == currentMonth && _ngayBatDauHoc.Year == currentYear)
                            {
                                var soNgayHocVienVaoSau = await TinhSoNgayHocVienVoSauAsync(currentYear, currentMonth, _ngayBatDauHoc, LopHocId);

                                item.HocPhiBuHocVienVaoSau = (HocPhiMoiNgay * (SoNgayHoc - soNgayHocVienVaoSau)) + (HocPhiMoiNgay * soNgayTruSauNghi) + (HocPhiMoiNgayCu * ngayDuocNghi * (100 - item.KhuyenMaiThangTruoc) / 100);
                                item.HocPhiMoi = item.HocPhiMoi - (item.HocPhiFixed * item.KhuyenMai / 100) + giaSach + item.Bonus - item.Minus + item.TienNo - item.HocPhiBuHocVienVaoSau - item.HocPhiTruTronGoi;
                            }
                            else
                            {
                                item.HocPhiBuHocVienVaoSau = (HocPhiMoiNgay * soNgayTruSauNghi) + (HocPhiMoiNgayCu * ngayDuocNghi * (100 - item.KhuyenMaiThangTruoc) / 100);
                                item.HocPhiMoi = item.HocPhiMoi - (item.HocPhiFixed * item.KhuyenMai / 100) + giaSach + item.Bonus - item.Minus + item.TienNo - item.HocPhiBuHocVienVaoSau - item.HocPhiTruTronGoi;
                            }
                        }
                        else
                        {
                            item.HocPhiMoi = item.HocPhiMoi - (item.HocPhiFixed * item.KhuyenMai / 100) + giaSach + item.Bonus - item.Minus + item.TienNo - item.HocPhiTruTronGoi;
                        }
                        item.LastBonus = item.Bonus;
                        item.LastMinus = item.Minus;
                        item.LastGiaSach = item.GiaSach;
                    }    

                    item.Stt = index;
                    
                    index++;
                }
                return model.OrderBy(x => x.Stt).ToList();
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private bool IsTronGoi(HocVienViewModel hocVien, Guid LopHocId, int month, int year)
        {
            var item = _context.HocPhiTronGois
                .Where(x => x.HocVienId == hocVien.HocVienId && x.IsDisabled == false && x.IsRemoved == false)
                .SelectMany(x => x.HocPhiTronGoi_LopHocs)
                .Where(x => x.LopHocId == LopHocId && (year < x.ToDate.Year || (year == x.ToDate.Year && month <= x.ToDate.Month)))
                .FirstOrDefault();

            return item != null;
        }

        private int TinhSoNgayHocTronGoi(HocVienViewModel hocVien, Guid LopHocId, int month, int year)
        {
            var item = _context.LopHocs
                                    .Include(x => x.NgayHoc)
                                    .Where(x => x.LopHocId == LopHocId)
                                    .AsNoTracking()
                                    .SingleOrDefault();

            var ngayHoc = item.NgayHoc.Name.Split('-');
            List<int> tongNgayHoc = new List<int>();

            foreach (string el in ngayHoc)
            {
                switch (el.Trim())
                {
                    case "2":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Monday));
                        break;
                    case "3":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Tuesday));
                        break;
                    case "4":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Wednesday));
                        break;
                    case "5":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Thursday));
                        break;
                    case "6":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Friday));
                        break;
                    case "7":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Saturday));
                        break;
                    default:
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Sunday));
                        break;
                }
            }

            var ngayHocPhi = _context.HocPhiTronGois
                .Where(x => x.HocVienId == hocVien.HocVienId)
                .SelectMany(x => x.HocPhiTronGoi_LopHocs)
                .FirstOrDefault(x => x.LopHocId == LopHocId);

            int soNgayTinhHocPhi = 0;

            if (ngayHocPhi.FromDate.Year == year && ngayHocPhi.FromDate.Month < month && (ngayHocPhi.ToDate.Year > year || ngayHocPhi.ToDate.Year == year && ngayHocPhi.ToDate.Month > month))
                return 0;

            if(ngayHocPhi.FromDate.Year == year && ngayHocPhi.FromDate.Month == month)
            {
                foreach(var ngay in tongNgayHoc)
                {
                    if (ngay < ngayHocPhi.FromDate.Day)
                        soNgayTinhHocPhi++;
                }    
            }

            if (ngayHocPhi.ToDate.Year == year && ngayHocPhi.ToDate.Month == month)
            {
                foreach (var ngay in tongNgayHoc)
                {
                    if (ngay > ngayHocPhi.ToDate.Day)
                        soNgayTinhHocPhi++;
                }
            }

            return soNgayTinhHocPhi;
        }

        private double TinhNo(IEnumerable<HocVien_No> noList, Guid lopHocId)
        {
            double tongNoKhacLop = 0;
            var lopList = noList.Where(x => x.LopHocId != lopHocId).Select(x => x.LopHocId).Distinct();

            foreach(Guid item in lopList)
            {
                var no = noList.Where(x => x.LopHocId == item).OrderByDescending(m => m.NgayNo).FirstOrDefault();
                tongNoKhacLop += no != null ? no.TienNo : 0;
            }

            var cungLop = noList.Where(x => x.LopHocId == lopHocId).OrderByDescending(m => m.NgayNo).FirstOrDefault();

            return tongNoKhacLop + (cungLop != null ? cungLop.TienNo : 0);
        }

        public async Task<List<int>> SoNgayHocAsync(Guid LopHocId, int month, int year)
        {
            var item = await _context.LopHocs
                                    .Include(x => x.NgayHoc)
                                    .Where(x => x.LopHocId == LopHocId)
                                    .AsNoTracking()
                                    .SingleOrDefaultAsync();

            var ngayHoc = item.NgayHoc.Name.Split('-');
            List<int> tongNgayHoc = new List<int>();

            foreach (string el in ngayHoc)
            {
                switch (el.Trim())
                {
                    case "2":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Monday));
                        break;
                    case "3":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Tuesday));
                        break;
                    case "4":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Wednesday));
                        break;
                    case "5":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Thursday));
                        break;
                    case "6":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Friday));
                        break;
                    case "7":
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Saturday));
                        break;
                    default:
                        tongNgayHoc.AddRange(DaysInMonth(year, month, DayOfWeek.Sunday));
                        break;
                }
            }

            return tongNgayHoc.OrderBy(x => x).ToList();
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = await _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_HocPhi)
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

        public async Task<bool> CanContributeTinhHocPhiAsync(ClaimsPrincipal User)
        {
            var CurUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(CurUser);

            var quyen_roles = await _context.Quyen_Roles
                .Where(x => x.QuyenId == (int)QuyenEnums.Contribute_TinhHocPhi)
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
    }
}
