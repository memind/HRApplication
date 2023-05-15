using IKApplication.Application.DTOs.TitleDTOs;
using IKApplication.Application.VMs.TitleVMs;

namespace IKApplication.Application.AbstractServices
{
    public interface ITitleService
    {
        Task Create(TitleCreateDTO createTitleDTO);
        Task<List<TitleVM>> GetAllTitles();
    }
}
