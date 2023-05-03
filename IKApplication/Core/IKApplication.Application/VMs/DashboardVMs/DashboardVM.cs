namespace IKApplication.Application.VMs.DashboardVMs
{
    public class DashboardVM
    {
        public DashboardVM()
        {
            CompanyListBySector = new List<DashboardCompaniesCountBySectorVM>();
        }
        public int TotalCompaniesCount { get; set; }
        public int TotalCompanyManagersCount { get; set; }
        public List<DashboardCompaniesCountBySectorVM> CompanyListBySector { get; set; }
    }
}
