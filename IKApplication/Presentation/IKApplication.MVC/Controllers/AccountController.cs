using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.UserVMs;
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
        public async Task<IActionResult> Login(LoginDTO loginDTO, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                if (await _appUserService.Login(loginDTO))
                {
                    return RedirectToLocal(returnUrl);
                }

                ViewData["InvalidLogin"] = "Wrong Credentials";
            }

            ViewData["ReturnURL"] = returnUrl;

            return View(loginDTO);
        }

        private IActionResult RedirectToLocal(string returnUrl = "/")
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var sectors = await _appUserService.GetSectorsAsync();
            return View(new RegisterVM { SectorList = sectors});
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerModel)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.RegisterUserWithCompany(registerModel, "Company Administrator");
                return RedirectToAction("Index", "Home");
            }
            var sectors = await _appUserService.GetSectorsAsync();

            RegisterVM registerVM = registerModel;
            registerVM.SectorList = sectors;

            return View(registerVM);
        }
    }
}
