using IKApplication.Domain.Enums;

namespace IKApplication.Domain.Entites
{
    public class Leave : IBaseEntity
    {
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        public Guid AppUserId { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Explanation { get; set; }
        public LeaveType LeaveType { get; set; }
        public LeaveStatus LeaveStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public AppUser AppUser { get; set; }
        public AppUser ApprovedBy { get; set; }
        public int TotalLeaveDays { get; set; }
        public int RemainingLeaveDays { get; set; }
    }
}
