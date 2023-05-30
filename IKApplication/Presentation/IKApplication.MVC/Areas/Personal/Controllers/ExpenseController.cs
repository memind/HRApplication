using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.ExpenseDTOs;
using IKApplication.Domain.Entites;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Data;

namespace IKApplication.MVC.Areas.Personal.Controllers
{
    [Area("Personal")]
    [Authorize(Roles = "Personal")]
    public class ExpenseController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IExpenseService _expenseService;
        private readonly IEmailService _emailService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;

        public ExpenseController(IAppUserService appUserService, IExpenseService expenseService, ICompanyService companyService, IMapper mapper, IToastNotification toast, IEmailService emailService)
        {
            _appUserService = appUserService;
            _expenseService = expenseService;
            _companyService = companyService;
            _mapper = mapper;
            _toast = toast;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // 1) Mevcut kullaniciyi bul
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            // 2) Mevcut kullanicinin CompanyId'sini kullanarak ilgili sirketin expense'lerini getir
            var expenses = await _expenseService.GetPersonalExpenses(user.Id);

            foreach (var expense in expenses)
            {
                expense.FullName = await _expenseService.GetPersonalName(expense.ExpenseById);
            }

            return View(expenses);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            ViewBag.ApprovedBy = $"{user.Patron.Name} {user.Patron.SecondName} {user.Patron.Surname}";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseCreateDTO model)
        {
            var expenseBy = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            if (ModelState.IsValid)
            {
                model.ExpenseById = expenseBy.Id;
                model.Id = Guid.NewGuid();
                await _expenseService.CreateExpense(model);
                _toast.AddSuccessToastMessage(Messages.Expense.Create(), new ToastrOptions { Title = "Creating Expense" });

                var mailExpense = await _expenseService.GetVMById(model.Id);

                string subject = "New Expense Request Arrived";
                string body = $"The user {mailExpense.ExpenseBy.Name} {mailExpense.ExpenseBy.SecondName} {mailExpense.ExpenseBy.Surname} requested an expense. See request by clicking the link: https://ikapp.azurewebsites.net/CompanyAdministrator/Expense/ExpenseRequestDetails/{model.Id}?";

                _emailService.SendMail(mailExpense.ExpenseBy.Patron.Email, subject, body);

                return RedirectToAction("Index", "Expense");
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Expense" });

            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            ViewBag.ApprovedBy = $"{user.Patron.Name} {user.Patron.SecondName} {user.Patron.Surname}";

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
    }
}
