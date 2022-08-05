
namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Data.Entities;
    using Up.Enums;
    using Up.Extensions;
    using Up.Models;
    using Up.Repositoties;

    public class HocPhiService : IHocPhiService
    {
        private readonly IHocPhiRepository _hocPhiRepository;
        private readonly ILopHocRepository _lopHocRepository;
        private readonly IHocPhiTronGoiRepository _hocPhiTronGoiRepository;

        public HocPhiService(IHocPhiRepository hocPhiRepository, ILopHocRepository lopHocRepository, IHocPhiTronGoiRepository hocPhiTronGoiRepository)
        {
            _hocPhiRepository = hocPhiRepository;
            _lopHocRepository = lopHocRepository;
            _hocPhiTronGoiRepository = hocPhiTronGoiRepository;
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

        public async Task<TinhHocPhiViewModel> TinhHocPhiAsync(TinhHocPhiInputModel input)
        {
            var (preMonth, preYear) = Helpers.TinhSubMonthSubYear(input.Month, input.Year);

            int soNgayHoc = await _lopHocRepository.DemSoNgayHocAsync(input.LopHocId, input.Month, input.Year);
            int soNgayHocCu = await _lopHocRepository.DemSoNgayHocAsync(input.LopHocId, preMonth, preYear);
            var hocPhiCu = (await _hocPhiRepository.GetHocPhiCuAsync(input.LopHocId, preMonth, preYear)) ?? 0;

            return new TinhHocPhiViewModel
            {
                HocPhi = input.HocPhi,
                SoNgayHoc = soNgayHoc,
                HocVienList = await GetHocVien_No_NgayHocAsync(
                    input.LopHocId,
                    input.Month,
                    input.Year,
                    input.HocPhi,
                    soNgayHoc,
                    hocPhiCu,
                    soNgayHocCu)
            };
        }

        private async Task<List<HocVienViewModel>> GetHocVien_No_NgayHocAsync(
            Guid lopHocId,
            int month,
            int year, 
            double hocPhiCurrent,
            int soNgayHocCurrent,
            double preHocPhi,
            int preSoNgayHoc)
        {
            int currentMonth = month;
            int currentYear = year;
            (month, year) = Helpers.TinhSubMonthSubYear(currentMonth, currentYear);

            var model = _hocPhiRepository.GetHocVien_LopHocsEntity(lopHocId, currentMonth, currentYear);

            int index = 1;
            var items = (await model.ToListAsync())
                .Select(hocVien =>
                {
                    var soNgayDuocNghi = hocVien.HocVien.LopHoc_DiemDanhs
                                    .Where(m => m.LopHocId == lopHocId && m.IsDuocNghi == true && m.NgayDiemDanh.Month == month && m.NgayDiemDanh.Year == year)
                                    .Select(m => m.NgayDiemDanh);

                    var ngayHocGeneric = hocVien.HocVien.HocVien_NgayHocs
                                    .FirstOrDefault(m => m.HocVienId == hocVien.HocVienId && m.LopHocId == lopHocId);

                    var ngayBatDau_Date = ngayHocGeneric?.NgayBatDau;
                    var ngayKetThuc_Date = ngayHocGeneric?.NgayKetThuc;
                    var ngayBatDauHoc = ngayHocGeneric?.NgayBatDau.ToClearDate();
                    var ngayKetThuc = ngayHocGeneric?.NgayKetThuc?.ToClearDate() ?? string.Empty;

                    // tinh no
                    var noCu = hocVien.HocVien.HocVien_Nos
                        .Where(m => !m.IsDisabled &&
                                    (m.NgayNo.Year < year ||
                                    (m.NgayNo.Year == year && m.NgayNo.Month <= month)));

                    double tienNo = 0;
                    if (noCu.Any())
                    {
                        tienNo = TinhNo(noCu, lopHocId);
                    }
                    else
                    {
                        var noCuThangTruoc = hocVien.HocVien.ThongKe_DoanhThuHocPhis
                                                            .FirstOrDefault(m => m.LopHocId == lopHocId && m.NgayDong.Month == month && m.NgayDong.Year == year && m.DaNo)
                                                            ?.HocPhi ?? 0;

                        bool daDongNoLopKhac = IsDaDongNoLopKhac(hocVien.HocVien.ThongKe_DoanhThuHocPhis.OrderByDescending(m => m.NgayDong), lopHocId, month, year);
                        var noLopKhac = daDongNoLopKhac ?
                                        0
                                        :
                                        hocVien.HocVien.ThongKe_DoanhThuHocPhis.OrderByDescending(m => m.NgayDong)
                                        .FirstOrDefault(m => m.LopHocId != lopHocId &&
                                                            (m.NgayDong.Year < year ||
                                                            (m.NgayDong.Year == year && m.NgayDong.Month <= month)))
                                        ?.HocPhi ?? 0;
                        tienNo = noCuThangTruoc + noLopKhac;
                    }

                    var daInput = hocVien.HocVien.ThongKe_DoanhThuHocPhis.Where(m => m.NgayDong.Month == currentMonth && m.NgayDong.Year == currentYear && m.LopHocId == lopHocId);
                    bool daSaveNhap = daInput.Any();
                    var bonus = daSaveNhap ? daInput.FirstOrDefault().Bonus : 0;
                    var minus = daSaveNhap ? daInput.FirstOrDefault().Minus : 0;
                    var khuyenMai = daSaveNhap ? daInput.FirstOrDefault().KhuyenMai : 0;
                    var ghiChu = daSaveNhap ? daInput.FirstOrDefault().GhiChu : string.Empty;

                    //tinh gia sach
                    SachViewModel[] giaSach = null;
                    if (daInput.Any())
                    {
                        if (daInput.FirstOrDefault().ThongKe_DoanhThuHocPhi_TaiLieus.Any())
                            giaSach = daInput
                                .SelectMany(m => m.ThongKe_DoanhThuHocPhi_TaiLieus)
                                .Select(t => new SachViewModel { Gia = t.Sach.Gia, SachId = t.SachId, Name = t.Sach.Name })
                                .ToArray();
                    }

                    var daDongHocPhi = daInput.Any(m => m.DaDong);
                    var tronGoi = daInput.Any(m => m.TronGoi);

                    var daNo = daInput.FirstOrDefault()?.DaNo
                                                        ??
                                                        hocVien.HocVien.HocVien_Nos
                                                                .Any(m => m.NgayNo.Month == currentMonth && m.NgayNo.Year == currentYear && m.LopHocId == lopHocId && !m.IsDisabled);

                    var item = new HocVienViewModel
                    {
                        SoNgayDuocNghi = soNgayDuocNghi,
                        NgayBatDau_Date = ngayBatDau_Date.Value,
                        NgayKetThuc_Date = ngayKetThuc_Date,
                        FullName = hocVien.HocVien.FullName,
                        HocVienId = hocVien.HocVienId,
                        NgayBatDauHoc = ngayBatDauHoc,
                        NgayKetThuc = ngayKetThuc,
                        TienNo = tienNo,
                        HocPhiMoi = hocPhiCurrent,
                        DaSaveNhap = daSaveNhap,
                        Bonus = bonus,
                        Minus = minus,
                        KhuyenMai = khuyenMai,
                        GhiChu = ghiChu,
                        HocPhiFixed = hocPhiCurrent,
                        GiaSach = giaSach,
                        LastGiaSach = giaSach,
                        DaDongHocPhi = daDongHocPhi,
                        TronGoi = tronGoi,
                        DaNo = daNo,
                        LastBonus = bonus,
                        LastMinus = minus
                    };

                    #region TINH HOC PHI THANG TRUOC

                    var khuyenMaiThangTruoc = hocVien.HocVien
                                                    .ThongKe_DoanhThuHocPhis
                                                    .FirstOrDefault(m => m.LopHocId == lopHocId && m.HocVienId == hocVien.HocVienId && m.NgayDong.Month == month && m.NgayDong.Year == year)
                                                    ?.KhuyenMai ?? 0;
                    var soNgayHocVienVaoSauThangTruoc = (item.NgayBatDau_Date.Month == month && item.NgayBatDau_Date.Year == year) ? 
                                                                _hocPhiRepository.TinhSoNgayHocVienVoSauAsync(year, month, item.NgayBatDau_Date, lopHocId) : preSoNgayHoc;
                    var soNgayConLaiSauKhiNghiThangTruoc = 0;
                    if (item.NgayKetThuc_Date != null)
                    {
                        if (item.NgayKetThuc_Date.Value.Month == month && item.NgayKetThuc_Date.Value.Year == year)
                            soNgayConLaiSauKhiNghiThangTruoc = _hocPhiRepository.TinhSoNgayHocVienVoSauAsync(year, month, item.NgayKetThuc_Date.Value, lopHocId);
                    }
                    var soNgayHocThucTeThangTruoc = preSoNgayHoc > soNgayHocVienVaoSauThangTruoc ?
                                                                            soNgayHocVienVaoSauThangTruoc - soNgayConLaiSauKhiNghiThangTruoc : preSoNgayHoc - soNgayConLaiSauKhiNghiThangTruoc;

                    var (soNgayNamTrongThoiHanHocPhiTronGoiThangTruoc, notUsed, notUsed2, notUsed3) = TinhSoNgayNamTrongThoiHanHocPhiTronGoi(item.HocVienId, lopHocId, month, year);
                    var hocPhiThangTruoc = (preHocPhi / preSoNgayHoc) * (soNgayHocThucTeThangTruoc - soNgayNamTrongThoiHanHocPhiTronGoiThangTruoc);
                    var khuyenMaiThangTruocValue = (khuyenMaiThangTruoc / 100.0) * hocPhiThangTruoc;
                    var hocPhiSauKhuyenMaiThangTruoc = hocPhiThangTruoc - khuyenMaiThangTruocValue;
                    var hocPhiMoiNgaySauKhuyenMai = soNgayHocThucTeThangTruoc == 0 ? 
                    0 : 
                    hocPhiSauKhuyenMaiThangTruoc / (soNgayHocThucTeThangTruoc > soNgayNamTrongThoiHanHocPhiTronGoiThangTruoc ? soNgayHocThucTeThangTruoc - soNgayNamTrongThoiHanHocPhiTronGoiThangTruoc : soNgayHocThucTeThangTruoc);

                    //List<DateTime> ngayDuocNghiDate = new List<DateTime>();
                    //foreach (var ngayNghi in item.SoNgayDuocNghi)
                    //{
                    //    if (ngayNghi >= item.NgayBatDau_Date)
                    //    {
                    //        ngayDuocNghiDate.Add(ngayNghi);
                    //    }
                    //}

                    #endregion

                    #region TINH HOC PHI THANG NAY
                    var soNgayHocVienVaoSau = (item.NgayBatDau_Date.Month == currentMonth && item.NgayBatDau_Date.Year == currentYear) ?
                                                    _hocPhiRepository.TinhSoNgayHocVienVoSauAsync(currentYear, currentMonth, item.NgayBatDau_Date, lopHocId) : soNgayHocCurrent;
                    var soNgayConLaiSauKhiNghi = 0;
                    if (item.NgayKetThuc_Date != null)
                    {
                        if (item.NgayKetThuc_Date.Value.Month == currentMonth && item.NgayKetThuc_Date.Value.Year == currentYear)
                            soNgayConLaiSauKhiNghi = _hocPhiRepository.TinhSoNgayHocVienVoSauAsync(currentYear, currentMonth, item.NgayKetThuc_Date.Value, lopHocId);
                    }
                    var soNgayHocThucTeThangNay = soNgayHocCurrent > soNgayHocVienVaoSau ?
                                                                            soNgayHocVienVaoSau - soNgayConLaiSauKhiNghi : soNgayHocCurrent - soNgayConLaiSauKhiNghi;

                    var (soNgayNamTrongThoiHanHocPhiTronGoi, buTronGoi, isInTronGoi, ngayDuocNghi) = TinhSoNgayNamTrongThoiHanHocPhiTronGoi(item.HocVienId, lopHocId, currentMonth, currentYear, item.SoNgayDuocNghi, item.NgayBatDau_Date);

                    var hocPhiThangNay = Math.Floor((hocPhiCurrent / soNgayHocCurrent) * (soNgayHocThucTeThangNay - soNgayNamTrongThoiHanHocPhiTronGoi));
                    var khuyenMaiThangNay = (khuyenMai / 100.0) * hocPhiThangNay;
                    var hocPhiSauKhuyenMai = hocPhiThangNay - khuyenMaiThangNay;
                    var truHocPhiDoOffThangTruoc = hocPhiMoiNgaySauKhuyenMai * ngayDuocNghi;

                    var calculatedGiaSach = item.GiaSach != null ? item.GiaSach.Select(x => x.Gia).Sum() : 0;
                    var final = hocPhiSauKhuyenMai - truHocPhiDoOffThangTruoc + item.Bonus - item.Minus + calculatedGiaSach + item.TienNo;

                    #endregion


                    #region TRON GOI
                    //var soNgayTinhHocPhiNgoaiTronGoi = _hocPhiRepository.TinhSoNgayHocTronGoi(item.HocVienId, lopHocId, currentMonth, currentYear, ngayBatDau_Date.Value, ngayKetThuc_Date);
                    var isTronGoi = _hocPhiRepository.IsTronGoi(item.HocVienId, lopHocId, currentMonth, currentYear);

                    //if (isTronGoi && soNgayTinhHocPhiNgoaiTronGoi == 0)
                    //{
                    //    final = 0;
                    //    ResetValue(item);
                    //}
                    //else if (isTronGoi && soNgayTinhHocPhiNgoaiTronGoi > 0)
                    //{
                    //    var hocPhiCanTinhNgoaiTronGoi = (hocPhiSauKhuyenMai / soNgayHocThucTeThangNay) * soNgayTinhHocPhiNgoaiTronGoi;
                    //    final = hocPhiCanTinhNgoaiTronGoi;
                    //}
                    item.TronGoi = isInTronGoi;

                    if (buTronGoi > 0)
                        item.GhiChu = isInTronGoi ?
                        $"Gia hạn {buTronGoi} buổi HPTG"
                        :
                        $"Đã trừ lại {buTronGoi} buổi cho nghỉ nằm trong HPTG";
                    #endregion

                    item.SoNgayHoc = soNgayHocThucTeThangNay;
                    item.HocPhiMoi = final;
                    item.HocPhiFixed = hocPhiThangNay;
                    item.HocPhiSauKhuyenMai = hocPhiSauKhuyenMai;
                    item.HocPhiBuHocVienVaoSau = truHocPhiDoOffThangTruoc;
                    item.Stt = index;
                    index++;
                    return item;
                })
                .Where(x => x.NgayKetThuc_Date == null || (x.NgayKetThuc_Date.Value.Month >= currentMonth && x.NgayKetThuc_Date.Value.Year == currentYear) || x.NgayKetThuc_Date.Value.Year > currentYear)
                .Where(x => (x.NgayBatDau_Date.Month <= currentMonth && x.NgayBatDau_Date.Year == currentYear) || x.NgayBatDau_Date.Year < currentYear);

            return items.OrderBy(x => x.Stt).ToList();
        }

        private (int, int, bool, int) TinhSoNgayNamTrongThoiHanHocPhiTronGoi(
            Guid hocVienId,
            Guid lopHocId,
            int month,
            int year, 
            IEnumerable<DateTime> soNgayDuocNghi = null,
            DateTime? ngayBatDau = null)
        {
            var hocPhis = _hocPhiTronGoiRepository.GetHocPhiTronGoi(hocVienId, lopHocId);

            var ngayHocs = _hocPhiRepository.TinhSoNgayHoc(lopHocId, month, year);

            int result = 0;
            int soNgayBu = 0;
            bool isInTronGoi = false;
            List<DateTime> ngayDuocNghiDate = new List<DateTime>();

            foreach (var hocPhi in hocPhis)
            {
                int yearHP = int.Parse(hocPhi.LopHocList.FirstOrDefault(x => x.LopHoc.LopHocId == lopHocId).FromDate.Substring(0, 4));
                int monthHP = int.Parse(hocPhi.LopHocList.FirstOrDefault(x => x.LopHoc.LopHocId == lopHocId).FromDate.Substring(5, 2));
                int dayHP = int.Parse(hocPhi.LopHocList.FirstOrDefault(x => x.LopHoc.LopHocId == lopHocId).FromDate.Substring(8, 2));
                DateTime fromDate = new DateTime(yearHP, monthHP, dayHP);

                int yearHP2 = int.Parse(hocPhi.LopHocList.FirstOrDefault(x => x.LopHoc.LopHocId == lopHocId).ToDate.Substring(0, 4));
                int monthHP2 = int.Parse(hocPhi.LopHocList.FirstOrDefault(x => x.LopHoc.LopHocId == lopHocId).ToDate.Substring(5, 2));
                int dayHP2 = int.Parse(hocPhi.LopHocList.FirstOrDefault(x => x.LopHoc.LopHocId == lopHocId).ToDate.Substring(8, 2));
                DateTime toDate = new DateTime(yearHP2, monthHP2, dayHP2);

                result = ngayHocs.Count(x => x >= fromDate && x <= toDate);

                if (soNgayDuocNghi != null)
                    soNgayBu = soNgayDuocNghi.Count(x => x >= fromDate && x <= toDate);

                if (ngayHocs.FirstOrDefault() >= fromDate && ngayHocs.FirstOrDefault() <= toDate)
                    isInTronGoi = true;

                if (ngayBatDau != null && soNgayDuocNghi != null)
                {
                    foreach (var ngayNghi in soNgayDuocNghi)
                    {
                        if (ngayNghi >= ngayBatDau && (ngayNghi < fromDate || ngayNghi > toDate) && !hocPhi.IsDisabled)
                        {
                            ngayDuocNghiDate.Add(ngayNghi);
                        }
                    }
                }

            }

            if (!hocPhis.Any() || hocPhis.All(x => x.IsDisabled))
                if (ngayBatDau != null && soNgayDuocNghi != null)
                {
                    foreach (var ngayNghi in soNgayDuocNghi)
                    {
                        if (ngayNghi >= ngayBatDau)
                        {
                            ngayDuocNghiDate.Add(ngayNghi);
                        }
                    }
                }
            return (result, soNgayBu, isInTronGoi, ngayDuocNghiDate.Distinct().Count());
        }

        private void ResetValue(HocVienViewModel item)
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

        private bool IsDaDongNoLopKhac(IOrderedEnumerable<ThongKe_DoanhThuHocPhi> hocPhis, Guid lopHocId, int month, int year)
        {
            var daNo = hocPhis
                .FirstOrDefault(m => m.LopHocId != lopHocId &&
                                    (m.NgayDong.Year < year ||
                                                        (m.NgayDong.Year == year && m.NgayDong.Month <= month)) && 
                                    m.DaNo);
            var daDong = hocPhis.FirstOrDefault(m => (m.NgayDong.Year < year ||
                                                        (m.NgayDong.Year == year && m.NgayDong.Month <= month)) && 
                                                     !(!m.DaDong && !m.DaNo));

            if (daNo == null)
                return true;

            if (daNo != null && daDong == null)
                return false;

            return daDong.NgayDong >= daNo.NgayDong;
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
