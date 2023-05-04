using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Domain.Entites;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task Create(CompanyDTO createCompanyDTO)
        {
            var map = _mapper.Map<Company>(createCompanyDTO);
            await _companyRepository.Create(map);

        }

        public async Task<List<Company>> GetAllCompanies()
        {
            //var companies = await _companyRepository.GetDefaults(x => x.Status == Domain.Enums.Status.Active || x.Status == Domain.Enums.Status.Modified);
            var companies = await _companyRepository.GetDefaults(x => x.Id != null);
            return companies;
        }

        public async Task Update(CompanyDTO updateCompanyDTO)
        {
            var map = _mapper.Map<Company>(updateCompanyDTO);
            await _companyRepository.Update(map);
        }

        public async Task<CompanyDTO> GetById(Guid id)
        {
            
            Company company = await _companyRepository.GetDefault(x => x.Id == id);
            if (company != null)
            {
                var model = _mapper.Map<CompanyDTO>(company);
                return model;
            }
            return null;
        }
    }
}
