using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Up.Converters;
using Up.Models;
using Up.Services;

namespace Up.Controllers
{
    public class ChallengeController : Controller
    {
        private readonly IHocVienService _hocVienService;
        private readonly IThuThachService _thuThachService;
        private readonly Converters.Converter _converter;
        private readonly IConverter _converterPdf;

        public ChallengeController(IHocVienService hocVienService, IThuThachService thuThachService, Converter converter, IConverter converterPdf)
        {
            _hocVienService = hocVienService;
            _thuThachService = thuThachService;
            _converter = converter;
            _converterPdf = converterPdf;
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

        [HttpGet]
        public async Task<IActionResult> GetCauHoiAsync(Guid thuThachId)
        {
            var model = await _thuThachService.GetCauHoiAsync(thuThachId);
            return Json(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> LuuKetQuaAsync([FromBody] ResultInputModel model)
        {
            int soLan = await _thuThachService.LuuKetQuaAsync(model);
            return Ok(soLan);
        }

        [HttpPost]
        public IActionResult ExportResult([FromBody] ExportResultInputModel model)
        {
            string[] args = new string[] {
                model.TenHocVien,
                model.Trigram,
                model.ChallengeName,
                DateTime.Now.ToString("MMMM dd, yyyy h:mm tt"),
                model.LanThi.ToString(),
                model.Score.ToString(),
                model.IsPass.ToString()
            };

            var pdf = SetupPdf(TemplateGenerator.GetChallengeResultTemplate(model.Results), args);
            var file = _converterPdf.Convert(pdf);

            return File(file, "application/pdf");
        }

        private HtmlToPdfDocument SetupPdf(string template, string[] args)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Landscape,
                PaperSize = PaperKind.A5,
                Margins = new MarginSettings { Top = 5 },
                DocumentTitle = "Biên Lai Học Phí",
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = string.Format(template, args),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "bootstrap.css") }
            };
            return new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
        }
    }
}
