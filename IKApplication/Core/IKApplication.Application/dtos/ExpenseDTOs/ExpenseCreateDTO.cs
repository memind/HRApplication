using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace IKApplication.Application.dtos.ExpenseDTOs
{
    public class ExpenseCreateDTO
    {
        public Guid Id { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Passive;
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int Amount { get; set; }
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public int? Penny { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid ExpenseById { get; set; }
        public Guid CompanyId { get; set; }
        public ExpenseType Type { get; set; }
        public Currency Currency { get; set; }
    }
}