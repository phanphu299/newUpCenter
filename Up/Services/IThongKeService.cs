namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IThongKeService
    {
        Task<List<ThongKeModel>> GetHocVienGiaoTiepAsync();
        Task<List<ThongKeModel>> GetHocVienThieuNhiAsync();
        Task<List<ThongKeModel>> GetHocVienCCQuocTeAsync();

        Task<List<ThongKeModel>> GetGiaoVienFullTimeAsync();
        Task<List<ThongKeModel>> GetGiaoVienPartTimeAsync();
        //Task<List<GiaoVienViewModel>> GetGiaoVienNuocNgoaiAsync();

        Task<int> GetTongGiaoVienAsync();
        Task<int> GetTongHocVienAsync();
        Task<double> GetTongDoanhThuAsync();
        Task<double> GetTongChiPhiAsync();

        Task<List<ThongKe_DoanhThuHocPhiViewModel>> GetDoanhThuHocPhiAsync();
        Task<List<ThongKe_DoanhThuHocPhiViewModel>> GetDoanhThuHocPhiTronGoiAsync();
        Task<List<ThongKe_ChiPhiViewModel>> GetChiPhiAsync();
        Task<List<NoViewModel>> GetNoAsync();

        Task<List<HocVienOffHon3NgayViewModel>> GetHocVienOffHon3NgayAsync();

        Task<List<HocVienTheoDoiViewModel>> GetHocVienTheoDoiAsync();

        Task<HocVienTheoDoiViewModel> CreateHocVienTheoDoiAsync(Guid hocVienId, string ghiChu, string loggedEmployee);
        
        Task<bool> UpdateHocVienTheoDoiAsync(Guid id, string ghiChu, string loggedEmployee);

        Task<bool> DeleteHocVienTheoDoiAsync(Guid id, string loggedEmployee);
    }
}
