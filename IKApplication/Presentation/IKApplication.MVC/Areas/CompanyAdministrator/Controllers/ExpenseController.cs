using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.ExpenseDTOs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Application.VMs.LeaveVMs;
using IKApplication.Domain.Entites;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using OfficeOpenXml;
using System.Data;
using System.Drawing;
using static IKApplication.MVC.ResultMessages.Messages;

namespace IKApplication.MVC.Areas.CompanyAdministrator.Controllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly ICompanyService _companyService;
        private readonly IEmailService _emailService;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;
        public ExpenseController(IExpenseService expenseService, IMapper mapper, IToastNotification toast, IAppUserService appUserService, ICompanyService companyService, IEmailService emailService)
        {
            _expenseService = expenseService;
            _mapper = mapper;
            _toast = toast;
            _appUserService = appUserService;
            _companyService = companyService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // 1) Mevcut kullaniciyi bul
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            // 2) Mevcut kullanicinin CompanyId'sini kullanarak ilgili sirketin expense'lerini getir
            var expenses = await _expenseService.GetAllExpenses(user.CompanyId);

            foreach (var expense in expenses)
            {
                expense.FullName = await _expenseService.GetPersonalName(expense.ExpenseById);
                expense.CurrentUserId = user.Id;
            }

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
                model.Amount = Convert.ToDecimal(model.AmountString);
                await _expenseService.CreateExpense(model);
                _toast.AddSuccessToastMessage(Messages.Expense.Create(), new ToastrOptions { Title = "Creating Expense" });

                string subject = "New Expense Request Arrived";
                string body = $"The user {expenseBy.Name} {expenseBy.SecondName} {expenseBy.Surname} requested an expense. See request by clicking the link: https://ikapp.azurewebsites.net/CompanyAdministrator/Expense/ExpenseRequestDetails/{model.Id}?";

                _emailService.SendMail(companyManagerMap.Email, subject, body);

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
            map.AmountString = map.Amount.ToString();
            return View(map);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ExpenseUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                model.Amount = Convert.ToDecimal(model.AmountString);
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

            foreach (var expense in expenseRequests)
            {
                expense.FullName = await _expenseService.GetPersonalName(expense.ExpenseById);
            }


            return View(expenseRequests);
        }

        [HttpGet]
        public async Task<IActionResult> ExpenseRequestDetails(Guid id)
        {
            var expense = await _expenseService.GetVMById(id);
            expense.FullName = await _expenseService.GetPersonalName(expense.ExpenseById);

            return View(expense);
        }

        [HttpGet]
        public async Task<IActionResult> AcceptExpense(Guid id)
        {
            var expense = await _expenseService.GetVMById(id);
            await _expenseService.AcceptExpense(expense);

            _toast.AddSuccessToastMessage(Messages.Expense.Accept(expense.ShortDescription), new ToastrOptions { Title = "Accepting Expense" });

            string subject = "Your Expense Request Accepted";
            string body = $"Your expense request for '{expense.ShortDescription}' accepted.";

            var expenseVM = await _expenseService.GetVMById(expense.Id);
            var expenseBy = await _appUserService.GetById(expenseVM.ExpenseById);

            _emailService.SendMail(expenseBy.Email, subject, body);

            return RedirectToAction("ExpenseRequests");
        }

        [HttpGet]
        public async Task<IActionResult> RefuseExpense(Guid id)
        {
            var expense = await _expenseService.GetById(id);
            await _expenseService.DeleteExpense(id);

            _toast.AddSuccessToastMessage(Messages.Expense.Refuse(expense.ShortDescription), new ToastrOptions { Title = "Refusing Expense" });

            string subject = "Your Expense Request Refused";
            string body = $"Your expense request for '{expense.ShortDescription}' refused.";

            var expenseVM = await _expenseService.GetVMById(expense.Id);
            var expenseBy = await _appUserService.GetById(expenseVM.ExpenseById);

            _emailService.SendMail(expenseBy.Email, subject, body);

            return RedirectToAction("ExpenseRequests");
        }

        [HttpGet]
        public async Task<IActionResult> ExpenseExcel()
        {
            var stream = new MemoryStream();
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            var date = DateTime.Now;
            var startDate = new DateTime(date.Year,date.Month, 1);
            var endDate = new DateTime(date.Year, date.Month + 1, 1);

            List<ExpenseVM> allExpenseList = await _expenseService.GetAllExpenses(user.CompanyId);
            List<ExpenseVM> expenseList = allExpenseList.Where(x => x.CreateDate >= startDate && x.CreateDate < endDate).ToList() ;

            ExcelPackage pck = new ExcelPackage(stream);
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Monthly Report";
            ws.Cells["B1"].Value = "Expense";

            ws.Cells["A2"].Value = "Date";
            ws.Cells["B2"].Value = $"{date.Month} - {date.Year}";

            ws.Cells["A4"].Value = "Expense By";
            ws.Cells["B4"].Value = "Approved By";
            ws.Cells["C4"].Value = "Expense Type";
            ws.Cells["D4"].Value = "Expense Date";
            ws.Cells["E4"].Value = "Amount";
            ws.Cells["F4"].Value = "Short Description";
            ws.Cells["G4"].Value = "Long Description";
            ws.Cells["H4"].Value = "Status";

            ws.Cells["A4:H4"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A4:H4"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A4:H4"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A4:H4"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A4:H4"].Style.Font.Bold = true;

            int rowStart = 5;
            decimal totalApprovedAmount = 0;
            decimal totalPendingAmount = 0;
            decimal totalAmount = 0;

            foreach (var expense in expenseList) 
            {
                if (expense.Status == Domain.Enums.Status.Passive)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[string.Format("B{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[string.Format("C{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[string.Format("D{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[string.Format("E{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[string.Format("F{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[string.Format("G{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[string.Format("H{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                    ws.Cells[string.Format("A{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("B{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("C{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("D{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("E{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("F{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("G{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("H{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = $"{expense.ExpenseBy.Name} {expense.ExpenseBy.SecondName} {expense.ExpenseBy.Surname}";
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("B{0}", rowStart)].Value = $"{expense.ApprovedBy.Name} {expense.ApprovedBy.SecondName} {expense.ApprovedBy.Surname}";
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("C{0}", rowStart)].Value = expense.Type;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("D{0}", rowStart)].Value = expense.ExpenseDate.ToShortDateString();
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("E{0}", rowStart)].Value = expense.Amount;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("F{0}", rowStart)].Value = expense.ShortDescription;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("G{0}", rowStart)].Value = expense.LongDescription;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("H{0}", rowStart)].Value = expense.Status == Domain.Enums.Status.Passive ? "In Pending" : "Active" ;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


                rowStart++;
                totalAmount += expense.Amount;

                if (expense.Status == Domain.Enums.Status.Passive)
                {
                    totalPendingAmount += expense.Amount;
                }

                if (expense.Status == Domain.Enums.Status.Active || expense.Status == Domain.Enums.Status.Modified)
                {
                    totalApprovedAmount += expense.Amount;
                }
            }

            ws.Cells[string.Format("G{0}", rowStart)].Value = "Total Approved Expenses Amount: ";
            ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("H{0}", rowStart)].Value = totalApprovedAmount;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("G{0}", rowStart)].Style.Font.Bold = true;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Font.Bold = true;
            rowStart++;

            ws.Cells[string.Format("G{0}", rowStart)].Value = "Total Pending Expenses Amount: ";
            ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("H{0}", rowStart)].Value = totalPendingAmount;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("G{0}", rowStart)].Style.Font.Bold = true;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Font.Bold = true;
            rowStart++;

            ws.Cells[string.Format("G{0}", rowStart)].Value = "Total Expense Amount: ";
            ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("H{0}", rowStart)].Value = totalAmount;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("G{0}", rowStart)].Style.Font.Bold = true;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Font.Bold = true;

            ws.Cells["A:AZ"].AutoFitColumns();
            pck.Save();
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",$"Monthly_Expense_Report_{date.Month}/{date.Year}.xlsx");
        }
    }
}