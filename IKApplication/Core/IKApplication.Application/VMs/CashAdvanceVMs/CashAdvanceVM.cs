using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;

namespace IKApplication.Application.VMs.CashAdvanceVMs
{
    public class CashAdvanceVM
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal RequestedAmount { get; set; }
        public AppUser Director { get; set; }
        public PaymentStatus IsPaymentProcessed { get; set; }
        public DateTime FinalDateRequest { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
    }
}
