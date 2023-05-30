using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.VMs.CompanyVMs;

namespace IKApplication.Application.AbstractServices
{
    public interface ICompanyService
    {
        Task Create(CompanyCreateDTO createCompanyDTO);
        Task Update(CompanyUpdateDTO updateCompanyDTO);
        Task Delete(Guid id);
        Task<List<CompanyVM>> GetAllCompanies();
        Task<List<CompanyVM>> GetAllPassiveCompanies();
        Task<CompanyUpdateDTO> GetById(Guid id);
    }
}
