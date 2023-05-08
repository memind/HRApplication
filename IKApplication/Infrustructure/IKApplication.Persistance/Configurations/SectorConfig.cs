using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKApplication.Persistance.Configurations
{
    public class SectorConfig : BaseEntityConfig<Sector>
    {
        public override void Configure(EntityTypeBuilder<Sector> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);
        }
    }
}
