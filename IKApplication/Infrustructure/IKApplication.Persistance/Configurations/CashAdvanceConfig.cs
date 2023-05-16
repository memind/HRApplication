using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKApplication.Persistance.Configurations
{
    public class CashAdvanceConfig : IEntityTypeConfiguration<CashAdvance>
    {
        public void Configure(EntityTypeBuilder<CashAdvance> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Description).HasMaxLength(255);
            builder.Property(c => c.RequestedAmount).HasColumnType("decimal(18, 2)");
            builder.Property(c => c.FinalDateRequest).HasColumnType("date");

            // Configure relationships
            builder.HasOne(c => c.Director)
                .WithMany(d => d.CashAdvances).HasForeignKey(c => c.DirectorId) //Todo: Director ile olan ilişkiyi düzelt
                .OnDelete(DeleteBehavior.Restrict);

            // Configure table name
            builder.ToTable("CashAdvances");
        }
    }
}
