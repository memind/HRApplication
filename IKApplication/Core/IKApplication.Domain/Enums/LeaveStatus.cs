using System.ComponentModel.DataAnnotations;

namespace IKApplication.Domain.Enums
{
    public enum LeaveStatus
    {
        [Display(Name = "Approved")]
        Approved = 1,
        [Display(Name = "Denied")]
        Denied = 2,
        [Display(Name = "In Approval")]
        InApproval = 3
    }
}
