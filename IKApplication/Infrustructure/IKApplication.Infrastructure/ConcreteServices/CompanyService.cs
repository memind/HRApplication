using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using Microsoft.EntityFrameworkCore;

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

        public async Task Create(CompanyCreateDTO createCompanyDTO)
        {
            var model = _mapper.Map<Company>(createCompanyDTO);
            await _companyRepository.Create(model);

        }

        public async Task<List<CompanyVM>> GetAllCompanies()
        {
            var companies = await _companyRepository.GetFilteredList(
                select: x => new CompanyVM
                {
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    SectorName = x.Sector.Name,
                    NumberOfEmployees = x.NumberOfEmployees
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name),
                include: x => x.Include(x => x.Sector));
            return companies;
        }

        public async Task Update(CompanyUpdateDTO updateCompanyDTO)
        {
            var model = _mapper.Map<Company>(updateCompanyDTO);
            await _companyRepository.Update(model);
        }

        public async Task<CompanyUpdateDTO> GetById(Guid id)
        {
            Company company = await _companyRepository.GetDefault(x => x.Id == id);
            if (company != null)
            {
                var model = _mapper.Map<CompanyUpdateDTO>(company);
                return model;
            }

            return null;
        }
    }
}
