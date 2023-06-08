using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKApplication.Persistance.Migrations
{
    public partial class first : Migration
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
                    PersonalEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatronId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        onDelete: ReferentialAction.NoAction);
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
                    InstallmentCount = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), "Y99IVM35FTRSD3J9CRQNP9XFGEGKJFWT", "Personal", "PERSONAL" },
                    { new Guid("9cb6c260-d902-496d-b625-131487ec9b6c"), "6MTQKLQK80Z4X49I5W06GNBEP5PGQH1Q", "Company Administrator", "COMPANY ADMINISTRATOR" },
                    { new Guid("d023341d-79ee-41aa-a5de-05d399e23b86"), "Z9ZV0KIPN7R3XNZIMIXJE7DY5AC7K51D", "Site Administrator", "SITE ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("0c859a69-ec31-4ab1-aaf9-271e20871acd"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(481), null, "Turizm, Konaklama, Yiyecek-İçecek Hizmetleri", 1, null },
                    { new Guid("0f040952-18ef-434f-bb2c-f0246f9af9ed"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(463), null, "Maden", 1, null },
                    { new Guid("1c82c712-e462-43f9-90f6-f21463a2913f"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(446), null, "Eğitim", 1, null },
                    { new Guid("20153450-cefa-44c1-be30-46e6e7a2ac13"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(476), null, "Tekstil, Hazır Giyim, Deri", 1, null },
                    { new Guid("29a72054-fa36-4ab2-9fef-7033328103c1"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(456), null, "Gıda", 1, null },
                    { new Guid("2df1c098-52d8-42b5-89e4-a3fca272f69a"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(462), null, "Kültür, Sanat ve Tasarım", 1, null },
                    { new Guid("3c67fb60-1716-4696-98eb-200da769f5d0"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(461), null, "Kimya, Petrol, Lastik ve PLastik", 1, null },
                    { new Guid("3f793886-9412-41ff-8504-12fda2fe9059"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(450), null, "Enerji", 1, null },
                    { new Guid("56673078-ed77-4644-8f33-75fe6152aa89"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(432), null, "Ahşap Teknolojisi", 1, null },
                    { new Guid("7dc2f156-043e-4ded-85fa-ebc9510bcfc6"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(445), null, "Cam, Çimento ve ToprakÇevre", 1, null },
                    { new Guid("82f8afc7-ecbd-4372-bd1c-eaf1049f488e"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(482), null, "Ulaştırma, Lojistik ve Haberleşme", 1, null },
                    { new Guid("8a782ed8-cd0e-4fd2-877a-35d28c9d569e"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(473), null, "Spor ve Rekreasyon", 1, null },
                    { new Guid("902955b9-c884-445b-9972-a18275d6faaf"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(447), null, "Elektrik ve Elektronik", 1, null },
                    { new Guid("92c309dd-fb08-4e79-b701-714e7e30b898"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(474), null, "Tarım, Avcılık ve Balıkçılık", 1, null },
                    { new Guid("97a3cc48-5c0b-4ad9-85ff-c3f84bcda13d"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(470), null, "Otomotiv", 1, null },
                    { new Guid("a32782c8-8958-4683-bec3-987f49d4346a"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(465), null, "Medya, İletişim ve Yayıncılık", 1, null },
                    { new Guid("ac549283-9c4e-44b6-a801-3d8c169b75f8"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(477), null, "Ticaret (Satış ve Pazarlama)", 1, null },
                    { new Guid("bd692903-3476-402b-9fd6-7a721970ac2b"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(455), null, "Finans", 1, null },
                    { new Guid("cb332685-3d1f-4b5a-8c06-126358aa3933"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(471), null, "Sağlık ve Sosyal Hizmetler", 1, null },
                    { new Guid("d306a5ce-0ae2-4afe-903c-a663abfb38a2"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(457), null, "İnşaat", 1, null },
                    { new Guid("df52ae8f-5a0d-4324-bacb-fca1b5b88b52"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(478), null, "Toplumsal ve Kişisel Hizmetler", 1, null },
                    { new Guid("e3d3b6af-5afe-40ea-b052-621bd8abd147"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(459), null, "İş ve Yönetimi", 1, null },
                    { new Guid("e6395c55-3d81-4d4b-8cd4-5dddbcc76ac9"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(468), null, "Metal", 1, null },
                    { new Guid("f48939c2-4df2-46ac-b052-1f797cebc079"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(443), null, "Bilişim", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "NumberOfEmployees", "PhoneNumber", "SectorId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), new DateTime(2023, 6, 8, 15, 21, 7, 195, DateTimeKind.Local).AddTicks(4890), null, "info@Demir.com", "Demir Kollektif Şirketi", 12, "+905598518168", new Guid("e6395c55-3d81-4d4b-8cd4-5dddbcc76ac9"), 1, null },
                    { new Guid("41e8e9e1-0d8d-47b5-a794-449367e713f0"), new DateTime(2023, 6, 8, 15, 21, 7, 143, DateTimeKind.Local).AddTicks(6955), null, "info@Yılmaz.com", "Yılmaz Kollektif Şirketi", 9, "+905555445250", new Guid("cb332685-3d1f-4b5a-8c06-126358aa3933"), 1, null },
                    { new Guid("491c53fb-7e75-4623-95c3-6932736a1f8c"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(485), null, "hrapp@gmail.com", "HRApp A.Ş.", 1, "+905555555555", new Guid("f48939c2-4df2-46ac-b052-1f797cebc079"), 1, null },
                    { new Guid("87bf7d92-a661-47fa-b1ff-8300dab57f02"), new DateTime(2023, 6, 8, 15, 21, 7, 266, DateTimeKind.Local).AddTicks(410), null, "info@Demir.com", "Demir Kollektif Şirketi", 2, "+905413955440", new Guid("e6395c55-3d81-4d4b-8cd4-5dddbcc76ac9"), 1, null },
                    { new Guid("97b0e12c-a4db-4432-91fb-fe36ad944734"), new DateTime(2023, 6, 8, 15, 21, 7, 277, DateTimeKind.Local).AddTicks(6837), null, "info@Öztürk.com", "Öztürk Anonim Şirketi", 1, "+905194780290", new Guid("0f040952-18ef-434f-bb2c-f0246f9af9ed"), 1, null },
                    { new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), new DateTime(2023, 6, 8, 15, 21, 7, 44, DateTimeKind.Local).AddTicks(8705), null, "info@Aydın.com", "Aydın Komandit Şirketi", 6, "+905320130022", new Guid("e6395c55-3d81-4d4b-8cd4-5dddbcc76ac9"), 1, null },
                    { new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), new DateTime(2023, 6, 8, 15, 21, 7, 79, DateTimeKind.Local).AddTicks(9817), null, "info@Aydın.com", "Aydın Limited Şirketi", 10, "+905445485000", new Guid("ac549283-9c4e-44b6-a801-3d8c169b75f8"), 1, null }
                });

            migrationBuilder.InsertData(
                table: "Professions",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("186c06ef-fd33-40ec-a4dd-5261392a611f"), new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), new DateTime(2023, 6, 8, 15, 21, 7, 79, DateTimeKind.Local).AddTicks(9850), null, "Travel agent", 1, null },
                    { new Guid("1eb9f494-f739-41be-a01e-f7f89e801fd6"), new Guid("97b0e12c-a4db-4432-91fb-fe36ad944734"), new DateTime(2023, 6, 8, 15, 21, 7, 277, DateTimeKind.Local).AddTicks(6863), null, "Veterinary doctor(Vet)", 1, null },
                    { new Guid("250096d2-cd8e-4707-9178-33db614f55fd"), new Guid("87bf7d92-a661-47fa-b1ff-8300dab57f02"), new DateTime(2023, 6, 8, 15, 21, 7, 266, DateTimeKind.Local).AddTicks(445), null, "Businessman", 1, null },
                    { new Guid("2683fca0-1500-4930-a1fc-625c70d5f80e"), new Guid("97b0e12c-a4db-4432-91fb-fe36ad944734"), new DateTime(2023, 6, 8, 15, 21, 7, 277, DateTimeKind.Local).AddTicks(6858), null, "Optician", 1, null },
                    { new Guid("329c0993-ef54-408e-a63a-0e13f18486ab"), new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), new DateTime(2023, 6, 8, 15, 21, 7, 195, DateTimeKind.Local).AddTicks(4917), null, "Politician", 1, null },
                    { new Guid("4bba6b3a-cc56-4a3d-8c19-6e9c28ffd7d7"), new Guid("41e8e9e1-0d8d-47b5-a794-449367e713f0"), new DateTime(2023, 6, 8, 15, 21, 7, 143, DateTimeKind.Local).AddTicks(6976), null, "Taxi driver", 1, null },
                    { new Guid("4cf5f02a-abfc-494a-972f-5ebc6730d6d0"), new Guid("491c53fb-7e75-4623-95c3-6932736a1f8c"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(494), null, "Engineer", 1, null },
                    { new Guid("599d587d-da26-410c-a17d-557d82577dbc"), new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), new DateTime(2023, 6, 8, 15, 21, 7, 79, DateTimeKind.Local).AddTicks(9852), null, "Artist", 1, null },
                    { new Guid("6174100b-cad5-43ef-859c-94ab0d350b9c"), new Guid("87bf7d92-a661-47fa-b1ff-8300dab57f02"), new DateTime(2023, 6, 8, 15, 21, 7, 266, DateTimeKind.Local).AddTicks(438), null, "Tailor", 1, null },
                    { new Guid("6b35dbbb-3b1a-4b80-8c51-b8bea094486f"), new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), new DateTime(2023, 6, 8, 15, 21, 7, 195, DateTimeKind.Local).AddTicks(4923), null, "Veterinary doctor(Vet)", 1, null },
                    { new Guid("72ee09f0-8c56-4d17-838d-aee2bc3abfdc"), new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), new DateTime(2023, 6, 8, 15, 21, 7, 44, DateTimeKind.Local).AddTicks(8726), null, "Librarian", 1, null },
                    { new Guid("9744520c-cf0d-4528-a166-6218e9af2a03"), new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), new DateTime(2023, 6, 8, 15, 21, 7, 79, DateTimeKind.Local).AddTicks(9845), null, "Newsreader", 1, null },
                    { new Guid("b070b405-067e-4596-8597-094d8312c930"), new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), new DateTime(2023, 6, 8, 15, 21, 7, 44, DateTimeKind.Local).AddTicks(8736), null, "Dancer", 1, null },
                    { new Guid("b7bff893-ff74-41cc-ad42-866243bc36d6"), new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), new DateTime(2023, 6, 8, 15, 21, 7, 44, DateTimeKind.Local).AddTicks(8730), null, "Scientist", 1, null },
                    { new Guid("d95460ba-41ec-47ef-bae2-cd14ae39a12e"), new Guid("97b0e12c-a4db-4432-91fb-fe36ad944734"), new DateTime(2023, 6, 8, 15, 21, 7, 277, DateTimeKind.Local).AddTicks(6861), null, "Translator", 1, null },
                    { new Guid("e76f3f05-05db-48ba-99dc-0771d8857927"), new Guid("97b0e12c-a4db-4432-91fb-fe36ad944734"), new DateTime(2023, 6, 8, 15, 21, 7, 277, DateTimeKind.Local).AddTicks(6856), null, "Architect", 1, null },
                    { new Guid("e9a2ff85-922c-47bb-be28-f3bc386abb47"), new Guid("41e8e9e1-0d8d-47b5-a794-449367e713f0"), new DateTime(2023, 6, 8, 15, 21, 7, 143, DateTimeKind.Local).AddTicks(7032), null, "Travel agent", 1, null },
                    { new Guid("f242bd5c-86e7-4185-a6fd-2d7249f5fc52"), new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), new DateTime(2023, 6, 8, 15, 21, 7, 44, DateTimeKind.Local).AddTicks(8732), null, "Veterinary doctor(Vet)", 1, null },
                    { new Guid("f3b7b0ea-f881-4976-9e9c-cdfef6f84fbf"), new Guid("87bf7d92-a661-47fa-b1ff-8300dab57f02"), new DateTime(2023, 6, 8, 15, 21, 7, 266, DateTimeKind.Local).AddTicks(442), null, "Travel agent", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("0f7b9433-fdbe-41e0-9e2c-b3a66f855370"), new Guid("97b0e12c-a4db-4432-91fb-fe36ad944734"), new DateTime(2023, 6, 8, 15, 21, 7, 277, DateTimeKind.Local).AddTicks(6843), null, "Sales Representative", 1, null },
                    { new Guid("11eaa0e0-ec2a-4e50-8985-927c4254e465"), new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), new DateTime(2023, 6, 8, 15, 21, 7, 195, DateTimeKind.Local).AddTicks(4909), null, "Marketing Coordinator", 1, null },
                    { new Guid("1937bb0f-1a57-4f06-aacd-56252b283b65"), new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), new DateTime(2023, 6, 8, 15, 21, 7, 44, DateTimeKind.Local).AddTicks(8716), null, "Concierge", 1, null },
                    { new Guid("213173e9-120c-41d8-9cc2-8b4cc5d0b920"), new Guid("491c53fb-7e75-4623-95c3-6932736a1f8c"), new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(490), null, "CTO", 1, null },
                    { new Guid("28f5c387-4ec4-4fc1-a3bb-bd13c8dc35e8"), new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), new DateTime(2023, 6, 8, 15, 21, 7, 79, DateTimeKind.Local).AddTicks(9830), null, "Safety Director", 1, null },
                    { new Guid("41565cad-8a52-4881-a417-37823d62f261"), new Guid("87bf7d92-a661-47fa-b1ff-8300dab57f02"), new DateTime(2023, 6, 8, 15, 21, 7, 266, DateTimeKind.Local).AddTicks(432), null, "Credit Analyst", 1, null },
                    { new Guid("6b9998e0-26dd-47dd-8409-44ac2eabc781"), new Guid("97b0e12c-a4db-4432-91fb-fe36ad944734"), new DateTime(2023, 6, 8, 15, 21, 7, 277, DateTimeKind.Local).AddTicks(6846), null, "Hotel Receptionist", 1, null },
                    { new Guid("6bd790b7-38bb-4543-8eba-07ae5571ff8b"), new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), new DateTime(2023, 6, 8, 15, 21, 7, 44, DateTimeKind.Local).AddTicks(8721), null, "Hotel Receptionist", 1, null },
                    { new Guid("735bb164-231a-445c-9b37-320810e96cef"), new Guid("87bf7d92-a661-47fa-b1ff-8300dab57f02"), new DateTime(2023, 6, 8, 15, 21, 7, 266, DateTimeKind.Local).AddTicks(425), null, "VP of Finance", 1, null },
                    { new Guid("a1c62a12-a739-48b8-91fc-643e13f1d8fc"), new Guid("41e8e9e1-0d8d-47b5-a794-449367e713f0"), new DateTime(2023, 6, 8, 15, 21, 7, 143, DateTimeKind.Local).AddTicks(6969), null, "VP of Client Services", 1, null },
                    { new Guid("cf327271-8463-4d52-95f9-c800574e97f0"), new Guid("97b0e12c-a4db-4432-91fb-fe36ad944734"), new DateTime(2023, 6, 8, 15, 21, 7, 277, DateTimeKind.Local).AddTicks(6851), null, "Construction Foreman", 1, null },
                    { new Guid("ea37bb3f-f6a8-4c48-9706-09f25509f9d6"), new Guid("97b0e12c-a4db-4432-91fb-fe36ad944734"), new DateTime(2023, 6, 8, 15, 21, 7, 277, DateTimeKind.Local).AddTicks(6853), null, "Contract Administrator", 1, null },
                    { new Guid("f518d54f-7912-444f-83ca-54bfcd3ad1ec"), new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), new DateTime(2023, 6, 8, 15, 21, 7, 44, DateTimeKind.Local).AddTicks(8723), null, "Safety Director", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "BirthDate", "BloodGroup", "CompanyId", "ConcurrencyStamp", "CreateDate", "DeleteDate", "Email", "EmailConfirmed", "IdentityNumber", "ImagePath", "JobStartDate", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PatronId", "PersonalEmail", "PhoneNumber", "PhoneNumberConfirmed", "ProfessionId", "SecondName", "SecurityStamp", "Status", "Surname", "TitleId", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("420ec281-8dfc-4a30-bbd0-ec5ce059a1d6"), 0, null, new DateTime(2057, 10, 15, 15, 21, 7, 277, DateTimeKind.Local).AddTicks(6893), 6, new Guid("97b0e12c-a4db-4432-91fb-fe36ad944734"), "67f3c665-95bf-429b-b051-72d558f17e52", new DateTime(2023, 6, 8, 15, 21, 7, 277, DateTimeKind.Local).AddTicks(6891), null, "yusuf.ozturk40@ozturk.com", false, "74345186200", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 277, DateTimeKind.Local).AddTicks(6901), false, null, "Yusuf", "YUSUF.OZTURK40@OZTURK.COM", "YUSUF.OZTURK40@OZTURK.COM", "AQAAAAEAACcQAAAAEP+kEmo//wRKBPpCd2d9I9VJXuLrMiUBTk5A1MS5DMQF7j0xC3BTx4FQHH8NcF0o7Q==", new Guid("420ec281-8dfc-4a30-bbd0-ec5ce059a1d6"), "yusuf.ozturk@outlook.com", "+905521633650", false, new Guid("2683fca0-1500-4930-a1fc-625c70d5f80e"), null, "9S4TLKL1NQU6I58FVO1N9VMLLU2UTARC", 1, "Öztürk", new Guid("0f7b9433-fdbe-41e0-9e2c-b3a66f855370"), false, null, "yusuf.ozturk40@ozturk.com" },
                    { new Guid("6351578f-4937-4215-af1b-69d403431b03"), 0, null, new DateTime(2074, 3, 26, 15, 21, 7, 143, DateTimeKind.Local).AddTicks(7086), 6, new Guid("41e8e9e1-0d8d-47b5-a794-449367e713f0"), "f20806af-7d24-4e60-9edd-1e7a3f35ef1e", new DateTime(2023, 6, 8, 15, 21, 7, 143, DateTimeKind.Local).AddTicks(7083), null, "mehmet.yilmaz17@yilmaz.com", false, "46725641500", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 143, DateTimeKind.Local).AddTicks(7098), false, null, "Mehmet", "MEHMET.YILMAZ17@YILMAZ.COM", "MEHMET.YILMAZ17@YILMAZ.COM", "AQAAAAEAACcQAAAAEOBiyJQGckFsiRWV2Z/96p0NoN/Gu24gddB+qiPsvYOZfDvUS4Ac533uUL+s/BAIOg==", new Guid("6351578f-4937-4215-af1b-69d403431b03"), "mehmet.yilmaz@outlook.com", "+905462039742", false, new Guid("4bba6b3a-cc56-4a3d-8c19-6e9c28ffd7d7"), null, "L4WJYVDASV0R938NT3JPEQNU9LM32XTO", 1, "Yılmaz", new Guid("a1c62a12-a739-48b8-91fc-643e13f1d8fc"), false, null, "mehmet.yilmaz17@yilmaz.com" },
                    { new Guid("a8fc4846-781e-43ef-b497-d65a03a24c13"), 0, null, new DateTime(2043, 12, 21, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(509), 6, new Guid("491c53fb-7e75-4623-95c3-6932736a1f8c"), "8488b4a9-6b08-45b1-92f2-06448d8cf99d", new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(506), null, "taha.kayapinar@hrapp.com", false, "18881883612", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 39, DateTimeKind.Local).AddTicks(529), false, null, "Taha", "TAHA.KAYAPINAR@HRAPP.COM", "TAHA.KAYAPINAR@HRAPP.COM", "AQAAAAEAACcQAAAAEMbb2nQtkvZ6RrPJqPFJp6TkzeikOvQ+PVwDRD9Frbu7mAjbdIcR0Hq2zNwwtwkiiw==", new Guid("a8fc4846-781e-43ef-b497-d65a03a24c13"), "tahakayapinar@gmail.com", "+905811223306", false, new Guid("4cf5f02a-abfc-494a-972f-5ebc6730d6d0"), null, "14ILDLON5BKB4Z05NED0TSNL0M5ZJ9Z2", 1, "Kayapınar", new Guid("213173e9-120c-41d8-9cc2-8b4cc5d0b920"), false, null, "taha.kayapinar@hrapp.com" },
                    { new Guid("c69450ad-8255-4686-9721-6bf51633b160"), 0, null, new DateTime(2055, 11, 12, 15, 21, 7, 266, DateTimeKind.Local).AddTicks(524), 6, new Guid("87bf7d92-a661-47fa-b1ff-8300dab57f02"), "be550ad0-fe21-4574-9bf5-94f888f41755", new DateTime(2023, 6, 8, 15, 21, 7, 266, DateTimeKind.Local).AddTicks(522), null, "ali.demir38@demir.com", false, "32114127168", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 266, DateTimeKind.Local).AddTicks(533), false, null, "Ali", "ALI.DEMIR38@DEMIR.COM", "ALI.DEMIR38@DEMIR.COM", "AQAAAAEAACcQAAAAENl28/UGHAm+aX94glQHtsXmHWeKl4+X9B/S95vQTQ40i3p/dDqjVZhKd/99nAnqvg==", new Guid("c69450ad-8255-4686-9721-6bf51633b160"), "ali.demir@yahoo.com", "+905962945477", false, new Guid("f3b7b0ea-f881-4976-9e9c-cdfef6f84fbf"), null, "N2CBTKZB4HPFTJ0DKUEDILWH6J7WCDFF", 1, "Demir", new Guid("735bb164-231a-445c-9b37-320810e96cef"), false, null, "ali.demir38@demir.com" },
                    { new Guid("d3ba67ed-566e-460e-aa23-62732c375159"), 0, null, new DateTime(2075, 11, 14, 15, 21, 7, 195, DateTimeKind.Local).AddTicks(4979), 6, new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), "215ae0a3-b944-4def-b08e-3ab1094534c7", new DateTime(2023, 6, 8, 15, 21, 7, 195, DateTimeKind.Local).AddTicks(4977), null, "ali.demir26@demir.com", false, "38341200810", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 195, DateTimeKind.Local).AddTicks(4987), false, null, "Ali", "ALI.DEMIR26@DEMIR.COM", "ALI.DEMIR26@DEMIR.COM", "AQAAAAEAACcQAAAAEH6BgrNu0U51eniyQVJCc3bxfyXeQKjgI1cuGcBOAjNi6+9VuwZu7HjDVNlLWcM0HQ==", new Guid("d3ba67ed-566e-460e-aa23-62732c375159"), "ali.demir@outlook.com", "+905105671440", false, new Guid("329c0993-ef54-408e-a63a-0e13f18486ab"), null, "ZKSLCYVBWKUBIGJKTX066ZHL59P4XGQN", 1, "Demir", new Guid("11eaa0e0-ec2a-4e50-8985-927c4254e465"), false, null, "ali.demir26@demir.com" },
                    { new Guid("d6e5ecf6-9a2f-4697-a866-c2cfc329904b"), 0, null, new DateTime(2062, 5, 20, 15, 21, 7, 44, DateTimeKind.Local).AddTicks(8793), 6, new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), "e185e0a9-d947-4bd8-a890-8d3475a51f29", new DateTime(2023, 6, 8, 15, 21, 7, 44, DateTimeKind.Local).AddTicks(8792), null, "ahmet.aydin1@aydin.com", false, "34817454602", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 44, DateTimeKind.Local).AddTicks(8801), false, null, "Ahmet", "AHMET.AYDIN1@AYDIN.COM", "AHMET.AYDIN1@AYDIN.COM", "AQAAAAEAACcQAAAAEBQ/cp35O5w90Y0+QOrFXvxn6xRfXbNSIrSznskKDVmXxrxGih1O06A4AqDaBT2D1w==", new Guid("d6e5ecf6-9a2f-4697-a866-c2cfc329904b"), "ahmet.aydin@hotmail.com", "+905877896006", false, new Guid("f242bd5c-86e7-4185-a6fd-2d7249f5fc52"), null, "HNYGPM3NRZ4BPFNE369BY3ZOZRANPNRM", 1, "Aydın", new Guid("1937bb0f-1a57-4f06-aacd-56252b283b65"), false, null, "ahmet.aydin1@aydin.com" },
                    { new Guid("ff6ae3ec-1b31-45ba-ad66-d1385f3e2ef8"), 0, null, new DateTime(2060, 10, 11, 15, 21, 7, 79, DateTimeKind.Local).AddTicks(9894), 6, new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), "6078509f-4c99-44ad-a82e-37cc1a18fd7e", new DateTime(2023, 6, 8, 15, 21, 7, 79, DateTimeKind.Local).AddTicks(9889), null, "huseyin.aydin7@aydin.com", false, "55410277214", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 79, DateTimeKind.Local).AddTicks(9902), false, null, "Hüseyin", "HUSEYIN.AYDIN7@AYDIN.COM", "HUSEYIN.AYDIN7@AYDIN.COM", "AQAAAAEAACcQAAAAEHklElwSpDb5+7lIbgSiLMHUzvVf8NMqtgmKzvnYbdd2MfPrIyO7Kb632dy4QMIwFw==", new Guid("ff6ae3ec-1b31-45ba-ad66-d1385f3e2ef8"), "huseyin.aydin@yahoo.com", "+905400678017", false, new Guid("9744520c-cf0d-4528-a166-6218e9af2a03"), null, "XZ4XHIA1FJFDKM7CJ4DJPEGMJ3JAJS7C", 1, "Aydın", new Guid("28f5c387-4ec4-4fc1-a3bb-bd13c8dc35e8"), false, null, "huseyin.aydin7@aydin.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("9cb6c260-d902-496d-b625-131487ec9b6c"), new Guid("420ec281-8dfc-4a30-bbd0-ec5ce059a1d6") },
                    { new Guid("9cb6c260-d902-496d-b625-131487ec9b6c"), new Guid("6351578f-4937-4215-af1b-69d403431b03") },
                    { new Guid("d023341d-79ee-41aa-a5de-05d399e23b86"), new Guid("a8fc4846-781e-43ef-b497-d65a03a24c13") },
                    { new Guid("9cb6c260-d902-496d-b625-131487ec9b6c"), new Guid("c69450ad-8255-4686-9721-6bf51633b160") },
                    { new Guid("9cb6c260-d902-496d-b625-131487ec9b6c"), new Guid("d3ba67ed-566e-460e-aa23-62732c375159") },
                    { new Guid("9cb6c260-d902-496d-b625-131487ec9b6c"), new Guid("d6e5ecf6-9a2f-4697-a866-c2cfc329904b") },
                    { new Guid("9cb6c260-d902-496d-b625-131487ec9b6c"), new Guid("ff6ae3ec-1b31-45ba-ad66-d1385f3e2ef8") }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "BirthDate", "BloodGroup", "CompanyId", "ConcurrencyStamp", "CreateDate", "DeleteDate", "Email", "EmailConfirmed", "IdentityNumber", "ImagePath", "JobStartDate", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PatronId", "PersonalEmail", "PhoneNumber", "PhoneNumberConfirmed", "ProfessionId", "SecondName", "SecurityStamp", "Status", "Surname", "TitleId", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("04b6cea1-c5cc-419a-9095-78b5d854e29f"), 0, null, new DateTime(2058, 4, 21, 15, 21, 7, 105, DateTimeKind.Local).AddTicks(5782), 6, new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), "0e1697bc-e048-4ecd-a9ce-c2c3a4b511da", new DateTime(2023, 6, 8, 15, 21, 7, 105, DateTimeKind.Local).AddTicks(5781), null, "mustafa.yilmaz11@aydin.com", false, "76485862624", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 105, DateTimeKind.Local).AddTicks(5789), false, null, "Mustafa", "MUSTAFA.YILMAZ11@AYDIN.COM", "MUSTAFA.YILMAZ11@AYDIN.COM", null, new Guid("ff6ae3ec-1b31-45ba-ad66-d1385f3e2ef8"), "mustafa.yilmaz@google.com", "+905859882638", false, new Guid("186c06ef-fd33-40ec-a4dd-5261392a611f"), null, "J0N4ZPQPN4QTXELS4GRZZCHMSG8VICOE", 1, "Yılmaz", new Guid("28f5c387-4ec4-4fc1-a3bb-bd13c8dc35e8"), false, null, "mustafa.yilmaz11@aydin.com" },
                    { new Guid("0ba9cdac-85a4-40c2-8e36-ab49d8a59ffc"), 0, null, new DateTime(2046, 9, 22, 15, 21, 7, 99, DateTimeKind.Local).AddTicks(8790), 6, new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), "f42e3c04-c99f-4b25-9783-ac8aa7c362c7", new DateTime(2023, 6, 8, 15, 21, 7, 99, DateTimeKind.Local).AddTicks(8788), null, "ali.celik10@aydin.com", false, "34605445584", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 99, DateTimeKind.Local).AddTicks(8797), false, null, "Ali", "ALI.CELIK10@AYDIN.COM", "ALI.CELIK10@AYDIN.COM", null, new Guid("ff6ae3ec-1b31-45ba-ad66-d1385f3e2ef8"), "ali.celik@google.com", "+905958651156", false, new Guid("186c06ef-fd33-40ec-a4dd-5261392a611f"), null, "BYW7HMZ9UHNS1NXZIADWDYDZQG1EASF2", 1, "Çelik", new Guid("28f5c387-4ec4-4fc1-a3bb-bd13c8dc35e8"), false, null, "ali.celik10@aydin.com" },
                    { new Guid("1237f713-102a-4200-9a34-4f6cfd051273"), 0, null, new DateTime(2045, 2, 19, 15, 21, 7, 118, DateTimeKind.Local).AddTicks(8854), 6, new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), "4f33acf6-ab98-4fd5-ae54-39d3bcbed269", new DateTime(2023, 6, 8, 15, 21, 7, 118, DateTimeKind.Local).AddTicks(8849), null, "ismail.ozturk13@aydin.com", false, "32846127474", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 118, DateTimeKind.Local).AddTicks(8866), false, null, "İsmail", "ISMAIL.OZTURK13@AYDIN.COM", "ISMAIL.OZTURK13@AYDIN.COM", null, new Guid("ff6ae3ec-1b31-45ba-ad66-d1385f3e2ef8"), "ismail.ozturk@google.com", "+905645160734", false, new Guid("186c06ef-fd33-40ec-a4dd-5261392a611f"), null, "N1E0XKVA14KWCB3BZBY7NHU9DT5XZGLD", 1, "Öztürk", new Guid("28f5c387-4ec4-4fc1-a3bb-bd13c8dc35e8"), false, null, "ismail.ozturk13@aydin.com" },
                    { new Guid("20ce1ff7-1ab2-4670-b1de-efd5a758cf80"), 0, null, new DateTime(2064, 3, 12, 15, 21, 7, 149, DateTimeKind.Local).AddTicks(4537), 6, new Guid("41e8e9e1-0d8d-47b5-a794-449367e713f0"), "f1b2b51d-3ae8-4797-8d21-8906b53a7686", new DateTime(2023, 6, 8, 15, 21, 7, 149, DateTimeKind.Local).AddTicks(4534), null, "mehmet.yildiz18@yilmaz.com", false, "25488250846", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 149, DateTimeKind.Local).AddTicks(4545), false, null, "Mehmet", "MEHMET.YILDIZ18@YILMAZ.COM", "MEHMET.YILDIZ18@YILMAZ.COM", null, new Guid("6351578f-4937-4215-af1b-69d403431b03"), "mehmet.yildiz@yahoo.com", "+905595453813", false, new Guid("4bba6b3a-cc56-4a3d-8c19-6e9c28ffd7d7"), null, "MXBQ54A2FVD3QA866JJX5RPNZRFYMTWA", 1, "Yıldız", new Guid("a1c62a12-a739-48b8-91fc-643e13f1d8fc"), false, null, "mehmet.yildiz18@yilmaz.com" },
                    { new Guid("20cf81da-3860-4f15-bfcb-b975de90d12b"), 0, null, new DateTime(2050, 1, 7, 15, 21, 7, 94, DateTimeKind.Local).AddTicks(1305), 6, new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), "c452a919-54c9-456f-b7b1-cb6878dbe1a4", new DateTime(2023, 6, 8, 15, 21, 7, 94, DateTimeKind.Local).AddTicks(1302), null, "mehmet.celik9@aydin.com", false, "45164082684", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 94, DateTimeKind.Local).AddTicks(1314), false, null, "Mehmet", "MEHMET.CELIK9@AYDIN.COM", "MEHMET.CELIK9@AYDIN.COM", null, new Guid("ff6ae3ec-1b31-45ba-ad66-d1385f3e2ef8"), "mehmet.celik@google.com", "+905751222921", false, new Guid("9744520c-cf0d-4528-a166-6218e9af2a03"), null, "ZBBG30ZRTFXZU2UTLTZPV4A0UMK6ZGSC", 1, "Çelik", new Guid("28f5c387-4ec4-4fc1-a3bb-bd13c8dc35e8"), false, null, "mehmet.celik9@aydin.com" },
                    { new Guid("308549b7-afa7-46ef-9133-d15bbeebd18c"), 0, null, new DateTime(2048, 3, 22, 15, 21, 7, 178, DateTimeKind.Local).AddTicks(2819), 6, new Guid("41e8e9e1-0d8d-47b5-a794-449367e713f0"), "4b534986-2d7a-44ee-89a4-bf0a438ffe0d", new DateTime(2023, 6, 8, 15, 21, 7, 178, DateTimeKind.Local).AddTicks(2810), null, "hasan.ozturk23@yilmaz.com", false, "34052448640", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 178, DateTimeKind.Local).AddTicks(2829), false, null, "Hasan", "HASAN.OZTURK23@YILMAZ.COM", "HASAN.OZTURK23@YILMAZ.COM", null, new Guid("6351578f-4937-4215-af1b-69d403431b03"), "hasan.ozturk@yahoo.com", "+905763497945", false, new Guid("4bba6b3a-cc56-4a3d-8c19-6e9c28ffd7d7"), null, "KOTFKEO2V3MS5BUDREJUQQRDFKR3DKPT", 1, "Öztürk", new Guid("a1c62a12-a739-48b8-91fc-643e13f1d8fc"), false, null, "hasan.ozturk23@yilmaz.com" },
                    { new Guid("350fe8c1-ff17-4e3c-ba00-8c8e943138e6"), 0, null, new DateTime(2048, 11, 25, 15, 21, 7, 207, DateTimeKind.Local).AddTicks(1834), 6, new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), "8e55f717-7409-463c-ac42-2c8e272f6ea6", new DateTime(2023, 6, 8, 15, 21, 7, 207, DateTimeKind.Local).AddTicks(1825), null, "huseyin.yildiz28@demir.com", false, "53657440880", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 207, DateTimeKind.Local).AddTicks(1848), false, null, "Hüseyin", "HUSEYIN.YILDIZ28@DEMIR.COM", "HUSEYIN.YILDIZ28@DEMIR.COM", null, new Guid("d3ba67ed-566e-460e-aa23-62732c375159"), "huseyin.yildiz@google.com", "+905374059111", false, new Guid("329c0993-ef54-408e-a63a-0e13f18486ab"), null, "F1LILBW8M5MO9TNWLU5ZXNM8ZJPN2GH6", 1, "Yıldız", new Guid("11eaa0e0-ec2a-4e50-8985-927c4254e465"), false, null, "huseyin.yildiz28@demir.com" },
                    { new Guid("3b881add-90b3-4bd5-9574-a734bdbd994d"), 0, null, new DateTime(2054, 11, 23, 15, 21, 7, 112, DateTimeKind.Local).AddTicks(1388), 6, new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), "758ac9eb-4d88-4431-9b4b-a71bee367af0", new DateTime(2023, 6, 8, 15, 21, 7, 112, DateTimeKind.Local).AddTicks(1376), null, "huseyin.kaya12@aydin.com", false, "24163116194", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 112, DateTimeKind.Local).AddTicks(1406), false, null, "Hüseyin", "HUSEYIN.KAYA12@AYDIN.COM", "HUSEYIN.KAYA12@AYDIN.COM", null, new Guid("ff6ae3ec-1b31-45ba-ad66-d1385f3e2ef8"), "huseyin.kaya@yahoo.com", "+905304727636", false, new Guid("9744520c-cf0d-4528-a166-6218e9af2a03"), null, "6X08N1R3PWNEQ40ZIOAMQ0EL9RT690TW", 1, "Kaya", new Guid("28f5c387-4ec4-4fc1-a3bb-bd13c8dc35e8"), false, null, "huseyin.kaya12@aydin.com" },
                    { new Guid("3f32a1b6-232c-497c-ac83-f03f34ccd0c1"), 0, null, new DateTime(2072, 3, 4, 15, 21, 7, 124, DateTimeKind.Local).AddTicks(8178), 6, new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), "27dc38cf-b65d-435d-ab0f-d37977604540", new DateTime(2023, 6, 8, 15, 21, 7, 124, DateTimeKind.Local).AddTicks(8177), null, "mehmet.sahin14@aydin.com", false, "33320666526", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 124, DateTimeKind.Local).AddTicks(8185), false, null, "Mehmet", "MEHMET.SAHIN14@AYDIN.COM", "MEHMET.SAHIN14@AYDIN.COM", null, new Guid("ff6ae3ec-1b31-45ba-ad66-d1385f3e2ef8"), "mehmet.sahin@google.com", "+905949958771", false, new Guid("186c06ef-fd33-40ec-a4dd-5261392a611f"), null, "M9N9RNNDXIUQBJCF419DQX5HPJJ4QP7L", 1, "Şahin", new Guid("28f5c387-4ec4-4fc1-a3bb-bd13c8dc35e8"), false, null, "mehmet.sahin14@aydin.com" },
                    { new Guid("4496c9f3-c1bf-4c71-9386-189c24aa9b8a"), 0, null, new DateTime(2070, 5, 15, 15, 21, 7, 183, DateTimeKind.Local).AddTicks(9850), 6, new Guid("41e8e9e1-0d8d-47b5-a794-449367e713f0"), "52931d4d-eaad-4bc5-8fbe-496f3e64b8c9", new DateTime(2023, 6, 8, 15, 21, 7, 183, DateTimeKind.Local).AddTicks(9842), null, "ibrahim.yildirim24@yilmaz.com", false, "58871134300", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 183, DateTimeKind.Local).AddTicks(9869), false, null, "İbrahim", "IBRAHIM.YILDIRIM24@YILMAZ.COM", "IBRAHIM.YILDIRIM24@YILMAZ.COM", null, new Guid("6351578f-4937-4215-af1b-69d403431b03"), "ibrahim.yildirim@google.com", "+905846909098", false, new Guid("4bba6b3a-cc56-4a3d-8c19-6e9c28ffd7d7"), null, "CHHMSW21GTSTOTFHRY8K4W38TBLO42KT", 1, "Yıldırım", new Guid("a1c62a12-a739-48b8-91fc-643e13f1d8fc"), false, null, "ibrahim.yildirim24@yilmaz.com" },
                    { new Guid("48f2f70f-db65-4f49-a430-d9ea05076480"), 0, null, new DateTime(2063, 7, 23, 15, 21, 7, 155, DateTimeKind.Local).AddTicks(2703), 6, new Guid("41e8e9e1-0d8d-47b5-a794-449367e713f0"), "0df4f05c-6c39-4689-9921-0176fb521b6c", new DateTime(2023, 6, 8, 15, 21, 7, 155, DateTimeKind.Local).AddTicks(2699), null, "ibrahim.kaya19@yilmaz.com", false, "84113474866", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 155, DateTimeKind.Local).AddTicks(2713), false, null, "İbrahim", "IBRAHIM.KAYA19@YILMAZ.COM", "IBRAHIM.KAYA19@YILMAZ.COM", null, new Guid("6351578f-4937-4215-af1b-69d403431b03"), "ibrahim.kaya@hotmail.com", "+905850886906", false, new Guid("4bba6b3a-cc56-4a3d-8c19-6e9c28ffd7d7"), null, "UPHVBXRYOVM3ME5LFDO10OA0G7URKFGB", 1, "Kaya", new Guid("a1c62a12-a739-48b8-91fc-643e13f1d8fc"), false, null, "ibrahim.kaya19@yilmaz.com" },
                    { new Guid("4b9f1e80-0452-408c-92a1-25cf09804f90"), 0, null, new DateTime(2075, 6, 24, 15, 21, 7, 242, DateTimeKind.Local).AddTicks(4157), 6, new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), "6635423c-b72b-4700-8c0a-bba019967472", new DateTime(2023, 6, 8, 15, 21, 7, 242, DateTimeKind.Local).AddTicks(4153), null, "ahmet.demir34@demir.com", false, "18787711538", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 242, DateTimeKind.Local).AddTicks(4166), false, null, "Ahmet", "AHMET.DEMIR34@DEMIR.COM", "AHMET.DEMIR34@DEMIR.COM", null, new Guid("d3ba67ed-566e-460e-aa23-62732c375159"), "ahmet.demir@google.com", "+905378759190", false, new Guid("329c0993-ef54-408e-a63a-0e13f18486ab"), null, "CTYIVY0FF209V16KORB2KRTIDXDQWD9M", 1, "Demir", new Guid("11eaa0e0-ec2a-4e50-8985-927c4254e465"), false, null, "ahmet.demir34@demir.com" },
                    { new Guid("4ba14c70-7b2a-4d64-8502-aa00f2b2250c"), 0, null, new DateTime(2061, 5, 4, 15, 21, 7, 236, DateTimeKind.Local).AddTicks(6058), 6, new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), "70d300fd-6be0-4aab-ae49-2147faeaef71", new DateTime(2023, 6, 8, 15, 21, 7, 236, DateTimeKind.Local).AddTicks(6057), null, "ahmet.yildiz33@demir.com", false, "40258088870", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 236, DateTimeKind.Local).AddTicks(6066), false, null, "Ahmet", "AHMET.YILDIZ33@DEMIR.COM", "AHMET.YILDIZ33@DEMIR.COM", null, new Guid("d3ba67ed-566e-460e-aa23-62732c375159"), "ahmet.yildiz@yandex.com", "+905930814530", false, new Guid("329c0993-ef54-408e-a63a-0e13f18486ab"), null, "S7EQXTDVR6MOI8RAY2SJQ3FPUW3995L6", 1, "Yıldız", new Guid("11eaa0e0-ec2a-4e50-8985-927c4254e465"), false, null, "ahmet.yildiz33@demir.com" },
                    { new Guid("4c245d7e-1d56-4ccd-90b5-aad9d60af99f"), 0, null, new DateTime(2044, 4, 8, 15, 21, 7, 260, DateTimeKind.Local).AddTicks(1119), 6, new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), "ba0999a8-1fb2-4d65-aebe-63c32581565e", new DateTime(2023, 6, 8, 15, 21, 7, 260, DateTimeKind.Local).AddTicks(1107), null, "hasan.celik37@demir.com", false, "41088054370", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 260, DateTimeKind.Local).AddTicks(1133), false, null, "Hasan", "HASAN.CELIK37@DEMIR.COM", "HASAN.CELIK37@DEMIR.COM", null, new Guid("d3ba67ed-566e-460e-aa23-62732c375159"), "hasan.celik@yahoo.com", "+905434628574", false, new Guid("329c0993-ef54-408e-a63a-0e13f18486ab"), null, "J50Z4E51YR6JNWKUGUL5VF5LY4ZVQAH6", 1, "Çelik", new Guid("11eaa0e0-ec2a-4e50-8985-927c4254e465"), false, null, "hasan.celik37@demir.com" },
                    { new Guid("5ae3bfa6-f5a4-4203-860a-be529885d4b2"), 0, null, new DateTime(2073, 8, 13, 15, 21, 7, 248, DateTimeKind.Local).AddTicks(5042), 6, new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), "31badd8f-f37e-42d8-b23d-be889559fe30", new DateTime(2023, 6, 8, 15, 21, 7, 248, DateTimeKind.Local).AddTicks(5031), null, "mustafa.yilmaz35@demir.com", false, "20483784174", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 248, DateTimeKind.Local).AddTicks(5099), false, null, "Mustafa", "MUSTAFA.YILMAZ35@DEMIR.COM", "MUSTAFA.YILMAZ35@DEMIR.COM", null, new Guid("d3ba67ed-566e-460e-aa23-62732c375159"), "mustafa.yilmaz@yahoo.com", "+905665143598", false, new Guid("329c0993-ef54-408e-a63a-0e13f18486ab"), null, "THA95PEK7A428EHS5O4TAITSDZWZ79XJ", 1, "Yılmaz", new Guid("11eaa0e0-ec2a-4e50-8985-927c4254e465"), false, null, "mustafa.yilmaz35@demir.com" },
                    { new Guid("6328ac0c-1719-4bb2-8fac-ead6c2d03d15"), 0, null, new DateTime(2057, 4, 9, 15, 21, 7, 166, DateTimeKind.Local).AddTicks(7127), 6, new Guid("41e8e9e1-0d8d-47b5-a794-449367e713f0"), "86e6dd06-c30f-44c2-b670-b47998ec94db", new DateTime(2023, 6, 8, 15, 21, 7, 166, DateTimeKind.Local).AddTicks(7126), null, "yusuf.celik21@yilmaz.com", false, "58577080550", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 166, DateTimeKind.Local).AddTicks(7132), false, null, "Yusuf", "YUSUF.CELIK21@YILMAZ.COM", "YUSUF.CELIK21@YILMAZ.COM", null, new Guid("6351578f-4937-4215-af1b-69d403431b03"), "yusuf.celik@yandex.com", "+905721617641", false, new Guid("4bba6b3a-cc56-4a3d-8c19-6e9c28ffd7d7"), null, "D49DGUE53U5MZERRR7O8D8Q5B4U99V6N", 1, "Çelik", new Guid("a1c62a12-a739-48b8-91fc-643e13f1d8fc"), false, null, "yusuf.celik21@yilmaz.com" },
                    { new Guid("688194a6-c658-468c-acb8-07c5a3e72835"), 0, null, new DateTime(2069, 3, 17, 15, 21, 7, 136, DateTimeKind.Local).AddTicks(3676), 6, new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), "853643c5-1912-4dc4-bb00-2c9c7608c0c7", new DateTime(2023, 6, 8, 15, 21, 7, 136, DateTimeKind.Local).AddTicks(3671), null, "hasan.sahin16@aydin.com", false, "58683627038", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 136, DateTimeKind.Local).AddTicks(3688), false, null, "Hasan", "HASAN.SAHIN16@AYDIN.COM", "HASAN.SAHIN16@AYDIN.COM", null, new Guid("ff6ae3ec-1b31-45ba-ad66-d1385f3e2ef8"), "hasan.sahin@yandex.com", "+905644716477", false, new Guid("186c06ef-fd33-40ec-a4dd-5261392a611f"), null, "1Q1LS9YKNUVSQ7XOA2DKA9V5YEC1LCKP", 1, "Şahin", new Guid("28f5c387-4ec4-4fc1-a3bb-bd13c8dc35e8"), false, null, "hasan.sahin16@aydin.com" },
                    { new Guid("6f606b4a-2d61-46a4-af52-882ea9036e93"), 0, null, new DateTime(2054, 4, 28, 15, 21, 7, 68, DateTimeKind.Local).AddTicks(2555), 6, new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), "0c58f276-b58e-4839-a230-8c7942c241b1", new DateTime(2023, 6, 8, 15, 21, 7, 68, DateTimeKind.Local).AddTicks(2552), null, "ahmet.yildiz5@aydin.com", false, "16063505758", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 68, DateTimeKind.Local).AddTicks(2563), false, null, "Ahmet", "AHMET.YILDIZ5@AYDIN.COM", "AHMET.YILDIZ5@AYDIN.COM", null, new Guid("d6e5ecf6-9a2f-4697-a866-c2cfc329904b"), "ahmet.yildiz@yahoo.com", "+905675259580", false, new Guid("b7bff893-ff74-41cc-ad42-866243bc36d6"), null, "OXC3OX4KQR4F06DDRFUMKAMWOH3MV91D", 1, "Yıldız", new Guid("6bd790b7-38bb-4543-8eba-07ae5571ff8b"), false, null, "ahmet.yildiz5@aydin.com" },
                    { new Guid("77582294-970b-4a47-adc2-16b4d6adc582"), 0, null, new DateTime(2054, 3, 20, 15, 21, 7, 219, DateTimeKind.Local).AddTicks(701), 6, new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), "07dfcf48-202f-492c-b93f-046de2873211", new DateTime(2023, 6, 8, 15, 21, 7, 219, DateTimeKind.Local).AddTicks(700), null, "hasan.yilmaz30@demir.com", false, "30447103552", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 219, DateTimeKind.Local).AddTicks(710), false, null, "Hasan", "HASAN.YILMAZ30@DEMIR.COM", "HASAN.YILMAZ30@DEMIR.COM", null, new Guid("d3ba67ed-566e-460e-aa23-62732c375159"), "hasan.yilmaz@outlook.com", "+905661068402", false, new Guid("329c0993-ef54-408e-a63a-0e13f18486ab"), null, "TENQCOMM3ZK0R2QLZSD428WFN1VMA30W", 1, "Yılmaz", new Guid("11eaa0e0-ec2a-4e50-8985-927c4254e465"), false, null, "hasan.yilmaz30@demir.com" },
                    { new Guid("827d6181-263d-4cc8-880d-87d283977af0"), 0, null, new DateTime(2069, 12, 15, 15, 21, 7, 87, DateTimeKind.Local).AddTicks(9436), 6, new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), "c74c0e15-fb92-4661-ab96-4c3b9257e2d3", new DateTime(2023, 6, 8, 15, 21, 7, 87, DateTimeKind.Local).AddTicks(9420), null, "ahmet.kaya8@aydin.com", false, "71585788188", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 87, DateTimeKind.Local).AddTicks(9458), false, null, "Ahmet", "AHMET.KAYA8@AYDIN.COM", "AHMET.KAYA8@AYDIN.COM", null, new Guid("ff6ae3ec-1b31-45ba-ad66-d1385f3e2ef8"), "ahmet.kaya@yandex.com", "+905474679824", false, new Guid("9744520c-cf0d-4528-a166-6218e9af2a03"), null, "9ZQPO3W9S8U9O39AUJ63LOW04FDDVIX1", 1, "Kaya", new Guid("28f5c387-4ec4-4fc1-a3bb-bd13c8dc35e8"), false, null, "ahmet.kaya8@aydin.com" },
                    { new Guid("91c8e121-5fb2-4d99-bd46-47b2e00db209"), 0, null, new DateTime(2050, 2, 21, 15, 21, 7, 161, DateTimeKind.Local).AddTicks(127), 6, new Guid("41e8e9e1-0d8d-47b5-a794-449367e713f0"), "a242210a-976f-46bd-9fc7-4f023dbe5be1", new DateTime(2023, 6, 8, 15, 21, 7, 161, DateTimeKind.Local).AddTicks(125), null, "huseyin.yildirim20@yilmaz.com", false, "33082008442", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 161, DateTimeKind.Local).AddTicks(133), false, null, "Hüseyin", "HUSEYIN.YILDIRIM20@YILMAZ.COM", "HUSEYIN.YILDIRIM20@YILMAZ.COM", null, new Guid("6351578f-4937-4215-af1b-69d403431b03"), "huseyin.yildirim@google.com", "+905496652188", false, new Guid("4bba6b3a-cc56-4a3d-8c19-6e9c28ffd7d7"), null, "CCYRPR5QR9SKRFUXJL9NBWY4TPMSSNLD", 1, "Yıldırım", new Guid("a1c62a12-a739-48b8-91fc-643e13f1d8fc"), false, null, "huseyin.yildirim20@yilmaz.com" },
                    { new Guid("9af8dd3d-37a1-48a1-a68e-8722c1002fe0"), 0, null, new DateTime(2071, 2, 8, 15, 21, 7, 56, DateTimeKind.Local).AddTicks(3324), 6, new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), "1d94a203-f09f-4fbd-8cf5-7dc68b2532d9", new DateTime(2023, 6, 8, 15, 21, 7, 56, DateTimeKind.Local).AddTicks(3324), null, "ismail.celik3@aydin.com", false, "57885420010", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 56, DateTimeKind.Local).AddTicks(3330), false, null, "İsmail", "ISMAIL.CELIK3@AYDIN.COM", "ISMAIL.CELIK3@AYDIN.COM", null, new Guid("d6e5ecf6-9a2f-4697-a866-c2cfc329904b"), "ismail.celik@hotmail.com", "+905661060608", false, new Guid("f242bd5c-86e7-4185-a6fd-2d7249f5fc52"), null, "GVPZU11GII7ZZY51PZ3KUIVFQ5FSO7DR", 1, "Çelik", new Guid("6bd790b7-38bb-4543-8eba-07ae5571ff8b"), false, null, "ismail.celik3@aydin.com" },
                    { new Guid("9b190eed-6be5-47cb-a718-6d66da6cc5dc"), 0, null, new DateTime(2066, 3, 12, 15, 21, 7, 74, DateTimeKind.Local).AddTicks(915), 6, new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), "3864848e-7a0c-4346-9e5d-d15d87dc364a", new DateTime(2023, 6, 8, 15, 21, 7, 74, DateTimeKind.Local).AddTicks(914), null, "hasan.yilmaz6@aydin.com", false, "75604011240", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 74, DateTimeKind.Local).AddTicks(920), false, null, "Hasan", "HASAN.YILMAZ6@AYDIN.COM", "HASAN.YILMAZ6@AYDIN.COM", null, new Guid("d6e5ecf6-9a2f-4697-a866-c2cfc329904b"), "hasan.yilmaz@google.com", "+905882131158", false, new Guid("b7bff893-ff74-41cc-ad42-866243bc36d6"), null, "8CDV2GSERQ3UJR7461I72W417IMDN7E0", 1, "Yılmaz", new Guid("6bd790b7-38bb-4543-8eba-07ae5571ff8b"), false, null, "hasan.yilmaz6@aydin.com" },
                    { new Guid("ae40629f-41ee-4322-bc0d-9d8a01b8b219"), 0, null, new DateTime(2056, 3, 12, 15, 21, 7, 224, DateTimeKind.Local).AddTicks(7826), 6, new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), "fb265b34-960d-41d9-80d3-4e7a387ed790", new DateTime(2023, 6, 8, 15, 21, 7, 224, DateTimeKind.Local).AddTicks(7825), null, "yusuf.yildirim31@demir.com", false, "30820314292", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 224, DateTimeKind.Local).AddTicks(7834), false, null, "Yusuf", "YUSUF.YILDIRIM31@DEMIR.COM", "YUSUF.YILDIRIM31@DEMIR.COM", null, new Guid("d3ba67ed-566e-460e-aa23-62732c375159"), "yusuf.yildirim@yahoo.com", "+905694056912", false, new Guid("329c0993-ef54-408e-a63a-0e13f18486ab"), null, "HGOGMNKMRUATSSRH5UEPJDH30FZ06SRC", 1, "Yıldırım", new Guid("11eaa0e0-ec2a-4e50-8985-927c4254e465"), false, null, "yusuf.yildirim31@demir.com" },
                    { new Guid("b14817d6-dc46-4a60-a097-fafa8d200a0d"), 0, null, new DateTime(2062, 6, 18, 15, 21, 7, 62, DateTimeKind.Local).AddTicks(3564), 6, new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), "780e48e9-5b90-48ec-9729-0dee4c9d4e78", new DateTime(2023, 6, 8, 15, 21, 7, 62, DateTimeKind.Local).AddTicks(3557), null, "yusuf.sahin4@aydin.com", false, "52808238758", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 62, DateTimeKind.Local).AddTicks(3581), false, null, "Yusuf", "YUSUF.SAHIN4@AYDIN.COM", "YUSUF.SAHIN4@AYDIN.COM", null, new Guid("d6e5ecf6-9a2f-4697-a866-c2cfc329904b"), "yusuf.sahin@yandex.com", "+905177256686", false, new Guid("f242bd5c-86e7-4185-a6fd-2d7249f5fc52"), null, "GH84PS3F98DDD7PXNYKCQ81DRRO3NBLE", 1, "Şahin", new Guid("6bd790b7-38bb-4543-8eba-07ae5571ff8b"), false, null, "yusuf.sahin4@aydin.com" },
                    { new Guid("b559ecf2-5837-44e4-b6d3-21f74dd403bd"), 0, null, new DateTime(2041, 11, 15, 15, 21, 7, 230, DateTimeKind.Local).AddTicks(8231), 6, new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), "29b4c575-397a-45cd-a297-cd93700b4947", new DateTime(2023, 6, 8, 15, 21, 7, 230, DateTimeKind.Local).AddTicks(8225), null, "huseyin.aydin32@demir.com", false, "28150834764", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 230, DateTimeKind.Local).AddTicks(8246), false, null, "Hüseyin", "HUSEYIN.AYDIN32@DEMIR.COM", "HUSEYIN.AYDIN32@DEMIR.COM", null, new Guid("d3ba67ed-566e-460e-aa23-62732c375159"), "huseyin.aydin@google.com", "+905105098824", false, new Guid("329c0993-ef54-408e-a63a-0e13f18486ab"), null, "479AAQ3M1AGNCC7RNW67G3CTR5ZW12KV", 1, "Aydın", new Guid("11eaa0e0-ec2a-4e50-8985-927c4254e465"), false, null, "huseyin.aydin32@demir.com" },
                    { new Guid("c584c492-d257-446d-ba52-c5aa1387f08e"), 0, null, new DateTime(2059, 3, 2, 15, 21, 7, 130, DateTimeKind.Local).AddTicks(5134), 6, new Guid("d6e792f2-c992-468a-a4fa-c07f552b9971"), "9e9b822e-bbfd-4a9d-89f9-916fd723aa57", new DateTime(2023, 6, 8, 15, 21, 7, 130, DateTimeKind.Local).AddTicks(5133), null, "ali.yildiz15@aydin.com", false, "75077471176", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 130, DateTimeKind.Local).AddTicks(5139), false, null, "Ali", "ALI.YILDIZ15@AYDIN.COM", "ALI.YILDIZ15@AYDIN.COM", null, new Guid("ff6ae3ec-1b31-45ba-ad66-d1385f3e2ef8"), "ali.yildiz@yahoo.com", "+905309619544", false, new Guid("186c06ef-fd33-40ec-a4dd-5261392a611f"), null, "H58UBE35T1AVD9KDVERID55OCE9EULRS", 1, "Yıldız", new Guid("28f5c387-4ec4-4fc1-a3bb-bd13c8dc35e8"), false, null, "ali.yildiz15@aydin.com" },
                    { new Guid("c677549f-0915-4ba0-bd68-4c12a2351952"), 0, null, new DateTime(2046, 7, 6, 15, 21, 7, 254, DateTimeKind.Local).AddTicks(2178), 6, new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), "316fbde1-a16b-450d-88bc-9bc30e8a7e11", new DateTime(2023, 6, 8, 15, 21, 7, 254, DateTimeKind.Local).AddTicks(2177), null, "ali.demir36@demir.com", false, "52256070276", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 254, DateTimeKind.Local).AddTicks(2186), false, null, "Ali", "ALI.DEMIR36@DEMIR.COM", "ALI.DEMIR36@DEMIR.COM", null, new Guid("d3ba67ed-566e-460e-aa23-62732c375159"), "ali.demir@google.com", "+905338622744", false, new Guid("329c0993-ef54-408e-a63a-0e13f18486ab"), null, "X41FM9WQ7TC8A78J3FZJV0W62B76BZXK", 1, "Demir", new Guid("11eaa0e0-ec2a-4e50-8985-927c4254e465"), false, null, "ali.demir36@demir.com" },
                    { new Guid("d62d6f0f-4c47-4d2f-a8c3-20418ec9d7af"), 0, null, new DateTime(2044, 4, 11, 15, 21, 7, 213, DateTimeKind.Local).AddTicks(3661), 6, new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), "25a5de41-66ad-4580-a7ee-98e1eccbfd99", new DateTime(2023, 6, 8, 15, 21, 7, 213, DateTimeKind.Local).AddTicks(3650), null, "mehmet.celik29@demir.com", false, "43247268062", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 213, DateTimeKind.Local).AddTicks(3676), false, null, "Mehmet", "MEHMET.CELIK29@DEMIR.COM", "MEHMET.CELIK29@DEMIR.COM", null, new Guid("d3ba67ed-566e-460e-aa23-62732c375159"), "mehmet.celik@yandex.com", "+905377834424", false, new Guid("329c0993-ef54-408e-a63a-0e13f18486ab"), null, "9UMTWNCS7WGA7KTNNLP3XZZI1W7QB8MX", 1, "Çelik", new Guid("11eaa0e0-ec2a-4e50-8985-927c4254e465"), false, null, "mehmet.celik29@demir.com" },
                    { new Guid("dfcb59d4-08b9-4fcf-a17d-87424ad7540b"), 0, null, new DateTime(2045, 9, 22, 15, 21, 7, 189, DateTimeKind.Local).AddTicks(7729), 6, new Guid("41e8e9e1-0d8d-47b5-a794-449367e713f0"), "fa270d48-a028-4b01-9701-feea44ced2ee", new DateTime(2023, 6, 8, 15, 21, 7, 189, DateTimeKind.Local).AddTicks(7725), null, "ahmet.kaya25@yilmaz.com", false, "52333723200", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 189, DateTimeKind.Local).AddTicks(7738), false, null, "Ahmet", "AHMET.KAYA25@YILMAZ.COM", "AHMET.KAYA25@YILMAZ.COM", null, new Guid("6351578f-4937-4215-af1b-69d403431b03"), "ahmet.kaya@yandex.com", "+905457813274", false, new Guid("4bba6b3a-cc56-4a3d-8c19-6e9c28ffd7d7"), null, "D4EG3LHF24OYX1NYSL5YU2AKUX0AINAA", 1, "Kaya", new Guid("a1c62a12-a739-48b8-91fc-643e13f1d8fc"), false, null, "ahmet.kaya25@yilmaz.com" },
                    { new Guid("e5216ea2-f6ec-4952-8386-e06bc3f8d21d"), 0, null, new DateTime(2055, 9, 23, 15, 21, 7, 271, DateTimeKind.Local).AddTicks(7803), 6, new Guid("87bf7d92-a661-47fa-b1ff-8300dab57f02"), "790f7ef2-f891-44ee-be41-4a8cc664b98a", new DateTime(2023, 6, 8, 15, 21, 7, 271, DateTimeKind.Local).AddTicks(7801), null, "ismail.aydin39@demir.com", false, "44573162710", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 271, DateTimeKind.Local).AddTicks(7809), false, null, "İsmail", "ISMAIL.AYDIN39@DEMIR.COM", "ISMAIL.AYDIN39@DEMIR.COM", null, new Guid("c69450ad-8255-4686-9721-6bf51633b160"), "ismail.aydin@hotmail.com", "+905721079614", false, new Guid("6174100b-cad5-43ef-859c-94ab0d350b9c"), null, "FOA566F3YKZ2B5I9JNYNQ33W6NRMDU0E", 1, "Aydın", new Guid("735bb164-231a-445c-9b37-320810e96cef"), false, null, "ismail.aydin39@demir.com" },
                    { new Guid("ecb88725-60fc-46f9-946e-23847081e03a"), 0, null, new DateTime(2070, 9, 19, 15, 21, 7, 50, DateTimeKind.Local).AddTicks(5818), 6, new Guid("d4fa4d98-dfe1-46ec-b6ea-2c9306b851c4"), "3ac03d36-22fd-43d5-b168-57ef83ffca09", new DateTime(2023, 6, 8, 15, 21, 7, 50, DateTimeKind.Local).AddTicks(5816), null, "ali.yildiz2@aydin.com", false, "38502753520", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 50, DateTimeKind.Local).AddTicks(5825), false, null, "Ali", "ALI.YILDIZ2@AYDIN.COM", "ALI.YILDIZ2@AYDIN.COM", null, new Guid("d6e5ecf6-9a2f-4697-a866-c2cfc329904b"), "ali.yildiz@yandex.com", "+905686812815", false, new Guid("f242bd5c-86e7-4185-a6fd-2d7249f5fc52"), null, "UEDC2UFG0O5GWX9P93V3BGU9OWFNTM6F", 1, "Yıldız", new Guid("6bd790b7-38bb-4543-8eba-07ae5571ff8b"), false, null, "ali.yildiz2@aydin.com" },
                    { new Guid("ef773520-5624-4bac-829a-e3923ab9e8bb"), 0, null, new DateTime(2072, 2, 13, 15, 21, 7, 172, DateTimeKind.Local).AddTicks(5642), 6, new Guid("41e8e9e1-0d8d-47b5-a794-449367e713f0"), "d93e7b60-907e-48f5-93c4-3608b1cc2a7e", new DateTime(2023, 6, 8, 15, 21, 7, 172, DateTimeKind.Local).AddTicks(5635), null, "ismail.aydin22@yilmaz.com", false, "72627013862", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 172, DateTimeKind.Local).AddTicks(5652), false, null, "İsmail", "ISMAIL.AYDIN22@YILMAZ.COM", "ISMAIL.AYDIN22@YILMAZ.COM", null, new Guid("6351578f-4937-4215-af1b-69d403431b03"), "ismail.aydin@yahoo.com", "+905694199260", false, new Guid("4bba6b3a-cc56-4a3d-8c19-6e9c28ffd7d7"), null, "MFBAPTNAW58NA87MAZV7K4RWUYKUK384", 1, "Aydın", new Guid("a1c62a12-a739-48b8-91fc-643e13f1d8fc"), false, null, "ismail.aydin22@yilmaz.com" },
                    { new Guid("f0f15d5b-4f6f-497d-880a-3cf5d5b0d0a9"), 0, null, new DateTime(2070, 3, 10, 15, 21, 7, 201, DateTimeKind.Local).AddTicks(1977), 6, new Guid("1f8f49cc-62f2-4d3a-8d08-502b7a6b6e90"), "b593975e-eb7d-4ba8-a3f8-2dd2e81fdc19", new DateTime(2023, 6, 8, 15, 21, 7, 201, DateTimeKind.Local).AddTicks(1975), null, "ibrahim.sahin27@demir.com", false, "77655468288", "/images/UserPhotos/defaultuser.jpg", new DateTime(2023, 6, 8, 15, 21, 7, 201, DateTimeKind.Local).AddTicks(1984), false, null, "İbrahim", "IBRAHIM.SAHIN27@DEMIR.COM", "IBRAHIM.SAHIN27@DEMIR.COM", null, new Guid("d3ba67ed-566e-460e-aa23-62732c375159"), "ibrahim.sahin@yandex.com", "+905631090202", false, new Guid("329c0993-ef54-408e-a63a-0e13f18486ab"), null, "39DP72HUV12FFGEJES0S6T6JZFSWEGSS", 1, "Şahin", new Guid("11eaa0e0-ec2a-4e50-8985-927c4254e465"), false, null, "ibrahim.sahin27@demir.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("04b6cea1-c5cc-419a-9095-78b5d854e29f") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("0ba9cdac-85a4-40c2-8e36-ab49d8a59ffc") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("1237f713-102a-4200-9a34-4f6cfd051273") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("20ce1ff7-1ab2-4670-b1de-efd5a758cf80") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("20cf81da-3860-4f15-bfcb-b975de90d12b") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("308549b7-afa7-46ef-9133-d15bbeebd18c") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("350fe8c1-ff17-4e3c-ba00-8c8e943138e6") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("3b881add-90b3-4bd5-9574-a734bdbd994d") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("3f32a1b6-232c-497c-ac83-f03f34ccd0c1") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("4496c9f3-c1bf-4c71-9386-189c24aa9b8a") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("48f2f70f-db65-4f49-a430-d9ea05076480") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("4b9f1e80-0452-408c-92a1-25cf09804f90") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("4ba14c70-7b2a-4d64-8502-aa00f2b2250c") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("4c245d7e-1d56-4ccd-90b5-aad9d60af99f") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("5ae3bfa6-f5a4-4203-860a-be529885d4b2") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("6328ac0c-1719-4bb2-8fac-ead6c2d03d15") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("688194a6-c658-468c-acb8-07c5a3e72835") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("6f606b4a-2d61-46a4-af52-882ea9036e93") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("77582294-970b-4a47-adc2-16b4d6adc582") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("827d6181-263d-4cc8-880d-87d283977af0") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("91c8e121-5fb2-4d99-bd46-47b2e00db209") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("9af8dd3d-37a1-48a1-a68e-8722c1002fe0") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("9b190eed-6be5-47cb-a718-6d66da6cc5dc") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("ae40629f-41ee-4322-bc0d-9d8a01b8b219") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("b14817d6-dc46-4a60-a097-fafa8d200a0d") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("b559ecf2-5837-44e4-b6d3-21f74dd403bd") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("c584c492-d257-446d-ba52-c5aa1387f08e") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("c677549f-0915-4ba0-bd68-4c12a2351952") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("d62d6f0f-4c47-4d2f-a8c3-20418ec9d7af") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("dfcb59d4-08b9-4fcf-a17d-87424ad7540b") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("e5216ea2-f6ec-4952-8386-e06bc3f8d21d") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("ecb88725-60fc-46f9-946e-23847081e03a") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("ef773520-5624-4bac-829a-e3923ab9e8bb") },
                    { new Guid("407c9a61-6fb8-467d-8d61-7868947d1c16"), new Guid("f0f15d5b-4f6f-497d-880a-3cf5d5b0d0a9") }
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
                name: "IX_Reports_CreatorId",
                table: "Reports",
                column: "CreatorId");

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
                name: "Reports");

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
