using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKApplication.Persistance.Migrations
{
    public partial class totalLeaveDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OpenAddress = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    City = table.Column<int>(type: "int", nullable: false),
                    District = table.Column<int>(type: "int", nullable: true),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfEmployees = table.Column<int>(type: "int", nullable: false),
                    SectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Titles_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BloodGroup = table.Column<int>(type: "int", nullable: true),
                    Profession = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentityNumber = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    JobStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatronId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_PatronId",
                        column: x => x.PatronId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashAdvances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RequestedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaymentProcessed = table.Column<int>(type: "int", nullable: false),
                    FinalDateRequest = table.Column<DateTime>(type: "date", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AdvanceToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DirectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashAdvances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashAdvances_AspNetUsers_AdvanceToId",
                        column: x => x.AdvanceToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashAdvances_AspNetUsers_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpenseById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_AspNetUsers_ExpenseById",
                        column: x => x.ExpenseById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Leaves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaveType = table.Column<int>(type: "int", nullable: false),
                    LeaveStatus = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalLeaveDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leaves_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leaves_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("03b82678-87fe-4887-86bb-78d550d93d2e"), "MUJ6X77TXGL3BE2QJU9Y5Q4BO8JM1V0F", "Site Administrator", "SITE ADMINISTRATOR" },
                    { new Guid("3f60e383-ddaf-482c-9f7f-0982d34d6ad0"), "YVXKHB9WRBTJMXWIYX1Y1OCHYGHB0MLP", "Personal", "PERSONAL" },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), "TS2TJWT8MVNJUIT59QKLDZLWB1VH0HA7", "Company Administrator", "COMPANY ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("12073634-f5ba-449b-9d41-a9aafc8c4429"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3806), null, "Otomotiv", 1, null },
                    { new Guid("1ca6df0b-529e-4972-ab52-851336bc39d4"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3798), null, "Maden", 1, null },
                    { new Guid("1dc4a384-574e-4c50-b095-edc51da44d36"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3813), null, "Tarım, Avcılık ve Balıkçılık", 1, null },
                    { new Guid("2853b6c6-e499-467b-bc22-e2db9459b7a1"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3803), null, "Medya, İletişim ve Yayıncılık", 1, null },
                    { new Guid("29499a8d-f87f-4b4c-9bf0-7a098d117914"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3701), null, "Elektrik ve Elektronik", 1, null },
                    { new Guid("2c47139c-33e9-49c1-b9a8-4b7ef20846b1"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3823), null, "Ulaştırma, Lojistik ve Haberleşme", 1, null },
                    { new Guid("2dff11e4-e699-4591-b329-9ba69b449577"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3793), null, "İş ve Yönetimi", 1, null },
                    { new Guid("310af52b-fa44-4ab2-b005-8c6945ac9048"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3789), null, "Gıda", 1, null },
                    { new Guid("38097d31-5e5f-4368-b42b-7fb1f892da98"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3797), null, "Kültür, Sanat ve Tasarım", 1, null },
                    { new Guid("3abe0ed1-f03f-4c92-8743-56cc657e7c6e"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3786), null, "Enerji", 1, null },
                    { new Guid("59393dcb-1853-439f-96bb-9a5a3f974dec"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3696), null, "Bilişim", 1, null },
                    { new Guid("635eb060-7767-4f4a-98e1-30de3ca70b4e"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3811), null, "Spor ve Rekreasyon", 1, null },
                    { new Guid("6bd82328-e1b0-4000-bf17-1790a28c0b86"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3815), null, "Tekstil, Hazır Giyim, Deri", 1, null },
                    { new Guid("7337aa77-85aa-407e-8929-6e3a750f64c6"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3821), null, "Turizm, Konaklama, Yiyecek-İçecek Hizmetleri", 1, null },
                    { new Guid("73564f12-904b-4e56-8ddf-65887f045754"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3788), null, "Finans", 1, null },
                    { new Guid("7f7bf24a-d470-44cd-8986-a4fdb9236da6"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3698), null, "Cam, Çimento ve ToprakÇevre", 1, null },
                    { new Guid("afa42780-9173-4d80-bc37-b372ce2ed474"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3805), null, "Metal", 1, null },
                    { new Guid("b514565d-6045-4829-91e1-22d8a10829f1"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3808), null, "Sağlık ve Sosyal Hizmetler", 1, null },
                    { new Guid("be04fdaa-f2f5-439b-a937-30ea3389288e"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3820), null, "Toplumsal ve Kişisel Hizmetler", 1, null },
                    { new Guid("c4f1a3bc-3723-4cca-a87f-4348972619f6"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3791), null, "İnşaat", 1, null },
                    { new Guid("d1cf6c4c-4996-4964-8101-4c7084bbef11"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3700), null, "Eğitim", 1, null },
                    { new Guid("eb3d38e4-9604-4eb3-9f86-7d1f62c04ac9"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3795), null, "Kimya, Petrol, Lastik ve PLastik", 1, null },
                    { new Guid("f1aaa859-d763-449d-8155-e7d990fd197f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3679), null, "Ahşap Teknolojisi", 1, null },
                    { new Guid("fc05fef3-c7da-4f95-afe2-929da3a5c709"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(3817), null, "Ticaret (Satış ve Pazarlama)", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "NumberOfEmployees", "PhoneNumber", "SectorId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("125a275f-35dc-41da-acde-fc5fef7d6914"), new DateTime(2023, 5, 24, 23, 21, 47, 997, DateTimeKind.Local).AddTicks(8138), null, "info@ozdemirkollektifsirketi.com", "Özdemir Kollektif Şirketi", 87, "+905297614506", new Guid("29499a8d-f87f-4b4c-9bf0-7a098d117914"), 3, null },
                    { new Guid("25d2a0de-be8c-4850-8bac-2f155de0d098"), new DateTime(2023, 5, 24, 23, 21, 48, 15, DateTimeKind.Local).AddTicks(4239), null, "info@celikkomanditsirketi.com", "Çelik Komandit Şirketi", 88, "+905919136928", new Guid("6bd82328-e1b0-4000-bf17-1790a28c0b86"), 3, null },
                    { new Guid("4f83adf7-8fa9-4c1e-b6c7-5718290f523a"), new DateTime(2023, 5, 24, 23, 21, 48, 9, DateTimeKind.Local).AddTicks(863), null, "info@celikkomanditsirketi.com", "Çelik Komandit Şirketi", 61, "+905946738532", new Guid("1ca6df0b-529e-4972-ab52-851336bc39d4"), 3, null },
                    { new Guid("5a9d227a-03e3-4a17-ae49-dddd1f20452a"), new DateTime(2023, 5, 24, 23, 21, 48, 1, DateTimeKind.Local).AddTicks(5258), null, "info@yildirimkooperatifsirketi.com", "Yıldırım Kooperatif Şirketi", 44, "+905309508500", new Guid("7f7bf24a-d470-44cd-8986-a4fdb9236da6"), 3, null },
                    { new Guid("79ef3352-91c5-4840-88ae-b5076f3db6cf"), new DateTime(2023, 5, 24, 23, 21, 48, 7, DateTimeKind.Local).AddTicks(8132), null, "info@yildizlimitedsirketi.com", "Yıldız Limited Şirketi", 31, "+905900674122", new Guid("1ca6df0b-529e-4972-ab52-851336bc39d4"), 3, null },
                    { new Guid("7b7b68d2-839c-4c78-8529-c091bfe05424"), new DateTime(2023, 5, 24, 23, 21, 48, 2, DateTimeKind.Local).AddTicks(7985), null, "info@aydinkomanditsirketi.com", "Aydın Komandit Şirketi", 7, "+905653426733", new Guid("f1aaa859-d763-449d-8155-e7d990fd197f"), 3, null },
                    { new Guid("89703451-fd75-4b63-aa70-d62b273c9f9e"), new DateTime(2023, 5, 24, 23, 21, 48, 11, DateTimeKind.Local).AddTicks(6613), null, "info@aydinlimitedsirketi.com", "Aydın Limited Şirketi", 10, "+905912757534", new Guid("6bd82328-e1b0-4000-bf17-1790a28c0b86"), 3, null },
                    { new Guid("9b09bc40-5ee7-4de1-9649-cc503037ff06"), new DateTime(2023, 5, 24, 23, 21, 47, 999, DateTimeKind.Local).AddTicks(672), null, "info@kayakomanditsirketi.com", "Kaya Komandit Şirketi", 19, "+905848742982", new Guid("3abe0ed1-f03f-4c92-8743-56cc657e7c6e"), 3, null },
                    { new Guid("9e0615ad-e0b4-46e7-a9fa-07bb6cc8044b"), new DateTime(2023, 5, 24, 23, 21, 48, 0, DateTimeKind.Local).AddTicks(2971), null, "info@sahinlimitedsirketi.com", "Şahin Limited Şirketi", 27, "+905626738106", new Guid("2853b6c6-e499-467b-bc22-e2db9459b7a1"), 3, null },
                    { new Guid("bfa1f2eb-867a-4068-9129-c2622111a84f"), new DateTime(2023, 5, 24, 23, 21, 48, 14, DateTimeKind.Local).AddTicks(1678), null, "info@sahinlimitedsirketi.com", "Şahin Limited Şirketi", 58, "+905807522282", new Guid("2dff11e4-e699-4591-b329-9ba69b449577"), 3, null },
                    { new Guid("cc8ec419-e188-4664-8cce-24d9d7db0d7f"), new DateTime(2023, 5, 24, 23, 21, 48, 6, DateTimeKind.Local).AddTicks(5120), null, "info@sahinlimitedsirketi.com", "Şahin Limited Şirketi", 72, "+905576108660", new Guid("7337aa77-85aa-407e-8929-6e3a750f64c6"), 3, null },
                    { new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4182), null, "ikapp@ikapp.com", "IKApp A.Ş.", 5, "+905695074111", new Guid("59393dcb-1853-439f-96bb-9a5a3f974dec"), 1, null },
                    { new Guid("d7427d72-19da-4e32-9cfd-9c1a5f1dbbb9"), new DateTime(2023, 5, 24, 23, 21, 48, 5, DateTimeKind.Local).AddTicks(2951), null, "info@ozdemiranonimsirketi.com", "Özdemir Anonim Şirketi", 100, "+905722203828", new Guid("3abe0ed1-f03f-4c92-8743-56cc657e7c6e"), 3, null },
                    { new Guid("e93f4331-4e64-4886-a6b6-ef87c48d1307"), new DateTime(2023, 5, 24, 23, 21, 48, 10, DateTimeKind.Local).AddTicks(3644), null, "info@yildirimanonimsirketi.com", "Yıldırım Anonim Şirketi", 63, "+905567573653", new Guid("1ca6df0b-529e-4972-ab52-851336bc39d4"), 3, null },
                    { new Guid("fca46396-6bbe-4429-bfcc-1392d058a5ec"), new DateTime(2023, 5, 24, 23, 21, 48, 4, DateTimeKind.Local).AddTicks(630), null, "info@ozturkkollektifsirketi.com", "Öztürk Kollektif Şirketi", 78, "+905212831775", new Guid("59393dcb-1853-439f-96bb-9a5a3f974dec"), 3, null },
                    { new Guid("fdcec0d7-359d-4866-bb82-3a13dab81940"), new DateTime(2023, 5, 24, 23, 21, 48, 12, DateTimeKind.Local).AddTicks(9221), null, "info@yildirimlimitedsirketi.com", "Yıldırım Limited Şirketi", 14, "+905570987318", new Guid("eb3d38e4-9604-4eb3-9f86-7d1f62c04ac9"), 3, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("0145678b-29da-4271-81bf-6eb19d8794fd"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4226), null, "Regional Sales Manager", 1, null },
                    { new Guid("05c8018f-53ca-47ed-b612-6c6ac9012d40"), new Guid("79ef3352-91c5-4840-88ae-b5076f3db6cf"), new DateTime(2023, 5, 24, 23, 21, 48, 7, DateTimeKind.Local).AddTicks(8137), null, "VP of Sales", 1, null },
                    { new Guid("0838c158-44a5-443b-8a9a-35c1d31e77d6"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4263), null, "COO (Chief Operating Officer)", 1, null },
                    { new Guid("0bd0a3c1-af0b-4c8d-bcda-bef8e6786ad6"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4240), null, "Marketing Analyst", 1, null },
                    { new Guid("0ea92fc7-6e3b-4bf6-be5d-f30209a4bab7"), new Guid("89703451-fd75-4b63-aa70-d62b273c9f9e"), new DateTime(2023, 5, 24, 23, 21, 48, 11, DateTimeKind.Local).AddTicks(6623), null, "VP of Client Services", 1, null },
                    { new Guid("115f8e76-6e21-4ab5-80ab-21a136e21ae2"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4373), null, "Safety Director", 1, null },
                    { new Guid("132212a7-024d-4d91-ac95-7a35e0305b22"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4368), null, "Server/Host/Hostess", 1, null },
                    { new Guid("1a3359a7-d65e-4f2a-b048-bff3ed017b72"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4338), null, "Physical Therapist", 1, null },
                    { new Guid("1dc04c09-458c-4335-b28d-5ce9fddf9c64"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4323), null, "Full Stack Developer", 1, null },
                    { new Guid("1fd5e6d2-cf69-4aff-8611-020922b3eacf"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4350), null, "Registrar", 1, null },
                    { new Guid("228f3752-4870-4b7e-9a32-36185f8efdbc"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4357), null, "Teaching Assistant", 1, null },
                    { new Guid("2445dcaa-7de1-40e8-9666-a65d9b8f55e2"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4251), null, "Risk Analyst", 1, null },
                    { new Guid("2539d3a4-c2a6-4d1e-81b0-f828a05a59df"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4272), null, "Director of Information Security", 1, null },
                    { new Guid("25dd0f37-2a96-4737-ae4c-ab88ef0f911a"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4354), null, "Teacher", 1, null },
                    { new Guid("27278d5b-9870-4e4f-8c11-c578b175bc17"), new Guid("bfa1f2eb-867a-4068-9129-c2622111a84f"), new DateTime(2023, 5, 24, 23, 21, 48, 14, DateTimeKind.Local).AddTicks(1681), null, "Front Desk Associate", 1, null },
                    { new Guid("2e5d6ab4-0498-4af4-93bc-64212e0e880d"), new Guid("4f83adf7-8fa9-4c1e-b6c7-5718290f523a"), new DateTime(2023, 5, 24, 23, 21, 48, 9, DateTimeKind.Local).AddTicks(866), null, "National Sales Director", 1, null },
                    { new Guid("36ba850e-4fb0-41a3-b7d7-18187cdaa192"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4341), null, "Clinical Laboratory Technician", 1, null },
                    { new Guid("38d5177f-61c8-4d6a-acac-753b52d466c2"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4248), null, "Investment Analyst", 1, null },
                    { new Guid("4b38434f-2c88-4e01-9b86-ac0fd2013084"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4325), null, "Systems Administrator", 1, null },
                    { new Guid("4c255a80-2340-4754-af81-9949e803d51f"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4343), null, "Occupational Therapy Aide", 1, null },
                    { new Guid("4c65989d-0f86-4bca-b793-bba29b74a7e9"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4339), null, "Nursing Assistant", 1, null },
                    { new Guid("561b6703-032c-4898-83a0-652662422653"), new Guid("9e0615ad-e0b4-46e7-a9fa-07bb6cc8044b"), new DateTime(2023, 5, 24, 23, 21, 48, 0, DateTimeKind.Local).AddTicks(2974), null, "Contract Administrator", 1, null },
                    { new Guid("56ffe139-f4e4-4d18-8d87-e0ca236b8c8b"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4224), null, "National Sales Director", 1, null },
                    { new Guid("5ad5013f-9dcc-4530-8f0a-95b55697ac55"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4259), null, "Customer Service Representative", 1, null },
                    { new Guid("5eacc33a-0bfd-46c5-abf2-b02e56e7bfc7"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4258), null, "Customer Success Manager", 1, null },
                    { new Guid("5f3dec08-752e-4576-9c59-90f1eaa860c5"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4242), null, "Marketing Coordinator", 1, null },
                    { new Guid("62ef2d0d-7b47-4aae-b87e-fdad4de51e73"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4264), null, "Director of Business Operations", 1, null },
                    { new Guid("6595a2e1-f955-4fce-9240-28323cfebd2c"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4346), null, "Principal", 1, null },
                    { new Guid("69ceab8b-1a1d-47ee-9583-9db248e94839"), new Guid("d7427d72-19da-4e32-9cfd-9c1a5f1dbbb9"), new DateTime(2023, 5, 24, 23, 21, 48, 5, DateTimeKind.Local).AddTicks(2954), null, "Pharmacy Technician", 1, null },
                    { new Guid("6a18d755-ddd4-45e1-85f1-464b2d968c3e"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4345), null, "Administrator", 1, null },
                    { new Guid("6c6c585d-5d25-411e-bfee-6367e0d618ca"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4229), null, "Sales Associate", 1, null },
                    { new Guid("6c92fdf2-de54-4f81-9650-46a45e1b3fd3"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4254), null, "VP of Client Services", 1, null },
                    { new Guid("70ba4b0c-1687-41f7-bbd1-12c6e41d4eb5"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4335), null, "Pharmacy Technician", 1, null },
                    { new Guid("7815e4c2-0d47-4c34-9276-5475f8877019"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4369), null, "Hotel Receptionist", 1, null },
                    { new Guid("7ad9249b-80fc-4b9d-b184-d7ab8af238f7"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4329), null, "Other Industries:", 1, null },
                    { new Guid("86513bf4-38dc-4d86-8ed1-b9eb1efb229b"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4232), null, "CMO (Chief Marketing Officer)", 1, null },
                    { new Guid("8bf2c68b-6b26-4969-a461-34f9ec4f7d3f"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4378), null, "Project Appraisal Engineer", 1, null },
                    { new Guid("8ecf60a5-d184-4449-a929-360952e7a22a"), new Guid("7b7b68d2-839c-4c78-8529-c091bfe05424"), new DateTime(2023, 5, 24, 23, 21, 48, 2, DateTimeKind.Local).AddTicks(7988), null, "Director of Business Operations", 1, null },
                    { new Guid("91e25da9-eaf2-4470-84ed-49cd190597d1"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4271), null, "HR Analyst", 1, null },
                    { new Guid("98b2d5d0-b1c5-4508-b408-16e516737973"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4371), null, "Construction Foreman", 1, null },
                    { new Guid("a7ffbc0a-ea8b-42d0-a86f-2972c65852d2"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4364), null, "Front Desk Associate", 1, null },
                    { new Guid("b130744b-7434-4efd-bc52-123ed317da22"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4353), null, "School Counselor", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("b626ed62-4a58-4f8d-a01a-053e5e03ab96"), new Guid("e93f4331-4e64-4886-a6b6-ef87c48d1307"), new DateTime(2023, 5, 24, 23, 21, 48, 10, DateTimeKind.Local).AddTicks(3648), null, "Other Industries:", 1, null },
                    { new Guid("b62875d8-7c7e-41d2-95cd-17ad8f08de31"), new Guid("cc8ec419-e188-4664-8cce-24d9d7db0d7f"), new DateTime(2023, 5, 24, 23, 21, 48, 6, DateTimeKind.Local).AddTicks(5127), null, "Teacher", 1, null },
                    { new Guid("b6968cc3-3f12-4714-a942-d745e6776c20"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4244), null, "VP of Finance", 1, null },
                    { new Guid("bbe594aa-15c7-4eea-b8c5-af6c47d0896c"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4234), null, "Marketing Director", 1, null },
                    { new Guid("bf13d68b-0c37-4673-a085-623c39d646e1"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4249), null, "Credit Analyst", 1, null },
                    { new Guid("bf1c64e5-ea25-4dd0-87ed-bcd5d1491427"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4228), null, "Sales Representative", 1, null },
                    { new Guid("c0b0ec2f-ced3-48f9-940b-8a2568965bc3"), new Guid("9b09bc40-5ee7-4de1-9649-cc503037ff06"), new DateTime(2023, 5, 24, 23, 21, 47, 999, DateTimeKind.Local).AddTicks(677), null, "Teacher", 1, null },
                    { new Guid("c79d54b3-4d36-45ea-8f3f-0980978af9c7"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4261), null, "Support Specialist", 1, null },
                    { new Guid("cd06c93a-1c32-42fd-8b32-b88900ea5b4b"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4376), null, "Contract Administrator", 1, null },
                    { new Guid("d093ddb0-783f-4829-b627-0a344b8ce0fb"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4327), null, "Data Analyst", 1, null },
                    { new Guid("d87959e6-7ee3-4ed0-8ce7-ed9876baf75a"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4238), null, "Sr. Marketing Manager", 1, null },
                    { new Guid("d8afd8c1-0e36-4c04-91c8-60dcf324e9d4"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4266), null, "Operations Supervisor", 1, null },
                    { new Guid("d9df31f9-0b11-4928-951d-7b4e32d254b1"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4269), null, "Sr. Manager of HR", 1, null },
                    { new Guid("dbc7466e-ce02-40fc-9c26-e0737c316d4f"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4375), null, "Project Manager", 1, null },
                    { new Guid("dc86951f-620e-4cbf-9217-df86879c4fd7"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4362), null, "Concierge", 1, null },
                    { new Guid("dcb3ba0f-e99a-4552-a3e4-9b854ad3f552"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4246), null, "Procurement Director", 1, null },
                    { new Guid("de0f8db0-8d13-4506-92a7-2b3898293cad"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4218), null, "VP of Sales", 1, null },
                    { new Guid("defdaaa3-bcb7-4531-9dc4-23025c2c1173"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4273), null, "Software Engineer I, II, III", 1, null },
                    { new Guid("e3c95c70-95ca-4d27-b056-f61391531000"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4360), null, "Guest Services Supervisor", 1, null },
                    { new Guid("e4bbf790-6d30-4a82-95ac-b39714f4b812"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4333), null, "Registered Nurse", 1, null },
                    { new Guid("e9cb13b2-887e-4f16-b3a6-7be8932574b1"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4255), null, "Account Manager", 1, null },
                    { new Guid("eeaa3009-9ab8-4d37-aab9-0324fc8ea76d"), new Guid("fca46396-6bbe-4429-bfcc-1392d058a5ec"), new DateTime(2023, 5, 24, 23, 21, 48, 4, DateTimeKind.Local).AddTicks(634), null, "Customer Service Representative", 1, null },
                    { new Guid("ef6e2f03-4511-4823-81dd-1bb1d7f43a52"), new Guid("125a275f-35dc-41da-acde-fc5fef7d6914"), new DateTime(2023, 5, 24, 23, 21, 47, 997, DateTimeKind.Local).AddTicks(8145), null, "Sr. Marketing Manager", 1, null },
                    { new Guid("f205f53d-b05d-46aa-9a52-0705c3a35c3a"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4359), null, "General Manager", 1, null },
                    { new Guid("f4a1fc3b-9d09-4491-ad97-e2c71e285ebe"), new Guid("25d2a0de-be8c-4850-8bac-2f155de0d098"), new DateTime(2023, 5, 24, 23, 21, 48, 15, DateTimeKind.Local).AddTicks(4241), null, "Customer Service Representative", 1, null },
                    { new Guid("f63b5a55-a9de-4b5a-843b-7cd2f0656fbe"), new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4380), null, "Inspector", 1, null },
                    { new Guid("fc9c00cd-7d3c-4cb0-8635-105462da3a0c"), new Guid("fdcec0d7-359d-4866-bb82-3a13dab81940"), new DateTime(2023, 5, 24, 23, 21, 48, 12, DateTimeKind.Local).AddTicks(9225), null, "Full Stack Developer", 1, null },
                    { new Guid("fec2960f-90d1-49bf-9150-53ae8ebec5e4"), new Guid("5a9d227a-03e3-4a17-ae49-dddd1f20452a"), new DateTime(2023, 5, 24, 23, 21, 48, 1, DateTimeKind.Local).AddTicks(5266), null, "Contract Administrator", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "BirthDate", "BloodGroup", "CompanyId", "ConcurrencyStamp", "CreateDate", "DeleteDate", "Email", "EmailConfirmed", "IdentityNumber", "ImagePath", "JobStartDate", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PatronId", "PhoneNumber", "PhoneNumberConfirmed", "Profession", "SecondName", "SecurityStamp", "Status", "Surname", "TitleId", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("1e1dd18b-909d-449a-bd27-885fcb077f2b"), 0, null, new DateTime(2070, 9, 7, 23, 21, 48, 1, DateTimeKind.Local).AddTicks(5271), null, new Guid("5a9d227a-03e3-4a17-ae49-dddd1f20452a"), "85f453c8-33bc-400f-b8e4-3dcbfb53079e", new DateTime(2023, 5, 24, 23, 21, 48, 1, DateTimeKind.Local).AddTicks(5270), null, "ahmet.yildirim@microsoft.com", false, "41048320866", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "AHMET.YILDIRIM@MICROSOFT.COM", "AHMET.YILDIRIM@MICROSOFT.COM", "AQAAAAEAACcQAAAAEDc1+S5gu9Qd8Fsnn3eBVAilM5fYqIarwTmIJpa3IYxmOASG2LJ6g0MvjwVbILJ1NA==", null, "+905771985021", false, "Painter", null, "XOFAV45SD85MLZO59ZAKS3NREF9MCEG7", 3, "Yıldırım", new Guid("fec2960f-90d1-49bf-9150-53ae8ebec5e4"), false, null, "ahmet.yildirim@microsoft.com" },
                    { new Guid("2d572fed-fe6c-45ca-a685-b9d38ded93d9"), 0, null, new DateTime(2047, 8, 2, 23, 21, 47, 994, DateTimeKind.Local).AddTicks(268), null, new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), "22184a1a-e72a-4d4b-9267-ab41f6fbf193", new DateTime(2023, 5, 24, 23, 21, 47, 994, DateTimeKind.Local).AddTicks(266), null, "test3@test.com", false, "27122557786", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "TEST3@TEST.COM", "TEST3@TEST.COM", "AQAAAAEAACcQAAAAEH7ynIk7xRMLAT/Q0f350JeRhCSuek+oX9Kiswn6832kRiKOfipNxlCcl+gBs+BQtQ==", null, "+905912360712", false, "Florist", null, "VSFVD6HK4BQ8LUQ02O0DNJ9G6KJIS3OW", 1, "Yıldız", new Guid("5f3dec08-752e-4576-9c59-90f1eaa860c5"), false, null, "test3@test.com" },
                    { new Guid("38858de8-3533-4ae7-b553-27831ce7c6e1"), 0, null, new DateTime(2073, 12, 20, 23, 21, 48, 5, DateTimeKind.Local).AddTicks(2959), null, new Guid("d7427d72-19da-4e32-9cfd-9c1a5f1dbbb9"), "faca8582-1337-4670-86c4-a25c558f7ff4", new DateTime(2023, 5, 24, 23, 21, 48, 5, DateTimeKind.Local).AddTicks(2958), null, "yusuf.ozdemir@google.com", false, "34265323772", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Yusuf", "YUSUF.OZDEMIR@GOOGLE.COM", "YUSUF.OZDEMIR@GOOGLE.COM", "AQAAAAEAACcQAAAAEP29j2wcf9zdQZmltDIwQSeSLtqRWBO0MZfrb9AHmH2wjsVBy6v94mXQ45LA7n8bGw==", null, "+905220804346", false, "Businessman", null, "M4JK1PVL8XRYPWC9XHQHDF9N0P36SY13", 3, "Özdemir", new Guid("69ceab8b-1a1d-47ee-9583-9db248e94839"), false, null, "yusuf.ozdemir@google.com" },
                    { new Guid("4249fef1-c5c9-492b-b2e5-5f94a9e1e26b"), 0, null, new DateTime(2050, 2, 13, 23, 21, 48, 11, DateTimeKind.Local).AddTicks(6629), null, new Guid("89703451-fd75-4b63-aa70-d62b273c9f9e"), "c4588239-0ff7-422c-86d1-be343809b0dc", new DateTime(2023, 5, 24, 23, 21, 48, 11, DateTimeKind.Local).AddTicks(6628), null, "ismail.aydin@outlook.com", false, "25667751324", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.AYDIN@OUTLOOK.COM", "ISMAIL.AYDIN@OUTLOOK.COM", "AQAAAAEAACcQAAAAEHa6zMBo1PtjvfatOM/rEom9Dap5gOzszdsUdJaykTYHQM7ctg/3+EStKISzXLTQ/g==", null, "+905681841896", false, "Soldier", null, "EQ4BS9402CI0WUZ2SKH9JGDDY4ZFQO8H", 3, "Aydın", new Guid("0ea92fc7-6e3b-4bf6-be5d-f30209a4bab7"), false, null, "ismail.aydin@outlook.com" },
                    { new Guid("4333f255-1303-403e-addb-fe24d5deac00"), 0, null, new DateTime(2067, 11, 26, 23, 21, 47, 995, DateTimeKind.Local).AddTicks(2837), null, new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), "0bb7a7de-9021-4789-a2c1-3bbb56cb571e", new DateTime(2023, 5, 24, 23, 21, 47, 995, DateTimeKind.Local).AddTicks(2836), null, "test4@test.com", false, "76607846150", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "TEST4@TEST.COM", "TEST4@TEST.COM", "AQAAAAEAACcQAAAAEKyzNdvzJBiOEjaX4hGk1FT1g8QgDJdnqOa8WeKKJ2rSiHCaWC3F7lxAPaf6Rc1ZXw==", null, "+905808817181", false, "Butcher", null, "HGMDB3HYAUZQL8VYK02BNGVV2D4TDIPX", 1, "Yılmaz", new Guid("132212a7-024d-4d91-ac95-7a35e0305b22"), false, null, "test4@test.com" },
                    { new Guid("54644449-4b2b-4a9f-9ba9-329dca7e2f16"), 0, null, new DateTime(2067, 9, 27, 23, 21, 48, 4, DateTimeKind.Local).AddTicks(639), null, new Guid("fca46396-6bbe-4429-bfcc-1392d058a5ec"), "c3648155-f032-4188-95f5-34075b15b2d4", new DateTime(2023, 5, 24, 23, 21, 48, 4, DateTimeKind.Local).AddTicks(638), null, "yusuf.ozturk@outlook.com", false, "56576675210", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Yusuf", "YUSUF.OZTURK@OUTLOOK.COM", "YUSUF.OZTURK@OUTLOOK.COM", "AQAAAAEAACcQAAAAEAFuJ3HXLv3wAI0uE8xxTowrxyOzfxAaycxM610TO9Jn/xo/VDfQKTBIw+eRxcobTw==", null, "+905288977819", false, "Nurse", null, "GZEOBOPGIUP362EVIM1OWZ1T5NY3G30F", 3, "Öztürk", new Guid("eeaa3009-9ab8-4d37-aab9-0324fc8ea76d"), false, null, "yusuf.ozturk@outlook.com" },
                    { new Guid("55fe4f08-c32e-447f-bd33-8c55d26745ed"), 0, null, new DateTime(2074, 3, 20, 23, 21, 47, 997, DateTimeKind.Local).AddTicks(8155), null, new Guid("125a275f-35dc-41da-acde-fc5fef7d6914"), "a1f64bfc-a180-4bdb-8aa9-1f96c27b3e88", new DateTime(2023, 5, 24, 23, 21, 47, 997, DateTimeKind.Local).AddTicks(8153), null, "osman.ozdemir@yahoo.com", false, "18870855170", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "OSMAN.OZDEMIR@YAHOO.COM", "OSMAN.OZDEMIR@YAHOO.COM", "AQAAAAEAACcQAAAAEPzI1XbFuyaWM2qAJrZdOIYFskKO4vYAeLlo982Ycf9ySunilg7qXya8oBA3RybA1g==", null, "+905876630683", false, "Mechanic", null, "A6HC5XGVZ0GVMG3QQS157UTPCUZJKI8U", 3, "Özdemir", new Guid("ef6e2f03-4511-4823-81dd-1bb1d7f43a52"), false, null, "osman.ozdemir@yahoo.com" },
                    { new Guid("6901b157-66e3-4bac-8f21-b26df69271b9"), 0, null, new DateTime(2057, 12, 10, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4413), null, new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), "5c98a753-c5d0-4111-9b02-745daec9ba5c", new DateTime(2023, 5, 24, 23, 21, 47, 991, DateTimeKind.Local).AddTicks(4406), null, "test1@test.com", false, "41147520600", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "TEST1@TEST.COM", "TEST1@TEST.COM", "AQAAAAEAACcQAAAAEIPAspTxy1kEHZswlpkCoNLgAVergHIuHGjzOXMxQuzO56ugvGOqtUSg79PKPTEsPQ==", null, "+905761518029", false, "Designer", null, "E2IFZ4G67XD3CTIHNJERELZ4TJ5T5FYN", 1, "Yıldırım", new Guid("0145678b-29da-4271-81bf-6eb19d8794fd"), false, null, "test1@test.com" },
                    { new Guid("6dc2134b-7159-432a-bfef-ae263da2dd96"), 0, null, new DateTime(2065, 3, 8, 23, 21, 48, 10, DateTimeKind.Local).AddTicks(3653), null, new Guid("e93f4331-4e64-4886-a6b6-ef87c48d1307"), "53dc020c-26eb-4b7d-9057-434dc9d6ffb0", new DateTime(2023, 5, 24, 23, 21, 48, 10, DateTimeKind.Local).AddTicks(3652), null, "hasan.yildirim@yandex.com", false, "83870343286", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hasan", "HASAN.YILDIRIM@YANDEX.COM", "HASAN.YILDIRIM@YANDEX.COM", "AQAAAAEAACcQAAAAEFA+OW6EN+VHE+sD3Xd102B6nQ654siUceah9UBJmxwgNqRY9bXUxqkyeZLlTnE3nQ==", null, "+905695017148", false, "Hairdresser", null, "T2EU69QEUITNELH7DQPWV1RXZYO1JCAZ", 3, "Yıldırım", new Guid("b626ed62-4a58-4f8d-a01a-053e5e03ab96"), false, null, "hasan.yildirim@yandex.com" },
                    { new Guid("77df8fe2-8845-4057-b131-755b44bafaec"), 0, null, new DateTime(2076, 1, 29, 23, 21, 47, 999, DateTimeKind.Local).AddTicks(684), null, new Guid("9b09bc40-5ee7-4de1-9649-cc503037ff06"), "85768bbe-b0b8-4346-8756-e477309ec969", new DateTime(2023, 5, 24, 23, 21, 47, 999, DateTimeKind.Local).AddTicks(683), null, "ismail.kaya@yandex.com", false, "52123552140", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.KAYA@YANDEX.COM", "ISMAIL.KAYA@YANDEX.COM", "AQAAAAEAACcQAAAAEBE30xn4Z4Yl0fNGleb4efv67kJwTDCHD/LIQVGX9fmxDthJKZvpBZ0Y2h6JtHNksw==", null, "+905799989391", false, "Shop assistant", null, "MNA6W7O5Y9RCL2CV8NLUCNS3HX4GWAK2", 3, "Kaya", new Guid("c0b0ec2f-ced3-48f9-940b-8a2568965bc3"), false, null, "ismail.kaya@yandex.com" },
                    { new Guid("845c1ed1-d421-4609-80fc-040562a3cad0"), 0, null, new DateTime(2047, 7, 30, 23, 21, 48, 15, DateTimeKind.Local).AddTicks(4246), null, new Guid("25d2a0de-be8c-4850-8bac-2f155de0d098"), "b1a56e65-2507-4bf5-9ca6-138e978719ad", new DateTime(2023, 5, 24, 23, 21, 48, 15, DateTimeKind.Local).AddTicks(4245), null, "huseyin.celik@yahoo.com", false, "41312125470", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "HUSEYIN.CELIK@YAHOO.COM", "HUSEYIN.CELIK@YAHOO.COM", "AQAAAAEAACcQAAAAELGWj3UsFytkDy8cZwD0+oz9EyJmHIIMoj9kMX/nEhp877stJnAVTekLOC7TuSZAaw==", null, "+905157567425", false, "Veterinary doctor(Vet)", null, "MXMUH915B800GBH85NEIKZICEK4LRGI3", 3, "Çelik", new Guid("f4a1fc3b-9d09-4491-ad97-e2c71e285ebe"), false, null, "huseyin.celik@yahoo.com" },
                    { new Guid("88091bf1-84d4-40bd-ae63-b378a887ad39"), 0, null, new DateTime(2043, 12, 1, 23, 21, 48, 9, DateTimeKind.Local).AddTicks(913), null, new Guid("4f83adf7-8fa9-4c1e-b6c7-5718290f523a"), "9ab06440-dda5-4e03-a727-b94bd873a41c", new DateTime(2023, 5, 24, 23, 21, 48, 9, DateTimeKind.Local).AddTicks(912), null, "mehmet.celik@outlook.com", false, "62740340556", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mehmet", "MEHMET.CELIK@OUTLOOK.COM", "MEHMET.CELIK@OUTLOOK.COM", "AQAAAAEAACcQAAAAEL52tT4z6ChKq4kQDPRU750wTb6sxOJnP18VRHPxNMkze61LrwzB5NIPddNIDjxZiA==", null, "+905572713841", false, "Secretary", null, "I3H18GR8GKWU99TBW2JD1JI4RRESJ9KU", 3, "Çelik", new Guid("2e5d6ab4-0498-4af4-93bc-64212e0e880d"), false, null, "mehmet.celik@outlook.com" },
                    { new Guid("8fa63403-d72d-434d-b906-f73d31399e11"), 0, null, new DateTime(2059, 9, 14, 23, 21, 48, 0, DateTimeKind.Local).AddTicks(2978), null, new Guid("9e0615ad-e0b4-46e7-a9fa-07bb6cc8044b"), "e3f5850f-97d7-4c3b-8c93-4d178d8c2091", new DateTime(2023, 5, 24, 23, 21, 48, 0, DateTimeKind.Local).AddTicks(2978), null, "yusuf.sahin@outlook.com", false, "72713758066", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Yusuf", "YUSUF.SAHIN@OUTLOOK.COM", "YUSUF.SAHIN@OUTLOOK.COM", "AQAAAAEAACcQAAAAEEAA6RM6qNSEuNBiPCNI21yd87N1WHo8gOEhr3onfgrmRVaSdxzpQlkF466ayayyRw==", null, "+905160493261", false, "Travel agent", null, "YZ0HC4HYVSSHPV7ULSEUJYMNZJ048IZW", 3, "Şahin", new Guid("561b6703-032c-4898-83a0-652662422653"), false, null, "yusuf.sahin@outlook.com" },
                    { new Guid("a96a9599-3a63-49bb-a414-672875b515ea"), 0, null, new DateTime(2068, 3, 9, 23, 21, 47, 992, DateTimeKind.Local).AddTicks(7626), null, new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), "bf1cc70c-5549-4bbc-a387-5f8994a70f61", new DateTime(2023, 5, 24, 23, 21, 47, 992, DateTimeKind.Local).AddTicks(7624), null, "test2@test.com", false, "55640861016", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "TEST2@TEST.COM", "TEST2@TEST.COM", "AQAAAAEAACcQAAAAENajc9z1DM3DWef1iMupPgsQnPd2WxQupQB5O2aePDTEDW6csLlack22vfNMHYss+g==", null, "+905249444189", false, "Pharmacist", null, "QJE28MG6VOTGS7E1NYCI3CA67O3UBF96", 1, "Yıldız", new Guid("2445dcaa-7de1-40e8-9666-a65d9b8f55e2"), false, null, "test2@test.com" },
                    { new Guid("b43ea1c7-6d3c-4b6b-8d64-8348a4490931"), 0, null, new DateTime(2070, 3, 27, 23, 21, 48, 14, DateTimeKind.Local).AddTicks(1686), null, new Guid("bfa1f2eb-867a-4068-9129-c2622111a84f"), "039cae4d-9594-4567-b39a-c873cdf31be9", new DateTime(2023, 5, 24, 23, 21, 48, 14, DateTimeKind.Local).AddTicks(1685), null, "ahmet.sahin@hotmail.com", false, "20074010194", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "AHMET.SAHIN@HOTMAIL.COM", "AHMET.SAHIN@HOTMAIL.COM", "AQAAAAEAACcQAAAAEKAYshQRKn1qhzxILuFxn4uSt86TQCjnLsQw4eZgawBFGk3ApftkfTCxuAmbVjdlgA==", null, "+905518807272", false, "Butcher", null, "ZUNCOGQLRPIVPEE9XHP3O1492UWCAVBH", 3, "Şahin", new Guid("27278d5b-9870-4e4f-8c11-c578b175bc17"), false, null, "ahmet.sahin@hotmail.com" },
                    { new Guid("b59ba530-8e49-4580-8384-9f6e1b69d29c"), 0, null, new DateTime(2050, 12, 19, 23, 21, 48, 6, DateTimeKind.Local).AddTicks(5132), null, new Guid("cc8ec419-e188-4664-8cce-24d9d7db0d7f"), "edb59ebd-ac3e-4331-98f6-a9af5b3b7af6", new DateTime(2023, 5, 24, 23, 21, 48, 6, DateTimeKind.Local).AddTicks(5131), null, "mehmet.sahin@yandex.com", false, "18040260360", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mehmet", "MEHMET.SAHIN@YANDEX.COM", "MEHMET.SAHIN@YANDEX.COM", "AQAAAAEAACcQAAAAEMPmVDS5XZ9nuziyO9oeTBC8r7EGnDfZqW+IB7cgtRoI1mVBlqjr0OdZxyHuAAy6Ig==", null, "+905620090312", false, "Designer", null, "DMW66CDOACXS62TWI24MGU8HYIA5I402", 3, "Şahin", new Guid("b62875d8-7c7e-41d2-95cd-17ad8f08de31"), false, null, "mehmet.sahin@yandex.com" },
                    { new Guid("c34f1eba-dcf0-4133-a093-49190afc4620"), 0, null, new DateTime(2056, 7, 23, 23, 21, 48, 12, DateTimeKind.Local).AddTicks(9234), null, new Guid("fdcec0d7-359d-4866-bb82-3a13dab81940"), "6b895845-7e9a-4004-a144-093417460ccb", new DateTime(2023, 5, 24, 23, 21, 48, 12, DateTimeKind.Local).AddTicks(9233), null, "osman.yildirim@microsoft.com", false, "78641843652", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "OSMAN.YILDIRIM@MICROSOFT.COM", "OSMAN.YILDIRIM@MICROSOFT.COM", "AQAAAAEAACcQAAAAEH2z8dc82BGs2yKRbBSiiSlla39ZgwRadWUSo8TRfF9RFC4Wgq6GVDmO+FQNXBfGAw==", null, "+905791436341", false, "Newsreader", null, "ZT8FCK63JGEBCPNEL0SKULEH0Y2AC1IV", 3, "Yıldırım", new Guid("fc9c00cd-7d3c-4cb0-8635-105462da3a0c"), false, null, "osman.yildirim@microsoft.com" },
                    { new Guid("d65c9449-777f-41f6-8557-1360bb841640"), 0, null, new DateTime(2062, 10, 3, 23, 21, 48, 2, DateTimeKind.Local).AddTicks(7996), null, new Guid("7b7b68d2-839c-4c78-8529-c091bfe05424"), "90f07dda-c336-4377-9a10-945f50cf78b6", new DateTime(2023, 5, 24, 23, 21, 48, 2, DateTimeKind.Local).AddTicks(7995), null, "hasan.aydin@microsoft.com", false, "15768147188", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hasan", "HASAN.AYDIN@MICROSOFT.COM", "HASAN.AYDIN@MICROSOFT.COM", "AQAAAAEAACcQAAAAEA0Rek8PY/1AbaX8E7a5henAS/sn0tq4mjCw3twfoWLlX9+uAFPRrgKrz56RTIn5/Q==", null, "+905743521368", false, "Travel agent", null, "GOK2RD8QUXEPPX52EAJF4JC76JLUG2EF", 3, "Aydın", new Guid("8ecf60a5-d184-4449-a929-360952e7a22a"), false, null, "hasan.aydin@microsoft.com" },
                    { new Guid("df8ab2ae-e30d-4911-8dc0-1ee188187264"), 0, null, new DateTime(2073, 11, 17, 23, 21, 47, 996, DateTimeKind.Local).AddTicks(5604), null, new Guid("cd3927a7-93d4-4068-bebb-c258090f4a2f"), "c1ddbc37-a018-4e7a-939c-d286fa9418b7", new DateTime(2023, 5, 24, 23, 21, 47, 996, DateTimeKind.Local).AddTicks(5603), null, "test5@test.com", false, "40266674218", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mehmet", "TEST5@TEST.COM", "TEST5@TEST.COM", "AQAAAAEAACcQAAAAEJS8XIZNMUCqpzTPPSt0sYw4Vz8qSUpBC9sJ/3I/iraCl8eOmJ9Zk3d5SnAZ2OiMuA==", null, "+905218608533", false, "Engineer", null, "95EANOFSVKGC9UXCTAF48RG9EQGZ9G6C", 1, "Özdemir", new Guid("de0f8db0-8d13-4506-92a7-2b3898293cad"), false, null, "test5@test.com" },
                    { new Guid("df988a2b-8cad-45ac-89c4-32eae542d174"), 0, null, new DateTime(2058, 7, 8, 23, 21, 48, 7, DateTimeKind.Local).AddTicks(8152), null, new Guid("79ef3352-91c5-4840-88ae-b5076f3db6cf"), "3a53e3b0-c843-4575-936e-9f5db602a68a", new DateTime(2023, 5, 24, 23, 21, 48, 7, DateTimeKind.Local).AddTicks(8150), null, "hasan.yildiz@yandex.com", false, "74281153786", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hasan", "HASAN.YILDIZ@YANDEX.COM", "HASAN.YILDIZ@YANDEX.COM", "AQAAAAEAACcQAAAAEIZMbxNdt09Hj8hrQjjcacTui5twvrXp92E65KE7vDJeANWPKEQm2I+SpQeJSa/Xvw==", null, "+905520830887", false, "Florist", null, "LJC8TM8H7LN78B5I3B6FE0913J3STEY7", 3, "Yıldız", new Guid("05c8018f-53ca-47ed-b612-6c6ac9012d40"), false, null, "hasan.yildiz@yandex.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("1e1dd18b-909d-449a-bd27-885fcb077f2b") },
                    { new Guid("03b82678-87fe-4887-86bb-78d550d93d2e"), new Guid("2d572fed-fe6c-45ca-a685-b9d38ded93d9") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("38858de8-3533-4ae7-b553-27831ce7c6e1") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("4249fef1-c5c9-492b-b2e5-5f94a9e1e26b") },
                    { new Guid("03b82678-87fe-4887-86bb-78d550d93d2e"), new Guid("4333f255-1303-403e-addb-fe24d5deac00") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("54644449-4b2b-4a9f-9ba9-329dca7e2f16") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("55fe4f08-c32e-447f-bd33-8c55d26745ed") },
                    { new Guid("03b82678-87fe-4887-86bb-78d550d93d2e"), new Guid("6901b157-66e3-4bac-8f21-b26df69271b9") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("6dc2134b-7159-432a-bfef-ae263da2dd96") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("77df8fe2-8845-4057-b131-755b44bafaec") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("845c1ed1-d421-4609-80fc-040562a3cad0") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("88091bf1-84d4-40bd-ae63-b378a887ad39") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("8fa63403-d72d-434d-b906-f73d31399e11") },
                    { new Guid("03b82678-87fe-4887-86bb-78d550d93d2e"), new Guid("a96a9599-3a63-49bb-a414-672875b515ea") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("b43ea1c7-6d3c-4b6b-8d64-8348a4490931") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("b59ba530-8e49-4580-8384-9f6e1b69d29c") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("c34f1eba-dcf0-4133-a093-49190afc4620") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("d65c9449-777f-41f6-8557-1360bb841640") },
                    { new Guid("03b82678-87fe-4887-86bb-78d550d93d2e"), new Guid("df8ab2ae-e30d-4911-8dc0-1ee188187264") },
                    { new Guid("9b3c8ef8-5124-4d9b-a02b-4820acb0857b"), new Guid("df988a2b-8cad-45ac-89c4-32eae542d174") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PatronId",
                table: "AspNetUsers",
                column: "PatronId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TitleId",
                table: "AspNetUsers",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CashAdvances_AdvanceToId",
                table: "CashAdvances",
                column: "AdvanceToId");

            migrationBuilder.CreateIndex(
                name: "IX_CashAdvances_DirectorId",
                table: "CashAdvances",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SectorId",
                table: "Companies",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ApprovedById",
                table: "Expenses",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseById",
                table: "Expenses",
                column: "ExpenseById");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_ApprovedById",
                table: "Leaves",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_AppUserId",
                table: "Leaves",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Titles_CompanyId",
                table: "Titles",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CashAdvances");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Leaves");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Titles");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Sectors");
        }
    }
}
