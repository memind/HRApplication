using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.CompanyAdministratorControllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
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
        public async Task<IActionResult> ProfileDetails()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            ViewBag.Title = "Profile Details";
            return View("Update", user);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            ViewBag.Title = "Update User";
            return View(await _appUserService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateDTO user)
        {
            if (ModelState.IsValid)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    await _appUserService.UpdateUser(user);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return View(user);
                }
            }
            return View(user);
        }
    }
}
