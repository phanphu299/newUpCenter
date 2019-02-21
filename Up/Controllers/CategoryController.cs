using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Up.Services;

namespace Up.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IKhoaHocService _khoaHocService;

        public CategoryController(IKhoaHocService khoaHocService)
        {
            _khoaHocService = khoaHocService;
        }

        public IActionResult KhoaHocIndex()
        {
            return View();
        }

        public async Task<IActionResult> GetKhoaHocAsync()
        {
            var model = await _khoaHocService.GetKhoaHocAsync();
            return Json(model);
        }
    }
}