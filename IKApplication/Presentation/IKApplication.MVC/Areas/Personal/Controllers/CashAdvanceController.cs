using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.CashAdvanceDTOs;
using IKApplication.Domain.Entites;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Data;

namespace IKApplication.MVC.Areas.Personal.Controllers
{
    [Area("Personal")]
    [Authorize(Roles = "Personal")]
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
            var advances = await _cashAdvanceServices.GetPersonalAdvances(user.Id);

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
    }
}
