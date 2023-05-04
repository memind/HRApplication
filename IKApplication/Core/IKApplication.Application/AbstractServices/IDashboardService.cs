using IKApplication.Application.VMs.DashboardVMs;
using IKApplication.Application.VMs.SectorVMs;

namespace IKApplication.Application.AbstractServices
{
    public interface IDashboardService
    {
        Task<int> GetCompaniesCount();
        Task<int> GetCompanyManagersCount();
        Task<List<SectorVM>> GetSectorList();
        Task<DashboardVM> GetDashboardInfos();
    }
}
