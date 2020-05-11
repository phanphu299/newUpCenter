namespace Up.Services
{
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
        Task<List<ThongKe_ChiPhiViewModel>> GetChiPhiAsync();
        Task<List<NoViewModel>> GetNoAsync();

        Task<List<HocVienOffHon3NgayViewModel>> GetHocVienOffHon3NgayAsync();
    }
}
