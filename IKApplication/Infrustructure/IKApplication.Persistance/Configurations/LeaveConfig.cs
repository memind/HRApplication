using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKApplication.Persistance.Configurations
{
    public class LeaveConfig : BaseEntityConfig<Leave>
    {
        public override void Configure(EntityTypeBuilder<Leave> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.LeaveType).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.Explanation).IsRequired();
            builder.Property(x => x.LeaveStatus).IsRequired();
            builder.Property(x => x.CompanyId).IsRequired();

            builder.HasOne(x => x.AppUser).WithMany(x => x.Leaves).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ApprovedBy).WithMany(x => x.ApproveLeaves).HasForeignKey(x => x.ApprovedById).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
