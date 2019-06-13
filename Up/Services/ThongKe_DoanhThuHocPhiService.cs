
namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Data.Entities;
    using Up.Models;

    public class ThongKe_DoanhThuHocPhiService : IThongKe_DoanhThuHocPhiService
    {
        private readonly ApplicationDbContext _context;

        public ThongKe_DoanhThuHocPhiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<ThongKe_DoanhThuHocPhiViewModel>> GetThongKe_DoanhThuHocPhiByLopHoc(Guid LopHocId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ThemThongKe_DoanhThuHocPhiAsync(Guid LopHocId, Guid HocVienId, double HocPhi, DateTime NgayDong, string LoggedEmployee)
        {
            var item = await _context.ThongKe_DoanhThuHocPhis
                .Where(x => x.HocVienId == HocVienId && x.LopHocId == LopHocId && x.NgayDong.Month == NgayDong.Month && x.NgayDong.Year == NgayDong.Year)
                .SingleOrDefaultAsync();

            try
            {
                if (item == null)
                {
                    ThongKe_DoanhThuHocPhi thongKe = new ThongKe_DoanhThuHocPhi
                    {
                        HocVienId = HocVienId,
                        ThongKe_DoanhThuHocPhiId = new Guid(),
                        NgayDong = NgayDong,
                        CreatedBy = LoggedEmployee,
                        CreatedDate = DateTime.Now,
                        HocPhi = HocPhi,
                        LopHocId = LopHocId
                    };
                    _context.ThongKe_DoanhThuHocPhis.Add(thongKe);

                    var no = _context.HocVien_Nos.Where(x => x.HocVienId == HocVienId && x.LopHocId == LopHocId && x.NgayNo <= thongKe.NgayDong);
                    foreach (var n in no)
                    {
                        n.IsDisabled = true;
                    }
                }
                else
                {
                    item.HocPhi = HocPhi;
                    item.UpdatedBy = LoggedEmployee;
                    item.UpdatedDate = DateTime.Now;

                    var no = _context.HocVien_Nos.Where(x => x.HocVienId == HocVienId && x.LopHocId == LopHocId && x.NgayNo <= item.NgayDong);
                    foreach (var n in no)
                    {
                        n.IsDisabled = true;
                    }
                }

                

                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception exeption)
            {
                throw new Exception("Lỗi khi lưu doanh thu : " + exeption.Message);
            }
        }
    }
}
