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
    public class NgayHocService: INgayHocService
    {
        private readonly ApplicationDbContext _context;

        public NgayHocService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<NgayHocViewModel> CreateNgayHocAsync(string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                return null;

            NgayHoc ngayHoc = new NgayHoc();
            ngayHoc.NgayHocId = new Guid();
            ngayHoc.Name = Name;
            ngayHoc.CreatedBy = LoggedEmployee;
            ngayHoc.CreatedDate = DateTime.Now;

            _context.NgayHocs.Add(ngayHoc);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                return null;
            return new NgayHocViewModel { NgayHocId = ngayHoc.NgayHocId, Name = ngayHoc.Name, CreatedBy = ngayHoc.CreatedBy, CreatedDate = ngayHoc.CreatedDate.ToString("dd/MM/yyyy") };
        }

        public async Task<bool> DeleteNgayHocAsync(Guid NgayHocId, string LoggedEmployee)
        {
            var item = await _context.NgayHocs
                                    .Where(x => x.NgayHocId == NgayHocId)
                                    .SingleOrDefaultAsync();

            if (item == null) return false;

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<NgayHocViewModel>> GetNgayHocAsync()
        {
            return await _context.NgayHocs
                .Where(x => x.IsDisabled == false)
                .Select(x => new NgayHocViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    NgayHocId = x.NgayHocId,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateNgayHocAsync(Guid NgayHocId, string Name, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                return false;

            var item = await _context.NgayHocs
                                    .Where(x => x.NgayHocId == NgayHocId)
                                    .SingleOrDefaultAsync();

            if (item == null) return false;

            item.Name = Name;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
