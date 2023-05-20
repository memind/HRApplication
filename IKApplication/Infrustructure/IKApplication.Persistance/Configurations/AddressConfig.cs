using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKApplication.Persistance.Configurations
{
    public class AddressConfig : BaseEntityConfig<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.OpenAddress).IsRequired(true).HasMaxLength(160);
            builder.Property(x => x.PostCode).IsRequired(false).HasMaxLength(5);
            builder.Property(x => x.AppUserId).IsRequired(true);

            builder.HasOne(x => x.AppUser).WithOne(x => x.Address).HasForeignKey<AppUser>(x => x.AddressId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
