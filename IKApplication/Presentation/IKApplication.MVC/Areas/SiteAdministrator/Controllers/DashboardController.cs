using IKApplication.Application.AbstractServices;
using IKApplication.Infrastructure.ConcreteServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.Areas.SiteAdministrator.Controllers
{
    [Area("SiteAdministrator")]
    [Authorize(Roles = "Site Administrator")]
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
            var model = await _dashboardService.GetDashboardInfos(user.CompanyId);
            return View(model);
        }
    }
}
