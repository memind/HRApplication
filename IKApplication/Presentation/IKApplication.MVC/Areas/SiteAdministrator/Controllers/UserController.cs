using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.Areas.SiteAdministrator.Controllers
{
    [Area("SiteAdministrator")]
    [Authorize(Roles = "Site Administrator")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;

        public UserController(IAppUserService appUserSerives)
        {
            _appUserService = appUserSerives;
        }

        public async Task<IActionResult> ProfileDetails()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            return View("Update",user);
        }

        [HttpGet]
        public IActionResult CreateSiteAdministrator()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSiteAdministrator(AppUserCreateDTO user)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.CreateUser(user, "Site Administrator");
                return RedirectToAction("Dashboard", "Index");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult CreateCompanyAdministrator()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyAdministrator(AppUserCreateDTO user)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.CreateUser(user, "Company Administrator");
                return RedirectToAction("Dashboard", "Index");
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            return View(await _appUserService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateDTO user)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.UpdateUser(user);
                return RedirectToAction("Dashboard", "Index");
            }
            return View(user);
        }
    }
}
