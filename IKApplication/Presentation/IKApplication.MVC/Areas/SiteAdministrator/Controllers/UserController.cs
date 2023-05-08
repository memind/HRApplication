﻿using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Domain.Enums;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using static IKApplication.MVC.ResultMessages.Messages;

namespace IKApplication.MVC.Areas.SiteAdministrator.Controllers
{
    [Area("SiteAdministrator")]
    [Authorize(Roles = "Site Administrator")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyService;
        private readonly IToastNotification _toast;

        public UserController(IAppUserService appUserSerives, ICompanyService companyService, IToastNotification toast)
        {
            _appUserService = appUserSerives;
            _companyService = companyService;
            _toast = toast;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Todo: Get all users and send to view
            var users = await _appUserService.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> RegistrationList()
        {
            var registrations = await _appUserService.GetAllRegistrations();
            return View(registrations);
        }

        [HttpGet]
        public async Task<IActionResult> RegistrationDetails(Guid userId, Guid companyId)
        {
            ViewBag.User = await _appUserService.GetById(userId);
            ViewBag.Company = await _companyService.GetById(companyId);
            return View();
        }
        public async Task<IActionResult> AcceptRegistration(Guid userId, Guid companyId)
        {
            var company = await _companyService.GetById(companyId);
            await _companyService.Update(company);

            var user = await _appUserService.GetById(userId);
            await _appUserService.UpdateUser(user);

            return RedirectToAction("RegistrationList");
        }

        public async Task<IActionResult> DeclineRegistration(Guid userId, Guid companyId)
        {
            await _appUserService.Delete(userId);
            await _companyService.Delete(companyId);

            return RedirectToAction("RegistrationList");
        }

        public async Task<IActionResult> ProfileDetails()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            ViewBag.Title = "Profile Details";
            return View("Update", user);
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
                _toast.AddSuccessToastMessage(Messages.User.Update(user.Email), new ToastrOptions { Title = "Updating User" });
                return RedirectToAction("Index", "Dashboard");
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating User" });
            return View(user);
        }
    }
}
