using System;
using System.Collections.Generic;
using System.Linq;
using Up.Data.Entities;
using Up.Extensions;
using Up.Models;

namespace Up.Converters
{
    public class EntityConverter
    {
        public HocVienViewModel ToHocVienViewModel(HocVien hocVien)
        {
            return new HocVienViewModel
            {
                FullName = hocVien.FullName,
                NgaySinh = hocVien.NgaySinh?.ToClearDate() ?? string.Empty,
                EnglishName = hocVien.EnglishName,
                QuanHeId = hocVien.QuanHeId,
                CreatedBy = hocVien.CreatedBy,
                CreatedDate = hocVien.CreatedDate.ToClearDate(),
                FacebookAccount = hocVien.FacebookAccount,
                IsDisabled = hocVien.IsDisabled,
                ParentFullName = hocVien.ParentFullName,
                ParentPhone = hocVien.ParentPhone,
                Phone = hocVien.Phone,
                OtherPhone = hocVien.OtherPhone,
                HocVienId = hocVien.HocVienId,
                QuanHe = hocVien.QuanHe?.Name ?? string.Empty,
                CMND = hocVien.CMND,
                DiaChi = hocVien.DiaChi,
                Notes = hocVien.Notes,
                Trigram = hocVien.Trigram,
                PassedChallenge = hocVien.ChallengeResults
                                        .Where(x => x.IsPass && !x.ThuThach.IsDisabled)
                                        .Select(x => x.ThuThach.Name)
                                        .Distinct(),
                LopHocList = hocVien.HocVien_LopHocs
                                    .Select(x => new LopHocViewModel
                                    {
                                        LopHocId = x.LopHocId,
                                        KhoaHocId = x.LopHoc.KhoaHocId,
                                        Name = x.LopHoc.Name,
                                        IsCanceled = x.LopHoc.IsCanceled,
                                        IsGraduated = x.LopHoc.IsGraduated,
                                        HocVienNghi = x.HocVien.HocVien_NgayHocs.FirstOrDefault(n => n.LopHocId == x.LopHocId).NgayKetThuc == null ? false : x.HocVien.HocVien_NgayHocs.FirstOrDefault(n => n.LopHocId == x.LopHocId).NgayKetThuc < DateTime.Now ? true : false
                                    })
                                    .ToList(),
                LopHoc_NgayHocList = hocVien.HocVien_LopHocs
                                    .Select(x => new LopHoc_NgayHocViewModel
                                    {
                                        LopHoc = new LopHocViewModel
                                        {
                                            LopHocId = x.LopHocId,
                                            Name = x.LopHoc.Name
                                        },
                                        NgayHoc = hocVien.HocVien_NgayHocs.FirstOrDefault(m => m.LopHocId == x.LopHocId)?.NgayBatDau.ToEditionDate() ?? string.Empty
                                    })
                                    .ToList()
            };
        }

        public HocVienLightViewModel ToHocVienLightViewModel(HocVien hocVien)
        {
            return new HocVienLightViewModel
            {
                FullName = hocVien.FullName,
                NgaySinh = hocVien.NgaySinh?.ToClearDate() ?? string.Empty,
                IsDisabled = hocVien.IsDisabled,
                Phone = hocVien.Phone,
                HocVienId = hocVien.HocVienId,
            };
        }

        public HocVien_NgayHocViewModel ToHocVien_NgayHocViewModel(HocVien_NgayHoc hocVien_NgayHoc)
        {
            return new HocVien_NgayHocViewModel
            {
                NgayBatDau = hocVien_NgayHoc.NgayBatDau.ToClearDate(),
                NgayKetThuc = hocVien_NgayHoc.NgayKetThuc?.ToClearDate() ?? string.Empty,
                HocVienId = hocVien_NgayHoc.HocVienId,
                LopHocId = hocVien_NgayHoc.LopHocId
            };
        }

        public NgayHocViewModel ToNgayHocViewModel(NgayHoc ngayHoc)
        {
            return new NgayHocViewModel
            {
                CreatedBy = ngayHoc.CreatedBy,
                CreatedDate = ngayHoc.CreatedDate.ToClearDate(),
                NgayHocId = ngayHoc.NgayHocId,
                Name = ngayHoc.Name,
                UpdatedBy = ngayHoc.UpdatedBy,
                UpdatedDate = ngayHoc.UpdatedDate?.ToClearDate() ?? string.Empty
            };
        }

