﻿using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.TitleDTOs;
using IKApplication.Application.DTOs.ProfessionDTOs;
using IKApplication.Application.DTOs.TitleDTOs;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace IKApplication.MVC.Areas.CompanyAdministrator.Controllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
    public class ProfessionController : Controller
    {
        private readonly IProfessionService _professionService;
        private readonly IAppUserService _appUserService;
        private readonly IToastNotification _toast;
        private readonly IMapper _mapper;
        public ProfessionController(IProfessionService professionService, IAppUserService appUserService, IToastNotification toast, IMapper mapper)
        {
            _professionService = professionService;
            _appUserService = appUserService;
            _toast = toast;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var professions = await _professionService.GetCompanyProfessionsWithDeleted(user.CompanyId);

            return View(professions);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProfession()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            ProfessionCreateDTO model = new ProfessionCreateDTO()
            {
                Id = Guid.NewGuid(),
                CompanyId = user.CompanyId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfession(ProfessionCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _professionService.Create(model);
                if (result)
                {
                    _toast.AddSuccessToastMessage(Messages.Profession.Create(), new ToastrOptions { Title = "Creating Profession" });
                    return RedirectToAction("Index", "Profession");
                }
            }
            _toast.AddErrorToastMessage(Messages.Profession.Duplicate("created"), new ToastrOptions { Title = "Creating Profession" });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfession(Guid id)
        {
            var profession = await _professionService.GetById(id);
            return View(profession);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfession(ProfessionUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _professionService.Update(model);
                if (result)
                {
                    _toast.AddSuccessToastMessage(Messages.Profession.Update(), new ToastrOptions { Title = "Updating Profession" });
                    return RedirectToAction("Index", "Profession");
                }
            }
            _toast.AddErrorToastMessage(Messages.Profession.Duplicate("updated"), new ToastrOptions { Title = "Updating Profession" });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProfession(Guid id)
        {
            await _professionService.Delete(id);
            _toast.AddSuccessToastMessage(Messages.Profession.Delete(), new ToastrOptions { Title = "Deleting Profession" });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RecoverProfession(Guid id)
        {
            await _professionService.Recover(id);
            _toast.AddSuccessToastMessage(Messages.Profession.Recover(), new ToastrOptions { Title = "Recovering Profession" });
            return RedirectToAction("Index");
        }
    }
}
