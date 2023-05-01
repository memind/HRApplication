using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Domain.Entites;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            var companies = _companyService.GetAllCompanies();
            return View(companies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyDTO createCompanyDTO)
        {
            if (ModelState.IsValid)
            {
                await _companyService.Create(createCompanyDTO);
                return RedirectToAction("Index");
            }
            return View(createCompanyDTO);
        }

        public async Task<IActionResult> Update(Guid id) 
        {
            return View(await _companyService.GetDtoById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CompanyDTO updateCompanyDTO)
        {
            if (ModelState.IsValid)
            {
                await _companyService.Update(updateCompanyDTO);
                return RedirectToAction("Index");
            }

            return View(updateCompanyDTO);
        }
    }
}
