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

    public class GiaoVienService : IGiaoVienService
    {
        private readonly ApplicationDbContext _context;

        public GiaoVienService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GiaoVienViewModel> CreateGiaoVienAsync(string Name, Guid LoaiGiaoVienId, string Phone, double TeachingRate, double TutoringRate, double BasicSalary, string FacebookAccount, string DiaChi, string InitialName, string CMND, string LoggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(Name) || LoaiGiaoVienId == null || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(FacebookAccount) || string.IsNullOrWhiteSpace(DiaChi) || string.IsNullOrWhiteSpace(InitialName) || string.IsNullOrWhiteSpace(CMND))
                throw new Exception("Tên Giáo Viên, Loại Giáo Viên, SĐT, FB, Địa Chỉ, Initial Name, CMND không được để trống !!!");

            GiaoVien giaoVien = new GiaoVien();
            giaoVien.GiaoVienId = new Guid();
            giaoVien.Name = Name;
            giaoVien.Phone = Phone;
            giaoVien.LoaiGiaoVienId = LoaiGiaoVienId;
            giaoVien.FacebookAccount = FacebookAccount;
            giaoVien.DiaChi = DiaChi;
            giaoVien.InitialName = InitialName;
            giaoVien.CMND = CMND;
            giaoVien.TeachingRate = TeachingRate;
            giaoVien.TutoringRate = TutoringRate;
            giaoVien.BasicSalary = BasicSalary;
            giaoVien.CreatedBy = LoggedEmployee;
            giaoVien.CreatedDate = DateTime.Now;

            _context.GiaoViens.Add(giaoVien);

            var saveResult = await _context.SaveChangesAsync();
            if (saveResult != 1)
                throw new Exception("Lỗi khi lưu Giáo Viên !!!");
            return new GiaoVienViewModel {
                GiaoVienId = giaoVien.GiaoVienId,
                Name = giaoVien.Name,
                InitialName = giaoVien.InitialName,
                BasicSalary = giaoVien.BasicSalary,
                TutoringRate = giaoVien.TutoringRate,
                TeachingRate = giaoVien.TeachingRate,
                CMND = giaoVien.CMND,
                DiaChi = giaoVien.DiaChi,
                FacebookAccount = giaoVien.FacebookAccount,
                Phone = giaoVien.Phone,
                LoaiGiaoVienId = giaoVien.LoaiGiaoVienId,
                LoaiGiaoVien = _context.LoaiGiaoViens.FindAsync(giaoVien.LoaiGiaoVienId).Result.Name,
                CreatedBy = giaoVien.CreatedBy,
                CreatedDate = giaoVien.CreatedDate.ToString("dd/MM/yyyy") };
        }

        public async Task<bool> DeleteGiaoVienAsync(Guid GiaoVienId, string LoggedEmployee)
        {
            var lopHoc = await _context.LopHocs.Where(x => x.GiaoVienId == GiaoVienId).ToListAsync();
            if (lopHoc.Any())
                throw new Exception("Hãy xóa những lớp học của giáo viên này trước !!!");

            var item = await _context.GiaoViens
                                    .FindAsync(GiaoVienId);

            if (item == null)
                throw new Exception("Không tìm thấy Giáo Viên !!!");

            item.IsDisabled = true;
            item.UpdatedBy = LoggedEmployee;
            item.UpdatedDate = DateTime.Now;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<List<GiaoVienViewModel>> GetGiaoVienAsync()
        {
            return await _context.GiaoViens
                .Where(x => x.IsDisabled == false)
                .Select(x => new GiaoVienViewModel
                {
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                    GiaoVienId = x.GiaoVienId,
                    Phone = x.Phone,
                    FacebookAccount = x.FacebookAccount,
                    DiaChi = x.DiaChi,
                    CMND = x.CMND,
                    TeachingRate = x.TeachingRate,
                    TutoringRate = x.TutoringRate,
                    BasicSalary = x.BasicSalary,
                    InitialName = x.InitialName,
                    LoaiGiaoVienId = x.LoaiGiaoVienId,
                    LoaiGiaoVien = x.LoaiGiaoVien.Name,
                    Name = x.Name,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate != null ? ((DateTime)x.UpdatedDate).ToString("dd/MM/yyyy") : ""
                })
                .ToListAsync();
        }

        public async Task<GiaoVienViewModel> UpdateGiaoVienAsync(Guid GiaoVienId, string Name, Guid LoaiGiaoVienId, string Phone, double TeachingRate, double TutoringRate, double BasicSalary, string FacebookAccount, string DiaChi, string InitialName, string CMND, string LoggedEmployee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name) || LoaiGiaoVienId == null || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(FacebookAccount) || string.IsNullOrWhiteSpace(DiaChi) || string.IsNullOrWhiteSpace(InitialName) || string.IsNullOrWhiteSpace(CMND))
                    throw new Exception("Tên Giáo Viên, Loại Giáo Viên, SĐT, FB, Địa Chỉ, Initial Name, CMND không được để trống !!!");

                var item = await _context.GiaoViens
                                        .Where(x => x.GiaoVienId == GiaoVienId)
                                        .SingleOrDefaultAsync();

                if (item == null)
                    throw new Exception("Không tìm thấy Học Viên!!!");

                item.Name = Name;
                item.Phone = Phone;
                item.FacebookAccount = FacebookAccount;
                item.DiaChi = DiaChi;
                item.InitialName = InitialName;
                item.CMND = CMND;
                item.GiaoVienId = GiaoVienId;
                item.TeachingRate = TeachingRate;
                item.TutoringRate = TutoringRate;
                item.BasicSalary = BasicSalary;
                item.UpdatedBy = LoggedEmployee;
                item.LoaiGiaoVienId = LoaiGiaoVienId;
                item.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                return new GiaoVienViewModel
                {
                    GiaoVienId = item.GiaoVienId,
                    BasicSalary = item.BasicSalary,
                    CMND = item.CMND,
                    DiaChi = item.DiaChi,
                    InitialName = item.InitialName,
                    Name = item.Name,
                    TeachingRate = item.TeachingRate,
                    TutoringRate = item.TutoringRate,
                    FacebookAccount = item.FacebookAccount,
                    LoaiGiaoVienId = item.LoaiGiaoVienId,
                    LoaiGiaoVien = _context.LoaiGiaoViens.FindAsync(item.LoaiGiaoVienId).Result.Name,
                    Phone = item.Phone,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate?.ToString("dd/MM/yyyy"),
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate.ToString("dd/MM/yyyy"),
                };
            }
            catch (Exception exception)
            {
                throw new Exception("Cập nhật lỗi: " + exception.Message);
            }
        }
    }
}
