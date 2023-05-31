using IKApplication.Domain.Enums;

namespace IKApplication.Domain.Entites
{
    public class CashAdvance : IBaseEntity
    {
        // Entity Properties
        public Guid Id { get; set; }
        public string Description { get; set; } //Advance Description
        public decimal RequestedAmount { get; set; } //How much the personel asked for

        public PaymentStatus IsPaymentProcessed { get; set; } //İs the advance paid
        public int InstallmentCount { get; set; } // How many installments are requested for the cash advance

        private DateTime? _finalDateRequest;
        public DateTime? FinalDateRequest  //The last date to request cashadvance
        {
            get => _finalDateRequest;
            set
            {
                _finalDateRequest = value;

                if (_finalDateRequest.HasValue && _finalDateRequest.Value < DateTime.Now)
                {
                    // Son geçerlilik tarihine kadar onaylanmadıysa otomatik olarak silinmesi
                    Status = Status.Deleted;
                }
            }
        }

        public Guid CompanyId { get; set; }

        // Implement IBaseEntity
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }

        // Navigation Properties
        public AppUser AdvanceTo { get; set; }
        public Guid AdvanceToId { get; set; }
        public AppUser Director { get; set; }
        public Guid DirectorId { get; set; } //Who will approve the advance
    }
}
