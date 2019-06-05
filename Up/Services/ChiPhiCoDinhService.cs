
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

    public class ChiPhiCoDinhService : IChiPhiCoDinhService
    {
        private readonly ApplicationDbContext _context;

        public ChiPhiCoDinhService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChiPhiCoDinhViewModel> CreateChiPhiCoDinhAsync(double Gia, string Name, string LoggedEmployee)
        {
            ChiPhiCoDinh chiPhi = new ChiPhiCoDinh();
            chiPhi.ChiPhiCoDinhId = new Guid();
            chiPhi.Gia = Gia;
            chiPhi.Name = Name;
            chiPhi.CreatedBy = LoggedEmployee;
            chiPhi.CreatedDate = DateTime.Now;

            _context.ChiPhiCoDinhs.Add(chiPhi);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Chi Phí !!!");
            return new ChiPhiCoDinhViewModel
            {
                ChiPhiCoDinhId = chiPhi.ChiPhiCoDinhId,
                Gia = chiPhi.Gia,
                Name = chiPhi.Name,
                CreatedBy = chiPhi.CreatedBy,
                CreatedDate = chiPhi.CreatedDate.ToString("dd/MM/yyyy")
            };
        }

        public async Task<bool> DeleteChiPhiCoDinhAsync(Guid ChiPhiCoDinhId, string LoggedEmployee)
        {
            var item = await _context.ChiPhiCoDinhs
                                    .Where(x => x.ChiPhiCoDinhId == ChiPhiCoDinhId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Chi Phí !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<ChiPhiCoDinhViewModel>> GetChiPhiCoDinhAsync()
        {
            return await _context.ChiPhiCoDinhs
                .Where(x => x.IsDisabled == false)
                .Select(x => new ChiPhiCoDinhViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    ChiPhiCoDinhId = x.ChiPhiCoDinhId,
                    Gia = x.Gia,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<ChiPhiCoDinhViewModel> UpdateChiPhiCoDinhAsync(Guid ChiPhiCoDinhId, double Gia, string Name, string LoggedEmployee)
        {
            var item = await _context.ChiPhiCoDinhs
                                    .Where(x => x.ChiPhiCoDinhId == ChiPhiCoDinhId)
                                    .SingleOrDefaultAsync();

            if (item == null)
                throw new Exception("Không tìm thấy Học Phí !!!");

            item.Gia = Gia;
            item.Name = Name;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return new ChiPhiCoDinhViewModel
            {
                Name = item.Name,
                CreatedBy = item.CreatedBy,
                Gia = item.Gia,
                ChiPhiCoDinhId = item.ChiPhiCoDinhId,
                UpdatedBy = item.UpdatedBy,
                CreatedDate = item.CreatedDate != null ? ((DateTime)item.CreatedDate).ToString("dd/MM/yyyy") : "",
                UpdatedDate = item.UpdatedDate != null ? ((DateTime)item.UpdatedDate).ToString("dd/MM/yyyy") : "",
            };
        }
    }
}
