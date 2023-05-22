using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using OfficeOpenXml;
using static IKApplication.MVC.ResultMessages.Messages;

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
        private readonly IMapper _mapper;

        public UserController(IAppUserService appUserSerives, IToastNotification toast, ICompanyService companyService, ITitleService titleService, IMapper mapper, IEmailService emailService)
        {
            _appUserService = appUserSerives;
            _toast = toast;
            _companyService = companyService;
            _titleService = titleService;
            _mapper = mapper;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            //Todo: Get all users and send to view
            var users = await _appUserService.GetUsersByCompany(user.CompanyId);
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> ProfileDetails()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            ViewBag.Title = "Profile Details";
            return View("Update", user);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            ViewBag.Title = "Update User";
            return View(await _appUserService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateDTO user)
        {
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

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating User" });
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCompanyManager()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            var companies = await _companyService.GetAllCompanies();
            var titles = await _titleService.GetAllTitles();
            var companyTitles = titles.Where(x => x.CompanyId == user.CompanyId).ToList();

            var model = new AppUserCreateDTO() { CompanyId = user.CompanyId, Companies = companies, Titles = titles, Password = "123", ConfirmPassword = "123" };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyManager(AppUserCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.CreateUser(model, "Company Administrator");

                var user = await _appUserService.GetByUserName(model.Email);
                user.Password = "123";

                await _appUserService.UpdateUser(user);

                var company = await _companyService.GetById(user.CompanyId);
                var create = _appUserService.AddCompanyManager(model, company);

                string subject = "Get Your Password";
                string body = "To set your password, please click the link below: https://ikapp.azurewebsites.net/Account/ForgotPassword";
                _emailService.SendMail(model.Email, subject, body);


                _toast.AddSuccessToastMessage(Messages.CompanyAdmin.Create(model.Email), new ToastrOptions { Title = "Creating Company Manager" });
                return RedirectToAction("Index");
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Company Manager" });

            var currentUser = await _appUserService.GetByUserName(User.Identity.Name);
            var companies = await _companyService.GetAllCompanies();
            var titles = await _titleService.GetAllTitles();
            var companyTitles = titles.Where(x => x.CompanyId == currentUser.CompanyId).ToList();

            model.CompanyId = currentUser.CompanyId;
            model.Companies = companies;
            model.Titles = titles;
            model.Password = "123";
            model.ConfirmPassword = "123";

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePersonal()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            var companies = await _companyService.GetAllCompanies();
            var titles = await _titleService.GetAllTitles();
            var companyTitles = titles.Where(x => x.CompanyId == user.CompanyId).ToList();

            var model = new AppUserCreateDTO() { CompanyId = user.CompanyId, Companies = companies, Titles = titles, Password = "123", ConfirmPassword = "123" };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonal(AppUserCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                await _appUserService.CreateUser(model, "Personal");

                var user = await _appUserService.GetByUserName(model.Email);
                user.Password = "123";

                await _appUserService.UpdateUser(user);

                string subject = "Get Your Password";
                string body = "To set your password, please click the link below: https://ikapp.azurewebsites.net/Account/ForgotPassword";
                _emailService.SendMail(model.Email, subject, body);


                _toast.AddSuccessToastMessage(Messages.Personal.Create(model.Email), new ToastrOptions { Title = "Creating Personal" });
                return RedirectToAction("Index");
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Personal" });

            var currentUser = await _appUserService.GetByUserName(User.Identity.Name);
            var companies = await _companyService.GetAllCompanies();
            var titles = await _titleService.GetAllTitles();
            var companyTitles = titles.Where(x => x.CompanyId == currentUser.CompanyId).ToList();

            model.CompanyId = currentUser.CompanyId;
            model.Companies = companies;
            model.Titles = titles;
            model.Password = "123";
            model.ConfirmPassword = "123";

            return View(model);
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
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            var date = DateTime.Now;
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = new DateTime(date.Year, date.Month + 1, 1);

            List<AppUserVM> allUsers = await _appUserService.GetAllUsers();
            List<AppUserVM> allUsersList = allUsers.Where(x => x.CompanyId == user.CompanyId).ToList();

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

            ws.Cells[string.Format("H{0}", rowStart)].Value = "Total Personal Count: ";
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
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"All_Users_Report_{date.Month}/{date.Year}.xlsx");
        }
    }
}
