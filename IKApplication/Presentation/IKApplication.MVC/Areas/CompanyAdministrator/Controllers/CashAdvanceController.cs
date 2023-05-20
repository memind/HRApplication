using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.CashAdvanceDTOs;
using IKApplication.Application.dtos.ExpenseDTOs;
using IKApplication.Domain.Entites;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Data;
using static IKApplication.MVC.ResultMessages.Messages;

namespace IKApplication.MVC.Areas.CompanyAdministrator.Controllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
    public class CashAdvanceController : Controller
    {
        private readonly ICashAdvanceService _cashAdvanceService;
        private readonly ICompanyService _companyService;
        private readonly IEmailService _emailService;
        private readonly IAppUserService _appUserService;
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
            var advances = await _cashAdvanceService.GetAllAdvances(user.CompanyId);

            foreach (var advance in advances)
            {
                advance.FullName = await _cashAdvanceService.GetPersonalName(advance.AdvanceToId);
                advance.CurrentUserId = user.Id;
            }

            return View(advances);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CashAdvanceCreateDTO model)
        {
            var advanceTo = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var advanceToMap = _mapper.Map<Domain.Entites.AppUser>(advanceTo);

            var company = await _companyService.GetById(advanceTo.CompanyId);
            var companyMap = _mapper.Map<Domain.Entites.Company>(company);

            var companyManagers = await _appUserService.GetUsersByRole("Company Administrator");
            var companyManager = companyManagers.Where(x => x.CompanyId == advanceTo.CompanyId).First();

            var companyManagerMap = _mapper.Map<AppUser>(companyManager);

            model.DirectorId = companyManagerMap.Id;
            model.AdvanceToId = advanceTo.Id;
            model.CompanyId = advanceTo.CompanyId;

            if (ModelState.IsValid)
            {
                await _cashAdvanceService.Create(model);
                _toast.AddSuccessToastMessage(Messages.Advance.Create(), new ToastrOptions { Title = "Creating Advance" });

                string subject = "New Advance Request Arrived";
                string body = $"The user {advanceTo.Name} {advanceTo.SecondName} {advanceTo.Surname} requested a cash advance. See request by clicking the link: https://ikapp.azurewebsites.net/CompanyAdministrator/CashAdvance/CashAdvanceRequestDetails/{model.Id}?";

                _emailService.SendMail(companyManagerMap.Email, subject, body);

                return RedirectToAction("Index", "CashAdvance");
            }
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Advance" });
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
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cashAdvanceService.Delete(id);
            _toast.AddSuccessToastMessage(Messages.Advance.Delete(), new ToastrOptions { Title = "Deleting Advance" });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CashAdvanceRequests()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var advanceRequests = await _cashAdvanceService.GetAdvanceRequests(user.CompanyId);

            foreach (var advance in advanceRequests)
            {
                advance.FullName = await _cashAdvanceService.GetPersonalName(advance.AdvanceToId);
            }


            return View(advanceRequests);
        }

        [HttpGet]
        public async Task<IActionResult> CashAdvanceRequestDetails(Guid id)
        {
            var expense = await _cashAdvanceService.GetVMById(id);
            expense.FullName = await _cashAdvanceService.GetPersonalName(expense.AdvanceToId);
            expense.RequestedAmount = Convert.ToInt32(expense.RequestedAmount);
            return View(expense);
        }

        [HttpGet]
        public async Task<IActionResult> AcceptCashAdvance(Guid id)
        {
            var advance = await _cashAdvanceService.GetVMById(id);
            await _cashAdvanceService.AcceptAdvance(advance);

            var advanceTo = await _appUserService.GetById(advance.AdvanceToId);

            _toast.AddSuccessToastMessage(Messages.Advance.Accept($"{advanceTo.Name} {advanceTo.SecondName} {advanceTo.Surname}"), new ToastrOptions { Title = "Accepting Expense" });

            string subject = "Your Advance Request Accepted";
            string body = $"Your cash advance request for '{advance.Description}' accepted.";

            _emailService.SendMail(advanceTo.Email, subject, body);

            return RedirectToAction("CashAdvanceRequests");
        }

        [HttpGet]
        public async Task<IActionResult> RefuseCashAdvance(Guid id)
        {
            var advance = await _cashAdvanceService.GetById(id);
            var user = await _appUserService.GetById(advance.AdvanceToId);
            await _cashAdvanceService.Delete(id);

            var advanceVM = await _cashAdvanceService.GetVMById(advance.Id);

            string subject = "Your Advance Request Refused";
            string body = $"Your cash advance request for '{advanceVM.Description}' refused.";

            var advanceTo = await _appUserService.GetById(advanceVM.AdvanceToId);

            _emailService.SendMail(advanceTo.Email, subject, body);

            _toast.AddSuccessToastMessage(Messages.Advance.Refuse($"{user.Name} {user.SecondName} {user.Surname}"), new ToastrOptions { Title = "Refusing Advance" });
            return RedirectToAction("CashAdvanceRequests");
        }
    }
}
