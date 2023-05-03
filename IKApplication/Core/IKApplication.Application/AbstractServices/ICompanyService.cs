using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Domain.Entites;

namespace IKApplication.Application.AbstractServices
{
    public interface ICompanyService
    {
        //Company Create
        Task Create(CompanyDTO createCompanyDTO);

        //Company Update
        Task Update(CompanyDTO updateCompanyDTO);

        //Get All Companies
        Task<List<Company>> GetAllCompanies();

        //Get Company By Id
        Task<CompanyDTO> GetById(Guid id);

    }
}
