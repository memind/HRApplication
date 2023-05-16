using IKApplication.Application.DTOs.PersonalDTO;
using Microsoft.AspNetCore.Mvc;

namespace IKApplication.MVC.ViewComponents
{
    public class PersonalUpdateViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PersonalUpdateDTO personal)
        {
            return View(personal);
        }
    }
}
