using IKApplication.Domain.Entites;
using IKApplication.Persistance.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IKApplication.Persistance
{
    public class IKAppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public IKAppDbContext(DbContextOptions<IKAppDbContext> options) : base(options) { }

        // DbSet
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Title> Titles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new CompanyConfig());
            builder.ApplyConfiguration(new SectorConfig());
            builder.ApplyConfiguration(new TitleConfig());

            base.OnModelCreating(builder);
        }
    }
}
