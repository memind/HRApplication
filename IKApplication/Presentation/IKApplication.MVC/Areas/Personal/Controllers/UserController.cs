using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.PersonalDTO;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Domain.Entites;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Data;

namespace IKApplication.MVC.Areas.Personal.Controllers
{
    [Area("Personal")]
    [Authorize(Roles = "Personal")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IToastNotification _toast;
        private readonly ICompanyService _companyService;
        private readonly ITitleService _titleService;
        private readonly IMapper _mapper;

        public UserController(IAppUserService appUserSerives, IToastNotification toast, ICompanyService companyService, ITitleService titleService, IMapper mapper, IEmailService emailService)
        {
            _appUserService = appUserSerives;
            _toast = toast;
            _companyService = companyService;
            _titleService = titleService;
            _mapper = mapper;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Todo: Get all users and send to view
            var personals = await _appUserService.GetAllUsers();
            return View(personals);
        }
        [HttpGet]
        public async Task<IActionResult> ProfileDetails()
        {
            var personal = await _appUserService.GetByUserName(User.Identity.Name);
            ViewBag.Title = "Profile Details";
            return View("Update", personal);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            ViewBag.Title = "Update Personal";
            return View(await _appUserService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(PersonalUpdateDTO personal)
        {
            if (ModelState.IsValid)
            {
                if (personal.Password == personal.ConfirmPassword)
                {
                    await _appUserService.UpdatePersonal(personal);
                    _toast.AddSuccessToastMessage(Messages.User.Update(personal.Email), new ToastrOptions { Title = "Updating Personal" });
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Personal" });
                    return View(personal);
                }
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Personal" });
            return View(personal);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _appUserService.GetById(id);
            await _appUserService.Delete(id);
            _toast.AddSuccessToastMessage(Messages.Personal.Delete(user.Email), new ToastrOptions { Title = "Deleting Personal" });
            return RedirectToAction("Index");
        }
    }
}
