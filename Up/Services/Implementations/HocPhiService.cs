﻿
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

        public HocPhiService(
            IHocPhiRepository hocPhiRepository,
            ILopHocRepository lopHocRepository)
        {
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

        public async Task<TinhHocPhiViewModel> TinhHocPhiAsync(TinhHocPhiInputModel input)
        {
            var (subMonth, subYear) = Helpers.TinhSubMonthSubYear(input.Month, input.Year);

            int soNgayHoc = await _lopHocRepository.DemSoNgayHocAsync(input.LopHocId, input.Month, input.Year);
            int soNgayHocCu = await _lopHocRepository.DemSoNgayHocAsync(input.LopHocId, subMonth, subYear);

            var hocPhiMoiNgay = input.HocPhi / soNgayHoc;

            var hocPhiCu = await _hocPhiRepository.GetHocPhiCuAsync(input.LopHocId, subMonth, subYear);
            var hocPhiMoiNgayCu = hocPhiCu == null ?
                (input.HocPhi / soNgayHocCu) : (hocPhiCu.Value / soNgayHocCu);

            return new TinhHocPhiViewModel
            {
                HocPhiMoiNgay = hocPhiMoiNgay,
                HocPhi = input.HocPhi,
                SoNgayHoc = soNgayHoc,
                HocVienList = await GetHocVien_No_NgayHocAsync(
                    input.LopHocId,
                    input.Month,
                    input.Year,
                    input.HocPhi,
                    soNgayHoc,
                    hocPhiMoiNgay,
                    hocPhiMoiNgayCu)
            };
        }

        private async Task<List<HocVienViewModel>> GetHocVien_No_NgayHocAsync(Guid lopHocId, int month, int year, double hocPhi, int soNgayHoc, double hocPhiMoiNgay, double hocPhiMoiNgayCu)
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

                    var khuyenMaiThangTruoc = hocVien
                                                    .LopHoc
                                                    .ThongKe_DoanhThuHocPhis
                                                    .FirstOrDefault(m => m.LopHocId == lopHocId && m.HocVienId == hocVien.HocVienId && m.NgayDong.Month == month && m.NgayDong.Year == year)
                                                    ?.KhuyenMai ?? 0;



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
                        HocPhiMoi = hocPhi,
                        DaSaveNhap = daSaveNhap,
                        Bonus = bonus,
                        Minus = minus,
                        KhuyenMai = khuyenMai,
                        GhiChu = ghiChu,
                        HocPhiFixed = hocPhi,
                        GiaSach = giaSach,
                        DaDongHocPhi = daDongHocPhi,
                        TronGoi = tronGoi,
                        DaNo = daNo,
                        LastBonus = bonus,
                        LastMinus = minus
                    };

                    var tinhSoNgayHoc = _hocPhiRepository.TinhSoNgayHocTronGoi(item.HocVienId, lopHocId, currentMonth, currentYear);
                    if (_hocPhiRepository.IsTronGoi(item.HocVienId, lopHocId, currentMonth, currentYear) && !tronGoi)
                    {
                        if (tinhSoNgayHoc == 0)
                        {
                            ResetValue(item);
                        }
                        else
                        {
                            item.TronGoi = true;
                            item.GhiChu = "Trọn gói";
                            item.HocPhiMoi = tinhSoNgayHoc * hocPhiMoiNgay;

                            var calculatedGiaSach = item.GiaSach != null ? item.GiaSach.Select(x => x.Gia).Sum() : 0;
                            item.HocPhiTruTronGoi = (item.HocPhiFixed - (tinhSoNgayHoc * hocPhiMoiNgay));

                            CalculateHocPhi(item, currentMonth, currentYear, lopHocId, calculatedGiaSach, hocPhiMoiNgay, hocPhiMoiNgayCu, soNgayHoc);
                        }
                    }
                    else
                    {
                        if (item.TronGoi)
                            item.HocPhiTruTronGoi = (item.HocPhiFixed - (tinhSoNgayHoc * hocPhiMoiNgay));

                        var calculatedGiaSach = item.GiaSach != null ? item.GiaSach.Select(x => x.Gia).Sum() : 0;
                        CalculateHocPhi(item, currentMonth, currentYear, lopHocId, calculatedGiaSach, hocPhiMoiNgay, hocPhiMoiNgayCu, soNgayHoc, true);
                    }

                    item.LastGiaSach = item.GiaSach;
                    item.Stt = index;

                    index++;
                    return item;
                })
                .Where(x => x.NgayKetThuc_Date == null || (x.NgayKetThuc_Date.Value.Month >= currentMonth && x.NgayKetThuc_Date.Value.Year == currentYear) || x.NgayKetThuc_Date.Value.Year > currentYear)
                .Where(x => (x.NgayBatDau_Date.Month <= currentMonth && x.NgayBatDau_Date.Year == currentYear) || x.NgayBatDau_Date.Year < currentYear);

            return items.OrderBy(x => x.Stt).ToList();
        }

        private void CalculateHocPhi(
            HocVienViewModel item,
            int currentMonth, 
            int currentYear, 
            Guid lopHocId, 
            double giaSach,
            double hocPhiMoiNgay,
            double hocPhiMoiNgayCu,
            int soNgayHoc,
            bool isTronGoi = false)
        {
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
                        soNgayTruSauNghi = _hocPhiRepository.TinhSoNgayHocVienVoSauAsync(currentYear, currentMonth, _ngayKetThuc, lopHocId);
                }


                if (_ngayBatDauHoc.Month == currentMonth && _ngayBatDauHoc.Year == currentYear)
                {
                    var soNgayHocVienVaoSau = _hocPhiRepository.TinhSoNgayHocVienVoSauAsync(currentYear, currentMonth, _ngayBatDauHoc, lopHocId);

                    item.HocPhiBuHocVienVaoSau = (hocPhiMoiNgay * (soNgayHoc - soNgayHocVienVaoSau)) + (hocPhiMoiNgay * soNgayTruSauNghi) + (hocPhiMoiNgayCu * ngayDuocNghi * (100 - item.KhuyenMaiThangTruoc) / 100);
                    item.HocPhiMoiFixed = item.HocPhiMoi - item.HocPhiBuHocVienVaoSau;
                    item.HocPhiMoi = item.HocPhiMoi - (item.HocPhiFixed * item.KhuyenMai / 100) + giaSach + item.Bonus - item.Minus + item.TienNo - item.HocPhiBuHocVienVaoSau - (isTronGoi ? item.HocPhiTruTronGoi : 0);
                }
                else
                {
                    item.HocPhiBuHocVienVaoSau = (hocPhiMoiNgay * soNgayTruSauNghi) + (hocPhiMoiNgayCu * ngayDuocNghi * (100 - item.KhuyenMaiThangTruoc) / 100);
                    item.HocPhiMoiFixed = item.HocPhiMoi - item.HocPhiBuHocVienVaoSau;
                    item.HocPhiMoi = item.HocPhiMoi - (item.HocPhiFixed * item.KhuyenMai / 100) + giaSach + item.Bonus - item.Minus + item.TienNo - item.HocPhiBuHocVienVaoSau - (isTronGoi ? item.HocPhiTruTronGoi : 0);
                }
            }
            else
            {
                item.HocPhiMoi = item.HocPhiMoi - (item.HocPhiFixed * item.KhuyenMai / 100) + giaSach + item.Bonus - item.Minus + item.TienNo - (isTronGoi ? item.HocPhiTruTronGoi : 0);
            }
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
            var daNo = hocPhis.FirstOrDefault(m => m.LopHocId != lopHocId && m.NgayDong.Month <= month && m.NgayDong.Year <= year && m.DaNo);
            var daDong = hocPhis.FirstOrDefault(m => m.NgayDong.Month <= month && m.NgayDong.Year <= year && !(!m.DaDong && !m.DaNo));

            if (daNo == null)
                return true;

            if (daNo != null && daDong == null)
                return false;

            return daDong.NgayDong > daNo.NgayDong;
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
