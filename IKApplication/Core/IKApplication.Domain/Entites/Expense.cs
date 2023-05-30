using IKApplication.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKApplication.Domain.Entites
{
    public class Expense : IBaseEntity
    {
        // Implement IBaseEntity
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        // Entity Properties
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int Amount { get; set; }
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public int? Penny { get; set; }

        [Range(typeof(DateTime), "2022-01-01", "2024-01-01", ErrorMessage = "You can't enter a date 1 year ago or later than today.")]
        public DateTime ExpenseDate { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid ExpenseById { get; set; }
        public Guid CompanyId { get; set; }
        public ExpenseType Type { get; set; }
        public Currency Currency { get; set; }
        // Navigation Properties
        public AppUser ApprovedBy { get; set; }
        public AppUser ExpenseBy { get; set; }
    }
}