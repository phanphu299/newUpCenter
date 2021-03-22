using System;
using System.Threading.Tasks;
using Up.Models;
using Up.Repositoties;

namespace Up.Services
{
    public class BienLaiService : IBienLaiService
    {
        private readonly IBienLaiRepository _bienLaiRepository;

        public BienLaiService(IBienLaiRepository bienLaiRepository)
        {
            _bienLaiRepository = bienLaiRepository;
        }

        public async Task CreateBienLaiAsync(CreateBienLaiInputModel input, string loggedEmployee)
        {
            if (await _bienLaiRepository.IsExistMaBienLaiAsync(input.MaBienLai))
                throw new Exception("Mã Biên Lai Đã Tồn Tại!!");

            await _bienLaiRepository.CreateBienLaiAsync(input, loggedEmployee);
        }

        public async Task<string> GenerateMaBienLaiAsync()
        {
            var currentDate = DateTime.Now;
            string latestMaBienLai = await _bienLaiRepository.GetLastestMaBienLaiAsync();

            if (string.IsNullOrEmpty(latestMaBienLai)) return $"00001{currentDate.ToString("MM")}{currentDate.ToString("yyyy")}";
            
            var numberBienLai = int.Parse(latestMaBienLai.Substring(0, 5));
            var yearBienLai = int.Parse(latestMaBienLai.Substring(7));

            string result = string.Empty;

            if (yearBienLai == currentDate.Year)
            {
                numberBienLai += 1;
                result = $"{numberBienLai.ToString("D5")}{currentDate.ToString("MM")}{currentDate.ToString("yyyy")}";
            }
            else
            {
                result = $"00001{currentDate.ToString("MM")}{currentDate.ToString("yyyy")}";
            }

            return result;
        }
    }
}