        public LopHocViewModel ToLopHocViewModel(LopHoc lopHoc)
        {
            return new LopHocViewModel
            {
                CreatedBy = lopHoc.CreatedBy,
                CreatedDate = lopHoc.CreatedDate.ToClearDate(),
                GioHocId = lopHoc.GioHocId,
                Name = lopHoc.Name,
                KhoaHocId = lopHoc.KhoaHocId,
                KhoaHoc = lopHoc.KhoaHoc.Name,
                IsGraduated = lopHoc.IsGraduated,
                GioHocFrom = lopHoc.GioHoc.From,
                GioHocTo = lopHoc.GioHoc.To,
                IsCanceled = lopHoc.IsCanceled,
                LopHocId = lopHoc.LopHocId,
                NgayHocId = lopHoc.NgayHocId,
                NgayHoc = lopHoc.NgayHoc.Name,
                NgayKhaiGiang = lopHoc.NgayKhaiGiang.ToClearDate(),
                NgayKetThuc = lopHoc.NgayKetThuc?.ToClearDate() ?? string.Empty,
                UpdatedBy = lopHoc.UpdatedBy,
                UpdatedDate = lopHoc.UpdatedDate?.ToClearDate() ?? string.Empty
            };
        }

        public KhoaHocViewModel ToKhoaHocViewModel(KhoaHoc khoaHoc)
        {
            return new KhoaHocViewModel
            {
                CreatedBy = khoaHoc.CreatedBy,
                CreatedDate = khoaHoc.CreatedDate.ToClearDate(),
                KhoaHocId = khoaHoc.KhoaHocId,
                Name = khoaHoc.Name,
                UpdatedBy = khoaHoc.UpdatedBy,
                UpdatedDate = khoaHoc.UpdatedDate?.ToClearDate() ?? string.Empty
            };
        }

        public SachViewModel ToSachViewModel(Sach sach)
        {
            return new SachViewModel
            {
                CreatedBy = sach.CreatedBy,
                CreatedDate = sach.CreatedDate.ToClearDate(),
                SachId = sach.SachId,
                Name = sach.Name,
                Gia = sach.Gia,
                UpdatedBy = sach.UpdatedBy,
                UpdatedDate = sach.UpdatedDate?.ToClearDate() ?? string.Empty,
                IsDisabled = sach.IsDisabled
            };
        }

        public HocPhiViewModel ToHocPhiViewModel(HocPhi hocPhi)
        {
            return new HocPhiViewModel
            {
                CreatedBy = hocPhi.CreatedBy,
                CreatedDate = hocPhi.CreatedDate.ToClearDate(),
                HocPhiId = hocPhi.HocPhiId,
                Gia = hocPhi.Gia,
                NgayApDung = hocPhi.NgayApDung?.ToClearDate() ?? string.Empty,
                GhiChu = hocPhi.GhiChu,
                UpdatedBy = hocPhi.UpdatedBy,
                UpdatedDate = hocPhi.UpdatedDate?.ToClearDate() ?? string.Empty
            };
        }

        public GioHocViewModel ToGioHocViewModel(GioHoc gioHoc)
        {
            return new GioHocViewModel
            {
                CreatedBy = gioHoc.CreatedBy,
                CreatedDate = gioHoc.CreatedDate.ToClearDate(),
                GioHocId = gioHoc.GioHocId,
                From = gioHoc.From,
                To = gioHoc.To,
                UpdatedBy = gioHoc.UpdatedBy,
                UpdatedDate = gioHoc.UpdatedDate?.ToClearDate() ?? string.Empty,
            };
        }

        public QuanHeViewModel ToQuanHeViewModel(QuanHe quanHe)
        {
            return new QuanHeViewModel
            {
                CreatedBy = quanHe.CreatedBy,
                CreatedDate = quanHe.CreatedDate.ToClearDate(),
                QuanHeId = quanHe.QuanHeId,
                Name = quanHe.Name,
                UpdatedBy = quanHe.UpdatedBy,
                UpdatedDate = quanHe.UpdatedDate?.ToClearDate() ?? string.Empty
            };
        }

        public LoaiGiaoVienViewModel ToLoaiGiaoVienViewModel(LoaiGiaoVien loaiGiaoVien)
        {
            return new LoaiGiaoVienViewModel
            {
                CreatedBy = loaiGiaoVien.CreatedBy,
                CreatedDate = loaiGiaoVien.CreatedDate.ToClearDate(),
                LoaiGiaoVienId = loaiGiaoVien.LoaiGiaoVienId,
                Name = loaiGiaoVien.Name,
                UpdatedBy = loaiGiaoVien.UpdatedBy,
                UpdatedDate = loaiGiaoVien.UpdatedDate?.ToClearDate() ?? string.Empty,
                Order = loaiGiaoVien.Order
            };
        }

