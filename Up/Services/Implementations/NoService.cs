
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;
    using Up.Repositoties;

    public class NoService : INoService
    {
        private readonly INoRepository _noRepository;
        private readonly IThongKe_DoanhThuHocPhiRepository _thongKeRepository;

        public NoService(
            INoRepository noRepository,
            IThongKe_DoanhThuHocPhiRepository thongKeRepository)
        {
            _noRepository = noRepository;
            _thongKeRepository = thongKeRepository;
        }

        public async Task<List<NoViewModel>> GetHocVien_No()
        {
            return await _noRepository.GetHocVien_No();
        }

        public async Task<List<NoViewModel>> GetHocVien_NoByLopHoc(Guid lopHocId)
        {
            return await _noRepository.GetHocVien_NoByLopHoc(lopHocId);
        }

        public async Task<bool> ThemHocVien_NoAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee)
        {
            if (input.LopHocId == null || input.HocVienId == null || input.NgayDong == null)
                throw new Exception("Tên Lớp Học, Học Viên, Ngày Nợ không được để trống !!!");

            var isExisting = await _noRepository.IsExistingAsync(input.LopHocId, input.HocVienId, input.NgayDong);

            if (!isExisting)
                await _noRepository.AddNoAsync(input, loggedEmployee);
            else
                await _noRepository.UpdateNoAsync(input, loggedEmployee);

            await _thongKeRepository.XoaDaDongThongKe_DoanhThuAsync(input, loggedEmployee);
            return true;
        }

        public async Task<bool> Undo_NoAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee)
        {
            return await _noRepository.Undo_NoAsync(input, loggedEmployee);
        }
    }
}
