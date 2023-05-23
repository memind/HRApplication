using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Domain.Entites;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using System.Data;
using static IKApplication.MVC.ResultMessages.Messages;

namespace IKApplication.MVC.Areas.Personal.Controllers
{
    [Area("Personal")]
    [Authorize(Roles = "Personal")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ITitleService _titleService;
        private readonly IToastNotification _toast;
        private readonly ICompanyService _companyService;
        private readonly ITitleService _titleService;
        private readonly IMapper _mapper;

        public UserController(IAppUserService appUserSerives, IToastNotification toast, ITitleService titleService)
        {
            _appUserService = appUserSerives;
            _toast = toast;
            _titleService = titleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index","Dashboard");
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
            ViewBag.Area = "Personal";
            var user = await _appUserService.GetById(id);
            user.Titles = await _titleService.GetCompanyTitles(user.CompanyId);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateDTO personal)
        {
            if (ModelState.IsValid)
            {
                if (personal.Password == personal.ConfirmPassword)
                {
                    await _appUserService.UpdateUser(personal);
                    _toast.AddSuccessToastMessage(Messages.User.Update(personal.Email), new ToastrOptions { Title = "Updating Personal" });
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Personal" });
                    return View(personal);
                }
            }

            personal.Titles = await _titleService.GetCompanyTitles(personal.CompanyId);
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
