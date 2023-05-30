using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IKApplication.Persistance.Configurations
{
    public class ProfessionConfig : BaseEntityConfig<Profession>
    {
        public void Configure(EntityTypeBuilder<Profession> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.CompanyId).IsRequired(true);

            builder.HasMany(x => x.AppUsers).WithOne(x => x.Profession).HasForeignKey(x => x.ProfessionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
