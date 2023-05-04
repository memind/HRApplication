using IKApplication.Application.AbstractServices;
using IKApplication.Application.VMs.DashboardVMs;
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

        public async Task<int> GetCompanyManagersCount()
        {
            var companyManagers = await _userManager.GetUsersInRoleAsync("Company Administrator");
            return companyManagers.Count();
        }

        public async Task<List<DashboardCompaniesCountBySectorVM>> GetCompaniesBySector()
        {
            // 1) Get all companies and create company list
            var companies = await _companyService.GetAllCompanies();
            List<DashboardCompaniesCountBySectorVM> categorizedCompaniesList = new List<DashboardCompaniesCountBySectorVM>();

            // 2) Group them by sector
            var sectors = companies.OrderBy(company => company.Sector)
                .GroupBy(company => company.Sector)
                .Select(x => new { Sector = x.Key, CompanyCount = x.Count() });

            // 3) Convert them into DashboardCompaniesCountBySectorDto and add to list
            foreach (var sectorInfo in sectors)
            {
                DashboardCompaniesCountBySectorVM sectorInfoDto = new DashboardCompaniesCountBySectorVM
                { SectorName = sectorInfo.Sector, CompanyCountInSector = sectorInfo.CompanyCount };

                categorizedCompaniesList.Add(sectorInfoDto);
            }

            // 4) Return that list
            return categorizedCompaniesList;
        }

        public async Task<DashboardVM> GetDashboardInfos()
        {
            DashboardVM infos = new();

            infos.TotalCompaniesCount = await this.GetCompaniesCount();
            infos.TotalCompanyManagersCount = await this.GetCompanyManagersCount();
            infos.CompanyListBySector = await this.GetCompaniesBySector();

            return infos;
        }
    }
}
