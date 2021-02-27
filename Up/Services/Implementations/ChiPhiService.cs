namespace Up.Services
{
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

        public async Task<TinhChiPhiViewModel> TinhChiPhiAsync(int month, int year)
        {
            return await _chiPhiRepository.TinhChiPhiAsync(month, year);
        }
    }
}
