using IKApplication.Application.VMs.TitleVMs;

namespace IKApplication.Application.AbstractServices
{
    public interface ITitleService
    {
        Task<List<TitleVM>> GetAllTitles();
    }
}
