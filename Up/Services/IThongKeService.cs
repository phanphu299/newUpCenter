namespace Up.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IThongKeService
    {
        Task<List<HocVienViewModel>> GetHocVienGiaoTiepAsync();
        Task<List<HocVienViewModel>> GetHocVienThieuNhiAsync();
        Task<List<HocVienViewModel>> GetHocVienCCQuocTeAsync();

        Task<List<GiaoVienViewModel>> GetGiaoVienFullTimeAsync();
        Task<List<GiaoVienViewModel>> GetGiaoVienPartTimeAsync();
        //Task<List<GiaoVienViewModel>> GetGiaoVienNuocNgoaiAsync();

        Task<int> GetTongGiaoVienAsync();
        Task<int> GetTongHocVienAsync();
        Task<double> GetTongDoanhThuAsync();
        Task<double> GetTongChiPhiAsync();

        Task<List<ThongKe_DoanhThuHocPhiViewModel>> GetDoanhThuHocPhiAsync();
        Task<List<ThongKe_ChiPhiViewModel>> GetChiPhiAsync();
        Task<List<NoViewModel>> GetNoAsync();
    }
}
