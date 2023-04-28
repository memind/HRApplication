using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Infrastructure.Configurations
{
    internal class SiteAdministratorConfig : BaseEntityConfig<SiteAdministrator>
    {
        public override void Configure(EntityTypeBuilder<SiteAdministrator> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.UserName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.ImagePath).IsRequired(false);

            base.Configure(builder);
        }
    }
}
