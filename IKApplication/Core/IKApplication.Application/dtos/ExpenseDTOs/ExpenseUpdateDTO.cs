using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using System.ComponentModel.DataAnnotations;

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
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public int? Penny { get; set; }
        [Range(typeof(DateTime), "2022-01-01", "2024-01-01", ErrorMessage = "You can't enter a date more than 1 year ago or later than today.")]
        public DateTime ExpenseDate { get; set; }
        public ExpenseType Type { get; set; }
        public Currency Currency { get; set; }
        public string? FullName { get; set; }
    }
}