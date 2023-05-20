using IKApplication.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace IKApplication.Application.DTOs.LeaveDTOs
{
    public class CreateLeaveDTO
    {
        public Guid Id => Guid.NewGuid();
        [Required(ErrorMessage = "Start Date area cannot be empty!")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date area cannot be empty!")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Explanation cannot be empty!")]
        [MaxLength(50)]
        public string Explanation { get; set; }
        public LeaveStatus LeaveStatus => LeaveStatus.InApproval;
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Passive;
        public Guid? AppUserId { get; set; }
        public Guid? ApprovedById { get; set; }
        public Guid CompanyId { get; set; }
        public LeaveType? LeaveTypes { get; set; }
    }
}
