using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.ExcelVMs;
using IKApplication.Application.VMs.LeaveVMs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using OfficeOpenXml;
using SixLabors.ImageSharp.ColorSpaces.Companding;
using System.Text;
using static IKApplication.MVC.ResultMessages.Messages;
using IKApplication.Application.DTOs.ReportDTOs;

namespace IKApplication.MVC.CompanyAdministratorControllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IToastNotification _toast;
        private readonly ICompanyService _companyService;
        private readonly ITitleService _titleService;
        private readonly IProfessionService _professionService;
        private readonly IMapper _mapper;
        private readonly IReportService _reportService;

        public UserController(IAppUserService appUserSerives, IToastNotification toast, ICompanyService companyService, ITitleService titleService, IMapper mapper, IEmailService emailService, UserManager<AppUser> userManager, IProfessionService professionService, IReportService reportService)
        {
            _appUserService = appUserSerives;
            _toast = toast;
            _companyService = companyService;
            _titleService = titleService;
            _mapper = mapper;
            _emailService = emailService;
            _userManager = userManager;
            _professionService = professionService;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            var users = await _appUserService.GetUsersByCompany(user.CompanyId);
            ViewBag.Header = "Staff";
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> StaffCards()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            var users = await _appUserService.GetUsersByCompany(user.CompanyId);
            return View(users);
        }

        [HttpGet]
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
            ViewBag.Title = "Update User";
            var model = await _appUserService.GetById(id);
            var patron = await _appUserService.GetCurrentUserInfo(id);

            ViewBag.Patron = $"{patron.Patron.Name} {patron.Patron.SecondName} {patron.Patron.Surname}";
            model.Professions = await _professionService.GetAllProfessions();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateDTO user)
        {
            user.Profession = null;

            if (ModelState.IsValid)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    await _appUserService.UpdateUser(user);
                    _toast.AddSuccessToastMessage(Messages.User.Update(user.Email), new ToastrOptions { Title = "Updating User" });
                    return RedirectToAction("Index");
                }
                else
                {
                    _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating User" });
                    return View(user);
                }
            }

            var currentUser = await _appUserService.GetByUserName(User.Identity.Name);
            var companies = await _companyService.GetAllCompanies();
            user.CompanyId = currentUser.CompanyId;
            user.PatronId = currentUser.Id;
            user.Companies = companies;
            user.Professions = await _professionService.GetAllProfessions();
            user.Titles = await _titleService.GetAllTitles();
            var patron = await _appUserService.GetCurrentUserInfo(user.Id);

            ViewBag.Patron = $"{patron.Patron.Name} {patron.Patron.SecondName} {patron.Patron.Surname}";
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating User" });
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCompanyManagerAndPersonal()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            var companies = await _companyService.GetAllCompanies();
            var titles = await _titleService.GetAllTitles();
            var companyTitles = titles.Where(x => x.CompanyId == user.CompanyId).ToList();
            var professions = await _professionService.GetCompanyProfessions(user.CompanyId);

            var model = new AppUserCreateDTO() { CompanyId = user.CompanyId, Companies = companies, Titles = titles, Password = "123", ConfirmPassword = "123", Professions = professions };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCompanyManagerAndPersonal(AppUserCreateDTO model)
        {
            var patron = await _appUserService.GetByUserName(User.Identity.Name);
            if (ModelState.IsValid)
            {
                model.PatronId = patron.Id;
                model.Id = Guid.NewGuid();
                model.Password = "123";
                model.ConfirmPassword = "123";
                var company = await _companyService.GetById(model.CompanyId);
                model.Email = model.Name.ToLower() + "." + model.Surname.ToLower() + "@" + company.Name + ".com";
                string role = model.Role;

                if (role == "companyManager")   // role burada belirlenecek
                {
                    await _appUserService.CreateUser(model, "Company Administrator");
                    await _appUserService.AddCompanyManager(model, company);
                }
                else if (role == "personal")
                {
                    await _appUserService.CreateUser(model, "Personal");

                }
                model.Email = _appUserService.ReplaceInvalidChars(model.Name.ToLower()) + "." + _appUserService.ReplaceInvalidChars(model.Surname.ToLower()) + "@" + _appUserService.ReplaceInvalidChars(company.Name.ToLower()) + ".com";
                var user = await _userManager.FindByEmailAsync(model.Email);

                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("SetPassword", "User", new { email = user.Email, Code = code });
                string subject = "Set Your Password";
                string body = "To set your password, please click the link below: hrapplication.azurewebsites.net" + callbackUrl;

                _emailService.SendMail(model.PersonalEmail, subject, body);

                _toast.AddSuccessToastMessage(Messages.CompanyAdminAndPersonal.Create(model.Email), new ToastrOptions { Title = "Creating User" });
                return RedirectToAction("Index", "User");
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating User" });

            var currentUser = await _appUserService.GetByUserName(User.Identity.Name);
            var companies = await _companyService.GetAllCompanies();
            var titles = await _titleService.GetAllTitles();
            var companyTitles = titles.Where(x => x.CompanyId == currentUser.CompanyId).ToList();
            var professions = await _professionService.GetCompanyProfessions(patron.CompanyId);

            model.CompanyId = currentUser.CompanyId;
            model.PatronId = currentUser.Id;
            model.Companies = companies;
            model.Titles = titles;
            model.Professions = professions;
            model.Password = "123";
            model.ConfirmPassword = "123";

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> SetPassword(string email, string Code)
        {
            var model = new ResetPasswordVM();

            if (email != null && Code != null)
            {
                model.Code = Code;
                model.Email = email;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> SetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                user.Status = Status.Active;
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

                if (result.Succeeded)
                {
                    var mailUser = await _appUserService.GetCurrentUserInfo(model.Email);

                    string subject = "New Personal";
                    string body = $"Personal {mailUser.Name} {mailUser.SecondName} {mailUser.Surname} has been active.";

                    _emailService.SendMail(mailUser.Patron.PersonalEmail, subject, body);

                    _toast.AddSuccessToastMessage(Messages.ResetPasswordMessage.Set(), new ToastrOptions { Title = "Success" });
                    return RedirectToAction("Login", "Account", new { Area = "" });
                }
            }

            _toast.AddErrorToastMessage(Messages.ResetPasswordMessage.Error(), new ToastrOptions { Title = "Error" });

            return View(model);
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
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            var date = DateTime.Now;
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = new DateTime(date.Year, date.Month + 1, 1);

            List<AppUserVM> allUsersList = await _appUserService.GetUsersByCompany(user.CompanyId);

            ExcelPackage pck = new ExcelPackage(stream);
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Users");

            ws.Cells["A1"].Value = "Staff List";
            ws.Cells["A1"].Style.Font.Bold = true;

            ws.Cells["A2"].Value = "Date";
            ws.Cells["B2"].Value = $"{date.Day} - {date.Month} - {date.Year}";
            ws.Cells["A2:B2"].Style.Font.Bold = true;

            ws.Cells["A5"].Value = "Total Personal Count: ";
            ws.Cells["A5"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells["B5"].Value = allUsersList.Count;
            ws.Cells["B5"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["B5"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["B5"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["B5"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells["A5"].Style.Font.Bold = true;
            ws.Cells["B5"].Style.Font.Bold = true;

            ws.Cells["A6"].Value = "Full Name";
            ws.Cells["B6"].Value = "Email Address";
            ws.Cells["C6"].Value = "Identity Number";
            ws.Cells["D6"].Value = "Birth Date";
            ws.Cells["E6"].Value = "Blood Group";
            ws.Cells["F6"].Value = "Company Name";
            ws.Cells["G6"].Value = "Title";
            ws.Cells["H6"].Value = "Profession";

            ws.Cells["A6:H6"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:H6"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:H6"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:H6"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:H6"].Style.Font.Bold = true;

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

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            pck.Save();
            stream.Position = 0;

            var report = new CreateReportDTO()
            {
                Id = Guid.NewGuid(),
                Name = $"All_Users_Report_{date.Month}/{date.Year}",
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

        [HttpGet]
        public async Task<FileResult> UserPDF(ExcelDateVM dates)
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            var date = DateTime.Now;
            var startDate = dates.Start;
            var endDate = dates.End;
            var endDateHours = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            List<AppUserVM> users = await _appUserService.GetUsersByCompany(user.CompanyId);

            //Building an HTML string.
            StringBuilder sb = new StringBuilder();

            //Table start.
            sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-family: Arial; font-size: 10pt;'>");

            //Total count.
            sb.Append("<tr>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Total Personal Count: </th>");
            sb.Append($"<th style='font-weight: bold;border: 1px solid #ccc'>{users.Count()}</th>");
            sb.Append("</tr>");

            //Building the Header row.
            sb.Append("<tr>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Full Name</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Email Address</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Identity Number</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Birth Date</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Blood Group</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Company Name</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Title</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Profession</th>");
            sb.Append("</tr>");

            //Building the Data rows.
            foreach (AppUserVM personal in users)
            {
                sb.Append("<tr>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append($"{personal.FullName}");
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append($"{personal.Email}");
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(personal.IdentityNumber);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(personal.BirthDate.ToShortDateString());
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(personal.BloodGroup);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(personal.CompanyName);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(personal.Title.Name);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(personal.Profession.Name);
                sb.Append("</td>");

                sb.Append("</tr>");

            }

            //Table end.
            sb.Append("</table>");

            MemoryStream stream = new MemoryStream();
            StringReader sr = new StringReader(sb.ToString());
            Document pdfDoc = new Document(PageSize.A4, 2f, 2f, 30f, 10f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();

            var report = new CreateReportDTO()
            {
                Id = Guid.NewGuid(),
                Name = $"All_Users_Report_{date.Month}/{date.Year}",
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