        public NgayLamViecViewModel ToNgayLamViecViewModel(NgayLamViec ngayLamViec)
        {
            return new NgayLamViecViewModel
            {
                CreatedBy = ngayLamViec.CreatedBy,
                CreatedDate = ngayLamViec.CreatedDate.ToClearDate(),
                NgayLamViecId = ngayLamViec.NgayLamViecId,
                Name = ngayLamViec.Name,
                UpdatedBy = ngayLamViec.UpdatedBy,
                UpdatedDate = ngayLamViec.UpdatedDate?.ToClearDate() ?? string.Empty
            };
        }

        public GiaoVienViewModel ToGiaoVienViewModel(GiaoVien giaoVien)
        {
            return new GiaoVienViewModel
            {
                CreatedBy = giaoVien.CreatedBy,
                CreatedDate = giaoVien.CreatedDate.ToClearDate(),
                GiaoVienId = giaoVien.GiaoVienId,
                Phone = giaoVien.Phone,
                FacebookAccount = giaoVien.FacebookAccount,
                DiaChi = giaoVien.DiaChi,
                CMND = giaoVien.CMND,
                TeachingRate = giaoVien.TeachingRate,
                TutoringRate = giaoVien.TutoringRate,
                BasicSalary = giaoVien.BasicSalary,
                InitialName = giaoVien.InitialName,
                Name = giaoVien.Name,
                MucHoaHong = giaoVien.MucHoaHong,
                UpdatedBy = giaoVien.UpdatedBy,
                UpdatedDate = giaoVien.UpdatedDate?.ToClearDate() ?? string.Empty,
                NgayBatDau = giaoVien.NgayBatDau.ToClearDate(),
                NgayKetThuc = giaoVien.NgayKetThuc?.ToClearDate() ?? string.Empty,
                NgayLamViecId = giaoVien.NgayLamViecId,
                NgayLamViec = giaoVien.NgayLamViec.Name,
                NganHang = giaoVien.NganHang,
                LoaiNhanVien_CheDo = giaoVien.NhanVien_ViTris
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
                                        .ToList()
            };
        }

        public LoaiCheDoViewModel ToLoaiCheDoViewModel(LoaiCheDo loaiCheDo)
        {
            return new LoaiCheDoViewModel
            {
                CreatedBy = loaiCheDo.CreatedBy,
                CreatedDate = loaiCheDo.CreatedDate.ToClearDate(),
                LoaiCheDoId = loaiCheDo.LoaiCheDoId,
                Name = loaiCheDo.Name,
                UpdatedBy = loaiCheDo.UpdatedBy,
                UpdatedDate = loaiCheDo.UpdatedDate?.ToClearDate() ?? string.Empty
            };
        }

        public ChiPhiCoDinhViewModel ToChiPhiCoDinhViewModel(ChiPhiCoDinh chiPhiCoDinh)
        {
            return new ChiPhiCoDinhViewModel
            {
                CreatedBy = chiPhiCoDinh.CreatedBy,
                CreatedDate = chiPhiCoDinh.CreatedDate.ToClearDate(),
                ChiPhiCoDinhId = chiPhiCoDinh.ChiPhiCoDinhId,
                Gia = chiPhiCoDinh.Gia,
                Name = chiPhiCoDinh.Name,
                UpdatedBy = chiPhiCoDinh.UpdatedBy,
                UpdatedDate = chiPhiCoDinh.UpdatedDate?.ToClearDate() ?? string.Empty
            };
        }

        public ChiPhiKhacViewModel ToChiPhiKhacViewModel(ChiPhiKhac chiPhiKhac)
        {
            return new ChiPhiKhacViewModel
            {
                CreatedBy = chiPhiKhac.CreatedBy,
                CreatedDate = chiPhiKhac.CreatedDate.ToClearDate(),
                ChiPhiKhacId = chiPhiKhac.ChiPhiKhacId,
                NgayChiPhi = chiPhiKhac.NgayChiPhi,
                _NgayChiPhi = chiPhiKhac.NgayChiPhi.ToClearDate(),
                Gia = chiPhiKhac.Gia,
                Name = chiPhiKhac.Name,
                UpdatedBy = chiPhiKhac.UpdatedBy,
                UpdatedDate = chiPhiKhac.UpdatedDate?.ToClearDate() ?? string.Empty
            };
        }

        public NoViewModel ToNoViewModel(HocVien_No no)
        {
            return new NoViewModel
            {
                CreatedBy = no.CreatedBy,
                CreatedDate = no.CreatedDate.ToClearDate(),
                LopHocId = no.LopHocId,
                HocVienId = no.HocVienId,
                LopHoc = no.LopHoc.Name,
                NgayNo = no.NgayNo.ToClearDate(),
                TienNo = no.TienNo,
                HocVien = no.HocVien.FullName
            };
        }

