using IKApplication.Application.dtos.TitleDTOs;
using IKApplication.Application.DTOs.TitleDTOs;
using IKApplication.Application.VMs.TitleVMs;

namespace IKApplication.Application.AbstractServices
{
    public interface ITitleService
    {
        Task Create(TitleCreateDTO createTitleDTO);
        Task Update(TitleUpdateDTO titleUpdateDTO);
        Task Delete(Guid titleId);
        Task Recover(Guid titleId);
        Task<List<TitleVM>> GetAllTitles();
        Task<List<TitleVM>> GetCompanyTitlesWithDeleted(Guid companyId);
        Task<List<TitleVM>> GetCompanyTitles(Guid companyId);
        Task<TitleVM> GetVMById(Guid id);
        Task<TitleUpdateDTO> GetById(Guid id);
    }
}
