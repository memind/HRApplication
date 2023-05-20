using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using System.ComponentModel;

namespace IKApplication.Application.VMs.LeaveVMs
{
    public class LeaveVM
    {
        public Guid Id { get; set; }
        public Guid? CurrentUserId { get; set; }
        public Guid CompanyId { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        public string? Explanation { get; set; }

        [DisplayName("Leave Status")]
        public LeaveStatus? LeaveStatus { get; set; }

        [DisplayName("Create Date")]
        public DateTime CreateDate { get; set; }

        [DisplayName("Update Date")]
        public DateTime? UpdateDate { get; set; }
        public Status? Status { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid ApprovedById { get; set; }
        public AppUser ApprovedBy { get; set; }
        public LeaveType LeaveType { get; set; }
        public string PersonalFullName { get; set; }
    }
}
