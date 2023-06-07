using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.CashAdvanceDTOs;
using IKApplication.Domain.Entites;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Data;
using static IKApplication.MVC.ResultMessages.Messages;

namespace IKApplication.MVC.Areas.Personal.Controllers
{
    [Area("Personal")]
    [Authorize(Roles = "Personal")]
    public class CashAdvanceController : Controller
    {
        private readonly ICashAdvanceService _cashAdvanceService;
        private readonly ICompanyService _companyService;
        private readonly IAppUserService _appUserService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;
        public CashAdvanceController(IMapper mapper, IToastNotification toast, IAppUserService appUserService, ICompanyService companyService, ICashAdvanceService cashAdvanceService, IEmailService emailService)
        {
            _mapper = mapper;
            _toast = toast;
            _appUserService = appUserService;
            _companyService = companyService;
            _cashAdvanceService = cashAdvanceService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // 1) Mevcut kullaniciyi bul
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            // 2) Mevcut kullanicinin CompanyId'sini kullanarak ilgili sirketin expense'lerini getir
            var advances = await _cashAdvanceService.GetPersonalAdvances(user.Id);

            foreach (var advance in advances)
            {
                advance.FullName = await _cashAdvanceService.GetPersonalName(advance.AdvanceToId);
            }

            return View(advances);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            ViewBag.ApprovedBy = $"{user.Patron.Name} {user.Patron.SecondName} {user.Patron.Surname}";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CashAdvanceCreateDTO model)
        {
            var advanceTo = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                model.AdvanceToId = advanceTo.Id;
                await _cashAdvanceService.Create(model);
                _toast.AddSuccessToastMessage(Messages.Advance.Create(), new ToastrOptions { Title = "Creating Advance" });

                var mailAdvance = await _cashAdvanceService.GetVMById(model.Id);

                string subject = "New Advance Request Arrived";
                string body = $"The user {mailAdvance.AdvanceTo.Name} {mailAdvance.AdvanceTo.SecondName} {mailAdvance.AdvanceTo.Surname} requested a cash advance. See request by clicking the link: https://hrapplication.azurewebsites.net/CompanyAdministrator/CashAdvance/CashAdvanceRequestDetails/{model.Id}?";

                _emailService.SendMail(mailAdvance.AdvanceTo.Patron.PersonalEmail, subject, body);

                return RedirectToAction("Index", "CashAdvance");
            }
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Advance" });

            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            ViewBag.ApprovedBy = $"{user.Patron.Name} {user.Patron.SecondName} {user.Patron.Surname}";

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var advance = await _cashAdvanceService.GetById(id);
            var map = _mapper.Map<CashAdvanceUpdateDTO>(advance);
            map.RequestedAmount = Convert.ToInt32(advance.RequestedAmount);
            return View(map);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CashAdvanceUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _cashAdvanceService.Update(model);
                _toast.AddSuccessToastMessage(Messages.Advance.Update(), new ToastrOptions { Title = "Updating Advance" });
                return RedirectToAction("Index", "CashAdvance");
            }
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Advance" });

            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            ViewBag.ApprovedBy = $"{user.Patron.Name} {user.Patron.SecondName} {user.Patron.Surname}";

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cashAdvanceService.Delete(id);
            _toast.AddSuccessToastMessage(Messages.Advance.Delete(), new ToastrOptions { Title = "Deleting Advance" });
            return RedirectToAction("Index");
        }
    }
}
