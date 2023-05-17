using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKApplication.Persistance.Migrations
{
    public partial class expenseFix : Migration
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
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), "BXY0VV0KZDCKHSXJSM8VXK63JHSZ2LZW", "Company Administrator", "COMPANY ADMINISTRATOR" },
                    { new Guid("73066ff6-8b20-4619-b36f-c80cfc4e239a"), "AEU4MV2KUT84W5CQKVKNVETJD9XF2XF0", "Personal", "PERSONAL" },
                    { new Guid("7f77b47e-a272-4ead-8a88-1c3b4f0b2d21"), "BY6LKD2QTHNMVCLUDDVHOQ560FVGABVE", "Site Administrator", "SITE ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("19b58672-8cc8-45c7-8140-294ee7e302d7"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6852), null, "Bilişim", 1, null },
                    { new Guid("1a4e34ee-2bb3-44a3-aa79-20db5e9da573"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6874), null, "Otomotiv", 1, null },
                    { new Guid("1edb60d8-3a91-421f-9c56-2759d03cba36"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6863), null, "Gıda", 1, null },
                    { new Guid("321d916d-88b3-4b9a-af05-10c77d719d06"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6835), null, "Ahşap Teknolojisi", 1, null },
                    { new Guid("3ef8693b-394c-403e-99d8-0fb9e6d6bf32"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6872), null, "Metal", 1, null },
                    { new Guid("3fe05c5c-fd30-48af-ad88-b13c4d659ed9"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6879), null, "Tarım, Avcılık ve Balıkçılık", 1, null },
                    { new Guid("510b26dd-0d97-470b-8237-c0c2ee2c884d"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6878), null, "Spor ve Rekreasyon", 1, null },
                    { new Guid("5274fd18-d817-4b91-ab63-486a5aa4fd6f"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6855), null, "Eğitim", 1, null },
                    { new Guid("689bdf3e-354a-4f23-b76c-2f1921adf7c4"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6886), null, "Ulaştırma, Lojistik ve Haberleşme", 1, null },
                    { new Guid("90ef931d-aaeb-464a-95b4-528c65784975"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6854), null, "Cam, Çimento ve ToprakÇevre", 1, null },
                    { new Guid("9150f447-ee56-443e-bca3-7d661cfc7ce3"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6866), null, "İş ve Yönetimi", 1, null },
                    { new Guid("925f1dcd-00a3-4e07-9abf-545915271c60"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6858), null, "Enerji", 1, null },
                    { new Guid("98403b65-a2d4-4b19-b951-2ef30d27dc78"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6859), null, "Finans", 1, null },
                    { new Guid("aa17b855-d24c-41a0-8b56-03017bebf3a1"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6883), null, "Turizm, Konaklama, Yiyecek-İçecek Hizmetleri", 1, null },
                    { new Guid("afd7d858-f4ba-4587-bb1c-d497121a5d84"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6870), null, "Maden", 1, null },
                    { new Guid("b5c03477-6273-4eff-8900-ae1d2a7fa718"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6880), null, "Tekstil, Hazır Giyim, Deri", 1, null },
                    { new Guid("b6605643-ba81-44fe-8916-c228189cc29b"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6882), null, "Toplumsal ve Kişisel Hizmetler", 1, null },
                    { new Guid("c294a152-59e7-436e-8943-baaf11aac600"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6868), null, "Kimya, Petrol, Lastik ve PLastik", 1, null },
                    { new Guid("ce44a681-286f-4782-a3ce-b4c17eab3798"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6881), null, "Ticaret (Satış ve Pazarlama)", 1, null },
                    { new Guid("d5c656fe-9a09-497b-90ba-ed6435e65e17"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6871), null, "Medya, İletişim ve Yayıncılık", 1, null },
                    { new Guid("d8ae58f4-c175-4773-a291-547fa3f8606e"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6869), null, "Kültür, Sanat ve Tasarım", 1, null },
                    { new Guid("e1278f96-9aca-4447-874b-4c6e725d4722"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6864), null, "İnşaat", 1, null },
                    { new Guid("fccdb25b-23fb-4548-8137-586b537da8d1"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6876), null, "Sağlık ve Sosyal Hizmetler", 1, null },
                    { new Guid("ff1c0275-b2d7-4564-995a-30a284dee519"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(6856), null, "Elektrik ve Elektronik", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "NumberOfEmployees", "PhoneNumber", "SectorId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("0492de86-03f9-47d6-9b3b-90d7bec20aad"), new DateTime(2023, 5, 17, 18, 54, 30, 585, DateTimeKind.Local).AddTicks(3130), null, "info@yildirimkollektifsirketi.com", "Yıldırım Kollektif Şirketi", 85, "+905940905461", new Guid("90ef931d-aaeb-464a-95b4-528c65784975"), 3, null },
                    { new Guid("16ddd30c-f66f-472c-8612-d81bd6e40d3a"), new DateTime(2023, 5, 17, 18, 54, 30, 596, DateTimeKind.Local).AddTicks(2215), null, "info@ozdemirkollektifsirketi.com", "Özdemir Kollektif Şirketi", 53, "+905124589346", new Guid("fccdb25b-23fb-4548-8137-586b537da8d1"), 3, null },
                    { new Guid("1c6a0fd5-8a98-44de-9be3-42ba1f8a2624"), new DateTime(2023, 5, 17, 18, 54, 30, 579, DateTimeKind.Local).AddTicks(9021), null, "info@demirkomanditsirketi.com", "Demir Komandit Şirketi", 66, "+905641090022", new Guid("e1278f96-9aca-4447-874b-4c6e725d4722"), 3, null },
                    { new Guid("3bc14794-acbf-4da5-8290-1a3c4b9c1b80"), new DateTime(2023, 5, 17, 18, 54, 30, 582, DateTimeKind.Local).AddTicks(6066), null, "info@yilmazkooperatifsirketi.com", "Yılmaz Kooperatif Şirketi", 91, "+905684596133", new Guid("b5c03477-6273-4eff-8900-ae1d2a7fa718"), 3, null },
                    { new Guid("4947e306-503c-4c10-9b1b-8c9941daf0ee"), new DateTime(2023, 5, 17, 18, 54, 30, 590, DateTimeKind.Local).AddTicks(7668), null, "info@aydinkooperatifsirketi.com", "Aydın Kooperatif Şirketi", 34, "+905265624353", new Guid("b5c03477-6273-4eff-8900-ae1d2a7fa718"), 3, null },
                    { new Guid("5e6c2342-a590-4013-a6bb-136808e2c4c9"), new DateTime(2023, 5, 17, 18, 54, 30, 597, DateTimeKind.Local).AddTicks(6160), null, "info@aydinkollektifsirketi.com", "Aydın Kollektif Şirketi", 11, "+905117347978", new Guid("e1278f96-9aca-4447-874b-4c6e725d4722"), 3, null },
                    { new Guid("71c334c9-c0c8-49f7-847a-829af84c46c8"), new DateTime(2023, 5, 17, 18, 54, 30, 583, DateTimeKind.Local).AddTicks(9522), null, "info@kayakomanditsirketi.com", "Kaya Komandit Şirketi", 64, "+905495305684", new Guid("1edb60d8-3a91-421f-9c56-2759d03cba36"), 3, null },
                    { new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7106), null, "ikapp@ikapp.com", "IKApp A.Ş.", 5, "+905919231734", new Guid("19b58672-8cc8-45c7-8140-294ee7e302d7"), 1, null },
                    { new Guid("9bdfbdf2-81d0-4f21-a1d7-bb099852efa0"), new DateTime(2023, 5, 17, 18, 54, 30, 578, DateTimeKind.Local).AddTicks(5502), null, "info@celiklimitedsirketi.com", "Çelik Limited Şirketi", 69, "+905613280631", new Guid("ff1c0275-b2d7-4564-995a-30a284dee519"), 3, null },
                    { new Guid("9f9442ac-121d-452a-a0b9-49a35e5b3d25"), new DateTime(2023, 5, 17, 18, 54, 30, 592, DateTimeKind.Local).AddTicks(1321), null, "info@celikkooperatifsirketi.com", "Çelik Kooperatif Şirketi", 94, "+905842882252", new Guid("3fe05c5c-fd30-48af-ad88-b13c4d659ed9"), 3, null },
                    { new Guid("a2163d66-55a5-48ff-94b8-9c6c75db4334"), new DateTime(2023, 5, 17, 18, 54, 30, 586, DateTimeKind.Local).AddTicks(6818), null, "info@celiklimitedsirketi.com", "Çelik Limited Şirketi", 51, "+905359258014", new Guid("5274fd18-d817-4b91-ab63-486a5aa4fd6f"), 3, null },
                    { new Guid("bef292cb-0219-4ab6-9e2d-cfa484e32518"), new DateTime(2023, 5, 17, 18, 54, 30, 589, DateTimeKind.Local).AddTicks(3887), null, "info@yildizkollektifsirketi.com", "Yıldız Kollektif Şirketi", 54, "+905372403308", new Guid("b5c03477-6273-4eff-8900-ae1d2a7fa718"), 3, null },
                    { new Guid("c2fb3767-381b-4210-80bc-0ae4e03e5947"), new DateTime(2023, 5, 17, 18, 54, 30, 594, DateTimeKind.Local).AddTicks(8715), null, "info@kayaanonimsirketi.com", "Kaya Anonim Şirketi", 88, "+905706635872", new Guid("d5c656fe-9a09-497b-90ba-ed6435e65e17"), 3, null },
                    { new Guid("c5d106e0-4b17-494c-a5f0-0375baa585ee"), new DateTime(2023, 5, 17, 18, 54, 30, 593, DateTimeKind.Local).AddTicks(5027), null, "info@celikanonimsirketi.com", "Çelik Anonim Şirketi", 28, "+905534545936", new Guid("321d916d-88b3-4b9a-af05-10c77d719d06"), 3, null },
                    { new Guid("ed277d2d-929d-430f-a58d-8743579f714d"), new DateTime(2023, 5, 17, 18, 54, 30, 588, DateTimeKind.Local).AddTicks(328), null, "info@sahinlimitedsirketi.com", "Şahin Limited Şirketi", 11, "+905784733586", new Guid("d8ae58f4-c175-4773-a291-547fa3f8606e"), 3, null },
                    { new Guid("f87aa45c-282d-42d6-9e84-b1690004c9ac"), new DateTime(2023, 5, 17, 18, 54, 30, 581, DateTimeKind.Local).AddTicks(2592), null, "info@ozdemirkollektifsirketi.com", "Özdemir Kollektif Şirketi", 50, "+905429183729", new Guid("ce44a681-286f-4782-a3ce-b4c17eab3798"), 3, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("0379e912-28cd-4d84-b524-45cb18734aef"), new Guid("0492de86-03f9-47d6-9b3b-90d7bec20aad"), new DateTime(2023, 5, 17, 18, 54, 30, 585, DateTimeKind.Local).AddTicks(3134), null, "Concierge", 1, null },
                    { new Guid("0b09fa2d-e6ec-48a5-a18c-c2b5cffa840c"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7152), null, "VP of Client Services", 1, null },
                    { new Guid("0e5ef46d-06be-42e1-9fc6-31d38b9fa480"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7255), null, "Inspector", 1, null },
                    { new Guid("0f04f817-11b7-4170-9c9f-4217cdb560d5"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7169), null, "Systems Administrator", 1, null },
                    { new Guid("131f2216-2312-4309-92dc-7456b505521d"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7166), null, "Director of Information Security", 1, null },
                    { new Guid("21abe347-3ac0-4370-8ecd-8d4892c50f48"), new Guid("9bdfbdf2-81d0-4f21-a1d7-bb099852efa0"), new DateTime(2023, 5, 17, 18, 54, 30, 578, DateTimeKind.Local).AddTicks(5582), null, "VP of Finance", 1, null },
                    { new Guid("231c95c9-96e4-4454-9338-e42975c35acd"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7133), null, "National Sales Director", 1, null },
                    { new Guid("25bcb86f-7898-4445-98a5-9e5b0e9bc91a"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7161), null, "Operations Supervisor", 1, null },
                    { new Guid("2e6e1880-4847-4564-806f-2ba6bad7250a"), new Guid("5e6c2342-a590-4013-a6bb-136808e2c4c9"), new DateTime(2023, 5, 17, 18, 54, 30, 597, DateTimeKind.Local).AddTicks(6168), null, "Safety Director", 1, null },
                    { new Guid("313d90f6-4858-4dc4-9858-040784c72329"), new Guid("a2163d66-55a5-48ff-94b8-9c6c75db4334"), new DateTime(2023, 5, 17, 18, 54, 30, 586, DateTimeKind.Local).AddTicks(6826), null, "Full Stack Developer", 1, null },
                    { new Guid("34d3b6e9-96c4-4675-8c8f-3082fa4a9e42"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7157), null, "Customer Service Representative", 1, null },
                    { new Guid("36d0f60c-abaf-4af1-b4df-3f6d96bc2635"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7173), null, "Pharmacy Technician", 1, null },
                    { new Guid("37514e18-c9ae-4dcf-807d-63f5523156e9"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7180), null, "Administrator", 1, null },
                    { new Guid("390b2992-d259-4570-80f2-a9fbbaf3c394"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7135), null, "Sales Representative", 1, null },
                    { new Guid("3e10fd48-231f-46fb-890e-e8bb160ec2c8"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7178), null, "Clinical Laboratory Technician", 1, null },
                    { new Guid("4014aaa4-671a-4f01-b42f-67ebcc9d1990"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7186), null, "Teacher", 1, null },
                    { new Guid("43134a9f-073a-4ad0-9b86-7d496ac3b3ea"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7245), null, "Front Desk Associate", 1, null },
                    { new Guid("4823e46f-4bea-42ed-bba2-3369ff01f13b"), new Guid("71c334c9-c0c8-49f7-847a-829af84c46c8"), new DateTime(2023, 5, 17, 18, 54, 30, 583, DateTimeKind.Local).AddTicks(9526), null, "Sales Associate", 1, null },
                    { new Guid("4878a03f-603d-4956-bed9-ac793937428b"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7181), null, "Principal", 1, null },
                    { new Guid("48ff745d-ef1c-47b9-babb-bd264d239d84"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7153), null, "Account Manager", 1, null },
                    { new Guid("491167ab-3749-49f3-9ef2-ff88367a9591"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7138), null, "CMO (Chief Marketing Officer)", 1, null },
                    { new Guid("49809b9a-0a60-4751-b90b-f3cb35619eb2"), new Guid("1c6a0fd5-8a98-44de-9be3-42ba1f8a2624"), new DateTime(2023, 5, 17, 18, 54, 30, 579, DateTimeKind.Local).AddTicks(9026), null, "Concierge", 1, null },
                    { new Guid("4db5b648-dcc8-49e5-9c68-9c6f5f1c2433"), new Guid("c5d106e0-4b17-494c-a5f0-0375baa585ee"), new DateTime(2023, 5, 17, 18, 54, 30, 593, DateTimeKind.Local).AddTicks(5030), null, "Administrator", 1, null },
                    { new Guid("4efc84ea-fa15-4027-b79d-ac49e9ddeb40"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7170), null, "Data Analyst", 1, null },
                    { new Guid("50bad660-a1c8-4342-b340-4bdba4234560"), new Guid("3bc14794-acbf-4da5-8290-1a3c4b9c1b80"), new DateTime(2023, 5, 17, 18, 54, 30, 582, DateTimeKind.Local).AddTicks(6123), null, "Administrator", 1, null },
                    { new Guid("5b4a61d1-d1ba-4c83-a6cf-c815a60bdcf0"), new Guid("c2fb3767-381b-4210-80bc-0ae4e03e5947"), new DateTime(2023, 5, 17, 18, 54, 30, 594, DateTimeKind.Local).AddTicks(8721), null, "Nursing Assistant", 1, null },
                    { new Guid("5c8f1dfc-c5b4-48c0-adfe-01bea0aa5059"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7246), null, "Server/Host/Hostess", 1, null },
                    { new Guid("5dff7060-b04a-4d39-a32a-4ec13734e4e1"), new Guid("9f9442ac-121d-452a-a0b9-49a35e5b3d25"), new DateTime(2023, 5, 17, 18, 54, 30, 592, DateTimeKind.Local).AddTicks(1336), null, "Investment Analyst", 1, null },
                    { new Guid("60d00f1b-6d11-4fde-bf0b-221d9039ade1"), new Guid("16ddd30c-f66f-472c-8612-d81bd6e40d3a"), new DateTime(2023, 5, 17, 18, 54, 30, 596, DateTimeKind.Local).AddTicks(2220), null, "Sales Representative", 1, null },
                    { new Guid("648da832-89f7-4f1b-80d9-ee72a5de2aec"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7163), null, "HR Analyst", 1, null },
                    { new Guid("65b21465-db2c-4108-b476-723805743f53"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7149), null, "Investment Analyst", 1, null },
                    { new Guid("66064dc4-0fe2-4199-b0f2-5001a76dda9c"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7159), null, "COO (Chief Operating Officer)", 1, null },
                    { new Guid("66a48ee6-65c5-4b23-a7d2-721f9cdebbe6"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7141), null, "Sr. Marketing Manager", 1, null },
                    { new Guid("67198283-1b91-4673-9e4d-4e66ad19837e"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7145), null, "Marketing Coordinator", 1, null },
                    { new Guid("6b5f6104-2ef1-4426-b1e0-79b1ef9f6d41"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7156), null, "Customer Success Manager", 1, null },
                    { new Guid("7af25a93-9ead-413b-b99e-0726e35fc927"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7251), null, "Safety Director", 1, null },
                    { new Guid("7fe9b09f-02c9-42c3-b296-283e1614f5be"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7146), null, "VP of Finance", 1, null },
                    { new Guid("89b79cfc-cc0f-4ec1-9bf6-4e5b130c1086"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7187), null, "Teaching Assistant", 1, null },
                    { new Guid("8e553890-473f-420f-b451-3c9ae5f124db"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7244), null, "Concierge", 1, null },
                    { new Guid("9087dfd3-b318-42bb-925f-e91a6d1431fb"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7242), null, "Guest Services Supervisor", 1, null },
                    { new Guid("9251becb-5ab9-4a08-a616-3674356f2b14"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7179), null, "Occupational Therapy Aide", 1, null },
                    { new Guid("92997569-dff8-4ce4-8e36-e1e60c48f8b2"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7134), null, "Regional Sales Manager", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("92b61100-c445-46bb-b847-c7fd9d5dad59"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7167), null, "Software Engineer I, II, III", 1, null },
                    { new Guid("9af97c85-6c39-44d2-b398-3c4d0cc9c39f"), new Guid("bef292cb-0219-4ab6-9e2d-cfa484e32518"), new DateTime(2023, 5, 17, 18, 54, 30, 589, DateTimeKind.Local).AddTicks(3891), null, "Sr. Manager of HR", 1, null },
                    { new Guid("9e71b9c4-4999-4442-96bd-5c8878a58c6c"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7252), null, "Project Manager", 1, null },
                    { new Guid("9ec1898c-de8c-453b-81ba-a4ef3f6635b4"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7247), null, "Hotel Receptionist", 1, null },
                    { new Guid("a130488d-ef46-4f37-ba14-9ce1c6670f74"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7162), null, "Sr. Manager of HR", 1, null },
                    { new Guid("a27e1694-ffa4-45a7-9055-da6aba8d1350"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7136), null, "Sales Associate", 1, null },
                    { new Guid("a58c0cc6-60b8-4be6-adb2-9b1ad284fb4f"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7128), null, "VP of Sales", 1, null },
                    { new Guid("a820ced9-fdc2-49e5-ba3d-68efbf40902d"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7253), null, "Contract Administrator", 1, null },
                    { new Guid("ae825905-854b-418c-8402-f5499169c31b"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7254), null, "Project Appraisal Engineer", 1, null },
                    { new Guid("b68f37ce-4601-4157-a036-c6262379323f"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7158), null, "Support Specialist", 1, null },
                    { new Guid("bf666aa1-bc54-458b-9a5f-cdf7dc2d1466"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7250), null, "Construction Foreman", 1, null },
                    { new Guid("c507020c-598b-4688-ae79-3b2c30af7236"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7171), null, "Other Industries:", 1, null },
                    { new Guid("c91766c5-647b-4cc9-93e0-b1ae41974ca3"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7172), null, "Registered Nurse", 1, null },
                    { new Guid("cd85f944-881c-4643-bb8b-2503469720a1"), new Guid("f87aa45c-282d-42d6-9e84-b1690004c9ac"), new DateTime(2023, 5, 17, 18, 54, 30, 581, DateTimeKind.Local).AddTicks(2600), null, "Safety Director", 1, null },
                    { new Guid("d0247051-a72d-4b8d-b3e4-1f5fb2da6520"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7188), null, "General Manager", 1, null },
                    { new Guid("d07f3443-826f-47a0-9b88-46fbc98445f8"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7177), null, "Nursing Assistant", 1, null },
                    { new Guid("d1c0a948-9d2c-4edc-8b12-01ecd1c4f23d"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7151), null, "Risk Analyst", 1, null },
                    { new Guid("d53b3c09-869e-4752-b88a-33f542358e9e"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7183), null, "School Counselor", 1, null },
                    { new Guid("e1a72e61-9752-4785-8d9d-bbdf07da1022"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7150), null, "Credit Analyst", 1, null },
                    { new Guid("e2010782-7b7d-4b70-96bd-45b33be4e1f0"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7142), null, "Marketing Analyst", 1, null },
                    { new Guid("e46a3ffa-3ce7-4555-966f-0605ec8d7e91"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7140), null, "Marketing Director", 1, null },
                    { new Guid("e4a307a8-8150-4980-8e9d-712d8ac103ee"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7148), null, "Procurement Director", 1, null },
                    { new Guid("e67bc954-cf26-4075-ad69-9b07c362dd05"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7182), null, "Registrar", 1, null },
                    { new Guid("e8dafaf9-5a42-44c2-b346-cadf2cb1831c"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7176), null, "Physical Therapist", 1, null },
                    { new Guid("f413211e-9144-4ad3-a5ff-38df84f3a181"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7168), null, "Full Stack Developer", 1, null },
                    { new Guid("f7acb68c-0aa8-48c1-9328-845a51e32325"), new Guid("4947e306-503c-4c10-9b1b-8c9941daf0ee"), new DateTime(2023, 5, 17, 18, 54, 30, 590, DateTimeKind.Local).AddTicks(7674), null, "Sr. Manager of HR", 1, null },
                    { new Guid("fb015809-3919-4bb7-b788-a736e7131ea0"), new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7160), null, "Director of Business Operations", 1, null },
                    { new Guid("fe2057fa-023c-4f36-b3e2-b9b0b40805c9"), new Guid("ed277d2d-929d-430f-a58d-8743579f714d"), new DateTime(2023, 5, 17, 18, 54, 30, 588, DateTimeKind.Local).AddTicks(335), null, "Server/Host/Hostess", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "BirthDate", "BloodGroup", "CompanyId", "ConcurrencyStamp", "CreateDate", "DeleteDate", "Email", "EmailConfirmed", "IdentityNumber", "ImagePath", "JobStartDate", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profession", "SecondName", "SecurityStamp", "Status", "Surname", "TitleId", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("05c382d2-0112-4410-9f0c-1f6133ae1ebe"), 0, null, new DateTime(2043, 4, 16, 18, 54, 30, 585, DateTimeKind.Local).AddTicks(3139), null, new Guid("0492de86-03f9-47d6-9b3b-90d7bec20aad"), "558cac69-8cf8-4652-a6c0-ef7afa65c658", new DateTime(2023, 5, 17, 18, 54, 30, 585, DateTimeKind.Local).AddTicks(3138), null, "huseyin.yildirim@google.com", false, "67638061636", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "HUSEYIN.YILDIRIM@GOOGLE.COM", "HUSEYIN.YILDIRIM@GOOGLE.COM", "AQAAAAEAACcQAAAAEKNx/BqdzzW/E4uHVOkbqyPEprDSmwXMZhiBkNM9ilyWaIBOwPA5TRhmcD9LXchFQA==", "+905980603296", false, "Veterinary doctor(Vet)", null, "N8GIL3RM8OPT3AYD1DMPXBAMOFXI5XIC", 3, "Yıldırım", new Guid("0379e912-28cd-4d84-b524-45cb18734aef"), false, null, "huseyin.yildirim@google.com" },
                    { new Guid("1bde8fad-93b8-4200-bcb5-45bb4669a0b7"), 0, null, new DateTime(2060, 10, 27, 18, 54, 30, 578, DateTimeKind.Local).AddTicks(5592), null, new Guid("9bdfbdf2-81d0-4f21-a1d7-bb099852efa0"), "421f75c9-09ac-465c-88e1-09981ccec0b5", new DateTime(2023, 5, 17, 18, 54, 30, 578, DateTimeKind.Local).AddTicks(5589), null, "osman.celik@outlook.com", false, "15805608238", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "OSMAN.CELIK@OUTLOOK.COM", "OSMAN.CELIK@OUTLOOK.COM", "AQAAAAEAACcQAAAAEE330wb6sP3kw4ZrJmJOMF53nQIRGvX3qVo3nNr5INEO0KiFUSW4Q54NkHiyjQGECA==", "+905417036372", false, "Politician", null, "IVO6ZWRKRQ2BMGOY4PIH82QOOOE695DN", 3, "Çelik", new Guid("21abe347-3ac0-4370-8ecd-8d4892c50f48"), false, null, "osman.celik@outlook.com" },
                    { new Guid("26371c2a-2a09-49d4-8f8c-6e53875f67c7"), 0, null, new DateTime(2051, 6, 15, 18, 54, 30, 589, DateTimeKind.Local).AddTicks(3897), null, new Guid("bef292cb-0219-4ab6-9e2d-cfa484e32518"), "4d6f0979-4440-4aa5-a9ea-0c42c4e2efa3", new DateTime(2023, 5, 17, 18, 54, 30, 589, DateTimeKind.Local).AddTicks(3896), null, "ismail.yildiz@google.com", false, "43377275312", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.YILDIZ@GOOGLE.COM", "ISMAIL.YILDIZ@GOOGLE.COM", "AQAAAAEAACcQAAAAEE72y/aVU5toN0fxAB+snygYSJzQPgyZjVmFd5/Ko4gWfEm3GFVQY8NSoEAJsch/8A==", "+905237054081", false, "Taxi driver", null, "U12VQQ6V999FVRH12GKNWFU7CCGGH79M", 3, "Yıldız", new Guid("9af97c85-6c39-44d2-b398-3c4d0cc9c39f"), false, null, "ismail.yildiz@google.com" },
                    { new Guid("43e8bdbd-9b7a-4bde-9141-07d30c0134bf"), 0, null, new DateTime(2049, 1, 24, 18, 54, 30, 592, DateTimeKind.Local).AddTicks(1342), null, new Guid("9f9442ac-121d-452a-a0b9-49a35e5b3d25"), "3921474a-66e3-4614-aa2f-00151e93beec", new DateTime(2023, 5, 17, 18, 54, 30, 592, DateTimeKind.Local).AddTicks(1341), null, "ahmet.celik@yandex.com", false, "50714686502", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ahmet", "AHMET.CELIK@YANDEX.COM", "AHMET.CELIK@YANDEX.COM", "AQAAAAEAACcQAAAAENit4q7stwpm5fhSUO30/kGxcO2Vkru2PI2sqVeTbvPxsn64UkFqQtWr1ORp3MtWkw==", "+905681592467", false, "Taxi driver", null, "O35R3QTZUKTUN4M0RYVISSMUP6O1DHFQ", 3, "Çelik", new Guid("5dff7060-b04a-4d39-a32a-4ec13734e4e1"), false, null, "ahmet.celik@yandex.com" },
                    { new Guid("442e0300-e362-40eb-adbf-59c6014b3263"), 0, null, new DateTime(2054, 3, 29, 18, 54, 30, 574, DateTimeKind.Local).AddTicks(4460), null, new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), "e9da5c99-f218-4e47-8068-f51b55661823", new DateTime(2023, 5, 17, 18, 54, 30, 574, DateTimeKind.Local).AddTicks(4458), null, "test3@test.com", false, "16546318554", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Yusuf", "TEST3@TEST.COM", "TEST3@TEST.COM", "AQAAAAEAACcQAAAAEDT4tDoxJxf1IbZqc6kVj1pPjvTh01T6let0ZkVLNiLx6KRJY8BipV5AsaM+2JR0tg==", "+905526569099", false, "Chef/Cook", null, "SE7X5MO5G64G736Q0RYJZJ3Z2DN74KDG", 1, "Öztürk", new Guid("66064dc4-0fe2-4199-b0f2-5001a76dda9c"), false, null, "test3@test.com" },
                    { new Guid("47c75cb6-0d05-453a-b780-ea471e175512"), 0, null, new DateTime(2058, 8, 13, 18, 54, 30, 586, DateTimeKind.Local).AddTicks(6834), null, new Guid("a2163d66-55a5-48ff-94b8-9c6c75db4334"), "738f263e-4c88-48a3-ba53-5034773b0ba6", new DateTime(2023, 5, 17, 18, 54, 30, 586, DateTimeKind.Local).AddTicks(6832), null, "mustafa.celik@outlook.com", false, "56880484508", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mustafa", "MUSTAFA.CELIK@OUTLOOK.COM", "MUSTAFA.CELIK@OUTLOOK.COM", "AQAAAAEAACcQAAAAEBpyADiPoZZ5dAz5MYSOOhEMHpANeh4oGtQmvGfTRXeuzDCXHCp/h3PNq0F4fPeEDg==", "+905332369714", false, "Teacher", null, "A2PUR739IXCNK77HWC2KCM1AB6JXFI21", 3, "Çelik", new Guid("313d90f6-4858-4dc4-9858-040784c72329"), false, null, "mustafa.celik@outlook.com" },
                    { new Guid("4b40736d-4b14-46d4-8621-fed8c14c7e93"), 0, null, new DateTime(2059, 3, 15, 18, 54, 30, 581, DateTimeKind.Local).AddTicks(2606), null, new Guid("f87aa45c-282d-42d6-9e84-b1690004c9ac"), "19dcb8d4-7a8a-4dda-95df-0e4fbf378fbe", new DateTime(2023, 5, 17, 18, 54, 30, 581, DateTimeKind.Local).AddTicks(2605), null, "ali.ozdemir@yahoo.com", false, "41053278336", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ali", "ALI.OZDEMIR@YAHOO.COM", "ALI.OZDEMIR@YAHOO.COM", "AQAAAAEAACcQAAAAEKKB0rNqjp1kIGJHPr00MHFNnKIypAsgu6RcZ0J+tJgqq7Jg4wwuh71JHc44tPCdsg==", "+905783923045", false, "Lawyer", null, "5326K46A6IQN5YDRZXNJ2UMQ2EBA6S56", 3, "Özdemir", new Guid("cd85f944-881c-4643-bb8b-2503469720a1"), false, null, "ali.ozdemir@yahoo.com" },
                    { new Guid("552fc1e4-f1cd-40c0-9e4a-da453911a08c"), 0, null, new DateTime(2048, 1, 5, 18, 54, 30, 573, DateTimeKind.Local).AddTicks(928), null, new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), "d63d286c-d6af-4617-85a9-23f5d8d1a13d", new DateTime(2023, 5, 17, 18, 54, 30, 573, DateTimeKind.Local).AddTicks(926), null, "test2@test.com", false, "63683811646", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hasan", "TEST2@TEST.COM", "TEST2@TEST.COM", "AQAAAAEAACcQAAAAEMmiquQj5nKFSqX3REEdvI4Lq+ysFI0qys/kKzYfSL76i0CKOTaL3xh7rj4k/t6XJA==", "+905445781664", false, "Baker", null, "R5949IZB32U4M1U0SEW7YU24XKHVHHF8", 1, "Yılmaz", new Guid("e67bc954-cf26-4075-ad69-9b07c362dd05"), false, null, "test2@test.com" },
                    { new Guid("7be0ce55-0f35-4a92-95e6-aa438d744525"), 0, null, new DateTime(2048, 12, 3, 18, 54, 30, 577, DateTimeKind.Local).AddTicks(1709), null, new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), "32193589-3daa-4691-bebe-04a1c3cab16e", new DateTime(2023, 5, 17, 18, 54, 30, 577, DateTimeKind.Local).AddTicks(1706), null, "test5@test.com", false, "81702767828", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mustafa", "TEST5@TEST.COM", "TEST5@TEST.COM", "AQAAAAEAACcQAAAAEB+AHkXvAnTPoA3hNqkNFVoLJlOm6jS+JLAU5rJub0xjkspAishMTbyGPkhaQow9nA==", "+905828893862", false, "Waiter/Waitress", null, "E7J53Z52KWG7A4Q8R3CLJUYGZKUTH26H", 1, "Çelik", new Guid("648da832-89f7-4f1b-80d9-ee72a5de2aec"), false, null, "test5@test.com" },
                    { new Guid("7f435165-d8d8-4b81-a6ba-9275645681d7"), 0, null, new DateTime(2050, 12, 4, 18, 54, 30, 590, DateTimeKind.Local).AddTicks(7690), null, new Guid("4947e306-503c-4c10-9b1b-8c9941daf0ee"), "ec4f5367-05cd-479d-b7ae-96c23bcba4a4", new DateTime(2023, 5, 17, 18, 54, 30, 590, DateTimeKind.Local).AddTicks(7688), null, "mehmet.aydin@yandex.com", false, "14337466330", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mehmet", "MEHMET.AYDIN@YANDEX.COM", "MEHMET.AYDIN@YANDEX.COM", "AQAAAAEAACcQAAAAEJxRD+4D/rH4OskftuRfYjpHMYF+0VE6hxL8lP0x8rrs7aIm+PyfJyUu39UBc2BB7A==", "+905146214009", false, "Traffic warden", null, "B99CG1XLT1U10WVMPCUH9SVVWYJP0W3F", 3, "Aydın", new Guid("f7acb68c-0aa8-48c1-9328-845a51e32325"), false, null, "mehmet.aydin@yandex.com" },
                    { new Guid("8f275260-4249-4a04-8380-e32f2f7a883c"), 0, null, new DateTime(2047, 12, 1, 18, 54, 30, 579, DateTimeKind.Local).AddTicks(9032), null, new Guid("1c6a0fd5-8a98-44de-9be3-42ba1f8a2624"), "72e4f6e2-80e7-4e33-b411-fdf8d68bd3e0", new DateTime(2023, 5, 17, 18, 54, 30, 579, DateTimeKind.Local).AddTicks(9031), null, "hasan.demir@outlook.com", false, "62500213694", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hasan", "HASAN.DEMIR@OUTLOOK.COM", "HASAN.DEMIR@OUTLOOK.COM", "AQAAAAEAACcQAAAAEN+DgnDfNpPLJLE1/CrdxMraCjW6RxaqPHT9kDNI0TGKWTWb/ZfbPgvE/v15tWFh1Q==", "+905746431935", false, "Hairdresser", null, "8CHHVAKAYZQRZ0HUEPS7JWZ3UPDXZYJ5", 3, "Demir", new Guid("49809b9a-0a60-4751-b90b-f3cb35619eb2"), false, null, "hasan.demir@outlook.com" },
                    { new Guid("90f75b29-1bce-4a66-9d5f-7bb404117ddb"), 0, null, new DateTime(2067, 2, 26, 18, 54, 30, 596, DateTimeKind.Local).AddTicks(2251), null, new Guid("16ddd30c-f66f-472c-8612-d81bd6e40d3a"), "4bf9250d-1feb-4cb6-bf34-9ddda947f194", new DateTime(2023, 5, 17, 18, 54, 30, 596, DateTimeKind.Local).AddTicks(2249), null, "ismail.ozdemir@google.com", false, "21432080088", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.OZDEMIR@GOOGLE.COM", "ISMAIL.OZDEMIR@GOOGLE.COM", "AQAAAAEAACcQAAAAEFSF3kKWgRoxunfYsIVgShtqZNlxg0O4URwZMI0Y3hWZRbvzDL9aOJxMtnvjgw2KzA==", "+905937238732", false, "Lawyer", null, "6HD4R5Z1G8Y8QUIJZH8DFWOJWY3VPZCC", 3, "Özdemir", new Guid("60d00f1b-6d11-4fde-bf0b-221d9039ade1"), false, null, "ismail.ozdemir@google.com" },
                    { new Guid("97c9c5e5-1a22-40f4-987c-8c4b67720d83"), 0, null, new DateTime(2041, 5, 30, 18, 54, 30, 594, DateTimeKind.Local).AddTicks(8727), null, new Guid("c2fb3767-381b-4210-80bc-0ae4e03e5947"), "3ff32930-f2f3-43fe-8e9b-01ad13cd7240", new DateTime(2023, 5, 17, 18, 54, 30, 594, DateTimeKind.Local).AddTicks(8726), null, "ismail.kaya@yahoo.com", false, "83000802416", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.KAYA@YAHOO.COM", "ISMAIL.KAYA@YAHOO.COM", "AQAAAAEAACcQAAAAECbwzZ3j5ZmXfjigfHQi9bKwK4loQy1BaME8gAbP/Vw0CNDuOcSjx+hE+trOXnqMkg==", "+905619582916", false, "Busdriver", null, "MFT2N7VVLMJ5T5ULFYRBZ8QJ4LBCNCC3", 3, "Kaya", new Guid("5b4a61d1-d1ba-4c83-a6cf-c815a60bdcf0"), false, null, "ismail.kaya@yahoo.com" },
                    { new Guid("ae4debf9-8d08-4d9d-8a56-3a3e7393e8eb"), 0, null, new DateTime(2065, 5, 11, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7272), null, new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), "ce6d08b7-bc92-4252-9b63-0ae321923b08", new DateTime(2023, 5, 17, 18, 54, 30, 571, DateTimeKind.Local).AddTicks(7269), null, "test1@test.com", false, "27233514524", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hasan", "TEST1@TEST.COM", "TEST1@TEST.COM", "AQAAAAEAACcQAAAAENl5qJRBwW1BhFb9Mjsp9UyrvUjGJLm0ZVEjYJGnebg6q+GzbB3lkOQ/4hfr7uCcCw==", "+905688134079", false, "Fireman/Fire fighter", null, "G1HYBDBB1VOIARZZR2CZL8065A2CIA98", 1, "Şahin", new Guid("e46a3ffa-3ce7-4555-966f-0605ec8d7e91"), false, null, "test1@test.com" },
                    { new Guid("d6b1e09f-0b00-4376-a911-cae5a130be21"), 0, null, new DateTime(2055, 6, 4, 18, 54, 30, 575, DateTimeKind.Local).AddTicks(7962), null, new Guid("7d15051b-2dfa-4251-86b2-8e1060820046"), "50b2ff4a-1db1-409e-8818-5a56e3a6a5fc", new DateTime(2023, 5, 17, 18, 54, 30, 575, DateTimeKind.Local).AddTicks(7961), null, "test4@test.com", false, "42624532086", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "TEST4@TEST.COM", "TEST4@TEST.COM", "AQAAAAEAACcQAAAAEC6x2KqC6DtDF1SD98GCimMJ57R75LaFpPI9bLPLVH9HCKKfSxQ721PpPrS5fmbDWA==", "+905916155493", false, "Electrician", null, "5L4LZKGA8YYKYNP635GCJ59V8UHYOTSJ", 1, "Öztürk", new Guid("e46a3ffa-3ce7-4555-966f-0605ec8d7e91"), false, null, "test4@test.com" },
                    { new Guid("e4a69843-c78b-4a13-a103-1ec47b9e1122"), 0, null, new DateTime(2044, 5, 8, 18, 54, 30, 597, DateTimeKind.Local).AddTicks(6174), null, new Guid("5e6c2342-a590-4013-a6bb-136808e2c4c9"), "d0a796ca-e963-4755-b1ba-c573369213bc", new DateTime(2023, 5, 17, 18, 54, 30, 597, DateTimeKind.Local).AddTicks(6172), null, "mehmet.aydin@google.com", false, "58704643726", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mehmet", "MEHMET.AYDIN@GOOGLE.COM", "MEHMET.AYDIN@GOOGLE.COM", "AQAAAAEAACcQAAAAEObAF0BA4IX/r43ceajpWP2ux4NFX5J29J4bFan9QjtMA17gnogkkT/qQtQbEJJl8w==", "+905589588212", false, "Postman", null, "L4CGH3CRQDSKYO6TKH3NY7MSLARA4CRA", 3, "Aydın", new Guid("2e6e1880-4847-4564-806f-2ba6bad7250a"), false, null, "mehmet.aydin@google.com" },
                    { new Guid("eb5cd4f2-b72a-4fa1-b245-257ca19d461a"), 0, null, new DateTime(2066, 10, 18, 18, 54, 30, 593, DateTimeKind.Local).AddTicks(5040), null, new Guid("c5d106e0-4b17-494c-a5f0-0375baa585ee"), "635b6426-867e-4051-902f-89eb71d8f9b7", new DateTime(2023, 5, 17, 18, 54, 30, 593, DateTimeKind.Local).AddTicks(5039), null, "huseyin.celik@outlook.com", false, "87280766048", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "HUSEYIN.CELIK@OUTLOOK.COM", "HUSEYIN.CELIK@OUTLOOK.COM", "AQAAAAEAACcQAAAAEK215fJIoVtyeOfA0zD5P3Eq9iNZCjgRnPslXcOq/C6Ygq6h6szGIRTIYyzZsyovPQ==", "+905286065777", false, "Dancer", null, "GLA2X5C4RIWUSDUVHX3FY9G6ZV7WTJSG", 3, "Çelik", new Guid("4db5b648-dcc8-49e5-9c68-9c6f5f1c2433"), false, null, "huseyin.celik@outlook.com" },
                    { new Guid("ee4fc5aa-8731-4359-bd97-f28c2d679f54"), 0, null, new DateTime(2057, 8, 15, 18, 54, 30, 582, DateTimeKind.Local).AddTicks(6132), null, new Guid("3bc14794-acbf-4da5-8290-1a3c4b9c1b80"), "bd28f1bd-37c7-4a61-ace5-17b96e3af8d2", new DateTime(2023, 5, 17, 18, 54, 30, 582, DateTimeKind.Local).AddTicks(6131), null, "ibrahim.yilmaz@google.com", false, "88837347222", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "IBRAHIM.YILMAZ@GOOGLE.COM", "IBRAHIM.YILMAZ@GOOGLE.COM", "AQAAAAEAACcQAAAAEAFEkzUwU7LHQuLAwnu/5RSQMYqVlRwEpcjHfHXKJ61UXpuoF86xtff3JENoY3KDrw==", "+905268491750", false, "Cleaner", null, "Z1VBK9LIR1ANDR02WBM5IBM75XNU1DPG", 3, "Yılmaz", new Guid("50bad660-a1c8-4342-b340-4bdba4234560"), false, null, "ibrahim.yilmaz@google.com" },
                    { new Guid("ef438a07-a60d-4cd7-b08c-f2739de09cf4"), 0, null, new DateTime(2069, 1, 29, 18, 54, 30, 583, DateTimeKind.Local).AddTicks(9531), null, new Guid("71c334c9-c0c8-49f7-847a-829af84c46c8"), "df194d57-b32b-4887-8418-81f4a44ecd9d", new DateTime(2023, 5, 17, 18, 54, 30, 583, DateTimeKind.Local).AddTicks(9530), null, "huseyin.kaya@outlook.com", false, "36265057688", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "HUSEYIN.KAYA@OUTLOOK.COM", "HUSEYIN.KAYA@OUTLOOK.COM", "AQAAAAEAACcQAAAAEBNxZYYOOWjIW7QqGNmfDjKlrIt1sOYIbgFcATijmNTA9cw71LHXeZKTbCvg+yDh1w==", "+905978431955", false, "Artist", null, "KWV2T5HGGTVW01G704BNUUL6L95PG4V5", 3, "Kaya", new Guid("4823e46f-4bea-42ed-bba2-3369ff01f13b"), false, null, "huseyin.kaya@outlook.com" },
                    { new Guid("fe004e02-4a7b-4439-b8b7-996967e8e1af"), 0, null, new DateTime(2045, 1, 25, 18, 54, 30, 588, DateTimeKind.Local).AddTicks(347), null, new Guid("ed277d2d-929d-430f-a58d-8743579f714d"), "ae74372c-3565-4c13-98ec-f0a509953c39", new DateTime(2023, 5, 17, 18, 54, 30, 588, DateTimeKind.Local).AddTicks(346), null, "mehmet.sahin@outlook.com", false, "82512041542", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mehmet", "MEHMET.SAHIN@OUTLOOK.COM", "MEHMET.SAHIN@OUTLOOK.COM", "AQAAAAEAACcQAAAAEAFWWJ+VBqE8EDZ4UBp2kTtP16Muja8eKeaOWTQyfjfIUhI3LGNNyu5XNxklutiGvA==", "+905803217031", false, "Designer", null, "NBOPLXUJHBRROXUPWU7PBSUO7VAJD3L0", 3, "Şahin", new Guid("fe2057fa-023c-4f36-b3e2-b9b0b40805c9"), false, null, "mehmet.sahin@outlook.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("05c382d2-0112-4410-9f0c-1f6133ae1ebe") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("1bde8fad-93b8-4200-bcb5-45bb4669a0b7") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("26371c2a-2a09-49d4-8f8c-6e53875f67c7") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("43e8bdbd-9b7a-4bde-9141-07d30c0134bf") },
                    { new Guid("7f77b47e-a272-4ead-8a88-1c3b4f0b2d21"), new Guid("442e0300-e362-40eb-adbf-59c6014b3263") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("47c75cb6-0d05-453a-b780-ea471e175512") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("4b40736d-4b14-46d4-8621-fed8c14c7e93") },
                    { new Guid("7f77b47e-a272-4ead-8a88-1c3b4f0b2d21"), new Guid("552fc1e4-f1cd-40c0-9e4a-da453911a08c") },
                    { new Guid("7f77b47e-a272-4ead-8a88-1c3b4f0b2d21"), new Guid("7be0ce55-0f35-4a92-95e6-aa438d744525") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("7f435165-d8d8-4b81-a6ba-9275645681d7") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("8f275260-4249-4a04-8380-e32f2f7a883c") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("90f75b29-1bce-4a66-9d5f-7bb404117ddb") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("97c9c5e5-1a22-40f4-987c-8c4b67720d83") },
                    { new Guid("7f77b47e-a272-4ead-8a88-1c3b4f0b2d21"), new Guid("ae4debf9-8d08-4d9d-8a56-3a3e7393e8eb") },
                    { new Guid("7f77b47e-a272-4ead-8a88-1c3b4f0b2d21"), new Guid("d6b1e09f-0b00-4376-a911-cae5a130be21") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("e4a69843-c78b-4a13-a103-1ec47b9e1122") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("eb5cd4f2-b72a-4fa1-b245-257ca19d461a") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("ee4fc5aa-8731-4359-bd97-f28c2d679f54") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("ef438a07-a60d-4cd7-b08c-f2739de09cf4") },
                    { new Guid("72f29bd1-4d04-436b-adf9-6154b5b3cc8b"), new Guid("fe004e02-4a7b-4439-b8b7-996967e8e1af") }
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
