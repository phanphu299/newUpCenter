using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Up.Models;
using Up.Services;

namespace Up.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private readonly IKhoaHocService _khoaHocService;

        public HomeController(UserManager<IdentityUser> userManager, IKhoaHocService khoaHocService)
        {
            _userManager = userManager;
            _khoaHocService = khoaHocService;
        }

        public async Task<IActionResult> Index()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            bool IsAdmin = currentUser.IsInRole("Admin");
            var user = await _userManager.GetUserAsync(User);
            var email = user.Email;
            var id = _userManager.GetUserId(User); // Get user id:
            var items = await _khoaHocService.GetKhoaHocAsync();
            return View();
        }

        [Authorize(Roles = Constants.Admin)]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
