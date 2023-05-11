using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.VMs.DashboardVMs;
using IKApplication.Application.VMs.SectorVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class DashboardService : IDashboardService
    {
        private readonly ICompanyService _companyService;
        private readonly IAppUserService _appUserService;
        private readonly ISectorService _sectorService;

        public DashboardService(ICompanyService companyService, IAppUserService appUserService, ISectorService sectorService)
        {
            _companyService = companyService;
            _appUserService = appUserService;
            _sectorService = sectorService;
        }

        public async Task<DashboardVM> GetDashboardInfos()
        {
            DashboardVM dashboardVM = new();

            dashboardVM.TotalCompanyCount = (await _companyService.GetAllCompanies()).Count();
            dashboardVM.TotalCompanyManagerCount = (await _appUserService.GetUsersByRole("Company Administrator")).Count();
            dashboardVM.Sectors = await _sectorService.GetAllSectors();

            return dashboardVM;
        }
    }
}
