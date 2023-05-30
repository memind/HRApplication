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
        private readonly IExpenseService _expenseService;
        private readonly ICashAdvanceService _cashAdvanceService;
        private readonly ILeaveService _leaveService;

        public DashboardService(ICompanyService companyService, IAppUserService appUserService, ISectorService sectorService, IExpenseService expenseService, ICashAdvanceService cashAdvanceService, ILeaveService leaveService)
        {
            _companyService = companyService;
            _appUserService = appUserService;
            _sectorService = sectorService;
            _expenseService = expenseService;
            _cashAdvanceService = cashAdvanceService;
            _leaveService = leaveService;
        }

        public async Task<DashboardVM> GetDashboardInfos(Guid companyId)
        {
            DashboardVM dashboardVM = new();

            dashboardVM.TotalCompanyCount = (await _companyService.GetAllCompanies()).Count();
            dashboardVM.TotalCompanyManagerCount = (await _appUserService.GetUsersByRole("Company Administrator")).Count();
            dashboardVM.Sectors = await _sectorService.GetAllSectors();

            dashboardVM.CompanyLeavesCount = (await _leaveService.GetAllLeaves(companyId)).Where(x => x.Status == Status.Active || x.Status == Status.Modified).Count();
            dashboardVM.CompanyExpensesCount = (await _expenseService.GetAllExpenses(companyId)).Where(x => x.Status == Status.Active || x.Status == Status.Modified).Count();
            dashboardVM.CompanyCashAdvancesCount = (await _cashAdvanceService.GetAllAdvances(companyId)).Where(x => x.Status == Status.Active || x.Status == Status.Modified).Count();

            dashboardVM.CompanyLeaveRequestsCount = (await _leaveService.GetLeaveRequests(companyId)).Count();
            dashboardVM.CompanyExpenseRequestsCount = (await _expenseService.GetExpenseRequests(companyId)).Count();
            dashboardVM.CompanyCashAdvanceRequestsCount = (await _cashAdvanceService.GetAdvanceRequests(companyId)).Count();

            dashboardVM.ActiveCompanies = (await _companyService.GetAllCompanies()).Count();
            dashboardVM.PassiveCompanies = (await _companyService.GetAllPassiveCompanies()).Count();
            dashboardVM.ActiveUsers = (await _appUserService.GetAllUsers()).Count();
            dashboardVM.ActiveUsers = (await _appUserService.GetAllPassiveUsers()).Count();

            return dashboardVM;
        }
    }
}
