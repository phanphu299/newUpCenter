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

    public class QuanHeService : IQuanHeService
    {
        private readonly IHocVienRepository _hocVienRepository;
        private readonly IQuanHeRepository _quanHeRepository;

        public QuanHeService(IHocVienRepository hocVienRepository, IQuanHeRepository quanHeRepository)
        {
            _hocVienRepository = hocVienRepository;
            _quanHeRepository = quanHeRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _quanHeRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_QuanHe);

            return canContribute;
        }

        public async Task<QuanHeViewModel> CreateQuanHeAsync(string name, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên Quan Hệ không được để trống !!!");

            var result = await _quanHeRepository.CreateQuanHeAsync(name, loggedEmployee);
            return await _quanHeRepository.GetQuanHeDetailAsync(result);
        }

        public async Task<bool> DeleteQuanHeAsync(Guid id, string loggedEmployee)
        {
            var hocVienIds = await _hocVienRepository.GetHocVienIdByQuanHeAsync(id);
            if (hocVienIds.Any())
                throw new Exception("Hãy xóa những học viên có quan hệ này trước !!!");

            return await _quanHeRepository.DeleteQuanHeAsync(id, loggedEmployee);
        }

        public async Task<List<QuanHeViewModel>> GetQuanHeAsync()
        {
            return await _quanHeRepository.GetQuanHeAsync();
        }

        public async Task<bool> UpdateQuanHeAsync(Guid id, string name, string loggedEmployee)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên Quan Hệ không được để trống !!!");

            return await _quanHeRepository.UpdateQuanHeAsync(id, name, loggedEmployee);
        }
    }
}
