using IKApplication.Application.AbstractServices;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.Areas.SiteAdministrator.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _boardService;

        public DashboardController(IDashboardService boardService)
        {
            _boardService = boardService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _boardService.GetCountInfos());
        }
    }
}
