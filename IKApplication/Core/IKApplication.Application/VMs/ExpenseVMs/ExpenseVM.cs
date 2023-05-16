using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;

namespace IKApplication.Application.VMs.ExpenseVMs
{
    public class ExpenseVM
    {
        public Guid Id { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid ExpenseById { get; set; }
        public ExpenseType Type { get; set; }

    }
}