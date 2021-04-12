﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;
using Up.Repositoties;

namespace Up.Services
{
    public class ThuThachService : IThuThachService
    {
        private readonly IThuThachRepository _thuThachRepository;
        private readonly IHocVienRepository _hocvienRepository;

        public ThuThachService(IThuThachRepository thuThachRepository, IHocVienRepository hocvienRepository)
        {
            _thuThachRepository = thuThachRepository;
            _hocvienRepository = hocvienRepository;
        }

        public async Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
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

        public async Task<ThuThachViewModel> UpdateThuThachAsync(UpdateThuThachInputModel input, string loggedEmployee)
        {
            var result = await _thuThachRepository.UpdateThuThachAsync(input, loggedEmployee);
            return await _thuThachRepository.GetThuThachDetailAsync(result);
        }
    }
}
