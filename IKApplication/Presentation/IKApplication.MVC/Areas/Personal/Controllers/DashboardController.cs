using IKApplication.Application.AbstractServices;
using IKApplication.Infrastructure.ConcreteServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IKApplication.MVC.Areas.Personal.Controllers
{
    [Area("Personal")]
    [Authorize(Roles = "Personal")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IAppUserService _appUserService;

        public DashboardController(IDashboardService dashboardService, IAppUserService appUserService)
        {
            _dashboardService = dashboardService;
            _appUserService = appUserService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            return View(await _dashboardService.GetDashboardInfos(user.CompanyId));
        }
    }
}
