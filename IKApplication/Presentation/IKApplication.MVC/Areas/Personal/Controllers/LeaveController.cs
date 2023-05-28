using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.LeaveDTOs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using System.Data;
using static IKApplication.MVC.ResultMessages.Messages;

namespace IKApplication.MVC.Areas.Personal.Controllers
{
    [Area("Personal"), Authorize(Roles = "Personal")]
    public class LeaveController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly ILeaveService _leaveService;
        private readonly IToastNotification _toast;

        public LeaveController(IAppUserService appUserService, IMapper mapper, ILeaveService leaveService, ICompanyService companyService, IToastNotification toast, IEmailService emailService)
        {
            _appUserService = appUserService;
            _mapper = mapper;
            _leaveService = leaveService;
            _companyService = companyService;
            _toast = toast;
            _emailService = emailService;
        }

        public async Task<IActionResult> ListLeave()
        {
            var user = await _appUserService.GetByUserName(User.Identity.Name);
            var employeeLeaves = await _leaveService.GetPersonelLeaves(User.Identity.Name);
            return View(employeeLeaves);
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

            ViewBag.ApprovedBy = $"{user.Patron.Name} {user.Patron.SecondName} {user.Patron.Surname}";
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

                return RedirectToAction("ListLeave", "Leave", new { Area = "Personal" });
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Leave" });

            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            ViewBag.ApprovedBy = $"{user.Patron.Name} {user.Patron.SecondName} {user.Patron.Surname}";

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _leaveService.Delete(id);
            _toast.AddSuccessToastMessage(Messages.Leaves.Delete(), new ToastrOptions { Title = "Deleting Leave" });
            return RedirectToAction("ListLeave", "Leave");
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
                    model.AppUserId = employee.Id;
                    model.TotalLeaveDays = (int)(model.EndDate - model.StartDate).TotalDays;
                    await _leaveService.Update(model);
                    _toast.AddSuccessToastMessage(Messages.Leaves.Update(), new ToastrOptions { Title = "Updating Leave" });
                }
                catch (Exception)
                {
                    _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Leave" });
                }
                return RedirectToAction("ListLeave", "Leave");
            }
            else
            {
                 _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Leave" });
                return View(model);
            }
        }
    }
}
