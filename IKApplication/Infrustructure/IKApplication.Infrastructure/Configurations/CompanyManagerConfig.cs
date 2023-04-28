using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Infrastructure.Configurations
{
    public class CompanyManagerConfig : BaseEntityConfig<CompanyManager>
    {
        public override void Configure(EntityTypeBuilder<CompanyManager> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.SecondName).IsRequired(true);
            builder.Property(x => x.Surname).IsRequired(true);
            builder.Property(x => x.EMail).IsRequired(true);
            builder.Property(x => x.Title).IsRequired(true);
            builder.Property(x => x.PhoneNumber).IsRequired(true);
            builder.Property(x => x.BloodGroup).IsRequired(true);
            builder.Property(x => x.Profession).IsRequired(true);
            builder.Property(x => x.BirthDate).IsRequired(true);
            builder.Property(x => x.IdentityId).IsRequired(true);
            builder.Property(x => x.ImagePath).IsRequired(false);



            base.Configure(builder);
        }
    }
}
