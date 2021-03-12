using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Converters;
using Up.Data;
using Up.Models;

namespace Up.Repositoties
{
    public class NoRepository : BaseRepository, INoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EntityConverter _entityConverter;
        public NoRepository(
           ApplicationDbContext context,
           EntityConverter entityConverter,
           UserManager<IdentityUser> userManager)
           : base(context, userManager)
        {
            _context = context;
            _entityConverter = entityConverter;
        }

        public async Task AddNoAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee)
        {
            var hocVienNos = await _context.HocVien_Nos
                                .Where(x => x.HocVienId == input.HocVienId)
                                .ToListAsync();

            foreach (var hv in hocVienNos)
            {
                hv.IsDisabled = true;
            }

            var hocVien_No = _entityConverter.ToEntityHocVien_No(input, loggedEmployee);
            await _context.HocVien_Nos.AddAsync(hocVien_No);
            await _context.SaveChangesAsync();
        }

        public async Task<List<NoViewModel>> GetHocVien_No()
        {
            var no = await _context.HocVien_Nos
                .Include(x => x.HocVien)
                .Include(x => x.LopHoc)
                .Where(x => !x.IsDisabled && !x.HocVien.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return no.Select(item => _entityConverter.ToNoViewModel(item)).ToList();
        }

        public async Task<List<NoViewModel>> GetHocVien_NoByLopHoc(Guid lopHocId)
        {
            var no = await _context.HocVien_Nos
                .Include(x => x.HocVien)
                .Include(x => x.LopHoc)
                .Where(x => x.LopHocId == lopHocId && !x.IsDisabled && !x.HocVien.IsDisabled)
                .AsNoTracking()
                .ToListAsync();

            return no.Select(item => _entityConverter.ToNoViewModel(item)).ToList();
        }

        public async Task<bool> IsExistingAsync(Guid lopHocId, Guid hocVienId, DateTime ngayNo)
        {
            return await _context.HocVien_Nos
                .AnyAsync(x => x.HocVienId == hocVienId && 
                                x.LopHocId == lopHocId &&
                                x.NgayNo.Month == ngayNo.Month && 
                                x.NgayNo.Year == ngayNo.Year);
        }

        public async Task UpdateNoAsync(ThongKe_DoanhThuHocPhiInputModel input, string loggedEmployee)
        {
            var item = await _context.HocVien_Nos
                .FirstOrDefaultAsync(x => x.HocVienId == input.HocVienId &&
                                        x.LopHocId == input.LopHocId &&
                                        x.NgayNo.Month == input.NgayDong.Month &&
                                        x.NgayNo.Year == input.NgayDong.Year);

            item.NgayNo = input.NgayDong;
            item.TienNo = input.HocPhi;
            item.IsDisabled = false;
            item.UpdatedBy = loggedEmployee;
            item.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}
