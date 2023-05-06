using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IKApplication.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;

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

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var sectors = await _appUserService.GetSectorsAsync();

            List<SelectListItem> sectorsSelectList = (from s in sectors select new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
            ViewBag.Sectors = sectorsSelectList;
            return View(new RegisterVM { SectorList = sectors });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerModel)
        {

            if (ModelState.IsValid)
            {
                await _appUserService.RegisterUserWithCompany(registerModel, "Company Administrator");
                // SendEmail(...);
                return RedirectToAction("Index", "Home");
            }

            var sectors = await _appUserService.GetSectorsAsync();
            registerModel.SectorList = sectors;

            return View(registerModel);
        }
    }
}
