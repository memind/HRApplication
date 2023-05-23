using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Domain.Enums;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using OfficeOpenXml;
using System.Drawing;
using static IKApplication.MVC.ResultMessages.Messages;

namespace IKApplication.MVC.Areas.SiteAdministrator.Controllers
{
    [Area("SiteAdministrator")]
    [Authorize(Roles = "Site Administrator")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyService;
        private readonly IToastNotification _toast;
        private readonly IEmailService _emailService;

        public UserController(IAppUserService appUserSerives, ICompanyService companyService, IToastNotification toast, IEmailService emailService)
        {
            _appUserService = appUserSerives;
            _companyService = companyService;
            _toast = toast;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Todo: Get all users and send to viewawait _appUserService.GetById(userId);
            var users = await _appUserService.GetAllUsers();
            ViewBag.Area = "Site Administrator";
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
                await _appUserService.UpdateUser(user);
                _toast.AddSuccessToastMessage(Messages.User.Accept(user.Email), new ToastrOptions { Title = "User Request" });
            }
            else
            {
                _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "User Request" });
            }

            var subject = "Registration Request Accepted";
            var body = "Your registration request has been accepted. To Login, click the link : ikapp.azurewebsites.net";
            _emailService.SendMail(user.Email, subject, body);

            return RedirectToAction("RegistrationList");
        }

        public async Task<IActionResult> DeclineRegistration(Guid userId, Guid companyId)
        {
            await _appUserService.Delete(userId);
            await _companyService.Delete(companyId);

            var user = await _appUserService.GetById(userId);

            var subject = "Registration Request Declined";
            var body = "We are sorry to tell you that your registration request has been declined. Check your information and register again";
            _emailService.SendMail(user.Email, subject, body);

            return RedirectToAction("RegistrationList");
        }

        public async Task<IActionResult> ProfileDetails()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            ViewBag.Title = "Profile Details";
            return View("Update", user);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var user = await _appUserService.GetById(id);
            ViewBag.Title = "Update User";
            return View("Update", user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateDTO user)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.UpdateUser(user);
                _toast.AddSuccessToastMessage(Messages.User.Update(user.Email), new ToastrOptions { Title = "Updating User" });
                return RedirectToAction("Index", "Dashboard");
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating User" });
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _appUserService.GetById(id);
            await _appUserService.Delete(id);
            _toast.AddSuccessToastMessage(Messages.Personal.Delete(user.Email), new ToastrOptions { Title = "Deleting User" });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UserExcel()
        {
            var stream = new MemoryStream();

            var date = DateTime.Now;
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = new DateTime(date.Year, date.Month + 1, 1);

            List<AppUserVM> allUsersList = await _appUserService.GetAllUsers();

            ExcelPackage pck = new ExcelPackage(stream);
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Users");

            ws.Cells["A1"].Value = "All Users";
            ws.Cells["A1"].Style.Font.Bold = true;

            ws.Cells["A2"].Value = "Date";
            ws.Cells["B2"].Value = $"{date.Month} - {date.Year}";

            ws.Cells["A5"].Value = "Full Name";
            ws.Cells["B5"].Value = "Email Address";
            ws.Cells["C5"].Value = "Identity Number";
            ws.Cells["D5"].Value = "Birth Date";
            ws.Cells["E5"].Value = "Blood Group";
            ws.Cells["F5"].Value = "Company Name";
            ws.Cells["G5"].Value = "Title";
            ws.Cells["H5"].Value = "Profession";
            ws.Cells["I5"].Value = "Role";

            ws.Cells["A5:I5"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5:I5"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5:I5"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5:I5"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5:I5"].Style.Font.Bold = true;

            int rowStart = 6;

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

            ws.Cells[string.Format("H{0}", rowStart)].Value = "Total User Count: ";
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("I{0}", rowStart)].Value = allUsersList.Count;
            ws.Cells[string.Format("I{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("I{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("I{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("I{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("I{0}", rowStart)].Style.Font.Bold = true;
            ws.Cells[string.Format("H{0}", rowStart)].Style.Font.Bold = true;

            ws.Cells["A:AZ"].AutoFitColumns();
            pck.Save();
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"All_Users_Report_{date.Day}/{date.Month}/{date.Year}.xlsx");
        }
    }
}
