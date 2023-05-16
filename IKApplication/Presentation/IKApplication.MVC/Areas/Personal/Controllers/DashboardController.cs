using IKApplication.Application.AbstractServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IKApplication.MVC.Areas.Personal.Controllers
{
    [Area("PersonalAdministrator")]
    [Authorize(Roles = "Personal Administrator")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dashboardService.GetDashboardInfos());
        }
    }
}
