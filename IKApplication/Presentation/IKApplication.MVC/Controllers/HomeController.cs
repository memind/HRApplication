using IKApplication.Application.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IKApplication.MVC.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Login(string returnUrl = "/")
		{
			if (User.Identity.IsAuthenticated)
			{
				//return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
				return RedirectToAction("Index", "Home");
			}

			ViewData["ReturnURL"] = returnUrl;

			return View();
		}
		[HttpPost]
		public IActionResult Login(LoginDTO loginDTO, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				return View("Index");
            }

            ViewData["ReturnURL"] = returnUrl;

            return View(loginDTO);
		}
	}
}