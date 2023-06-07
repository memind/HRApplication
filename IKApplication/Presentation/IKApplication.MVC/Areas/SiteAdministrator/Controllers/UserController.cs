using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Domain.Enums;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using OfficeOpenXml;
using System.Drawing;
using System.Text;
using static IKApplication.MVC.ResultMessages.Messages;
using IKApplication.Domain.Entites;
using IKApplication.Application.DTOs.ReportDTOs;

namespace IKApplication.MVC.Areas.SiteAdministrator.Controllers
{
    [Area("SiteAdministrator")]
    [Authorize(Roles = "Site Administrator")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyService;
        private readonly IProfessionService _professionService;
        private readonly ITitleService _titleService;
        private readonly IToastNotification _toast;
        private readonly IEmailService _emailService;
        private readonly IReportService _reportService;

        public UserController(IAppUserService appUserSerives, ICompanyService companyService, IToastNotification toast, IEmailService emailService, IProfessionService professionService, ITitleService titleService, IReportService reportService)
        {
            _appUserService = appUserSerives;
            _companyService = companyService;
            _toast = toast;
            _emailService = emailService;
            _professionService = professionService;
            _titleService = titleService;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Todo: Get all users and send to viewawait _appUserService.GetById(userId);
            var users = await _appUserService.GetAllUsers();
            ViewBag.Area = "Site Administrator";
            ViewBag.Header = "User List";
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> RegistrationList()
        {
            var registrations = await _appUserService.GetAllRegistrations();
            return View(registrations);
        }

        [HttpGet]
        public async Task<IActionResult> RegistrationDetails(Guid userId, Guid companyId)
        {
            ViewBag.User = await _appUserService.GetById(userId);
            ViewBag.Company = await _companyService.GetById(companyId);
            return View();
        }
        public async Task<IActionResult> AcceptRegistration(Guid userId, Guid companyId)
        {
            var company = await _companyService.GetById(companyId);
            var user = await _appUserService.GetById(userId);
            if (company != null)
            {
                await _companyService.Update(company);
                _toast.AddSuccessToastMessage(Messages.Company.Accept(company.Name), new ToastrOptions { Title = "Company Request" });
            }
            else
            {
                _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Company Request" });
            }

            if (user != null)
            {
                Guid id = user.Id;
                user.PatronId = id;
                await _appUserService.UpdateUser(user);
                _toast.AddSuccessToastMessage(Messages.User.Accept(user.PersonalEmail), new ToastrOptions { Title = "User Request" });
            }
            else
            {
                _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "User Request" });
            }

            var subject = "Registration Request Accepted";
            var body = "Your registration request has been accepted. To Login, click the link : hrapplication.azurewebsites.net";
            _emailService.SendMail(user.PersonalEmail, subject, body);

            return RedirectToAction("RegistrationList");
        }

        public async Task<IActionResult> DeclineRegistration(Guid userId, Guid companyId)
        {
            await _appUserService.Delete(userId);
            await _companyService.Delete(companyId);

            var user = await _appUserService.GetById(userId);

            var subject = "Registration Request Declined";
            var body = "We are sorry to tell you that your registration request has been declined. Check your information and register again";
            _emailService.SendMail(user.PersonalEmail, subject, body);

            return RedirectToAction("RegistrationList");
        }

        public async Task<IActionResult> ProfileDetails()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            ViewBag.Title = "Profile Details";
            user.Professions = await _professionService.GetAllProfessions();
            var patron = await _appUserService.GetCurrentUserInfo(user.Id);

            ViewBag.Patron = $"{patron.Patron.Name} {patron.Patron.SecondName} {patron.Patron.Surname}";
            return View("Update", user);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var user = await _appUserService.GetById(id);
            var patron = await _appUserService.GetCurrentUserInfo(id);

            ViewBag.Patron = $"{patron.Patron.Name} {patron.Patron.SecondName} {patron.Patron.Surname}";
            ViewBag.Title = "Update User";

