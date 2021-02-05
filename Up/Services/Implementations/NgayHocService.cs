namespace Up.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Enums;
    using Up.Models;
    using Up.Repositoties;

    public class NgayHocService : INgayHocService
    {
        private readonly ApplicationDbContext _context;
        private readonly INgayHocRepository _ngayHocRepository;

        public NgayHocService(ApplicationDbContext context, INgayHocRepository ngayHocRepository)
        {
            _ngayHocRepository = ngayHocRepository;
            _context = context;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            return await _ngayHocRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_NgayHoc);
        }

        public async Task<NgayHocViewModel> CreateNgayHocAsync(string name, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên Ngày Học không được để trống !!!");

            var result = await _ngayHocRepository.CreateNgayHocAsync(name, loggedEmployee);
            return await _ngayHocRepository.GetNgayHocDetailAsync(result);
        }

        public async Task<bool> CreateUpdateHocVien_NgayHocAsync(HocVien_NgayHocInputModel input, string loggedEmployee)
        {
            if (input.NgayKetThucDate < input.NgayBatDauDate)
                throw new Exception("Ngày kết thúc phải lớn hơn Ngày bắt đầu !!!");

            return await _ngayHocRepository.CreateUpdateHocVien_NgayHocAsync(input, loggedEmployee);
        }

        public async Task<bool> DeleteNgayHocAsync(Guid ngayHocId, string loggedEmployee)
        {
            // TODO
            var lopHoc = await _context.LopHocs.Where(x => x.NgayHocId == ngayHocId).ToListAsync();
            if (lopHoc.Any())
                throw new Exception("Hãy xóa những lớp học thuộc ngày học này trước !!!");

            return await _ngayHocRepository.DeleteNgayHocAsync(ngayHocId, loggedEmployee);
        }

        public async Task<HocVien_NgayHocViewModel> GetHocVien_NgayHocByHocVienAsync(Guid hocVienId, Guid lopHocId)
        {
            if (hocVienId == null)
                throw new Exception("Không tìm thấy Học Viên!");

            if (lopHocId == null)
                throw new Exception("Không tìm thấy Lớp Học!");

            return await _ngayHocRepository.GetHocVien_NgayHocByHocVienAsync(hocVienId, lopHocId);
        }

        public async Task<List<NgayHocViewModel>> GetNgayHocAsync()
        {
            return await _ngayHocRepository.GetNgayHocAsync();
        }

        public async Task<bool> UpdateNgayHocAsync(NgayHocViewModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
                throw new Exception("Tên Ngày Học không được để trống !!!");

            return await _ngayHocRepository.UpdateNgayHocAsync(input, loggedEmployee);
        }
    }
}
