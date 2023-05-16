using IKApplication.Domain.Enums;

namespace IKApplication.Application.VMs.LeaveVMs
{
    public class LeaveVM
    {
        public Guid AppUserId { get; set; }
        public Guid Id { get; set; }
        public string Explanation { get; set; }
        public LeaveType PermissionType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
    }
}
