using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.SiteManagerDTO;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.Controllers
{
    public class SiteManagerController : Controller
    {
        private readonly ISiteManagerServices _managerServices;
        private readonly IMapper _mapper;

        public SiteManagerController(IMapper mapper, ISiteManagerServices managerSerives = null)
        {
            _mapper = mapper;
            _managerServices = managerSerives;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Update (int id)
        {
           

        }
    }
}
