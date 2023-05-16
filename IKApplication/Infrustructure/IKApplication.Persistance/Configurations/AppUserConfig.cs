using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKApplication.Persistance.Configurations
{
    public class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.SecondName).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.Surname).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.BloodGroup).IsRequired(false);
            builder.Property(x => x.Profession).IsRequired(false).HasMaxLength(30);
            builder.Property(x => x.BirthDate).IsRequired(true);
            builder.Property(x => x.IdentityNumber).IsRequired(true).HasMaxLength(11).IsFixedLength(true);
            builder.Property(x => x.ImagePath).IsRequired(true);
            builder.Property(x => x.CompanyId).IsRequired(true);
            builder.Property(x => x.TitleId).IsRequired(true);
            //builder.Property(x => x.ExpenseId).IsRequired(false);

            //builder.HasMany(x => x.Expenses).WithOne(x => x.ExpenseBy).HasForeignKey(x => x.ExpenseById);
        }
    }
}
