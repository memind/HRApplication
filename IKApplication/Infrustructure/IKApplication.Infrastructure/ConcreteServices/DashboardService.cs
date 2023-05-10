using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.VMs.DashboardVMs;
using IKApplication.Application.VMs.SectorVMs;
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

        public async Task<List<SectorVM>> GetSectorList()
        {
            // 1) Get all companies and create company list
            var companies = await _companyService.GetAllCompanies();
            List<SectorVM> Sectors = new List<SectorVM>();

            // 2) Group them by sector
            var result = companies.OrderBy(company => company.SectorName)
                .GroupBy(company => company.SectorName)
                .Select(x => new { Name = x.Key, CompanyCount = x.Count() });

            // 3) Convert them into DashboardCompaniesCountBySectorDto and add to list
            foreach (var item in result)
            {
                SectorVM sector = new SectorVM
                {
                    Name = item.Name,
                    CompanyCount = item.CompanyCount
                };

                Sectors.Add(sector);
            }

            // 4) Return that list
            return Sectors;
        }

        public async Task<DashboardVM> GetDashboardInfos()
        {
            DashboardVM dashboardVM = new();

            dashboardVM.TotalCompanyCount = await this.GetCompaniesCount();
            dashboardVM.TotalCompanyManagerCount = await this.GetCompanyManagersCount();
            dashboardVM.Sectors = await this.GetSectorList();

            return dashboardVM;
        }
    }
}
