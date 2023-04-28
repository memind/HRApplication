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
				return RedirectToAction("Index", "");
			}

			ViewData["ReturnURL"] = returnUrl;

			return View();
		}
		[HttpPost]
		public IActionResult Login(LoginDTO loginDTO, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				//var result = await _appUserService.Login(loginDTO);
				//if (result.Succeeded)
				//	return RedirectToLocal(returnUrl);

				return RedirectToAction("Index", "");

				//ModelState.AddModelError("", "Invalid Login Attempt");
			}
			return View(loginDTO);
		}
	}
}