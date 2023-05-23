using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.TitleDTOs;
using IKApplication.Application.DTOs.TitleDTOs;
using IKApplication.Domain.Entites;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Data;

namespace IKApplication.MVC.Areas.CompanyAdministrator.Controllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
    public class TitleController : Controller
    {
        private readonly ITitleService _titleService;
        private readonly IAppUserService _appUserService;
        private readonly IToastNotification _toast;
        private readonly IMapper _mapper;
        public TitleController(ITitleService titleService, IAppUserService appUserService, IToastNotification toast, IMapper mapper)
        {
            _titleService = titleService;
            _appUserService = appUserService;
            _toast = toast;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var titles = await _titleService.GetCompanyTitles(user.CompanyId);

            return View(titles);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTitle()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            TitleCreateDTO model = new TitleCreateDTO() 
            { 
            Id = Guid.NewGuid(),
            CompanyId = user.CompanyId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTitle(TitleCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _titleService.Create(model);
                _toast.AddSuccessToastMessage(Messages.Title.Create(), new ToastrOptions { Title = "Creating Title" });

                return RedirectToAction("Index", "Title");
            }
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Title" });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTitle(Guid id)
        {
            var title = await _titleService.GetById(id);
            return View(title);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTitle(TitleUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _titleService.Update(model);
                _toast.AddSuccessToastMessage(Messages.Title.Update(), new ToastrOptions { Title = "Updating Title" });
                return RedirectToAction("Index", "Title");
            }
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Title" });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTitle(Guid id)
        {
            await _titleService.Delete(id);
            _toast.AddSuccessToastMessage(Messages.Title.Delete(), new ToastrOptions { Title = "Deleting Title" });
            return RedirectToAction("Index");
        }
    }
}
