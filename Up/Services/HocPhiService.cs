using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Up.Data;
using Up.Data.Entities;
using Up.Models;

namespace Up.Services
{
    public class HocPhiService : IHocPhiService
    {
        private readonly ApplicationDbContext _context;

        public HocPhiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HocPhiViewModel> CreateHocPhiAsync(double Gia, string LoggedEmployee)
        {
            HocPhi hocPhi = new HocPhi();
            hocPhi.HocPhiId = new Guid();
            hocPhi.Gia = Gia;
            hocPhi.CreatedBy = LoggedEmployee;
            hocPhi.CreatedDate = DateTime.Now;

            _context.HocPhis.Add(hocPhi);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Học Phí !!!");
            return new HocPhiViewModel { HocPhiId = hocPhi.HocPhiId, Gia = hocPhi.Gia, CreatedBy = hocPhi.CreatedBy, CreatedDate = hocPhi.CreatedDate.ToString("dd/MM/yyyy") };
        }

        public async Task<bool> DeleteHocPhiAsync(Guid HocPhiId, string LoggedEmployee)
        {
            var lopHoc = await _context.LopHocs.Where(x => x.HocPhiId == HocPhiId).ToListAsync();
            if (lopHoc.Any())
                throw new Exception("Hãy xóa những lớp học có học phí này trước !!!");

            var item = await _context.HocPhis
                                    .Where(x => x.HocPhiId == HocPhiId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Học Phí !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<HocPhiViewModel>> GetHocPhiAsync()
        {
            return await _context.HocPhis
                .Where(x => x.IsDisabled == false)
                .Select(x => new HocPhiViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    HocPhiId = x.HocPhiId,
                    Gia = x.Gia,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateHocPhiAsync(Guid HocPhiId, double Gia, string LoggedEmployee)
        {
            var item = await _context.HocPhis
                                    .Where(x => x.HocPhiId == HocPhiId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Học Phí !!!");

            item.Gia = Gia;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
