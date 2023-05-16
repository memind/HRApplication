using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKApplication.Persistance.Configurations
{
    public class ExpenseConfig : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.Property(x => x.ShortDescription).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.LongDescription).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Amount).IsRequired(true);
            builder.Property(x => x.ExpenseDate).IsRequired(true);
            builder.Property(x => x.ExpenseById).IsRequired(true);
            builder.Property(x => x.ApprovedById).IsRequired(true);
            builder.Property(x => x.Type).IsRequired(true);
        }
    }
}