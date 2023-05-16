using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKApplication.Persistance.Migrations
{
    public partial class SyncCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    DirectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPaymentProcessed = table.Column<int>(type: "int", nullable: false),
                    FinalDateRequest = table.Column<DateTime>(type: "date", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashAdvances", x => x.Id);
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
                    ShortDescription = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LongDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpenseById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
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
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaveType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leaves_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3b6233e4-8070-46ed-a3b8-cd59171cf3ac"), "ODCE7GF41R1AUM71QR0P6UC8BB809EK5", "Site Administrator", "SITE ADMINISTRATOR" },
                    { new Guid("6a9a9858-d856-4bd8-ad59-9a2891b13903"), "D60V9Z9SU5LUPQSXXMNJ0AGGJOR42DAG", "Personal", "PERSONAL" },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), "BKWCQCMPFKYYGIDTGTC6LFL4RUHKCJJA", "Company Administrator", "COMPANY ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("153fb663-d57d-4b19-9803-a0a21f27ccb3"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(359), null, "Metal", 1, null },
                    { new Guid("16a6dc59-2ec9-4295-acf8-a12046a37955"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(361), null, "Sağlık ve Sosyal Hizmetler", 1, null },
                    { new Guid("206e4aca-2596-4f86-9cfa-d5581ed6c706"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(323), null, "Ahşap Teknolojisi", 1, null },
                    { new Guid("24b5e10e-9068-409a-b400-37ba3454453a"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(354), null, "Kültür, Sanat ve Tasarım", 1, null },
                    { new Guid("266b61bd-ece3-4dba-8cdf-494c5638e3b3"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(349), null, "Gıda", 1, null },
                    { new Guid("2a2c68d0-346c-4b49-bd06-b7ec7af2d8a5"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(344), null, "Enerji", 1, null },
                    { new Guid("3158e781-15e9-4552-82ea-10bec4302f1d"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(339), null, "Bilişim", 1, null },
                    { new Guid("41ea0c61-a464-4157-b39c-7de310592925"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(342), null, "Elektrik ve Elektronik", 1, null },
                    { new Guid("633a1522-7775-45ad-9137-9b51b0ebf1be"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(353), null, "Kimya, Petrol, Lastik ve PLastik", 1, null },
                    { new Guid("70b212d4-1881-4e8b-8d61-7554d67a6f8e"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(366), null, "Ticaret (Satış ve Pazarlama)", 1, null },
                    { new Guid("900d74a5-7f5e-472d-8c6e-6b96cddbc068"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(397), null, "Turizm, Konaklama, Yiyecek-İçecek Hizmetleri", 1, null },
                    { new Guid("92ed51fd-ec4f-4aed-b4db-0b52852132d2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(365), null, "Tekstil, Hazır Giyim, Deri", 1, null },
                    { new Guid("95c87f7c-2941-4a35-ad7b-ccacbdac6a4b"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(340), null, "Cam, Çimento ve ToprakÇevre", 1, null },
                    { new Guid("a79c85eb-9aca-40cb-9fe0-722a68deb508"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(341), null, "Eğitim", 1, null },
                    { new Guid("b0d244d3-a4b3-4bf9-abc8-3a872aa67fb2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(364), null, "Tarım, Avcılık ve Balıkçılık", 1, null },
                    { new Guid("b4d26ede-79c9-4ffd-a70c-5fa23f536bf2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(357), null, "Medya, İletişim ve Yayıncılık", 1, null },
                    { new Guid("c13a5aca-5472-41d6-81e2-a6db3652bca2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(360), null, "Otomotiv", 1, null },
                    { new Guid("c92b9d1b-8591-454c-9308-25f916a9e549"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(367), null, "Toplumsal ve Kişisel Hizmetler", 1, null },
                    { new Guid("c948d118-5bad-493f-ae1b-dbfb22984df1"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(398), null, "Ulaştırma, Lojistik ve Haberleşme", 1, null },
                    { new Guid("ce02fffd-11a1-4a2a-9597-fb6bb9051259"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(350), null, "İnşaat", 1, null },
                    { new Guid("d7ea0e21-4071-423b-b7fe-5bda1fe81b1f"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(352), null, "İş ve Yönetimi", 1, null },
                    { new Guid("d87fb5a8-497b-4e39-a5df-41663839b9b7"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(363), null, "Spor ve Rekreasyon", 1, null },
                    { new Guid("f144667c-4e21-4e38-a514-2ba23db52290"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(348), null, "Finans", 1, null },
                    { new Guid("f4b8e2b2-5e38-4b8d-a3a7-fa8416aca55e"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(356), null, "Maden", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "NumberOfEmployees", "PhoneNumber", "SectorId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("087a84d3-7685-406d-ab80-735bd2b4899a"), new DateTime(2023, 5, 17, 0, 34, 59, 919, DateTimeKind.Local).AddTicks(787), null, "info@sahinanonimsirketi.com", "Şahin Anonim Şirketi", 33, "+905134251960", new Guid("a79c85eb-9aca-40cb-9fe0-722a68deb508"), 3, null },
                    { new Guid("1089b923-128a-41a3-8055-4531cc96bcb8"), new DateTime(2023, 5, 17, 0, 34, 59, 920, DateTimeKind.Local).AddTicks(3657), null, "info@ozturkkomanditsirketi.com", "Öztürk Komandit Şirketi", 72, "+905799327077", new Guid("266b61bd-ece3-4dba-8cdf-494c5638e3b3"), 3, null },
                    { new Guid("233462cc-903b-4d37-bd29-478db6148421"), new DateTime(2023, 5, 17, 0, 34, 59, 917, DateTimeKind.Local).AddTicks(7133), null, "info@yildirimkomanditsirketi.com", "Yıldırım Komandit Şirketi", 9, "+905183653656", new Guid("b4d26ede-79c9-4ffd-a70c-5fa23f536bf2"), 3, null },
                    { new Guid("3df51d67-ed70-4f0a-81a9-22ba7e8569e2"), new DateTime(2023, 5, 17, 0, 34, 59, 921, DateTimeKind.Local).AddTicks(6894), null, "info@yildizlimitedsirketi.com", "Yıldız Limited Şirketi", 80, "+905630577989", new Guid("2a2c68d0-346c-4b49-bd06-b7ec7af2d8a5"), 3, null },
                    { new Guid("520d2102-5003-4ff2-9f84-ef150238754b"), new DateTime(2023, 5, 17, 0, 34, 59, 915, DateTimeKind.Local).AddTicks(1009), null, "info@ozdemirkomanditsirketi.com", "Özdemir Komandit Şirketi", 41, "+905358197477", new Guid("b4d26ede-79c9-4ffd-a70c-5fa23f536bf2"), 3, null },
                    { new Guid("56d921c6-d025-4332-abd1-42508a1d2f82"), new DateTime(2023, 5, 17, 0, 34, 59, 916, DateTimeKind.Local).AddTicks(4300), null, "info@kayakomanditsirketi.com", "Kaya Komandit Şirketi", 17, "+905449848046", new Guid("c948d118-5bad-493f-ae1b-dbfb22984df1"), 3, null },
                    { new Guid("59e5edfb-77fc-4727-a735-358ef9b4736b"), new DateTime(2023, 5, 17, 0, 34, 59, 922, DateTimeKind.Local).AddTicks(9783), null, "info@sahinkollektifsirketi.com", "Şahin Kollektif Şirketi", 70, "+905764261839", new Guid("d7ea0e21-4071-423b-b7fe-5bda1fe81b1f"), 3, null },
                    { new Guid("6c16c6d9-f095-428f-9ef5-05834c7c8abf"), new DateTime(2023, 5, 17, 0, 34, 59, 912, DateTimeKind.Local).AddTicks(4888), null, "info@demirkomanditsirketi.com", "Demir Komandit Şirketi", 64, "+905669047454", new Guid("633a1522-7775-45ad-9137-9b51b0ebf1be"), 3, null },
                    { new Guid("78922a67-596c-4b9a-b6d6-8d3eca5e87a2"), new DateTime(2023, 5, 17, 0, 34, 59, 908, DateTimeKind.Local).AddTicks(5021), null, "info@sahinkollektifsirketi.com", "Şahin Kollektif Şirketi", 60, "+905287131191", new Guid("b4d26ede-79c9-4ffd-a70c-5fa23f536bf2"), 3, null },
                    { new Guid("7a2b9ecd-1044-446b-b293-9f3767c6ad6a"), new DateTime(2023, 5, 17, 0, 34, 59, 905, DateTimeKind.Local).AddTicks(8208), null, "info@yildirimkomanditsirketi.com", "Yıldırım Komandit Şirketi", 32, "+905854323180", new Guid("c948d118-5bad-493f-ae1b-dbfb22984df1"), 3, null },
                    { new Guid("89cd36d8-0b37-4a66-b124-29054b6d4e14"), new DateTime(2023, 5, 17, 0, 34, 59, 924, DateTimeKind.Local).AddTicks(2218), null, "info@sahinkollektifsirketi.com", "Şahin Kollektif Şirketi", 80, "+905754056903", new Guid("24b5e10e-9068-409a-b400-37ba3454453a"), 3, null },
                    { new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(487), null, "ikapp@ikapp.com", "IKApp A.Ş.", 5, "+905696567126", new Guid("3158e781-15e9-4552-82ea-10bec4302f1d"), 1, null },
                    { new Guid("cc30e96f-822b-46be-bd15-bef0ff51a441"), new DateTime(2023, 5, 17, 0, 34, 59, 909, DateTimeKind.Local).AddTicks(8653), null, "info@aydinkomanditsirketi.com", "Aydın Komandit Şirketi", 68, "+905338369623", new Guid("ce02fffd-11a1-4a2a-9597-fb6bb9051259"), 3, null },
                    { new Guid("e11d302b-cda5-45ce-9c7b-b29a5aad2557"), new DateTime(2023, 5, 17, 0, 34, 59, 911, DateTimeKind.Local).AddTicks(1717), null, "info@yilmazkooperatifsirketi.com", "Yılmaz Kooperatif Şirketi", 87, "+905648190032", new Guid("c92b9d1b-8591-454c-9308-25f916a9e549"), 3, null },
                    { new Guid("f0155b24-45ac-4a56-b1b3-82187f1e390c"), new DateTime(2023, 5, 17, 0, 34, 59, 913, DateTimeKind.Local).AddTicks(8332), null, "info@kayakomanditsirketi.com", "Kaya Komandit Şirketi", 23, "+905654559843", new Guid("900d74a5-7f5e-472d-8c6e-6b96cddbc068"), 3, null },
                    { new Guid("f3d8e4e1-6ca9-4b67-8245-8d57cbf14ff0"), new DateTime(2023, 5, 17, 0, 34, 59, 907, DateTimeKind.Local).AddTicks(1907), null, "info@sahinkomanditsirketi.com", "Şahin Komandit Şirketi", 20, "+905111357051", new Guid("d87fb5a8-497b-4e39-a5df-41663839b9b7"), 3, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("02144bd0-938e-456b-90d9-73023b719142"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(553), null, "Clinical Laboratory Technician", 1, null },
                    { new Guid("03a60eee-ba3d-4b9b-a344-22529c08c031"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(554), null, "Occupational Therapy Aide", 1, null },
                    { new Guid("064d1451-e5bb-48da-be5c-ff0f9b68a163"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(537), null, "Sr. Manager of HR", 1, null },
                    { new Guid("0a9591aa-a46d-4713-8c75-824e4060610f"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(557), null, "Registrar", 1, null },
                    { new Guid("0c2fb941-8781-4e8d-97d4-986482679a5a"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(606), null, "Construction Foreman", 1, null },
                    { new Guid("10b803a0-43b0-41c9-9415-6c62248cfc39"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(601), null, "Front Desk Associate", 1, null },
                    { new Guid("1b58f05d-2f76-48f5-baaa-e846cce1de15"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(602), null, "Server/Host/Hostess", 1, null },
                    { new Guid("1cdc5e9b-38d5-45e7-bcc3-9860b68b7144"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(521), null, "VP of Finance", 1, null },
                    { new Guid("1db7d5b8-5c67-4efc-aee3-f9ff10089021"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(531), null, "Customer Service Representative", 1, null },
                    { new Guid("20c3ca58-029c-4e74-a20f-16f99783458a"), new Guid("e11d302b-cda5-45ce-9c7b-b29a5aad2557"), new DateTime(2023, 5, 17, 0, 34, 59, 911, DateTimeKind.Local).AddTicks(1721), null, "HR Analyst", 1, null },
                    { new Guid("28b39c9f-ee28-4d17-8aaa-aee25cdc3e4d"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(555), null, "Administrator", 1, null },
                    { new Guid("2c90585a-802a-43d8-88e9-bb9fa417c3b1"), new Guid("087a84d3-7685-406d-ab80-735bd2b4899a"), new DateTime(2023, 5, 17, 0, 34, 59, 919, DateTimeKind.Local).AddTicks(791), null, "Account Manager", 1, null },
                    { new Guid("31b59d47-40e5-4084-9484-91e0403f2035"), new Guid("f0155b24-45ac-4a56-b1b3-82187f1e390c"), new DateTime(2023, 5, 17, 0, 34, 59, 913, DateTimeKind.Local).AddTicks(8336), null, "Physical Therapist", 1, null },
                    { new Guid("34ec0082-6598-4e5e-9f8e-375a02b02dcd"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(605), null, "Hotel Receptionist", 1, null },
                    { new Guid("36a4f3a1-30d4-4b5f-9568-cfbdcb187e8a"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(513), null, "CMO (Chief Marketing Officer)", 1, null },
                    { new Guid("43f330a3-6c07-48ec-b166-32e39e5c507b"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(607), null, "Safety Director", 1, null },
                    { new Guid("49a95727-ca4a-4af6-8686-d9309d4c36ea"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(515), null, "Sr. Marketing Manager", 1, null },
                    { new Guid("4abcb3d5-9d6e-477e-9135-f57f12503463"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(503), null, "VP of Sales", 1, null },
                    { new Guid("4cf4c6bb-1f2c-40f2-b906-4ef490cea7cb"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(608), null, "Project Manager", 1, null },
                    { new Guid("52ab7740-0f7c-4076-b4c5-d094e8c4b195"), new Guid("6c16c6d9-f095-428f-9ef5-05834c7c8abf"), new DateTime(2023, 5, 17, 0, 34, 59, 912, DateTimeKind.Local).AddTicks(4891), null, "Director of Business Operations", 1, null },
                    { new Guid("55c48b18-c126-4d1a-b969-95d84ac6ed4e"), new Guid("89cd36d8-0b37-4a66-b124-29054b6d4e14"), new DateTime(2023, 5, 17, 0, 34, 59, 924, DateTimeKind.Local).AddTicks(2220), null, "Server/Host/Hostess", 1, null },
                    { new Guid("634072dc-3dd1-40e1-ad01-9b43df35463d"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(548), null, "Pharmacy Technician", 1, null },
                    { new Guid("6437ae64-b452-4072-a616-64586b44a2f7"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(541), null, "Software Engineer I, II, III", 1, null },
                    { new Guid("64b64ea5-fbf2-4ea0-9edd-b2f74271119e"), new Guid("cc30e96f-822b-46be-bd15-bef0ff51a441"), new DateTime(2023, 5, 17, 0, 34, 59, 909, DateTimeKind.Local).AddTicks(8666), null, "Sr. Manager of HR", 1, null },
                    { new Guid("6970655c-b93d-4f3a-aaef-e79fa764f34b"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(595), null, "Teacher", 1, null },
                    { new Guid("697a98a1-21c0-4e11-a271-89e2c6035072"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(525), null, "Risk Analyst", 1, null },
                    { new Guid("79695e50-a562-4cea-8dca-f825d7172e1e"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(523), null, "Investment Analyst", 1, null },
                    { new Guid("7c9c0a87-511c-4605-8c0d-72ab62c3036b"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(510), null, "Sales Representative", 1, null },
                    { new Guid("7f10cecd-19f8-472c-be90-471964bcc6ac"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(520), null, "Marketing Coordinator", 1, null },
                    { new Guid("841c034a-eeb8-4299-80f4-d6cfc0203573"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(514), null, "Marketing Director", 1, null },
                    { new Guid("84cdad30-d527-44f8-bc4d-0a0c4f8683cc"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(511), null, "Sales Associate", 1, null },
                    { new Guid("85ec71df-bdef-4012-a8f4-d7e145a1534a"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(518), null, "Marketing Analyst", 1, null },
                    { new Guid("8be0f0e9-05ea-451b-b60f-25812fe5f912"), new Guid("1089b923-128a-41a3-8055-4531cc96bcb8"), new DateTime(2023, 5, 17, 0, 34, 59, 920, DateTimeKind.Local).AddTicks(3660), null, "Construction Foreman", 1, null },
                    { new Guid("8cc07fe4-d21d-4faa-9da2-33470d186797"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(600), null, "Concierge", 1, null },
                    { new Guid("8d15d13a-fd6f-444f-89e6-a44e5d5fbea5"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(536), null, "Operations Supervisor", 1, null },
                    { new Guid("8ddb6419-4360-48a3-901a-5f6943250220"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(599), null, "Guest Services Supervisor", 1, null },
                    { new Guid("8de4166f-2f22-4aa5-9716-24ffc09718a9"), new Guid("56d921c6-d025-4332-abd1-42508a1d2f82"), new DateTime(2023, 5, 17, 0, 34, 59, 916, DateTimeKind.Local).AddTicks(4304), null, "Marketing Analyst", 1, null },
                    { new Guid("96e918cf-15a7-4382-8fdd-d7364836e9a5"), new Guid("f3d8e4e1-6ca9-4b67-8245-8d57cbf14ff0"), new DateTime(2023, 5, 17, 0, 34, 59, 907, DateTimeKind.Local).AddTicks(1913), null, "Marketing Director", 1, null },
                    { new Guid("97eec902-db97-4009-95c6-6ac00ff15f02"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(598), null, "General Manager", 1, null },
                    { new Guid("9eab9bd3-c779-4341-9cf8-b74b03f017a2"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(544), null, "Data Analyst", 1, null },
                    { new Guid("a5da96eb-96f9-48f8-9493-5e47a59fe2f5"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(543), null, "Systems Administrator", 1, null },
                    { new Guid("a894abf7-b733-45b9-af5a-92409a5628cf"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(559), null, "School Counselor", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("a8bb6d7d-405e-4469-919c-25b70d3021be"), new Guid("59e5edfb-77fc-4727-a735-358ef9b4736b"), new DateTime(2023, 5, 17, 0, 34, 59, 922, DateTimeKind.Local).AddTicks(9856), null, "Sr. Manager of HR", 1, null },
                    { new Guid("aa99a805-104f-48b9-b523-228c2e854832"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(542), null, "Full Stack Developer", 1, null },
                    { new Guid("abad928d-739c-49a9-94d7-84845615da49"), new Guid("78922a67-596c-4b9a-b6d6-8d3eca5e87a2"), new DateTime(2023, 5, 17, 0, 34, 59, 908, DateTimeKind.Local).AddTicks(5026), null, "Procurement Director", 1, null },
                    { new Guid("b16fa03e-3540-4ec0-a49f-9ee1bd30381b"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(608), null, "Contract Administrator", 1, null },
                    { new Guid("b2be7c23-0eec-4698-a2d6-ebe4fc6b5623"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(534), null, "COO (Chief Operating Officer)", 1, null },
                    { new Guid("b3cab202-d979-40f2-b4fe-7348cd5a0ef6"), new Guid("233462cc-903b-4d37-bd29-478db6148421"), new DateTime(2023, 5, 17, 0, 34, 59, 917, DateTimeKind.Local).AddTicks(7136), null, "Administrator", 1, null },
                    { new Guid("b59ae56b-57c8-4a24-8587-fe06947b8242"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(508), null, "National Sales Director", 1, null },
                    { new Guid("b6a02c39-696a-4dbb-91a5-47417916b2a7"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(522), null, "Procurement Director", 1, null },
                    { new Guid("b9a511fe-3dd9-402d-aba3-a1f9ebf46b73"), new Guid("7a2b9ecd-1044-446b-b293-9f3767c6ad6a"), new DateTime(2023, 5, 17, 0, 34, 59, 905, DateTimeKind.Local).AddTicks(8213), null, "Construction Foreman", 1, null },
                    { new Guid("ba3be959-317f-44b1-b13e-8723b3c55e15"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(556), null, "Principal", 1, null },
                    { new Guid("bbcc27ca-3039-4ceb-b642-74ad54d08cdb"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(597), null, "Teaching Assistant", 1, null },
                    { new Guid("bce397a2-020b-49cf-aa14-be41d483fc6b"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(533), null, "Support Specialist", 1, null },
                    { new Guid("c065ef8a-0336-4eee-9144-c2752ca04da7"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(540), null, "Director of Information Security", 1, null },
                    { new Guid("c395b0ea-8ab0-4736-902c-6826789cf6da"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(551), null, "Nursing Assistant", 1, null },
                    { new Guid("c4b7845f-ca45-4cef-a88d-3d4dbd62fa15"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(509), null, "Regional Sales Manager", 1, null },
                    { new Guid("c8198def-5c72-4c46-a135-078bdfa00452"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(526), null, "VP of Client Services", 1, null },
                    { new Guid("cb87c2bc-3535-4457-acdf-451822a39b8b"), new Guid("520d2102-5003-4ff2-9f84-ef150238754b"), new DateTime(2023, 5, 17, 0, 34, 59, 915, DateTimeKind.Local).AddTicks(1012), null, "Sales Associate", 1, null },
                    { new Guid("cc345999-b94e-4068-b3ba-960a47950475"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(550), null, "Physical Therapist", 1, null },
                    { new Guid("d08970f2-9421-4d7e-96ce-ce226695bd8f"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(524), null, "Credit Analyst", 1, null },
                    { new Guid("d0b0a308-515b-4db8-a018-d8831d2130f6"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(610), null, "Inspector", 1, null },
                    { new Guid("d0e9fca0-3122-47f9-8a22-1d7c707be720"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(530), null, "Customer Success Manager", 1, null },
                    { new Guid("d22b1d9f-f540-4da1-b6f2-8fa0326f20da"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(535), null, "Director of Business Operations", 1, null },
                    { new Guid("dd94cff7-1ee3-4b8e-a9f5-9fc81f121b6f"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(539), null, "HR Analyst", 1, null },
                    { new Guid("e4be4020-fe1c-4bcd-ba1e-ed93e575da3d"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(610), null, "Project Appraisal Engineer", 1, null },
                    { new Guid("e70d7bf2-dda5-452d-bd4f-aeb6f9b70475"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(545), null, "Other Industries:", 1, null },
                    { new Guid("ecbcbf38-03c2-42bb-9d27-ff312a3c7448"), new Guid("3df51d67-ed70-4f0a-81a9-22ba7e8569e2"), new DateTime(2023, 5, 17, 0, 34, 59, 921, DateTimeKind.Local).AddTicks(6898), null, "Clinical Laboratory Technician", 1, null },
                    { new Guid("f574df10-9c54-47f8-91ff-a0af085ac8cd"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(528), null, "Account Manager", 1, null },
                    { new Guid("ff15af1b-9440-47c2-bf4b-10ce0804d5b7"), new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(546), null, "Registered Nurse", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "BirthDate", "BloodGroup", "CompanyId", "ConcurrencyStamp", "CreateDate", "DeleteDate", "Email", "EmailConfirmed", "IdentityNumber", "ImagePath", "JobStartDate", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profession", "SecondName", "SecurityStamp", "Status", "Surname", "TitleId", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("05cdd321-a4f3-47ed-b856-229e74ad3663"), 0, null, new DateTime(2071, 12, 31, 0, 34, 59, 920, DateTimeKind.Local).AddTicks(3665), null, new Guid("1089b923-128a-41a3-8055-4531cc96bcb8"), "563714b9-c8c2-4206-97e3-cc4fc81bb346", new DateTime(2023, 5, 17, 0, 34, 59, 920, DateTimeKind.Local).AddTicks(3664), null, "ahmet.ozturk@yandex.com", false, "83583041394", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "AHMET.OZTURK@YANDEX.COM", "AHMET.OZTURK@YANDEX.COM", "AQAAAAEAACcQAAAAEMg8YwKmFaBjZGyb2Sqa2xTJmNo0bjsW5obu297/zY6bi7IiYrOinjLDeFN7AKJCnA==", "+905989032598", false, "Policeman/Policewoman", null, "3T1L8C7RAVOJCF6G9EQRCOXAVBF07M5R", 3, "Öztürk", new Guid("8be0f0e9-05ea-451b-b60f-25812fe5f912"), false, null, "ahmet.ozturk@yandex.com" },
                    { new Guid("0adadabf-987a-4c45-9efb-f0f9db8edcf9"), 0, null, new DateTime(2074, 11, 18, 0, 34, 59, 913, DateTimeKind.Local).AddTicks(8341), null, new Guid("f0155b24-45ac-4a56-b1b3-82187f1e390c"), "326192ce-eb87-4a54-aa88-febf891a4913", new DateTime(2023, 5, 17, 0, 34, 59, 913, DateTimeKind.Local).AddTicks(8340), null, "huseyin.kaya@yandex.com", false, "30822135734", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "HUSEYIN.KAYA@YANDEX.COM", "HUSEYIN.KAYA@YANDEX.COM", "AQAAAAEAACcQAAAAEE7d1k11obxpxtrefOrPkeJsnJCeGlqS9EFHybQkTob4hz12JuUBaVjhPidcxBR7xg==", "+905568085595", false, "Chef/Cook", null, "U0QNCX0R6WGOA97EJSHX7VWHXOYJ0K2Z", 3, "Kaya", new Guid("31b59d47-40e5-4084-9484-91e0403f2035"), false, null, "huseyin.kaya@yandex.com" },
                    { new Guid("0ed03d90-2e96-440e-9779-6beebc540913"), 0, null, new DateTime(2067, 4, 28, 0, 34, 59, 919, DateTimeKind.Local).AddTicks(825), null, new Guid("087a84d3-7685-406d-ab80-735bd2b4899a"), "32e5672d-2b60-45f7-82e3-c176ba171dd8", new DateTime(2023, 5, 17, 0, 34, 59, 919, DateTimeKind.Local).AddTicks(824), null, "hasan.sahin@yahoo.com", false, "35731581092", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hasan", "HASAN.SAHIN@YAHOO.COM", "HASAN.SAHIN@YAHOO.COM", "AQAAAAEAACcQAAAAEAF2U4v+FX7NTtgTumCE2/Kh8gpgQ6vWZ4h/kym0yiWVZ439r7e2MOiM0JmTUvudJA==", "+905791770992", false, "Hairdresser", null, "S7HJD9QAI14AENAINVJ3SPCAGZ4YPD46", 3, "Şahin", new Guid("2c90585a-802a-43d8-88e9-bb9fa417c3b1"), false, null, "hasan.sahin@yahoo.com" },
                    { new Guid("0fa18ebe-8a8b-4674-bb81-fbf7e0f793da"), 0, null, new DateTime(2075, 6, 15, 0, 34, 59, 901, DateTimeKind.Local).AddTicks(6808), null, new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), "5bb21576-134c-44b1-ab96-3ccf4adbf5fd", new DateTime(2023, 5, 17, 0, 34, 59, 901, DateTimeKind.Local).AddTicks(6803), null, "test3@test.com", false, "15330306012", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hasan", "TEST3@TEST.COM", "TEST3@TEST.COM", "AQAAAAEAACcQAAAAENFE1/n5P/TKPuwvOPzWnQ0fbQM/rmbPTL4ydiZ98kYdy0VvAsKJCj5xgHjTGyvsIg==", "+905771017033", false, "Politician", null, "F3ITY8FY8VBF3T4GDWXEAULMFHZD3NRF", 1, "Kaya", new Guid("4cf4c6bb-1f2c-40f2-b906-4ef490cea7cb"), false, null, "test3@test.com" },
                    { new Guid("1520cfff-92f8-4576-95c9-0ae3c05a07d2"), 0, null, new DateTime(2062, 7, 1, 0, 34, 59, 905, DateTimeKind.Local).AddTicks(8224), null, new Guid("7a2b9ecd-1044-446b-b293-9f3767c6ad6a"), "624df512-63da-44f2-a6d4-95e033d6e9d1", new DateTime(2023, 5, 17, 0, 34, 59, 905, DateTimeKind.Local).AddTicks(8222), null, "ali.yildirim@google.com", false, "72782686680", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ali", "ALI.YILDIRIM@GOOGLE.COM", "ALI.YILDIRIM@GOOGLE.COM", "AQAAAAEAACcQAAAAEHz4nqHsy0odqFlhnrm8Ue5B+TtoUIE3Q5YvNiLO5wUsrOfi5JlY46tcxVTjIj4cgg==", "+905991113398", false, "Bricklayer", null, "YA7Y9UOJUIL32ZH73XAJRPMW551CWUOS", 3, "Yıldırım", new Guid("b9a511fe-3dd9-402d-aba3-a1f9ebf46b73"), false, null, "ali.yildirim@google.com" },
                    { new Guid("3b571f01-58e0-484a-94cb-c69063750a50"), 0, null, new DateTime(2075, 12, 17, 0, 34, 59, 924, DateTimeKind.Local).AddTicks(2227), null, new Guid("89cd36d8-0b37-4a66-b124-29054b6d4e14"), "374ba8f5-06de-4a1c-a6c5-1d277b65813a", new DateTime(2023, 5, 17, 0, 34, 59, 924, DateTimeKind.Local).AddTicks(2225), null, "ismail.sahin@outlook.com", false, "12478440202", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.SAHIN@OUTLOOK.COM", "ISMAIL.SAHIN@OUTLOOK.COM", "AQAAAAEAACcQAAAAEIcNgriekt+P2ZSoBMWRQBoWNS6B4j9/GplB6PrBwfki5RMvyhktBvvbDM2wrMjU3Q==", "+905143796797", false, "Nurse", null, "BDRST5376D3U12YEUFYHXSTCKV362DZF", 3, "Şahin", new Guid("55c48b18-c126-4d1a-b969-95d84ac6ed4e"), false, null, "ismail.sahin@outlook.com" },
                    { new Guid("412535c5-48e3-4620-9125-2f5bdf796348"), 0, null, new DateTime(2061, 11, 20, 0, 34, 59, 917, DateTimeKind.Local).AddTicks(7142), null, new Guid("233462cc-903b-4d37-bd29-478db6148421"), "3746212b-5a82-4669-bdc5-7456ca51fa63", new DateTime(2023, 5, 17, 0, 34, 59, 917, DateTimeKind.Local).AddTicks(7142), null, "hasan.yildirim@hotmail.com", false, "71687216626", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hasan", "HASAN.YILDIRIM@HOTMAIL.COM", "HASAN.YILDIRIM@HOTMAIL.COM", "AQAAAAEAACcQAAAAEItlAy6pW5TtZXunLhsafatJqU2vcNrsY4dPCTcKvfz805hI8ZVTgFwB429RzqXLnA==", "+905391162957", false, "Secretary", null, "GX64BNZHG26TO7O3I8AJMTSSV3VTEH4F", 3, "Yıldırım", new Guid("b3cab202-d979-40f2-b4fe-7348cd5a0ef6"), false, null, "hasan.yildirim@hotmail.com" },
                    { new Guid("45adbfa8-da94-4af8-bf96-ab5624f74fc5"), 0, null, new DateTime(2050, 3, 7, 0, 34, 59, 908, DateTimeKind.Local).AddTicks(5030), null, new Guid("78922a67-596c-4b9a-b6d6-8d3eca5e87a2"), "9577c43e-09f3-4200-b87b-3790bf93b14f", new DateTime(2023, 5, 17, 0, 34, 59, 908, DateTimeKind.Local).AddTicks(5029), null, "ibrahim.sahin@yahoo.com", false, "28640074236", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "IBRAHIM.SAHIN@YAHOO.COM", "IBRAHIM.SAHIN@YAHOO.COM", "AQAAAAEAACcQAAAAEMtCJ7llsfuprlKGArcDZ/udD+Phlg78ZeJfpwZoV0M9gA+Q3+ptxnOPEuIOiOl/Rg==", "+905157262808", false, "Scientist", null, "J3AXJCM7O91T5M6K2RUD7M9YKANLMC76", 3, "Şahin", new Guid("abad928d-739c-49a9-94d7-84845615da49"), false, null, "ibrahim.sahin@yahoo.com" },
                    { new Guid("46f332ff-fdf1-4f7c-bbd5-c725aa7ef15a"), 0, null, new DateTime(2048, 5, 3, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(628), null, new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), "48d15b71-b957-4b49-aa81-d361e84faef2", new DateTime(2023, 5, 17, 0, 34, 59, 899, DateTimeKind.Local).AddTicks(625), null, "test1@test.com", false, "61843221434", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mehmet", "TEST1@TEST.COM", "TEST1@TEST.COM", "AQAAAAEAACcQAAAAECCzg+Tf/bUB1YMHx+KL2ZaRY5bdEqqYmgglLlv7GBPubM/b2BjeWu+gzVtjFTKlmQ==", "+905473288139", false, "Busdriver", null, "QFL3NEOUUBZ8OPLEBYW8VL100ZU5YQLH", 1, "Demir", new Guid("064d1451-e5bb-48da-be5c-ff0f9b68a163"), false, null, "test1@test.com" },
                    { new Guid("4a765081-8035-4753-be13-1375263a0923"), 0, null, new DateTime(2061, 5, 11, 0, 34, 59, 904, DateTimeKind.Local).AddTicks(5186), null, new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), "fa7bbdba-48f5-42fa-be94-5b995ad24d02", new DateTime(2023, 5, 17, 0, 34, 59, 904, DateTimeKind.Local).AddTicks(5183), null, "test5@test.com", false, "34276415596", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ali", "TEST5@TEST.COM", "TEST5@TEST.COM", "AQAAAAEAACcQAAAAEEUDsl3GobI91f/0VfgthXSIOFUh+7GYU8IpQWS9/GETmTMkL00GCJhb4elArpgvpQ==", "+905866094837", false, "Chef/Cook", null, "73LCJ1SX9EBCQ47NJ30Z0A8VDNQ8KWCO", 1, "Kaya", new Guid("bce397a2-020b-49cf-aa14-be41d483fc6b"), false, null, "test5@test.com" },
                    { new Guid("5805b2a8-dd08-4576-b800-0e8b140f4f64"), 0, null, new DateTime(2073, 12, 2, 0, 34, 59, 909, DateTimeKind.Local).AddTicks(8673), null, new Guid("cc30e96f-822b-46be-bd15-bef0ff51a441"), "ed1bebcd-3794-49d8-9523-6bea14afe1cf", new DateTime(2023, 5, 17, 0, 34, 59, 909, DateTimeKind.Local).AddTicks(8671), null, "huseyin.aydin@microsoft.com", false, "33107128780", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "HUSEYIN.AYDIN@MICROSOFT.COM", "HUSEYIN.AYDIN@MICROSOFT.COM", "AQAAAAEAACcQAAAAEHiOTsRVXZvXM3huv+BU+YBdZuAAgZrRYf3BRDgR9zRWIkvqxgbPxleRsRevqmE+Dg==", "+905465471456", false, "Lawyer", null, "9CLTJER93NZHQHRXC8D44EPCC55Q980G", 3, "Aydın", new Guid("64b64ea5-fbf2-4ea0-9edd-b2f74271119e"), false, null, "huseyin.aydin@microsoft.com" },
                    { new Guid("5e17b4af-3192-40fe-bc54-9c3888ce08ae"), 0, null, new DateTime(2050, 10, 27, 0, 34, 59, 903, DateTimeKind.Local).AddTicks(1085), null, new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), "1d0aeff4-d011-4345-b752-c210657d08d6", new DateTime(2023, 5, 17, 0, 34, 59, 903, DateTimeKind.Local).AddTicks(1075), null, "test4@test.com", false, "33806463496", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "TEST4@TEST.COM", "TEST4@TEST.COM", "AQAAAAEAACcQAAAAED3qMnUZbQVyz7nGJ7e0HLhYu19T7Jp7U6ET4+l8XFbyY1HdxeA6dAYSGZ0Bh7dU9A==", "+905602685286", false, "Postman", null, "JT981WFGWPXLD44KDCM9WLOQT6GHKIDC", 1, "Yılmaz", new Guid("7f10cecd-19f8-472c-be90-471964bcc6ac"), false, null, "test4@test.com" },
                    { new Guid("5ee3dcdf-bb08-4f5c-8c74-1c92997366b0"), 0, null, new DateTime(2075, 12, 16, 0, 34, 59, 907, DateTimeKind.Local).AddTicks(1919), null, new Guid("f3d8e4e1-6ca9-4b67-8245-8d57cbf14ff0"), "330e8741-0802-48c6-8c4a-4272d72905e2", new DateTime(2023, 5, 17, 0, 34, 59, 907, DateTimeKind.Local).AddTicks(1918), null, "ahmet.sahin@hotmail.com", false, "67367787562", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "AHMET.SAHIN@HOTMAIL.COM", "AHMET.SAHIN@HOTMAIL.COM", "AQAAAAEAACcQAAAAEMsSqnppuWH28kY6T/OpUUmPxt+01MwSbpqwKUjfvru5sidQEQ3pPDUMONia3MpHbg==", "+905386076353", false, "Travel agent", null, "83FBQ5Z5KM1I9L89CMZ0BYL4MSUF6TQQ", 3, "Şahin", new Guid("96e918cf-15a7-4382-8fdd-d7364836e9a5"), false, null, "ahmet.sahin@hotmail.com" },
                    { new Guid("75312d02-52df-400d-a4d7-1480a81117e4"), 0, null, new DateTime(2050, 11, 22, 0, 34, 59, 915, DateTimeKind.Local).AddTicks(1017), null, new Guid("520d2102-5003-4ff2-9f84-ef150238754b"), "df41ded8-b711-4429-81a8-cf9c26c3399d", new DateTime(2023, 5, 17, 0, 34, 59, 915, DateTimeKind.Local).AddTicks(1016), null, "mustafa.ozdemir@yandex.com", false, "75865507384", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mustafa", "MUSTAFA.OZDEMIR@YANDEX.COM", "MUSTAFA.OZDEMIR@YANDEX.COM", "AQAAAAEAACcQAAAAEAjPAyXAhG8rHBGTnkk9aML+zuYwi5QiE5a/Kfxo/48gbsJA8EEv5ATRdbchIT+o8A==", "+905655511861", false, "Lecturer", null, "INFZH0P1Q354JBLJ3ZJOA17JHTYP9GE0", 3, "Özdemir", new Guid("cb87c2bc-3535-4457-acdf-451822a39b8b"), false, null, "mustafa.ozdemir@yandex.com" },
                    { new Guid("96a90859-405e-4e88-a3ea-2b0c2ca8f94b"), 0, null, new DateTime(2061, 7, 6, 0, 34, 59, 921, DateTimeKind.Local).AddTicks(6907), null, new Guid("3df51d67-ed70-4f0a-81a9-22ba7e8569e2"), "877f7b36-f683-4018-b1cc-9ad954421e74", new DateTime(2023, 5, 17, 0, 34, 59, 921, DateTimeKind.Local).AddTicks(6907), null, "ibrahim.yildiz@hotmail.com", false, "48553636482", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "IBRAHIM.YILDIZ@HOTMAIL.COM", "IBRAHIM.YILDIZ@HOTMAIL.COM", "AQAAAAEAACcQAAAAEOeIkDh9YDjUUD7BkUYdR97S0i65VsC6i6iJNT5aVMgduua5LaE07LP0mSOrkfim0g==", "+905994651140", false, "Waiter/Waitress", null, "40ZESSBS41OMK9WY8XVIF13JGC0VGJRX", 3, "Yıldız", new Guid("ecbcbf38-03c2-42bb-9d27-ff312a3c7448"), false, null, "ibrahim.yildiz@hotmail.com" },
                    { new Guid("99e55d11-0316-47e4-9bd6-a225ceb871e7"), 0, null, new DateTime(2072, 11, 2, 0, 34, 59, 916, DateTimeKind.Local).AddTicks(4312), null, new Guid("56d921c6-d025-4332-abd1-42508a1d2f82"), "3a7ef7b9-1abd-49fb-8fef-efee607c693e", new DateTime(2023, 5, 17, 0, 34, 59, 916, DateTimeKind.Local).AddTicks(4311), null, "yusuf.kaya@google.com", false, "31787147626", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Yusuf", "YUSUF.KAYA@GOOGLE.COM", "YUSUF.KAYA@GOOGLE.COM", "AQAAAAEAACcQAAAAEHuwwq4TtlfxwVN1zZW7Bkjj6UFQ+EShPP6b8Op9gnh+JevqGBb4mw3acfjJ8R3z4w==", "+905690272732", false, "Real estate agent", null, "IGUTQRM5NEGEOG55F7NYM6IAOHOOCTMS", 3, "Kaya", new Guid("8de4166f-2f22-4aa5-9716-24ffc09718a9"), false, null, "yusuf.kaya@google.com" },
                    { new Guid("bb94dce7-2911-4bad-aa49-7aec6214a6be"), 0, null, new DateTime(2075, 11, 10, 0, 34, 59, 911, DateTimeKind.Local).AddTicks(1729), null, new Guid("e11d302b-cda5-45ce-9c7b-b29a5aad2557"), "953198ec-637a-4621-bb73-348a76393b6f", new DateTime(2023, 5, 17, 0, 34, 59, 911, DateTimeKind.Local).AddTicks(1727), null, "mustafa.yilmaz@microsoft.com", false, "65667742632", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mustafa", "MUSTAFA.YILMAZ@MICROSOFT.COM", "MUSTAFA.YILMAZ@MICROSOFT.COM", "AQAAAAEAACcQAAAAEMk05JGOcLKLbqdZXzq9STJwgaAtkVK+lUnT9y1ICyDC/ptEHpB3/Tt9Tr/KcUnGcA==", "+905133096206", false, "Hairdresser", null, "ANMP1HU0MUKWV5TCFFR90DMUEFPP52RO", 3, "Yılmaz", new Guid("20c3ca58-029c-4e74-a20f-16f99783458a"), false, null, "mustafa.yilmaz@microsoft.com" },
                    { new Guid("bd1938f0-69ea-4d27-a717-3b7e6116da62"), 0, null, new DateTime(2045, 5, 13, 0, 34, 59, 900, DateTimeKind.Local).AddTicks(4065), null, new Guid("9d0d601f-8501-4d22-9393-4ffc4db445b2"), "fd12fbf6-f525-4594-be01-d0ed6dbdb6a7", new DateTime(2023, 5, 17, 0, 34, 59, 900, DateTimeKind.Local).AddTicks(4063), null, "test2@test.com", false, "17440004314", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "TEST2@TEST.COM", "TEST2@TEST.COM", "AQAAAAEAACcQAAAAEPza30mm7FSqxwO+q+0w7jedgXiGa8LnQ7nMMAawYPAKYvByD36VinNCi1szDtHj7w==", "+905167166531", false, "Painter", null, "KCLHNOS98YK8CDWNVV0IGV5Y80KTLP2G", 1, "Yıldırım", new Guid("cc345999-b94e-4068-b3ba-960a47950475"), false, null, "test2@test.com" },
                    { new Guid("e912b1fd-f73f-4fcb-8b03-8b1a2075560a"), 0, null, new DateTime(2046, 5, 5, 0, 34, 59, 912, DateTimeKind.Local).AddTicks(4896), null, new Guid("6c16c6d9-f095-428f-9ef5-05834c7c8abf"), "441c1f84-b37d-4326-8706-a6cf484c07a0", new DateTime(2023, 5, 17, 0, 34, 59, 912, DateTimeKind.Local).AddTicks(4895), null, "yusuf.demir@yandex.com", false, "83488377086", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Yusuf", "YUSUF.DEMIR@YANDEX.COM", "YUSUF.DEMIR@YANDEX.COM", "AQAAAAEAACcQAAAAEEb7CZpi030itC0ncO0DIAXJyNAa8StkmujeC4elHpNnsQyoLNlYqIqHxH0DlmNGuw==", "+905881700797", false, "Veterinary doctor(Vet)", null, "TT27KBBVKGE3OVSS8ORFIBSJIPKMDW3L", 3, "Demir", new Guid("52ab7740-0f7c-4076-b4c5-d094e8c4b195"), false, null, "yusuf.demir@yandex.com" },
                    { new Guid("fcefef7f-2e6f-49e0-8c6a-f53855bf66c2"), 0, null, new DateTime(2073, 4, 26, 0, 34, 59, 922, DateTimeKind.Local).AddTicks(9861), null, new Guid("59e5edfb-77fc-4727-a735-358ef9b4736b"), "cf011232-2aaa-4a48-a505-091ebd8f5dbe", new DateTime(2023, 5, 17, 0, 34, 59, 922, DateTimeKind.Local).AddTicks(9860), null, "hasan.sahin@yandex.com", false, "57557756614", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hasan", "HASAN.SAHIN@YANDEX.COM", "HASAN.SAHIN@YANDEX.COM", "AQAAAAEAACcQAAAAEL+dBIoIojqU/kbFQWPwjwnYwy4RIos6dcu5/kZeFVFV8ziFx1ss0W0W7kyhGu4YHg==", "+905298470293", false, "Veterinary doctor(Vet)", null, "S0VX7G7GWSQGBU78MTTY17KI3XI4M2CO", 3, "Şahin", new Guid("a8bb6d7d-405e-4469-919c-25b70d3021be"), false, null, "hasan.sahin@yandex.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("05cdd321-a4f3-47ed-b856-229e74ad3663") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("0adadabf-987a-4c45-9efb-f0f9db8edcf9") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("0ed03d90-2e96-440e-9779-6beebc540913") },
                    { new Guid("3b6233e4-8070-46ed-a3b8-cd59171cf3ac"), new Guid("0fa18ebe-8a8b-4674-bb81-fbf7e0f793da") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("1520cfff-92f8-4576-95c9-0ae3c05a07d2") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("3b571f01-58e0-484a-94cb-c69063750a50") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("412535c5-48e3-4620-9125-2f5bdf796348") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("45adbfa8-da94-4af8-bf96-ab5624f74fc5") },
                    { new Guid("3b6233e4-8070-46ed-a3b8-cd59171cf3ac"), new Guid("46f332ff-fdf1-4f7c-bbd5-c725aa7ef15a") },
                    { new Guid("3b6233e4-8070-46ed-a3b8-cd59171cf3ac"), new Guid("4a765081-8035-4753-be13-1375263a0923") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("5805b2a8-dd08-4576-b800-0e8b140f4f64") },
                    { new Guid("3b6233e4-8070-46ed-a3b8-cd59171cf3ac"), new Guid("5e17b4af-3192-40fe-bc54-9c3888ce08ae") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("5ee3dcdf-bb08-4f5c-8c74-1c92997366b0") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("75312d02-52df-400d-a4d7-1480a81117e4") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("96a90859-405e-4e88-a3ea-2b0c2ca8f94b") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("99e55d11-0316-47e4-9bd6-a225ceb871e7") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("bb94dce7-2911-4bad-aa49-7aec6214a6be") },
                    { new Guid("3b6233e4-8070-46ed-a3b8-cd59171cf3ac"), new Guid("bd1938f0-69ea-4d27-a717-3b7e6116da62") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("e912b1fd-f73f-4fcb-8b03-8b1a2075560a") },
                    { new Guid("d6edbe69-da4f-48cc-b878-9dd096b99219"), new Guid("fcefef7f-2e6f-49e0-8c6a-f53855bf66c2") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AppUserId",
                table: "Addresses",
                column: "AppUserId",
                unique: true);

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
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

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
                name: "IX_CashAdvances_DirectorId",
                table: "CashAdvances",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SectorId",
                table: "Companies",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseById",
                table: "Expenses",
                column: "ExpenseById");

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
                name: "Addresses");

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
                name: "Titles");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Sectors");
        }
    }
}
