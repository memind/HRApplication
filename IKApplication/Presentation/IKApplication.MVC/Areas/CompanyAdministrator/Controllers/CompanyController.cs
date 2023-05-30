using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using static IKApplication.MVC.ResultMessages.Messages;
using System.Security.Policy;

namespace IKApplication.MVC.Areas.SiteAdministrator.Controllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IToastNotification _toast;
        public CompanyController(ICompanyService companyService, IToastNotification toast)
        {
            _companyService = companyService;
            _toast = toast;
        }
        public async Task<IActionResult> Update(Guid id)
        {
            return View(await _companyService.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(CompanyUpdateDTO updateCompanyDTO)
        {
            if (ModelState.IsValid)
            {
                await _companyService.Update(updateCompanyDTO);
                _toast.AddSuccessToastMessage(Messages.Company.Update(updateCompanyDTO.Name), new ToastrOptions { Title = "Updating Company" });
                return RedirectToAction("Update", "Company");
            }

            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Company" });
            return View(updateCompanyDTO);
        }
    }
}
