using IKApplication.Application.DTOs.ProfessionDTOs;
using IKApplication.Application.VMs.ProfessionVMs;

namespace IKApplication.Application.AbstractServices
{
    public interface IProfessionService
    {
        Task Create(ProfessionCreateDTO createTitleDTO);
        Task Update(ProfessionUpdateDTO titleUpdateDTO);
        Task Delete(Guid titleId);
        Task Recover(Guid titleId);
        Task<List<ProfessionVM>> GetAllProfessions();
        Task<List<ProfessionVM>> GetCompanyProfessions(Guid companyId);
        Task<List<ProfessionVM>> GetCompanyProfessionsWithDeleted(Guid companyId);
        Task<ProfessionVM> GetVMById(Guid id);
        Task<ProfessionUpdateDTO> GetById(Guid id);
    }
}
