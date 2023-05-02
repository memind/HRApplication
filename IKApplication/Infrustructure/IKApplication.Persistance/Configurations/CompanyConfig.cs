using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Persistance.Configurations
{
    public class CompanyConfig : BaseEntityConfig<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.Email).IsRequired(true);
            builder.Property(x => x.PhoneNumber).IsRequired(true);
            builder.Property(x => x.Sector).IsRequired(true);
            builder.Property(x => x.NumberOfEmployees).IsRequired(true);
        }
    }
}