        public HocPhiTronGoiViewModel ToHocPhiTronGoiViewModel(HocPhiTronGoi hocPhi)
        {
            return new HocPhiTronGoiViewModel
            {
                CreatedBy = hocPhi.CreatedBy,
                CreatedDate = hocPhi.CreatedDate.ToClearDate(),
                HocPhiTronGoiId = hocPhi.HocPhiTronGoiId,
                Name = hocPhi.HocVien.FullName,
                HocVienId = hocPhi.HocVienId,
                HocPhi = hocPhi.HocPhi,
                IsDisabled = hocPhi.IsDisabled,
                GhiChu = hocPhi.GhiChu,
                FromDate = hocPhi.FromDate.ToClearDate(),
                ToDate = hocPhi.ToDate.ToClearDate(),
                UpdatedBy = hocPhi.UpdatedBy,
                UpdatedDate = hocPhi.UpdatedDate?.ToClearDate() ?? string.Empty,
                LopHocList = hocPhi.HocPhiTronGoi_LopHocs
                    .Select(m => new HocPhiTronGoi_LopHocViewModel
                    {
                        FromDate = m.FromDate.ToEditionDate(),
                        ToDate = m.ToDate.ToEditionDate(),
                        LopHoc = new LopHocViewModel
                        {
                            LopHocId = m.LopHocId,
                            Name = m.LopHoc.Name
                        }
                    })
                    .ToList()
            };
        }

        public BienLaiViewModel ToBienLaiViewModel(BienLai bienLai, LopHocViewModel lopHoc)
        {
            return new BienLaiViewModel
            {
                CreatedBy = bienLai.CreatedBy,
                CreatedDate = bienLai.CreatedDate.ToClearDate(),
                HocPhi = bienLai.HocPhi,
                FullName = bienLai.HocVien.FullName,
                ThangHocPhi = bienLai.ThangHocPhi,
                BienLaiId = bienLai.BienLaiId,
                TenLop = lopHoc?.Name ?? string.Empty,
                MaBienLai = bienLai.MaBienLai
            };
        }

        public ThuThachViewModel ToThuThachViewModel(ThuThach thuThach)
        {
            return new ThuThachViewModel
            {
                CreatedBy = thuThach.CreatedBy,
                CreatedDate = thuThach.CreatedDate.ToClearDate(),
                ThuThachId = thuThach.ThuThachId,
                Name = thuThach.Name,
                MinGrade = thuThach.MinGrade,
                SoCauHoi = thuThach.SoCauHoi,
                KhoaHocId = thuThach.KhoaHocId,
                TenKhoaHoc = thuThach.KhoaHoc.Name,
                ThoiGianLamBai = thuThach.ThoiGianLamBai,
                UpdatedBy = thuThach.UpdatedBy,
                UpdatedDate = thuThach.UpdatedDate?.ToClearDate() ?? string.Empty
            };
        }

        public CauHoiViewModel ToCauHoiViewModel(CauHoi cauHoi)
        {
            return new CauHoiViewModel
            {
                CreatedBy = cauHoi.CreatedBy,
                CreatedDate = cauHoi.CreatedDate.ToClearDate(),
                ThuThachId = cauHoi.ThuThachId,
                TenThuThach = cauHoi.ThuThach.Name,
                CauHoiId = cauHoi.CauHoiId,
                Name = cauHoi.Name,
                STT = cauHoi.STT,
                DapAns = cauHoi.DapAns
                            .Select(dapAn => new DapAnModel 
                            {
                                DapAnId = dapAn.DapAnId,
                                Name = dapAn.Name,
                                IsTrue = dapAn.IsTrue
                            })
                            .ToList(),
                UpdatedBy = cauHoi.UpdatedBy,
                UpdatedDate = cauHoi.UpdatedDate?.ToClearDate() ?? string.Empty
            };
        }

        ///ENTITY

        public HocVien ToEntityHocVien<T>(T input, string loggedEmployee) where T : CreateHocVienInput
        {
            return new HocVien
            {
                FullName = input.FullName,
                Phone = input.Phone,
                OtherPhone = input.OtherPhone,
                FacebookAccount = input.FacebookAccount,
                ParentPhone = input.ParentPhone,
                ParentFullName = input.ParentFullName,
                QuanHeId = input.QuanHeId,
                EnglishName = input.EnglishName,
                NgaySinh = input.NgaySinhDate,
                CreatedBy = loggedEmployee,
                CMND = input.CMND,
                DiaChi = input.DiaChi,
                Notes = input.Notes
            };
        }

