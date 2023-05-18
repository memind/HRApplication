using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKApplication.Persistance.Migrations
{
    public partial class LeaveFix : Migration
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
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaveType = table.Column<int>(type: "int", nullable: false),
                    LeaveStatus = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), "R94AHK0K5LRAAJGQFVR6EQU72XK817C6", "Company Administrator", "COMPANY ADMINISTRATOR" },
                    { new Guid("2be9301f-d247-4fdb-bfa2-82a5e21ce964"), "ZMZYFR942QAU5EJ5LN7Z6SXT9T6IBUHK", "Personal", "PERSONAL" },
                    { new Guid("5f8b24f1-f442-4dca-a36d-9a78634f75bd"), "4PPEH4SD4PE8I586BDRS6093CFFILVD7", "Site Administrator", "SITE ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("015e1818-e616-485e-9082-15d90062615d"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2257), null, "Tekstil, Hazır Giyim, Deri", 1, null },
                    { new Guid("0be39fa0-6b86-4d6f-b033-8236560f829a"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2195), null, "Elektrik ve Elektronik", 1, null },
                    { new Guid("0e71cfb0-346b-491e-b2f4-63713eedbe3d"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2242), null, "İş ve Yönetimi", 1, null },
                    { new Guid("1564575c-c06a-4744-9326-685f0e6de2c9"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2258), null, "Ticaret (Satış ve Pazarlama)", 1, null },
                    { new Guid("1d5d6847-4782-4d69-8bbe-2c72bcaa2077"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2263), null, "Ulaştırma, Lojistik ve Haberleşme", 1, null },
                    { new Guid("20205ffe-69a5-423f-b417-2ffd5c137042"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2245), null, "Kültür, Sanat ve Tasarım", 1, null },
                    { new Guid("27269331-433b-4167-8f32-250e61c00811"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2211), null, "Enerji", 1, null },
                    { new Guid("42e4a4c4-a6bd-4b5d-9bbf-d5534f531fdb"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2193), null, "Cam, Çimento ve ToprakÇevre", 1, null },
                    { new Guid("48fd088e-aab6-4f7b-84fd-53c26321eeea"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2252), null, "Otomotiv", 1, null },
                    { new Guid("60a15090-50ab-4d2b-94db-51bc91629445"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2215), null, "İnşaat", 1, null },
                    { new Guid("678d48fe-73cf-46fa-b37d-c65524de887d"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2214), null, "Gıda", 1, null },
                    { new Guid("7770a2c0-a546-4451-b30b-650bff8507e9"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2251), null, "Metal", 1, null },
                    { new Guid("7ff36913-28e2-4b53-bbee-0c78bcee7af3"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2255), null, "Spor ve Rekreasyon", 1, null },
                    { new Guid("8d6c06ba-5221-4752-bb33-3cb6c44422c3"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2247), null, "Maden", 1, null },
                    { new Guid("96a958c8-08f3-4999-a918-dd6f4ca86cb7"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2172), null, "Ahşap Teknolojisi", 1, null },
                    { new Guid("9f2fedc5-b638-4b3d-baa4-c2e4c2c79f4f"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2261), null, "Toplumsal ve Kişisel Hizmetler", 1, null },
                    { new Guid("a3a76b2b-1ada-474a-b93a-8f3c6e4fd063"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2213), null, "Finans", 1, null },
                    { new Guid("c687ea68-b2e1-4143-abca-c1bce580f520"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2191), null, "Bilişim", 1, null },
                    { new Guid("d270bab8-b63f-4d10-a07f-5fcfa66e11e9"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2253), null, "Sağlık ve Sosyal Hizmetler", 1, null },
                    { new Guid("da9bd61f-eba4-47b7-a3c6-0cfc59104b3d"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2244), null, "Kimya, Petrol, Lastik ve PLastik", 1, null },
                    { new Guid("db7a83c1-172b-4430-ac99-7c5d5c41c84f"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2250), null, "Medya, İletişim ve Yayıncılık", 1, null },
                    { new Guid("ddf44cbd-7b5d-4e53-a599-ec48c3b2da7e"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2256), null, "Tarım, Avcılık ve Balıkçılık", 1, null },
                    { new Guid("deb73b2f-23a6-4f17-93e5-2b262936dd14"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2194), null, "Eğitim", 1, null },
                    { new Guid("dfd3c73f-181b-4992-9b6a-50895df79b38"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2262), null, "Turizm, Konaklama, Yiyecek-İçecek Hizmetleri", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "NumberOfEmployees", "PhoneNumber", "SectorId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("099bb6d7-d2f2-4533-a58c-6288028320f1"), new DateTime(2023, 5, 18, 1, 0, 3, 83, DateTimeKind.Local).AddTicks(8909), null, "info@ozturkkooperatifsirketi.com", "Öztürk Kooperatif Şirketi", 7, "+905736614375", new Guid("7ff36913-28e2-4b53-bbee-0c78bcee7af3"), 3, null },
                    { new Guid("0ae4bb3a-0db1-4876-ba90-9f5c4c670202"), new DateTime(2023, 5, 18, 1, 0, 3, 75, DateTimeKind.Local).AddTicks(8507), null, "info@yildizkooperatifsirketi.com", "Yıldız Kooperatif Şirketi", 49, "+905385727895", new Guid("7770a2c0-a546-4451-b30b-650bff8507e9"), 3, null },
                    { new Guid("2970e3e7-cd55-4705-9a95-791a4bf066f3"), new DateTime(2023, 5, 18, 1, 0, 3, 74, DateTimeKind.Local).AddTicks(5049), null, "info@ozturklimitedsirketi.com", "Öztürk Limited Şirketi", 7, "+905576630704", new Guid("20205ffe-69a5-423f-b417-2ffd5c137042"), 3, null },
                    { new Guid("4bfa1ec7-5c8e-4a36-bc8c-013e68170fde"), new DateTime(2023, 5, 18, 1, 0, 3, 81, DateTimeKind.Local).AddTicks(2091), null, "info@ozdemirkomanditsirketi.com", "Özdemir Komandit Şirketi", 59, "+905580332270", new Guid("7770a2c0-a546-4451-b30b-650bff8507e9"), 3, null },
                    { new Guid("5aa21363-8cec-40ed-b633-d153937cf76e"), new DateTime(2023, 5, 18, 1, 0, 3, 78, DateTimeKind.Local).AddTicks(5491), null, "info@yilmazanonimsirketi.com", "Yılmaz Anonim Şirketi", 77, "+905325644431", new Guid("db7a83c1-172b-4430-ac99-7c5d5c41c84f"), 3, null },
                    { new Guid("648e0ccb-b3a6-4f90-9bf7-60631ab1bbe9"), new DateTime(2023, 5, 18, 1, 0, 3, 82, DateTimeKind.Local).AddTicks(5559), null, "info@celikkomanditsirketi.com", "Çelik Komandit Şirketi", 1, "+905920411644", new Guid("db7a83c1-172b-4430-ac99-7c5d5c41c84f"), 3, null },
                    { new Guid("7908e0a1-1c1f-4579-8df8-021ea2fdf3f7"), new DateTime(2023, 5, 18, 1, 0, 3, 69, DateTimeKind.Local).AddTicks(1248), null, "info@aydinkollektifsirketi.com", "Aydın Kollektif Şirketi", 59, "+905857081633", new Guid("dfd3c73f-181b-4992-9b6a-50895df79b38"), 3, null },
                    { new Guid("827308ec-02b9-45ff-afa1-c13c4bd3b29d"), new DateTime(2023, 5, 18, 1, 0, 3, 73, DateTimeKind.Local).AddTicks(1638), null, "info@ozturkkomanditsirketi.com", "Öztürk Komandit Şirketi", 30, "+905388392372", new Guid("60a15090-50ab-4d2b-94db-51bc91629445"), 3, null },
                    { new Guid("9d31d78f-d82f-4fa1-82a7-d32b78cf85b8"), new DateTime(2023, 5, 18, 1, 0, 3, 77, DateTimeKind.Local).AddTicks(2016), null, "info@yildirimkooperatifsirketi.com", "Yıldırım Kooperatif Şirketi", 27, "+905969043331", new Guid("96a958c8-08f3-4999-a918-dd6f4ca86cb7"), 3, null },
                    { new Guid("d043db2b-ce58-439c-9788-875da71ea18b"), new DateTime(2023, 5, 18, 1, 0, 3, 70, DateTimeKind.Local).AddTicks(4799), null, "info@yildirimkooperatifsirketi.com", "Yıldırım Kooperatif Şirketi", 51, "+905108489157", new Guid("1d5d6847-4782-4d69-8bbe-2c72bcaa2077"), 3, null },
                    { new Guid("d873d817-3756-43d4-a9cb-8c530e75bc95"), new DateTime(2023, 5, 18, 1, 0, 3, 67, DateTimeKind.Local).AddTicks(7462), null, "info@kayalimitedsirketi.com", "Kaya Limited Şirketi", 96, "+905352917911", new Guid("42e4a4c4-a6bd-4b5d-9bbf-d5534f531fdb"), 3, null },
                    { new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2388), null, "ikapp@ikapp.com", "IKApp A.Ş.", 5, "+905206899355", new Guid("c687ea68-b2e1-4143-abca-c1bce580f520"), 1, null },
                    { new Guid("e4914c49-d66a-4bc1-881a-cd997b2a0b60"), new DateTime(2023, 5, 18, 1, 0, 3, 65, DateTimeKind.Local).AddTicks(537), null, "info@sahinkollektifsirketi.com", "Şahin Kollektif Şirketi", 68, "+905245468674", new Guid("7ff36913-28e2-4b53-bbee-0c78bcee7af3"), 3, null },
                    { new Guid("e7f49565-b1d0-44d0-b7a3-35423dac89c3"), new DateTime(2023, 5, 18, 1, 0, 3, 71, DateTimeKind.Local).AddTicks(8168), null, "info@kayakollektifsirketi.com", "Kaya Kollektif Şirketi", 13, "+905711581522", new Guid("015e1818-e616-485e-9082-15d90062615d"), 3, null },
                    { new Guid("f2f3ec6a-7ffd-42b3-a903-840c158611c4"), new DateTime(2023, 5, 18, 1, 0, 3, 66, DateTimeKind.Local).AddTicks(3979), null, "info@ozturklimitedsirketi.com", "Öztürk Limited Şirketi", 60, "+905214601459", new Guid("015e1818-e616-485e-9082-15d90062615d"), 3, null },
                    { new Guid("f31dccff-80d9-4948-bd3a-74fadb83720d"), new DateTime(2023, 5, 18, 1, 0, 3, 79, DateTimeKind.Local).AddTicks(8713), null, "info@aydinkomanditsirketi.com", "Aydın Komandit Şirketi", 33, "+905790423792", new Guid("dfd3c73f-181b-4992-9b6a-50895df79b38"), 3, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("03e5fed3-47d5-4078-bd20-6d15c802d105"), new Guid("2970e3e7-cd55-4705-9a95-791a4bf066f3"), new DateTime(2023, 5, 18, 1, 0, 3, 74, DateTimeKind.Local).AddTicks(5058), null, "Principal", 1, null },
                    { new Guid("07d2abfb-5e98-47de-bac8-3cba82d1e29c"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2487), null, "Registrar", 1, null },
                    { new Guid("0b772502-1301-41cc-ad01-267974474ff3"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2431), null, "Credit Analyst", 1, null },
                    { new Guid("13027ed5-9dcf-4fd9-a93e-3106007ef5c4"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2424), null, "Marketing Analyst", 1, null },
                    { new Guid("1473c539-fd2f-4396-8bf2-a367608571fc"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2449), null, "Full Stack Developer", 1, null },
                    { new Guid("207766a1-6b84-4484-97c9-54a440aa0d24"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2499), null, "Safety Director", 1, null },
                    { new Guid("2302c24a-faa4-4714-bbaa-731fd88ffda6"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2499), null, "Construction Foreman", 1, null },
                    { new Guid("23177dfe-b7e4-44bc-95b9-a75b89a92fa4"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2494), null, "Front Desk Associate", 1, null },
                    { new Guid("23645226-b1f3-4804-ab2c-a53408d77752"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2447), null, "Director of Information Security", 1, null },
                    { new Guid("29938c30-4e1f-4972-9560-e032c3953b38"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2489), null, "Teacher", 1, null },
                    { new Guid("2b4c3fa4-5f01-4e6c-b87c-65a62269adbe"), new Guid("f2f3ec6a-7ffd-42b3-a903-840c158611c4"), new DateTime(2023, 5, 18, 1, 0, 3, 66, DateTimeKind.Local).AddTicks(3982), null, "Customer Success Manager", 1, null },
                    { new Guid("3af851ee-3e53-4516-8aef-e263023d2b4a"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2452), null, "Other Industries:", 1, null },
                    { new Guid("3c7ae8e9-c514-40c5-8492-3ba211c3724e"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2419), null, "CMO (Chief Marketing Officer)", 1, null },
                    { new Guid("402350c8-a743-4d6d-953c-542b692acd70"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2484), null, "Administrator", 1, null },
                    { new Guid("42d3ec8b-4ad9-49a7-a19b-9f5a04c14bc6"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2488), null, "School Counselor", 1, null },
                    { new Guid("44e70f24-d2c9-4354-abd8-82359b4aed23"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2482), null, "Clinical Laboratory Technician", 1, null },
                    { new Guid("4555b604-03be-4be4-a3c5-470cad49d100"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2434), null, "VP of Client Services", 1, null },
                    { new Guid("4a1b416e-63ee-48ac-b84c-469aa6ce011f"), new Guid("827308ec-02b9-45ff-afa1-c13c4bd3b29d"), new DateTime(2023, 5, 18, 1, 0, 3, 73, DateTimeKind.Local).AddTicks(1641), null, "HR Analyst", 1, null },
                    { new Guid("4b624623-1d39-4917-9203-77886df1b440"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2416), null, "Sales Representative", 1, null },
                    { new Guid("4db51b75-4085-4b89-9509-12d5a71fc5dc"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2492), null, "Guest Services Supervisor", 1, null },
                    { new Guid("4e1bc76a-0c2a-4448-bc3b-51963509660e"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2448), null, "Software Engineer I, II, III", 1, null },
                    { new Guid("5472b47c-fa4c-49eb-a1cf-ffb035ff7367"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2501), null, "Contract Administrator", 1, null },
                    { new Guid("58888d1a-5d00-4aed-a82f-bc4217793dcb"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2441), null, "Director of Business Operations", 1, null },
                    { new Guid("59ec4288-417c-4d45-8f74-d5810b3c38c8"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2481), null, "Nursing Assistant", 1, null },
                    { new Guid("5b7f5887-f552-4303-a278-d5ffd5fcfeec"), new Guid("0ae4bb3a-0db1-4876-ba90-9f5c4c670202"), new DateTime(2023, 5, 18, 1, 0, 3, 75, DateTimeKind.Local).AddTicks(8511), null, "Investment Analyst", 1, null },
                    { new Guid("634b1e83-d3b6-476e-899e-869e66bdf483"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2413), null, "National Sales Director", 1, null },
                    { new Guid("65cc9309-a6df-4254-97dd-10cb838d0fa9"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2449), null, "Systems Administrator", 1, null },
                    { new Guid("67da5305-07e7-442b-b3e4-aa53aaa5efed"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2440), null, "COO (Chief Operating Officer)", 1, null },
                    { new Guid("6a80345e-bc77-4524-8d69-db763c0b00d8"), new Guid("4bfa1ec7-5c8e-4a36-bc8c-013e68170fde"), new DateTime(2023, 5, 18, 1, 0, 3, 81, DateTimeKind.Local).AddTicks(2093), null, "Safety Director", 1, null },
                    { new Guid("6afc3b1d-8911-4b09-a407-308bc5b2f57b"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2491), null, "General Manager", 1, null },
                    { new Guid("6b287ac5-692c-4759-a7bf-410711fbeb65"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2420), null, "Marketing Director", 1, null },
                    { new Guid("6cfc69f1-4c44-486e-8310-e4d5b0ae68cc"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2442), null, "Operations Supervisor", 1, null },
                    { new Guid("772a5d87-2e47-4cef-8fe3-67921fcf72d7"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2423), null, "Sr. Marketing Manager", 1, null },
                    { new Guid("79612f6c-8e8b-41fb-bab9-c5e94756dc4d"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2497), null, "Server/Host/Hostess", 1, null },
                    { new Guid("7965aece-e5d5-41ee-ab3e-27795f44c160"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2415), null, "Regional Sales Manager", 1, null },
                    { new Guid("7cba9505-f45e-4f40-aa38-6edddbee6596"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2498), null, "Hotel Receptionist", 1, null },
                    { new Guid("7cd01411-01c2-408b-8f9d-bd3b0010021c"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2435), null, "Account Manager", 1, null },
                    { new Guid("851282f8-330a-49fe-ae6e-8119dd7cc302"), new Guid("d043db2b-ce58-439c-9788-875da71ea18b"), new DateTime(2023, 5, 18, 1, 0, 3, 70, DateTimeKind.Local).AddTicks(4802), null, "Marketing Director", 1, null },
                    { new Guid("865e972e-c524-44fe-a2ae-5a75537f7081"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2490), null, "Teaching Assistant", 1, null },
                    { new Guid("89fcca6a-ef38-47b8-a2a7-2d22cb64231b"), new Guid("9d31d78f-d82f-4fa1-82a7-d32b78cf85b8"), new DateTime(2023, 5, 18, 1, 0, 3, 77, DateTimeKind.Local).AddTicks(2018), null, "HR Analyst", 1, null },
                    { new Guid("8a3bfc36-0e04-4dff-93ec-05328c542803"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2426), null, "Marketing Coordinator", 1, null },
                    { new Guid("8c3b2926-b3e7-4e76-8ce0-0e3eb413213e"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2427), null, "VP of Finance", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("9103f04a-535f-4e42-838f-7ee8cb05c6eb"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2493), null, "Concierge", 1, null },
                    { new Guid("91374955-eb67-4454-ab68-faadb6f104a4"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2454), null, "Registered Nurse", 1, null },
                    { new Guid("97e1b018-9717-451b-9bf8-bb57f5b1e21b"), new Guid("d873d817-3756-43d4-a9cb-8c530e75bc95"), new DateTime(2023, 5, 18, 1, 0, 3, 67, DateTimeKind.Local).AddTicks(7466), null, "National Sales Director", 1, null },
                    { new Guid("a460ad10-b20b-4031-b826-efb2be095c55"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2485), null, "Principal", 1, null },
                    { new Guid("abedeaf5-b4d4-4393-8ee2-ee9c92402994"), new Guid("648e0ccb-b3a6-4f90-9bf7-60631ab1bbe9"), new DateTime(2023, 5, 18, 1, 0, 3, 82, DateTimeKind.Local).AddTicks(5562), null, "Regional Sales Manager", 1, null },
                    { new Guid("ac639c08-fca3-4498-b89e-6fc6979aac9a"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2430), null, "Investment Analyst", 1, null },
                    { new Guid("acee5d20-55a6-4720-af9e-3f2400a3224f"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2417), null, "Sales Associate", 1, null },
                    { new Guid("acf3540d-f73e-4c25-94bd-6b291120ed89"), new Guid("f31dccff-80d9-4948-bd3a-74fadb83720d"), new DateTime(2023, 5, 18, 1, 0, 3, 79, DateTimeKind.Local).AddTicks(8724), null, "Marketing Director", 1, null },
                    { new Guid("afb82938-963c-4155-a8ed-bd24100b2fad"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2500), null, "Project Manager", 1, null },
                    { new Guid("b084182f-ba5c-4a94-89aa-bcc22abd598f"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2432), null, "Risk Analyst", 1, null },
                    { new Guid("b0ba5296-73fc-49ed-8bb3-9d44cc334afe"), new Guid("e4914c49-d66a-4bc1-881a-cd997b2a0b60"), new DateTime(2023, 5, 18, 1, 0, 3, 65, DateTimeKind.Local).AddTicks(546), null, "Front Desk Associate", 1, null },
                    { new Guid("b454306f-0c24-4183-9c94-94e0bfd22123"), new Guid("e7f49565-b1d0-44d0-b7a3-35423dac89c3"), new DateTime(2023, 5, 18, 1, 0, 3, 71, DateTimeKind.Local).AddTicks(8170), null, "Procurement Director", 1, null },
                    { new Guid("bc410d0b-d29e-45da-9002-02af8fbe2e97"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2451), null, "Data Analyst", 1, null },
                    { new Guid("bcf6f2ad-8b13-49da-a106-85718a144fd4"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2445), null, "Sr. Manager of HR", 1, null },
                    { new Guid("bd39d82d-a1f5-42a5-b748-948cfe098bac"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2455), null, "Pharmacy Technician", 1, null },
                    { new Guid("c996c93c-c630-4e7e-9fdf-ac5ac1796a04"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2483), null, "Occupational Therapy Aide", 1, null },
                    { new Guid("ca201414-b2da-4199-871b-5e0c36d27b14"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2446), null, "HR Analyst", 1, null },
                    { new Guid("cc38d1e7-b71c-4416-8009-4be3231c9967"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2438), null, "Customer Service Representative", 1, null },
                    { new Guid("d1a688f3-f3a2-476c-a202-8560194fc515"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2437), null, "Customer Success Manager", 1, null },
                    { new Guid("d4f39cce-6ff5-4c79-b189-bae6a00031f2"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2409), null, "VP of Sales", 1, null },
                    { new Guid("d4f42393-55fd-431a-aed0-645efa92ec13"), new Guid("099bb6d7-d2f2-4533-a58c-6288028320f1"), new DateTime(2023, 5, 18, 1, 0, 3, 83, DateTimeKind.Local).AddTicks(8969), null, "Hotel Receptionist", 1, null },
                    { new Guid("dcd5905a-957d-420a-bb4a-4ae367d209b7"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2428), null, "Procurement Director", 1, null },
                    { new Guid("e00c6efb-1e83-4a5a-bf67-e1cd0c98c4f4"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2502), null, "Project Appraisal Engineer", 1, null },
                    { new Guid("e2d6ed3e-8c3c-4aff-bfbb-21f6f7ce4400"), new Guid("5aa21363-8cec-40ed-b633-d153937cf76e"), new DateTime(2023, 5, 18, 1, 0, 3, 78, DateTimeKind.Local).AddTicks(5494), null, "Director of Business Operations", 1, null },
                    { new Guid("fb9df43d-f17f-44b9-8662-6792bb04d97d"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2439), null, "Support Specialist", 1, null },
                    { new Guid("fba0323f-088f-40f2-b5ef-061f8ff83f3c"), new Guid("7908e0a1-1c1f-4579-8df8-021ea2fdf3f7"), new DateTime(2023, 5, 18, 1, 0, 3, 69, DateTimeKind.Local).AddTicks(1263), null, "Physical Therapist", 1, null },
                    { new Guid("fd37dd68-61fb-4ef5-a2c4-0ae5e64f3897"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2503), null, "Inspector", 1, null },
                    { new Guid("fea4ca72-5d7b-4174-8691-6cbe3c958c17"), new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2480), null, "Physical Therapist", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "BirthDate", "BloodGroup", "CompanyId", "ConcurrencyStamp", "CreateDate", "DeleteDate", "Email", "EmailConfirmed", "IdentityNumber", "ImagePath", "JobStartDate", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profession", "SecondName", "SecurityStamp", "Status", "Surname", "TitleId", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("0aa33112-9f3b-449c-bf95-2cd9e5d35ffd"), 0, null, new DateTime(2048, 9, 7, 1, 0, 3, 78, DateTimeKind.Local).AddTicks(5499), null, new Guid("5aa21363-8cec-40ed-b633-d153937cf76e"), "2a3d45af-b3e2-421b-9712-ecf345e6922c", new DateTime(2023, 5, 18, 1, 0, 3, 78, DateTimeKind.Local).AddTicks(5498), null, "yusuf.yilmaz@hotmail.com", false, "40675664008", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Yusuf", "YUSUF.YILMAZ@HOTMAIL.COM", "YUSUF.YILMAZ@HOTMAIL.COM", "AQAAAAEAACcQAAAAECYmOcp+uO+YFtt7BAAgftUazaqoTIhWo1xViMnscYx/eecxHgFBTRKBsXGqcNhUjA==", "+905740412513", false, "Factory worker", null, "V7ZN7ZRJRGSPK5D3Y34RZ1HZTYC7VTL5", 3, "Yılmaz", new Guid("e2d6ed3e-8c3c-4aff-bfbb-21f6f7ce4400"), false, null, "yusuf.yilmaz@hotmail.com" },
                    { new Guid("1186c752-86bf-4555-b838-68778c05d56b"), 0, null, new DateTime(2074, 11, 9, 1, 0, 3, 82, DateTimeKind.Local).AddTicks(5567), null, new Guid("648e0ccb-b3a6-4f90-9bf7-60631ab1bbe9"), "dd748eda-d9ca-41d9-8986-a8d564ff084a", new DateTime(2023, 5, 18, 1, 0, 3, 82, DateTimeKind.Local).AddTicks(5566), null, "ahmet.celik@hotmail.com", false, "72538334612", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "AHMET.CELIK@HOTMAIL.COM", "AHMET.CELIK@HOTMAIL.COM", "AQAAAAEAACcQAAAAEJZM3VBd2THU/PVR4AyX/PQMQ02TTgMb9HZh7glURIetF6v/wuQZFrWelPnmu05lRA==", "+905768727755", false, "Mechanic", null, "A6ZUKBH0385JEVJMI5HEHIBKIEU8OMZ1", 3, "Çelik", new Guid("abedeaf5-b4d4-4393-8ee2-ee9c92402994"), false, null, "ahmet.celik@hotmail.com" },
                    { new Guid("2a3864f6-068f-4466-8893-8f1f0d8546e0"), 0, null, new DateTime(2045, 12, 16, 1, 0, 3, 71, DateTimeKind.Local).AddTicks(8175), null, new Guid("e7f49565-b1d0-44d0-b7a3-35423dac89c3"), "1fd11bc1-a6e8-4169-a9a8-ae8ee47d3f8f", new DateTime(2023, 5, 18, 1, 0, 3, 71, DateTimeKind.Local).AddTicks(8174), null, "huseyin.kaya@hotmail.com", false, "47085350552", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "HUSEYIN.KAYA@HOTMAIL.COM", "HUSEYIN.KAYA@HOTMAIL.COM", "AQAAAAEAACcQAAAAEB0EJWKoLTjugt3qXyDc6tCWB/5Mmcraj9gfQnc1Y+gDDLvCO6GM6B3/jRMY95hq3w==", "+905967275034", false, "Farmer", null, "VHWC2S6E8DG8OJR5WJJQHLMYE90XHYHP", 3, "Kaya", new Guid("b454306f-0c24-4183-9c94-94e0bfd22123"), false, null, "huseyin.kaya@hotmail.com" },
                    { new Guid("2fd913b9-b623-4c21-a94b-71c98913eddf"), 0, null, new DateTime(2076, 4, 14, 1, 0, 3, 77, DateTimeKind.Local).AddTicks(2025), null, new Guid("9d31d78f-d82f-4fa1-82a7-d32b78cf85b8"), "f93bbd90-61c5-40df-b6f5-bad2057e7962", new DateTime(2023, 5, 18, 1, 0, 3, 77, DateTimeKind.Local).AddTicks(2024), null, "ibrahim.yildirim@microsoft.com", false, "44565214168", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "IBRAHIM.YILDIRIM@MICROSOFT.COM", "IBRAHIM.YILDIRIM@MICROSOFT.COM", "AQAAAAEAACcQAAAAECaeH0XU+o5WiDivst1b3gutSOxLo3kISYHOGI5DEi279qnv85e82Du94y/Dpye5zA==", "+905906502932", false, "Dentist", null, "1Y0D3ZPJKXCYM0NTMH7M9J6R1EQBRMLP", 3, "Yıldırım", new Guid("89fcca6a-ef38-47b8-a2a7-2d22cb64231b"), false, null, "ibrahim.yildirim@microsoft.com" },
                    { new Guid("481e0a35-3cfa-4aa3-b803-e78c084408b9"), 0, null, new DateTime(2073, 7, 24, 1, 0, 3, 70, DateTimeKind.Local).AddTicks(4812), null, new Guid("d043db2b-ce58-439c-9788-875da71ea18b"), "fad04948-ce86-44ed-9e80-7fd168a351dd", new DateTime(2023, 5, 18, 1, 0, 3, 70, DateTimeKind.Local).AddTicks(4811), null, "huseyin.yildirim@google.com", false, "56845274472", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "HUSEYIN.YILDIRIM@GOOGLE.COM", "HUSEYIN.YILDIRIM@GOOGLE.COM", "AQAAAAEAACcQAAAAECT3QpaiYWLd1mx1DTik4wrSFWkFP4oe4BBMHQSoFNaxx94sQcdMHsV+OfX9HnVt5w==", "+905390232002", false, "Artist", null, "YAXHSADPBAMWQ26QOUAPY34L55YXAZEX", 3, "Yıldırım", new Guid("851282f8-330a-49fe-ae6e-8119dd7cc302"), false, null, "huseyin.yildirim@google.com" },
                    { new Guid("7e311a61-31eb-4365-af8f-59bc515dad10"), 0, null, new DateTime(2051, 7, 28, 1, 0, 3, 67, DateTimeKind.Local).AddTicks(7471), null, new Guid("d873d817-3756-43d4-a9cb-8c530e75bc95"), "de8dcb71-f256-4e45-a3e4-785bc04b58a8", new DateTime(2023, 5, 18, 1, 0, 3, 67, DateTimeKind.Local).AddTicks(7470), null, "ismail.kaya@yahoo.com", false, "55767358444", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.KAYA@YAHOO.COM", "ISMAIL.KAYA@YAHOO.COM", "AQAAAAEAACcQAAAAELAlBKr0BIsKe1IAXdfObacbic/oeGSTB6eGHX+8WDLoDk1bNiDX5KPVmXCv8uRZiA==", "+905123535383", false, "Busdriver", null, "0E5XGVVRRZ0V51FJDSCCFRNQJKK9JTKA", 3, "Kaya", new Guid("97e1b018-9717-451b-9bf8-bb57f5b1e21b"), false, null, "ismail.kaya@yahoo.com" },
                    { new Guid("87694f3c-2a20-4457-9e0b-d3ad364ba23f"), 0, null, new DateTime(2049, 3, 3, 1, 0, 3, 69, DateTimeKind.Local).AddTicks(1268), null, new Guid("7908e0a1-1c1f-4579-8df8-021ea2fdf3f7"), "41902e54-447a-41f5-9032-b0396bf90ec8", new DateTime(2023, 5, 18, 1, 0, 3, 69, DateTimeKind.Local).AddTicks(1267), null, "ibrahim.aydin@hotmail.com", false, "63606564208", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "IBRAHIM.AYDIN@HOTMAIL.COM", "IBRAHIM.AYDIN@HOTMAIL.COM", "AQAAAAEAACcQAAAAEC3tn2vRszuoIU6ne/r1DEcFwWB7IwLG/04YGXbqyKBGJ2L1UI1LO5PEuy8SGhiovQ==", "+905136278958", false, "Doctor", null, "GLJLVSJQLN3DR6RL22B4R7N6ZHJJNWY1", 3, "Aydın", new Guid("fba0323f-088f-40f2-b5ef-061f8ff83f3c"), false, null, "ibrahim.aydin@hotmail.com" },
                    { new Guid("90526a21-967d-496d-a62b-21e0d47b7a32"), 0, null, new DateTime(2061, 9, 23, 1, 0, 3, 81, DateTimeKind.Local).AddTicks(2101), null, new Guid("4bfa1ec7-5c8e-4a36-bc8c-013e68170fde"), "91d48c9f-5002-4b2f-92d8-ff86668bfdf8", new DateTime(2023, 5, 18, 1, 0, 3, 81, DateTimeKind.Local).AddTicks(2100), null, "ismail.ozdemir@google.com", false, "77466637804", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.OZDEMIR@GOOGLE.COM", "ISMAIL.OZDEMIR@GOOGLE.COM", "AQAAAAEAACcQAAAAEN0TKjYYOqSlw6hizf9Jn9O4LFSePaQnjzuUx75D+68J8Mj3GGxW5Ooy9kuzvXQh0w==", "+905677635615", false, "Florist", null, "CWZSXGP4FKQ5IAF8IAFWRWIZFMMB56GS", 3, "Özdemir", new Guid("6a80345e-bc77-4524-8d69-db763c0b00d8"), false, null, "ismail.ozdemir@google.com" },
                    { new Guid("96060ffd-b1e8-45c5-a28e-dbc0eb5e55b5"), 0, null, new DateTime(2072, 10, 4, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2522), null, new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), "17c66b1a-3d3f-4a44-a903-fa9a0fa9bf51", new DateTime(2023, 5, 18, 1, 0, 3, 58, DateTimeKind.Local).AddTicks(2517), null, "test1@test.com", false, "31411415336", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mustafa", "TEST1@TEST.COM", "TEST1@TEST.COM", "AQAAAAEAACcQAAAAEM+62sFIGmvCH8hW6QqKmfvx58Y5JXkCQROT/04vm6sGBROQQXjJRnWE8UlqGBmt2A==", "+905718290040", false, "Businessman", null, "UDYUUQNVIQ7LYGKCOH1PGLT0GTNT2AUM", 1, "Şahin", new Guid("23177dfe-b7e4-44bc-95b9-a75b89a92fa4"), false, null, "test1@test.com" },
                    { new Guid("a45ddfbc-a786-4bde-89cf-f12fe91d4a33"), 0, null, new DateTime(2066, 1, 13, 1, 0, 3, 62, DateTimeKind.Local).AddTicks(3373), null, new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), "363e68b0-6918-4f11-8f0b-0431e0d8883a", new DateTime(2023, 5, 18, 1, 0, 3, 62, DateTimeKind.Local).AddTicks(3372), null, "test4@test.com", false, "66414387694", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hasan", "TEST4@TEST.COM", "TEST4@TEST.COM", "AQAAAAEAACcQAAAAEBYMieb2eXGm+DF6mXoVJYcW+JOQS5IFbbuq8QE+8qo6oHn3LydCCt6glwa8wix6Yw==", "+905712689520", false, "Real estate agent", null, "BCKM84UY3MDYDXF072KBYESTS4J2TB7D", 1, "Çelik", new Guid("13027ed5-9dcf-4fd9-a93e-3106007ef5c4"), false, null, "test4@test.com" },
                    { new Guid("b4b2ad6a-98ee-45bb-9050-2e8e51caf658"), 0, null, new DateTime(2066, 11, 18, 1, 0, 3, 73, DateTimeKind.Local).AddTicks(1645), null, new Guid("827308ec-02b9-45ff-afa1-c13c4bd3b29d"), "bbcd341c-8ec7-4ae0-bad8-53bd17f3a574", new DateTime(2023, 5, 18, 1, 0, 3, 73, DateTimeKind.Local).AddTicks(1644), null, "osman.ozturk@google.com", false, "28004026642", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "OSMAN.OZTURK@GOOGLE.COM", "OSMAN.OZTURK@GOOGLE.COM", "AQAAAAEAACcQAAAAEJJmAvd3nwcyZGRfDJgxfU3kLzPf6aLNnIcI4oBJa60g7KOUtqCN5Y3k1oTycXocCA==", "+905216726664", false, "Architect", null, "NBJTPIJWPCIJHEODQDOVZG8884O3I2LL", 3, "Öztürk", new Guid("4a1b416e-63ee-48ac-b84c-469aa6ce011f"), false, null, "osman.ozturk@google.com" },
                    { new Guid("b513846d-27e7-411d-a1f1-fe5ce90c733e"), 0, null, new DateTime(2067, 5, 21, 1, 0, 3, 65, DateTimeKind.Local).AddTicks(566), null, new Guid("e4914c49-d66a-4bc1-881a-cd997b2a0b60"), "5fe0345d-a90b-47ff-998c-a7f733c797fa", new DateTime(2023, 5, 18, 1, 0, 3, 65, DateTimeKind.Local).AddTicks(564), null, "ismail.sahin@yahoo.com", false, "50515748072", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.SAHIN@YAHOO.COM", "ISMAIL.SAHIN@YAHOO.COM", "AQAAAAEAACcQAAAAEFcUFomenVgFl3zr0lR8V37uwuGAqZL+qub0kqJufkLGlR4FXQrlHAG7P4/oOejgYg==", "+905438569610", false, "Chef/Cook", null, "EOCFWTXDIUHJRRMCA7EF4JYKI98K77HK", 3, "Şahin", new Guid("b0ba5296-73fc-49ed-8bb3-9d44cc334afe"), false, null, "ismail.sahin@yahoo.com" },
                    { new Guid("bca2b50e-373e-44a1-96c0-5745296dc60d"), 0, null, new DateTime(2067, 3, 20, 1, 0, 3, 61, DateTimeKind.Local).AddTicks(21), null, new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), "b229ff8f-94b7-4de9-97e7-bfd88bc303e7", new DateTime(2023, 5, 18, 1, 0, 3, 61, DateTimeKind.Local).AddTicks(19), null, "test3@test.com", false, "41312110698", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ali", "TEST3@TEST.COM", "TEST3@TEST.COM", "AQAAAAEAACcQAAAAECNSuB1L5cq9PF14ZpOTIv0+GdgHaEBcTvvZaqdGUQFUKCNxBQSGMNsNPRxTkGb9jA==", "+905694704105", false, "Electrician", null, "RSC2TCPVNNZ0TVC4MQ9G6BUAB51GE096", 1, "Yılmaz", new Guid("6afc3b1d-8911-4b09-a407-308bc5b2f57b"), false, null, "test3@test.com" },
                    { new Guid("d59c67b4-1ae9-4f14-a911-c6cb61748b59"), 0, null, new DateTime(2070, 4, 16, 1, 0, 3, 79, DateTimeKind.Local).AddTicks(8729), null, new Guid("f31dccff-80d9-4948-bd3a-74fadb83720d"), "e2c9bf0d-f877-4083-956b-b4c7f2452b23", new DateTime(2023, 5, 18, 1, 0, 3, 79, DateTimeKind.Local).AddTicks(8728), null, "mustafa.aydin@yandex.com", false, "61737214146", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mustafa", "MUSTAFA.AYDIN@YANDEX.COM", "MUSTAFA.AYDIN@YANDEX.COM", "AQAAAAEAACcQAAAAEEgJpFp+WgoR45vBhbjND9+1RNgEIZV2IozatRNkJsy/sy+TPs5k8d01zp4krwjRUg==", "+905627138700", false, "Pilot", null, "K0HFYCKQ5FWAJ8IGOLI4IYSGPC1AUDF4", 3, "Aydın", new Guid("acf3540d-f73e-4c25-94bd-6b291120ed89"), false, null, "mustafa.aydin@yandex.com" },
                    { new Guid("e0bde80d-0990-4673-9c78-6ceb588215ac"), 0, null, new DateTime(2070, 10, 1, 1, 0, 3, 75, DateTimeKind.Local).AddTicks(8520), null, new Guid("0ae4bb3a-0db1-4876-ba90-9f5c4c670202"), "bbf264a2-5d9d-4184-aef4-5522fa2af828", new DateTime(2023, 5, 18, 1, 0, 3, 75, DateTimeKind.Local).AddTicks(8519), null, "mustafa.yildiz@microsoft.com", false, "85606437504", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mustafa", "MUSTAFA.YILDIZ@MICROSOFT.COM", "MUSTAFA.YILDIZ@MICROSOFT.COM", "AQAAAAEAACcQAAAAEKKq7zQT9j29lPROJWepZMBXG6DGWEWs5Qb7BfZfDPkte7BEo2jaajkWn7vbvcU7sQ==", "+905296693242", false, "Soldier", null, "8JZ36UBRIHXYDLEQP6PZWCV6GGFYAPC9", 3, "Yıldız", new Guid("5b7f5887-f552-4303-a278-d5ffd5fcfeec"), false, null, "mustafa.yildiz@microsoft.com" },
                    { new Guid("e31519ae-8f00-4a49-8b72-d020f486fc2f"), 0, null, new DateTime(2059, 1, 13, 1, 0, 3, 83, DateTimeKind.Local).AddTicks(8974), null, new Guid("099bb6d7-d2f2-4533-a58c-6288028320f1"), "e81083f5-39d8-4c33-965b-a19ecc0f8ed5", new DateTime(2023, 5, 18, 1, 0, 3, 83, DateTimeKind.Local).AddTicks(8973), null, "mustafa.ozturk@outlook.com", false, "31688857006", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mustafa", "MUSTAFA.OZTURK@OUTLOOK.COM", "MUSTAFA.OZTURK@OUTLOOK.COM", "AQAAAAEAACcQAAAAEM21wOE99TGzuPgQzTnvTc9Ca/Y3emAco3461HiLY2TAsORyix7/RVWTpcLDfI25WA==", "+905970883508", false, "Farmer", null, "R28ER6MG0OI3G0HQF1Y0AUC43G5SX43N", 3, "Öztürk", new Guid("d4f42393-55fd-431a-aed0-645efa92ec13"), false, null, "mustafa.ozturk@outlook.com" },
                    { new Guid("e513d7df-d9de-437f-9585-4421c2a1f5d5"), 0, null, new DateTime(2071, 1, 14, 1, 0, 3, 59, DateTimeKind.Local).AddTicks(6289), null, new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), "ccb5c545-fb79-48f5-82d4-dbe67494f4d3", new DateTime(2023, 5, 18, 1, 0, 3, 59, DateTimeKind.Local).AddTicks(6285), null, "test2@test.com", false, "65217882122", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "TEST2@TEST.COM", "TEST2@TEST.COM", "AQAAAAEAACcQAAAAED3ZDvi/gDSfMZetuVN8Qx9OB6U8zOidPMMspUWMB4wV2109x2HE6DZjnbnsMsAJiw==", "+905696361001", false, "Painter", null, "WC3PZREBFEXGRG98SJZNRIC3EMA9A3EY", 1, "Şahin", new Guid("afb82938-963c-4155-a8ed-bd24100b2fad"), false, null, "test2@test.com" },
                    { new Guid("e741e7bd-e3e7-4a22-be9a-049fdf72758a"), 0, null, new DateTime(2063, 5, 1, 1, 0, 3, 66, DateTimeKind.Local).AddTicks(4012), null, new Guid("f2f3ec6a-7ffd-42b3-a903-840c158611c4"), "879e247d-8334-42a7-a05a-bd4e4550e95d", new DateTime(2023, 5, 18, 1, 0, 3, 66, DateTimeKind.Local).AddTicks(4011), null, "ali.ozturk@yahoo.com", false, "63552687136", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ali", "ALI.OZTURK@YAHOO.COM", "ALI.OZTURK@YAHOO.COM", "AQAAAAEAACcQAAAAED4pC2Iq65FCCLPT3LaVrlCcdtpvYOwuOnWd9aSp+vO7ysfcv448BL7VxiWcliYfTg==", "+905494203784", false, "Teacher", null, "ZHOS2AEIA0Z8KWI70E48BEE8R61HPKH2", 3, "Öztürk", new Guid("2b4c3fa4-5f01-4e6c-b87c-65a62269adbe"), false, null, "ali.ozturk@yahoo.com" },
                    { new Guid("f30271ef-8e25-4b0b-ae87-e028bc8ccfdf"), 0, null, new DateTime(2055, 8, 17, 1, 0, 3, 63, DateTimeKind.Local).AddTicks(6891), null, new Guid("e230f09b-5879-4c23-bd1c-c18468d7b664"), "1f7660e0-9414-4513-bcae-6abb059e24ae", new DateTime(2023, 5, 18, 1, 0, 3, 63, DateTimeKind.Local).AddTicks(6889), null, "test5@test.com", false, "84801066250", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "TEST5@TEST.COM", "TEST5@TEST.COM", "AQAAAAEAACcQAAAAEK0H7B9yShvozJ6mPHTYAMnzKsQGrDuLpsTzLVLdXxvZuLFvE64FgJ+j5trhQ2mgEw==", "+905934070952", false, "Politician", null, "F0KFF5Y4K97XIHYJGF2I6VKW88S06D7F", 1, "Aydın", new Guid("5472b47c-fa4c-49eb-a1cf-ffb035ff7367"), false, null, "test5@test.com" },
                    { new Guid("fe071ded-d5c9-40ea-83b0-5f27f5b2104b"), 0, null, new DateTime(2065, 4, 14, 1, 0, 3, 74, DateTimeKind.Local).AddTicks(5063), null, new Guid("2970e3e7-cd55-4705-9a95-791a4bf066f3"), "d22b75c5-4e4a-424e-88ed-21b94f8ed148", new DateTime(2023, 5, 18, 1, 0, 3, 74, DateTimeKind.Local).AddTicks(5062), null, "yusuf.ozturk@outlook.com", false, "37525402378", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Yusuf", "YUSUF.OZTURK@OUTLOOK.COM", "YUSUF.OZTURK@OUTLOOK.COM", "AQAAAAEAACcQAAAAEG9YLjGdH8m1GjYfSzSG3THdHI16jwMDcuZwTe9Osl3sXEbSx1/xTM5lRuJZbhw4RQ==", "+905211521140", false, "Artist", null, "6U33795VKBAIEIPFZTNT9L5JMP0YJ2AK", 3, "Öztürk", new Guid("03e5fed3-47d5-4078-bd20-6d15c802d105"), false, null, "yusuf.ozturk@outlook.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("0aa33112-9f3b-449c-bf95-2cd9e5d35ffd") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("1186c752-86bf-4555-b838-68778c05d56b") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("2a3864f6-068f-4466-8893-8f1f0d8546e0") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("2fd913b9-b623-4c21-a94b-71c98913eddf") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("481e0a35-3cfa-4aa3-b803-e78c084408b9") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("7e311a61-31eb-4365-af8f-59bc515dad10") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("87694f3c-2a20-4457-9e0b-d3ad364ba23f") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("90526a21-967d-496d-a62b-21e0d47b7a32") },
                    { new Guid("5f8b24f1-f442-4dca-a36d-9a78634f75bd"), new Guid("96060ffd-b1e8-45c5-a28e-dbc0eb5e55b5") },
                    { new Guid("5f8b24f1-f442-4dca-a36d-9a78634f75bd"), new Guid("a45ddfbc-a786-4bde-89cf-f12fe91d4a33") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("b4b2ad6a-98ee-45bb-9050-2e8e51caf658") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("b513846d-27e7-411d-a1f1-fe5ce90c733e") },
                    { new Guid("5f8b24f1-f442-4dca-a36d-9a78634f75bd"), new Guid("bca2b50e-373e-44a1-96c0-5745296dc60d") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("d59c67b4-1ae9-4f14-a911-c6cb61748b59") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("e0bde80d-0990-4673-9c78-6ceb588215ac") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("e31519ae-8f00-4a49-8b72-d020f486fc2f") },
                    { new Guid("5f8b24f1-f442-4dca-a36d-9a78634f75bd"), new Guid("e513d7df-d9de-437f-9585-4421c2a1f5d5") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("e741e7bd-e3e7-4a22-be9a-049fdf72758a") },
                    { new Guid("5f8b24f1-f442-4dca-a36d-9a78634f75bd"), new Guid("f30271ef-8e25-4b0b-ae87-e028bc8ccfdf") },
                    { new Guid("075ec7cc-e044-4dbf-8f5a-67352686fdb6"), new Guid("fe071ded-d5c9-40ea-83b0-5f27f5b2104b") }
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
