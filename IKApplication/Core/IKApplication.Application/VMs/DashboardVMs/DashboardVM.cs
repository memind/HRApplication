using IKApplication.Application.VMs.SectorVMs;

namespace IKApplication.Application.VMs.DashboardVMs
{
    public class DashboardVM
    {
        public int TotalCompanyCount { get; set; }
        public int TotalCompanyManagerCount { get; set; }
        public List<SectorVM> Sectors { get; set; }
    }
}
