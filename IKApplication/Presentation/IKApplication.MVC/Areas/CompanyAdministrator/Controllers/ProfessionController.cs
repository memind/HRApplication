using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.TitleDTOs;
using IKApplication.Application.DTOs.ProfessionDTOs;
using IKApplication.Application.DTOs.TitleDTOs;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace IKApplication.MVC.Areas.CompanyAdministrator.Controllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
    public class ProfessionController : Controller
    {
        private readonly IProfessionService _professionService;
        private readonly IAppUserService _appUserService;
        private readonly IToastNotification _toast;
        private readonly IMapper _mapper;
        public ProfessionController(IProfessionService professionService, IAppUserService appUserService, IToastNotification toast, IMapper mapper)
        {
            _professionService = professionService;
            _appUserService = appUserService;
            _toast = toast;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var professions = await _professionService.GetCompanyProfessions(user.CompanyId);

            return View(professions);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProfession()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            ProfessionCreateDTO model = new ProfessionCreateDTO()
            {
                Id = Guid.NewGuid(),
                CompanyId = user.CompanyId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfession(ProfessionCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _professionService.Create(model);
                _toast.AddSuccessToastMessage(Messages.Title.Create(), new ToastrOptions { Title = "Creating Title" });

                return RedirectToAction("Index", "Title");
            }
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Creating Title" });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfession(Guid id)
        {
            var profession = await _professionService.GetById(id);
            return View(profession);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfession(ProfessionUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _professionService.Update(model);
                _toast.AddSuccessToastMessage(Messages.Title.Update(), new ToastrOptions { Title = "Updating Title" });
                return RedirectToAction("Index", "Title");
            }
            _toast.AddErrorToastMessage(Messages.Errors.Error(), new ToastrOptions { Title = "Updating Title" });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTitle(Guid id)
        {
            await _professionService.Delete(id);
            _toast.AddSuccessToastMessage(Messages.Title.Delete(), new ToastrOptions { Title = "Deleting Title" });
            return RedirectToAction("Index");
        }
    }
}
