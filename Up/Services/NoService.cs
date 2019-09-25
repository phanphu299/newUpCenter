
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

    public class NoService : INoService
    {
        private readonly ApplicationDbContext _context;

        public NoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<NoViewModel>> GetHocVien_NoByLopHoc(Guid LopHocId)
        {
            return await _context.HocVien_Nos
                .Where(x => x.LopHocId == LopHocId && x.IsDisabled == false)
                .Select(x => new NoViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    LopHocId = x.LopHocId,
                    HocVienId = x.HocVienId,
                    LopHoc = x.LopHoc.Name,
                    NgayNo = x.NgayNo.ToString("dd/MM/yyyy"),
                    TienNo = x.TienNo,
                    HocVien = x.HocVien.FullName
                })
                .ToListAsync();
        }

        public async Task<bool> ThemHocVien_NoAsync(Guid LopHocId, Guid HocVienId, double TienNo, DateTime NgayNo, string LoggedEmployee)
        {
            if (LopHocId == null || HocVienId == null || NgayNo == null)
                throw new Exception("Tên Lớp Học, Học Viên, Ngày Nợ không được để trống !!!");

            var item = await _context.HocVien_Nos
                .Where(x => x.HocVienId == HocVienId && x.LopHocId == LopHocId && x.NgayNo.Month == NgayNo.Month && x.NgayNo.Year == NgayNo.Year)
                .SingleOrDefaultAsync();

            try
            {
                if (item == null)
                {
                    HocVien_No hocVien_No = new HocVien_No
                    {
                        HocVienId = HocVienId,
                        HocVien_NoId = new Guid(),
                        CreatedBy = LoggedEmployee,
                        CreatedDate = DateTime.Now,
                        TienNo = TienNo,
                        LopHocId = LopHocId,
                        NgayNo = NgayNo
                    };
                    await _context.HocVien_Nos.AddAsync(hocVien_No);
                }
                else
                {
                    item.NgayNo = NgayNo;
                    item.TienNo = TienNo;
                    item.IsDisabled = false;
                    item.UpdatedBy = LoggedEmployee;
                    item.UpdatedDate = DateTime.Now;
                }

                var doanhThu = await _context.ThongKe_DoanhThuHocPhis.FirstOrDefaultAsync(x => x.HocVienId == HocVienId && x.LopHocId == LopHocId && x.NgayDong.Month == NgayNo.Month && x.NgayDong.Year == NgayNo.Year);
                doanhThu.DaDong = false;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exeption)
            {
                throw new Exception("Lỗi khi lưu nợ : " + exeption.Message);
            }
        }

        public async Task<bool> Undo_NoAsync(Guid LopHocId, Guid HocVienId, int Month, int Year, string LoggedEmployee)
        {
            try
            {

                var item = await _context.HocVien_Nos
                                    .Where(x => x.LopHocId == LopHocId && x.HocVienId == HocVienId)
                                    .Where(x => x.NgayNo.Month == Month && x.NgayNo.Year == Year)
                                    .SingleOrDefaultAsync();

                if(item != null)
                {
                    _context.HocVien_Nos.Remove(item);

                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Lỗi khi undo Nợ : " + exception.Message);
            }
        }

        public async Task<bool> XoaHocVien_NoAsync(Guid LopHocId, Guid HocVienId, DateTime NgayNo, string LoggedEmployee)
        {
            try
            {
                var month = 0;
                var year = 0;
                if (NgayNo.Month == 1)
                {
                    month = 12;
                    year = NgayNo.Year - 1;
                }
                else
                {
                    month = NgayNo.Month - 1;
                }
                var item = await _context.HocVien_Nos
                                    .Where(x => x.LopHocId == LopHocId && x.HocVienId == HocVienId)
                                    .Where(x => x.NgayNo.Month == month && x.NgayNo.Year == year)
                                    .SingleOrDefaultAsync();

                if (item == null)
                    throw new Exception("Không tìm thấy Học viên nợ !!!");

                item.IsDisabled = true;
                item.UpdatedBy = LoggedEmployee;
                item.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Lỗi khi xóa : " + exception.Message);
            }
        }
    }
}
