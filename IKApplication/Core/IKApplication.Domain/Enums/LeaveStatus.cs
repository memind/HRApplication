using System.ComponentModel.DataAnnotations;

namespace IKApplication.Domain.Enums
{
    public enum LeaveStatus
    {
        Approved = 1,
        Denied = 2,
        [Display(Name = "In Approval")]
        InApproval = 3
    }
}
