using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.LeaveDTOs;
using IKApplication.Domain.Enums;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Data;

namespace IKApplication.MVC.Areas.CompanyAdministrator.Controllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
    public class LeaveController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toast;
        private readonly ILeaveService _leaveService;

        public LeaveController(IAppUserService appUserService, IMapper mapper, ILeaveService leaveService, ICompanyService companyService, IToastNotification toast)
        {
            _appUserService = appUserService;
            _mapper = mapper;
            _leaveService = leaveService;
            _companyService = companyService;
            _toast = toast;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var leaveList = await _leaveService.GetAllLeaves(user.CompanyId);

            foreach (var leave in leaveList)
            {
                leave.PersonalFullName = await _leaveService.GetPersonalName(leave.AppUserId);
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
            var leave = await _leaveService.GetByID(id);
            await _leaveService.Update(leave);

            _toast.AddSuccessToastMessage(Messages.Leaves.Accept(), new ToastrOptions { Title = "Accepting Leave" });
            return RedirectToAction("LeaveRequests");
        }

        [HttpGet]
        public async Task<IActionResult> RefuseLeave(Guid id)
        {
            var leave = await _leaveService.GetByID(id);
            await _leaveService.Delete(id);

            _toast.AddSuccessToastMessage(Messages.Leaves.Refuse(), new ToastrOptions { Title = "Refusing Leave" });
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
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLeave(CreateLeaveDTO model)
        {
            var leaveFor = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            var company = await _companyService.GetById(leaveFor.CompanyId);
            model.CompanyId = company.Id;

            if (DateTime.Compare(model.StartDate, model.EndDate) > 0)
            {
                _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Leave" });
                return View(model);
            }


            if (ModelState.IsValid)
            {
                await _leaveService.Create(model, User.Identity.Name);

                _toast.AddSuccessToastMessage(Messages.Leaves.Create(), new ToastrOptions { Title = "Creating Leave" });
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
                    model.AppUserId = employee.Id;
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
    }
}
