
namespace Up.Services
{
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
    using Up.Repositoties;

    public class HocPhiService : IHocPhiService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHocPhiRepository _hocPhiRepository;
        private readonly ILopHocRepository _lopHocRepository;

        public HocPhiService(
            ApplicationDbContext context,
            IHocPhiRepository hocPhiRepository,
            ILopHocRepository lopHocRepository)
        {
            _context = context;
            _hocPhiRepository = hocPhiRepository;
            _lopHocRepository = lopHocRepository;
        }

        public async Task<HocPhiViewModel> CreateHocPhiAsync(CreateHocPhiInputModel input, string loggedEmployee)
        {
            var result = await _hocPhiRepository.CreateHocPhiAsync(input, loggedEmployee);
            return await _hocPhiRepository.GetHocPhiDetailAsync(result);
        }

        public async Task<bool> DeleteHocPhiAsync(Guid id, string loggedEmployee)
        {
            return await _hocPhiRepository.DeleteHocPhiAsync(id, loggedEmployee);
        }

        public async Task<List<HocPhiViewModel>> GetHocPhiAsync()
        {
            return await _hocPhiRepository.GetHocPhiAsync();
        }

        public async Task<HocPhiViewModel> UpdateHocPhiAsync(UpdateHocPhiInputModel input, string loggedEmployee)
        {
            var result = await _hocPhiRepository.UpdateHocPhiAsync(input, loggedEmployee);

            return await _hocPhiRepository.GetHocPhiDetailAsync(result);
        }

        private (int, int) TinhSubMonthSubYear(int month, int year)
        {
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
            return (subMonth, subYear);
        }