        public LopHoc ToEntityLopHoc(CreateLopHocInputModel input, string loggedEmployee)
        {
            return new LopHoc
            {
                Name = input.Name,
                KhoaHocId = input.KhoaHocId,
                NgayKhaiGiang = input.NgayKhaiGiangDate,
                NgayHocId = input.NgayHocId,
                GioHocId = input.GioHocId,
                CreatedBy = loggedEmployee
            };
        }

        public Sach ToEntitySach(CreateSachInputModel input, string loggedEmployee)
        {
            return new Sach
            {
                Name = input.Name,
                Gia = input.Gia,
                CreatedBy = loggedEmployee
            };
        }

        public LopHoc_HocPhi ToEntityHocPhi(Guid lopHocId, Guid hocPhiId, int thang, int nam)
        {
            return new LopHoc_HocPhi
            {
                LopHocId = lopHocId,
                Nam = nam,
                Thang = thang,
                HocPhiId = hocPhiId
            };
        }

        public HocPhi ToEntityHocPhi(CreateHocPhiInputModel input, string loggedEmployee)
        {
            return new HocPhi
            {
                Gia = input.Gia,
                GhiChu = input.GhiChu,
                NgayApDung = input.NgayApDungDate,
                CreatedBy = loggedEmployee
            };
        }

        public GioHoc ToEntityGioHoc(CreateGioHocInputModel input, string loggedEmployee)
        {
            return new GioHoc
            {
                From = input.From,
                To = input.To,
                CreatedBy = loggedEmployee
            };
        }

        public LoaiGiaoVien ToEntityLoaiGiaoVien(CreateLoaiGiaoVienInputModel input, string loggedEmployee)
        {
            return new LoaiGiaoVien
            {
                Name = input.Name,
                Order = input.Order,
                CreatedBy = loggedEmployee
            };
        }

        public GiaoVien ToEntityGiaoVien(CreateGiaoVienInputModel input, string loggedEmployee)
        {
            DateTime? ngayKetThuc = null;
            if (input.NgayKetThucDate != new DateTime())
                ngayKetThuc = input.NgayKetThucDate;

            return new GiaoVien
            {
                Name = input.Name,
                Phone = input.Phone,
                FacebookAccount = input.FacebookAccount,
                DiaChi = input.DiaChi,
                InitialName = input.InitialName,
                CMND = input.CMND,
                TeachingRate = input.TeachingRate,
                TutoringRate = input.TutoringRate,
                BasicSalary = input.BasicSalary,
                MucHoaHong = input.MucHoaHong,
                NgayKetThuc = ngayKetThuc,
                NgayLamViecId = input.NgayLamViecId,
                NgayBatDau = input.NgayBatDauDate,
                NganHang = input.NganHang,
                CreatedBy = loggedEmployee
            };
        }

        public ChiPhiKhac ToEntityChiPhiKhac(CreateChiPhiKhacInputModel input, string loggedEmployee)
        {
            return new ChiPhiKhac
            {
                Name = input.Name,
                Gia = input.Gia,
                NgayChiPhi = input.NgayChiPhi,
                CreatedBy = loggedEmployee
            };
        }

        public IList<ThongKe_ChiPhi> ToThongKe_ChiPhiList(ThongKe_ChiPhiViewModel[] input, DateTime ngayChiPhi, string loggedEmployee)
            => input.Select(item =>
            {
                return new ThongKe_ChiPhi
                {
                    NgayChiPhi = ngayChiPhi,
                    CreatedBy = loggedEmployee,
                    CreatedDate = DateTime.Now,
                    ChiPhi = item.ChiPhiMoi,
                    Bonus = item.Bonus,
                    Minus = item.Minus,
                    SoGioDay = item.SoGioDay,
                    SoGioKem = item.SoGioKem,
                    ChiPhiCoDinhId = item.ChiPhiCoDinhId,
                    NhanVienId = item.NhanVienId,
                    DaLuu = item.DaLuu,
                    SoHocVien = item.SoHocVien,
                    DailySalary = item.DailySalary,
                    NgayLamViec = item.NgayLamViec,
                    Salary_Expense = item.Salary_Expense,
                    SoNgayLam = item.SoNgayLam,
                    SoNgayLamVoSau = item.SoNgayLamVoSau,
                    SoNgayNghi = item.SoNgayNghi,
                    TeachingRate = item.TeachingRate,
                    TutoringRate = item.TutoringRate,
                    MucHoaHong = item.MucHoaHong,
                    GhiChu = item.GhiChu
                };
            })
            .ToList();

