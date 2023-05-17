using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
namespace IKApplication.Application.dtos.ExpenseDTOs
{
    public class ExpenseCreateDTO
    {
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Passive;
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid? ApprovedById { get; set; }
        public Guid ExpenseById { get; set; }
        public Guid CompanyId { get; set; }
        public ExpenseType Type { get; set; }
    }
}