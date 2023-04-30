using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Domain.Entites;

namespace IKApplication.Application.AbstractServices
{
    public interface ICompanyService
    {
        //Company Create
        Task Create(Company model);

        //Company Update
        Task Update(Company model);

        //Get All Companies
        Task<List<Company>> GetAllCompanies();

        //Get Company By Id
        Task<Company> GetById(Guid id);

    }
}