        public async Task<TinhHocPhiViewModel> TinhHocPhiAsync(TinhHocPhiInputModel input)
        {
            //int soNgayDuocNghi = await TinhSoNgayDuocChoNghiAsync(LopHocId, month, year);

            var (subMonth, subYear) = TinhSubMonthSubYear(input.Month, input.Year);

            int soNgayHoc = await _lopHocRepository.DemSoNgayHocAsync(input.LopHocId, input.Month, input.Year);
            int soNgayHocCu = await _lopHocRepository.DemSoNgayHocAsync(input.LopHocId, subMonth, subYear);

            var hocPhiMoiNgay = input.HocPhi / soNgayHoc;

            var hocPhiCu = await _context.LopHoc_HocPhis
                .Include(x => x.HocPhi)
                .AsNoTracking().
                FirstOrDefaultAsync(x => x.Thang == subMonth && x.Nam == subYear && x.LopHocId == input.LopHocId);

            var hocPhiMoiNgayCu = hocPhiCu == null ? (input.HocPhi / soNgayHocCu) : (hocPhiCu.HocPhi.Gia / soNgayHocCu);

            return new TinhHocPhiViewModel
            {
                HocPhiMoiNgay = hocPhiMoiNgay,
                //SoNgayDuocNghi = soNgayDuocNghi,
                HocPhi = input.HocPhi,
                SoNgayHoc = soNgayHoc,
                HocVienList = await GetHocVien_No_NgayHocAsync(input.LopHocId, input.Month, input.Year, input.HocPhi, soNgayHoc, hocPhiMoiNgay, hocPhiMoiNgayCu)
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
                                                            TinhNo(x.HocVien.HocVien_Nos.Where(m => m.IsDisabled == false && (m.NgayNo.Year < year ||
                                                                        m.NgayNo.Year == year && m.NgayNo.Month <= month)), LopHocId) :
                                                            /////
                                                            x.HocVien.ThongKe_DoanhThuHocPhis.FirstOrDefault(m => m.LopHocId == LopHocId && m.NgayDong.Month == month && m.NgayDong.Year == year && m.DaNo) != null ?
                                                            x.HocVien.ThongKe_DoanhThuHocPhis.FirstOrDefault(m => m.LopHocId == LopHocId && m.NgayDong.Month == month && m.NgayDong.Year == year && m.DaNo).HocPhi
                                                            +
                                                            (!IsDaDong(x.HocVien.ThongKe_DoanhThuHocPhis.OrderByDescending(m => m.NgayDong), LopHocId, month, year) ?
                                                            x.HocVien.ThongKe_DoanhThuHocPhis.OrderByDescending(m => m.NgayDong).FirstOrDefault(m => m.LopHocId != LopHocId && m.NgayDong.Month <= month && m.NgayDong.Year <= year).HocPhi : 0)
                                                            //////
                                                            :
                                                            0
                                                            + (!IsDaDong(x.HocVien.ThongKe_DoanhThuHocPhis.OrderByDescending(m => m.NgayDong), LopHocId, month, year) ?
                                                            x.HocVien.ThongKe_DoanhThuHocPhis.OrderByDescending(m => m.NgayDong).FirstOrDefault(m => m.LopHocId != LopHocId && m.NgayDong.Month <= month && m.NgayDong.Year <= year).HocPhi : 0)
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
                    if (IsTronGoi(item, LopHocId, currentMonth, currentYear) && !item.TronGoi)
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
                                        soNgayTruSauNghi = await _hocPhiRepository.TinhSoNgayHocVienVoSauAsync(currentYear, currentMonth, _ngayKetThuc, LopHocId);
                                }

                                if (_ngayBatDauHoc.Month == currentMonth && _ngayBatDauHoc.Year == currentYear)
                                {
                                    var soNgayHocVienVaoSau = await _hocPhiRepository.TinhSoNgayHocVienVoSauAsync(currentYear, currentMonth, _ngayBatDauHoc, LopHocId);

                                    item.HocPhiBuHocVienVaoSau = (HocPhiMoiNgay * (SoNgayHoc - soNgayHocVienVaoSau)) + (HocPhiMoiNgay * soNgayTruSauNghi) + (HocPhiMoiNgayCu * ngayDuocNghi * (100 - item.KhuyenMaiThangTruoc) / 100);
                                    item.HocPhiMoiFixed = item.HocPhiMoi - item.HocPhiBuHocVienVaoSau;
                                    item.HocPhiMoi = item.HocPhiMoi - (item.HocPhiMoiFixed * item.KhuyenMai / 100) + giaSach + item.Bonus - item.Minus + item.TienNo - item.HocPhiBuHocVienVaoSau;
                                }
                                else
                                {
                                    item.HocPhiBuHocVienVaoSau = (HocPhiMoiNgay * soNgayTruSauNghi) + (HocPhiMoiNgayCu * ngayDuocNghi * (100 - item.KhuyenMaiThangTruoc) / 100);
                                    item.HocPhiMoiFixed = item.HocPhiMoi - item.HocPhiBuHocVienVaoSau;
                                    item.HocPhiMoi = item.HocPhiMoi - (item.HocPhiMoiFixed * item.KhuyenMai / 100) + giaSach + item.Bonus - item.Minus + item.TienNo - item.HocPhiBuHocVienVaoSau;
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
                        if (item.TronGoi)
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
                                    soNgayTruSauNghi = await _hocPhiRepository.TinhSoNgayHocVienVoSauAsync(currentYear, currentMonth, _ngayKetThuc, LopHocId);
                            }


                            if (_ngayBatDauHoc.Month == currentMonth && _ngayBatDauHoc.Year == currentYear)
                            {
                                var soNgayHocVienVaoSau = await _hocPhiRepository.TinhSoNgayHocVienVoSauAsync(currentYear, currentMonth, _ngayBatDauHoc, LopHocId);

                                item.HocPhiBuHocVienVaoSau = (HocPhiMoiNgay * (SoNgayHoc - soNgayHocVienVaoSau)) + (HocPhiMoiNgay * soNgayTruSauNghi) + (HocPhiMoiNgayCu * ngayDuocNghi * (100 - item.KhuyenMaiThangTruoc) / 100);
                                item.HocPhiMoiFixed = item.HocPhiMoi - item.HocPhiBuHocVienVaoSau;
                                item.HocPhiMoi = item.HocPhiMoi - (item.HocPhiMoiFixed * item.KhuyenMai / 100) + giaSach + item.Bonus - item.Minus + item.TienNo - item.HocPhiBuHocVienVaoSau - item.HocPhiTruTronGoi;
                            }
                            else
                            {
                                item.HocPhiBuHocVienVaoSau = (HocPhiMoiNgay * soNgayTruSauNghi) + (HocPhiMoiNgayCu * ngayDuocNghi * (100 - item.KhuyenMaiThangTruoc) / 100);
                                item.HocPhiMoiFixed = item.HocPhiMoi - item.HocPhiBuHocVienVaoSau;
                                item.HocPhiMoi = item.HocPhiMoi - (item.HocPhiMoiFixed * item.KhuyenMai / 100) + giaSach + item.Bonus - item.Minus + item.TienNo - item.HocPhiBuHocVienVaoSau - item.HocPhiTruTronGoi;
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
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private bool IsDaDong(IOrderedEnumerable<ThongKe_DoanhThuHocPhi> hocPhis, Guid lopHocId, int month, int year)
        {
            var daNo = hocPhis.FirstOrDefault(m => m.LopHocId != lopHocId && m.NgayDong.Month <= month && m.NgayDong.Year <= year && m.DaNo);
            var daDong = hocPhis.FirstOrDefault(m => m.NgayDong.Month <= month && m.NgayDong.Year <= year && !(!m.DaDong && !m.DaNo));

            if (daNo == null)
                return true;

            if (daNo != null && daDong == null)
                return false;

            return daDong.NgayDong > daNo.NgayDong;
        }

        private bool IsTronGoi(HocVienViewModel hocVien, Guid LopHocId, int month, int year)
        {
            var item = _context.HocPhiTronGois
                .Where(x => x.HocVienId == hocVien.HocVienId && !x.IsDisabled && !x.IsRemoved)
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
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Monday));
                        break;
                    case "3":
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Tuesday));
                        break;
                    case "4":
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Wednesday));
                        break;
                    case "5":
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Thursday));
                        break;
                    case "6":
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Friday));
                        break;
                    case "7":
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Saturday));
                        break;
                    default:
                        tongNgayHoc.AddRange(Helpers.DaysInMonth(year, month, DayOfWeek.Sunday));
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

            if (ngayHocPhi.FromDate.Year == year && ngayHocPhi.FromDate.Month == month)
            {
                foreach (var ngay in tongNgayHoc)
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

            foreach (Guid item in lopList)
            {
                var no = noList.Where(x => x.LopHocId == item).OrderByDescending(m => m.NgayNo).FirstOrDefault();
                tongNoKhacLop += no != null ? no.TienNo : 0;
            }

            var cungLop = noList.Where(x => x.LopHocId == lopHocId).OrderByDescending(m => m.NgayNo).FirstOrDefault();

            return tongNoKhacLop + (cungLop != null ? cungLop.TienNo : 0);
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _hocPhiRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_HocPhi);
            return canContribute;
        }

        public async Task<bool> CanContributeTinhHocPhiAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _hocPhiRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_TinhHocPhi);
            return canContribute;
        }
    }
}
