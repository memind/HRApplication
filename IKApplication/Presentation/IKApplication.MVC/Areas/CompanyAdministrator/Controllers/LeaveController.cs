using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.LeaveDTOs;
using IKApplication.Application.VMs.ExcelVMs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Application.VMs.LeaveVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
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

namespace IKApplication.MVC.Areas.CompanyAdministrator.Controllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
    public class LeaveController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;
        private readonly ILeaveService _leaveService;

        public LeaveController(IAppUserService appUserService, IMapper mapper, ILeaveService leaveService, ICompanyService companyService, IToastNotification toast, IEmailService emailService)
        {
            _appUserService = appUserService;
            _mapper = mapper;
            _leaveService = leaveService;
            _companyService = companyService;
            _toast = toast;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var leaveList = await _leaveService.GetAllLeaves(user.CompanyId);

            foreach (var leave in leaveList)
            {
                leave.PersonalFullName = await _leaveService.GetPersonalName(leave.AppUserId);
                leave.CurrentUserId = user.Id;
            }

            return View(leaveList);
        }


        [HttpGet]
        public async Task<IActionResult> LeaveRequests()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var leaveRequests = await _leaveService.GetLeaveRequests(user.CompanyId);

            foreach (var leave in leaveRequests)
            {
                leave.PersonalFullName = await _leaveService.GetPersonalName(leave.AppUserId);
            }

            return View(leaveRequests);
        }

        [HttpGet]
        public async Task<IActionResult> LeaveRequestDetails(Guid id)
        {
            var leave = await _leaveService.GetVMById(id);
            leave.PersonalFullName = await _leaveService.GetPersonalName(leave.AppUserId);
            return View(leave);
        }

        [HttpGet]
        public async Task<IActionResult> AcceptLeave(Guid id)
        {
            var leave = await _leaveService.GetVMById(id);
            await _leaveService.AcceptLeave(leave);

            _toast.AddSuccessToastMessage(Messages.Leaves.Accept(), new ToastrOptions { Title = "Accepting Leave" });

            string subject = "Your Leave Request Accepted";
            string body = $"Your leave request for '{leave.Explanation}' accepted.";

            _emailService.SendMail(leave.AppUser.Email, subject, body);

            return RedirectToAction("LeaveRequests");
        }

        [HttpGet]
        public async Task<IActionResult> RefuseLeave(Guid id)
        {
            var leave = await _leaveService.GetVMById(id);
            await _leaveService.Delete(id);

            _toast.AddSuccessToastMessage(Messages.Leaves.Refuse(), new ToastrOptions { Title = "Refusing Leave" });

            string subject = "Your Leave Request Refused";
            string body = $"Your leave request for '{leave.Explanation}' refused.";

            _emailService.SendMail(leave.AppUser.Email, subject, body);

            return RedirectToAction("LeaveRequests");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _leaveService.Delete(id);
            _toast.AddSuccessToastMessage(Messages.Leaves.Delete(), new ToastrOptions { Title = "Deleting Leave" });
            return RedirectToAction("Index", "Leave");
        }

        [HttpGet]
        public async Task<IActionResult> CreateLeave()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var leaveList = await _leaveService.GetAllLeaves(user.CompanyId);

            foreach (var leave in leaveList)
            {
                leave.PersonalFullName = await _leaveService.GetPersonalName(leave.AppUserId);
                leave.CurrentUserId = user.Id;
            }
            
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLeave(CreateLeaveDTO model)
        {
            var leaveFor = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            if (DateTime.Compare(model.StartDate, model.EndDate) > 0)
            {
                _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Leave" });
                return View(model);
            }

            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                model.AppUserId = leaveFor.Id;
                await _leaveService.Create(model, User.Identity.Name);

                _toast.AddSuccessToastMessage(Messages.Leaves.Create(), new ToastrOptions { Title = "Creating Leave" });

                var mailLeave = await _leaveService.GetVMById(model.Id);

                string subject = "New Leave Request Arrived";
                string body = $"The user {mailLeave.AppUser.Name} {mailLeave.AppUser.SecondName} {mailLeave.AppUser.Surname} requested a leave. See request by clicking the link: https://ikapp.azurewebsites.net/CompanyAdministrator/Leave/LeaveRequestDetails/{model.Id}?";

                _emailService.SendMail(mailLeave.AppUser.Patron.Email, subject, body);

                model.TotalLeaveDays = (int)(model.EndDate - model.StartDate).TotalDays;
                return RedirectToAction("Index", "Leave");
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Leave" });

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditLeave(Guid id)
        {
            var leave = await _leaveService.GetByID(id);
            leave.FullName = await _leaveService.GetPersonalName(leave.AppUserId);
            return View(leave);
        }

        [HttpPost]
        public async Task<IActionResult> EditLeave(UpdateLeaveDTO model)
        {
            var employee = await _appUserService.GetByUserName(User.Identity.Name);
            if (ModelState.IsValid)
            {
                try
                {
                    await _leaveService.Update(model);
                    _toast.AddSuccessToastMessage(Messages.Leaves.Update(), new ToastrOptions { Title = "Updating Leave" });

                }
                catch (Exception)
                {
                    _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Leave" });
                }
                return RedirectToAction("Index", "Leave");
            }
            else
            {
                _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Leave" });
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult LeaveExport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LeaveExcel(ExcelDateVM dates)
        {
            var stream = new MemoryStream();
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            var date = DateTime.Now;
            var startDate = dates.Start;
            var endDate = dates.End;
            var endDateHours = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            List<LeaveVM> allLeaveList = await _leaveService.GetAllLeaves(user.CompanyId);
            List<LeaveVM> leaveList = allLeaveList.Where(x => x.CreateDate >= startDate && x.CreateDate <= endDateHours).ToList();

            ExcelPackage pck = new ExcelPackage(stream);
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Leave Report";
            ws.Cells["B1"].Value = "Created at";
            ws.Cells["C1"].Value = date.ToShortDateString();
            ws.Cells["A2"].Value = startDate.ToShortDateString();
            ws.Cells["B2"].Value = "to";
            ws.Cells["C2"].Value = endDate.ToShortDateString();

            ws.Cells["A5"].Value = "Leave For";
            ws.Cells["B5"].Value = "Approved By";
            ws.Cells["C5"].Value = "Start Date";
            ws.Cells["D5"].Value = "End Date";
            ws.Cells["E5"].Value = "Explanation";
            ws.Cells["F5"].Value = "Leave Type";
            ws.Cells["G5"].Value = "Total Leave Days";
            ws.Cells["H5"].Value = "Status";

            ws.Cells["A5:H5"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5:H5"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5:H5"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5:H5"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5:H5"].Style.Font.Bold = true;

            int rowStart = 6;

            foreach (var leave in leaveList)
            {
                if (leave.Status == Domain.Enums.Status.Passive)
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

                ws.Cells[string.Format("A{0}", rowStart)].Value = $"{leave.AppUser.Name} {leave.AppUser.SecondName} {leave.AppUser.Surname}";
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("B{0}", rowStart)].Value = $"{leave.ApprovedBy.Name} {leave.ApprovedBy.SecondName} {leave.ApprovedBy.Surname}";
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("C{0}", rowStart)].Value = leave.StartDate.ToShortDateString();
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("D{0}", rowStart)].Value = leave.EndDate.ToShortDateString();
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("E{0}", rowStart)].Value = leave.Explanation;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("F{0}", rowStart)].Value = leave.LeaveType.ToString();
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("G{0}", rowStart)].Value = leave.TotalLeaveDays;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("H{0}", rowStart)].Value = leave.Status == Domain.Enums.Status.Passive ? "In Pending" : "Active";
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


                rowStart++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            pck.Save();
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Leave_Report_{startDate.Day}{startDate.Month}{startDate.Year}_{endDateHours.Day}{endDateHours.Month}{endDateHours.Year}_{date.Day}{date.Month}{date.Year}.xlsx");
        }

        [HttpPost]
        public async Task<FileResult> LeavePDF(ExcelDateVM dates)
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var allLeaves = await _leaveService.GetAllLeaves(user.CompanyId);

            var date = DateTime.Now;
            var startDate = dates.Start;
            var endDate = dates.End;
            var endDateHours = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            List<LeaveVM> leaves = allLeaves.Where(x => x.Status != Domain.Enums.Status.Deleted).Where(x => x.CreateDate >= startDate && x.CreateDate <= endDateHours).ToList();

            //Building an HTML string.
            StringBuilder sb = new StringBuilder();

            //Table start.
            sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-family: Arial; font-size: 10pt;'>");

            //Building the Header row.
            sb.Append("<tr>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Leave For</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Approved By</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Start Date</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>End Date</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Explanation</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Leave Type</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Total Leave Days</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Status</th>");
            sb.Append("</tr>");

            //Building the Data rows.
            foreach (LeaveVM leave in leaves)
            {
                if (leave.Status == Status.Passive)
                {
                    sb.Append("<tr style='background-color: #ffc0cb'>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append($"{leave.AppUser.Name} {leave.AppUser.SecondName} {leave.AppUser.Surname}");
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append($"{leave.ApprovedBy.Name} {leave.ApprovedBy.SecondName} {leave.ApprovedBy.Surname}");
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append(leave.StartDate.ToShortDateString());
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append(leave.EndDate.ToShortDateString());
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append(leave.Explanation);
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append(leave.LeaveType.ToString());
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append(leave.TotalLeaveDays.ToString());
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append(leave.Status == Domain.Enums.Status.Passive ? "In Pending" : "Approved");
                    sb.Append("</td>");

                    sb.Append("</tr>");
                }
                else
                {
                    sb.Append("<tr>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append($"{leave.AppUser.Name} {leave.AppUser.SecondName} {leave.AppUser.Surname}");
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append($"{leave.ApprovedBy.Name} {leave.ApprovedBy.SecondName} {leave.ApprovedBy.Surname}");
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append(leave.StartDate.ToShortDateString());
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append(leave.EndDate.ToShortDateString());
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append(leave.Explanation);
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append(leave.LeaveType.ToString());
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append(leave.TotalLeaveDays.ToString());
                    sb.Append("</td>");

                    sb.Append("<td style='border: 1px solid #ccc'>");
                    sb.Append(leave.Status == Domain.Enums.Status.Passive ? "In Pending" : "Approved");
                    sb.Append("</td>");

                    sb.Append("</tr>");
                }
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
            return File(stream.ToArray(), "application/pdf", $"Leave_Report_{startDate.Day}{startDate.Month}{startDate.Year}_{endDateHours.Day}{endDateHours.Month}{endDateHours.Year}_{date.Day}{date.Month}{date.Year}.pdf");

        }
    }
}
