using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.ExpenseDTOs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Domain.Entites;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Data;
namespace IKApplication.MVC.Areas.CompanyAdministrator.Controllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly ICompanyService _companyService;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;
        public ExpenseController(IExpenseService expenseService, IMapper mapper, IToastNotification toast, IAppUserService appUserService, ICompanyService companyService)
        {
            _expenseService = expenseService;
            _mapper = mapper;
            _toast = toast;
            _appUserService = appUserService;
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // 1) Mevcut kullaniciyi bul
            var expenseBy = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            // 2) Mevcut kullanicinin CompanyId'sini kullanarak ilgili sirketin expense'lerini getir
            var expenses = await _expenseService.GetAllExpenses(expenseBy.CompanyId);

            return View(expenses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseCreateDTO model)
        {
            var expenseBy = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            var company = await _companyService.GetById(expenseBy.CompanyId);
            var companyMap = _mapper.Map<Company>(company);
            var companyManager = companyMap.CompanyManagers.First();

            model.ApprovedById = companyManager.Id;
            model.ExpenseById = expenseBy.Id;

            if (ModelState.IsValid)
            {
                await _expenseService.CreateExpense(model);
                _toast.AddSuccessToastMessage(Messages.Expense.Create(), new ToastrOptions { Title = "Creating Expense" });
                return RedirectToAction("Index", "Expense");
            }
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Expense" });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var expense = await _expenseService.GetById(id);
            var map = _mapper.Map<ExpenseUpdateDTO>(expense);
            return View(map);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ExpenseUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _expenseService.UpdateExpense(model);
                _toast.AddSuccessToastMessage(Messages.Expense.Update(), new ToastrOptions { Title = "Updating Expense" });
                return RedirectToAction("Index", "Expense");
            }
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Expense" });
            return View(model);
        }

        [HttpGet] 
        public async Task Delete(Guid id)
        {
            var expense = _expenseService.GetById(id);
            await _expenseService.DeleteExpense(id);
        }
    }
}