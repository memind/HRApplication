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

        public SiteManagerController(IAppUserService managerSerives)
        {
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
          if(ModelState.IsValid) 
          {
                _managerServices.UpdateUser(user);
                return RedirectToAction("Index");
          }
          return View(user);
        }



    }
}
