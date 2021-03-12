
namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Data.Entities;
    using Up.Models;
    using Up.Repositoties;

    public class ThongKe_DoanhThuHocPhiService : IThongKe_DoanhThuHocPhiService
    {
        private readonly ApplicationDbContext _context;
        private readonly IThongKe_DoanhThuHocPhiRepository _thongkeRepository;

        public ThongKe_DoanhThuHocPhiService(ApplicationDbContext context, IThongKe_DoanhThuHocPhiRepository thongkeRepository)
        {
            _context = context;
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

        public async Task<bool> Undo_DoanhThuAsync(Guid LopHocId, Guid HocVienId, int Month, int Year, string LoggedEmployee)
        {
            try
            {
                var item = await _context.ThongKe_DoanhThuHocPhis
                .Where(x => x.HocVienId == HocVienId && x.LopHocId == LopHocId && x.NgayDong.Month == Month && x.NgayDong.Year == Year)
                .SingleOrDefaultAsync();

                if (item != null)
                {
                    item.DaDong = false;
                    item.DaNo = false;
                    item.UpdatedDate = DateTime.Now;
                    item.UpdatedBy = LoggedEmployee;

                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Lỗi khi undo Học Phí : " + exception.Message);
            }
        }
    }
}
