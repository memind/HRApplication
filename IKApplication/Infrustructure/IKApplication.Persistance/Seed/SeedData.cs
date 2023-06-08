using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace IKApplication.Persistance.Seed
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            Random random = new Random();
            PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();

            int alreadyRegisteredCompanyCount = 6;
            int deletedCompanyCount = 4;
            int passiveCompanyCount = 10;
            int totalTitleCount = SeedDataConstantsandMethods.Titles.Count;
            int totalProfessionCount = SeedDataConstantsandMethods.Professions.Count;
            int totalUserCount = 0;

            List<IdentityRole<Guid>> Roles = new List<IdentityRole<Guid>>();
            List<Sector> Sectors = new List<Sector>();
            List<Company> Companies = new List<Company>();
            List<Title> Titles = new List<Title>();
            List<Profession> Professions = new List<Profession>();
            List<AppUser> AppUsers = new List<AppUser>();
            List<IdentityUserRole<Guid>> userRoles = new List<IdentityUserRole<Guid>>();
            List<CashAdvance> CashAdvances = new List<CashAdvance>();
            List<Expense> Expenses = new List<Expense>();
            List<Leave> Leaves = new List<Leave>();

            //Create Roles
            Roles.Add(new IdentityRole<Guid>()
            {
                Id = Guid.NewGuid(),
                Name = "Site Administrator",
                NormalizedName = "SITE ADMINISTRATOR",
                ConcurrencyStamp = SeedDataConstantsandMethods.StringGenerator(32)
            });

            Roles.Add(new IdentityRole<Guid>()
            {
                Id = Guid.NewGuid(),
                Name = "Company Administrator",
                NormalizedName = "COMPANY ADMINISTRATOR",
                ConcurrencyStamp = SeedDataConstantsandMethods.StringGenerator(32)
            });

            Roles.Add(new IdentityRole<Guid>()
            {
                Id = Guid.NewGuid(),
                Name = "Personal",
                NormalizedName = "PERSONAL",
                ConcurrencyStamp = SeedDataConstantsandMethods.StringGenerator(32)
            });

            // Create Sectors
            foreach (var item in SeedDataConstantsandMethods.Sectors)
            {
                Sectors.Add(new Sector()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    Name = item,
                });
            }

            //Create Main Company
            Companies.Add(new Company()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Status = Status.Active,
                Name = "HRApp A.Ş.",
                Email = "hrapp@gmail.com",
                PhoneNumber = "+905555555555",
                NumberOfEmployees = 1,
                SectorId = Sectors[1].Id
            });

            //Create SiteManager's Title
            Titles.Add(new Title()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Status = Status.Active,
                Name = "CTO",
                CompanyId = Companies[0].Id,
            });

            //Create SiteManager's Profession
            Professions.Add(new Profession()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Status = Status.Active,
                Name = "Engineer",
                CompanyId = Companies[0].Id,
            });

            //Create SiteManager
            string siteManagerEmail = "taha.kayapinar@hrapp.com";
            Guid siteManagerId = Guid.NewGuid();

            var user = new AppUser()
            {
                Id = siteManagerId,
                CreateDate = DateTime.Now,
                Status = Status.Active,
                Name = "Taha",
                Surname = "Kayapınar",
                BloodGroup = BloodGroup.ABNegative,
                BirthDate = SeedDataConstantsandMethods.randomDate(),
                IdentityNumber = SeedDataConstantsandMethods.IdNumberGenerator(),
                PersonalEmail = "tahakayapinar@gmail.com",
                JobStartDate = DateTime.Now,
                ImagePath = "/images/UserPhotos/defaultuser.jpg",
                CompanyId = Companies[0].Id,
                TitleId = Titles[0].Id,
                PatronId = siteManagerId,
                ProfessionId = Professions[0].Id,
                UserName = siteManagerEmail,
                NormalizedUserName = siteManagerEmail.ToUpperInvariant(),
                Email = siteManagerEmail,
                NormalizedEmail = siteManagerEmail.ToUpperInvariant(),
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
            totalUserCount++;

            //Add Site Manager Role to User
            userRoles.Add(new IdentityUserRole<Guid>()
            {
                RoleId = Roles[0].Id,
                UserId = user.Id,
            });

            //Create Already registered Companies
            for (int i = 0; i < alreadyRegisteredCompanyCount; i++)
            {
                string nameOfCompanyManager = SeedDataConstantsandMethods.MaleNames[random.Next(0, SeedDataConstantsandMethods.MaleNames.Count - 1)];
                string surnameOfCompanyManager = SeedDataConstantsandMethods.Surnames[random.Next(0, SeedDataConstantsandMethods.Surnames.Count - 1)];
                string companyType = SeedDataConstantsandMethods.CompanyTypes[random.Next(0, SeedDataConstantsandMethods.CompanyTypes.Count - 1)];
                string companyName = surnameOfCompanyManager + " " + companyType;
                string companyEmailDomain = surnameOfCompanyManager + ".com";
                string companyEmail = "info@" + companyEmailDomain;
                int numberOfEmployess = random.Next(3, 15);
                int titleCountForCompany = random.Next(3, 10);
                int previousTitleIndex = -1;
                int professionCountForCompany = random.Next(3, 10);
                int previousProfessionIndex = -1;
                List<Title> CompanyTitles = new List<Title>();
                List<Profession> CompanyProfessions = new List<Profession>();

                Company company = new Company()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    Name = companyName,
                    Email = companyEmail,
                    PhoneNumber = SeedDataConstantsandMethods.PhoneNumberGenerator(),
                    NumberOfEmployees = numberOfEmployess,
                    SectorId = Sectors[random.Next(0, Sectors.Count - 1)].Id
                };

                Companies.Add(company);

                for (int j = 0; j < titleCountForCompany; j++)
                {
                    int currentTitleIndex = random.Next(previousTitleIndex + 1, totalTitleCount + j - titleCountForCompany);
                    string titleName = SeedDataConstantsandMethods.Titles[currentTitleIndex];
                    previousTitleIndex = currentTitleIndex;

                    Title title = new Title()
                    {
                        Id = Guid.NewGuid(),
                        CreateDate = DateTime.Now,
                        Status = Status.Active,
                        Name = titleName,
                        CompanyId = company.Id
                    };

                    CompanyTitles.Add(title);
                    Titles.Add(title);
                }

                for (int j = 0; j < professionCountForCompany; j++)
                {
                    int currentProfessionIndex = random.Next(previousProfessionIndex + 1, totalProfessionCount + j - professionCountForCompany);
                    string professionName = SeedDataConstantsandMethods.Professions[currentProfessionIndex];
                    previousProfessionIndex = currentProfessionIndex;

                    Profession profession = new Profession()
                    {
                        Id = Guid.NewGuid(),
                        CreateDate = DateTime.Now,
                        Status = Status.Active,
                        Name = professionName,
                        CompanyId = company.Id
                    };

                    CompanyProfessions.Add(profession);
                    Professions.Add(profession);
                }

                Guid companyManagerId = Guid.NewGuid();
                var personalEmail = (nameOfCompanyManager + "." + surnameOfCompanyManager + "@" + SeedDataConstantsandMethods.DomainNames[random.Next(SeedDataConstantsandMethods.DomainNames.Count - 1)]).ToLower().Replace(" ", "").Replace('ğ', 'g').Replace('ü', 'u').Replace('ş', 's').Replace('ı', 'i').Replace('ö', 'o').Replace('ç', 'c');
                var companyManagerEmail = (nameOfCompanyManager + "." + surnameOfCompanyManager + totalUserCount + "@" + companyEmailDomain).ToLower().Replace(" ", "").Replace('ğ', 'g').Replace('ü', 'u').Replace('ş', 's').Replace('ı', 'i').Replace('ö', 'o').Replace('ç', 'c');

                var companyManager = new AppUser()
                {
                    Id = companyManagerId,
                    CreateDate = DateTime.Now,
                    Status = Status.Active,
                    Name = nameOfCompanyManager,
                    Surname = surnameOfCompanyManager,
                    BloodGroup = BloodGroup.ABNegative,
                    BirthDate = SeedDataConstantsandMethods.randomDate(),
                    IdentityNumber = SeedDataConstantsandMethods.IdNumberGenerator(),
                    PersonalEmail = personalEmail,
                    JobStartDate = DateTime.Now,
                    ImagePath = "/images/UserPhotos/defaultuser.jpg",
                    CompanyId = company.Id,
                    TitleId = CompanyTitles[random.Next(0, titleCountForCompany - 1)].Id,
                    PatronId = companyManagerId,
                    ProfessionId = CompanyProfessions[random.Next(0, professionCountForCompany - 1)].Id,
                    UserName = companyManagerEmail,
                    NormalizedUserName = companyManagerEmail.ToUpperInvariant(),
                    Email = companyManagerEmail,
                    NormalizedEmail = companyManagerEmail.ToUpperInvariant(),
                    EmailConfirmed = false,
                    SecurityStamp = SeedDataConstantsandMethods.StringGenerator(32),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = SeedDataConstantsandMethods.PhoneNumberGenerator(),
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                };

                companyManager.PasswordHash = hasher.HashPassword(companyManager, "123");

                AppUsers.Add(companyManager);
                totalUserCount++;

                //Add Company Manager Role to User
                userRoles.Add(new IdentityUserRole<Guid>()
                {
                    RoleId = Roles[1].Id,
                    UserId = companyManager.Id,
                });

                for (int j = 0; j < numberOfEmployess - 1; j++)
                {
                    Guid standardUserId = Guid.NewGuid();
                    string standardUserName = SeedDataConstantsandMethods.MaleNames[random.Next(0, SeedDataConstantsandMethods.MaleNames.Count - 1)];
                    string standardUserSurname = SeedDataConstantsandMethods.Surnames[random.Next(0, SeedDataConstantsandMethods.Surnames.Count - 1)];
                    var standardUserPersonalEmail = (standardUserName + "." + standardUserSurname + "@" + SeedDataConstantsandMethods.DomainNames[random.Next(SeedDataConstantsandMethods.DomainNames.Count - 1)]).ToLower().Replace(" ", "").Replace('ğ', 'g').Replace('ü', 'u').Replace('ş', 's').Replace('ı', 'i').Replace('ö', 'o').Replace('ç', 'c');
                    var standardUserEmail = (standardUserName + "." + standardUserSurname + totalUserCount + "@" + companyEmailDomain).ToLower().Replace(" ", "").Replace('ğ', 'g').Replace('ü', 'u').Replace('ş', 's').Replace('ı', 'i').Replace('ö', 'o').Replace('ç', 'c');

                    var standardUser = new AppUser()
                    {
                        Id = standardUserId,
                        CreateDate = DateTime.Now,
                        Status = Status.Active,
                        Name = standardUserName,
                        Surname = standardUserSurname,
                        BloodGroup = BloodGroup.ABNegative,
                        BirthDate = SeedDataConstantsandMethods.randomDate(),
                        IdentityNumber = SeedDataConstantsandMethods.IdNumberGenerator(),
                        PersonalEmail = standardUserPersonalEmail,
                        JobStartDate = DateTime.Now,
                        ImagePath = "/images/UserPhotos/defaultuser.jpg",
                        CompanyId = company.Id,
                        TitleId = CompanyTitles[random.Next(0, titleCountForCompany - 1)].Id,
                        PatronId = companyManagerId,
                        ProfessionId = CompanyProfessions[random.Next(0, professionCountForCompany - 1)].Id,
                        UserName = standardUserEmail,
                        NormalizedUserName = standardUserEmail.ToUpperInvariant(),
                        Email = standardUserEmail,
                        NormalizedEmail = standardUserEmail.ToUpperInvariant(),
                        EmailConfirmed = false,
                        SecurityStamp = SeedDataConstantsandMethods.StringGenerator(32),
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        PhoneNumber = SeedDataConstantsandMethods.PhoneNumberGenerator(),
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0
                    };

                    standardUser.PasswordHash = hasher.HashPassword(standardUser, "123");

                    AppUsers.Add(standardUser);
                    totalUserCount++;

                    //Add Company Manager Role to User
                    userRoles.Add(new IdentityUserRole<Guid>()
                    {
                        RoleId = Roles[2].Id,
                        UserId = standardUser.Id,
                    });

                    int numberOfExpenses = random.Next(0, 5);
                    int numberOfCashAdvances = random.Next(0, 5);
                    int numberOfLeaves = random.Next(0, 3);

                    for (int k = 0; k < numberOfExpenses; k++)
                    {
                        var expense = new Expense()
                        {
                            Id = Guid.NewGuid(),
                            CreateDate = DateTime.Now,
                            Status = (Status)random.Next(1, 4),
                            ShortDescription = totalUserCount + ". user's " + k + ". expense Desc.",
                            LongDescription = totalUserCount + ". user's " + k + ". expense Long Desc.",
                            Amount = (Decimal)random.NextDouble() * 400 + 100,
                            ExpenseDate = DateTime.Now.AddDays(-1 * random.Next(10, 60)),
                            ApprovedById = companyManager.Id,
                            ExpenseById = standardUser.Id,
                            CompanyId = company.Id,
                            Type = (ExpenseType)random.Next(1, 14),
                            Currency = (Currency)random.Next(0, 3),
                        };

                        Expenses.Add(expense);
                    }

                    for (int k = 0; k < numberOfCashAdvances; k++)
                    {
                        var cashAdvance = new CashAdvance()
                        {
                            Id = Guid.NewGuid(),
                            CreateDate = DateTime.Now,
                            Status = (Status)random.Next(1, 4),
                            Description = totalUserCount + ". user's " + k + ". cash advance Desc.",
                            RequestedAmount = (Decimal)random.NextDouble() * 400 + 100,
                            IsPaymentProcessed = (PaymentStatus)random.Next(1, 3),
                            InstallmentCount = random.Next(1, 10),
                            FinalDateRequest = DateTime.Now.AddDays(random.Next(10)),
                            CompanyId = company.Id,
                            AdvanceToId = standardUser.Id,
                            DirectorId = companyManager.Id,
                            Currency = (Currency)random.Next(0, 3),
                        };

                        CashAdvances.Add(cashAdvance);
                    }

                    for (int k = 0; k < numberOfLeaves; k++)
                    {
                        int totalLeaveDays = random.Next(1, 6);
                        DateTime startDate = DateTime.Now.AddDays(random.Next(0, 50) - 25);

                        var leave = new Leave()
                        {
                            Id = Guid.NewGuid(),
                            CreateDate = DateTime.Now,
                            Status = (Status)random.Next(1, 4),
                            Explanation = totalUserCount + ". user's " + k + ". Leave Desc.",
                            LeaveType = (LeaveType)random.Next(1, 8),
                            LeaveStatus = (LeaveStatus)random.Next(1, 4),
                            StartDate = startDate,
                            EndDate = startDate.AddDays(totalLeaveDays),
                            TotalLeaveDays = totalLeaveDays,
                            CompanyId = company.Id,
                            AppUserId = standardUser.Id,
                            ApprovedById = companyManager.Id,
                        };

                        Leaves.Add(leave);
                    }
                }
            }

            for (int i = 0; i < passiveCompanyCount; i++)
            {
                string nameOfCompanyManager = SeedDataConstantsandMethods.MaleNames[random.Next(0, SeedDataConstantsandMethods.MaleNames.Count - 1)];
                string surnameOfCompanyManager = SeedDataConstantsandMethods.Surnames[random.Next(0, SeedDataConstantsandMethods.Surnames.Count - 1)];
                string companyType = SeedDataConstantsandMethods.CompanyTypes[random.Next(0, SeedDataConstantsandMethods.CompanyTypes.Count - 1)];
                string companyName = surnameOfCompanyManager + " " + companyType;
                string companyEmailDomain = surnameOfCompanyManager + ".com";
                string companyEmail = "info@" + companyEmailDomain;
                int numberOfEmployess = random.Next(3, 15);

                DateTime creationDate = DateTime.Now.AddDays(random.Next(1, 15) * -1);

                Company company = new Company()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = creationDate,
                    Status = Status.Passive,
                    Name = companyName,
                    Email = companyEmail,
                    PhoneNumber = SeedDataConstantsandMethods.PhoneNumberGenerator(),
                    NumberOfEmployees = numberOfEmployess,
                    SectorId = Sectors[random.Next(0, Sectors.Count - 1)].Id
                };

                Companies.Add(company);

                int currentTitleIndex = random.Next(totalTitleCount);
                string titleName = SeedDataConstantsandMethods.Titles[currentTitleIndex];

                Title title = new Title()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = creationDate,
                    Status = Status.Active,
                    Name = titleName,
                    CompanyId = company.Id
                };

                Titles.Add(title);

                int currentProfessionIndex = random.Next(totalProfessionCount);
                string professionName = SeedDataConstantsandMethods.Professions[currentProfessionIndex];

                Profession profession = new Profession()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = creationDate,
                    Status = Status.Active,
                    Name = professionName,
                    CompanyId = company.Id
                };

                Professions.Add(profession);

                Guid companyManagerId = Guid.NewGuid();
                var personalEmail = (nameOfCompanyManager + "." + surnameOfCompanyManager + "@" + SeedDataConstantsandMethods.DomainNames[random.Next(SeedDataConstantsandMethods.DomainNames.Count - 1)]).ToLower().Replace(" ", "").Replace('ğ', 'g').Replace('ü', 'u').Replace('ş', 's').Replace('ı', 'i').Replace('ö', 'o').Replace('ç', 'c');
                var companyManagerEmail = (nameOfCompanyManager + "." + surnameOfCompanyManager + totalUserCount + "@" + companyEmailDomain).ToLower().Replace(" ", "").Replace('ğ', 'g').Replace('ü', 'u').Replace('ş', 's').Replace('ı', 'i').Replace('ö', 'o').Replace('ç', 'c');

                var companyManager = new AppUser()
                {
                    Id = companyManagerId,
                    CreateDate = creationDate,
                    Status = Status.Passive,
                    Name = nameOfCompanyManager,
                    Surname = surnameOfCompanyManager,
                    BloodGroup = BloodGroup.ABNegative,
                    BirthDate = SeedDataConstantsandMethods.randomDate(),
                    IdentityNumber = SeedDataConstantsandMethods.IdNumberGenerator(),
                    PersonalEmail = personalEmail,
                    JobStartDate = DateTime.Now,
                    ImagePath = "/images/UserPhotos/defaultuser.jpg",
                    CompanyId = company.Id,
                    TitleId = title.Id,
                    PatronId = companyManagerId,
                    ProfessionId = profession.Id,
                    UserName = companyManagerEmail,
                    NormalizedUserName = companyManagerEmail.ToUpperInvariant(),
                    Email = companyManagerEmail,
                    NormalizedEmail = companyManagerEmail.ToUpperInvariant(),
                    EmailConfirmed = false,
                    SecurityStamp = SeedDataConstantsandMethods.StringGenerator(32),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = SeedDataConstantsandMethods.PhoneNumberGenerator(),
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                };

                companyManager.PasswordHash = hasher.HashPassword(companyManager, "123");

                AppUsers.Add(companyManager);
                totalUserCount++;

                //Add Company Manager Role to User
                userRoles.Add(new IdentityUserRole<Guid>()
                {
                    RoleId = Roles[1].Id,
                    UserId = companyManager.Id,
                });
            }

            for (int i = 0; i < deletedCompanyCount; i++)
            {
                string nameOfCompanyManager = SeedDataConstantsandMethods.MaleNames[random.Next(0, SeedDataConstantsandMethods.MaleNames.Count - 1)];
                string surnameOfCompanyManager = SeedDataConstantsandMethods.Surnames[random.Next(0, SeedDataConstantsandMethods.Surnames.Count - 1)];
                string companyType = SeedDataConstantsandMethods.CompanyTypes[random.Next(0, SeedDataConstantsandMethods.CompanyTypes.Count - 1)];
                string companyName = surnameOfCompanyManager + " " + companyType;
                string companyEmailDomain = surnameOfCompanyManager + ".com";
                string companyEmail = "info@" + companyEmailDomain;
                int numberOfEmployess = random.Next(3, 15);
                int titleCountForCompany = random.Next(3, 10);
                int previousTitleIndex = -1;
                int professionCountForCompany = random.Next(3, 10);
                int previousProfessionIndex = -1;
                List<Title> CompanyTitles = new List<Title>();
                List<Profession> CompanyProfessions = new List<Profession>();

                Company company = new Company()
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.Now.AddDays(-50),
                    DeleteDate = DateTime.Now,
                    Status = Status.Deleted,
                    Name = companyName,
                    Email = companyEmail,
                    PhoneNumber = SeedDataConstantsandMethods.PhoneNumberGenerator(),
                    NumberOfEmployees = numberOfEmployess,
                    SectorId = Sectors[random.Next(0, Sectors.Count - 1)].Id
                };

                Companies.Add(company);

                for (int j = 0; j < titleCountForCompany; j++)
                {
                    int currentTitleIndex = random.Next(previousTitleIndex + 1, totalTitleCount + j - titleCountForCompany);
                    string titleName = SeedDataConstantsandMethods.Titles[currentTitleIndex];
                    previousTitleIndex = currentTitleIndex;

                    Title title = new Title()
                    {
                        Id = Guid.NewGuid(),
                        CreateDate = DateTime.Now.AddDays(-50),
                        DeleteDate = DateTime.Now,
                        Status = Status.Deleted,
                        Name = titleName,
                        CompanyId = company.Id
                    };

                    CompanyTitles.Add(title);
                    Titles.Add(title);
                }

                for (int j = 0; j < professionCountForCompany; j++)
                {
                    int currentProfessionIndex = random.Next(previousProfessionIndex + 1, totalProfessionCount + j - professionCountForCompany);
                    string professionName = SeedDataConstantsandMethods.Professions[currentProfessionIndex];
                    previousProfessionIndex = currentProfessionIndex;

                    Profession profession = new Profession()
                    {
                        Id = Guid.NewGuid(),
                        CreateDate = DateTime.Now.AddDays(-50),
                        DeleteDate = DateTime.Now,
                        Status = Status.Deleted,
                        Name = professionName,
                        CompanyId = company.Id
                    };

                    CompanyProfessions.Add(profession);
                    Professions.Add(profession);
                }

                Guid companyManagerId = Guid.NewGuid();
                var personalEmail = (nameOfCompanyManager + "." + surnameOfCompanyManager + "@" + SeedDataConstantsandMethods.DomainNames[random.Next(SeedDataConstantsandMethods.DomainNames.Count - 1)]).ToLower().Replace(" ", "").Replace('ğ', 'g').Replace('ü', 'u').Replace('ş', 's').Replace('ı', 'i').Replace('ö', 'o').Replace('ç', 'c');
                var companyManagerEmail = (nameOfCompanyManager + "." + surnameOfCompanyManager + totalUserCount + "@" + companyEmailDomain).ToLower().Replace(" ", "").Replace('ğ', 'g').Replace('ü', 'u').Replace('ş', 's').Replace('ı', 'i').Replace('ö', 'o').Replace('ç', 'c');

                var companyManager = new AppUser()
                {
                    Id = companyManagerId,
                    CreateDate = DateTime.Now.AddDays(-50),
                    DeleteDate = DateTime.Now,
                    Status = Status.Deleted,
                    Name = nameOfCompanyManager,
                    Surname = surnameOfCompanyManager,
                    BloodGroup = BloodGroup.ABNegative,
                    BirthDate = SeedDataConstantsandMethods.randomDate(),
                    IdentityNumber = SeedDataConstantsandMethods.IdNumberGenerator(),
                    PersonalEmail = personalEmail,
                    JobStartDate = DateTime.Now,
                    ImagePath = "/images/UserPhotos/defaultuser.jpg",
                    CompanyId = company.Id,
                    TitleId = CompanyTitles[random.Next(0, titleCountForCompany - 1)].Id,
                    PatronId = companyManagerId,
                    ProfessionId = CompanyProfessions[random.Next(0, professionCountForCompany - 1)].Id,
                    UserName = companyManagerEmail,
                    NormalizedUserName = companyManagerEmail.ToUpperInvariant(),
                    Email = companyManagerEmail,
                    NormalizedEmail = companyManagerEmail.ToUpperInvariant(),
                    EmailConfirmed = false,
                    SecurityStamp = SeedDataConstantsandMethods.StringGenerator(32),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = SeedDataConstantsandMethods.PhoneNumberGenerator(),
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                };

                companyManager.PasswordHash = hasher.HashPassword(companyManager, "123");

                AppUsers.Add(companyManager);
                totalUserCount++;

                //Add Company Manager Role to User
                userRoles.Add(new IdentityUserRole<Guid>()
                {
                    RoleId = Roles[1].Id,
                    UserId = companyManager.Id,
                });

                for (int j = 0; j < numberOfEmployess - 1; j++)
                {
                    Guid standardUserId = Guid.NewGuid();
                    string standardUserName = SeedDataConstantsandMethods.MaleNames[random.Next(0, SeedDataConstantsandMethods.MaleNames.Count - 1)];
                    string standardUserSurname = SeedDataConstantsandMethods.Surnames[random.Next(0, SeedDataConstantsandMethods.Surnames.Count - 1)];
                    var standardUserPersonalEmail = (standardUserName + "." + standardUserSurname + "@" + SeedDataConstantsandMethods.DomainNames[random.Next(SeedDataConstantsandMethods.DomainNames.Count - 1)]).ToLower().Replace(" ", "").Replace('ğ', 'g').Replace('ü', 'u').Replace('ş', 's').Replace('ı', 'i').Replace('ö', 'o').Replace('ç', 'c');
                    var standardUserEmail = (standardUserName + "." + standardUserSurname + totalUserCount + "@" + companyEmailDomain).ToLower().Replace(" ", "").Replace('ğ', 'g').Replace('ü', 'u').Replace('ş', 's').Replace('ı', 'i').Replace('ö', 'o').Replace('ç', 'c');

                    var standardUser = new AppUser()
                    {
                        Id = standardUserId,
                        CreateDate = DateTime.Now.AddDays(-50),
                        DeleteDate = DateTime.Now,
                        Status = Status.Deleted,
                        Name = standardUserName,
                        Surname = standardUserSurname,
                        BloodGroup = BloodGroup.ABNegative,
                        BirthDate = SeedDataConstantsandMethods.randomDate(),
                        IdentityNumber = SeedDataConstantsandMethods.IdNumberGenerator(),
                        PersonalEmail = standardUserPersonalEmail,
                        JobStartDate = DateTime.Now,
                        ImagePath = "/images/UserPhotos/defaultuser.jpg",
                        CompanyId = company.Id,
                        TitleId = CompanyTitles[random.Next(0, titleCountForCompany - 1)].Id,
                        PatronId = companyManagerId,
                        ProfessionId = CompanyProfessions[random.Next(0, professionCountForCompany - 1)].Id,
                        UserName = standardUserEmail,
                        NormalizedUserName = standardUserEmail.ToUpperInvariant(),
                        Email = standardUserEmail,
                        NormalizedEmail = standardUserEmail.ToUpperInvariant(),
                        EmailConfirmed = false,
                        SecurityStamp = SeedDataConstantsandMethods.StringGenerator(32),
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        PhoneNumber = SeedDataConstantsandMethods.PhoneNumberGenerator(),
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0
                    };

                    standardUser.PasswordHash = hasher.HashPassword(standardUser, "123");

                    AppUsers.Add(standardUser);
                    totalUserCount++;

                    //Add Company Manager Role to User
                    userRoles.Add(new IdentityUserRole<Guid>()
                    {
                        RoleId = Roles[2].Id,
                        UserId = standardUser.Id,
                    });

                    int numberOfExpenses = random.Next(0, 5);
                    int numberOfCashAdvances = random.Next(0, 5);
                    int numberOfLeaves = random.Next(0, 3);

                    for (int k = 0; k < numberOfExpenses; k++)
                    {
                        var expense = new Expense()
                        {
                            Id = Guid.NewGuid(),
                            CreateDate = DateTime.Now.AddDays(-50),
                            DeleteDate = DateTime.Now,
                            Status = Status.Deleted,
                            ShortDescription = totalUserCount + ". user's " + k + ". expense Desc.",
                            LongDescription = totalUserCount + ". user's " + k + ". expense Long Desc.",
                            Amount = (Decimal)random.NextDouble() * 400 + 100,
                            ExpenseDate = DateTime.Now.AddDays(-1 * random.Next(10, 60)),
                            ApprovedById = companyManager.Id,
                            ExpenseById = standardUser.Id,
                            CompanyId = company.Id,
                            Type = (ExpenseType)random.Next(1, 14),
                            Currency = (Currency)random.Next(0, 3),
                        };

                        Expenses.Add(expense);
                    }

                    for (int k = 0; k < numberOfCashAdvances; k++)
                    {
                        var cashAdvance = new CashAdvance()
                        {
                            Id = Guid.NewGuid(),
                            CreateDate = DateTime.Now.AddDays(-50),
                            DeleteDate = DateTime.Now,
                            Status = Status.Deleted,
                            Description = totalUserCount + ". user's " + k + ". cash advance Desc.",
                            RequestedAmount = (Decimal)random.NextDouble() * 400 + 100,
                            IsPaymentProcessed = (PaymentStatus)random.Next(1, 3),
                            InstallmentCount = random.Next(1, 10),
                            FinalDateRequest = DateTime.Now.AddDays(random.Next(10)),
                            CompanyId = company.Id,
                            AdvanceToId = standardUser.Id,
                            DirectorId = companyManager.Id,
                            Currency = (Currency)random.Next(0, 3),
                        };

                        CashAdvances.Add(cashAdvance);
                    }

                    for (int k = 0; k < numberOfLeaves; k++)
                    {
                        int totalLeaveDays = random.Next(1, 6);
                        DateTime startDate = DateTime.Now.AddDays(random.Next(0, 50) - 25);

                        var leave = new Leave()
                        {
                            Id = Guid.NewGuid(),
                            CreateDate = DateTime.Now.AddDays(-50),
                            DeleteDate = DateTime.Now,
                            Status = Status.Deleted,
                            Explanation = totalUserCount + ". user's " + k + ". Leave Desc.",
                            LeaveType = (LeaveType)random.Next(1, 8),
                            LeaveStatus = (LeaveStatus)random.Next(1, 4),
                            StartDate = startDate,
                            EndDate = startDate.AddDays(totalLeaveDays),
                            TotalLeaveDays = totalLeaveDays,
                            CompanyId = company.Id,
                            AppUserId = standardUser.Id,
                            ApprovedById = companyManager.Id,
                        };

                        Leaves.Add(leave);
                    }
                }
            }

            builder.Entity<IdentityRole<Guid>>().HasData(Roles);
            builder.Entity<Sector>().HasData(Sectors);
            builder.Entity<Company>().HasData(Companies);
            builder.Entity<Title>().HasData(Titles);
            builder.Entity<Profession>().HasData(Professions);
            builder.Entity<AppUser>().HasData(AppUsers);
            builder.Entity<IdentityUserRole<Guid>>().HasData(userRoles);
            builder.Entity<CashAdvance>().HasData(CashAdvances);
            builder.Entity<Expense>().HasData(Expenses);
            builder.Entity<Leave>().HasData(Leaves);
        }
    }
}