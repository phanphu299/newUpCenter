using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Enums;
using Up.Models;
using Up.Repositoties;

namespace Up.Services
{
    public class ThuThachService : IThuThachService
    {
        private readonly IThuThachRepository _thuThachRepository;
        private readonly IHocVienRepository _hocvienRepository;
        private readonly ICauHoiRepository _cauHoiRepository;

        public ThuThachService(IThuThachRepository thuThachRepository, IHocVienRepository hocvienRepository, ICauHoiRepository cauHoiRepository)
        {
            _thuThachRepository = thuThachRepository;
            _hocvienRepository = hocvienRepository;
            _cauHoiRepository = cauHoiRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            return await _thuThachRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_ThuThach);
        }

        public async Task<bool> CanContributeExportAsync(ClaimsPrincipal user)
        {
            return await _thuThachRepository.CanContributeAsync(user, (int)QuyenEnums.Contribute_ThuThach_Export);
        }

        public async Task<ThuThachViewModel> CreateThuThachAsync(CreateThuThachInputModel input, string loggedEmployee)
        {
            if (input.MinGrade <= 0 ||
                    string.IsNullOrWhiteSpace(input.Name) ||
                    input.KhoaHocId == null ||
                    input.SoCauHoi <= 0 ||
                    input.ThoiGianLamBai <= 0)
                throw new Exception("Tên Thử Thách, Số Câu Hỏi, Thời Gian Làm Bài và Điểm Sàn không được để trống !!!");

            var result = await _thuThachRepository.CreateThuThachAsync(input, loggedEmployee);
            return await _thuThachRepository.GetThuThachDetailAsync(result);
        }

        public async Task<bool> DeleteThuThachAsync(Guid id, string loggedEmployee)
        {
            return await _thuThachRepository.DeleteThuThachAsync(id, loggedEmployee);
        }

        public async Task<List<CauHoiViewModel>> GetCauHoiAsync(Guid thuThachId)
        {
            var cauHois = await _cauHoiRepository.GetCauHoiAsync(thuThachId);

            return cauHois
                .OrderBy(x => x.STT)
                .GroupBy(x => x.STT)
                .Select(group => 
                {
                    Random rand = new Random();
                    int toSkip = rand.Next(0, group.Count());

                    var randomCauHoi = group.OrderBy(x => Guid.NewGuid()).Skip(toSkip).Take(1).FirstOrDefault();
                    return randomCauHoi;
                })
                .ToList();
        }

        public async Task<List<HocVienViewModel>> GetKetQuaAsync(Guid lopHocId, int month, int year)
        {
            var result = await _hocvienRepository.GetHocVienKetQuaAsync(lopHocId, month, year);
            int index = 1;
            foreach (var item in result)
            {
                item.Stt = index;
                index++;
            }

            return result;
        }

        public async Task<List<ThuThachViewModel>> GetThuThachAsync()
        {
            return await _thuThachRepository.GetThuThachAsync();
        }

        public async Task<List<ThuThachViewModel>> GetThuThachByHocVienAsync(Guid hocVienId)
        {
            var hocVien = await _hocvienRepository.GetHocVienDetailAsync(hocVienId);

            var khoaHocIds = hocVien.LopHocList
                                    .Where(x => !x.IsCanceled && !x.IsDisabled && !x.HocVienNghi)
                                    .Select(x => x.KhoaHocId)
                                    .ToList();

            return await _thuThachRepository.GetThuThachByKhoaHocIdsAsync(khoaHocIds);
        }

        public async Task<int> LuuKetQuaAsync(ResultInputModel input)
        {
            var latestLanThi = await _thuThachRepository.GetLatestLanThi(input.HocVienId, input.ThuThachId);
            input.LanThi = latestLanThi + 1;

            await _thuThachRepository.LuuKetQuaAsync(input);
            return input.LanThi;
        }

        public async Task<ThuThachViewModel> UpdateThuThachAsync(UpdateThuThachInputModel input, string loggedEmployee)
        {
            var result = await _thuThachRepository.UpdateThuThachAsync(input, loggedEmployee);
            return await _thuThachRepository.GetThuThachDetailAsync(result);
        }
    }
}
