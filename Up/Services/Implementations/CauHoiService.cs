using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Up.Models;
using Up.Repositoties;

namespace Up.Services
{
    public class CauHoiService : ICauHoiService
    {
        private readonly ICauHoiRepository _cauHoiRepository;
        private readonly IThuThachRepository _thuThachRepository;

        public CauHoiService(ICauHoiRepository cauHoiRepository, IThuThachRepository thuThachRepository)
        {
            _cauHoiRepository = cauHoiRepository;
            _thuThachRepository = thuThachRepository;
        }

        public Task<bool> CanContributeAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public async Task<CauHoiViewModel> CreateCauHoiAsync(CreateCauHoiInputModel input, string loggedEmployee)
        {
            if (!(await ValidateCreateCauHoi(input)))
                throw new Exception("Data không hợp lệ !!!");

            var result = await _cauHoiRepository.CreateCauHoiAsync(input, loggedEmployee);
            return await _cauHoiRepository.GetCauHoiDetailAsync(result);
        }

        public async Task<bool> DeleteCauHoiAsync(Guid id, string loggedEmployee)
        {
            return await _cauHoiRepository.DeleteCauHoiAsync(id, loggedEmployee);
        }

        public async Task<List<CauHoiViewModel>> GetCauHoiAsync()
        {
            return await _cauHoiRepository.GetCauHoiAsync();
        }

        public async Task<List<CauHoiViewModel>> GetCauHoiAsync(IList<Guid> ids)
        {
            return await _cauHoiRepository.GetCauHoiAsync(ids);
        }

        public async Task<List<CauHoiViewModel>> GetCauHoiAsync(Guid thuThachId, int stt = 1)
        {
            return await _cauHoiRepository.GetCauHoiAsync(thuThachId, stt);
        }

        public async Task<List<CauHoiViewModel>> ImportCauHoiAsync(ImportCauHoiInputModel input, string loggedEmployee)
        {
            if (!(await ValidateImportCauHoi(input)))
                throw new Exception("Data không hợp lệ !!!");

            var result = await _cauHoiRepository.ImportCauHoiAsync(input, loggedEmployee);
            return await _cauHoiRepository.GetCauHoiAsync(result);
        }

        private async Task<bool> ValidateCreateCauHoi(CreateCauHoiInputModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
                throw new Exception("Câu hỏi không được có giá trị rỗng !!!");

            if (input.DapAns.Any(x => string.IsNullOrWhiteSpace(x.Name)))
                throw new Exception("Đáp án không được có giá trị rỗng !!!");

            var thuThach = await _thuThachRepository.GetThuThachDetailAsync(input.ThuThachId);
            if (input.STT <= 0 || input.STT > thuThach.SoCauHoi)
                throw new Exception("Câu Hỏi Số phải lớn hơn 0 và không vượt quá Số Câu Hỏi của Thử Thách !!!");

            return true;
        }

        private async Task<bool> ValidateImportCauHoi(ImportCauHoiInputModel input)
        {
            if (input.CauHois.Any(x => string.IsNullOrWhiteSpace(x.Name)))
                throw new Exception("Câu hỏi không được có giá trị rỗng !!!");

            if (input.CauHois.Any(x => x.DapAns.Any(m => string.IsNullOrWhiteSpace(m.Name))))
                throw new Exception("Đáp án không được có giá trị rỗng !!!");

            var thuThach = await _thuThachRepository.GetThuThachDetailAsync(input.ThuThachId);
            if (input.CauHois.Any(x => x.STT <= 0 || x.STT > thuThach.SoCauHoi))
                throw new Exception("cột Câu Hỏi Số phải lớn hơn 0 và không vượt quá Số Câu Hỏi của Thử Thách !!!");

            return true;
        }
    }
}
