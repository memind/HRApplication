using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKApplication.Persistance.Migrations
{
    public partial class advanceSync : Migration
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
                    FinalDateRequest = table.Column<DateTime>(type: "date", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AdvanceToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashAdvances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashAdvances_AspNetUsers_AdvanceToId",
                        column: x => x.AdvanceToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), "1C8731K85PV6PERQDX1FS80LMCN4BAQF", "Company Administrator", "COMPANY ADMINISTRATOR" },
                    { new Guid("ca212714-3182-45bd-ae6e-98920a78a6b7"), "U5QJ0HO2IF3VAE1M1W07LOOUZQ9UEEM7", "Site Administrator", "SITE ADMINISTRATOR" },
                    { new Guid("ec6467e8-9188-48b7-a103-1dbd9a7592cc"), "KFVRBD77FFNSZ1RPEKE6EU4SJXPDVS8A", "Personal", "PERSONAL" }
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("016b3554-058c-42ab-abdc-f1cc2170ca5b"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9212), null, "Maden", 1, null },
                    { new Guid("0328465e-f461-4c4c-8857-283d548e61aa"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9218), null, "Sağlık ve Sosyal Hizmetler", 1, null },
                    { new Guid("081c32df-6971-475c-aa03-4b0a580a590b"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9223), null, "Ticaret (Satış ve Pazarlama)", 1, null },
                    { new Guid("10b59d3b-ed99-48fb-a9cc-be3a4ccbd2b6"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9215), null, "Medya, İletişim ve Yayıncılık", 1, null },
                    { new Guid("2f7dd057-f6b2-41de-9751-f17af5a97890"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9227), null, "Turizm, Konaklama, Yiyecek-İçecek Hizmetleri", 1, null },
                    { new Guid("30447641-e436-4a04-8dc1-c025a1043819"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9222), null, "Tekstil, Hazır Giyim, Deri", 1, null },
                    { new Guid("45396d21-a3ed-456b-abc8-f272cb1b3d21"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9221), null, "Tarım, Avcılık ve Balıkçılık", 1, null },
                    { new Guid("4c60741c-a0bf-4e9b-b920-4c735e09008f"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9226), null, "Toplumsal ve Kişisel Hizmetler", 1, null },
                    { new Guid("79a5bd0b-ff1d-4e67-8b69-8c00ffb79eed"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9188), null, "Cam, Çimento ve ToprakÇevre", 1, null },
                    { new Guid("80ef694a-a15f-4bee-82a1-af06d20544a0"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9190), null, "Elektrik ve Elektronik", 1, null },
                    { new Guid("84741a65-6803-4a9f-afef-2620e8d0739b"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9220), null, "Spor ve Rekreasyon", 1, null },
                    { new Guid("a14aeeed-cbc5-42ae-ab87-395a396adbc8"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9205), null, "İnşaat", 1, null },
                    { new Guid("a6d9efe9-9359-402c-be1f-3c16d1e4b06f"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9186), null, "Bilişim", 1, null },
                    { new Guid("ae8c34af-6033-4a5a-8a34-65d401027387"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9216), null, "Metal", 1, null },
                    { new Guid("b5c16064-d333-4e7c-8c29-eb350ec50c90"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9202), null, "Enerji", 1, null },
                    { new Guid("b916dcdb-03fa-4921-8414-c9e9917b05c3"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9210), null, "Kimya, Petrol, Lastik ve PLastik", 1, null },
                    { new Guid("b996f13b-2f88-4403-a2b9-321645ae2f6a"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9204), null, "Gıda", 1, null },
                    { new Guid("cc46ea27-66fe-4c29-8b61-4af6c2ac11c4"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9217), null, "Otomotiv", 1, null },
                    { new Guid("d13b573e-d00c-4be2-be6a-39c533f3b922"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9203), null, "Finans", 1, null },
                    { new Guid("d15183a3-d619-4df6-ac25-cf5502d91ba5"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9169), null, "Ahşap Teknolojisi", 1, null },
                    { new Guid("d473c4ce-2b57-48d0-b4c1-a4e62fbe2790"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9208), null, "İş ve Yönetimi", 1, null },
                    { new Guid("d7315504-1c02-449c-bae1-32512b28fd7b"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9189), null, "Eğitim", 1, null },
                    { new Guid("e2b0c0c9-d16c-4073-810b-5d460058d5e5"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9228), null, "Ulaştırma, Lojistik ve Haberleşme", 1, null },
                    { new Guid("f99d7f68-e9b6-4005-ac20-2ec1a2dd6231"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9211), null, "Kültür, Sanat ve Tasarım", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "NumberOfEmployees", "PhoneNumber", "SectorId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("10d4756c-632f-444f-b3d1-cb76360bb181"), new DateTime(2023, 5, 18, 18, 33, 11, 650, DateTimeKind.Local).AddTicks(160), null, "info@demirlimitedsirketi.com", "Demir Limited Şirketi", 35, "+905658424132", new Guid("0328465e-f461-4c4c-8857-283d548e61aa"), 3, null },
                    { new Guid("176e8a94-9006-41f0-8134-898438aa012a"), new DateTime(2023, 5, 18, 18, 33, 11, 637, DateTimeKind.Local).AddTicks(6490), null, "info@yildizanonimsirketi.com", "Yıldız Anonim Şirketi", 74, "+905356615208", new Guid("b5c16064-d333-4e7c-8c29-eb350ec50c90"), 3, null },
                    { new Guid("1ac947c4-c3d1-4cd5-9394-2b4cb6812f5b"), new DateTime(2023, 5, 18, 18, 33, 11, 648, DateTimeKind.Local).AddTicks(6526), null, "info@celikkollektifsirketi.com", "Çelik Kollektif Şirketi", 32, "+905354704631", new Guid("d13b573e-d00c-4be2-be6a-39c533f3b922"), 3, null },
                    { new Guid("302f8fea-9db4-416a-8a47-31c4a21bc34f"), new DateTime(2023, 5, 18, 18, 33, 11, 645, DateTimeKind.Local).AddTicks(9170), null, "info@yildizkomanditsirketi.com", "Yıldız Komandit Şirketi", 87, "+905816525037", new Guid("a6d9efe9-9359-402c-be1f-3c16d1e4b06f"), 3, null },
                    { new Guid("64aee2e4-bc6e-4f37-b314-0eac30a77aa8"), new DateTime(2023, 5, 18, 18, 33, 11, 641, DateTimeKind.Local).AddTicks(8025), null, "info@celikkooperatifsirketi.com", "Çelik Kooperatif Şirketi", 64, "+905496908782", new Guid("4c60741c-a0bf-4e9b-b920-4c735e09008f"), 3, null },
                    { new Guid("729811ae-3eb3-44e9-a016-ba70480db52b"), new DateTime(2023, 5, 18, 18, 33, 11, 654, DateTimeKind.Local).AddTicks(1514), null, "info@yildirimkollektifsirketi.com", "Yıldırım Kollektif Şirketi", 53, "+905578329313", new Guid("d7315504-1c02-449c-bae1-32512b28fd7b"), 3, null },
                    { new Guid("868440d4-ab7f-4155-8a1d-698fadda0644"), new DateTime(2023, 5, 18, 18, 33, 11, 634, DateTimeKind.Local).AddTicks(9003), null, "info@yildirimkooperatifsirketi.com", "Yıldırım Kooperatif Şirketi", 63, "+905703084298", new Guid("a14aeeed-cbc5-42ae-ab87-395a396adbc8"), 3, null },
                    { new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9328), null, "ikapp@ikapp.com", "IKApp A.Ş.", 5, "+905308599091", new Guid("a6d9efe9-9359-402c-be1f-3c16d1e4b06f"), 1, null },
                    { new Guid("99fb68cb-3b77-41b8-9f75-94ce45d51e6b"), new DateTime(2023, 5, 18, 18, 33, 11, 639, DateTimeKind.Local).AddTicks(607), null, "info@kayakollektifsirketi.com", "Kaya Kollektif Şirketi", 34, "+905809005198", new Guid("30447641-e436-4a04-8dc1-c025a1043819"), 3, null },
                    { new Guid("a67e8bae-1b8f-489a-83d1-5617377613b8"), new DateTime(2023, 5, 18, 18, 33, 11, 643, DateTimeKind.Local).AddTicks(1823), null, "info@yilmazkomanditsirketi.com", "Yılmaz Komandit Şirketi", 61, "+905797845817", new Guid("ae8c34af-6033-4a5a-8a34-65d401027387"), 3, null },
                    { new Guid("ae628b87-907f-4daa-a2c5-06e884ac606f"), new DateTime(2023, 5, 18, 18, 33, 11, 636, DateTimeKind.Local).AddTicks(2780), null, "info@yilmazkollektifsirketi.com", "Yılmaz Kollektif Şirketi", 84, "+905374623765", new Guid("30447641-e436-4a04-8dc1-c025a1043819"), 3, null },
                    { new Guid("aefb6d3e-b970-4bac-9659-170a63b731a7"), new DateTime(2023, 5, 18, 18, 33, 11, 640, DateTimeKind.Local).AddTicks(4586), null, "info@ozdemirlimitedsirketi.com", "Özdemir Limited Şirketi", 56, "+905267356770", new Guid("cc46ea27-66fe-4c29-8b61-4af6c2ac11c4"), 3, null },
                    { new Guid("d28f5f9a-7010-444c-ad0f-2d4dd98a288c"), new DateTime(2023, 5, 18, 18, 33, 11, 644, DateTimeKind.Local).AddTicks(5578), null, "info@aydinkooperatifsirketi.com", "Aydın Kooperatif Şirketi", 74, "+905546498281", new Guid("0328465e-f461-4c4c-8857-283d548e61aa"), 3, null },
                    { new Guid("e2de0c2f-cddf-4c21-b392-7e190f16dab1"), new DateTime(2023, 5, 18, 18, 33, 11, 647, DateTimeKind.Local).AddTicks(2877), null, "info@yildizkomanditsirketi.com", "Yıldız Komandit Şirketi", 66, "+905451725137", new Guid("cc46ea27-66fe-4c29-8b61-4af6c2ac11c4"), 3, null },
                    { new Guid("e75594dd-6197-4608-8add-3ae264c52a79"), new DateTime(2023, 5, 18, 18, 33, 11, 651, DateTimeKind.Local).AddTicks(3660), null, "info@ozdemirkomanditsirketi.com", "Özdemir Komandit Şirketi", 5, "+905522477156", new Guid("f99d7f68-e9b6-4005-ac20-2ec1a2dd6231"), 3, null },
                    { new Guid("f8cb8e4a-2788-4ef1-9f8b-d70e5f4b5a87"), new DateTime(2023, 5, 18, 18, 33, 11, 652, DateTimeKind.Local).AddTicks(7492), null, "info@celikanonimsirketi.com", "Çelik Anonim Şirketi", 86, "+905966025138", new Guid("a14aeeed-cbc5-42ae-ab87-395a396adbc8"), 3, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("0248b36d-775e-4ff9-89b5-8c12f62a85e4"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9448), null, "Guest Services Supervisor", 1, null },
                    { new Guid("03bcffe3-26b0-42f7-a0cf-bf41cb55164e"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9450), null, "Front Desk Associate", 1, null },
                    { new Guid("047a5115-2b1c-4123-95df-dbdd59c55a43"), new Guid("868440d4-ab7f-4155-8a1d-698fadda0644"), new DateTime(2023, 5, 18, 18, 33, 11, 634, DateTimeKind.Local).AddTicks(9011), null, "Credit Analyst", 1, null },
                    { new Guid("05caa53e-3f29-4ce2-afba-424236382fda"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9421), null, "Systems Administrator", 1, null },
                    { new Guid("07f737cf-25a5-482f-a5e4-de097ac9b970"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9453), null, "Server/Host/Hostess", 1, null },
                    { new Guid("0c077de1-afa9-47ee-bd8e-529ef5d1fdb2"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9456), null, "Construction Foreman", 1, null },
                    { new Guid("0d6275ff-4fa1-4a06-a647-888f70819ed5"), new Guid("99fb68cb-3b77-41b8-9f75-94ce45d51e6b"), new DateTime(2023, 5, 18, 18, 33, 11, 639, DateTimeKind.Local).AddTicks(621), null, "Systems Administrator", 1, null },
                    { new Guid("1a63add8-5008-4429-8baa-7ed856d78d15"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9445), null, "Teaching Assistant", 1, null },
                    { new Guid("1ad93ebd-5116-4847-aeb6-5fdb5987f5fb"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9366), null, "Investment Analyst", 1, null },
                    { new Guid("1af05b79-c2b9-48f3-806d-5ead23223620"), new Guid("aefb6d3e-b970-4bac-9659-170a63b731a7"), new DateTime(2023, 5, 18, 18, 33, 11, 640, DateTimeKind.Local).AddTicks(4591), null, "Server/Host/Hostess", 1, null },
                    { new Guid("1c6d4af5-9657-4512-b569-225b09e8c19e"), new Guid("e2de0c2f-cddf-4c21-b392-7e190f16dab1"), new DateTime(2023, 5, 18, 18, 33, 11, 647, DateTimeKind.Local).AddTicks(2881), null, "Project Appraisal Engineer", 1, null },
                    { new Guid("28022b0d-786e-4a95-8028-a920c96b3201"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9444), null, "Teacher", 1, null },
                    { new Guid("2b45a8bd-cd45-4cab-aafd-807f7f3ab127"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9410), null, "Operations Supervisor", 1, null },
                    { new Guid("2ca303b8-e86c-443f-a0a4-7bdaf7348ee1"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9446), null, "General Manager", 1, null },
                    { new Guid("2d3f02a5-7b97-479c-8b95-a3e72209b821"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9427), null, "Registered Nurse", 1, null },
                    { new Guid("313f267f-98a4-4f46-8785-b5e69e705943"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9420), null, "Full Stack Developer", 1, null },
                    { new Guid("31a81584-1c91-4118-91de-b8fb6082dd20"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9346), null, "VP of Sales", 1, null },
                    { new Guid("323a2f9c-f8c8-46dc-9013-159d2fa3a759"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9460), null, "Contract Administrator", 1, null },
                    { new Guid("32cd6a5b-78ae-49a7-903b-ad41e02c5629"), new Guid("10d4756c-632f-444f-b3d1-cb76360bb181"), new DateTime(2023, 5, 18, 18, 33, 11, 650, DateTimeKind.Local).AddTicks(166), null, "National Sales Director", 1, null },
                    { new Guid("3373e07d-339a-4af9-8070-0379e4d6fea3"), new Guid("e75594dd-6197-4608-8add-3ae264c52a79"), new DateTime(2023, 5, 18, 18, 33, 11, 651, DateTimeKind.Local).AddTicks(3664), null, "Guest Services Supervisor", 1, null },
                    { new Guid("3cbf169a-4637-4901-af9a-964e55aa7842"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9424), null, "Other Industries:", 1, null },
                    { new Guid("3d69d750-871b-450d-880e-159d3a1d8f82"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9436), null, "Administrator", 1, null },
                    { new Guid("3ee2ff34-aaeb-4c4b-8323-8c9ae397fd20"), new Guid("302f8fea-9db4-416a-8a47-31c4a21bc34f"), new DateTime(2023, 5, 18, 18, 33, 11, 645, DateTimeKind.Local).AddTicks(9174), null, "Registrar", 1, null },
                    { new Guid("4148b0f2-35d8-4552-8714-ffc92dc694ae"), new Guid("f8cb8e4a-2788-4ef1-9f8b-d70e5f4b5a87"), new DateTime(2023, 5, 18, 18, 33, 11, 652, DateTimeKind.Local).AddTicks(7495), null, "Principal", 1, null },
                    { new Guid("41fcd104-d295-4222-b329-d289d9cb449c"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9374), null, "Account Manager", 1, null },
                    { new Guid("43d89918-fd0b-4950-8cb3-76a0a7511be6"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9458), null, "Project Manager", 1, null },
                    { new Guid("469c81da-ac27-4b57-b4f8-b6bddb000924"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9422), null, "Data Analyst", 1, null },
                    { new Guid("49ef8f54-41f7-456b-861f-039c80587488"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9362), null, "Marketing Coordinator", 1, null },
                    { new Guid("4a36243e-313e-47f8-9834-86ad1bfdf011"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9377), null, "Customer Service Representative", 1, null },
                    { new Guid("4ae06e7b-1126-45f4-8988-3059d3b1bc75"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9378), null, "Support Specialist", 1, null },
                    { new Guid("4c7cca1e-67ff-45b0-abba-6c1a3a8cedd6"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9431), null, "Physical Therapist", 1, null },
                    { new Guid("52cb8774-1c8e-45b1-bd5a-1b85fc8aefbd"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9381), null, "Director of Business Operations", 1, null },
                    { new Guid("537196b6-5ccf-481c-953f-8e40e1b0653b"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9432), null, "Nursing Assistant", 1, null },
                    { new Guid("54f37ed2-9900-4f24-9600-5c40239d3f41"), new Guid("64aee2e4-bc6e-4f37-b314-0eac30a77aa8"), new DateTime(2023, 5, 18, 18, 33, 11, 641, DateTimeKind.Local).AddTicks(8028), null, "Regional Sales Manager", 1, null },
                    { new Guid("55eb126d-d1ed-49ef-ac34-9df80cf8c202"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9365), null, "Procurement Director", 1, null },
                    { new Guid("61158f2e-a458-4c3b-abc0-0d4255fa9627"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9417), null, "Director of Information Security", 1, null },
                    { new Guid("675a1f92-6fef-4b45-a62b-4b9bfe6fabaf"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9352), null, "Sales Associate", 1, null },
                    { new Guid("68f6dd66-7832-453d-895b-2be59bf85322"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9380), null, "COO (Chief Operating Officer)", 1, null },
                    { new Guid("6a998d48-e3a1-4bc7-b900-a0c668be7c0d"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9369), null, "Risk Analyst", 1, null },
                    { new Guid("78d1004a-e3e0-42ed-9c0c-4849b8ad2ed0"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9457), null, "Safety Director", 1, null },
                    { new Guid("7abf2cd6-c520-4041-94b1-a2e963a93352"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9350), null, "Regional Sales Manager", 1, null },
                    { new Guid("7d4271ab-ec91-4d3a-acfc-9085397d49d5"), new Guid("d28f5f9a-7010-444c-ad0f-2d4dd98a288c"), new DateTime(2023, 5, 18, 18, 33, 11, 644, DateTimeKind.Local).AddTicks(5588), null, "Account Manager", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("7fe70f6d-69a8-414e-90d9-1163f73de5df"), new Guid("176e8a94-9006-41f0-8134-898438aa012a"), new DateTime(2023, 5, 18, 18, 33, 11, 637, DateTimeKind.Local).AddTicks(6496), null, "Marketing Coordinator", 1, null },
                    { new Guid("83fbdb17-0e74-4432-9e6b-39fe6dde5392"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9455), null, "Hotel Receptionist", 1, null },
                    { new Guid("8790ad3f-fcc4-41cc-8a20-3d679848f137"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9355), null, "Marketing Director", 1, null },
                    { new Guid("8b8ed7af-5e84-4919-98bf-2b7f63947819"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9354), null, "CMO (Chief Marketing Officer)", 1, null },
                    { new Guid("9b6703d1-eb49-46fe-b40d-3ba6a9a94b9a"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9372), null, "VP of Client Services", 1, null },
                    { new Guid("a257a8d8-d5a6-48f8-864c-3990957f6729"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9419), null, "Software Engineer I, II, III", 1, null },
                    { new Guid("a63ca779-c327-4e4f-872c-a7f00459e774"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9435), null, "Occupational Therapy Aide", 1, null },
                    { new Guid("a95e9560-93e8-418a-8b7e-01b12d277499"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9433), null, "Clinical Laboratory Technician", 1, null },
                    { new Guid("ac78e419-04b5-44d1-bc3c-c6db8db6361d"), new Guid("1ac947c4-c3d1-4cd5-9394-2b4cb6812f5b"), new DateTime(2023, 5, 18, 18, 33, 11, 648, DateTimeKind.Local).AddTicks(6531), null, "Marketing Analyst", 1, null },
                    { new Guid("b3049a0f-a97d-4baa-933b-78ed3c3ec495"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9449), null, "Concierge", 1, null },
                    { new Guid("b891bfa3-dbaf-48b6-8708-0fd697d77bff"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9461), null, "Project Appraisal Engineer", 1, null },
                    { new Guid("b95961b5-3075-4ccb-91e5-1592f392ca24"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9437), null, "Principal", 1, null },
                    { new Guid("bbebd5b3-86de-4829-8ee2-a79b02095a17"), new Guid("a67e8bae-1b8f-489a-83d1-5617377613b8"), new DateTime(2023, 5, 18, 18, 33, 11, 643, DateTimeKind.Local).AddTicks(1827), null, "Customer Success Manager", 1, null },
                    { new Guid("c7e81b0f-ec79-4c86-a67d-41f9eda86f5e"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9441), null, "Registrar", 1, null },
                    { new Guid("cda99746-ccc3-4a0c-8fee-5399e1d321b8"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9359), null, "Sr. Marketing Manager", 1, null },
                    { new Guid("d0da5f8b-2b20-4e25-a56d-d57371f6ec10"), new Guid("729811ae-3eb3-44e9-a016-ba70480db52b"), new DateTime(2023, 5, 18, 18, 33, 11, 654, DateTimeKind.Local).AddTicks(1519), null, "Pharmacy Technician", 1, null },
                    { new Guid("d4035060-daf9-47cf-8e7f-fb6778f2c024"), new Guid("ae628b87-907f-4daa-a2c5-06e884ac606f"), new DateTime(2023, 5, 18, 18, 33, 11, 636, DateTimeKind.Local).AddTicks(2785), null, "Data Analyst", 1, null },
                    { new Guid("d83a560f-19d3-4d55-899e-0fea955dcbe1"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9442), null, "School Counselor", 1, null },
                    { new Guid("de9b35c6-4990-4a3d-854e-817f4d876728"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9414), null, "Sr. Manager of HR", 1, null },
                    { new Guid("df2955ba-4e4a-407b-a6fe-73ad411b9127"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9351), null, "Sales Representative", 1, null },
                    { new Guid("e2e36bc7-2457-4f85-80bf-02176e8c3f8f"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9416), null, "HR Analyst", 1, null },
                    { new Guid("e8f84c01-af00-487c-a3c5-c5f6a5685a35"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9429), null, "Pharmacy Technician", 1, null },
                    { new Guid("ebe22364-925a-41c7-bb29-1c7308b17de0"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9463), null, "Inspector", 1, null },
                    { new Guid("f0fbaf29-cb40-445d-aad1-529cba19d1e2"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9368), null, "Credit Analyst", 1, null },
                    { new Guid("f252690f-5704-45d4-997a-69b0ee250dff"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9360), null, "Marketing Analyst", 1, null },
                    { new Guid("f774a8f0-0999-401f-b1d2-89790dcae4d2"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9364), null, "VP of Finance", 1, null },
                    { new Guid("f86073f8-2f83-494e-982f-4b7fcc2a64f0"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9376), null, "Customer Success Manager", 1, null },
                    { new Guid("fc040a7d-50a8-4e5f-8a4d-afa33992c3db"), new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9348), null, "National Sales Director", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "BirthDate", "BloodGroup", "CompanyId", "ConcurrencyStamp", "CreateDate", "DeleteDate", "Email", "EmailConfirmed", "IdentityNumber", "ImagePath", "JobStartDate", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profession", "SecondName", "SecurityStamp", "Status", "Surname", "TitleId", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("05f638e8-e75d-464a-bdeb-33a4e0ceabe9"), 0, null, new DateTime(2070, 4, 27, 18, 33, 11, 651, DateTimeKind.Local).AddTicks(3671), null, new Guid("e75594dd-6197-4608-8add-3ae264c52a79"), "8298031e-cb6e-43d5-8cdf-557e83a6ae56", new DateTime(2023, 5, 18, 18, 33, 11, 651, DateTimeKind.Local).AddTicks(3670), null, "ismail.ozdemir@hotmail.com", false, "11213035512", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.OZDEMIR@HOTMAIL.COM", "ISMAIL.OZDEMIR@HOTMAIL.COM", "AQAAAAEAACcQAAAAEF8XzoqMP0nNHQEHqbu9cOsmB8xSj7nJbuu+wed7IN4OzWF/8mlHQRsFjHrI4wmr0Q==", "+905351796122", false, "Judge", null, "UN2IVSMLIY9KK12JGND6PME32ILKXS30", 3, "Özdemir", new Guid("3373e07d-339a-4af9-8070-0379e4d6fea3"), false, null, "ismail.ozdemir@hotmail.com" },
                    { new Guid("0ff0eba9-3c7b-41ca-b770-9ca74a7a7c41"), 0, null, new DateTime(2069, 4, 20, 18, 33, 11, 648, DateTimeKind.Local).AddTicks(6538), null, new Guid("1ac947c4-c3d1-4cd5-9394-2b4cb6812f5b"), "fec94ef8-df4c-4eeb-845b-74d68ba4837b", new DateTime(2023, 5, 18, 18, 33, 11, 648, DateTimeKind.Local).AddTicks(6537), null, "ibrahim.celik@microsoft.com", false, "78412362160", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İbrahim", "IBRAHIM.CELIK@MICROSOFT.COM", "IBRAHIM.CELIK@MICROSOFT.COM", "AQAAAAEAACcQAAAAEFwZ93WOhzueO65Aphye+7pzR0Boogj+L9J3NIUVkWXFHYVlK/cvJBig87g2RdcjGw==", "+905281466673", false, "Lawyer", null, "RHB5E56ZV349LPN25Y4RKBNZXWKVUV5B", 3, "Çelik", new Guid("ac78e419-04b5-44d1-bc3c-c6db8db6361d"), false, null, "ibrahim.celik@microsoft.com" },
                    { new Guid("10083ba2-0926-4297-926a-9196ed002c30"), 0, null, new DateTime(2076, 4, 11, 18, 33, 11, 640, DateTimeKind.Local).AddTicks(4601), null, new Guid("aefb6d3e-b970-4bac-9659-170a63b731a7"), "3185e374-0664-4105-a12b-39fe81e31f15", new DateTime(2023, 5, 18, 18, 33, 11, 640, DateTimeKind.Local).AddTicks(4600), null, "mehmet.ozdemir@outlook.com", false, "25037337820", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mehmet", "MEHMET.OZDEMIR@OUTLOOK.COM", "MEHMET.OZDEMIR@OUTLOOK.COM", "AQAAAAEAACcQAAAAELIOWr2SckuEfipAMkGViCsPyJejonYacvZx1De6JlarBSSwVQOh4DglmY3wtTBABQ==", "+905494482847", false, "Waiter/Waitress", null, "PVCOW6NNGANFRMF36GOZ28X8C7O5FXWB", 3, "Özdemir", new Guid("1af05b79-c2b9-48f3-806d-5ead23223620"), false, null, "mehmet.ozdemir@outlook.com" },
                    { new Guid("3e783ca2-5b37-420e-971a-69cfb1d8d749"), 0, null, new DateTime(2048, 5, 2, 18, 33, 11, 636, DateTimeKind.Local).AddTicks(2792), null, new Guid("ae628b87-907f-4daa-a2c5-06e884ac606f"), "0f70137c-148b-4a80-8fd6-1930317b5f08", new DateTime(2023, 5, 18, 18, 33, 11, 636, DateTimeKind.Local).AddTicks(2791), null, "osman.yilmaz@hotmail.com", false, "72728578758", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "OSMAN.YILMAZ@HOTMAIL.COM", "OSMAN.YILMAZ@HOTMAIL.COM", "AQAAAAEAACcQAAAAELWESCoGNvWufLkflVd3qIUNFKL0XYbJ/zWdIjBdBNOVIyiNgykkzzhPgkjWyWpp5A==", "+905716810708", false, "Tailor", null, "2FEU3E8EDSBZSCMESG787VYBFRXLD1UE", 3, "Yılmaz", new Guid("d4035060-daf9-47cf-8e7f-fb6778f2c024"), false, null, "osman.yilmaz@hotmail.com" },
                    { new Guid("49e4161c-eda9-4543-8937-5902f78da11a"), 0, null, new DateTime(2047, 9, 7, 18, 33, 11, 644, DateTimeKind.Local).AddTicks(5594), null, new Guid("d28f5f9a-7010-444c-ad0f-2d4dd98a288c"), "af140036-af4e-47d0-85bc-dd55f9b72ab2", new DateTime(2023, 5, 18, 18, 33, 11, 644, DateTimeKind.Local).AddTicks(5593), null, "huseyin.aydin@hotmail.com", false, "67654807430", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "HUSEYIN.AYDIN@HOTMAIL.COM", "HUSEYIN.AYDIN@HOTMAIL.COM", "AQAAAAEAACcQAAAAEF1PtMYsV0I8boHm53QKE1iqNw2suNKFemZww9dCbsJAeZ0xTj1ydTPyHY7tVNBsKw==", "+905224638182", false, "Farmer", null, "8G1ERCIXPWYQZ40ULKO430UQ9D4V8BQL", 3, "Aydın", new Guid("7d4271ab-ec91-4d3a-acfc-9085397d49d5"), false, null, "huseyin.aydin@hotmail.com" },
                    { new Guid("50f9458f-1931-4f0e-9fdb-f6c04e4946d4"), 0, null, new DateTime(2066, 4, 23, 18, 33, 11, 637, DateTimeKind.Local).AddTicks(6501), null, new Guid("176e8a94-9006-41f0-8134-898438aa012a"), "f053bff7-eb34-4a3f-9c76-06e93d12d0f0", new DateTime(2023, 5, 18, 18, 33, 11, 637, DateTimeKind.Local).AddTicks(6500), null, "yusuf.yildiz@microsoft.com", false, "63007420080", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Yusuf", "YUSUF.YILDIZ@MICROSOFT.COM", "YUSUF.YILDIZ@MICROSOFT.COM", "AQAAAAEAACcQAAAAEOffcAofiuAdd1PVK4suT+f3BECwBlp40y3dMmyeWL9dEsAU67dyklPcDs+4wy4pOg==", "+905896045247", false, "Butcher", null, "1C1ZD392KRXV25O1TM3HF1OEKB232M9F", 3, "Yıldız", new Guid("7fe70f6d-69a8-414e-90d9-1163f73de5df"), false, null, "yusuf.yildiz@microsoft.com" },
                    { new Guid("5415c26e-556e-4392-9ac9-dafac9d49329"), 0, null, new DateTime(2047, 9, 12, 18, 33, 11, 641, DateTimeKind.Local).AddTicks(8034), null, new Guid("64aee2e4-bc6e-4f37-b314-0eac30a77aa8"), "8714f7e0-801a-4c8c-92d7-7f9bca1327cc", new DateTime(2023, 5, 18, 18, 33, 11, 641, DateTimeKind.Local).AddTicks(8032), null, "huseyin.celik@google.com", false, "31813215756", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "HUSEYIN.CELIK@GOOGLE.COM", "HUSEYIN.CELIK@GOOGLE.COM", "AQAAAAEAACcQAAAAEOGrSHmU/lOn4oqo8X4Np8ludiAH9X1GwsNjYt7UFGm60iRx+eWiWNGXevWh7VdnfQ==", "+905607547861", false, "Secretary", null, "S2WPUJAHUM3JTQY82B8NYMJ4BE592PUZ", 3, "Çelik", new Guid("54f37ed2-9900-4f24-9600-5c40239d3f41"), false, null, "huseyin.celik@google.com" },
                    { new Guid("54fa39d6-b8d2-4881-bc31-7218c1697b49"), 0, null, new DateTime(2073, 10, 8, 18, 33, 11, 654, DateTimeKind.Local).AddTicks(1524), null, new Guid("729811ae-3eb3-44e9-a016-ba70480db52b"), "0ccef7c2-8a53-43dc-b4c5-880076280a9c", new DateTime(2023, 5, 18, 18, 33, 11, 654, DateTimeKind.Local).AddTicks(1523), null, "ismail.yildirim@outlook.com", false, "22382823640", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.YILDIRIM@OUTLOOK.COM", "ISMAIL.YILDIRIM@OUTLOOK.COM", "AQAAAAEAACcQAAAAEOR6XR9qbVE+cTIA+y4T4JP9VIFptk0COp9Hzsl3Bb3ufT55C5STl/fyyEY1PcQT4g==", "+905584163216", false, "Traffic warden", null, "2AQO8GWM5HDQ4F5HAAB3NZKMH86Y5W7P", 3, "Yıldırım", new Guid("d0da5f8b-2b20-4e25-a56d-d57371f6ec10"), false, null, "ismail.yildirim@outlook.com" },
                    { new Guid("57f0cc7b-4a3d-4d7d-bcbb-0c46ac56bd18"), 0, null, new DateTime(2061, 9, 19, 18, 33, 11, 643, DateTimeKind.Local).AddTicks(1834), null, new Guid("a67e8bae-1b8f-489a-83d1-5617377613b8"), "98bb8195-06f3-4163-af85-1d99b1f421d4", new DateTime(2023, 5, 18, 18, 33, 11, 643, DateTimeKind.Local).AddTicks(1832), null, "mustafa.yilmaz@yahoo.com", false, "50382646532", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mustafa", "MUSTAFA.YILMAZ@YAHOO.COM", "MUSTAFA.YILMAZ@YAHOO.COM", "AQAAAAEAACcQAAAAEOQhNRNDbEtqzP7Gbww3LQqQqc8KwsCZsN9vlSV7EljjXbj3M1+qI+tvLcjCc0v9cA==", "+905944637240", false, "Mechanic", null, "SJEPYD77IPFMLD5PRN0H7X4087AMCSQZ", 3, "Yılmaz", new Guid("bbebd5b3-86de-4829-8ee2-a79b02095a17"), false, null, "mustafa.yilmaz@yahoo.com" },
                    { new Guid("679e9d0b-d12b-472a-b52c-0df33a5ced10"), 0, null, new DateTime(2053, 10, 12, 18, 33, 11, 633, DateTimeKind.Local).AddTicks(5220), null, new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), "256e5e1d-94a2-47c0-84d9-0400b96f40ba", new DateTime(2023, 5, 18, 18, 33, 11, 633, DateTimeKind.Local).AddTicks(5218), null, "test5@test.com", false, "41334103234", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "TEST5@TEST.COM", "TEST5@TEST.COM", "AQAAAAEAACcQAAAAEN8i0RxEtXUzqT5Pm970ePgYHcDbHNB8ZsnkI1UCcE3izH0fac6zMCAQPH98KLmHEA==", "+905198165989", false, "Florist", null, "FVOLS5YE7IM50GBVWEVBNJHVENVFMZG8", 1, "Demir", new Guid("df2955ba-4e4a-407b-a6fe-73ad411b9127"), false, null, "test5@test.com" },
                    { new Guid("681478c6-242c-4dcc-85ec-2ea7baf47a85"), 0, null, new DateTime(2041, 7, 22, 18, 33, 11, 629, DateTimeKind.Local).AddTicks(3874), null, new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), "96d71591-8893-4081-8d8a-048411013ab7", new DateTime(2023, 5, 18, 18, 33, 11, 629, DateTimeKind.Local).AddTicks(3866), null, "test2@test.com", false, "85080847448", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Mustafa", "TEST2@TEST.COM", "TEST2@TEST.COM", "AQAAAAEAACcQAAAAEJZVTSBEPrkD9bYP12pU8B9wkKVcEi+bNADf83uW1vhe3RNldFXx1XKgZpJJpOwQbA==", "+905377075247", false, "Pharmacist", null, "BH7Z9KFQAYIWZVBV0BAIX0KPHCTIIZDZ", 1, "Şahin", new Guid("05caa53e-3f29-4ce2-afba-424236382fda"), false, null, "test2@test.com" },
                    { new Guid("6c655f87-fe0d-43b5-887a-6c88f78eadc4"), 0, null, new DateTime(2067, 1, 17, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9484), null, new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), "fb47fae7-0613-4e72-8922-1a111a9d3780", new DateTime(2023, 5, 18, 18, 33, 11, 627, DateTimeKind.Local).AddTicks(9479), null, "test1@test.com", false, "63248761478", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hasan", "TEST1@TEST.COM", "TEST1@TEST.COM", "AQAAAAEAACcQAAAAEItVHV5fHw9X3DbQVtQHCRgTO7NZwc1Mot58WEeO3eulSEBqv1m4574r4sCzO+N+Fg==", "+905430457249", false, "Waiter/Waitress", null, "GY2IECDM6N4FYLSBBGLD5IP06FL28JUB", 1, "Yılmaz", new Guid("f774a8f0-0999-401f-b1d2-89790dcae4d2"), false, null, "test1@test.com" },
                    { new Guid("72f5f61f-5063-4363-b951-94f5b82ddcf7"), 0, null, new DateTime(2063, 12, 24, 18, 33, 11, 650, DateTimeKind.Local).AddTicks(171), null, new Guid("10d4756c-632f-444f-b3d1-cb76360bb181"), "65b8df58-5255-4095-bcf7-4295055f881a", new DateTime(2023, 5, 18, 18, 33, 11, 650, DateTimeKind.Local).AddTicks(170), null, "huseyin.demir@outlook.com", false, "76260757718", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hüseyin", "HUSEYIN.DEMIR@OUTLOOK.COM", "HUSEYIN.DEMIR@OUTLOOK.COM", "AQAAAAEAACcQAAAAEAeApJ6i0lft13hcDDgsPC/Ed0dG4irm5qM95yFlwDSD3qgbO7NsmtwMSRfh7q8HHg==", "+905903783758", false, "Teacher", null, "FMNTVJK9XSLVK9JNEX18OLG29JMGE8HM", 3, "Demir", new Guid("32cd6a5b-78ae-49a7-903b-ad41e02c5629"), false, null, "huseyin.demir@outlook.com" },
                    { new Guid("7efe0ef2-774d-48d1-90fb-c3ae16877087"), 0, null, new DateTime(2065, 10, 30, 18, 33, 11, 645, DateTimeKind.Local).AddTicks(9229), null, new Guid("302f8fea-9db4-416a-8a47-31c4a21bc34f"), "13bfcd23-4d68-491f-9ee9-0e684f5f90b2", new DateTime(2023, 5, 18, 18, 33, 11, 645, DateTimeKind.Local).AddTicks(9228), null, "hasan.yildiz@google.com", false, "72877348264", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Hasan", "HASAN.YILDIZ@GOOGLE.COM", "HASAN.YILDIZ@GOOGLE.COM", "AQAAAAEAACcQAAAAEJT6IlwuGw8mtAIEXb+g0Jf5Em28wpFC2mm0tPyjpM7c1KWourV8lRX6HWBl4pfilw==", "+905431855951", false, "Scientist", null, "9INRPBGGQKIRSANFVQPSMSPP4W0S96C8", 3, "Yıldız", new Guid("3ee2ff34-aaeb-4c4b-8323-8c9ae397fd20"), false, null, "hasan.yildiz@google.com" },
                    { new Guid("96b83eb2-b6db-4834-b126-7472424254dd"), 0, null, new DateTime(2056, 10, 4, 18, 33, 11, 647, DateTimeKind.Local).AddTicks(2889), null, new Guid("e2de0c2f-cddf-4c21-b392-7e190f16dab1"), "57947c19-955b-4c0c-98fe-e722cd10edec", new DateTime(2023, 5, 18, 18, 33, 11, 647, DateTimeKind.Local).AddTicks(2888), null, "ismail.yildiz@outlook.com", false, "51002034500", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "İsmail", "ISMAIL.YILDIZ@OUTLOOK.COM", "ISMAIL.YILDIZ@OUTLOOK.COM", "AQAAAAEAACcQAAAAENNIpREZhoEyun13+y0cgzoD9OMJxui0jrMJxsLID31EhYxxfJn2D6cSbYxsln6VUQ==", "+905755463426", false, "Busdriver", null, "FKSREAUU7XNC5SHFQH2UJSCFIO15S59G", 3, "Yıldız", new Guid("1c6d4af5-9657-4512-b569-225b09e8c19e"), false, null, "ismail.yildiz@outlook.com" },
                    { new Guid("b11aad72-9709-4e51-a7fe-cec4ba278173"), 0, null, new DateTime(2042, 9, 14, 18, 33, 11, 639, DateTimeKind.Local).AddTicks(627), null, new Guid("99fb68cb-3b77-41b8-9f75-94ce45d51e6b"), "934eae9b-6c6b-4a70-8816-20701df1dd70", new DateTime(2023, 5, 18, 18, 33, 11, 639, DateTimeKind.Local).AddTicks(625), null, "osman.kaya@yandex.com", false, "10423453574", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "OSMAN.KAYA@YANDEX.COM", "OSMAN.KAYA@YANDEX.COM", "AQAAAAEAACcQAAAAEKH2xRCep2CREs2yUB0RyikfgUzPS8P+iNO3P2ugLsgkEv1aJfUugoEt/krDX7MfVA==", "+905601392036", false, "Waiter/Waitress", null, "N1K2A7C5B4ATK1IXOKA4HORA297CGYNT", 3, "Kaya", new Guid("0d6275ff-4fa1-4a06-a647-888f70819ed5"), false, null, "osman.kaya@yandex.com" },
                    { new Guid("cd0f0b99-69ba-474c-90ed-0cccdb963a7c"), 0, null, new DateTime(2056, 12, 7, 18, 33, 11, 630, DateTimeKind.Local).AddTicks(7572), null, new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), "8265a3ef-0ff8-41a2-8255-5b5dff0a640c", new DateTime(2023, 5, 18, 18, 33, 11, 630, DateTimeKind.Local).AddTicks(7569), null, "test3@test.com", false, "71527055866", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Ali", "TEST3@TEST.COM", "TEST3@TEST.COM", "AQAAAAEAACcQAAAAEDFzQGCVdq7pSfHLesVk025k5NKyTuHG+JOwUQuWFzvAOJ4oBtP5XGhdjlIPKjIktg==", "+905973236068", false, "Newsreader", null, "LMSXI1L9VBUY3T5MJPXS6BKR7E525WWO", 1, "Özdemir", new Guid("4a36243e-313e-47f8-9834-86ad1bfdf011"), false, null, "test3@test.com" },
                    { new Guid("e1370e43-fd21-4c85-97d2-a45f897be275"), 0, null, new DateTime(2043, 1, 6, 18, 33, 11, 652, DateTimeKind.Local).AddTicks(7501), null, new Guid("f8cb8e4a-2788-4ef1-9f8b-d70e5f4b5a87"), "95fcf270-a7e0-4e7c-b5c9-bc13a8e607d1", new DateTime(2023, 5, 18, 18, 33, 11, 652, DateTimeKind.Local).AddTicks(7500), null, "osman.celik@outlook.com", false, "47788085096", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "OSMAN.CELIK@OUTLOOK.COM", "OSMAN.CELIK@OUTLOOK.COM", "AQAAAAEAACcQAAAAEEZqwLmDlE7FvtqOlZbu4e0qcUY1/icNCdolNrOQHUA/UDEur21iHMAD95UF9pGwQg==", "+905506548596", false, "Doctor", null, "86F9YBE1T3CXFLYZ5LT6UBX1724Z0ZZ2", 3, "Çelik", new Guid("4148b0f2-35d8-4552-8714-ffc92dc694ae"), false, null, "osman.celik@outlook.com" },
                    { new Guid("f0c10cd8-d165-4c12-93f6-4b6887127a0e"), 0, null, new DateTime(2046, 10, 4, 18, 33, 11, 634, DateTimeKind.Local).AddTicks(9022), null, new Guid("868440d4-ab7f-4155-8a1d-698fadda0644"), "384ade3d-e954-4051-8f5b-85646b90e8c2", new DateTime(2023, 5, 18, 18, 33, 11, 634, DateTimeKind.Local).AddTicks(9020), null, "osman.yildirim@outlook.com", false, "38414776426", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Osman", "OSMAN.YILDIRIM@OUTLOOK.COM", "OSMAN.YILDIRIM@OUTLOOK.COM", "AQAAAAEAACcQAAAAEBisoH0/IrJ6Sl/ZaPN99FWlXDnGhCeMuIi6lM2LoDriMeo/Tc7qCpRmrm8U6YHmhA==", "+905312083794", false, "Shop assistant", null, "2J3W1UN95N140S1H09SOSOL6L8A88P0U", 3, "Yıldırım", new Guid("047a5115-2b1c-4123-95df-dbdd59c55a43"), false, null, "osman.yildirim@outlook.com" },
                    { new Guid("f6ce342a-561f-4d9b-9c60-5fa299e73f61"), 0, null, new DateTime(2075, 7, 18, 18, 33, 11, 632, DateTimeKind.Local).AddTicks(1525), null, new Guid("8ecd092c-4e1f-4a64-aea1-7a4bcd4a9a22"), "c6f7d5e1-0635-451e-8d8f-567674238799", new DateTime(2023, 5, 18, 18, 33, 11, 632, DateTimeKind.Local).AddTicks(1522), null, "test4@test.com", false, "54325656466", "/images/UserPhotos/defaultuser.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Yusuf", "TEST4@TEST.COM", "TEST4@TEST.COM", "AQAAAAEAACcQAAAAEHU12jYvjl8WaDmX5NWU6GfnS4BAEcdGPmOPEcO4ThHjt9+Mp9zgj/1V990OINqxOw==", "+905997064325", false, "Soldier", null, "PXBFEW7EX2W5AYTZ8VDC1IAYPRQOI926", 1, "Çelik", new Guid("f0fbaf29-cb40-445d-aad1-529cba19d1e2"), false, null, "test4@test.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("05f638e8-e75d-464a-bdeb-33a4e0ceabe9") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("0ff0eba9-3c7b-41ca-b770-9ca74a7a7c41") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("10083ba2-0926-4297-926a-9196ed002c30") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("3e783ca2-5b37-420e-971a-69cfb1d8d749") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("49e4161c-eda9-4543-8937-5902f78da11a") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("50f9458f-1931-4f0e-9fdb-f6c04e4946d4") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("5415c26e-556e-4392-9ac9-dafac9d49329") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("54fa39d6-b8d2-4881-bc31-7218c1697b49") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("57f0cc7b-4a3d-4d7d-bcbb-0c46ac56bd18") },
                    { new Guid("ca212714-3182-45bd-ae6e-98920a78a6b7"), new Guid("679e9d0b-d12b-472a-b52c-0df33a5ced10") },
                    { new Guid("ca212714-3182-45bd-ae6e-98920a78a6b7"), new Guid("681478c6-242c-4dcc-85ec-2ea7baf47a85") },
                    { new Guid("ca212714-3182-45bd-ae6e-98920a78a6b7"), new Guid("6c655f87-fe0d-43b5-887a-6c88f78eadc4") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("72f5f61f-5063-4363-b951-94f5b82ddcf7") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("7efe0ef2-774d-48d1-90fb-c3ae16877087") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("96b83eb2-b6db-4834-b126-7472424254dd") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("b11aad72-9709-4e51-a7fe-cec4ba278173") },
                    { new Guid("ca212714-3182-45bd-ae6e-98920a78a6b7"), new Guid("cd0f0b99-69ba-474c-90ed-0cccdb963a7c") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("e1370e43-fd21-4c85-97d2-a45f897be275") },
                    { new Guid("b2a02c7c-9c65-4eed-9860-2a16470df245"), new Guid("f0c10cd8-d165-4c12-93f6-4b6887127a0e") },
                    { new Guid("ca212714-3182-45bd-ae6e-98920a78a6b7"), new Guid("f6ce342a-561f-4d9b-9c60-5fa299e73f61") }
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
