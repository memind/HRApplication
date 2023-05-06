using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnURL"] = returnUrl;

            return View();
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                if (await _appUserService.Login(loginDTO))
                {
                    var model = await _appUserService.GetCurrentUserInfo(loginDTO.UserName);
                    return RedirectToAction("Index", "Dashboard", new { area = model.Roles[0].Replace(" ", "") });
                }

                ViewData["InvalidLogin"] = "Wrong Credentials";
            }

            return View(loginDTO);
        }
    }
}