            user.Titles = await _titleService.GetCompanyTitles(user.CompanyId);
            user.Professions = await _professionService.GetAllProfessions();
            return View("Update", user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateDTO user)
        {
            user.Profession = null;
            if (ModelState.IsValid)
            {
                await _appUserService.UpdateUser(user);
                _toast.AddSuccessToastMessage(Messages.User.Update(user.Email), new ToastrOptions { Title = "Updating User" });
                return RedirectToAction("Index", "Dashboard");
            }
            var patron = await _appUserService.GetCurrentUserInfo(user.Id);

            ViewBag.Patron = $"{patron.Patron.Name} {patron.Patron.SecondName} {patron.Patron.Surname}";
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating User" });
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _appUserService.GetById(id);
            await _appUserService.Delete(id);
            _toast.AddSuccessToastMessage(Messages.CompanyAdminAndPersonal.Delete(user.Email), new ToastrOptions { Title = "Deleting User" });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UserExcel()
        {
            var stream = new MemoryStream();

            var date = DateTime.Now;

            List<AppUserVM> allUsersList = await _appUserService.GetAllUsers();
            var currentUser = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            ExcelPackage pck = new ExcelPackage(stream);
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Users");

            ws.Cells["A1"].Value = "All Users";
            ws.Cells["A1"].Style.Font.Bold = true;

            ws.Cells["A2"].Value = "Date";
            ws.Cells["B2"].Value = $"{date.Month} - {date.Year}";

            ws.Cells[string.Format("A5")].Value = "Total User Count: ";
            ws.Cells[string.Format("A5")].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("A5")].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("A5")].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("A5")].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("B5")].Value = allUsersList.Count;
            ws.Cells[string.Format("B5")].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("B5")].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("B5")].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("B5")].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("A5")].Style.Font.Bold = true;
            ws.Cells[string.Format("B5")].Style.Font.Bold = true;

            ws.Cells["A6"].Value = "Full Name";
            ws.Cells["B6"].Value = "Email Address";
            ws.Cells["C6"].Value = "Identity Number";
            ws.Cells["D6"].Value = "Birth Date";
            ws.Cells["E6"].Value = "Blood Group";
            ws.Cells["F6"].Value = "Company Name";
            ws.Cells["G6"].Value = "Title";
            ws.Cells["H6"].Value = "Profession";
            ws.Cells["I6"].Value = "Role";

            ws.Cells["A6:I6"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:I6"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:I6"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:I6"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:I6"].Style.Font.Bold = true;


            int rowStart = 7;

            foreach (var userData in allUsersList)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = userData.FullName;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("B{0}", rowStart)].Value = userData.Email;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("C{0}", rowStart)].Value = userData.IdentityNumber;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("D{0}", rowStart)].Value = userData.BirthDate.ToShortDateString();
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("E{0}", rowStart)].Value = userData.BloodGroup.ToString();
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("F{0}", rowStart)].Value = userData.CompanyName;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("G{0}", rowStart)].Value = userData.Title.Name;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("G{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("H{0}", rowStart)].Value = userData.Profession;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("I{0}", rowStart)].Value = userData.Roles;
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
                Name = $"All_Users_Report_{date.Day}/{date.Month}/{date.Year}",
                ReportPath = "..\\Reports\\" + Guid.NewGuid() + ".xlsx",
                CreatorId = currentUser.Id,
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

        [HttpGet]
        public async Task<FileResult> UserPDF()
        {
            // ffc0cb (pembe)
            var allUsers = await _appUserService.GetAllUsers();
            var currentUser = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            var date = DateTime.Now;

            List<AppUserVM> users = allUsers.ToList();

            //Building an HTML string.
            StringBuilder sb = new StringBuilder();

            //Table start.
            sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-family: Arial; font-size: 10pt;'>");

            //Building the Header row.
            sb.Append("<tr>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Full Name</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Email Address</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Identity Number</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Birth Date</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Company Name</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Title</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Role</th>");
            sb.Append("</tr>");

            //Building the Data rows.
            foreach (AppUserVM user in users)
            {
                if (user.Status == Domain.Enums.Status.Passive)
                {
                    sb.Append("<tr style='background-color: #ffc0cb'>");
                }

                else
                {
                    sb.Append("<tr>");
                }

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append($"{user.FullName}");
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(user.Email);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(user.IdentityNumber);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(user.BirthDate.ToShortDateString());
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(user.CompanyName);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(user.Title.Name);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(user.Roles[0]);
                sb.Append("</td>");

                sb.Append("</tr>");
            }

            #region Total User Count
            sb.Append("<tr style='border: none'>");

            sb.Append("<td style='border: 0px solid #ccc'>");
            sb.Append("");
            sb.Append("</td>");

            sb.Append("<td style='border: 0px solid #ccc'>");
            sb.Append("");
            sb.Append("</td>");

            sb.Append("<td style='border: 0px solid #ccc'>");
            sb.Append("");
            sb.Append("</td>");

            sb.Append("<td style='border: 0px solid #ccc'>");
            sb.Append("");
            sb.Append("</td>");

            sb.Append("<td style='border: 0px solid #ccc'>");
            sb.Append("");
            sb.Append("</td>");

            sb.Append("<td style='font-weight: bold;border: 1px solid #ccc'>");
            sb.Append("Total User Count: ");
            sb.Append("</td>");

            sb.Append("<td style='font-weight: bold;border: 1px solid #ccc'>");
            sb.Append(users.Count);
            sb.Append("</td>");

            sb.Append("</tr>");
            #endregion

            //Table end.
            sb.Append("</table>");

            MemoryStream stream = new MemoryStream();
            StringReader sr = new StringReader(sb.ToString());
            Document pdfDoc = new Document(PageSize.A3, 5f, 10f, 30f, 5f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();

            var report = new CreateReportDTO()
            {
                Id = Guid.NewGuid(),
                Name = $"All_Users_Report_{date.Day}/{date.Month}/{date.Year}",
                ReportPath = "..\\Reports\\" + Guid.NewGuid() + ".pdf",
                CreatorId = currentUser.Id,
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