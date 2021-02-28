namespace Up.Services
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Up.Enums;
    using Up.Models;
    using Up.Repositoties;

    public class ChiPhiService : IChiPhiService
    {
        private readonly IChiPhiRepository _chiPhiRepository;

        public ChiPhiService(IChiPhiRepository chiPhiRepository)
        {
            _chiPhiRepository = chiPhiRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            bool canContribute = await _chiPhiRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_TinhLuong);
            return canContribute;
        }

        public async Task<bool> ThemThongKe_ChiPhiAsync(ThongKe_ChiPhiViewModel[] model, DateTime ngayChiPhi, string loggedEmployee)
        {
            return await _chiPhiRepository.ThemThongKe_ChiPhiAsync(model, ngayChiPhi, loggedEmployee);
        }

        public async Task<TinhChiPhiViewModel> TinhChiPhiAsync(int month, int year)
        {
            return await _chiPhiRepository.TinhChiPhiAsync(month, year);
        }
    }
}
