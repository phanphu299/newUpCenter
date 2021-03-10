namespace Up.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Enums;
    using Up.Models;
    using Up.Repositoties;

    public class ThongKeService : IThongKeService
    {
        private readonly IThongKeRepository _thongKeRepository;

        public ThongKeService(IThongKeRepository thongKeRepository)
        {
            _thongKeRepository = thongKeRepository;
        }

        public async Task<List<ThongKeModel>> GetHocVienGiaoTiepAsync()
        {
            await _thongKeRepository.TinhTongHocVienTheoThangAsync(LoaiHocVienEnums.GiaoTiep, LoaiKhoaHocEnums.GiaoTiep);
            return await _thongKeRepository.GetThongKeHocVienAsync(LoaiHocVienEnums.GiaoTiep);
        }

        public async Task<List<ThongKeModel>> GetHocVienThieuNhiAsync()
        {
            await _thongKeRepository.TinhTongHocVienTheoThangAsync(LoaiHocVienEnums.ThieuNhi, LoaiKhoaHocEnums.ThieuNhi);
            return await _thongKeRepository.GetThongKeHocVienAsync(LoaiHocVienEnums.ThieuNhi);
        }

        public async Task<List<ThongKeModel>> GetHocVienCCQuocTeAsync()
        {
            await _thongKeRepository.TinhTongHocVienQuocTeTheoThangAsync();
            return await _thongKeRepository.GetThongKeHocVienAsync(LoaiHocVienEnums.QuocTe);
        }

        public async Task<List<ThongKeModel>> GetGiaoVienFullTimeAsync()
        {
            await _thongKeRepository.TinhTongGiaoVienTheoThangAsync(LoaiCheDoEnums.FullTime);
            return await _thongKeRepository.GetThongKeGiaoVienAsync(LoaiCheDoEnums.FullTime);
        }

        public async Task<List<ThongKeModel>> GetGiaoVienPartTimeAsync()
        {
            await _thongKeRepository.TinhTongGiaoVienTheoThangAsync(LoaiCheDoEnums.PartTime);
            return await _thongKeRepository.GetThongKeGiaoVienAsync(LoaiCheDoEnums.PartTime);
        }

        //public async Task<List<GiaoVienViewModel>> GetGiaoVienNuocNgoaiAsync()
        //{
        //    try
        //    {
        //        var nuocNgoai = await _context.GiaoViens
        //        .Where(x => x.LoaiGiaoVienId == LoaiGiaoVienEnums.GVNN.ToId() && x.IsDisabled == false)
        //        .Where(x => x.CreatedDate.Year == DateTime.Now.Year)
        //        .OrderBy(x => x.CreatedDate)

        //        .Select(g => new GiaoVienViewModel
        //        {
        //            GiaoVienId = g.GiaoVienId,
        //            CreatedDate_Date = g.CreatedDate
        //        })
        //        .ToListAsync();

        //        return nuocNgoai;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception(exception.Message);
        //    }
        //}

        public async Task<int> GetTongGiaoVienAsync()
        {
            return await _thongKeRepository.GetTongGiaoVienAsync();
        }

        public async Task<int> GetTongHocVienAsync()
        {
            return await _thongKeRepository.GetTongHocVienAsync();
        }

        public async Task<List<ThongKe_DoanhThuHocPhiViewModel>> GetDoanhThuHocPhiAsync()
        {
            return await _thongKeRepository.GetDoanhThuHocPhiAsync();
        }

        public async Task<List<NoViewModel>> GetNoAsync()
        {
            return await _thongKeRepository.GetNoAsync();
        }

        public async Task<double> GetTongDoanhThuAsync()
        {
            return await _thongKeRepository.GetTongDoanhThuAsync();
        }

        public async Task<List<ThongKe_ChiPhiViewModel>> GetChiPhiAsync()
        {
            return await _thongKeRepository.GetChiPhiAsync();
        }

        public async Task<double> GetTongChiPhiAsync()
        {
            return await _thongKeRepository.GetTongChiPhiAsync();
        }

        public async Task<List<HocVienOffHon3NgayViewModel>> GetHocVienOffHon3NgayAsync()
        {
            return await _thongKeRepository.GetHocVienOffHon3NgayAsync();
        }

        public async Task<List<ThongKe_DoanhThuHocPhiViewModel>> GetDoanhThuHocPhiTronGoiAsync()
        {
            return await _thongKeRepository.GetDoanhThuHocPhiTronGoiAsync();
        }
    }
}
