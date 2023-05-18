using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
namespace IKApplication.Application.dtos.ExpenseDTOs
{
    public class ExpenseUpdateDTO
    {
        public Guid Id { get; set; }
        public DateTime? UpdateDate => DateTime.Now;
        public DateTime? DeleteDate { get; set; }
        public Status Status => Status.Modified;
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public ExpenseType Type { get; set; }
        public string? FullName { get; set; }
    }
}