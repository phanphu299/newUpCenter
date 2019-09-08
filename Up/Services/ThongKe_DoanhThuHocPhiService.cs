
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

        public async Task<bool> ThemThongKe_DoanhThuHocPhiAsync(Guid LopHocId, Guid HocVienId, double HocPhi, DateTime NgayDong,
            double Bonus, double Minus, int KhuyenMai, string GhiChu, Guid[] SachIds, bool DaDong, string LoggedEmployee)
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
                        LopHocId = LopHocId,
                        Bonus = Bonus,
                        KhuyenMai = KhuyenMai,
                        Minus = Minus,
                        GhiChu = GhiChu,
                        DaDong = DaDong
                    };
                    await _context.ThongKe_DoanhThuHocPhis.AddAsync(thongKe);

                    if(DaDong == true)
                    {
                        var no = _context.HocVien_Nos.Where(x => x.HocVienId == HocVienId && x.LopHocId == LopHocId && x.NgayNo <= thongKe.NgayDong);
                        foreach (var n in no)
                        {
                            n.IsDisabled = true;
                        }
                    }

                    foreach (Guid sachId in SachIds)
                    {
                        ThongKe_DoanhThuHocPhi_TaiLieu thongKe_TaiLieu = new ThongKe_DoanhThuHocPhi_TaiLieu
                        {
                            ThongKe_DoanhThuHocPhi_TaiLieuId = new Guid(),
                            ThongKe_DoanhThuHocPhiId = thongKe.ThongKe_DoanhThuHocPhiId,
                            CreatedBy = LoggedEmployee,
                            CreatedDate = DateTime.Now,
                            SachId = sachId
                        };
                        await _context.ThongKe_DoanhThuHocPhi_TaiLieus.AddAsync(thongKe_TaiLieu);
                    }
                }
                else
                {
                    item.GhiChu = GhiChu;
                    item.Bonus = Bonus;
                    item.KhuyenMai = KhuyenMai;
                    item.Minus = Minus;
                    item.HocPhi = HocPhi;
                    item.UpdatedBy = LoggedEmployee;
                    item.UpdatedDate = DateTime.Now;
                    item.DaDong = DaDong;

                    if(DaDong == true)
                    {
                        var no = _context.HocVien_Nos.Where(x => x.HocVienId == HocVienId && x.LopHocId == LopHocId && x.NgayNo <= item.NgayDong).ToList();
                        foreach (var n in no)
                        {
                            n.IsDisabled = true;
                        }
                    }

                    var sach = _context.ThongKe_DoanhThuHocPhi_TaiLieus.Where(x => x.ThongKe_DoanhThuHocPhiId == item.ThongKe_DoanhThuHocPhiId);
                    _context.ThongKe_DoanhThuHocPhi_TaiLieus.RemoveRange(sach);

                    foreach (Guid sachId in SachIds)
                    {
                        ThongKe_DoanhThuHocPhi_TaiLieu thongKe_TaiLieu = new ThongKe_DoanhThuHocPhi_TaiLieu
                        {
                            ThongKe_DoanhThuHocPhi_TaiLieuId = new Guid(),
                            ThongKe_DoanhThuHocPhiId = item.ThongKe_DoanhThuHocPhiId,
                            CreatedBy = LoggedEmployee,
                            CreatedDate = DateTime.Now,
                            SachId = sachId
                        };
                        await _context.ThongKe_DoanhThuHocPhi_TaiLieus.AddAsync(thongKe_TaiLieu);
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exeption)
            {
                throw new Exception("Lỗi khi lưu doanh thu : " + exeption.Message);
            }
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
