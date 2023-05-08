﻿using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKApplication.Persistance.Configurations
{
    public class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.Email).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.SecondName).IsRequired(false);
            builder.Property(x => x.Surname).IsRequired(true);
            builder.Property(x => x.BloodGroup).IsRequired(false);
            builder.Property(x => x.Profession).IsRequired(false);
            builder.Property(x => x.BirthDate).IsRequired(true);
            builder.Property(x => x.IdentityNumber).IsRequired(true);
            builder.Property(x => x.ImagePath).IsRequired(true);
            builder.Property(x => x.CompanyId).IsRequired(true);
            builder.Property(x => x.TitleId).IsRequired(true);
        }
    }
}
