using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKApplication.Persistance.Configurations
{
    public class ReportConfig : BaseEntityConfig<Report>
    {
        public override void Configure(EntityTypeBuilder<Report> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.ReportPath).IsRequired(true);
            builder.Property(x => x.CreatorId).IsRequired(true);
            builder.Property(x => x.FileType).IsRequired(true);

            builder.HasOne(x => x.Creator).WithMany(x => x.Reports).HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
