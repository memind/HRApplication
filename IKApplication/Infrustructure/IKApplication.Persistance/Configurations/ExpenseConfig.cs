using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKApplication.Persistance.Configurations
{
    public class ExpenseConfig : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.Property(x => x.ShortDescription).IsRequired(true);
            builder.Property(x => x.LongDescription).IsRequired(true);
            builder.Property(x => x.Amount).IsRequired(true).HasColumnType("decimal(18, 2)");
            builder.Property(x => x.ExpenseDate).IsRequired(true);
            builder.Property(x => x.ExpenseById).IsRequired(true);
            builder.Property(x => x.ApprovedById).IsRequired(true);
            builder.Property(x => x.CompanyId).IsRequired(true);
            builder.Property(x => x.Type).IsRequired(true);
            builder.Property(x => x.Currency).IsRequired(true);

            builder.HasOne(x => x.ExpenseBy).WithMany(x => x.Expenses).HasForeignKey(x => x.ExpenseById).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ApprovedBy).WithMany(x => x.ApproveExpenses).HasForeignKey(x => x.ApprovedById).OnDelete(DeleteBehavior.Restrict);
        }
    }
}