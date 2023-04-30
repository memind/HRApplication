using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Domain.Entites;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.Controllers
{
    public class SiteManagerController : Controller
    {
        private readonly IAppUserService _managerServices;
        private readonly IMapper _mapper;

        public SiteManagerController(IMapper mapper, IAppUserService managerSerives)
        {
            _mapper = mapper;
            _managerServices = managerSerives;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var user = await _managerServices.GetById(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateDTO user)
        {
            var map = _mapper.Map<AppUser>(user);  
          if(ModelState.IsValid) 
          {
                _managerServices.UpdateUser(map);
                return RedirectToAction("Index");
          }
          return View(map);
        }



    }
}
