
namespace Up.Services
{
    using System.Threading.Tasks;
    using Up.Models;
    using Up.Repositoties;

    public class ThongKe_DoanhThuHocPhiService : IThongKe_DoanhThuHocPhiService
    {
        private readonly IThongKe_DoanhThuHocPhiRepository _thongkeRepository;

        public ThongKe_DoanhThuHocPhiService(IThongKe_DoanhThuHocPhiRepository thongkeRepository)
        {
            _thongkeRepository = thongkeRepository;
        }

        public async Task<bool> ThemThongKe_DoanhThuHocPhiAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee)
        {
            bool isExisting = await _thongkeRepository.IsExistingAsync(input.HocVienId, input.LopHocId, input.NgayDong);

            if (!isExisting)
                await _thongkeRepository.AddThongKe_DoanhThuAsync(input, loggedEmployee);
            else
                await _thongkeRepository.UpdateThongKe_DoanhThuAsync(input, loggedEmployee);

            return true;
        }

        public async Task<bool> Undo_DoanhThuAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee)
        {
            return await _thongkeRepository.Undo_DoanhThuAsync(input, loggedEmployee);
        }
    }
}
