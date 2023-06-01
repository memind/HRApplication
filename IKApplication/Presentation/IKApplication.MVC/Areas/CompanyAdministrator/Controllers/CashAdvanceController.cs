using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.CashAdvanceDTOs;
using IKApplication.Application.VMs.CashAdvanceVMs;
using IKApplication.Application.VMs.ExcelVMs;
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
using IKApplication.Application.DTOs.ReportDTOs;
using IKApplication.Domain.Enums;
using IKApplication.Application.VMs.UserVMs;
using Microsoft.VisualBasic.FileIO;

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
        private readonly IReportService _reportService;
        public CashAdvanceController(IMapper mapper, IToastNotification toast, IAppUserService appUserService, ICompanyService companyService, ICashAdvanceService cashAdvanceService, IEmailService emailService, IReportService reportService)
        {
            _mapper = mapper;
            _toast = toast;
            _appUserService = appUserService;
            _companyService = companyService;
            _cashAdvanceService = cashAdvanceService;
            _emailService = emailService;
            _reportService = reportService;
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

                _emailService.SendMail(mailAdvance.AdvanceTo.Patron.Email, subject, body);
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

            _toast.AddSuccessToastMessage(Messages.Advance.Accept($"{advance.AdvanceTo.Name} {advance.AdvanceTo.SecondName} {advance.AdvanceTo.Surname}"), new ToastrOptions { Title = "Accepting Expense" });

            string subject = "Your Advance Request Accepted";
            string body = $"Your cash advance request for '{advance.Description}' accepted.";

            _emailService.SendMail(advance.AdvanceTo.Email, subject, body);

            return RedirectToAction("CashAdvanceRequests");
        }

        [HttpGet]
        public async Task<IActionResult> RefuseCashAdvance(Guid id)
        {
            var advance = await _cashAdvanceService.GetVMById(id);
            await _cashAdvanceService.Delete(id);

            string subject = "Your Advance Request Refused";
            string body = $"Your cash advance request for '{advance.Description}' refused.";

            _emailService.SendMail(advance.AdvanceTo.Email, subject, body);

            _toast.AddSuccessToastMessage(Messages.Advance.Refuse($"{advance.AdvanceTo.Name} {advance.AdvanceTo.SecondName} {advance.AdvanceTo.Surname}"), new ToastrOptions { Title = "Refusing Advance" });
            return RedirectToAction("CashAdvanceRequests");
        }

        [HttpGet]
        public IActionResult CashAdvanceExport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CashAdvanceExcel(ExcelDateVM dates)
        {
            var stream = new MemoryStream();
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            var date = DateTime.Now;
            var startDate = dates.Start;
            var endDate = dates.End;
            var endDateHours = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            List<CashAdvanceVM> allAdvanceList = await _cashAdvanceService.GetAllAdvances(user.CompanyId);
            List<CashAdvanceVM> advanceList = allAdvanceList.Where(x => x.CreateDate >= startDate && x.CreateDate <= endDateHours).ToList();

            ExcelPackage pck = new ExcelPackage(stream);
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Cash Advance Report";
            ws.Cells["B1"].Value = "Created at";
            ws.Cells["C1"].Value = date.ToShortDateString();
            ws.Cells["A2"].Value = startDate.ToShortDateString();
            ws.Cells["B2"].Value = "to";
            ws.Cells["C2"].Value = endDate.ToShortDateString();
            ws.Cells["A1:C2"].Style.Font.Bold = true;

            ws.Cells["A6"].Value = "Advance To";
            ws.Cells["B6"].Value = "Approved By";
            ws.Cells["C6"].Value = "Requested Amount";
            ws.Cells["D6"].Value = "Currency";
            ws.Cells["E6"].Value = "Installment Count";
            ws.Cells["F6"].Value = "Description";
            ws.Cells["G6"].Value = "Create Date";
            ws.Cells["H6"].Value = "Status";

            ws.Cells["A6:H6"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:H6"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:H6"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:H6"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:H6"].Style.Font.Bold = true;

            int rowStart = 5;
            decimal totalAmount = 0;

            foreach (var advance in advanceList)
            {
                if (advance.Currency == Domain.Enums.Currency.TL)
                {
                    totalAmount += advance.RequestedAmount;
                }
                if (advance.Currency == Domain.Enums.Currency.USD)
                {
                    totalAmount += (advance.RequestedAmount * 20.15m);
                }
                if (advance.Currency == Domain.Enums.Currency.EUR)
                {
                    totalAmount += (advance.RequestedAmount * 21.58m);
                }
            }

            ws.Cells[string.Format("A{0}", rowStart)].Value = "Total Cash Advance Amount: ";
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

            foreach (var advance in advanceList)
            {
                if (advance.Status == Domain.Enums.Status.Passive)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[string.Format("B{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[string.Format("C{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[string.Format("D{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[string.Format("E{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[string.Format("F{0}", rowStart)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                    ws.Cells[string.Format("A{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("B{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("C{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("D{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("E{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Cells[string.Format("F{0}", rowStart)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = $"{advance.AdvanceTo.Name} {advance.AdvanceTo.SecondName} {advance.AdvanceTo.Surname}";
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("B{0}", rowStart)].Value = $"{advance.Director.Name} {advance.Director.SecondName} {advance.Director.Surname}";
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("C{0}", rowStart)].Value = advance.RequestedAmount;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("D{0}", rowStart)].Value = advance.Currency.ToString();
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("E{0}", rowStart)].Value = advance.InstallmentCount;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("F{0}", rowStart)].Value = advance.Description;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("G{0}", rowStart)].Value = advance.CreateDate.ToShortDateString();
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("H{0}", rowStart)].Value = advance.Status == Domain.Enums.Status.Passive ? "In Pending" : "Active";
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            pck.Save();
            stream.Position = 0;

            var report = new CreateReportDTO()
            {
                Id = Guid.NewGuid(),
                Name = $"Cash_Advance_Report_{startDate.Day}{startDate.Month}{startDate.Year}_{endDateHours.Day}{endDateHours.Month}{endDateHours.Year}_{date.Day}{date.Month}{date.Year}",
                ReportPath = "/Reports/" + Guid.NewGuid() + ".xlsx",
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
        public async Task<FileResult> CashAdvancePDF(ExcelDateVM dates)
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var allAdvances = await _cashAdvanceService.GetAllAdvances(user.CompanyId);

            var date = DateTime.Now;
            var startDate = dates.Start;
            var endDate = dates.End;
            var endDateHours = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            List<CashAdvanceVM> advances = allAdvances.Where(x => x.Status != Domain.Enums.Status.Deleted).Where(x => x.CreateDate >= startDate && x.CreateDate <= endDateHours).ToList();

            decimal totalAmount = 0;

            foreach (var advance in advances)
            {
                if (advance.Currency == Domain.Enums.Currency.TL)
                {
                    totalAmount += advance.RequestedAmount;
                }
                if (advance.Currency == Domain.Enums.Currency.USD)
                {
                    totalAmount += (advance.RequestedAmount * 20.15m);
                }
                if (advance.Currency == Domain.Enums.Currency.EUR)
                {
                    totalAmount += (advance.RequestedAmount * 21.58m);
                }
            }

            //Building an HTML string.
            StringBuilder sb = new StringBuilder();

            //Table start.
            sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-family: Arial; font-size: 10pt;'>");

            // Total Counts
            #region Total Cash Advance Amount
            sb.Append("<tr style='border: none'>");

            sb.Append("<td style='font-weight: bold;border: 1px solid #ccc'>");
            sb.Append("Total Cash Advance Amount: ");
            sb.Append("</td>");

            sb.Append("<td style='font-weight: bold;border: 1px solid #ccc'>");
            sb.Append(totalAmount);
            sb.Append("</td>");

            sb.Append("</tr>");
            #endregion

            //Building the Header row.
            sb.Append("<tr>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Advance To</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Approved By</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Requested Amount</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Currency</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Installment Count</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Description</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Create Date</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Status</th>");
            sb.Append("</tr>");

            //Building the Data rows.
            foreach (CashAdvanceVM advance in advances)
            {
                if (advance.Status == Domain.Enums.Status.Passive)
                {
                    sb.Append("<tr style='background-color: #ffc0cb'>");
                }
                else
                {
                    sb.Append("<tr>");
                }
                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append($"{advance.AdvanceTo.Name} {advance.AdvanceTo.SecondName} {advance.AdvanceTo.Surname}");
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append($"{advance.Director.Name} {advance.Director.SecondName} {advance.Director.Surname}");
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(advance.RequestedAmount);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(advance.Currency);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(advance.InstallmentCount);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(advance.Description);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(advance.CreateDate.ToShortDateString());
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(advance.Status == Domain.Enums.Status.Passive ? "In Pending" : "Approved");
                sb.Append("</td>");

                sb.Append("</tr>");
            }

            //Table end.
            sb.Append("</table>");

            MemoryStream stream = new MemoryStream();
            StringReader sr = new StringReader(sb.ToString());
            Document pdfDoc = new Document(PageSize.A4, 4f, 4f, 30f, 10f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();

            var report = new CreateReportDTO()
            {
                Id = Guid.NewGuid(),
                Name = $"Cash_Advance_Report_{startDate.Day}{startDate.Month}{startDate.Year}_{endDateHours.Day}{endDateHours.Month}{endDateHours.Year}_{date.Day}{date.Month}{date.Year}",
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