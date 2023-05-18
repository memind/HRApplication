using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.DTOs.PersonalDTO;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
                    return RedirectToAction("Index");
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

            var model = new AppUserCreateDTO() { CompanyId = user.CompanyId, Companies = companies, Titles = titles, Password = "123", ConfirmPassword = "123" };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyManager(AppUserCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.CreateUser(model, "Company Administrator");

                var user = await _appUserService.GetByUserName(model.Email);
                user.Password = "123";

                await _appUserService.UpdateUser(user);

                var company = await _companyService.GetById(user.CompanyId);
                var create = _appUserService.AddCompanyManager(model, company);

                string subject = "Get Your Password";
                string body = "To set your password, please click the link below: https://ikapp.azurewebsites.net/Account/ForgotPassword";
                _emailService.SendMail(model.Email, subject, body);


                _toast.AddSuccessToastMessage(Messages.CompanyAdmin.Create(model.Email), new ToastrOptions { Title = "Creating Company Manager" });
                return RedirectToAction("Index");
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Company Manager" });

            var currentUser = await _appUserService.GetByUserName(User.Identity.Name);
            var companies = await _companyService.GetAllCompanies();
            var titles = await _titleService.GetAllTitles();
            var companyTitles = titles.Where(x => x.CompanyId == currentUser.CompanyId).ToList();

            model.CompanyId = currentUser.CompanyId;
            model.Companies = companies;
            model.Titles = titles;
            model.Password = "123";
            model.ConfirmPassword = "123";

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePersonal()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            var companies = await _companyService.GetAllCompanies();
            var titles = await _titleService.GetAllTitles();
            var companyTitles = titles.Where(x => x.CompanyId == user.CompanyId).ToList();

            var model = new PersonalCreateDTO() { CompanyId = user.CompanyId, Companies = companies, Titles = titles, Password = "123", ConfirmPassword = "123" };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonal(PersonalCreateDTO model)
        {
            model.Id = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                var modelMap = _mapper.Map<AppUserCreateDTO>(model);
                await _appUserService.CreateUser(modelMap, "Personal");

                var user = await _appUserService.GetByUserName(model.Email);
                user.Password = "123";

                await _appUserService.UpdateUser(user);

                string subject = "Get Your Password";
                string body = "To set your password, please click the link below: https://ikapp.azurewebsites.net/Account/ForgotPassword";
                _emailService.SendMail(model.Email, subject, body);


                _toast.AddSuccessToastMessage(Messages.Personal.Create(model.Email), new ToastrOptions { Title = "Creating Personal" });
                return RedirectToAction("Index");
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Personal" });

            var currentUser = await _appUserService.GetByUserName(User.Identity.Name);
            var companies = await _companyService.GetAllCompanies();
            var titles = await _titleService.GetAllTitles();
            var companyTitles = titles.Where(x => x.CompanyId == currentUser.CompanyId).ToList();

            model.CompanyId = currentUser.CompanyId;
            model.Companies = companies;
            model.Titles = titles;
            model.Password = "123";
            model.ConfirmPassword = "123";

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _appUserService.GetById(id);
            await _appUserService.Delete(id);
            _toast.AddSuccessToastMessage(Messages.Personal.Delete(user.Email), new ToastrOptions { Title = "Deleting User" });
            return RedirectToAction("Index");
        }
    }
}
