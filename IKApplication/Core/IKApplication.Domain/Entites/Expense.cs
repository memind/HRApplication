using IKApplication.Domain.Enums;
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
        public DateTime ExpenseDate { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid ExpenseById { get; set; }
        public ExpenseType Type { get; set; }
        // Navigation Properties
        [NotMapped]
        public AppUser ApprovedBy { get; set; }
        public AppUser ExpenseBy { get; set; }
    }
}