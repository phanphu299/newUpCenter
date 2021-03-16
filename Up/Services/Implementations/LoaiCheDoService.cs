
namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Enums;
    using Up.Models;
    using Up.Repositoties;

    public class LoaiCheDoService : ILoaiCheDoService
    {
        private readonly ILoaiGiaoVienRepository _loaiGiaoVienRepository;
        private readonly ILoaiCheDoRepository _loaiCheDoRepository;

        public LoaiCheDoService(ILoaiGiaoVienRepository loaiGiaoVienRepository, ILoaiCheDoRepository loaiCheDoRepository)
        {
            _loaiGiaoVienRepository = loaiGiaoVienRepository;
            _loaiCheDoRepository = loaiCheDoRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _loaiCheDoRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_CheDoHopTac);

            return canContribute;
        }

        public async Task<LoaiCheDoViewModel> CreateLoaiCheDoAsync(string name, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên Loại Chế Độ không được để trống !!!");

            var result = await _loaiCheDoRepository.CreateLoaiCheDoAsync(name, loggedEmployee);
            return await _loaiCheDoRepository.GetLoaiCheDoDetailAsync(result);
        }

        public async Task<bool> DeleteLoaiCheDoAsync(Guid id, string loggedEmployee)
        {
            var giaoVienIds = await _loaiGiaoVienRepository.GetNhanVienIdsByLoaiCheDoAsync(id);
            if (giaoVienIds.Any())
                throw new Exception("Hãy xóa những nhân viên thuộc loại này trước !!!");

            return await _loaiCheDoRepository.DeleteLoaiCheDoAsync(id, loggedEmployee);
        }

        public async Task<List<LoaiCheDoViewModel>> GetLoaiCheDoAsync()
        {
            return await _loaiCheDoRepository.GetLoaiCheDoAsync();
        }

        public async Task<bool> UpdateLoaiCheDoAsync(Guid id, string name, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên Loại Chế Độ không được để trống !!!");

            return await _loaiCheDoRepository.UpdateLoaiCheDoAsync(id, name, loggedEmployee);
        }
    }
}
