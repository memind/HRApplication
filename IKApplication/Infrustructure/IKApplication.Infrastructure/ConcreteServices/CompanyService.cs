using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Domain.Entites;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task Create(CompanyDTO createCompanyDTO)
        {
            var map = _mapper.Map<Company>(createCompanyDTO);
            await _companyRepository.Create(map);

        }

        public async Task<List<Company>> GetAllCompanies()
        {
            List<Company> companies = await _companyRepository.GetAll();
            return companies;
        }

        public async Task Update(CompanyDTO updateCompanyDTO)
        {
            var map = _mapper.Map<Company>(updateCompanyDTO);
            await _companyRepository.Update(map);
        }

        public async Task<Company> GetById(Guid id)
        {
           var company = await _companyRepository.GetDefault(x=> x.Id == id);
            return company;
        }

        public async Task<CompanyDTO> GetDtoById(Guid id)
        {
            var companyDto = _mapper.Map<CompanyDTO>(await _companyRepository.GetDefault(x => x.Id == id));
            return companyDto;
        }
    }
}
