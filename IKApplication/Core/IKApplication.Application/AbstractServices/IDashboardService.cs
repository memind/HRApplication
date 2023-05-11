using IKApplication.Application.VMs.DashboardVMs;
using IKApplication.Application.VMs.SectorVMs;

namespace IKApplication.Application.AbstractServices
{
    public interface IDashboardService
    {
        Task<DashboardVM> GetDashboardInfos();
    }
}
