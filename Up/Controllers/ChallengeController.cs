using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Up.Converters;
using Up.Services;

namespace Up.Controllers
{
    public class ChallengeController : Controller
    {
        private readonly IHocVienService _hocVienService;
        private readonly IThuThachService _thuThachService;
        private readonly Converters.Converter _converter;

        public ChallengeController(IHocVienService hocVienService, IThuThachService thuThachService, Converter converter)
        {
            _hocVienService = hocVienService;
            _thuThachService = thuThachService;
            _converter = converter;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetHocVienAsync(string trigram)
        {
            var model = await _hocVienService.GetHocVienByTrigramAsync(trigram);
            return model == null ?
                Json(_converter.ToResultModel("Incorrect ID !!!", false))
                :
                Json(_converter.ToResultModel(string.Empty, true, model));
        }

        [HttpGet]
        public async Task<IActionResult> GetThuThachHocVienAsync(Guid hocVienId)
        {
            var model = await _thuThachService.GetThuThachByHocVienAsync(hocVienId);
            return Json(model);
        }
    }
}
