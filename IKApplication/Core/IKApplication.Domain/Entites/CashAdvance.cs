using IKApplication.Domain.Enums;

namespace IKApplication.Domain.Entites
{
    public class CashAdvance : IBaseEntity
    {
        // Entity Properties
        public Guid Id { get; set; }
        public string Description { get; set; } //Advance Description
        public decimal RequestedAmount { get; set; } //How much the personel asked for
        //public AppUser? Director { get; set; } //Who will approve the advance
        public Guid? DirectorId { get; set; } //Who will approve the advance
        public PaymentStatus IsPaymentProcessed { get; set; } //İs the advance paid
        public DateTime? FinalDateRequest { get; set; } //The last date to request cashadvance
        public Guid CompanyId { get; set; }


        // Implement IBaseEntity
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // Navigation Properties
        public AppUser? AdvanceTo { get; set; }
        public Guid AdvanceToId { get; set; }
    }
}
