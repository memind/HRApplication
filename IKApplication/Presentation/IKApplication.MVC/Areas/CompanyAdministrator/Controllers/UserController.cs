using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Domain.Enums;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using static IKApplication.MVC.ResultMessages.Messages;

namespace IKApplication.MVC.CompanyAdministratorControllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IToastNotification _toast;
        private readonly ICompanyService _companyService;
        private readonly ITitleService _titleService;
        private readonly IMapper _mapper;

        public UserController(IAppUserService appUserSerives, IToastNotification toast, ICompanyService companyService, ITitleService titleService, IMapper mapper)
        {
            _appUserService = appUserSerives;
            _toast = toast;
            _companyService = companyService;
            _titleService = titleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            //Todo: Get all users and send to view
            var users = await _appUserService.GetUsersByCompany(user.CompanyId);
            return View(users);
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
                    _toast.AddSuccessToastMessage(Messages.User.Update(user.Email), new ToastrOptions { Title = "Updating User" });
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating User" });
                    return View(user);
                }
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating User" });
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCompanyManager()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            var companies = await _companyService.GetAllCompanies();
            var titles = await _titleService.GetAllTitles();
            var companyTitles = titles.Where(x => x.CompanyId == user.CompanyId).ToList();

            var model = new AppUserCreateDTO() { CompanyId = user.CompanyId, Companies = companies, Titles = titles };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyManager(AppUserCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.CreateUser(model, "Company Administrator");
                var user = await _appUserService.GetByUserName(model.Email);
                await _appUserService.UpdateUser(user);
                _toast.AddSuccessToastMessage(Messages.CompanyAdmin.Create(model.Email), new ToastrOptions { Title = "Creating Company Manager" });
                return RedirectToAction("Index");
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Company Manager" });
            return View(model);
        }
    }
}
