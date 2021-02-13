using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NowDelivary.Models;
using NowDelivary.ViewModel;

namespace NowDelivary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<CustomUser> userManager;

        public HomeController(ILogger<HomeController> logger,UserManager<CustomUser> _userMnager)
        {
            _logger = logger;
            userManager = _userMnager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            CustomUser user = await userManager.GetUserAsync(User);
            var userEmail = user.Email;
            CurrentLoginUser currentLoginUser = new CurrentLoginUser();
            currentLoginUser.CurrentUserID = userID;
            currentLoginUser.CurrentUserName = userName;
            currentLoginUser.CurrentUserEmail = userEmail;

            return View(currentLoginUser);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
