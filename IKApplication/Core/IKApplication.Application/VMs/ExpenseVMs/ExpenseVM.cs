using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;

namespace IKApplication.Application.VMs.ExpenseVMs
{
    public class ExpenseVM
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ApprovedById { get; set; }
        public AppUser? ApprovedBy { get; set; }
        public Guid ExpenseById { get; set; }
        public Guid? CurrentUserId { get; set; }
        public Guid CompanyId { get; set; }
        public AppUser ExpenseBy { get; set; }
        public ExpenseType Type { get; set; }
        public string? FullName { get; set; }
    }
}