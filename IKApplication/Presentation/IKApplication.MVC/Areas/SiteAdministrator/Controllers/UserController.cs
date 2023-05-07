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

        [HttpGet]
        public IActionResult Index()
        {
            //Todo: Get all users and send to view
            return View();
        }

        [HttpGet]
        public IActionResult RegistrationList()
        {
            return View();
        }

        public async Task<IActionResult> ProfileDetails()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            ViewBag.Title = "Profile Details";
            return View("Update",user);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var user = await _appUserService.GetById(id);
            ViewBag.Title = "Update User";
            return View("Update", user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateDTO user)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.UpdateUser(user);
                return RedirectToAction("Index", "Dashboard");
            }
            return View(user);
        }
    }
}
