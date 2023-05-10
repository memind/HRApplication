using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.SectorVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ISectorRepository _sectorRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, ISectorRepository sectorRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _sectorRepository = sectorRepository;
            _mapper = mapper;
        }

        public async Task<CompanyUpdateDTO> GetById(Guid id)
        {
            Company company = await _companyRepository.GetDefault(x => x.Id == id);

            if (company != null)
            {
                var model = _mapper.Map<CompanyUpdateDTO>(company);

                model.Sectors = await _sectorRepository.GetFilteredList(
                        select: x => new SectorVM
                        {
                            Id = x.Id,
                            Name = x.Name,
                            CompanyCount = x.Companies.Count,
                        },
                        where: x => (x.Status == Status.Active || x.Status == Status.Modified),
                        orderBy: x => x.OrderBy(x => x.CreateDate),
                        include: x => x.Include(x => x.Companies));

                return model;
            }

            return null;
        }

        public async Task<List<CompanyVM>> GetAllCompanies()
        {
            var companies = await _companyRepository.GetFilteredList(
                select: x => new CompanyVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    SectorName = x.Sector.Name,
                    NumberOfEmployees = x.NumberOfEmployees
                },
                where: x => (x.Status == Status.Active || x.Status == Status.Modified),
                orderBy: x => x.OrderBy(x => x.CreateDate),
                include: x => x.Include(x => x.Sector));

            return companies;
        }

        public async Task Create(CompanyCreateDTO createCompanyDTO)
        {
            var model = _mapper.Map<Company>(createCompanyDTO);
            await _companyRepository.Create(model);
        }

        public async Task Update(CompanyUpdateDTO updateCompanyDTO)
        {
            var model = _mapper.Map<Company>(updateCompanyDTO);
            await _companyRepository.Update(model);
        }

        public async Task Delete(Guid id)
        {
            Company company = await _companyRepository.GetDefault(x => x.Id == id);

            if (company != null)
            {
                company.DeleteDate = DateTime.Now;
                company.Status = Status.Deleted;

                await _companyRepository.Delete(company);
            }
        }
    }
}