        public LopHoc_DiemDanh ToEntityLopHoc_DiemDanh(DiemDanhHocVienInput input, string loggedEmployee)
        {
            return new LopHoc_DiemDanh
            {
                NgayDiemDanh = input.NgayDiemDanh,
                IsOff = input.IsOff,
                LopHocId = input.LopHocId,
                HocVienId = input.HocVienId,
                CreatedBy = loggedEmployee
            };
        }

        public IList<LopHoc_DiemDanh> ToLopHoc_DiemDanhList(DiemDanhHocVienInput input, IList<HocVienViewModel> hocViens, string loggedEmployee)
        {
            return hocViens.Select(hocVien =>
            {
                return new LopHoc_DiemDanh
                {
                    NgayDiemDanh = input.NgayDiemDanh,
                    IsOff = input.IsOff,
                    LopHocId = input.LopHocId,
                    HocVienId = hocVien.HocVienId,
                    CreatedBy = loggedEmployee
                };
            })
            .ToList();
        }

        public IList<LopHoc_DiemDanh> ToDiemDanhDuocNghiList(DiemDanhHocVienInput input, IList<HocVienViewModel> hocViens, string loggedEmployee)
        {
            return hocViens.Select(hocVien =>
            {
                return new LopHoc_DiemDanh
                {
                    NgayDiemDanh = input.NgayDiemDanh,
                    IsOff = true,
                    IsDuocNghi = true,
                    LopHocId = input.LopHocId,
                    HocVienId = hocVien.HocVienId,
                    CreatedBy = loggedEmployee
                };
            })
            .ToList();
        }

        public IList<LopHoc_DiemDanh> ToDiemDanhSinhVienOffList(Guid lopHocId, DateTime ngayDiemDanh, IList<HocVienViewModel> hocViens, string loggedEmployee)
        {
            return hocViens.Select(hocVien =>
            {
                return new LopHoc_DiemDanh
                {
                    NgayDiemDanh = ngayDiemDanh,
                    IsOff = true,
                    IsDuocNghi = true,
                    LopHocId = lopHocId,
                    HocVienId = hocVien.HocVienId,
                    CreatedBy = loggedEmployee
                };
            })
            .ToList();
        }

        public ThongKe_DoanhThuHocPhi ToEntityThongKe_DoanhThuHocPhi(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee)
        {
            return new ThongKe_DoanhThuHocPhi
            {
                LopHocId = input.LopHocId,
                HocVienId = input.HocVienId,
                CreatedBy = loggedEmployee,
                NgayDong = input.NgayDong,
                HocPhi = input.HocPhi,
                Bonus = input.Bonus,
                KhuyenMai = input.KhuyenMai,
                Minus = input.Minus,
                GhiChu = input.GhiChu,
                DaDong = input.DaDong,
                DaNo = input.DaNo,
                TronGoi = input.TronGoi
            };
        }

        public HocVien_No ToEntityHocVien_No(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee)
        {
            return new HocVien_No
            {
                HocVienId = input.HocVienId,
                CreatedBy = loggedEmployee,
                TienNo = input.HocPhi,
                LopHocId = input.LopHocId,
                NgayNo = input.NgayDong
            };
        }

        public IList<ThongKe_DoanhThuHocPhi_TaiLieu> ToThongKe_DoanhThuHocPhi_TaiLieuList(Guid thongKe_DoanhThuHocPhiId, IList<Guid> sachIds, string loggedEmployee)
        {
            return sachIds.Select(sachId =>
            {
                return new ThongKe_DoanhThuHocPhi_TaiLieu
                {
                    ThongKe_DoanhThuHocPhiId = thongKe_DoanhThuHocPhiId,
                    CreatedBy = loggedEmployee,
                    SachId = sachId
                };
            })
            .ToList();
        }

        public HocPhiTronGoi ToEntityHocPhiTronGoi(CreateHocPhiTronGoiInputModel input, string loggedEmployee)
        {
            return new HocPhiTronGoi
            {
                HocVienId = input.HocVienId,
                CreatedBy = loggedEmployee,
                HocPhi = input.HocPhi,
                FromDate = Convert.ToDateTime(input.FromDate),
                ToDate = Convert.ToDateTime(input.ToDate),
                GhiChu = input.GhiChu
            };
        }

        public IList<HocPhiTronGoi_LopHoc> ToHocPhiTronGoi_LopHocList(Guid hocPhiTronGoiId, IList<HocPhiTronGoi_LopHocViewModel> lopHocList, string loggedEmployee)
        {
            return lopHocList.Select(lopHoc =>
            {
                return new HocPhiTronGoi_LopHoc
                {
                    CreatedBy = loggedEmployee,
                    LopHocId = lopHoc.LopHoc.LopHocId,
                    FromDate = Convert.ToDateTime(lopHoc.FromDate),
                    ToDate = Convert.ToDateTime(lopHoc.ToDate),
                    HocPhiTronGoiId = hocPhiTronGoiId
                };
            })
            .ToList();
        }

