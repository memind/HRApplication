﻿using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Domain.Entites;
using IKApplication.Infrastructure.ConcreteServices;
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
        private readonly IProfessionService _professionService;
        private readonly IMapper _mapper;

        public UserController(IAppUserService appUserSerives, IToastNotification toast, ITitleService titleService, IProfessionService professionService)
        {
            _appUserService = appUserSerives;
            _toast = toast;
            _titleService = titleService;
            _professionService = professionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index","Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> StaffCards()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            var users = await _appUserService.GetUsersByCompany(user.CompanyId);
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> ProfileDetails()
        {
            var personal = await _appUserService.GetByUserName(User.Identity.Name);
            ViewBag.Title = "Profile Details";
            var patron = await _appUserService.GetCurrentUserInfo(personal.Id);

            ViewBag.Patron = $"{patron.Patron.Name} {patron.Patron.SecondName} {patron.Patron.Surname}";
            personal.Professions = await _professionService.GetAllProfessions();
            return View("Update", personal);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            ViewBag.Title = "Update Personal";
            ViewBag.Area = "Personal";
            var user = await _appUserService.GetById(id);
            var patron = await _appUserService.GetCurrentUserInfo(id);

            ViewBag.Patron = $"{patron.Patron.Name} {patron.Patron.SecondName} {patron.Patron.Surname}";
            user.Titles = await _titleService.GetCompanyTitles(user.CompanyId);
            user.Professions = await _professionService.GetAllProfessions();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateDTO personal)
        {
            personal.Profession = null;
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
            var patron = await _appUserService.GetCurrentUserInfo(personal.Id);

            ViewBag.Patron = $"{patron.Patron.Name} {patron.Patron.SecondName} {patron.Patron.Surname}";
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Personal" });
            return View(personal);
        }
    }
}
