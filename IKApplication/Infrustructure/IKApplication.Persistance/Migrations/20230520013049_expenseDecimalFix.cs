using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKApplication.Persistance.Migrations
{
    public partial class expenseDecimalFix : Migration
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
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    { new Guid("044f46ff-686c-48e6-8713-a70e9f73e803"), "GHEZ7G00NUIM2LUJMSOVKFYK5U9E2R17", "Personal", "PERSONAL" },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), "4A8RWBCZIIS21GIJPX3418J292HMCZGW", "Company Administrator", "COMPANY ADMINISTRATOR" },
                    { new Guid("f0b88cd0-cee6-44ca-b8ef-76c4a3674ef9"), "8KZ4IBJK13J6K9V76MKH365YG2DQ4OR0", "Site Administrator", "SITE ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("13f6ca0b-d117-4e7a-be1b-3378034478a6"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2400), null, "İnşaat", 1, null },
                    { new Guid("1baf96ff-5737-47c0-ab8d-faf6531f03d8"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2369), null, "Ahşap Teknolojisi", 1, null },
                    { new Guid("1fcdaaf0-6164-49d5-a5ab-948bed6f3645"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2396), null, "Gıda", 1, null },
                    { new Guid("29dd8aa0-9c95-41b5-9bdb-3df61f277fd1"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2412), null, "Sağlık ve Sosyal Hizmetler", 1, null },
                    { new Guid("2c027048-7114-4681-b9a8-0a6ec472531d"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2407), null, "Medya, İletişim ve Yayıncılık", 1, null },
                    { new Guid("36928cd7-9f03-4489-a82c-1913957d7e42"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2417), null, "Ticaret (Satış ve Pazarlama)", 1, null },
                    { new Guid("3bb07729-d463-4f68-b952-ef56b756027b"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2402), null, "İş ve Yönetimi", 1, null },
                    { new Guid("45e42113-794b-4d56-b20d-bb381b8fcb81"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2403), null, "Kimya, Petrol, Lastik ve PLastik", 1, null },
                    { new Guid("4c334a8e-e4a8-4ea2-9757-bca588e116f7"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2416), null, "Tekstil, Hazır Giyim, Deri", 1, null },
                    { new Guid("4cc9dd07-76f1-4c01-abc8-e9bf1391f3fa"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2394), null, "Enerji", 1, null },
                    { new Guid("4d0d52db-e2ef-47de-a4e1-435f56c2a7bd"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2408), null, "Metal", 1, null },
                    { new Guid("4dd980d6-5e69-4c76-9569-29e40042a70b"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2391), null, "Elektrik ve Elektronik", 1, null },
                    { new Guid("69e5f746-ad2a-4c0a-866a-28e5644a7194"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2387), null, "Bilişim", 1, null },
                    { new Guid("71016439-b055-49bc-99d1-b154672381cc"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2405), null, "Maden", 1, null },
                    { new Guid("9e29e1af-9269-4032-a08b-7797d77c8124"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2404), null, "Kültür, Sanat ve Tasarım", 1, null },
                    { new Guid("9f3f1ad8-e51b-42ac-802b-2108dfbc2978"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2390), null, "Eğitim", 1, null },
                    { new Guid("a85eb9a7-029d-416f-9371-8d32267d82e0"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2395), null, "Finans", 1, null },
                    { new Guid("aaab4784-3bf7-483d-8466-721bdc806d7a"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2479), null, "Ulaştırma, Lojistik ve Haberleşme", 1, null },
                    { new Guid("c30cd1ae-a24b-4e18-b996-00300321f91d"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2477), null, "Toplumsal ve Kişisel Hizmetler", 1, null },
                    { new Guid("c6ec3ab8-0b10-4a93-89e2-85a72350cf58"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2409), null, "Otomotiv", 1, null },
                    { new Guid("e9cdcf93-97ed-4174-941b-ecdc16f5fbd3"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2415), null, "Tarım, Avcılık ve Balıkçılık", 1, null },
                    { new Guid("eb0a743e-9f28-44e2-8a69-6d64ae01bb6e"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2414), null, "Spor ve Rekreasyon", 1, null },
                    { new Guid("f26ffe1a-d92a-4679-a2b5-8559fd0614c2"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2389), null, "Cam, Çimento ve ToprakÇevre", 1, null },
                    { new Guid("fe1fb8fd-8d92-469f-b166-1d173dceaf34"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2478), null, "Turizm, Konaklama, Yiyecek-İçecek Hizmetleri", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "NumberOfEmployees", "PhoneNumber", "SectorId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("2d3657ce-2ed9-41e1-b098-30c9010f7d7a"), new DateTime(2023, 5, 20, 4, 30, 49, 24, DateTimeKind.Local).AddTicks(983), null, "info@aydinanonimsirketi.com", "Aydın Anonim Şirketi", 43, "+905461858790", new Guid("29dd8aa0-9c95-41b5-9bdb-3df61f277fd1"), 3, null },
                    { new Guid("2ed96e47-94e5-486f-ae14-f7e2211d5b2d"), new DateTime(2023, 5, 20, 4, 30, 49, 25, DateTimeKind.Local).AddTicks(4606), null, "info@celikkomanditsirketi.com", "Çelik Komandit Şirketi", 69, "+905951908611", new Guid("fe1fb8fd-8d92-469f-b166-1d173dceaf34"), 3, null },
                    { new Guid("2ef23ac2-6bc7-4689-8390-13428e834c16"), new DateTime(2023, 5, 20, 4, 30, 49, 36, DateTimeKind.Local).AddTicks(3494), null, "info@yilmazkooperatifsirketi.com", "Yılmaz Kooperatif Şirketi", 2, "+905789597207", new Guid("69e5f746-ad2a-4c0a-866a-28e5644a7194"), 3, null },
                    { new Guid("6a3db742-88af-4d3b-b5e0-a5465fdce134"), new DateTime(2023, 5, 20, 4, 30, 49, 26, DateTimeKind.Local).AddTicks(8334), null, "info@demirkollektifsirketi.com", "Demir Kollektif Şirketi", 4, "+905937861797", new Guid("29dd8aa0-9c95-41b5-9bdb-3df61f277fd1"), 3, null },
                    { new Guid("7802f35d-b134-4b25-b1ed-a44d37c39486"), new DateTime(2023, 5, 20, 4, 30, 49, 38, DateTimeKind.Local).AddTicks(7943), null, "info@sahinanonimsirketi.com", "Şahin Anonim Şirketi", 15, "+905154839010", new Guid("1baf96ff-5737-47c0-ab8d-faf6531f03d8"), 3, null },
                    { new Guid("87827267-86d9-4f3f-a393-14e401bbfaf6"), new DateTime(2023, 5, 20, 4, 30, 49, 40, DateTimeKind.Local).AddTicks(288), null, "info@yildirimkomanditsirketi.com", "Yıldırım Komandit Şirketi", 16, "+905733403844", new Guid("29dd8aa0-9c95-41b5-9bdb-3df61f277fd1"), 3, null },
                    { new Guid("8b8f4eb4-c63e-4bc7-b9bb-b05faf3f955a"), new DateTime(2023, 5, 20, 4, 30, 49, 33, DateTimeKind.Local).AddTicks(5360), null, "info@yildizkooperatifsirketi.com", "Yıldız Kooperatif Şirketi", 19, "+905364710003", new Guid("29dd8aa0-9c95-41b5-9bdb-3df61f277fd1"), 3, null },
                    { new Guid("8d66ef91-46b2-4257-b6d3-b3c80cbf8ac3"), new DateTime(2023, 5, 20, 4, 30, 49, 42, DateTimeKind.Local).AddTicks(6043), null, "info@yildizlimitedsirketi.com", "Yıldız Limited Şirketi", 48, "+905707833030", new Guid("9e29e1af-9269-4032-a08b-7797d77c8124"), 3, null },
                    { new Guid("9be3a077-c5bc-47fb-8a57-7357f055f48b"), new DateTime(2023, 5, 20, 4, 30, 49, 30, DateTimeKind.Local).AddTicks(8708), null, "info@sahinkollektifsirketi.com", "Şahin Kollektif Şirketi", 82, "+905490442184", new Guid("4d0d52db-e2ef-47de-a4e1-435f56c2a7bd"), 3, null },
                    { new Guid("a7630351-f3ca-443a-a5dc-33ec653b0e44"), new DateTime(2023, 5, 20, 4, 30, 49, 28, DateTimeKind.Local).AddTicks(1769), null, "info@ozturkkooperatifsirketi.com", "Öztürk Kooperatif Şirketi", 68, "+905607843426", new Guid("29dd8aa0-9c95-41b5-9bdb-3df61f277fd1"), 3, null },
                    { new Guid("b1c800bb-4a11-4d1e-9251-53173a55eee4"), new DateTime(2023, 5, 20, 4, 30, 49, 29, DateTimeKind.Local).AddTicks(5242), null, "info@ozdemirlimitedsirketi.com", "Özdemir Limited Şirketi", 20, "+905893329867", new Guid("71016439-b055-49bc-99d1-b154672381cc"), 3, null },
                    { new Guid("bb776678-d447-480e-85a5-97930ff7aa7c"), new DateTime(2023, 5, 20, 4, 30, 49, 37, DateTimeKind.Local).AddTicks(5706), null, "info@ozturkanonimsirketi.com", "Öztürk Anonim Şirketi", 94, "+905491336111", new Guid("4d0d52db-e2ef-47de-a4e1-435f56c2a7bd"), 3, null },
                    { new Guid("d86797a5-d56f-41c0-9f71-688275910e44"), new DateTime(2023, 5, 20, 4, 30, 49, 34, DateTimeKind.Local).AddTicks(8530), null, "info@demirkooperatifsirketi.com", "Demir Kooperatif Şirketi", 25, "+905162899038", new Guid("a85eb9a7-029d-416f-9371-8d32267d82e0"), 3, null },
                    { new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2593), null, "ikapp@ikapp.com", "IKApp A.Ş.", 5, "+905602020815", new Guid("69e5f746-ad2a-4c0a-866a-28e5644a7194"), 1, null },
                    { new Guid("e6bd5d14-4c4c-46ce-ac73-0772a582e9b1"), new DateTime(2023, 5, 20, 4, 30, 49, 32, DateTimeKind.Local).AddTicks(2146), null, "info@ozturkkooperatifsirketi.com", "Öztürk Kooperatif Şirketi", 23, "+905989649075", new Guid("2c027048-7114-4681-b9a8-0a6ec472531d"), 3, null },
                    { new Guid("f76f3d42-3170-4702-b678-77e4eb3ef609"), new DateTime(2023, 5, 20, 4, 30, 49, 41, DateTimeKind.Local).AddTicks(3540), null, "info@ozturklimitedsirketi.com", "Öztürk Limited Şirketi", 85, "+905792616681", new Guid("71016439-b055-49bc-99d1-b154672381cc"), 3, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("029352bb-234d-4916-8bb8-675e718e4161"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2727), null, "Hotel Receptionist", 1, null },
                    { new Guid("0552b1c9-8d43-4609-b14e-6e0bb18fde95"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2725), null, "Front Desk Associate", 1, null },
                    { new Guid("0adb7cd4-6546-44f9-b32c-1641638a879e"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2649), null, "Director of Business Operations", 1, null },
                    { new Guid("0c2db55d-02c6-4fe2-94d7-e1c087bb688b"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2731), null, "Project Manager", 1, null },
                    { new Guid("0e912cdd-9cb0-4dbd-be7e-ff9aebeeedf0"), new Guid("e6bd5d14-4c4c-46ce-ac73-0772a582e9b1"), new DateTime(2023, 5, 20, 4, 30, 49, 32, DateTimeKind.Local).AddTicks(2149), null, "Other Industries:", 1, null },
                    { new Guid("0f3dc84c-fe95-41a5-a929-1f7b396fb8a5"), new Guid("2d3657ce-2ed9-41e1-b098-30c9010f7d7a"), new DateTime(2023, 5, 20, 4, 30, 49, 24, DateTimeKind.Local).AddTicks(989), null, "Teaching Assistant", 1, null },
                    { new Guid("124ad5ce-1bfd-4204-9370-b61ddbbb1c8c"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2651), null, "Sr. Manager of HR", 1, null },
                    { new Guid("134406d5-e737-4d3a-a50d-36514ab99f8a"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2622), null, "Sales Representative", 1, null },
                    { new Guid("136d0182-a985-41c7-957a-1746f15ef2fb"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2635), null, "VP of Finance", 1, null },
                    { new Guid("16356e0a-8ee2-416f-a9ec-9a5e4d105987"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2667), null, "Clinical Laboratory Technician", 1, null },
                    { new Guid("16cc8223-7d02-4b2b-9529-7bf83797e817"), new Guid("d86797a5-d56f-41c0-9f71-688275910e44"), new DateTime(2023, 5, 20, 4, 30, 49, 34, DateTimeKind.Local).AddTicks(8533), null, "Marketing Coordinator", 1, null },
                    { new Guid("21ce1f2d-b1c3-451a-80e0-324849aa2335"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2716), null, "School Counselor", 1, null },
                    { new Guid("325204be-41f3-4747-b4c7-f5f31d57801e"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2657), null, "Full Stack Developer", 1, null },
                    { new Guid("35473068-6272-42a5-bcd5-13fa3d55e863"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2660), null, "Registered Nurse", 1, null },
                    { new Guid("397a62d7-745b-455d-8039-7171877c85d7"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2647), null, "Support Specialist", 1, null },
                    { new Guid("3cbfc8c0-ffad-47f9-a8ad-93e1ba02c9fb"), new Guid("2ed96e47-94e5-486f-ae14-f7e2211d5b2d"), new DateTime(2023, 5, 20, 4, 30, 49, 25, DateTimeKind.Local).AddTicks(4609), null, "Systems Administrator", 1, null },
                    { new Guid("3de63186-b4ea-4a5f-bd6c-ff3e2b891491"), new Guid("87827267-86d9-4f3f-a393-14e401bbfaf6"), new DateTime(2023, 5, 20, 4, 30, 49, 40, DateTimeKind.Local).AddTicks(290), null, "Registered Nurse", 1, null },
                    { new Guid("434628ac-4014-4a2e-9bc5-e4568464d7a5"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2646), null, "Customer Service Representative", 1, null },
                    { new Guid("49da5610-d532-42cd-b634-5c5ff58bdad2"), new Guid("8d66ef91-46b2-4257-b6d3-b3c80cbf8ac3"), new DateTime(2023, 5, 20, 4, 30, 49, 42, DateTimeKind.Local).AddTicks(6046), null, "Registered Nurse", 1, null },
                    { new Guid("4f77d46d-0b4a-4e3f-be6e-9871833a6b56"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2625), null, "CMO (Chief Marketing Officer)", 1, null },
                    { new Guid("50679332-5eba-4950-b652-2865409d528e"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2733), null, "Project Appraisal Engineer", 1, null },
                    { new Guid("52c55a8c-f10b-41d5-aa84-9c21758ba7c9"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2636), null, "Procurement Director", 1, null },
                    { new Guid("5640dbf6-bb0a-42e6-94fd-195fac865ba0"), new Guid("7802f35d-b134-4b25-b1ed-a44d37c39486"), new DateTime(2023, 5, 20, 4, 30, 49, 38, DateTimeKind.Local).AddTicks(7945), null, "Inspector", 1, null },
                    { new Guid("56886eb7-ed6c-4bb2-816b-18bd7898edff"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2652), null, "HR Analyst", 1, null },
                    { new Guid("66a09fda-dc77-4107-a931-1ed7a89d8330"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2669), null, "Administrator", 1, null },
                    { new Guid("6b96e8e8-93d1-4437-bde6-9082d5f198f6"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2653), null, "Director of Information Security", 1, null },
                    { new Guid("6e40ce18-b1df-49b0-9c9e-e133c4594d5d"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2718), null, "Teacher", 1, null },
                    { new Guid("70918b50-42e2-4bc5-a9c0-73edfe2c3c4f"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2658), null, "Data Analyst", 1, null },
                    { new Guid("80153eac-7455-44f5-b432-a8f7cd569b8c"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2648), null, "COO (Chief Operating Officer)", 1, null },
                    { new Guid("8ab4bff1-57b5-42f2-8be8-78e278e4235c"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2726), null, "Server/Host/Hostess", 1, null },
                    { new Guid("8bc9d257-3ade-435c-bb12-f0518eded6da"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2722), null, "General Manager", 1, null },
                    { new Guid("8df2a3ac-8c4b-454b-a8e4-0e8db58f6e4a"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2637), null, "Investment Analyst", 1, null },
                    { new Guid("8fd7c7d4-bf7c-441b-be3a-7490807fe938"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2639), null, "Risk Analyst", 1, null },
                    { new Guid("8ff81520-dccb-4227-8b4a-6fb3a76b2e63"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2617), null, "National Sales Director", 1, null },
                    { new Guid("9056a962-81e4-4e77-b6f2-70521bfdc376"), new Guid("f76f3d42-3170-4702-b678-77e4eb3ef609"), new DateTime(2023, 5, 20, 4, 30, 49, 41, DateTimeKind.Local).AddTicks(3542), null, "Sr. Manager of HR", 1, null },
                    { new Guid("92f2a080-5dcc-48d1-9d6a-56dac4034c51"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2650), null, "Operations Supervisor", 1, null },
                    { new Guid("9920ebed-80b2-45f7-a5d6-422f410e0ad3"), new Guid("2ef23ac2-6bc7-4689-8390-13428e834c16"), new DateTime(2023, 5, 20, 4, 30, 49, 36, DateTimeKind.Local).AddTicks(3497), null, "Construction Foreman", 1, null },
                    { new Guid("9a808e1b-685a-4c4e-be90-abdee334cf0e"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2640), null, "VP of Client Services", 1, null },
                    { new Guid("9b97bfb4-c5c3-4087-b0a7-6ea182060079"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2671), null, "Registrar", 1, null },
                    { new Guid("9f51fe39-4c4d-4386-b492-32f9b5d669a1"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2623), null, "Sales Associate", 1, null },
                    { new Guid("a58a9895-bece-4429-ba1b-436d20c8235d"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2663), null, "Physical Therapist", 1, null },
                    { new Guid("aa86c4af-8973-4d3a-9e3e-7082dbb52653"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2728), null, "Construction Foreman", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("b189772f-d982-4fea-96b4-93e0fd156193"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2666), null, "Nursing Assistant", 1, null },
                    { new Guid("b1c32da2-80fd-44b5-aff9-adf5b2ce5912"), new Guid("a7630351-f3ca-443a-a5dc-33ec653b0e44"), new DateTime(2023, 5, 20, 4, 30, 49, 28, DateTimeKind.Local).AddTicks(1775), null, "Credit Analyst", 1, null },
                    { new Guid("b480a2d3-83d9-41d4-a57e-516e1cfd2a86"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2658), null, "Systems Administrator", 1, null },
                    { new Guid("b7813dc4-a3e7-40fa-925b-3835c372930b"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2734), null, "Inspector", 1, null },
                    { new Guid("bc523957-a3c0-4db6-bfbc-91ae1698f9de"), new Guid("8b8f4eb4-c63e-4bc7-b9bb-b05faf3f955a"), new DateTime(2023, 5, 20, 4, 30, 49, 33, DateTimeKind.Local).AddTicks(5364), null, "Procurement Director", 1, null },
                    { new Guid("bd08d168-cf8b-4ccd-a586-df68f32244cd"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2632), null, "Marketing Coordinator", 1, null },
                    { new Guid("c69321ee-2d88-4aa9-8cbf-db1e3e9f8f2d"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2724), null, "Concierge", 1, null },
                    { new Guid("c695565a-1c52-4569-9145-9a0163c2c464"), new Guid("6a3db742-88af-4d3b-b5e0-a5465fdce134"), new DateTime(2023, 5, 20, 4, 30, 49, 26, DateTimeKind.Local).AddTicks(8336), null, "Server/Host/Hostess", 1, null },
                    { new Guid("c76bb8ec-56f2-4474-9205-0b76c43404d7"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2668), null, "Occupational Therapy Aide", 1, null },
                    { new Guid("caef2706-3abf-4837-a80e-7f96b7f98e10"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2670), null, "Principal", 1, null },
                    { new Guid("cc669e2b-9313-425b-974c-7650e92b16be"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2641), null, "Account Manager", 1, null },
                    { new Guid("cd248f5a-2da9-4bc4-84ff-fab84419f7c5"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2638), null, "Credit Analyst", 1, null },
                    { new Guid("cf20e561-a562-40b3-b09c-42403feb1c31"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2656), null, "Software Engineer I, II, III", 1, null },
                    { new Guid("d1fe7e73-dbca-4dc7-ba2a-98f41d38aa06"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2612), null, "VP of Sales", 1, null },
                    { new Guid("d8144fde-a38d-4075-a5b7-0210449f2202"), new Guid("9be3a077-c5bc-47fb-8a57-7357f055f48b"), new DateTime(2023, 5, 20, 4, 30, 49, 30, DateTimeKind.Local).AddTicks(8711), null, "Credit Analyst", 1, null },
                    { new Guid("d85d2e6c-2359-4b22-829d-290659f7d281"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2643), null, "Customer Success Manager", 1, null },
                    { new Guid("db9c5aea-d680-4a79-b3d8-fef8c80a7ec0"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2661), null, "Pharmacy Technician", 1, null },
                    { new Guid("dba2e3ef-a1fc-4109-841b-1080720edea9"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2732), null, "Contract Administrator", 1, null },
                    { new Guid("df5c8e15-d2f0-45ae-999c-c71e8684012b"), new Guid("b1c800bb-4a11-4d1e-9251-53173a55eee4"), new DateTime(2023, 5, 20, 4, 30, 49, 29, DateTimeKind.Local).AddTicks(5245), null, "Procurement Director", 1, null },
                    { new Guid("e299cb1b-54ec-4977-b76a-8660cf94b97f"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2659), null, "Other Industries:", 1, null },
                    { new Guid("e3935327-d50c-4390-856a-b8bd3a386d26"), new Guid("bb776678-d447-480e-85a5-97930ff7aa7c"), new DateTime(2023, 5, 20, 4, 30, 49, 37, DateTimeKind.Local).AddTicks(5708), null, "Operations Supervisor", 1, null },
                    { new Guid("ea45edfb-a68b-4415-8921-ed8c4dff84da"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2627), null, "Marketing Director", 1, null },
                    { new Guid("eba97f69-e778-40d0-87a0-623fd69c7ca3"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2730), null, "Safety Director", 1, null },
                    { new Guid("f0c2e93b-6448-4273-8394-ba1ba012f5de"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2621), null, "Regional Sales Manager", 1, null },
                    { new Guid("f6059bc4-ff29-4b3c-a954-3bec93472ce2"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2721), null, "Teaching Assistant", 1, null },
                    { new Guid("f901e5f2-4b46-4f7a-9c88-445dcf5370d9"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2630), null, "Marketing Analyst", 1, null },
                    { new Guid("f9375199-2853-43ca-82e1-e1ed34c4cd1b"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2628), null, "Sr. Marketing Manager", 1, null },
                    { new Guid("ffabea06-7909-43e3-ba60-9e708ebcbf36"), new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2723), null, "Guest Services Supervisor", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "BirthDate", "BloodGroup", "CompanyId", "ConcurrencyStamp", "CreateDate", "DeleteDate", "Email", "EmailConfirmed", "IdentityNumber", "ImagePath", "JobStartDate", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profession", "SecondName", "SecurityStamp", "Status", "Surname", "TitleId", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("09bd5576-f0e9-4c93-b7ca-c7b856301b46"), 0, null, new DateTime(2057, 1, 6, 4, 30, 49, 32, DateTimeKind.Local).AddTicks(2159), null, new Guid("e6bd5d14-4c4c-46ce-ac73-0772a582e9b1"), "6279897b-c6aa-4ae1-b269-9984a52a4d97", new DateTime(2023, 5, 20, 4, 30, 49, 32, DateTimeKind.Local).AddTicks(2158), null, "mehmet.ozturk@hotmail.com", false, "45374560576", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mehmet", "MEHMET.OZTURK@HOTMAIL.COM", "MEHMET.OZTURK@HOTMAIL.COM", "AQAAAAEAACcQAAAAEE8bo5Y+SUq7rZ/Y5TfNAO1v3kXBemsrB23RW5QBizg5MwCB2WpHHJNl0VpTiujH9w==", "+905399837686", false, "Real estate agent", null, "A0NSHGCF0NWZE0DN2EZ00P8TUKXXUKTE", 3, "Öztürk", new Guid("0e912cdd-9cb0-4dbd-be7e-ff9aebeeedf0"), false, null, "mehmet.ozturk@hotmail.com" },
                    { new Guid("16b7d74f-dc06-4730-baf2-1be2386dc8e0"), 0, null, new DateTime(2051, 2, 14, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2752), null, new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), "014596f0-7f16-4e12-8b88-ceb220876fd4", new DateTime(2023, 5, 20, 4, 30, 49, 17, DateTimeKind.Local).AddTicks(2749), null, "test1@test.com", false, "82447027284", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "TEST1@TEST.COM", "TEST1@TEST.COM", "AQAAAAEAACcQAAAAEEXJa1PS/5vIu73OyQIZNEYdxI17ssA0aLCxtuMKuT6TtwzuKg1YGb767SJycK1glA==", "+905126607703", false, "Cleaner", null, "0U0E9F5J47HM1W7DGWBLIFOBHFJL348G", 1, "Demir", new Guid("f9375199-2853-43ca-82e1-e1ed34c4cd1b"), false, null, "test1@test.com" },
                    { new Guid("1da71eb7-29e4-42f7-9d84-4a79fc2dbe34"), 0, null, new DateTime(2055, 4, 22, 4, 30, 49, 18, DateTimeKind.Local).AddTicks(6411), null, new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), "9cd15cd0-cfe6-4727-a743-1b513172a9ca", new DateTime(2023, 5, 20, 4, 30, 49, 18, DateTimeKind.Local).AddTicks(6409), null, "test2@test.com", false, "77111036242", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "TEST2@TEST.COM", "TEST2@TEST.COM", "AQAAAAEAACcQAAAAEAdHOQzFsQZiePyn3Goyfe6Q3YnGGrmDTQDOSJQVPG/s2EvOUvco1AE1peuQoa6u5Q==", "+905103183993", false, "Photographer", null, "VUW78E7IN3ZF15AK662W0WZC15JN2X9R", 1, "Yıldız", new Guid("21ce1f2d-b1c3-451a-80e0-324849aa2335"), false, null, "test2@test.com" },
                    { new Guid("223bcbfd-a80c-49e7-ba0e-c2dfe0ab3d51"), 0, null, new DateTime(2068, 2, 13, 4, 30, 49, 38, DateTimeKind.Local).AddTicks(7987), null, new Guid("7802f35d-b134-4b25-b1ed-a44d37c39486"), "26675b13-c222-41fb-8cd6-7f13da68ef6c", new DateTime(2023, 5, 20, 4, 30, 49, 38, DateTimeKind.Local).AddTicks(7986), null, "mustafa.sahin@microsoft.com", false, "20506622216", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mustafa", "MUSTAFA.SAHIN@MICROSOFT.COM", "MUSTAFA.SAHIN@MICROSOFT.COM", "AQAAAAEAACcQAAAAEPa0q540wtBEcHUILx+AUZYnfm5rvvm3gBBWtSenWlNR3ZBFVRypZMKZlSYOKqrA3w==", "+905549074303", false, "Real estate agent", null, "H00Q99BPVAUQL1RWEYFBPOWFQZTUVFLO", 3, "Şahin", new Guid("5640dbf6-bb0a-42e6-94fd-195fac865ba0"), false, null, "mustafa.sahin@microsoft.com" },
                    { new Guid("22a06b3d-437b-4a28-ba8b-f17efd29c66f"), 0, null, new DateTime(2041, 7, 1, 4, 30, 49, 24, DateTimeKind.Local).AddTicks(996), null, new Guid("2d3657ce-2ed9-41e1-b098-30c9010f7d7a"), "be2fc927-8dd0-4d6b-a52b-3cbe68b5e08c", new DateTime(2023, 5, 20, 4, 30, 49, 24, DateTimeKind.Local).AddTicks(994), null, "ali.aydin@hotmail.com", false, "24036871304", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ali", "ALI.AYDIN@HOTMAIL.COM", "ALI.AYDIN@HOTMAIL.COM", "AQAAAAEAACcQAAAAEHpO3QkD7q5WpZkkTpE0v11L4eXKV6gGQ+qgL8rrrf7oWxBBMUo2aXG8UzOM+HPeGA==", "+905462538403", false, "Optician", null, "JQ4BBEK3ZMCUARJSQ945ADL5KGZWQ84B", 3, "Aydın", new Guid("0f3dc84c-fe95-41a5-a929-1f7b396fb8a5"), false, null, "ali.aydin@hotmail.com" },
                    { new Guid("27be8602-4a1b-4d12-856a-721006479465"), 0, null, new DateTime(2044, 7, 1, 4, 30, 49, 41, DateTimeKind.Local).AddTicks(3549), null, new Guid("f76f3d42-3170-4702-b678-77e4eb3ef609"), "0d177be2-765d-49bc-bb53-c79661c1618e", new DateTime(2023, 5, 20, 4, 30, 49, 41, DateTimeKind.Local).AddTicks(3547), null, "ibrahim.ozturk@yandex.com", false, "40353358304", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "IBRAHIM.OZTURK@YANDEX.COM", "IBRAHIM.OZTURK@YANDEX.COM", "AQAAAAEAACcQAAAAEDq0jYMm6o0ILDDDdcwoFOqP1xicEPCcUk7UHfHS/o6g9ANgEyT8dROnMUwK2U/esg==", "+905796078863", false, "Teacher", null, "EHMP2DKC66R0UFBM0Z3GAYTN1OZ95GBT", 3, "Öztürk", new Guid("9056a962-81e4-4e77-b6f2-70521bfdc376"), false, null, "ibrahim.ozturk@yandex.com" },
                    { new Guid("2a81c220-ad78-4a36-944f-8ad75c92ba64"), 0, null, new DateTime(2068, 11, 21, 4, 30, 49, 36, DateTimeKind.Local).AddTicks(3506), null, new Guid("2ef23ac2-6bc7-4689-8390-13428e834c16"), "9e69d215-e05b-4cc4-8214-0017a7079736", new DateTime(2023, 5, 20, 4, 30, 49, 36, DateTimeKind.Local).AddTicks(3505), null, "osman.yilmaz@yandex.com", false, "32757543400", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "OSMAN.YILMAZ@YANDEX.COM", "OSMAN.YILMAZ@YANDEX.COM", "AQAAAAEAACcQAAAAEO7JmMnuL65Y4OPpYFXodKQnkpQgQL/ss9HNil6Bcdt+Ew2ZyPW1g3YBVMSxd01EUw==", "+905434594664", false, "Factory worker", null, "B79P4BFG3OFXNHA4X7HJWSTQ0H5Z87GT", 3, "Yılmaz", new Guid("9920ebed-80b2-45f7-a5d6-422f410e0ad3"), false, null, "osman.yilmaz@yandex.com" },
                    { new Guid("36a59766-c2d8-48a9-8e6a-64f3d60ea6c6"), 0, null, new DateTime(2064, 1, 20, 4, 30, 49, 22, DateTimeKind.Local).AddTicks(7397), null, new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), "f821ae66-1a40-47a7-b0bc-c298e2a2061f", new DateTime(2023, 5, 20, 4, 30, 49, 22, DateTimeKind.Local).AddTicks(7395), null, "test5@test.com", false, "40775148118", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "TEST5@TEST.COM", "TEST5@TEST.COM", "AQAAAAEAACcQAAAAEKaN9b3NxgWmKbuWMpgOWVecZvnaEd6ZCiP2vLG9Sz8YY5Lqj0zHgAJoDMirqUduUg==", "+905439801730", false, "Florist", null, "6DJBMOJCR9EKX582S1932ELIFR5JJWOE", 1, "Aydın", new Guid("8ff81520-dccb-4227-8b4a-6fb3a76b2e63"), false, null, "test5@test.com" },
                    { new Guid("442ba5ee-1401-4d1d-8a59-242bfacecb5f"), 0, null, new DateTime(2075, 1, 8, 4, 30, 49, 37, DateTimeKind.Local).AddTicks(5716), null, new Guid("bb776678-d447-480e-85a5-97930ff7aa7c"), "e844e6c6-6c49-4026-b965-b6ccfd27bbe4", new DateTime(2023, 5, 20, 4, 30, 49, 37, DateTimeKind.Local).AddTicks(5715), null, "ibrahim.ozturk@outlook.com", false, "11011742704", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "IBRAHIM.OZTURK@OUTLOOK.COM", "IBRAHIM.OZTURK@OUTLOOK.COM", "AQAAAAEAACcQAAAAEM9mPgStn0Z3qNT0Apt1I4qSRJi/83UneyRd+9l/y43m+Li4QPvAF9jHZY0PBjBUYw==", "+905352186316", false, "Electrician", null, "PBMYCHMHFOQM9Q4Z7AO151RMJVXVH734", 3, "Öztürk", new Guid("e3935327-d50c-4390-856a-b8bd3a386d26"), false, null, "ibrahim.ozturk@outlook.com" },
                    { new Guid("48595a98-8150-4cb9-a70a-587c40044387"), 0, null, new DateTime(2046, 10, 12, 4, 30, 49, 28, DateTimeKind.Local).AddTicks(1780), null, new Guid("a7630351-f3ca-443a-a5dc-33ec653b0e44"), "58b244a1-4b10-4a6e-b991-4488927921a9", new DateTime(2023, 5, 20, 4, 30, 49, 28, DateTimeKind.Local).AddTicks(1780), null, "yusuf.ozturk@hotmail.com", false, "37475036296", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Yusuf", "YUSUF.OZTURK@HOTMAIL.COM", "YUSUF.OZTURK@HOTMAIL.COM", "AQAAAAEAACcQAAAAEEQId2hfrSBZ//aRKYBb91lRJ5XavqqQko2OEou+BbEmcEFJEZR9uYHk6JJhqf/LyQ==", "+905907951170", false, "Fireman/Fire fighter", null, "NQWH8V1WYTL7PRG5USK5IOROPM49PR3G", 3, "Öztürk", new Guid("b1c32da2-80fd-44b5-aff9-adf5b2ce5912"), false, null, "yusuf.ozturk@hotmail.com" },
                    { new Guid("4c944dd1-3ad7-457c-a22c-245821aa5df4"), 0, null, new DateTime(2061, 11, 5, 4, 30, 49, 29, DateTimeKind.Local).AddTicks(5376), null, new Guid("b1c800bb-4a11-4d1e-9251-53173a55eee4"), "b3f0f9e7-9b2c-4299-953e-fdf1dc44c52d", new DateTime(2023, 5, 20, 4, 30, 49, 29, DateTimeKind.Local).AddTicks(5374), null, "huseyin.ozdemir@google.com", false, "61440701070", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "HUSEYIN.OZDEMIR@GOOGLE.COM", "HUSEYIN.OZDEMIR@GOOGLE.COM", "AQAAAAEAACcQAAAAEJEJCZMb2L5OxOgCRG5+IxoPkHBHnLIAczERz6rTi1vVhJ6XczFwUrI38Trazm+YOw==", "+905864851056", false, "Designer", null, "70TXCN0TTMLYVGD1IGO6IG7K4E5PMETQ", 3, "Özdemir", new Guid("df5c8e15-d2f0-45ae-999c-c71e8684012b"), false, null, "huseyin.ozdemir@google.com" },
                    { new Guid("512021fb-bcb0-41e4-81b0-5fcc6663ec02"), 0, null, new DateTime(2047, 3, 22, 4, 30, 49, 25, DateTimeKind.Local).AddTicks(4616), null, new Guid("2ed96e47-94e5-486f-ae14-f7e2211d5b2d"), "51ae6e96-8d73-42ae-9d10-28c96d96a986", new DateTime(2023, 5, 20, 4, 30, 49, 25, DateTimeKind.Local).AddTicks(4615), null, "mehmet.celik@yahoo.com", false, "23703185256", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mehmet", "MEHMET.CELIK@YAHOO.COM", "MEHMET.CELIK@YAHOO.COM", "AQAAAAEAACcQAAAAEFflQjZxIr9DMCtvC6rriJn3R3OCLTh/lQXlSO9BR3FIx8P765DZpdCJXsjZiWw0sg==", "+905728022398", false, "Fireman/Fire fighter", null, "7FPKI96K8QJ5VXMALR9EWOUHD4S04YAC", 3, "Çelik", new Guid("3cbfc8c0-ffad-47f9-a8ad-93e1ba02c9fb"), false, null, "mehmet.celik@yahoo.com" },
                    { new Guid("541bb153-2d0e-4594-b103-19c7c6b815b8"), 0, null, new DateTime(2047, 6, 8, 4, 30, 49, 26, DateTimeKind.Local).AddTicks(8344), null, new Guid("6a3db742-88af-4d3b-b5e0-a5465fdce134"), "5ff1bc5b-2dd2-4419-87b1-c20db2a8ac35", new DateTime(2023, 5, 20, 4, 30, 49, 26, DateTimeKind.Local).AddTicks(8344), null, "ismail.demir@hotmail.com", false, "26310358334", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.DEMIR@HOTMAIL.COM", "ISMAIL.DEMIR@HOTMAIL.COM", "AQAAAAEAACcQAAAAEAVGSsCJ393WHm98YsH0yliYIS1isFwZvrZu6OorLiuO/4I8KFKqwmAknQQQfCi1Lg==", "+905742433804", false, "Doctor", null, "RWMUHT1UN1IHRW0FTXNP2RD0TWVGMWY8", 3, "Demir", new Guid("c695565a-1c52-4569-9145-9a0163c2c464"), false, null, "ismail.demir@hotmail.com" },
                    { new Guid("57e609d2-87c5-40b1-ad6d-c07a54e6369e"), 0, null, new DateTime(2061, 2, 6, 4, 30, 49, 30, DateTimeKind.Local).AddTicks(8716), null, new Guid("9be3a077-c5bc-47fb-8a57-7357f055f48b"), "ee32cdc0-b943-4e6c-8e33-cac1043345e6", new DateTime(2023, 5, 20, 4, 30, 49, 30, DateTimeKind.Local).AddTicks(8715), null, "mustafa.sahin@yandex.com", false, "80100334212", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mustafa", "MUSTAFA.SAHIN@YANDEX.COM", "MUSTAFA.SAHIN@YANDEX.COM", "AQAAAAEAACcQAAAAEH07a89eBLWxvgcVX55bnyHKHseGLrkoemmz4lJYCcDcSoNFj/qO/QLPgXe5P8wwrw==", "+905219774721", false, "Actor/Actress", null, "KJ497PW7E0YM9SZOBWC8W6Z0V99P3KIX", 3, "Şahin", new Guid("d8144fde-a38d-4075-a5b7-0210449f2202"), false, null, "mustafa.sahin@yandex.com" },
                    { new Guid("5a08af2f-d578-4d6d-91cc-447bf92b793f"), 0, null, new DateTime(2052, 2, 5, 4, 30, 49, 33, DateTimeKind.Local).AddTicks(5369), null, new Guid("8b8f4eb4-c63e-4bc7-b9bb-b05faf3f955a"), "e6bbd29d-3b13-41bc-8c44-60afd35f136d", new DateTime(2023, 5, 20, 4, 30, 49, 33, DateTimeKind.Local).AddTicks(5369), null, "ismail.yildiz@yandex.com", false, "31764413560", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.YILDIZ@YANDEX.COM", "ISMAIL.YILDIZ@YANDEX.COM", "AQAAAAEAACcQAAAAED0aRA7WFM93lxEms++lQfW1OTiqMZh72xoVvsci6rAlRnC3jZYuUJ5QTKURLPOzkQ==", "+905468880930", false, "Judge", null, "12760H5V6N1TQJA3DHSJVLKKCMXOQU4S", 3, "Yıldız", new Guid("bc523957-a3c0-4db6-bfbc-91ae1698f9de"), false, null, "ismail.yildiz@yandex.com" },
                    { new Guid("aa58732a-8438-4327-b904-700082671027"), 0, null, new DateTime(2043, 7, 18, 4, 30, 49, 21, DateTimeKind.Local).AddTicks(3970), null, new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), "84bfaf7d-b9ca-477b-b8ff-015d809b31dc", new DateTime(2023, 5, 20, 4, 30, 49, 21, DateTimeKind.Local).AddTicks(3968), null, "test4@test.com", false, "60605537394", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ali", "TEST4@TEST.COM", "TEST4@TEST.COM", "AQAAAAEAACcQAAAAEIn0nlnWLS63FU3laVeXefrdmmPlK5Px+TY/PMIkiUAVWJ/4/s/oN/hD4hAWYArdug==", "+905262839556", false, "Waiter/Waitress", null, "UQRT9NNZ09ZMVSREGB0DGC785KX9UVTH", 1, "Şahin", new Guid("f9375199-2853-43ca-82e1-e1ed34c4cd1b"), false, null, "test4@test.com" },
                    { new Guid("c25a7e1a-7d9f-43a5-8cbd-04783341e708"), 0, null, new DateTime(2062, 12, 4, 4, 30, 49, 20, DateTimeKind.Local).AddTicks(520), null, new Guid("db7907f9-3379-4c6d-8523-e887860b9914"), "92eea74a-9ec1-4375-8aad-02558078e75b", new DateTime(2023, 5, 20, 4, 30, 49, 20, DateTimeKind.Local).AddTicks(518), null, "test3@test.com", false, "15028627696", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "TEST3@TEST.COM", "TEST3@TEST.COM", "AQAAAAEAACcQAAAAEIGVfF1Tm9YDC8aYjXoqYu4YiPGvh9kOwjg+2owOe8a2P0rYisr0qsJEXtycAhVZSw==", "+905586142811", false, "Plumber", null, "H60X8DW1I1MMD6F6YQWD69KS0E4AIUYC", 1, "Yıldız", new Guid("dba2e3ef-a1fc-4109-841b-1080720edea9"), false, null, "test3@test.com" },
                    { new Guid("cc3110aa-ee46-406c-84be-43f157a2bc58"), 0, null, new DateTime(2076, 2, 29, 4, 30, 49, 42, DateTimeKind.Local).AddTicks(6053), null, new Guid("8d66ef91-46b2-4257-b6d3-b3c80cbf8ac3"), "a5b9b489-30ba-4493-b2b5-2ceeef616514", new DateTime(2023, 5, 20, 4, 30, 49, 42, DateTimeKind.Local).AddTicks(6052), null, "huseyin.yildiz@yahoo.com", false, "37346041794", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "HUSEYIN.YILDIZ@YAHOO.COM", "HUSEYIN.YILDIZ@YAHOO.COM", "AQAAAAEAACcQAAAAEG00e+eqQGJ9Irjudk6Iy/wF7bt4/C4gmtmoAosjpuvbWPNhnaWZBoQjQl39g3WoRQ==", "+905522716230", false, "Photographer", null, "8OHNP234RQ9W2WEUVHXJZ5Z3MZ2L0EQA", 3, "Yıldız", new Guid("49da5610-d532-42cd-b634-5c5ff58bdad2"), false, null, "huseyin.yildiz@yahoo.com" },
                    { new Guid("d8397f36-fa8c-4200-8143-dba2b66d85cd"), 0, null, new DateTime(2062, 8, 26, 4, 30, 49, 34, DateTimeKind.Local).AddTicks(8538), null, new Guid("d86797a5-d56f-41c0-9f71-688275910e44"), "9fd2cb19-dee9-42f3-8e9d-5899f659cf9d", new DateTime(2023, 5, 20, 4, 30, 49, 34, DateTimeKind.Local).AddTicks(8537), null, "ismail.demir@microsoft.com", false, "78028786398", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.DEMIR@MICROSOFT.COM", "ISMAIL.DEMIR@MICROSOFT.COM", "AQAAAAEAACcQAAAAEJymZMXM5He/uRFJkZamxjJHejbNS3Ryqzty6ZSA2SJ/FY7ztyKGco9aDsTRMT5BQA==", "+905126870247", false, "Mechanic", null, "2YKDD0EVLMBJW3JYIP49T3EDUI34PCWG", 3, "Demir", new Guid("16cc8223-7d02-4b2b-9529-7bf83797e817"), false, null, "ismail.demir@microsoft.com" },
                    { new Guid("df0bb13c-13aa-48a8-8656-a54b8f297940"), 0, null, new DateTime(2061, 3, 2, 4, 30, 49, 40, DateTimeKind.Local).AddTicks(295), null, new Guid("87827267-86d9-4f3f-a393-14e401bbfaf6"), "2b69eb91-1943-4309-94cb-9512a5b8f663", new DateTime(2023, 5, 20, 4, 30, 49, 40, DateTimeKind.Local).AddTicks(294), null, "ahmet.yildirim@microsoft.com", false, "68133658000", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "AHMET.YILDIRIM@MICROSOFT.COM", "AHMET.YILDIRIM@MICROSOFT.COM", "AQAAAAEAACcQAAAAEI2QM9QFVM3u8bWglt4bZzMruKxwcy3PfEOMYq/00kxNvPA9sxai578hHAGLg3wFGg==", "+905885394761", false, "Hairdresser", null, "R4TS11W0KU600M6U24METIOOYWML62W6", 3, "Yıldırım", new Guid("3de63186-b4ea-4a5f-bd6c-ff3e2b891491"), false, null, "ahmet.yildirim@microsoft.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("09bd5576-f0e9-4c93-b7ca-c7b856301b46") },
                    { new Guid("f0b88cd0-cee6-44ca-b8ef-76c4a3674ef9"), new Guid("16b7d74f-dc06-4730-baf2-1be2386dc8e0") },
                    { new Guid("f0b88cd0-cee6-44ca-b8ef-76c4a3674ef9"), new Guid("1da71eb7-29e4-42f7-9d84-4a79fc2dbe34") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("223bcbfd-a80c-49e7-ba0e-c2dfe0ab3d51") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("22a06b3d-437b-4a28-ba8b-f17efd29c66f") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("27be8602-4a1b-4d12-856a-721006479465") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("2a81c220-ad78-4a36-944f-8ad75c92ba64") },
                    { new Guid("f0b88cd0-cee6-44ca-b8ef-76c4a3674ef9"), new Guid("36a59766-c2d8-48a9-8e6a-64f3d60ea6c6") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("442ba5ee-1401-4d1d-8a59-242bfacecb5f") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("48595a98-8150-4cb9-a70a-587c40044387") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("4c944dd1-3ad7-457c-a22c-245821aa5df4") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("512021fb-bcb0-41e4-81b0-5fcc6663ec02") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("541bb153-2d0e-4594-b103-19c7c6b815b8") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("57e609d2-87c5-40b1-ad6d-c07a54e6369e") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("5a08af2f-d578-4d6d-91cc-447bf92b793f") },
                    { new Guid("f0b88cd0-cee6-44ca-b8ef-76c4a3674ef9"), new Guid("aa58732a-8438-4327-b904-700082671027") },
                    { new Guid("f0b88cd0-cee6-44ca-b8ef-76c4a3674ef9"), new Guid("c25a7e1a-7d9f-43a5-8cbd-04783341e708") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("cc3110aa-ee46-406c-84be-43f157a2bc58") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("d8397f36-fa8c-4200-8143-dba2b66d85cd") },
                    { new Guid("55a0e75b-56db-40c8-ae41-8b5582e46415"), new Guid("df0bb13c-13aa-48a8-8656-a54b8f297940") }
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
