namespace Up.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Up.Models;

    public interface IChiPhiCoDinhService
    {
        Task<List<ChiPhiCoDinhViewModel>> GetChiPhiCoDinhAsync();
        Task<ChiPhiCoDinhViewModel> CreateChiPhiCoDinhAsync(double Gia, string Name, string LoggedEmployee);
        Task<ChiPhiCoDinhViewModel> UpdateChiPhiCoDinhAsync(Guid ChiPhiCoDinhId, double Gia, string Name, string LoggedEmployee);
        Task<bool> DeleteChiPhiCoDinhAsync(Guid ChiPhiCoDinhId, string LoggedEmployee);
    }
}