        public BienLai ToEntityBienLai(CreateBienLaiInputModel input, string loggedEmployee)
        {
            return new BienLai
            {
                MaBienLai = input.MaBienLai,
                HocPhi = input.HocPhi,
                HocVienId = input.HocVienId,
                LopHocId = input.LopHocId,
                IsDisabled = false,
                ThangHocPhi = input.ThangHocPhi,
                CreatedBy = loggedEmployee
            };
        }

        public ThuThach ToEntityThuThach(CreateThuThachInputModel input, string loggedEmployee)
        {
            return new ThuThach
            {
                Name = input.Name,
                KhoaHocId = input.KhoaHocId,
                SoCauHoi = input.SoCauHoi,
                MinGrade = input.MinGrade,
                ThoiGianLamBai = input.ThoiGianLamBai,
                CreatedBy = loggedEmployee
            };
        }

        public ChallengeResult ToEntityKetQua(ResultInputModel input)
        {
            return new ChallengeResult
            {
                HocVienId = input.HocVienId,
                ThuThachId = input.ThuThachId,
                IsPass = input.IsPass,
                LanThi = input.LanThi,
                Score = input.Score,
                CreatedBy = string.Empty
            };
        }

        public CauHoi ToEntityCauHoi(CreateCauHoiInputModel input, string loggedEmployee)
        {
            return new CauHoi
            {
                Name = input.Name,
                ThuThachId = input.ThuThachId,
                STT = input.STT,
                DapAns = input.DapAns
                            .Select(dapAn => new DapAn 
                            {
                                Name = dapAn.Name,
                                IsTrue = dapAn.IsTrue,
                                CreatedBy = loggedEmployee
                            })
                            .ToList(),
                CreatedBy = loggedEmployee
            };
        }

        public IList<CauHoi> ToEntityCauHoiList(ImportCauHoiInputModel input, string loggedEmployee)
        {
            return input.CauHois.Select(cauHoi =>
            {
                return new CauHoi
                {
                    Name = cauHoi.Name,
                    ThuThachId = input.ThuThachId,
                    STT = cauHoi.STT,
                    DapAns = cauHoi.DapAns
                            .Where(x => !string.IsNullOrWhiteSpace(x.Name))
                            .Select(dapAn => new DapAn
                            {
                                Name = dapAn.Name,
                                IsTrue = dapAn.IsTrue,
                                CreatedBy = loggedEmployee
                            })
                            .ToList(),
                    CreatedBy = loggedEmployee
                };
            })
            .ToList();
        }

        ///MAPPING

        public void MappingEntityHocVien(UpdateHocVienInputModel input, HocVien item, string loggedEmployee)
        {
            item.FullName = input.FullName;
            item.QuanHeId = input.QuanHeId;
            item.Phone = input.Phone;
            item.OtherPhone = input.OtherPhone;
            item.FacebookAccount = input.FacebookAccount;
            item.ParentFullName = input.ParentFullName;
            item.ParentPhone = input.ParentPhone;
            item.EnglishName = input.EnglishName;
            item.NgaySinh = input.NgaySinhDate;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;
            item.CMND = input.CMND;
            item.DiaChi = input.DiaChi;
            item.Notes = input.Notes;
        }

        public void MappingEntityLopHoc(UpdateLopHocInputModel input, LopHoc item, string loggedEmployee)
        {
            item.Name = input.Name;
            item.KhoaHocId = input.KhoaHocId;
            item.NgayHocId = input.NgayHocId;
            item.GioHocId = input.GioHocId;
            item.NgayKhaiGiang = input.NgayKhaiGiangDate;
            item.NgayKetThuc = input.NgayKetThucDate;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;
        }

        public void MappingEntitySach(UpdateSachInputModel input, Sach item, string loggedEmployee)
        {
            item.Name = input.Name;
            item.Gia = input.Gia;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;
        }

        public void MappingEntityHocPhi(UpdateHocPhiInputModel input, HocPhi item, string loggedEmployee)
        {
            item.GhiChu = input.GhiChu;
            item.NgayApDung = input.NgayApDungDate;
            item.Gia = input.Gia;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;
        }

        public void MappingEntityGioHoc(UpdateGioHocInputModel input, GioHoc item, string loggedEmployee)
        {
            item.From = input.From;
            item.To = input.To;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;
        }

        public void MappingEntityLoaiGiaoVien(UpdateLoaiGiaoVienInputModel input, LoaiGiaoVien item, string loggedEmployee)
        {
            item.Name = input.Name;
            item.Order = input.Order;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;
        }

