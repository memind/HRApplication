using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKApplication.Persistance.Migrations
{
    public partial class currencyAndProfession : Migration
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
                name: "Professions",
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
                    table.PrimaryKey("PK_Professions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
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
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentityNumber = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    JobStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatronId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_AspNetUsers_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
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
                    Currency = table.Column<int>(type: "int", nullable: false),
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
                    Type = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false)
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
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), "DHYD102FJKSVGV7U6Q6MN56GDJ3E09HY", "Company Administrator", "COMPANY ADMINISTRATOR" },
                    { new Guid("719553c4-19fa-47b0-bf35-a29604b475cc"), "DJ2AHRFNTUIPANG9A8LQ1PJ3MQJTBHR7", "Personal", "PERSONAL" },
                    { new Guid("783be846-6d5a-4b95-963a-bdd977ab24ac"), "3341HXXKV3QS3KVVIM9F3KL3Z1LK4YTM", "Site Administrator", "SITE ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("05179807-32ec-4605-8fe1-133a29be87b6"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(225), null, "Finans", 1, null },
                    { new Guid("0875d4fd-dd9f-4d9f-8dff-895d1141ac8c"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(243), null, "Ticaret (Satış ve Pazarlama)", 1, null },
                    { new Guid("0dbf7859-3826-4841-b403-905d24c4fa41"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(247), null, "Ulaştırma, Lojistik ve Haberleşme", 1, null },
                    { new Guid("11f937dd-f85a-477c-8e9f-8f68fa67c1ec"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(246), null, "Turizm, Konaklama, Yiyecek-İçecek Hizmetleri", 1, null },
                    { new Guid("224880b4-56ad-4670-a72c-ebbf88242708"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(229), null, "İş ve Yönetimi", 1, null },
                    { new Guid("2895a566-9b28-4ad8-82c5-cfbe1ad24d04"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(244), null, "Toplumsal ve Kişisel Hizmetler", 1, null },
                    { new Guid("34e967bb-240a-4f25-b501-3afbdc823c5a"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(240), null, "Spor ve Rekreasyon", 1, null },
                    { new Guid("3ec797d4-8fc8-4057-aa42-d8c2163c6ccd"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(241), null, "Tarım, Avcılık ve Balıkçılık", 1, null },
                    { new Guid("433c932c-26c9-4483-a901-46cb7717356c"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(190), null, "Bilişim", 1, null },
                    { new Guid("5d77ab61-8cf6-4068-b493-13769ed912cb"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(227), null, "İnşaat", 1, null },
                    { new Guid("7dc590fb-0fe8-4e77-91c8-c92feada899e"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(236), null, "Metal", 1, null },
                    { new Guid("862c2289-e355-4f49-b301-e2d26ab66b5a"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(192), null, "Eğitim", 1, null },
                    { new Guid("865993b9-2ec0-47bd-8821-407d68f736e3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(232), null, "Maden", 1, null },
                    { new Guid("8c2b5483-1a24-4413-871a-8a2e395623a9"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(231), null, "Kültür, Sanat ve Tasarım", 1, null },
                    { new Guid("966b691a-31a1-4480-ab06-1d4683daebed"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(222), null, "Enerji", 1, null },
                    { new Guid("ab522e9c-668e-4b8a-94d5-96c6d399c746"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(242), null, "Tekstil, Hazır Giyim, Deri", 1, null },
                    { new Guid("ac54939d-6f61-488c-8d88-fcdfeb1559dd"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(176), null, "Ahşap Teknolojisi", 1, null },
                    { new Guid("b456118c-ebaa-4fcd-9b50-c4a5f048a752"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(238), null, "Sağlık ve Sosyal Hizmetler", 1, null },
                    { new Guid("cb44e437-f28c-43a3-af55-5e0d9e7e19c4"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(191), null, "Cam, Çimento ve ToprakÇevre", 1, null },
                    { new Guid("cce3025e-7d82-4f61-b3c4-01b4921cef72"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(230), null, "Kimya, Petrol, Lastik ve PLastik", 1, null },
                    { new Guid("cd20a820-42c8-4281-bfd2-36926c62f93d"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(193), null, "Elektrik ve Elektronik", 1, null },
                    { new Guid("cd73e2d5-856a-49a1-ab95-db055727640f"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(233), null, "Medya, İletişim ve Yayıncılık", 1, null },
                    { new Guid("d4e9179d-5cd2-4b70-afea-2f32bf348c68"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(237), null, "Otomotiv", 1, null },
                    { new Guid("ead9161f-649e-4f4b-ad91-0ee5477c2093"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(226), null, "Gıda", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "NumberOfEmployees", "PhoneNumber", "SectorId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("0301f8a6-9827-4fe9-8e87-4eed41170833"), new DateTime(2023, 5, 29, 21, 2, 3, 112, DateTimeKind.Local).AddTicks(5528), null, "info@yildizkomanditsirketi.com", "Yıldız Komandit Şirketi", 38, "+905908544585", new Guid("0875d4fd-dd9f-4d9f-8dff-895d1141ac8c"), 3, null },
                    { new Guid("0986b5ae-b2d2-4271-977f-f78633d9ccc1"), new DateTime(2023, 5, 29, 21, 2, 3, 123, DateTimeKind.Local).AddTicks(9466), null, "info@demiranonimsirketi.com", "Demir Anonim Şirketi", 36, "+905120208551", new Guid("cd20a820-42c8-4281-bfd2-36926c62f93d"), 3, null },
                    { new Guid("25c98067-29d2-4df6-9af9-19817b07e803"), new DateTime(2023, 5, 29, 21, 2, 3, 120, DateTimeKind.Local).AddTicks(2125), null, "info@ozturkkomanditsirketi.com", "Öztürk Komandit Şirketi", 4, "+905956890149", new Guid("8c2b5483-1a24-4413-871a-8a2e395623a9"), 3, null },
                    { new Guid("2e436b3d-3a62-4221-9548-9163631aa310"), new DateTime(2023, 5, 29, 21, 2, 3, 118, DateTimeKind.Local).AddTicks(9742), null, "info@yildirimlimitedsirketi.com", "Yıldırım Limited Şirketi", 14, "+905196450752", new Guid("8c2b5483-1a24-4413-871a-8a2e395623a9"), 3, null },
                    { new Guid("40a011d8-7b9e-4b40-8a56-ce1af7d65568"), new DateTime(2023, 5, 29, 21, 2, 3, 122, DateTimeKind.Local).AddTicks(7149), null, "info@aydinlimitedsirketi.com", "Aydın Limited Şirketi", 6, "+905903759899", new Guid("cd20a820-42c8-4281-bfd2-36926c62f93d"), 3, null },
                    { new Guid("44750478-0513-4836-95c1-8b537427d58d"), new DateTime(2023, 5, 29, 21, 2, 3, 117, DateTimeKind.Local).AddTicks(7356), null, "info@aydinkooperatifsirketi.com", "Aydın Kooperatif Şirketi", 15, "+905591219007", new Guid("ead9161f-649e-4f4b-ad91-0ee5477c2093"), 3, null },
                    { new Guid("56c07785-dcc5-4448-b5aa-af011bf7711e"), new DateTime(2023, 5, 29, 21, 2, 3, 115, DateTimeKind.Local).AddTicks(1816), null, "info@yildizkollektifsirketi.com", "Yıldız Kollektif Şirketi", 61, "+905739302688", new Guid("5d77ab61-8cf6-4068-b493-13769ed912cb"), 3, null },
                    { new Guid("85544b4c-3077-4c1d-8c8d-129b7a7b5ec5"), new DateTime(2023, 5, 29, 21, 2, 3, 116, DateTimeKind.Local).AddTicks(4832), null, "info@yildirimanonimsirketi.com", "Yıldırım Anonim Şirketi", 74, "+905803178295", new Guid("0dbf7859-3826-4841-b403-905d24c4fa41"), 3, null },
                    { new Guid("a4dbf9f7-2382-4d5f-9ada-bb6af5284a29"), new DateTime(2023, 5, 29, 21, 2, 3, 110, DateTimeKind.Local).AddTicks(947), null, "info@yildizlimitedsirketi.com", "Yıldız Limited Şirketi", 70, "+905311577213", new Guid("862c2289-e355-4f49-b301-e2d26ab66b5a"), 3, null },
                    { new Guid("ac17ab07-bef7-4a3c-9acf-5d46f4203e86"), new DateTime(2023, 5, 29, 21, 2, 3, 113, DateTimeKind.Local).AddTicks(8735), null, "info@yildizanonimsirketi.com", "Yıldız Anonim Şirketi", 69, "+905647800403", new Guid("3ec797d4-8fc8-4057-aa42-d8c2163c6ccd"), 3, null },
                    { new Guid("b3e54d51-7039-4211-9238-a80bd94b7ed5"), new DateTime(2023, 5, 29, 21, 2, 3, 106, DateTimeKind.Local).AddTicks(3973), null, "info@yildizlimitedsirketi.com", "Yıldız Limited Şirketi", 50, "+905172622486", new Guid("ab522e9c-668e-4b8a-94d5-96c6d399c746"), 3, null },
                    { new Guid("b57cb575-9690-4b1f-9be0-d486bf89e2f0"), new DateTime(2023, 5, 29, 21, 2, 3, 111, DateTimeKind.Local).AddTicks(3255), null, "info@sahinanonimsirketi.com", "Şahin Anonim Şirketi", 84, "+905134689735", new Guid("966b691a-31a1-4480-ab06-1d4683daebed"), 3, null },
                    { new Guid("bb952bd3-1858-4901-b151-62793d718f76"), new DateTime(2023, 5, 29, 21, 2, 3, 121, DateTimeKind.Local).AddTicks(4794), null, "info@yildirimkollektifsirketi.com", "Yıldırım Kollektif Şirketi", 54, "+905165574294", new Guid("ead9161f-649e-4f4b-ad91-0ee5477c2093"), 3, null },
                    { new Guid("d9800162-e8a7-48af-8033-d4efe3875b97"), new DateTime(2023, 5, 29, 21, 2, 3, 107, DateTimeKind.Local).AddTicks(6276), null, "info@yildirimlimitedsirketi.com", "Yıldırım Limited Şirketi", 55, "+905552422911", new Guid("8c2b5483-1a24-4413-871a-8a2e395623a9"), 3, null },
                    { new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(346), null, "ikapp@ikapp.com", "IKApp A.Ş.", 5, "+905522109252", new Guid("433c932c-26c9-4483-a901-46cb7717356c"), 1, null },
                    { new Guid("e9efaf95-c9d3-4991-abb7-b08e02818d95"), new DateTime(2023, 5, 29, 21, 2, 3, 108, DateTimeKind.Local).AddTicks(8706), null, "info@celikkollektifsirketi.com", "Çelik Kollektif Şirketi", 5, "+905777750056", new Guid("cb44e437-f28c-43a3-af55-5e0d9e7e19c4"), 3, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("085a3ce3-3e27-4047-9fa8-a0715c4ee865"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(369), null, "Sales Representative", 1, null },
                    { new Guid("0a688378-ef8b-493a-97e8-0ca243378b05"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(401), null, "Full Stack Developer", 1, null },
                    { new Guid("0d6b370f-3a5a-4e49-bc50-09a05aaf08f1"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(432), null, "Pharmacy Technician", 1, null },
                    { new Guid("13b1b52f-21de-428a-8c8b-49c670421efe"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(368), null, "Regional Sales Manager", 1, null },
                    { new Guid("13baa901-4424-478e-98e3-12460187676a"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(453), null, "Safety Director", 1, null },
                    { new Guid("151e1236-86eb-420c-9ebe-a3e5c35ed4a5"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(448), null, "Front Desk Associate", 1, null },
                    { new Guid("1bb92b9f-37af-4ac8-af48-301557a16924"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(383), null, "Credit Analyst", 1, null },
                    { new Guid("1d63aacb-48aa-4635-889a-ce8a17f82caa"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(436), null, "Clinical Laboratory Technician", 1, null },
                    { new Guid("1e8c36af-6828-42a9-8f95-8aed3412985b"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(372), null, "CMO (Chief Marketing Officer)", 1, null },
                    { new Guid("23cfb3ef-8d4f-46d4-b8e9-4f968ef0b3da"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(398), null, "HR Analyst", 1, null },
                    { new Guid("287eebcd-636c-4aa5-ac5f-754c6a2d913b"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(455), null, "Project Appraisal Engineer", 1, null },
                    { new Guid("294e4a64-a769-4790-ad96-176987a2891f"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(390), null, "Customer Success Manager", 1, null },
                    { new Guid("30ab3241-bd68-42f5-9188-53460c38df59"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(380), null, "VP of Finance", 1, null },
                    { new Guid("320f52ff-7c7b-4aee-a8f5-4c6201d9d8e5"), new Guid("25c98067-29d2-4df6-9af9-19817b07e803"), new DateTime(2023, 5, 29, 21, 2, 3, 120, DateTimeKind.Local).AddTicks(2127), null, "Marketing Director", 1, null },
                    { new Guid("333d7054-6729-42e6-a921-5f4147e49615"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(391), null, "Customer Service Representative", 1, null },
                    { new Guid("35d4d68a-cec9-4a40-b866-209927e5bf49"), new Guid("44750478-0513-4836-95c1-8b537427d58d"), new DateTime(2023, 5, 29, 21, 2, 3, 117, DateTimeKind.Local).AddTicks(7358), null, "Teacher", 1, null },
                    { new Guid("38d7f2d5-80f7-44ff-a812-380d3125f9ba"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(444), null, "Teaching Assistant", 1, null },
                    { new Guid("3abd9d54-4e5d-48a8-b5e0-07cf414e3298"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(373), null, "Marketing Director", 1, null },
                    { new Guid("3d464f78-8818-4aac-b236-b1470532b0e6"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(388), null, "Account Manager", 1, null },
                    { new Guid("4984fea8-da23-4f05-bd76-eeaba2e73f14"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(374), null, "Sr. Marketing Manager", 1, null },
                    { new Guid("499789d1-9dde-49be-97b5-12d4014ca161"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(442), null, "School Counselor", 1, null },
                    { new Guid("4a38a5e7-fa7c-4f35-8983-595ce4f8dad3"), new Guid("d9800162-e8a7-48af-8033-d4efe3875b97"), new DateTime(2023, 5, 29, 21, 2, 3, 107, DateTimeKind.Local).AddTicks(6278), null, "Sales Representative", 1, null },
                    { new Guid("514f3cfa-4c4e-4926-b73c-a31f05d2da3c"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(381), null, "Procurement Director", 1, null },
                    { new Guid("56e58557-f444-48a8-a5ea-d1dbf6305f2d"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(427), null, "Data Analyst", 1, null },
                    { new Guid("61f17925-de30-4269-b2f5-9681192c7738"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(447), null, "Concierge", 1, null },
                    { new Guid("65758767-4be4-49bf-adc2-147d24237bbe"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(394), null, "Director of Business Operations", 1, null },
                    { new Guid("6d169411-a691-4735-b83d-ededbcaf53ad"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(393), null, "COO (Chief Operating Officer)", 1, null },
                    { new Guid("6dcc8973-ae06-47e2-9383-b67925c30482"), new Guid("0301f8a6-9827-4fe9-8e87-4eed41170833"), new DateTime(2023, 5, 29, 21, 2, 3, 112, DateTimeKind.Local).AddTicks(5530), null, "Occupational Therapy Aide", 1, null },
                    { new Guid("73559151-1590-4da7-849a-bcc5ba506bba"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(392), null, "Support Specialist", 1, null },
                    { new Guid("7761dfb8-bdf4-480e-8278-b783a3c6b5ee"), new Guid("a4dbf9f7-2382-4d5f-9ada-bb6af5284a29"), new DateTime(2023, 5, 29, 21, 2, 3, 110, DateTimeKind.Local).AddTicks(951), null, "Contract Administrator", 1, null },
                    { new Guid("791ad158-e7b0-458c-ba9a-c85093eb2345"), new Guid("85544b4c-3077-4c1d-8c8d-129b7a7b5ec5"), new DateTime(2023, 5, 29, 21, 2, 3, 116, DateTimeKind.Local).AddTicks(4835), null, "Physical Therapist", 1, null },
                    { new Guid("79ba5f29-8602-47aa-aab6-e7a212a9e43d"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(439), null, "Registrar", 1, null },
                    { new Guid("7a85791e-6083-4880-bfc5-4bad6725380b"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(435), null, "Nursing Assistant", 1, null },
                    { new Guid("81e1df0e-df9d-487a-b5fc-48f0cae6f017"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(452), null, "Construction Foreman", 1, null },
                    { new Guid("85290556-c190-464c-ad12-4ae6e1ade63a"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(437), null, "Occupational Therapy Aide", 1, null },
                    { new Guid("8b303ba7-5aaf-4478-b079-b1c591ac1b0d"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(370), null, "Sales Associate", 1, null },
                    { new Guid("8f60841a-6fdb-4a09-9d32-bedf24f0d27b"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(437), null, "Administrator", 1, null },
                    { new Guid("903c8dcf-2696-4b36-b9e1-07e38adac7e9"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(454), null, "Project Manager", 1, null },
                    { new Guid("9163456b-b4b9-4a40-a870-37ffec6f404a"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(425), null, "Systems Administrator", 1, null },
                    { new Guid("92772e36-af22-4dbc-b130-dca7f90642b9"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(446), null, "Guest Services Supervisor", 1, null },
                    { new Guid("9458593b-dc0d-463e-abd9-5846a6926073"), new Guid("b3e54d51-7039-4211-9238-a80bd94b7ed5"), new DateTime(2023, 5, 29, 21, 2, 3, 106, DateTimeKind.Local).AddTicks(3977), null, "Data Analyst", 1, null },
                    { new Guid("9b9c64c0-3000-4154-a70f-0dce26522539"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(379), null, "Marketing Coordinator", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("9e050e41-f5ea-4451-a1c8-1bb4ade067ad"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(377), null, "Marketing Analyst", 1, null },
                    { new Guid("a50df42d-9a40-4c1a-b0b9-95e6ae07aa8c"), new Guid("56c07785-dcc5-4448-b5aa-af011bf7711e"), new DateTime(2023, 5, 29, 21, 2, 3, 115, DateTimeKind.Local).AddTicks(1823), null, "Registered Nurse", 1, null },
                    { new Guid("aa618c50-7121-4146-8d38-042a41e6dde0"), new Guid("b57cb575-9690-4b1f-9be0-d486bf89e2f0"), new DateTime(2023, 5, 29, 21, 2, 3, 111, DateTimeKind.Local).AddTicks(3257), null, "Sr. Marketing Manager", 1, null },
                    { new Guid("ab108018-1967-42db-a1c0-97dbd37d255e"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(429), null, "Registered Nurse", 1, null },
                    { new Guid("b21a65f2-a52d-4f16-b348-895bac64bc4c"), new Guid("e9efaf95-c9d3-4991-abb7-b08e02818d95"), new DateTime(2023, 5, 29, 21, 2, 3, 108, DateTimeKind.Local).AddTicks(8709), null, "Project Manager", 1, null },
                    { new Guid("b7b4707e-847f-4dd7-bed8-f8b2cee431e4"), new Guid("bb952bd3-1858-4901-b151-62793d718f76"), new DateTime(2023, 5, 29, 21, 2, 3, 121, DateTimeKind.Local).AddTicks(4797), null, "Marketing Coordinator", 1, null },
                    { new Guid("bdc7043d-bac3-4e0b-ac47-57aa861d9bac"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(382), null, "Investment Analyst", 1, null },
                    { new Guid("bfdbe8f7-8201-451e-a699-d9daf40b2d90"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(384), null, "Risk Analyst", 1, null },
                    { new Guid("c0ac2dc1-3fe8-46e3-ad7e-628c1881be41"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(364), null, "VP of Sales", 1, null },
                    { new Guid("c47df793-a049-4c97-baad-d824260b5a11"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(443), null, "Teacher", 1, null },
                    { new Guid("c62bfe62-7a37-412b-8e72-53e00b9cf330"), new Guid("ac17ab07-bef7-4a3c-9acf-5d46f4203e86"), new DateTime(2023, 5, 29, 21, 2, 3, 113, DateTimeKind.Local).AddTicks(8740), null, "VP of Finance", 1, null },
                    { new Guid("d4e8c337-fd69-427e-afee-5690a35ed77f"), new Guid("0986b5ae-b2d2-4271-977f-f78633d9ccc1"), new DateTime(2023, 5, 29, 21, 2, 3, 123, DateTimeKind.Local).AddTicks(9468), null, "Teacher", 1, null },
                    { new Guid("d705fcf7-2452-4d86-8ae4-6ffae7b0391d"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(367), null, "National Sales Director", 1, null },
                    { new Guid("da55e8ee-4f76-4ca4-a943-7880df55c7c4"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(395), null, "Operations Supervisor", 1, null },
                    { new Guid("e1672b77-2766-487d-bafd-a5f40b7d5f72"), new Guid("2e436b3d-3a62-4221-9548-9163631aa310"), new DateTime(2023, 5, 29, 21, 2, 3, 118, DateTimeKind.Local).AddTicks(9744), null, "Account Manager", 1, null },
                    { new Guid("e505c11a-8fd3-46b2-ab61-560649ea8200"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(434), null, "Physical Therapist", 1, null },
                    { new Guid("e5637d75-6042-42e4-98ab-730aa336ced8"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(400), null, "Software Engineer I, II, III", 1, null },
                    { new Guid("e8c924ce-c325-4713-8ef2-f37dfdaec070"), new Guid("40a011d8-7b9e-4b40-8a56-ce1af7d65568"), new DateTime(2023, 5, 29, 21, 2, 3, 122, DateTimeKind.Local).AddTicks(7151), null, "COO (Chief Operating Officer)", 1, null },
                    { new Guid("ea62747a-f235-4419-a3be-b662b49124bf"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(451), null, "Hotel Receptionist", 1, null },
                    { new Guid("ed1229bc-65e2-4e5e-8182-6dae771e8ebd"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(448), null, "Server/Host/Hostess", 1, null },
                    { new Guid("f11d9c99-fc2d-47fa-ae84-3179d168bc64"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(428), null, "Other Industries:", 1, null },
                    { new Guid("f1d07bb3-1a15-40ae-bfa1-3c014d98d704"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(438), null, "Principal", 1, null },
                    { new Guid("f23262ad-ad3f-431c-9cef-74767a5e1f10"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(445), null, "General Manager", 1, null },
                    { new Guid("f36b2473-7799-4075-a41b-dd2a295601ca"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(454), null, "Contract Administrator", 1, null },
                    { new Guid("f511426a-d354-4fc1-9177-81d5d4c243e7"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(396), null, "Sr. Manager of HR", 1, null },
                    { new Guid("f7a4ca58-6aac-4b0c-b9dd-b976d8338ea1"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(399), null, "Director of Information Security", 1, null },
                    { new Guid("fb99977f-93ca-4c15-8335-ddd9beb6c093"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(456), null, "Inspector", 1, null },
                    { new Guid("fcee848a-743d-4121-a527-b3bf977b0df1"), new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(385), null, "VP of Client Services", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "BirthDate", "BloodGroup", "CompanyId", "ConcurrencyStamp", "CreateDate", "DeleteDate", "Email", "EmailConfirmed", "IdentityNumber", "ImagePath", "JobStartDate", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PatronId", "PhoneNumber", "PhoneNumberConfirmed", "ProfessionId", "SecondName", "SecurityStamp", "Status", "Surname", "TitleId", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("0b7a5bd6-ce40-4dca-a22d-c0cde22cd952"), 0, null, new DateTime(2066, 11, 28, 21, 2, 3, 120, DateTimeKind.Local).AddTicks(2133), null, new Guid("25c98067-29d2-4df6-9af9-19817b07e803"), "9cf652a3-f87e-4222-a73a-43f45d244631", new DateTime(2023, 5, 29, 21, 2, 3, 120, DateTimeKind.Local).AddTicks(2133), null, "ibrahim.ozturk@hotmail.com", false, "43008714692", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "IBRAHIM.OZTURK@HOTMAIL.COM", "IBRAHIM.OZTURK@HOTMAIL.COM", "AQAAAAEAACcQAAAAEMPW+g6Pfxi8pnFGI+Bt+SJkizY6CtiYCf+EvwYqb/7EZ9JLlHEou0riFDJg0/sP3A==", new Guid("00000000-0000-0000-0000-000000000000"), "+905369522651", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "30NA0IKCPW84PKD0KBBXWYVZQXYDL5KQ", 3, "Öztürk", new Guid("320f52ff-7c7b-4aee-a8f5-4c6201d9d8e5"), false, null, "ibrahim.ozturk@hotmail.com" },
                    { new Guid("0ebed8a1-2d62-4c4d-ba24-364b37aa794d"), 0, null, new DateTime(2051, 1, 22, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(480), null, new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), "1f29075b-8a77-4541-af7d-b0a664f36a3f", new DateTime(2023, 5, 29, 21, 2, 3, 100, DateTimeKind.Local).AddTicks(476), null, "test1@test.com", false, "85048331620", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Yusuf", "TEST1@TEST.COM", "TEST1@TEST.COM", "AQAAAAEAACcQAAAAECVWJjgTSgd4da294WClrrpNhMM/9X8GHTDhY0QybLOWdbDA5S7HVCq+/T57+FX+FA==", new Guid("00000000-0000-0000-0000-000000000000"), "+905657351451", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "T272UJZ65K63XGILMGMFYOLJ5MZS23Y4", 1, "Kaya", new Guid("9e050e41-f5ea-4451-a1c8-1bb4ade067ad"), false, null, "test1@test.com" },
                    { new Guid("2039f5ba-8513-4ced-92cf-2c401d61443d"), 0, null, new DateTime(2047, 2, 18, 21, 2, 3, 110, DateTimeKind.Local).AddTicks(958), null, new Guid("a4dbf9f7-2382-4d5f-9ada-bb6af5284a29"), "9ecf5f7d-69bf-43ec-8690-c4170d5f62d8", new DateTime(2023, 5, 29, 21, 2, 3, 110, DateTimeKind.Local).AddTicks(957), null, "ahmet.yildiz@microsoft.com", false, "44315316142", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "AHMET.YILDIZ@MICROSOFT.COM", "AHMET.YILDIZ@MICROSOFT.COM", "AQAAAAEAACcQAAAAED4xsSGmVXCbQL/cIb6Xf1EGltN4RQueY+ZwayEA2esSelEZ3kzH5p4GVD0kDZICNQ==", new Guid("00000000-0000-0000-0000-000000000000"), "+905874668465", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "0CLZTORG10VJY1YH07XSYCU8L1L729YE", 3, "Yıldız", new Guid("7761dfb8-bdf4-480e-8278-b783a3c6b5ee"), false, null, "ahmet.yildiz@microsoft.com" },
                    { new Guid("248754ac-60c0-44db-91c5-5e46819b55c8"), 0, null, new DateTime(2046, 4, 19, 21, 2, 3, 122, DateTimeKind.Local).AddTicks(7157), null, new Guid("40a011d8-7b9e-4b40-8a56-ce1af7d65568"), "fefd5ee8-dc00-4f99-bdfa-46ea78852ad7", new DateTime(2023, 5, 29, 21, 2, 3, 122, DateTimeKind.Local).AddTicks(7156), null, "mehmet.aydin@hotmail.com", false, "74528758354", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mehmet", "MEHMET.AYDIN@HOTMAIL.COM", "MEHMET.AYDIN@HOTMAIL.COM", "AQAAAAEAACcQAAAAEO7z42tV0+WY1uGFlgdy8HTJmoE/0zbUhwQ47Ep4JgTrSyCNtze1MZZ05VGDek7Alg==", new Guid("00000000-0000-0000-0000-000000000000"), "+905790294979", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "RGFAI49A1BXXASA2B1PENHGDDMTHPFSM", 3, "Aydın", new Guid("e8c924ce-c325-4713-8ef2-f37dfdaec070"), false, null, "mehmet.aydin@hotmail.com" },
                    { new Guid("2a8d4ef3-6f78-4a90-af33-605da31d278a"), 0, null, new DateTime(2055, 11, 15, 21, 2, 3, 103, DateTimeKind.Local).AddTicks(8913), null, new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), "6977a974-4d78-4f22-8a3e-8d32857e1c9b", new DateTime(2023, 5, 29, 21, 2, 3, 103, DateTimeKind.Local).AddTicks(8912), null, "test4@test.com", false, "45082326044", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "TEST4@TEST.COM", "TEST4@TEST.COM", "AQAAAAEAACcQAAAAEMhqGvPuFfOCXJ3N+T1ohhAC0TddqYC+kZrgHldSM5pN9DLBqGylbuFEy+kWLY7z2w==", new Guid("00000000-0000-0000-0000-000000000000"), "+905120960678", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "YUZK5K8SK3DV5E1IQIZ19KQMWHBEEE5L", 1, "Şahin", new Guid("0a688378-ef8b-493a-97e8-0ca243378b05"), false, null, "test4@test.com" },
                    { new Guid("2fe634f7-12ff-42a8-bf98-1986950b0723"), 0, null, new DateTime(2076, 3, 19, 21, 2, 3, 117, DateTimeKind.Local).AddTicks(7393), null, new Guid("44750478-0513-4836-95c1-8b537427d58d"), "ea33a22b-6568-40f9-adab-37785e311031", new DateTime(2023, 5, 29, 21, 2, 3, 117, DateTimeKind.Local).AddTicks(7393), null, "ahmet.aydin@hotmail.com", false, "28657682790", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "AHMET.AYDIN@HOTMAIL.COM", "AHMET.AYDIN@HOTMAIL.COM", "AQAAAAEAACcQAAAAELR7EIK1CYc1AK5cqREs4zkyl1XWzCgU0aO/W0JXgs95ecmgHgGqQo9bNEdYABq/Iw==", new Guid("00000000-0000-0000-0000-000000000000"), "+905635998889", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "20H6JWXF4JDSKPNI10CL2Y8XVHIE3IYU", 3, "Aydın", new Guid("35d4d68a-cec9-4a40-b866-209927e5bf49"), false, null, "ahmet.aydin@hotmail.com" },
                    { new Guid("4bd0d033-b30f-4d13-b298-630c8b565685"), 0, null, new DateTime(2047, 6, 1, 21, 2, 3, 121, DateTimeKind.Local).AddTicks(4802), null, new Guid("bb952bd3-1858-4901-b151-62793d718f76"), "eb19e1e5-7136-41d8-872d-64d650aa6554", new DateTime(2023, 5, 29, 21, 2, 3, 121, DateTimeKind.Local).AddTicks(4801), null, "ismail.yildirim@outlook.com", false, "33600657718", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.YILDIRIM@OUTLOOK.COM", "ISMAIL.YILDIRIM@OUTLOOK.COM", "AQAAAAEAACcQAAAAEGM1aA2Z3eJ6C0LnTjNvYedjt/exWUNfe6f+HAlVwbleGImPq+m+rkEZUHZaZfv1zQ==", new Guid("00000000-0000-0000-0000-000000000000"), "+905940533433", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "HOQE7HL8RHKX4JTBXCI18896JRBEW5Y8", 3, "Yıldırım", new Guid("b7b4707e-847f-4dd7-bed8-f8b2cee431e4"), false, null, "ismail.yildirim@outlook.com" },
                    { new Guid("53691b9b-7af3-4521-9e38-e46f88593f41"), 0, null, new DateTime(2048, 7, 11, 21, 2, 3, 116, DateTimeKind.Local).AddTicks(4839), null, new Guid("85544b4c-3077-4c1d-8c8d-129b7a7b5ec5"), "53324d92-4a20-4a7d-9177-7dec53688b11", new DateTime(2023, 5, 29, 21, 2, 3, 116, DateTimeKind.Local).AddTicks(4839), null, "ahmet.yildirim@hotmail.com", false, "13122452640", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "AHMET.YILDIRIM@HOTMAIL.COM", "AHMET.YILDIRIM@HOTMAIL.COM", "AQAAAAEAACcQAAAAELlUT1aLtp/9WaFoaxX4wKF7MdzksCY7R3M1RbCrF0wcyN430hmUbZyBrvopKPxxhg==", new Guid("00000000-0000-0000-0000-000000000000"), "+905426073471", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "LJ648FI91S1CRAJVQBZKJR1T1LZVB7RF", 3, "Yıldırım", new Guid("791ad158-e7b0-458c-ba9a-c85093eb2345"), false, null, "ahmet.yildirim@hotmail.com" },
                    { new Guid("619f341e-0b60-4b27-8eab-b68a93563804"), 0, null, new DateTime(2073, 4, 19, 21, 2, 3, 112, DateTimeKind.Local).AddTicks(5535), null, new Guid("0301f8a6-9827-4fe9-8e87-4eed41170833"), "4dce4e9f-121c-47a0-8f54-aafdf348d8d1", new DateTime(2023, 5, 29, 21, 2, 3, 112, DateTimeKind.Local).AddTicks(5534), null, "ibrahim.yildiz@google.com", false, "17387448530", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "IBRAHIM.YILDIZ@GOOGLE.COM", "IBRAHIM.YILDIZ@GOOGLE.COM", "AQAAAAEAACcQAAAAEBRaRmKbMSQmisdffp7OeUY+uGQEzsG2x2gbKxr+/dEWVc3EXBlXfVTE0VbxEYTbSQ==", new Guid("00000000-0000-0000-0000-000000000000"), "+905599163574", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "WRW50S1E013YTQ63ISFPBAR7SBBQK5VS", 3, "Yıldız", new Guid("6dcc8973-ae06-47e2-9383-b67925c30482"), false, null, "ibrahim.yildiz@google.com" },
                    { new Guid("61ee232a-1f82-4983-b582-9d21d6408248"), 0, null, new DateTime(2047, 10, 9, 21, 2, 3, 105, DateTimeKind.Local).AddTicks(1470), null, new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), "9ca7947f-731f-4914-bcd9-5987cce27b4a", new DateTime(2023, 5, 29, 21, 2, 3, 105, DateTimeKind.Local).AddTicks(1469), null, "test5@test.com", false, "87870744364", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mustafa", "TEST5@TEST.COM", "TEST5@TEST.COM", "AQAAAAEAACcQAAAAEH90MWGZvJPA8kppZB788jCggrF/ZPYDf+HmEVLjfuYjTVEN2NrFzTQlj3dFZ/MvHA==", new Guid("00000000-0000-0000-0000-000000000000"), "+905954547552", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "MNTQAYT3NARBNUIA75WHWZZN37TQ6QVY", 1, "Öztürk", new Guid("fb99977f-93ca-4c15-8335-ddd9beb6c093"), false, null, "test5@test.com" },
                    { new Guid("775a3978-c2ce-4c82-8c4b-350e18876ddb"), 0, null, new DateTime(2062, 2, 25, 21, 2, 3, 107, DateTimeKind.Local).AddTicks(6283), null, new Guid("d9800162-e8a7-48af-8033-d4efe3875b97"), "0410cc1c-6ffc-4c4b-a860-882eb4f3bd17", new DateTime(2023, 5, 29, 21, 2, 3, 107, DateTimeKind.Local).AddTicks(6282), null, "ismail.yildirim@google.com", false, "81727284188", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.YILDIRIM@GOOGLE.COM", "ISMAIL.YILDIRIM@GOOGLE.COM", "AQAAAAEAACcQAAAAEHgCimPIlX+S4ss3L2NKHGXj8slAv88zq12LylfLOnwtGYKhiMt0RDwO30k2oY7AiA==", new Guid("00000000-0000-0000-0000-000000000000"), "+905876184779", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "NK636DRYQCAOM38N3MVQHD6AWZ5BDADC", 3, "Yıldırım", new Guid("4a38a5e7-fa7c-4f35-8983-595ce4f8dad3"), false, null, "ismail.yildirim@google.com" },
                    { new Guid("78267705-c7f1-4313-8812-e31275a67bee"), 0, null, new DateTime(2053, 10, 16, 21, 2, 3, 108, DateTimeKind.Local).AddTicks(8766), null, new Guid("e9efaf95-c9d3-4991-abb7-b08e02818d95"), "622fb6d8-7733-412f-9989-8a60e43d9555", new DateTime(2023, 5, 29, 21, 2, 3, 108, DateTimeKind.Local).AddTicks(8766), null, "mehmet.celik@yahoo.com", false, "51633825294", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mehmet", "MEHMET.CELIK@YAHOO.COM", "MEHMET.CELIK@YAHOO.COM", "AQAAAAEAACcQAAAAEO31YOEIVZatX6UTSsPfhnHn9krBGqywafI0LaMAUgx+OoOJ1uSnSASHQxDH++HUFg==", new Guid("00000000-0000-0000-0000-000000000000"), "+905866677043", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "U67ICKWQU3HVEV7FMI89XMV5BNEMX1ZA", 3, "Çelik", new Guid("b21a65f2-a52d-4f16-b348-895bac64bc4c"), false, null, "mehmet.celik@yahoo.com" },
                    { new Guid("8f043985-d8a6-42fc-ac06-8cf594f861b3"), 0, null, new DateTime(2072, 12, 23, 21, 2, 3, 106, DateTimeKind.Local).AddTicks(3984), null, new Guid("b3e54d51-7039-4211-9238-a80bd94b7ed5"), "dbccb588-e0f9-4b40-94a6-7b52b0d23eb1", new DateTime(2023, 5, 29, 21, 2, 3, 106, DateTimeKind.Local).AddTicks(3982), null, "ibrahim.yildiz@microsoft.com", false, "70526853466", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "IBRAHIM.YILDIZ@MICROSOFT.COM", "IBRAHIM.YILDIZ@MICROSOFT.COM", "AQAAAAEAACcQAAAAEDmAxNp+BGDVb3lx752uJFEZ+e6XP/J8F6hL/QEy4ZayifqzD2PePRDSOsLbtg8KdA==", new Guid("00000000-0000-0000-0000-000000000000"), "+905481525113", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "R129D5DJY8BRX8XJ93H5D6IG1QWHB1MI", 3, "Yıldız", new Guid("9458593b-dc0d-463e-abd9-5846a6926073"), false, null, "ibrahim.yildiz@microsoft.com" },
                    { new Guid("8f86018a-91f8-43ef-b491-ef2061a813b5"), 0, null, new DateTime(2064, 8, 30, 21, 2, 3, 101, DateTimeKind.Local).AddTicks(3561), null, new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), "b41403e2-8e49-4ec1-b805-65a8123476af", new DateTime(2023, 5, 29, 21, 2, 3, 101, DateTimeKind.Local).AddTicks(3560), null, "test2@test.com", false, "61242547326", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "TEST2@TEST.COM", "TEST2@TEST.COM", "AQAAAAEAACcQAAAAEC2g+dZeUGkEp+ASguc6mRWdplI8lRohYEyowMp57MrIR06TOZ6TfMfKMprTmc4PhQ==", new Guid("00000000-0000-0000-0000-000000000000"), "+905858268415", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "P6JC53JEV6IKL9QCUHQ62DZ8GI5VU224", 1, "Demir", new Guid("85290556-c190-464c-ad12-4ae6e1ade63a"), false, null, "test2@test.com" },
                    { new Guid("a4deab07-b233-4700-88bf-d002d0ebc27f"), 0, null, new DateTime(2046, 8, 27, 21, 2, 3, 111, DateTimeKind.Local).AddTicks(3262), null, new Guid("b57cb575-9690-4b1f-9be0-d486bf89e2f0"), "4823ba26-b746-45f3-bd3b-5b7777c7a61b", new DateTime(2023, 5, 29, 21, 2, 3, 111, DateTimeKind.Local).AddTicks(3261), null, "ahmet.sahin@hotmail.com", false, "80575001220", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "AHMET.SAHIN@HOTMAIL.COM", "AHMET.SAHIN@HOTMAIL.COM", "AQAAAAEAACcQAAAAEOJVxAOXC5/I8bZ4qh3fjIGVZj5gV+Pyb9TJnmyVYvq/RoZJNK9Mfq0vqXdrHxmOfQ==", new Guid("00000000-0000-0000-0000-000000000000"), "+905756771814", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "ZYQ2XK91RT47D35VLWETY1DP83VANKG2", 3, "Şahin", new Guid("aa618c50-7121-4146-8d38-042a41e6dde0"), false, null, "ahmet.sahin@hotmail.com" },
                    { new Guid("b2755225-14e5-4f00-9434-1f88df384aab"), 0, null, new DateTime(2075, 4, 13, 21, 2, 3, 102, DateTimeKind.Local).AddTicks(6543), null, new Guid("e6a8661e-1ce1-480d-8b4d-d5bf4a30e0d3"), "77d0fa6d-305b-48ac-92dc-07aa3c99abb4", new DateTime(2023, 5, 29, 21, 2, 3, 102, DateTimeKind.Local).AddTicks(6542), null, "test3@test.com", false, "58488124718", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "TEST3@TEST.COM", "TEST3@TEST.COM", "AQAAAAEAACcQAAAAEBDtDoZtcQR9ggathIggjrQq6kvvWAfoBcspyz1A/0wR66JwgJZjw0o3txiACxnNrQ==", new Guid("00000000-0000-0000-0000-000000000000"), "+905880951586", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "76M8XNEMO35IUCYWK0BUO32MLS4G67TP", 1, "Yıldırım", new Guid("294e4a64-a769-4790-ad96-176987a2891f"), false, null, "test3@test.com" },
                    { new Guid("c48d740f-4a63-495e-9e8c-be099e9355da"), 0, null, new DateTime(2071, 10, 28, 21, 2, 3, 113, DateTimeKind.Local).AddTicks(8747), null, new Guid("ac17ab07-bef7-4a3c-9acf-5d46f4203e86"), "25544542-1049-4f28-9d2b-ff4d85b2f7e8", new DateTime(2023, 5, 29, 21, 2, 3, 113, DateTimeKind.Local).AddTicks(8746), null, "osman.yildiz@yandex.com", false, "55445343632", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "OSMAN.YILDIZ@YANDEX.COM", "OSMAN.YILDIZ@YANDEX.COM", "AQAAAAEAACcQAAAAEIGvSCJJ82o3nt6wtsE1P3yyP6NkPeEOVDHomyOKkcSG46IdsxwXPWUxZagXfcXCRg==", new Guid("00000000-0000-0000-0000-000000000000"), "+905757103561", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "PJHN53TRN13TA6WF1JEQ0CZBD7CVVHWH", 3, "Yıldız", new Guid("c62bfe62-7a37-412b-8e72-53e00b9cf330"), false, null, "osman.yildiz@yandex.com" },
                    { new Guid("dd531f7d-9594-47fa-a5e8-83aba3148072"), 0, null, new DateTime(2055, 11, 28, 21, 2, 3, 118, DateTimeKind.Local).AddTicks(9749), null, new Guid("2e436b3d-3a62-4221-9548-9163631aa310"), "d3e9d0a8-b3d0-43f8-8422-ae15c885e8e7", new DateTime(2023, 5, 29, 21, 2, 3, 118, DateTimeKind.Local).AddTicks(9748), null, "ahmet.yildirim@google.com", false, "82017532446", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "AHMET.YILDIRIM@GOOGLE.COM", "AHMET.YILDIRIM@GOOGLE.COM", "AQAAAAEAACcQAAAAENAO7TFuzbPg1kY5CuGeZpiWhV5algctL+4waeX4/vEiI6aUgf7aKsh1v+4CDdiNNw==", new Guid("00000000-0000-0000-0000-000000000000"), "+905615968013", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "OSX5APK435VESFMF1E82P0Z41L1H1SGW", 3, "Yıldırım", new Guid("e1672b77-2766-487d-bafd-a5f40b7d5f72"), false, null, "ahmet.yildirim@google.com" },
                    { new Guid("f3320430-7163-4a44-abd1-9ed0e312a244"), 0, null, new DateTime(2062, 5, 17, 21, 2, 3, 123, DateTimeKind.Local).AddTicks(9473), null, new Guid("0986b5ae-b2d2-4271-977f-f78633d9ccc1"), "195689ec-ee7e-42b4-ad2a-ae300c304178", new DateTime(2023, 5, 29, 21, 2, 3, 123, DateTimeKind.Local).AddTicks(9472), null, "ibrahim.demir@google.com", false, "45670053314", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "IBRAHIM.DEMIR@GOOGLE.COM", "IBRAHIM.DEMIR@GOOGLE.COM", "AQAAAAEAACcQAAAAEIYRwLdDAf03BA2H6PZPG3UPTIk0dTo1D8r+tlv6K1ieNhvMlBpGOzphMl6C1ptR2A==", new Guid("00000000-0000-0000-0000-000000000000"), "+905331439296", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "YSRXLMHOKAI1TVCMWIDRYFHES5QSZYK7", 3, "Demir", new Guid("d4e8c337-fd69-427e-afee-5690a35ed77f"), false, null, "ibrahim.demir@google.com" },
                    { new Guid("f89dfe4b-6ced-4316-8d19-751221b346d3"), 0, null, new DateTime(2070, 4, 16, 21, 2, 3, 115, DateTimeKind.Local).AddTicks(1832), null, new Guid("56c07785-dcc5-4448-b5aa-af011bf7711e"), "77d6bf48-272c-4c10-a71e-7b545d765f73", new DateTime(2023, 5, 29, 21, 2, 3, 115, DateTimeKind.Local).AddTicks(1832), null, "ahmet.yildiz@yandex.com", false, "45200314526", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "AHMET.YILDIZ@YANDEX.COM", "AHMET.YILDIZ@YANDEX.COM", "AQAAAAEAACcQAAAAEMh4sbPKPPvXbl3IRFGp1gEfQgm7Khlriq9WesVksFSKFGTaMesNS+HxZVJSk/akCA==", new Guid("00000000-0000-0000-0000-000000000000"), "+905443950885", false, new Guid("00000000-0000-0000-0000-000000000000"), null, "AVDA5USRTSF2QJL7PPSA3PATIPIG0M15", 3, "Yıldız", new Guid("a50df42d-9a40-4c1a-b0b9-95e6ae07aa8c"), false, null, "ahmet.yildiz@yandex.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("0b7a5bd6-ce40-4dca-a22d-c0cde22cd952") },
                    { new Guid("783be846-6d5a-4b95-963a-bdd977ab24ac"), new Guid("0ebed8a1-2d62-4c4d-ba24-364b37aa794d") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("2039f5ba-8513-4ced-92cf-2c401d61443d") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("248754ac-60c0-44db-91c5-5e46819b55c8") },
                    { new Guid("783be846-6d5a-4b95-963a-bdd977ab24ac"), new Guid("2a8d4ef3-6f78-4a90-af33-605da31d278a") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("2fe634f7-12ff-42a8-bf98-1986950b0723") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("4bd0d033-b30f-4d13-b298-630c8b565685") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("53691b9b-7af3-4521-9e38-e46f88593f41") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("619f341e-0b60-4b27-8eab-b68a93563804") },
                    { new Guid("783be846-6d5a-4b95-963a-bdd977ab24ac"), new Guid("61ee232a-1f82-4983-b582-9d21d6408248") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("775a3978-c2ce-4c82-8c4b-350e18876ddb") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("78267705-c7f1-4313-8812-e31275a67bee") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("8f043985-d8a6-42fc-ac06-8cf594f861b3") },
                    { new Guid("783be846-6d5a-4b95-963a-bdd977ab24ac"), new Guid("8f86018a-91f8-43ef-b491-ef2061a813b5") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("a4deab07-b233-4700-88bf-d002d0ebc27f") },
                    { new Guid("783be846-6d5a-4b95-963a-bdd977ab24ac"), new Guid("b2755225-14e5-4f00-9434-1f88df384aab") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("c48d740f-4a63-495e-9e8c-be099e9355da") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("dd531f7d-9594-47fa-a5e8-83aba3148072") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("f3320430-7163-4a44-abd1-9ed0e312a244") },
                    { new Guid("29b2fd25-ec67-40e3-8518-492cfa91ac66"), new Guid("f89dfe4b-6ced-4316-8d19-751221b346d3") }
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
                name: "IX_AspNetUsers_ProfessionId",
                table: "AspNetUsers",
                column: "ProfessionId");

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
                name: "IX_Professions_CompanyId",
                table: "Professions",
                column: "CompanyId");

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
                name: "Professions");

            migrationBuilder.DropTable(
                name: "Titles");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Sectors");
        }
    }
}
