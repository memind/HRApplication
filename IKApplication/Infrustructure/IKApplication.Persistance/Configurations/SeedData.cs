using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace IKApplication.Persistance.Configurations
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            Random random = new Random();
            PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();

            List<Sector> Sectors = new List<Sector>();
            List<Company> Companies = new List<Company>();
            List<Title> Titles = new List<Title>();
            List<IdentityRole<Guid>> Roles = new List<IdentityRole<Guid>>();
            List<AppUser> AppUsers = new List<AppUser>();
            List<IdentityUserRole<Guid>> userRoles = new List<IdentityUserRole<Guid>>();

            // Create Sectors
            foreach (var item in SeedDataConstantsandMethods.Sectors)
            {
                Sectors.Add(new Sector()
                {
                    Id = Guid.NewGuid(),
                    Name = item,
                    CreateDate = DateTime.Now,
                    Status = Status.Active
                });
            }

            //Create Roles
            Roles.Add(new IdentityRole<Guid>()
            {
                Id = Guid.NewGuid(),
                Name = "Site Administrator",
                NormalizedName = "SITE ADMINITRATOR",
                ConcurrencyStamp = SeedDataConstantsandMethods.StringGenerator(32)
            });

            Roles.Add(new IdentityRole<Guid>()
            {
                Id = Guid.NewGuid(),
                Name = "Company Administrator",
                NormalizedName = "COMPANY ADMINITRATOR",
                ConcurrencyStamp = SeedDataConstantsandMethods.StringGenerator(32)
            });

            Roles.Add(new IdentityRole<Guid>()
            {
                Id = Guid.NewGuid(),
                Name = "Personal",
                NormalizedName = "PERSONAL",
                ConcurrencyStamp = SeedDataConstantsandMethods.StringGenerator(32)
            });

            //Create Company, Title, System Admins
            Companies.Add(new Company()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Status = Status.Active,
                Name = "IKApp A.Ş.",
                Email = "ikapp@ikapp.com",
                PhoneNumber = SeedDataConstantsandMethods.PhoneNumberGenerator(),
                NumberOfEmployees = 5,
                SectorId = Sectors[1].Id
            });

            foreach (var item in SeedDataConstantsandMethods.Titles)
            {
                Titles.Add(new Title()
                {
                    Id = Guid.NewGuid(),
                    Name = item,
                    CompanyId = Companies[0].Id
                });
            }

            for (int i = 0; i < 5; i++)
            {
                var name = SeedDataConstantsandMethods.MaleNames[random.Next(SeedDataConstantsandMethods.MaleNames.Count)];
                var surname = SeedDataConstantsandMethods.Surnames[random.Next(SeedDataConstantsandMethods.Surnames.Count)];
                var email = "test" + (i + 1) + "@test.com";

                var user = new AppUser()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    Name = name,
                    Surname = surname,
                    TitleId = Titles[random.Next(Titles.Count)].Id,
                    Profession = SeedDataConstantsandMethods.Professions[random.Next(SeedDataConstantsandMethods.Professions.Count)],
                    BirthDate = SeedDataConstantsandMethods.randomDate(),
                    IdentityNumber = SeedDataConstantsandMethods.IdNumberGenerator(),
                    ImagePath = "/images/UserPhotos/defaultuser.jpg",
                    CompanyId = Companies[0].Id,
                    UserName = email,
                    NormalizedUserName = email.ToUpperInvariant(),
                    Email = email,
                    NormalizedEmail = email.ToUpperInvariant(),
                    EmailConfirmed = false,
                    SecurityStamp = SeedDataConstantsandMethods.StringGenerator(32),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = SeedDataConstantsandMethods.PhoneNumberGenerator(),
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                };

                user.PasswordHash = hasher.HashPassword(user, "123");

                AppUsers.Add(user);

                userRoles.Add(new IdentityUserRole<Guid>()
                {
                    RoleId = Roles[0].Id,
                    UserId = user.Id,
                });
            }

            for (int i = 0; i < 15; i++)
            {
                var userName = SeedDataConstantsandMethods.MaleNames[random.Next(SeedDataConstantsandMethods.MaleNames.Count)];
                var userSurname = SeedDataConstantsandMethods.Surnames[random.Next(SeedDataConstantsandMethods.Surnames.Count)];
                var userEmail = (userName + "." + userSurname + "@" + SeedDataConstantsandMethods.DomainNames[random.Next(SeedDataConstantsandMethods.DomainNames.Count)]).ToLower().Replace(" ", "").Replace('ğ', 'g').Replace('ü', 'u').Replace('ş', 's').Replace('ı', 'i').Replace('ö', 'o').Replace('ç', 'c');
                var companyId = Guid.NewGuid();
                var companyName = userSurname + " " + SeedDataConstantsandMethods.CompanyTypes[random.Next(SeedDataConstantsandMethods.CompanyTypes.Count)];
                var companyEmail = "info@" + companyName.ToLower().Replace(" ", "").Replace('ğ', 'g').Replace('ü', 'u').Replace('ş', 's').Replace('ı', 'i').Replace('ö', 'o').Replace('ç', 'c') + ".com";

                Companies.Add(new Company()
                {
                    Id = companyId,
                    CreateDate = DateTime.Now,
                    Status = Status.Passive,
                    Name = companyName,
                    Email = companyEmail,
                    PhoneNumber = SeedDataConstantsandMethods.PhoneNumberGenerator(),
                    NumberOfEmployees = random.Next(100) + 1,
                    SectorId = Sectors[random.Next(Sectors.Count)].Id
                });

                var titleId = Guid.NewGuid();

                Titles.Add(new Title()
                {
                    Id = titleId,
                    Name = SeedDataConstantsandMethods.Titles[random.Next(SeedDataConstantsandMethods.Titles.Count)],
                    CompanyId = companyId
                });

                var user = new AppUser()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.Now,
                    Status = Status.Passive,
                    Name = userName,
                    Surname = userSurname,
                    TitleId = titleId,
                    Profession = SeedDataConstantsandMethods.Professions[random.Next(SeedDataConstantsandMethods.Professions.Count)],
                    BirthDate = SeedDataConstantsandMethods.randomDate(),
                    IdentityNumber = SeedDataConstantsandMethods.IdNumberGenerator(),
                    ImagePath = "/images/UserPhotos/defaultuser.jpg",
                    CompanyId = companyId,
                    UserName = userEmail,
                    NormalizedUserName = userEmail.ToUpperInvariant(),
                    Email = userEmail,
                    NormalizedEmail = userEmail.ToUpperInvariant(),
                    EmailConfirmed = false,
                    SecurityStamp = SeedDataConstantsandMethods.StringGenerator(32),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = SeedDataConstantsandMethods.PhoneNumberGenerator(),
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                };

                user.PasswordHash = hasher.HashPassword(user, "123");

                AppUsers.Add(user);

                userRoles.Add(new IdentityUserRole<Guid>()
                {
                    RoleId = Roles[1].Id,
                    UserId = user.Id,
                });
            }

            builder.Entity<Sector>().HasData(Sectors);
            builder.Entity<Company>().HasData(Companies);
            builder.Entity<Title>().HasData(Titles);
            builder.Entity<IdentityRole<Guid>>().HasData(Roles);
            builder.Entity<AppUser>().HasData(AppUsers);
            builder.Entity<IdentityUserRole<Guid>>().HasData(userRoles);
        }
    }
}