        public void MappingEntityGiaoVien(UpdateGiaoVienInputModel input, GiaoVien item, string loggedEmployee)
        {
            DateTime? ngayKetThuc = null;
            if (input.NgayKetThucDate != new DateTime())
                ngayKetThuc = input.NgayKetThucDate;

            item.Name = input.Name;
            item.Phone = input.Phone;
            item.FacebookAccount = input.FacebookAccount;
            item.DiaChi = input.DiaChi;
            item.InitialName = input.InitialName;
            item.CMND = input.CMND;
            item.GiaoVienId = input.GiaoVienId;
            item.TeachingRate = input.TeachingRate;
            item.TutoringRate = input.TutoringRate;
            item.BasicSalary = input.BasicSalary;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;
            item.MucHoaHong = input.MucHoaHong;
            item.NgayBatDau = input.NgayBatDauDate;
            item.NgayKetThuc = ngayKetThuc;
            item.NgayLamViecId = input.NgayLamViecId;
            item.NganHang = input.NganHang;
        }

        public void MappingEntityChiPhiKhac(UpdateChiPhiKhacInputModel input, ChiPhiKhac item, string loggedEmployee)
        {
            item.Name = input.Name;
            item.Gia = input.Gia;
            item.NgayChiPhi = input.NgayChiPhi;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;
        }

        public void MappingEntityThongKe_DoanhThuHocPhi(ThongKe_DoanhThuHocPhiInputModel input, ThongKe_DoanhThuHocPhi item, string loggedEmployee)
        {
            item.GhiChu = input.GhiChu;
            item.Bonus = input.Bonus;
            item.KhuyenMai = input.KhuyenMai;
            item.Minus = input.Minus;
            item.HocPhi = input.HocPhi;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;
            item.TronGoi = input.TronGoi;
        }

        public void MappingEntityHocPhiTronGoi(UpdateHocPhiTronGoiInputModel input, HocPhiTronGoi item, string loggedEmployee)
        {
            item.HocPhi = input.HocPhi;
            item.FromDate = Convert.ToDateTime(input.FromDate);
            item.ToDate = Convert.ToDateTime(input.ToDate);
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;
            item.GhiChu = input.GhiChu;
        }

        public void MappingEntityThuThach(UpdateThuThachInputModel input, ThuThach item, string loggedEmployee)
        {
            item.Name = input.Name;
            item.KhoaHocId = input.KhoaHocId;
            item.SoCauHoi = input.SoCauHoi;
            item.ThoiGianLamBai = input.ThoiGianLamBai;
            item.MinGrade = input.MinGrade;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;
        }

        //TINH CHI PHI

        public ChiPhiModel ToChiPhiModel(
            GiaoVien nhanVien,
            double salary,
            double teachingRate,
            double tutoringRate,
            double hoaHong,
            double bonus,
            double minus,
            int loaiChiPhi,
            double chiPhiMoi,
            double soGioDay,
            double soGioKem,
            double soHocVien,
            bool daLuu,
            string ngayLamViec,
            int soNgayLam,
            int soNgayLamVoSau,
            double dailySalary,
            double soNgayNghi,
            string ghiChu)
        {
            return new ChiPhiModel
            {
                NhanVienId = nhanVien.GiaoVienId,
                Name = nhanVien.Name,
                Salary_Expense = salary,
                TeachingRate = teachingRate,
                TutoringRate = tutoringRate,
                MucHoaHong = hoaHong,
                Bonus = bonus,
                Minus = minus,
                LoaiChiPhi = loaiChiPhi,
                ChiPhiMoi = chiPhiMoi,
                SoGioDay = soGioDay,
                SoGioKem = soGioKem,
                SoHocVien = soHocVien,
                DaLuu = daLuu,
                NgayLamViec = ngayLamViec,
                SoNgayLam = soNgayLam,
                SoNgayLamVoSau = soNgayLamVoSau,
                DailySalary = dailySalary,
                SoNgayNghi = soNgayNghi,
                GhiChu = ghiChu
            };
        }

        public ChiPhiModel ToChiPhiModel(
            ChiPhiCoDinh chiPhi,
            double chiPhiMoi,
            bool daLuu)
        {
            return new ChiPhiModel
            {
                Name = chiPhi.Name,
                Salary_Expense = chiPhi.Gia,
                Bonus = 0,
                Minus = 0,
                LoaiChiPhi = 3,
                ChiPhiCoDinhId = chiPhi.ChiPhiCoDinhId,
                ChiPhiMoi = chiPhiMoi,
                DaLuu = daLuu
            };
        }
    }
}
