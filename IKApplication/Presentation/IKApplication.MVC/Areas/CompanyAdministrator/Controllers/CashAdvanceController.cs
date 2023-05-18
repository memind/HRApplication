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
        private readonly ICashAdvanceServices _cashAdvanceServices;
        private readonly ICompanyService _companyService;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;
        public CashAdvanceController(IMapper mapper, IToastNotification toast, IAppUserService appUserService, ICompanyService companyService, ICashAdvanceServices cashAdvanceServices)
        {
            _mapper = mapper;
            _toast = toast;
            _appUserService = appUserService;
            _companyService = companyService;
            _cashAdvanceServices = cashAdvanceServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // 1) Mevcut kullaniciyi bul
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            // 2) Mevcut kullanicinin CompanyId'sini kullanarak ilgili sirketin expense'lerini getir
            var advances = await _cashAdvanceServices.GetAllAdvances(user.CompanyId);

            foreach (var advance in advances)
            {
                advance.FullName = await _cashAdvanceServices.GetPersonalName(advance.AdvanceToId);
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
            model.Director = companyManagerMap;
            model.AdvanceToId = advanceTo.Id;
            model.AdvanceTo = advanceToMap;
            model.CompanyId = advanceTo.CompanyId;

            if (ModelState.IsValid)
            {
                await _cashAdvanceServices.Create(model);
                _toast.AddSuccessToastMessage(Messages.Advance.Create(), new ToastrOptions { Title = "Creating Advance" });
                return RedirectToAction("Index", "Advance");
            }
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Advance" });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var advance = await _cashAdvanceServices.GetById(id);
            var map = _mapper.Map<CashAdvanceUpdateDTO>(advance);
            return View(map);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CashAdvanceUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _cashAdvanceServices.Update(model);
                _toast.AddSuccessToastMessage(Messages.Advance.Update(), new ToastrOptions { Title = "Updating Advance" });
                return RedirectToAction("Index", "Advance");
            }
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Advance" });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cashAdvanceServices.Delete(id);
            _toast.AddSuccessToastMessage(Messages.Advance.Delete(), new ToastrOptions { Title = "Deleting Advance" });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CashAdvanceRequests()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var advanceRequests = await _cashAdvanceServices.GetAdvanceRequests(user.CompanyId);

            foreach (var advance in advanceRequests)
            {
                advance.FullName = await _cashAdvanceServices.GetPersonalName(advance.AdvanceToId);
            }


            return View(advanceRequests);
        }

        [HttpGet]
        public async Task<IActionResult> CashAdvanceRequestDetails(Guid id)
        {
            var expense = await _cashAdvanceServices.GetVMById(id);
            expense.FullName = await _cashAdvanceServices.GetPersonalName(expense.AdvanceToId);

            return View(expense);
        }

        [HttpGet]
        public async Task<IActionResult> AcceptCashAdvance(Guid id)
        {
            var advance = await _cashAdvanceServices.GetById(id);
            var user = await _appUserService.GetById(advance.AdvanceToId);
            await _cashAdvanceServices.Update(advance);

            _toast.AddSuccessToastMessage(Messages.Advance.Accept($"{user.Name} {user.SecondName} {user.Surname}"), new ToastrOptions { Title = "Accepting Advance" });
            return RedirectToAction("CashAdvanceRequests");
        }

        [HttpGet]
        public async Task<IActionResult> RefuseCashAdvance(Guid id)
        {
            var advance = await _cashAdvanceServices.GetById(id);
            var user = await _appUserService.GetById(advance.AdvanceToId);
            await _cashAdvanceServices.Delete(id);

            _toast.AddSuccessToastMessage(Messages.Advance.Refuse($"{user.Name} {user.SecondName} {user.Surname}"), new ToastrOptions { Title = "Refusing Advance" });
            return RedirectToAction("CashAdvanceRequests");
        }
    }
}
