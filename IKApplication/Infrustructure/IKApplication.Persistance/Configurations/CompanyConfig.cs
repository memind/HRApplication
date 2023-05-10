using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKApplication.Persistance.Configurations
{
    public class CompanyConfig : BaseEntityConfig<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.Email).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.PhoneNumber).IsRequired(true);
            builder.Property(x => x.NumberOfEmployees).IsRequired(true);
            builder.Property(x => x.SectorId).IsRequired(true);
        }
    }
}
