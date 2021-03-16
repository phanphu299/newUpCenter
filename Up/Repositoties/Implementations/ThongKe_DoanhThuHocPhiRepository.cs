using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Up.Converters;
using Up.Data;
using Up.Models;

namespace Up.Repositoties
{
    public class ThongKe_DoanhThuHocPhiRepository : BaseRepository, IThongKe_DoanhThuHocPhiRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public ThongKe_DoanhThuHocPhiRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task AddThongKe_DoanhThuAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee)
        {
            var thongKe = _entityConverter.ToEntityThongKe_DoanhThuHocPhi(input, loggedEmployee);
            await _context.ThongKe_DoanhThuHocPhis.AddAsync(thongKe);

            if (input.DaDong)
                XoaNo(input.HocVienId, thongKe.NgayDong);

            var thongKeList = _entityConverter.ToThongKe_DoanhThuHocPhi_TaiLieuList(thongKe.ThongKe_DoanhThuHocPhiId, input.SachIds, loggedEmployee);
            await _context.ThongKe_DoanhThuHocPhi_TaiLieus.AddRangeAsync(thongKeList);
            await _context.SaveChangesAsync();
        }

        private void XoaNo(Guid hocVienId, DateTime ngayDong)
        {
            var no = _context.HocVien_Nos.Where(x => x.HocVienId == hocVienId && x.NgayNo <= ngayDong);
            foreach (var n in no)
            {
                n.IsDisabled = true;
            }
        }

        public async Task<bool> IsExistingAsync(Guid hocVienId, Guid lopHocId, DateTime ngayDong)
        {
            return await _context.ThongKe_DoanhThuHocPhis
                .AnyAsync(x => x.HocVienId == hocVienId &&
                               x.LopHocId == lopHocId &&
                               x.NgayDong.Month == ngayDong.Month &&
                               x.NgayDong.Year == ngayDong.Year);
        }

        public async Task UpdateThongKe_DoanhThuAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee)
        {
            var item = await _context.ThongKe_DoanhThuHocPhis
                .FirstOrDefaultAsync(x => x.HocVienId == input.HocVienId &&
                               x.LopHocId == input.LopHocId &&
                               x.NgayDong.Month == input.NgayDong.Month &&
                               x.NgayDong.Year == input.NgayDong.Year);

            _entityConverter.MappingEntityThongKe_DoanhThuHocPhi(input, item, loggedEmployee);
            if (input.DaDong != false || input.DaNo != false)
            {
                item.DaDong = input.DaDong;
                item.DaNo = input.DaNo;
            }

            if (input.DaDong)
                XoaNo(input.HocVienId, item.NgayDong);

            var sach = _context.ThongKe_DoanhThuHocPhi_TaiLieus.Where(x => x.ThongKe_DoanhThuHocPhiId == item.ThongKe_DoanhThuHocPhiId);
            _context.ThongKe_DoanhThuHocPhi_TaiLieus.RemoveRange(sach);

            var thongKeList = _entityConverter.ToThongKe_DoanhThuHocPhi_TaiLieuList(item.ThongKe_DoanhThuHocPhiId, input.SachIds, loggedEmployee);
            await _context.ThongKe_DoanhThuHocPhi_TaiLieus.AddRangeAsync(thongKeList);
            await _context.SaveChangesAsync();
        }

        public async Task XoaDaDongThongKe_DoanhThuAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee)
        {
            var doanhThu = await _context.ThongKe_DoanhThuHocPhis
                .FirstOrDefaultAsync(x => x.HocVienId == input.HocVienId &&
                                        x.LopHocId == input.LopHocId &&
                                        x.NgayDong.Month == input.NgayDong.Month &&
                                        x.NgayDong.Year == input.NgayDong.Year);
            doanhThu.DaDong = false;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Undo_DoanhThuAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee)
        {
            var item = await _context.ThongKe_DoanhThuHocPhis
                 .Where(x => x.HocVienId == input.HocVienId &&
                             x.LopHocId == input.LopHocId &&
                             x.NgayDong.Month == input.month &&
                             x.NgayDong.Year == input.year)
                 .SingleOrDefaultAsync();

            if (item != null)
            {
                item.DaDong = false;
                item.DaNo = false;
                item.UpdatedDate = DateTime.Now;
                item.UpdatedBy = loggedEmployee;

                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
}
