using System.ComponentModel.DataAnnotations;

namespace IKApplication.Domain.Enums
{
    public enum LeaveType
    {
        [Display(Name = "Annual Leave")]
        AnnualLeave = 1,
        [Display(Name = "Sick Leave")]
        SickLeave = 2,
        [Display(Name = " Excused Leave")]
        ExcusedLeave = 3,
        [Display(Name = "Maternity/Paternity Leave")]
        MaternityLeave = 4,
        [Display(Name = "Unpaid Leave")]
        UnpaidLeave = 5,
        [Display(Name = "Marriage Leave")]
        MarriageLeave = 6,
        [Display(Name = "Bereavement Leave")]
        BereavementLeave = 7,
    }
}
