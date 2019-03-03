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
    public class SachService : ISachService
    {
        private readonly ApplicationDbContext _context;

        public SachService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SachViewModel> CreateSachAsync(string Name, double Gia, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                return null;

            Sach sach = new Sach();
            sach.SachId = new Guid();
            sach.Name = Name;
            sach.Gia = Gia;
            sach.CreatedBy = LoggedEmployee;
            sach.CreatedDate = DateTime.Now;

            _context.Sachs.Add(sach);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                return null;
            return new SachViewModel { SachId = sach.SachId, Name = sach.Name, Gia = sach.Gia, CreatedBy = sach.CreatedBy, CreatedDate = sach.CreatedDate.ToString("dd/MM/yyyy") };
        }

        public async Task<bool> DeleteSachAsync(Guid SachId, string LoggedEmployee)
        {
            var item = await _context.Sachs
                                    .Where(x => x.SachId == SachId)
                                    .SingleOrDefaultAsync();

            if (item == null) return false;

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<SachViewModel>> GetSachAsync()
        {
            return await _context.Sachs
                .Where(x => x.IsDisabled == false)
                .Select(x => new SachViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    SachId = x.SachId,
                    Name = x.Name,
                    Gia = x.Gia,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateSachAsync(Guid SachId, string Name, double Gia, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name))
                return false;

            var item = await _context.Sachs
                                    .Where(x => x.SachId == SachId)
                                    .SingleOrDefaultAsync();

            if (item == null) return false;

            item.Name = Name;
            item.Gia = Gia;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
