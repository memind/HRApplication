using IKApplication.Domain.Enums;

namespace IKApplication.Application.DTOs.LeaveDTOs
{
    public class LeaveDTO
    {
        public Guid AppUserId { get; set; }
        public Guid Id { get; set; }
        public string Explanation { get; set; }
        public LeaveType LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
    }
}
