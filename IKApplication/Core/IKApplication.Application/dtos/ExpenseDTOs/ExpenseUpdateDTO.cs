using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
namespace IKApplication.Application.dtos.ExpenseDTOs
{
    public class ExpenseUpdateDTO
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; } = DateTime.Now;
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; } = Status.Modified;
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid ExpenseById { get; set; }
        public ExpenseType Type { get; set; }
        public AppUser ApprovedBy { get; set; }
        public AppUser ExpenseBy { get; set; }
    }
}