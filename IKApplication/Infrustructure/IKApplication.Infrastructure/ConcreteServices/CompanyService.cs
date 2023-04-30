using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Domain.Entites;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task Create(Company model)
        {
          await _companyRepository.Create(model);

        }

        public async Task<List<Company>> GetAllCompanies()
        {
            List<Company> companies = await _companyRepository.GetAll();
            return companies;
        }

        public async Task Update(Company model)
        {
            await _companyRepository.Update(model);
        }

        public async Task<Company> GetById(Guid id)
        {
           var company = await _companyRepository.GetDefault(x=> x.Id == id);
            return company;
        }
    }
}
