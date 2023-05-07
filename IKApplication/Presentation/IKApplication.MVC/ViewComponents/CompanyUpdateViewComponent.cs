using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.ViewComponents
{
    public class CompanyUpdateViewComponent : ViewComponent
    {

        private readonly ICompanyService _companyService;

        public CompanyUpdateViewComponent(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<IViewComponentResult> InvokeAsync(CompanyUpdateDTO company)
        {
            return View(company);
        }
    }
}
