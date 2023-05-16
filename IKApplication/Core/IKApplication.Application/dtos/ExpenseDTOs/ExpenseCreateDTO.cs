using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
namespace IKApplication.Application.dtos.ExpenseDTOs
{
    public class ExpenseCreateDTO
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public Status Status { get; set; } = Status.Passive;
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid? ApprovedById { get; set; }
        public Guid ExpenseById { get; set; }
        public ExpenseType Type { get; set; }
        public AppUser? ApprovedBy { get; set; }
        public AppUser ExpenseBy { get; set; }
    }
}