using IKApplication.Application.VMs.PersonalVM;
using Microsoft.AspNetCore.Mvc;
using static IKApplication.MVC.ResultMessages.Messages;

namespace IKApplication.MVC.ViewComponents
{
    public class PersonalListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<PersonalVM> personals)
        {
            return View(personals);
        }
    }
}
