using System.ComponentModel.DataAnnotations;

namespace IKApplication.Domain.Enums
{
    public enum PaymentStatus
    {
        [Display(Name = "Payment Processed")]
        PaymentProcessed = 1,
        [Display(Name = "Payment Pending")]
        PaymentPending = 2
    }
}
