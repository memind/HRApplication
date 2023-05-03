using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.Controllers
{
    [Authorize(Roles = "Site Administrator")]
    public class HomeController : Controller
    {
        private readonly IDashboardService _boardService;

        public HomeController(IDashboardService boardService)
        {
            _boardService = boardService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _boardService.GetDashboardInfos());
        }
    }
}