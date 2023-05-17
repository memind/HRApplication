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
using static IKApplication.MVC.ResultMessages.Messages;

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
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            // 2) Mevcut kullanicinin CompanyId'sini kullanarak ilgili sirketin expense'lerini getir
            var expenses = await _expenseService.GetAllExpenses(user.CompanyId);

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
            var companyMap = _mapper.Map<Domain.Entites.Company>(company);

            var companyManagers = await _appUserService.GetUsersByRole("Company Administrator");
            var companyManager = companyManagers.Where(x => x.CompanyId == expenseBy.CompanyId).First();

            var companyManagerMap = _mapper.Map<AppUser>(companyManager);

            model.ApprovedById = companyManagerMap.Id;
            model.ExpenseById = expenseBy.Id;
            model.CompanyId = expenseBy.CompanyId;

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
        public async Task<IActionResult> Delete(Guid id)
        {
            await _expenseService.DeleteExpense(id);
            _toast.AddSuccessToastMessage(Messages.Expense.Delete(), new ToastrOptions { Title = "Deleting Expense" });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ExpenseRequests()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var expenseRequests = await _expenseService.GetExpenseRequests(user.CompanyId);

            return View(expenseRequests);
        }

        [HttpGet]
        public async Task<IActionResult> ExpenseRequestDetails(Guid id)
        {
            var expense = await _expenseService.GetVMById(id);
            return View(expense);
        }

        [HttpGet]
        public async Task<IActionResult> AcceptExpense(Guid id)
        {
            var expense = await _expenseService.GetById(id);
            await _expenseService.UpdateExpense(expense);

            _toast.AddSuccessToastMessage(Messages.Expense.Accept(expense.ShortDescription), new ToastrOptions { Title = "Accepting Expense" });
            return RedirectToAction("ExpenseRequests");
        }

        [HttpGet]
        public async Task<IActionResult> RefuseExpense(Guid id)
        {
            var expense = await _expenseService.GetById(id);
            await _expenseService.DeleteExpense(id);

            _toast.AddSuccessToastMessage(Messages.Expense.Refuse(expense.ShortDescription), new ToastrOptions { Title = "Refusing Expense" });
            return RedirectToAction("ExpenseRequests");
        }
    }
}