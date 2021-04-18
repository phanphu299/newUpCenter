using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Up.Enums;
using Up.Models;

namespace Up.Repositoties
{
    public interface IThongKeRepository
    {
        Task TinhTongHocVienTheoThangAsync(LoaiHocVienEnums loaiHocVien, LoaiKhoaHocEnums loaiKhoaHoc);

        Task TinhTongHocVienQuocTeTheoThangAsync();

        Task<List<ThongKeModel>> GetThongKeHocVienAsync(LoaiHocVienEnums loaiHocVien);

        Task TinhTongGiaoVienTheoThangAsync(LoaiCheDoEnums loaiCheDo);

        Task<List<ThongKeModel>> GetThongKeGiaoVienAsync(LoaiCheDoEnums loaiCheDo);

        Task<int> GetTongGiaoVienAsync();

        Task<int> GetTongHocVienAsync();

        Task<List<ThongKe_DoanhThuHocPhiViewModel>> GetDoanhThuHocPhiAsync();

        Task<List<ThongKe_ChiPhiViewModel>> GetChiPhiAsync();

        Task<double> GetTongDoanhThuAsync();

        Task<double> GetTongChiPhiAsync();

        Task<List<NoViewModel>> GetNoAsync();

        Task<List<HocVienOffHon3NgayViewModel>> GetHocVienOffHon3NgayAsync();

        Task<List<ThongKe_DoanhThuHocPhiViewModel>> GetDoanhThuHocPhiTronGoiAsync();

        Task<List<HocVienTheoDoiViewModel>> GetHocVienTheoDoiAsync();

        Task<HocVienTheoDoiViewModel> GetHocVienTheoDoiDetailAsync(Guid id);

        Task<Guid> CreateHocVienTheoDoiAsync(Guid hocVienId, string ghiChu, string loggedEmployee);

        Task<bool> UpdateHocVienTheoDoiAsync(Guid id, string ghiChu, string loggedEmployee);

        Task<bool> DeleteHocVienTheoDoiAsync(Guid id, string loggedEmployee);
    }
}
