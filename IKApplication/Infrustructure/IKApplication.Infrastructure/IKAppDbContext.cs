using IKApplication.Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IKApplication.Infrastructure
{
	public class IKAppDbContext : IdentityDbContext<SiteAdministrator>
	{

        public IKAppDbContext(DbContextOptions<IKAppDbContext> options) : base(options) { }


        // DbSet
        public DbSet<CompanyManager> CompanyManagers { get; set; }
        public DbSet<SiteAdministrator> SiteAdministrators { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Mapping Fluent API  EKLENECEK !


            base.OnModelCreating(builder);
        }


    }
}
