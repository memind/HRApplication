using IKApplication.Application.AbstractServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IKApplication.MVC.Areas.CompanyAdministrator.Controllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
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
