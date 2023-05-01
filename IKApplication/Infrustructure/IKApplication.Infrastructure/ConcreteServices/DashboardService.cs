using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.DashBoardDTOs;
using IKApplication.Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class DashboardService : IDashboardService
    {
        private readonly ICompanyService _companyService;
        private readonly UserManager<AppUser> _userManager;

        public DashboardService(ICompanyService companyService, UserManager<AppUser> userManager)
        {
            _companyService = companyService;
            _userManager = userManager;
        }

        public async Task<int> GetCompaniesCount()
        {
            var companies = await _companyService.GetAllCompanies();
            return companies.Count();
        }

        public async Task<int> GetCompanyAdminsCount()
        {
            var companyAdmins = await _userManager.GetUsersInRoleAsync("CompanyAdmin");
            return companyAdmins.Count();
        }

        public async Task<List<DashboardCompaniesCountBySectorDTO>> GetCompanyBySector()
        {
            // 1) Get all companies and create company list
            var companies = await _companyService.GetAllCompanies();
            List<DashboardCompaniesCountBySectorDTO> categorizedCompaniesList = new List<DashboardCompaniesCountBySectorDTO>();

            // 2) Group them by sector
            var sectors = companies.OrderBy(company => company.Sector)
                .GroupBy(company => company.Sector)
                .Select(x => new { Sector = x.Key, CompanyCount = x.Count() });

            // 3) Convert them into DashboardCompaniesCountBySectorDto and add to list
            foreach (var sectorInfo in sectors)
            {
                DashboardCompaniesCountBySectorDTO sectorInfoDto = new DashboardCompaniesCountBySectorDTO 
                { CompanySectorName = sectorInfo.Sector, CompaniesCount = sectorInfo.CompanyCount };

                categorizedCompaniesList.Add(sectorInfoDto);
            }

            // 4) Return that list
            return categorizedCompaniesList;
        }

        public async Task<DashboardCountInfosDTO> GetCountInfos()
        {
            DashboardCountInfosDTO infos = new();

            infos.CompaniesCount = await GetCompaniesCount();
            infos.CompanyAdminsCount = await GetCompanyAdminsCount();

            return infos;
        }
    }
}
