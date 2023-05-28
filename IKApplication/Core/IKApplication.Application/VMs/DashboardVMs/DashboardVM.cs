using IKApplication.Application.VMs.SectorVMs;

namespace IKApplication.Application.VMs.DashboardVMs
{
    public class DashboardVM
    {
        public int TotalCompanyCount { get; set; }
        public int TotalCompanyManagerCount { get; set; }
        public List<SectorVM> Sectors { get; set; }
        public int CompanyExpensesCount { get; set; }
        public int CompanyCashAdvancesCount { get; set; }
        public int CompanyLeavesCount { get; set; }
        public int CompanyExpenseRequestsCount { get; set; }
        public int CompanyCashAdvanceRequestsCount { get; set; }
        public int CompanyLeaveRequestsCount { get; set; }
        public int ActiveCompanies { get; set; }
        public int PassiveCompanies { get; set; }
        public int ActiveUsers{ get; set; }
        public int PassiveUsers { get; set; }
        public Guid CompanyId { get; set; }
    }
}
