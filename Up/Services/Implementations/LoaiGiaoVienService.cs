namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Data;
    using Up.Enums;
    using Up.Models;
    using Up.Repositoties;

    public class LoaiGiaoVienService : ILoaiGiaoVienService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoaiGiaoVienRepository _loaiGiaoVienRepository;

        public LoaiGiaoVienService(ApplicationDbContext context, ILoaiGiaoVienRepository loaiGiaoVienRepository)
        {
            _context = context;
            _loaiGiaoVienRepository = loaiGiaoVienRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _loaiGiaoVienRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_ViTriCongViec);
            return canContribute;
        }

        public async Task<LoaiGiaoVienViewModel> CreateLoaiGiaoVienAsync(CreateLoaiGiaoVienInputModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
                throw new Exception("Tên Loại Nhân Viên không được để trống !!!");

            var result = await _loaiGiaoVienRepository.CreateLoaiGiaoVienAsync(input, loggedEmployee);
            return await _loaiGiaoVienRepository.GetLoaiGiaoVienDetailAsync(result);
        }

        public async Task<bool> DeleteLoaiGiaoVienAsync(Guid id, string loggedEmployee)
        {
            var giaoVienIds = await _loaiGiaoVienRepository.GetNhanVienIdsAsync(id);
            if (giaoVienIds.Any())
                throw new Exception("Hãy xóa những nhân viên thuộc loại này trước !!!");

            return await _loaiGiaoVienRepository.DeleteLoaiGiaoVienAsync(id, loggedEmployee);
        }

        public async Task<List<LoaiGiaoVienViewModel>> GetLoaiGiaoVienAsync()
        {
            return await _loaiGiaoVienRepository.GetLoaiGiaoVienAsync();
        }

        public async Task<bool> UpdateLoaiGiaoVienAsync(UpdateLoaiGiaoVienInputModel input, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
                throw new Exception("Tên Loại Nhân Viên không được để trống !!!");

            return await _loaiGiaoVienRepository.UpdateLoaiGiaoVienAsync(input, loggedEmployee);
        }
    }
}
