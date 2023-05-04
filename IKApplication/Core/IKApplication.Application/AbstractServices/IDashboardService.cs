
using IKApplication.Application.VMs.DashboardVMs;

namespace IKApplication.Application.AbstractServices
{
    public interface IDashboardService
    {
        Task<int> GetCompaniesCount();
        Task<int> GetCompanyManagersCount();
        Task<List<DashboardCompaniesCountBySectorVM>> GetCompaniesBySector();
        Task<DashboardVM> GetDashboardInfos();
    }
}
