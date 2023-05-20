using IKApplication.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace IKApplication.Application.DTOs.LeaveDTOs
{
    public class UpdateLeaveDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Start Date area cannot be empty!")]

        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date area cannot be empty!")]

        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Comments field cannot be empty!")]
        [MaxLength(50)]

        public string Explanation { get; set; }
        public LeaveStatus LeaveStatus { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public DateTime? DeleteDate { get; set; }

        public Status Status => Status.Modified;

        public Guid AppUserId { get; set; }

        public string? FullName { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}
