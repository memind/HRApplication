using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Domain.Entites;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NToastNotify;

namespace IKApplication.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IToastNotification _toast;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ISectorService _sectorService;

        public AccountController(IAppUserService appUserService, IToastNotification toast, IEmailService emailService, UserManager<AppUser> userManager, IConfiguration configuration, ISectorService sectorService)
        {
            _appUserService = appUserService;
            _toast = toast;
            _emailService = emailService;
            _userManager = userManager;
            _configuration = configuration;
            _sectorService = sectorService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
                return RedirectToAction("Index", "Dashboard", new { area = model.Roles[0].Replace(" ", "") });
            }

            ViewData["ReturnURL"] = returnUrl;

            return View();
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                if (await _appUserService.Login(loginDTO))
                {
                    var model = await _appUserService.GetCurrentUserInfo(loginDTO.UserName);
                    return RedirectToAction("Index", "Dashboard", new { area = model.Roles[0].Replace(" ", "") });
                }

                ViewData["InvalidLogin"] = "Wrong Credentials";
            }

            return View(loginDTO);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _appUserService.LogOut();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return View(await _appUserService.CreateRegister());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {

            if (ModelState.IsValid)
            {
                await _appUserService.RegisterUserWithCompany(model, "Company Administrator");
                _toast.AddSuccessToastMessage(Messages.Register.Success(), new ToastrOptions { Title = "Registration" });

                string subject = "Registration Request Arrived";
                string body = model.CompanyName + " sent a registration request. See request by clicking the link: https://hrapplication.azurewebsites.net/SiteAdministrator/User/RegistrationList";

                _emailService.SendMail(_configuration.GetSection("AdminEmails").GetSection("DefaultAdminEmail").Value, subject, body);

                subject = "Registration Request Sent";
                body = " Your registration request has been arrived. We will send you the result via Email.";

                _emailService.SendMail(model.PersonalEmail, subject, body);
                return RedirectToAction("Login", "Account");
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Registration" });

            model.Sectors = await _sectorService.GetAllSectors();

            return View(model);
        }
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    _toast.AddErrorToastMessage(Messages.ResetPasswordMessage.Error(), new ToastrOptions { Title = "Error" });
                    return View(model);
                }

                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { email = user.Email, Code = code });
                string subject = "Password Reset";
                string body = "To reset your password, please click the link below: hrapplication.azurewebsites.net" + callbackUrl;

                _emailService.SendMail(user.PersonalEmail, subject, body);


                return RedirectToAction("ResetPasswordConfirmation");
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string email, string Code)
        {
            var model = new ResetPasswordVM();

            if (email != null &&  Code != null)
            {
                model.Code = Code;
                model.Email = email;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

                if (result.Succeeded)
                {
                    _toast.AddSuccessToastMessage(Messages.ResetPasswordMessage.Success(), new ToastrOptions { Title = "Succes" });
                    return RedirectToAction("Login", "Account");
                }
            }

            _toast.AddErrorToastMessage(Messages.ResetPasswordMessage.Error(), new ToastrOptions { Title = "Error" });
            
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
