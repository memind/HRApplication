using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.UserDTOs;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Domain.Entites;
using IKApplication.Infrastructure.ConcreteServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IKApplication.MVC.Controllers
{
    [Authorize(Roles = "Site Administrator")]
    public class SiteManagerController : Controller
    {
        private readonly IAppUserService _userService;

        public SiteManagerController(IAppUserService managerSerives)
        {
            _userService = managerSerives;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var user = await _userService.GetById(id);
            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppUserCreateDTO user)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateCompanyManagerAsync(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            return View(await _userService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUserUpdateDTO user)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
