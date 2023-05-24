using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKApplication.Persistance.Configurations
{
    public class TitleConfig : BaseEntityConfig<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.CompanyId).IsRequired(true);

            builder.HasMany(x => x.AppUsers).WithOne(x => x.Title).HasForeignKey(x => x.TitleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
