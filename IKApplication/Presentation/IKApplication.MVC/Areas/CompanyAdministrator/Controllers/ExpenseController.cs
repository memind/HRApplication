using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.ExpenseDTOs;
using IKApplication.Application.VMs.ExcelVMs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Application.VMs.LeaveVMs;
using IKApplication.Domain.Entites;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using OfficeOpenXml;
using System.Data;
using System.Drawing;
using System.Text;
using static IKApplication.MVC.ResultMessages.Messages;
using iTextSharp.tool.xml.html.head;
using IKApplication.Application.DTOs.ReportDTOs;
using IKApplication.Domain.Enums;

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
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;
        public ExpenseController(IExpenseService expenseService, IMapper mapper, IToastNotification toast, IAppUserService appUserService, ICompanyService companyService, IEmailService emailService, IReportService reportService)
        {
            _expenseService = expenseService;
            _mapper = mapper;
            _toast = toast;
            _appUserService = appUserService;
            _companyService = companyService;
            _emailService = emailService;
            _reportService = reportService;
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
                string body = $"The user {mailExpense.ExpenseBy.Name} {mailExpense.ExpenseBy.SecondName} {mailExpense.ExpenseBy.Surname} requested an expense. See request by clicking the link: https://hrapplication.azurewebsites.net/CompanyAdministrator/Expense/ExpenseRequestDetails/{model.Id}?";

                _emailService.SendMail(mailExpense.ExpenseBy.Patron.Email, subject, body);

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

            _emailService.SendMail(expense.ExpenseBy.Email, subject, body);

            return RedirectToAction("ExpenseRequests");
        }

        [HttpGet]
        public async Task<IActionResult> RefuseExpense(Guid id)
        {
            var expense = await _expenseService.GetVMById(id);
            await _expenseService.DeleteExpense(id);

            _toast.AddSuccessToastMessage(Messages.Expense.Refuse(expense.ShortDescription), new ToastrOptions { Title = "Refusing Expense" });

            string subject = "Your Expense Request Refused";
            string body = $"Your expense request for '{expense.ShortDescription}' refused.";

            _emailService.SendMail(expense.ExpenseBy.Email, subject, body);

            return RedirectToAction("ExpenseRequests");
        }

        [HttpGet]
        public IActionResult ExpenseExport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExpenseExcel(ExcelDateVM dates)
        {
            var stream = new MemoryStream();
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            var date = DateTime.Now;
            var startDate = dates.Start;
            var endDate = dates.End;
            var endDateHours = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            List<ExpenseVM> allExpenseList = await _expenseService.GetAllExpenses(user.CompanyId);
            List<ExpenseVM> expenseList = allExpenseList.Where(x => x.CreateDate >= startDate && x.CreateDate <= endDateHours).ToList();

            ExcelPackage pck = new ExcelPackage(stream);
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");


            ws.Cells["A1"].Value = "Expense Report";
            ws.Cells["B1"].Value = "Created at";
            ws.Cells["C1"].Value = date.ToShortDateString();
            ws.Cells["A2"].Value = startDate.ToShortDateString();
            ws.Cells["B2"].Value = "to";
            ws.Cells["C2"].Value = endDate.ToShortDateString();
            ws.Cells["A1:C2"].Style.Font.Bold = true;


            ws.Cells["A8"].Value = "Expense By";
            ws.Cells["B8"].Value = "Approved By";
            ws.Cells["C8"].Value = "Expense Type";
            ws.Cells["D8"].Value = "Expense Date";
            ws.Cells["E8"].Value = "Amount";
            ws.Cells["F8"].Value = "Currency";
            ws.Cells["G8"].Value = "Short Description";
            ws.Cells["H8"].Value = "Long Description";
            ws.Cells["I8"].Value = "Status";

            ws.Cells["A8:I8"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A8:I8"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A8:I8"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A8:I8"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A8:I8"].Style.Font.Bold = true;

            int rowStart = 5;

            decimal? totalApprovedAmount = 0;
            decimal? totalPendingAmount = 0;
            decimal? totalAmount = 0;

            decimal? totalApprovedPenny = 0;
            decimal? totalPendingPenny = 0;
            decimal? totalPenny = 0;

            foreach (var expense in expenseList)
            {
                if (expense.Currency == Domain.Enums.Currency.TL)
                {
                    totalAmount += expense.Amount;
                    totalPenny += expense.Penny / 100m;
                }
                if (expense.Currency == Domain.Enums.Currency.USD)
                {
                    totalAmount += (expense.Amount * 20.15m);
                    totalPenny += (expense.Penny * 20.15m) / 100m;
                }
                if (expense.Currency == Domain.Enums.Currency.EUR)
                {
                    totalAmount += (expense.Amount * 21.58m);
                    totalPenny += (expense.Penny * 21.58m) / 100m;
                }



                if (expense.Status == Domain.Enums.Status.Passive)
                {
                    if (expense.Currency == Domain.Enums.Currency.TL)
                    {
                        totalPendingAmount += expense.Amount;
                        totalPendingPenny += expense.Penny / 100m;
                    }
                    if (expense.Currency == Domain.Enums.Currency.USD)
                    {
                        totalPendingAmount += (expense.Amount * 20.15m);
                        totalPendingPenny += (expense.Penny * 20.15m) / 100m;
                    }
                    if (expense.Currency == Domain.Enums.Currency.EUR)
                    {
                        totalPendingAmount += (expense.Amount * 21.58m);
                        totalPendingPenny += (expense.Penny * 21.58m) / 100m;
                    }

                }

                if (expense.Status == Domain.Enums.Status.Active || expense.Status == Domain.Enums.Status.Modified)
                {
                    if (expense.Currency == Domain.Enums.Currency.TL)
                    {
                        totalApprovedAmount += expense.Amount;
                        totalApprovedPenny += expense.Penny / 100m;
                    }
                    if (expense.Currency == Domain.Enums.Currency.USD)
                    {
                        totalApprovedAmount += (expense.Amount * 20.15m);
                        totalApprovedPenny += (expense.Penny * 20.15m) / 100m;
                    }
                    if (expense.Currency == Domain.Enums.Currency.EUR)
                    {
                        totalApprovedAmount += (expense.Amount * 21.58m);
                        totalApprovedPenny += (expense.Penny * 21.58m) / 100m;
                    }
                }
            }

            totalAmount += totalPenny;
            totalApprovedAmount += totalApprovedPenny;
            totalPendingAmount += totalPendingPenny;

            ws.Cells[string.Format("A{0}", rowStart)].Value = "Total Approved Expenses Amount (TL): ";
            ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("B{0}", rowStart)].Value = totalApprovedAmount;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("A{0}", rowStart)].Style.Font.Bold = true;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Font.Bold = true;
            rowStart++;

            ws.Cells[string.Format("A{0}", rowStart)].Value = "Total Pending Expenses Amount (TL): ";
            ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("B{0}", rowStart)].Value = totalPendingAmount;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("A{0}", rowStart)].Style.Font.Bold = true;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Font.Bold = true;
            rowStart++;

            ws.Cells[string.Format("A{0}", rowStart)].Value = "Total Expense Amount (TL): ";
            ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("B{0}", rowStart)].Value = totalAmount;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("A{0}", rowStart)].Style.Font.Bold = true;
            ws.Cells[string.Format("B{0}", rowStart)].Style.Font.Bold = true;
            rowStart++;
            rowStart++;

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
                    ws.Cells[string.Format("I{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                    ws.Cells[string.Format("A{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("B{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("C{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("D{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("E{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("F{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("G{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("H{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("I{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
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

                ws.Cells[string.Format("E{0}", rowStart)].Value = expense.Penny < 10 ? $"{expense.Amount},0{expense.Penny}" : $"{expense.Amount},{expense.Penny}";
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("F{0}", rowStart)].Value = expense.Currency.ToString();
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("G{0}", rowStart)].Value = expense.ShortDescription;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("H{0}", rowStart)].Value = expense.LongDescription;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("I{0}", rowStart)].Value = expense.Status == Domain.Enums.Status.Passive ? "In Pending" : "Active";
                ws.Cells[string.Format("I{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("I{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("I{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("I{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


                rowStart++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            pck.Save();
            stream.Position = 0;

            var report = new CreateReportDTO()
            {
                Id = Guid.NewGuid(),
                Name = $"Expense_Report_{startDate.Day}{startDate.Month}{startDate.Year}_{endDateHours.Day}{endDateHours.Month}{endDateHours.Year}_{date.Day}{date.Month}{date.Year}",
                ReportPath = "..\\Reports\\" + Guid.NewGuid() + ".xlsx",
                CreatorId = user.Id,
                FileType = FileType.xls,
            };

            using (FileStream file = new FileStream(report.ReportPath, FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                file.Write(bytes, 0, bytes.Length);
                stream.Close();
            }

            await _reportService.Create(report);

            return new FileStreamResult(new FileStream(report.ReportPath, FileMode.Open, FileAccess.Read), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [HttpPost]
        public async Task<FileResult> ExpensePDF(ExcelDateVM dates)
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var expenses = await _expenseService.GetAllExpenses(user.CompanyId);

            var date = DateTime.Now;
            var startDate = dates.Start;
            var endDate = dates.End;
            var endDateHours = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            List<ExpenseVM> customers = expenses.Where(x => x.Status != Domain.Enums.Status.Deleted).Where(x => x.CreateDate >= startDate && x.CreateDate <= endDateHours).ToList();

            decimal? totalApprovedAmount = 0;
            decimal? totalPendingAmount = 0;
            decimal? totalAmount = 0;

            decimal? totalApprovedPenny = 0;
            decimal? totalPendingPenny = 0;
            decimal? totalPenny = 0;

            foreach (var expense in customers)
            {
                if (expense.Currency == Domain.Enums.Currency.TL)
                {
                    totalAmount += expense.Amount;
                    totalPenny += expense.Penny / 100m;
                }
                if (expense.Currency == Domain.Enums.Currency.USD)
                {
                    totalAmount += (expense.Amount * 20.15m);
                    totalPenny += (expense.Penny * 20.15m) / 100m;
                }
                if (expense.Currency == Domain.Enums.Currency.EUR)
                {
                    totalAmount += (expense.Amount * 21.58m);
                    totalPenny += (expense.Penny * 21.58m) / 100m;
                }



                if (expense.Status == Domain.Enums.Status.Passive)
                {
                    if (expense.Currency == Domain.Enums.Currency.TL)
                    {
                        totalPendingAmount += expense.Amount;
                        totalPendingPenny += expense.Penny / 100m;
                    }
                    if (expense.Currency == Domain.Enums.Currency.USD)
                    {
                        totalPendingAmount += (expense.Amount * 20.15m);
                        totalPendingPenny += (expense.Penny * 20.15m) / 100m;
                    }
                    if (expense.Currency == Domain.Enums.Currency.EUR)
                    {
                        totalPendingAmount += (expense.Amount * 21.58m);
                        totalPendingPenny += (expense.Penny * 21.58m) / 100m;
                    }

                }

                if (expense.Status == Domain.Enums.Status.Active || expense.Status == Domain.Enums.Status.Modified)
                {
                    if (expense.Currency == Domain.Enums.Currency.TL)
                    {
                        totalApprovedAmount += expense.Amount;
                        totalApprovedPenny += expense.Penny / 100m;
                    }
                    if (expense.Currency == Domain.Enums.Currency.USD)
                    {
                        totalApprovedAmount += (expense.Amount * 20.15m);
                        totalApprovedPenny += (expense.Penny * 20.15m) / 100m;
                    }
                    if (expense.Currency == Domain.Enums.Currency.EUR)
                    {
                        totalApprovedAmount += (expense.Amount * 21.58m);
                        totalApprovedPenny += (expense.Penny * 21.58m) / 100m;
                    }
                }
            }

            totalAmount += totalPenny;
            totalApprovedAmount += totalApprovedPenny;
            totalPendingAmount += totalPendingPenny;

            //Building an HTML string.
            StringBuilder sb = new StringBuilder();

            //Table start.
            sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-family: Arial; font-size: 10pt;'>");

            //Counts
            #region Total Approved Expenses Amount
            sb.Append("<tr style='border: none'>");

            sb.Append("<td style='font-weight: bold;border: 1px solid #ccc'>");
            sb.Append("Total Approved Expenses Amount (TL): ");
            sb.Append("</td>");

            sb.Append("<td style='font-weight: bold;border: 1px solid #ccc'>");
            sb.Append(totalApprovedAmount);
            sb.Append("</td>");

            sb.Append("</tr>");
            #endregion

            #region Total Pending Expenses Amount
            sb.Append("<tr style='border: none'>");

            sb.Append("<td style='font-weight: bold;border: 1px solid #ccc'>");
            sb.Append("Total Pending Expenses Amount (TL): ");
            sb.Append("</td>");

            sb.Append("<td style='font-weight: bold;border: 1px solid #ccc'>");
            sb.Append(totalPendingAmount);
            sb.Append("</td>");

            sb.Append("</tr>");
            #endregion

            #region Total Expenses Amount
            sb.Append("<tr style='border: none'>");

            sb.Append("<td style='font-weight: bold;border: 1px solid #ccc'>");
            sb.Append("Total Expenses Amount (TL): ");
            sb.Append("</td>");

            sb.Append("<td style='font-weight: bold;border: 1px solid #ccc'>");
            sb.Append(totalAmount);
            sb.Append("</td>");

            sb.Append("</tr>");
            #endregion

            //Building the Header row.
            sb.Append("<tr>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Expense By</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Approved By</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Expense Type</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Expense Date</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Amount</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Currency</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Short Description</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Long Description</th>");
            sb.Append("</tr>");

            //Building the Data rows.
            foreach (ExpenseVM expense in customers)
            {
                if (expense.Status == Domain.Enums.Status.Passive)
                {
                    sb.Append("<tr style='background-color: #ffc0cb'>");
                }

                else
                {
                    sb.Append("<tr>");
                }
                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append($"{expense.ExpenseBy.Name} {expense.ExpenseBy.SecondName} {expense.ExpenseBy.Surname}");
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append($"{expense.ApprovedBy.Name} {expense.ApprovedBy.SecondName} {expense.ApprovedBy.Surname}");
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(expense.Type);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(expense.ExpenseDate.ToShortDateString());
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(expense.Penny < 10 ? $"{expense.Amount},0{expense.Penny}" : $"{expense.Amount},{expense.Penny}");
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(expense.Currency.ToString());
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(expense.ShortDescription);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(expense.LongDescription);
                sb.Append("</td>");

                sb.Append("</tr>");
            }

            //Table end.
            sb.Append("</table>");

            MemoryStream stream = new MemoryStream();
            StringReader sr = new StringReader(sb.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 30f, 10f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();

            var report = new CreateReportDTO()
            {
                Id = Guid.NewGuid(),
                Name = $"Expense_Report_{startDate.Day}{startDate.Month}{startDate.Year}_{endDateHours.Day}{endDateHours.Month}{endDateHours.Year}_{date.Day}{date.Month}{date.Year}",
                ReportPath = "..\\Reports\\" + Guid.NewGuid() + ".pdf",
                CreatorId = user.Id,
                FileType = FileType.pdf,
            };

            using (FileStream file = new FileStream(report.ReportPath, FileMode.Create, System.IO.FileAccess.Write))
            {
                var memoryStream = new MemoryStream(stream.ToArray());
                byte[] bytes = new byte[memoryStream.Length];
                memoryStream.Read(bytes, 0, (int)memoryStream.Length);
                file.Write(bytes, 0, bytes.Length);
            }

            await _reportService.Create(report);

            return new FileStreamResult(new FileStream(report.ReportPath, FileMode.Open, FileAccess.Read), "application/pdf");

        }
    }
}