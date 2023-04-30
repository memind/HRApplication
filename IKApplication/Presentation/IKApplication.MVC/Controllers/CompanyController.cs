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
        private readonly IMapper _mapper;
        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
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
        public async Task<IActionResult> Create(CreateCompanyDTO createCompanyDTO)
        {
            var map= _mapper.Map<Company>(createCompanyDTO);
            if (ModelState.IsValid)
            {
                await _companyService.Create(map);
                return RedirectToAction("Index");
            }
            return View(createCompanyDTO);
        }

        public async Task<IActionResult> Update(Guid id) 
        {
           var map = _mapper.Map<UpdateCompanyDTO>(await _companyService.GetById(id));
            return View(map);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCompanyDTO updateCompanyDTO)
        {
            var map = _mapper.Map<Company>(updateCompanyDTO);
            if (ModelState.IsValid)
            {
                await _companyService.Update(map);
                return RedirectToAction("Index");
            }

            return View(map);

        }



    }
}
