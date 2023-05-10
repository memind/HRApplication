using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKApplication.Persistance.Migrations
{
    public partial class ilk : Migration
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
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("efca208f-a077-42a8-ae00-2ddb74878a3d"), "NUBOZJBK57D7K55YX5JUDNCEZFZ40LA8", "Site Administrator", "SITE ADMINITRATOR" },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), "7B3JJ3RSBPP5HAS3M9TPQM1XZRMIMEG2", "Company Administrator", "COMPANY ADMINITRATOR" },
                    { new Guid("fc1c96b4-c0d8-4b43-ba6e-24128bd1407a"), "BVPK0ZK5N6BV6LC0SEIG3302D6F92YLN", "Personal", "PERSONAL" }
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("0efcd5c9-9104-4bb5-93d5-39eaa77c50df"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5631), null, "Medya, İletişim ve Yayıncılık", 1, null },
                    { new Guid("306a1776-c093-4c78-8487-4ed2afe4b0c2"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5635), null, "Sağlık ve Sosyal Hizmetler", 1, null },
                    { new Guid("35721b3c-b4b6-415c-bac6-af039cc66c6b"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5571), null, "Bilişim", 1, null },
                    { new Guid("375b1a96-2220-40e3-a6ac-9500dfb38a57"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5644), null, "Ticaret (Satış ve Pazarlama)", 1, null },
                    { new Guid("43d66745-1058-4b76-93d0-f3f89a0216fc"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5637), null, "Spor ve Rekreasyon", 1, null },
                    { new Guid("4696db45-be46-43e3-b1b9-e535f2eba7a8"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5595), null, "Finans", 1, null },
                    { new Guid("493365a6-0723-4262-8a86-666da1c281c7"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5633), null, "Otomotiv", 1, null },
                    { new Guid("52cb8ccc-7f80-40f4-8c80-20082ec5d8cc"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5556), null, "Ahşap Teknolojisi", 1, null },
                    { new Guid("7304cf78-6888-47ab-806d-6f624aecbca8"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5648), null, "Ulaştırma, Lojistik ve Haberleşme", 1, null },
                    { new Guid("75ab3467-bb74-4c93-8f46-20e438f9a1c6"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5588), null, "Cam, Çimento ve ToprakÇevre", 1, null },
                    { new Guid("86f3b215-bf14-4dd4-9c0a-dc0887ed7791"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5621), null, "İnşaat", 1, null },
                    { new Guid("8af8e9a3-3df1-4477-933c-1dee9db110be"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5628), null, "Kültür, Sanat ve Tasarım", 1, null },
                    { new Guid("934ad3dd-a32e-4d4f-a129-e3023bba7059"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5591), null, "Elektrik ve Elektronik", 1, null },
                    { new Guid("997e21d1-45d8-4b85-96d3-36023d909750"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5641), null, "Tarım, Avcılık ve Balıkçılık", 1, null },
                    { new Guid("9e9f2ddd-e399-4996-9226-0ac92eb56a22"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5589), null, "Eğitim", 1, null },
                    { new Guid("c30f3a33-5fc9-4b92-b59d-a8d419f73686"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5642), null, "Tekstil, Hazır Giyim, Deri", 1, null },
                    { new Guid("d98bbfee-ffed-4282-b228-4a7d2dbcf5ab"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5623), null, "İş ve Yönetimi", 1, null },
                    { new Guid("daa79903-2cdb-4522-a8c1-997727703ba0"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5632), null, "Metal", 1, null },
                    { new Guid("dcb97d68-d508-4b0f-8fb9-c44e8715920c"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5594), null, "Enerji", 1, null },
                    { new Guid("e18c0b92-b7c5-4262-84ba-492b23e17b53"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5627), null, "Kimya, Petrol, Lastik ve PLastik", 1, null },
                    { new Guid("e3fc7f33-e773-4f9b-92c8-1c5980b87235"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5620), null, "Gıda", 1, null },
                    { new Guid("f731d347-045e-427e-bfc7-b5dd534be21b"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5629), null, "Maden", 1, null },
                    { new Guid("fa699c77-08d0-49a1-bfe9-bd2e1f171170"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5645), null, "Toplumsal ve Kişisel Hizmetler", 1, null },
                    { new Guid("ff0e4b8b-0d58-4fa1-b4c5-17d6828c5273"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5646), null, "Turizm, Konaklama, Yiyecek-İçecek Hizmetleri", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "NumberOfEmployees", "PhoneNumber", "SectorId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("27b061ab-87c3-40c5-95b0-b1e694bea44b"), new DateTime(2023, 5, 10, 10, 59, 21, 662, DateTimeKind.Local).AddTicks(2484), null, "info@ozturkkooperatifsirketi.com", "Öztürk Anonim Şirketi", 80, "+905718240521", new Guid("375b1a96-2220-40e3-a6ac-9500dfb38a57"), 3, null },
                    { new Guid("52b2624e-c2bb-4388-8ca5-0b1883500992"), new DateTime(2023, 5, 10, 10, 59, 21, 645, DateTimeKind.Local).AddTicks(3519), null, "info@ozturkkollektifsirketi.com", "Öztürk Komandit Şirketi", 65, "+905332051574", new Guid("43d66745-1058-4b76-93d0-f3f89a0216fc"), 3, null },
                    { new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5740), null, "ikapp@ikapp.com", "IKApp A.Ş.", 5, "+905463813258", new Guid("35721b3c-b4b6-415c-bac6-af039cc66c6b"), 1, null },
                    { new Guid("71db8e77-31db-4b05-93f3-8391aba50655"), new DateTime(2023, 5, 10, 10, 59, 21, 667, DateTimeKind.Local).AddTicks(9168), null, "info@yildizkooperatifsirketi.com", "Yıldız Limited Şirketi", 58, "+905405121077", new Guid("934ad3dd-a32e-4d4f-a129-e3023bba7059"), 3, null },
                    { new Guid("7295f361-d22f-47d4-a242-408119c1eb6b"), new DateTime(2023, 5, 10, 10, 59, 21, 651, DateTimeKind.Local).AddTicks(486), null, "info@yildizkooperatifsirketi.com", "Yıldız Anonim Şirketi", 58, "+905281103084", new Guid("4696db45-be46-43e3-b1b9-e535f2eba7a8"), 3, null },
                    { new Guid("81b12ec9-da90-47c6-ab90-fb1b8ea7dbd4"), new DateTime(2023, 5, 10, 10, 59, 21, 611, DateTimeKind.Local).AddTicks(1244), null, "info@ozturkkomanditsirketi.com", "Öztürk Komandit Şirketi", 8, "+905703075699", new Guid("0efcd5c9-9104-4bb5-93d5-39eaa77c50df"), 3, null },
                    { new Guid("850b038c-2e4f-4035-9bdb-fa264da09a84"), new DateTime(2023, 5, 10, 10, 59, 21, 679, DateTimeKind.Local).AddTicks(1661), null, "info@ozdemirlimitedsirketi.com", "Özdemir Anonim Şirketi", 26, "+905954270206", new Guid("997e21d1-45d8-4b85-96d3-36023d909750"), 3, null },
                    { new Guid("8a1f69b4-98fa-43d0-8f47-75ac79553f5b"), new DateTime(2023, 5, 10, 10, 59, 21, 622, DateTimeKind.Local).AddTicks(5535), null, "info@celikkomanditsirketi.com", "Çelik Kollektif Şirketi", 89, "+905778579682", new Guid("c30f3a33-5fc9-4b92-b59d-a8d419f73686"), 3, null },
                    { new Guid("935ed7bb-a385-4c3f-8377-22a40d4ec03a"), new DateTime(2023, 5, 10, 10, 59, 21, 628, DateTimeKind.Local).AddTicks(3213), null, "info@sahinkollektifsirketi.com", "Şahin Limited Şirketi", 28, "+905907596943", new Guid("75ab3467-bb74-4c93-8f46-20e438f9a1c6"), 3, null },
                    { new Guid("975b516d-ca6c-4e65-906b-b6376c7aeb1a"), new DateTime(2023, 5, 10, 10, 59, 21, 634, DateTimeKind.Local).AddTicks(282), null, "info@demirkooperatifsirketi.com", "Demir Limited Şirketi", 59, "+905685005279", new Guid("e3fc7f33-e773-4f9b-92c8-1c5980b87235"), 3, null },
                    { new Guid("ab8c212d-b262-4fc4-9e55-ef59a4c86daf"), new DateTime(2023, 5, 10, 10, 59, 21, 656, DateTimeKind.Local).AddTicks(6236), null, "info@yildirimkomanditsirketi.com", "Yıldırım Kollektif Şirketi", 35, "+905901900813", new Guid("9e9f2ddd-e399-4996-9226-0ac92eb56a22"), 3, null },
                    { new Guid("b8939d8f-e18b-469f-b9bb-917f3468bbbd"), new DateTime(2023, 5, 10, 10, 59, 21, 673, DateTimeKind.Local).AddTicks(5379), null, "info@ozturkkollektifsirketi.com", "Öztürk Limited Şirketi", 20, "+905988236496", new Guid("375b1a96-2220-40e3-a6ac-9500dfb38a57"), 3, null },
                    { new Guid("c0fa8b33-0e67-4e3a-b1a9-66ef6c1721ee"), new DateTime(2023, 5, 10, 10, 59, 21, 616, DateTimeKind.Local).AddTicks(8420), null, "info@celikkollektifsirketi.com", "Çelik Kollektif Şirketi", 97, "+905375585510", new Guid("934ad3dd-a32e-4d4f-a129-e3023bba7059"), 3, null },
                    { new Guid("c1b583df-72e3-4461-a136-f304c55a155a"), new DateTime(2023, 5, 10, 10, 59, 21, 639, DateTimeKind.Local).AddTicks(7325), null, "info@yildizkollektifsirketi.com", "Yıldız Limited Şirketi", 73, "+905200901915", new Guid("dcb97d68-d508-4b0f-8fb9-c44e8715920c"), 3, null },
                    { new Guid("e63470f7-522e-4209-a3dc-056593a063f6"), new DateTime(2023, 5, 10, 10, 59, 21, 599, DateTimeKind.Local).AddTicks(5312), null, "info@demirkollektifsirketi.com", "Demir Kollektif Şirketi", 19, "+905786733832", new Guid("934ad3dd-a32e-4d4f-a129-e3023bba7059"), 3, null },
                    { new Guid("f633590b-6832-4639-8019-52050ebe81b3"), new DateTime(2023, 5, 10, 10, 59, 21, 605, DateTimeKind.Local).AddTicks(2956), null, "info@yildirimanonimsirketi.com", "Yıldırım Kollektif Şirketi", 7, "+905208290364", new Guid("4696db45-be46-43e3-b1b9-e535f2eba7a8"), 3, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("012a5b9c-9d64-4013-92e5-830a0b586c26"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5835), null, "Occupational Therapy Aide", 1, null },
                    { new Guid("09081201-35fa-4eac-9c07-7e69c0632d5e"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5844), null, "General Manager", 1, null },
                    { new Guid("0fbd9908-e9d9-4a3e-b4a6-62b860286ada"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5765), null, "CMO (Chief Marketing Officer)", 1, null },
                    { new Guid("13dd469d-66af-4fb5-a902-571f31035515"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5853), null, "Construction Foreman", 1, null },
                    { new Guid("157264d2-0411-4c8a-a5e9-13e0c8ac2d37"), new Guid("f633590b-6832-4639-8019-52050ebe81b3"), new DateTime(2023, 5, 10, 10, 59, 21, 605, DateTimeKind.Local).AddTicks(2959), null, "Sales Associate", 1, null },
                    { new Guid("189782ef-1f94-481f-8044-9dbc87c55189"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5796), null, "Director of Information Security", 1, null },
                    { new Guid("1935b337-e6d1-4a62-991a-c2af101d01fd"), new Guid("7295f361-d22f-47d4-a242-408119c1eb6b"), new DateTime(2023, 5, 10, 10, 59, 21, 651, DateTimeKind.Local).AddTicks(492), null, "Principal", 1, null },
                    { new Guid("22747ab2-6406-4781-b80e-be01523c6706"), new Guid("8a1f69b4-98fa-43d0-8f47-75ac79553f5b"), new DateTime(2023, 5, 10, 10, 59, 21, 622, DateTimeKind.Local).AddTicks(5541), null, "COO (Chief Operating Officer)", 1, null },
                    { new Guid("2b9f6c95-bd09-4ab7-bc1c-8e9bbd2931d5"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5780), null, "VP of Client Services", 1, null },
                    { new Guid("2d81cefe-8968-42df-8dbc-64730f8175b3"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5848), null, "Concierge", 1, null },
                    { new Guid("317f0b14-e194-4e6f-8e1c-c299630f59dd"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5784), null, "Customer Success Manager", 1, null },
                    { new Guid("3365f2ab-bf13-43ea-ae55-655081070da0"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5837), null, "Principal", 1, null },
                    { new Guid("34865807-1657-4708-8c5b-6ffa5d1e87c3"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5803), null, "Data Analyst", 1, null },
                    { new Guid("398fc98a-c435-4186-9a67-503e7fed6cfd"), new Guid("935ed7bb-a385-4c3f-8377-22a40d4ec03a"), new DateTime(2023, 5, 10, 10, 59, 21, 628, DateTimeKind.Local).AddTicks(3219), null, "Physical Therapist", 1, null },
                    { new Guid("41f8ae5e-2d87-4145-b710-6eb3cfd628fe"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5766), null, "Marketing Director", 1, null },
                    { new Guid("42c7d449-01d1-4e01-b469-f96db1c00b27"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5791), null, "Director of Business Operations", 1, null },
                    { new Guid("45060650-82c2-4444-a8a0-e75f52605523"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5792), null, "Operations Supervisor", 1, null },
                    { new Guid("468ba217-c0dd-4318-9ae1-9b185b66f1c2"), new Guid("b8939d8f-e18b-469f-b9bb-917f3468bbbd"), new DateTime(2023, 5, 10, 10, 59, 21, 673, DateTimeKind.Local).AddTicks(5383), null, "Pharmacy Technician", 1, null },
                    { new Guid("4885b988-326b-4e73-9a21-2c44de3e5681"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5851), null, "Server/Host/Hostess", 1, null },
                    { new Guid("4ee96015-cbdb-4d00-bfd6-c46344da4629"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5793), null, "Sr. Manager of HR", 1, null },
                    { new Guid("51e1e65f-62a0-4867-877d-1bbbdcd16535"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5852), null, "Hotel Receptionist", 1, null },
                    { new Guid("5531095b-e912-4cd1-8374-68b9da72fc38"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5832), null, "Clinical Laboratory Technician", 1, null },
                    { new Guid("5ee7c677-70e2-424a-91b8-02e115fdee2a"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5789), null, "COO (Chief Operating Officer)", 1, null },
                    { new Guid("61eec2d6-0ef9-4517-bc18-84a205ee0fad"), new Guid("81b12ec9-da90-47c6-ab90-fb1b8ea7dbd4"), new DateTime(2023, 5, 10, 10, 59, 21, 611, DateTimeKind.Local).AddTicks(1253), null, "Procurement Director", 1, null },
                    { new Guid("61f3cab8-fd5e-4765-86c9-b4cdccd867b6"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5769), null, "Marketing Analyst", 1, null },
                    { new Guid("6312ea8a-a7f4-4848-aa53-04586d21d43f"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5840), null, "School Counselor", 1, null },
                    { new Guid("65429260-52f7-45e7-b2a9-b3a9c212bc6f"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5797), null, "Software Engineer I, II, III", 1, null },
                    { new Guid("6852e40a-336d-4816-a3e7-df530a72b9bd"), new Guid("ab8c212d-b262-4fc4-9e55-ef59a4c86daf"), new DateTime(2023, 5, 10, 10, 59, 21, 656, DateTimeKind.Local).AddTicks(6240), null, "Pharmacy Technician", 1, null },
                    { new Guid("6aa8e93d-cff1-44bb-86eb-2aaca322a3e4"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5772), null, "VP of Finance", 1, null },
                    { new Guid("6d65904a-3236-47d6-9a67-fd48b81f1206"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5847), null, "Guest Services Supervisor", 1, null },
                    { new Guid("705da81d-318b-439e-8850-a1dc4086268b"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5829), null, "Physical Therapist", 1, null },
                    { new Guid("71751f4f-951f-49fb-ac64-e052b2f8144f"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5795), null, "HR Analyst", 1, null },
                    { new Guid("740570ee-845b-4c46-96ff-29c765141dec"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5841), null, "Teacher", 1, null },
                    { new Guid("7ea456f7-a335-48fd-88f1-a934597e72fc"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5779), null, "Risk Analyst", 1, null },
                    { new Guid("87e930e5-7a74-4ce2-9a15-194bf5ba8013"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5778), null, "Credit Analyst", 1, null },
                    { new Guid("99978318-d25f-4577-bae4-b3f89365c8f2"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5785), null, "Customer Service Representative", 1, null },
                    { new Guid("99d141fa-cdf8-4c1c-9bf9-784fa8ed0ae9"), new Guid("27b061ab-87c3-40c5-95b0-b1e694bea44b"), new DateTime(2023, 5, 10, 10, 59, 21, 662, DateTimeKind.Local).AddTicks(2488), null, "Credit Analyst", 1, null },
                    { new Guid("9b3c57b7-b996-4b39-8e23-e4f1514ceadd"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5839), null, "Registrar", 1, null },
                    { new Guid("9d763319-90b2-48ec-83b2-fbe593e04e0a"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5826), null, "Registered Nurse", 1, null },
                    { new Guid("a17456da-4015-4d08-ba06-f1973b99ecbc"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5756), null, "National Sales Director", 1, null },
                    { new Guid("a8899a31-48be-4319-beed-f01d5b754779"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5859), null, "Contract Administrator", 1, null },
                    { new Guid("aec71e47-4433-4ab1-8e85-9b4d625a2747"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5762), null, "Sales Associate", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("af94727c-fed1-4cb0-a078-e63e44939afc"), new Guid("c0fa8b33-0e67-4e3a-b1a9-66ef6c1721ee"), new DateTime(2023, 5, 10, 10, 59, 21, 616, DateTimeKind.Local).AddTicks(8428), null, "General Manager", 1, null },
                    { new Guid("b0418781-57fd-4bb4-97ef-75205cd56de7"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5758), null, "Regional Sales Manager", 1, null },
                    { new Guid("b4a2488a-650a-4c11-a024-c83b7f145e31"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5771), null, "Marketing Coordinator", 1, null },
                    { new Guid("b958ff00-fa34-40c4-9615-5a4436ef6d40"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5777), null, "Investment Analyst", 1, null },
                    { new Guid("b9a5c0c5-e4d3-4270-856e-6a8969371f90"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5754), null, "VP of Sales", 1, null },
                    { new Guid("be157216-35bd-4c8c-9f45-681a25785c50"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5849), null, "Front Desk Associate", 1, null },
                    { new Guid("bfbf67d7-8026-4917-aa24-46025b218d15"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5831), null, "Nursing Assistant", 1, null },
                    { new Guid("c7f61837-ea3a-4258-9b0d-da97162da128"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5801), null, "Systems Administrator", 1, null },
                    { new Guid("cbe4cc4e-078c-4739-a678-46d05d54531c"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5861), null, "Inspector", 1, null },
                    { new Guid("cd032e86-f522-4b53-acff-c18ac50308ed"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5773), null, "Procurement Director", 1, null },
                    { new Guid("cf116d9a-12c0-40df-9d75-6576c6ec6876"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5824), null, "Other Industries:", 1, null },
                    { new Guid("d0a7e9d2-f18a-4aa4-8748-1aa6888cb943"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5854), null, "Safety Director", 1, null },
                    { new Guid("d241ab24-1dc9-4cf3-832f-2a42bbf9611e"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5842), null, "Teaching Assistant", 1, null },
                    { new Guid("d3f70fd5-b4ed-41d5-a709-f644dfbcae4a"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5860), null, "Project Appraisal Engineer", 1, null },
                    { new Guid("d77a3c7a-0205-4ae6-9078-8e9d9afeda3e"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5798), null, "Full Stack Developer", 1, null },
                    { new Guid("d85fdbe0-fbbd-4f76-83fd-332db5c19e3c"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5836), null, "Administrator", 1, null },
                    { new Guid("dbbbb3e5-5cb7-4221-a27e-499768e3f74e"), new Guid("e63470f7-522e-4209-a3dc-056593a063f6"), new DateTime(2023, 5, 10, 10, 59, 21, 599, DateTimeKind.Local).AddTicks(5320), null, "Occupational Therapy Aide", 1, null },
                    { new Guid("deca627e-b731-46b0-81a1-87925d62ed61"), new Guid("975b516d-ca6c-4e65-906b-b6376c7aeb1a"), new DateTime(2023, 5, 10, 10, 59, 21, 634, DateTimeKind.Local).AddTicks(286), null, "Support Specialist", 1, null },
                    { new Guid("e1cdf8fa-0d15-46e6-8239-25d18502d861"), new Guid("71db8e77-31db-4b05-93f3-8391aba50655"), new DateTime(2023, 5, 10, 10, 59, 21, 667, DateTimeKind.Local).AddTicks(9173), null, "Teacher", 1, null },
                    { new Guid("e9a16a6f-6fcd-4334-abc7-643ce02a6e17"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5827), null, "Pharmacy Technician", 1, null },
                    { new Guid("ea37ae29-0fcf-46a7-8b78-d92095d5a8be"), new Guid("c1b583df-72e3-4461-a136-f304c55a155a"), new DateTime(2023, 5, 10, 10, 59, 21, 639, DateTimeKind.Local).AddTicks(7332), null, "Customer Success Manager", 1, null },
                    { new Guid("ef57645b-0cd3-4b57-8a2d-c75ca508b780"), new Guid("850b038c-2e4f-4035-9bdb-fa264da09a84"), new DateTime(2023, 5, 10, 10, 59, 21, 679, DateTimeKind.Local).AddTicks(1666), null, "VP of Finance", 1, null },
                    { new Guid("f4a18f19-b5b7-4330-a0d7-74d6bf744b9d"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5856), null, "Project Manager", 1, null },
                    { new Guid("fb311c61-e3f8-4489-a594-1e8379f1dae8"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5786), null, "Support Specialist", 1, null },
                    { new Guid("fdad674c-4956-40cc-8adb-5dac84d9240f"), new Guid("52b2624e-c2bb-4388-8ca5-0b1883500992"), new DateTime(2023, 5, 10, 10, 59, 21, 645, DateTimeKind.Local).AddTicks(3522), null, "Procurement Director", 1, null },
                    { new Guid("fe031f48-94fb-4c94-b65f-c848d7a761a9"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5767), null, "Sr. Marketing Manager", 1, null },
                    { new Guid("ff32f235-fb99-4bb1-b047-3236e2741db7"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5759), null, "Sales Representative", 1, null },
                    { new Guid("ff6bffab-ef00-4130-9001-7a5b839deacd"), new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5782), null, "Account Manager", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "BloodGroup", "CompanyId", "ConcurrencyStamp", "CreateDate", "DeleteDate", "Email", "EmailConfirmed", "IdentityNumber", "ImagePath", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profession", "SecondName", "SecurityStamp", "Status", "Surname", "TitleId", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("2bf1ad77-d254-403d-84a5-fda515a10451"), 0, new DateTime(2074, 12, 7, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5876), null, new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), "45a3b645-42d9-4e79-bf33-078a47988206", new DateTime(2023, 5, 10, 10, 59, 21, 570, DateTimeKind.Local).AddTicks(5873), null, "test0@test.com", false, "5360401548", "/images/UserPhotos/defaultuser.jpg", false, null, "Mustafa", "TEST0@TEST.COM", "TEST0@TEST.COM", "AQAAAAEAACcQAAAAEPBxUgtebd+MVI2CLLLlWFCmcMhr/p/U7U9zPWLVvXMW+7kWtKijeY5mxeEK9dVxxg==", "+905708003192", false, "Butcher", null, "EMQW2FPB8RLBV3L2XMH2QFRCPN5H0XG9", 1, "Yılmaz", new Guid("99978318-d25f-4577-bae4-b3f89365c8f2"), false, null, "test0@test.com" },
                    { new Guid("2df709e8-9478-4247-aeb5-608d0c273224"), 0, new DateTime(2043, 1, 7, 10, 59, 21, 628, DateTimeKind.Local).AddTicks(3230), null, new Guid("935ed7bb-a385-4c3f-8377-22a40d4ec03a"), "2c16c466-ac8c-47fe-a5b9-07fc41cedbfc", new DateTime(2023, 5, 10, 10, 59, 21, 628, DateTimeKind.Local).AddTicks(3228), null, "yusuf.sahin@yahoo.com", false, "1071834790", "/images/UserPhotos/defaultuser.jpg", false, null, "Yusuf", "YUSUF.SAHIN@YAHOO.COM", "YUSUF.SAHIN@YAHOO.COM", "AQAAAAEAACcQAAAAEC2kqwtMi7jIOzaOueyi/9zDcwno9afpRT8npsBxvYgNrDkC+okwNUj4yo8r6dSPlQ==", "+905636778337", false, "Model", null, "4QUD4XE1WTRU2U0REZSMX1ECRVYPVNHN", 3, "Şahin", new Guid("398fc98a-c435-4186-9a67-503e7fed6cfd"), false, null, "yusuf.sahin@yahoo.com" },
                    { new Guid("4375c33f-37b5-450b-b9c0-642d9c7515a0"), 0, new DateTime(2063, 10, 9, 10, 59, 21, 662, DateTimeKind.Local).AddTicks(2492), null, new Guid("27b061ab-87c3-40c5-95b0-b1e694bea44b"), "e90a33ca-96e0-4db6-b848-0ff9ce1281f1", new DateTime(2023, 5, 10, 10, 59, 21, 662, DateTimeKind.Local).AddTicks(2491), null, "ali.ozturk@yandex.com", false, "1843701004", "/images/UserPhotos/defaultuser.jpg", false, null, "Ali", "ALI.OZTURK@YANDEX.COM", "ALI.OZTURK@YANDEX.COM", "AQAAAAEAACcQAAAAEKq8Ue0fSS4CNB2iYFY/MqBHGNp7BbzHmjIluEqPwEL/sn6n+CgR7zdccKK1jfhyhA==", "+905735406823", false, "Doctor", null, "CFREL0UKB6U46OII91ANFJ5CV6SG6R4B", 3, "Öztürk", new Guid("99d141fa-cdf8-4c1c-9bf9-784fa8ed0ae9"), false, null, "ali.ozturk@yandex.com" },
                    { new Guid("48da37b6-0065-4ae9-ae7c-dbf4d00ee742"), 0, new DateTime(2057, 9, 5, 10, 59, 21, 616, DateTimeKind.Local).AddTicks(8433), null, new Guid("c0fa8b33-0e67-4e3a-b1a9-66ef6c1721ee"), "538743ed-be27-42c5-8234-7e7c9172098f", new DateTime(2023, 5, 10, 10, 59, 21, 616, DateTimeKind.Local).AddTicks(8432), null, "ali.celik@google.com", false, "3848070246", "/images/UserPhotos/defaultuser.jpg", false, null, "Ali", "ALI.CELIK@GOOGLE.COM", "ALI.CELIK@GOOGLE.COM", "AQAAAAEAACcQAAAAEMtjV8T7yTqt+EK8n9xADNFesTYvVYoNuPeVlCGWhDysnf2FU1hkIVvb+qdyAWmQIw==", "+905672366938", false, "Cleaner", null, "HWZFFOU3WGLVHDMBUMGGKYL7VTB93BPU", 3, "Çelik", new Guid("af94727c-fed1-4cb0-a078-e63e44939afc"), false, null, "ali.celik@google.com" },
                    { new Guid("53d9a0b1-89f6-4587-9dc7-a6d36232e35c"), 0, new DateTime(2051, 9, 23, 10, 59, 21, 605, DateTimeKind.Local).AddTicks(2967), null, new Guid("f633590b-6832-4639-8019-52050ebe81b3"), "57e3cc20-bf78-429a-b926-132599162847", new DateTime(2023, 5, 10, 10, 59, 21, 605, DateTimeKind.Local).AddTicks(2966), null, "hasan.yildirim@yandex.com", false, "7600647550", "/images/UserPhotos/defaultuser.jpg", false, null, "Hasan", "HASAN.YILDIRIM@YANDEX.COM", "HASAN.YILDIRIM@YANDEX.COM", "AQAAAAEAACcQAAAAEInAVd8Hi5HzbQHgW/5JS8m7R5NNuGehL+RC7g4Cls2Ol5cNcJBPvvLqm9H7qI846A==", "+905741729597", false, "Carpenter", null, "IAUXM7D2EPJE4X3BAWIQUH1OX05TVC48", 3, "Yıldırım", new Guid("157264d2-0411-4c8a-a5e9-13e0c8ac2d37"), false, null, "hasan.yildirim@yandex.com" },
                    { new Guid("5dc08428-481b-44f3-be90-cca3f1d5225d"), 0, new DateTime(2041, 6, 26, 10, 59, 21, 656, DateTimeKind.Local).AddTicks(6289), null, new Guid("ab8c212d-b262-4fc4-9e55-ef59a4c86daf"), "16d99286-0370-4149-a61d-af0b39ddc20a", new DateTime(2023, 5, 10, 10, 59, 21, 656, DateTimeKind.Local).AddTicks(6287), null, "mustafa.yildirim@hotmail.com", false, "1315865100", "/images/UserPhotos/defaultuser.jpg", false, null, "Mustafa", "MUSTAFA.YILDIRIM@HOTMAIL.COM", "MUSTAFA.YILDIRIM@HOTMAIL.COM", "AQAAAAEAACcQAAAAENIH2H9K0LyvzygiK8d8b7gZLlXAvOk/YowoEtVJH6WPUFi3AO5qLzIunIkUGu+mLw==", "+905977313064", false, "Cleaner", null, "FHIF7JVF5JVGYTO6NIT8N8N960NSOXNB", 3, "Yıldırım", new Guid("6852e40a-336d-4816-a3e7-df530a72b9bd"), false, null, "mustafa.yildirim@hotmail.com" },
                    { new Guid("6f101a49-324b-40b5-a7eb-7612dd104a4f"), 0, new DateTime(2051, 6, 27, 10, 59, 21, 611, DateTimeKind.Local).AddTicks(1261), null, new Guid("81b12ec9-da90-47c6-ab90-fb1b8ea7dbd4"), "4ef8e019-1bce-4a04-97ea-a0a1d69de827", new DateTime(2023, 5, 10, 10, 59, 21, 611, DateTimeKind.Local).AddTicks(1259), null, "huseyin.ozturk@microsoft.com", false, "4516726034", "/images/UserPhotos/defaultuser.jpg", false, null, "Hüseyin", "HUSEYIN.OZTURK@MICROSOFT.COM", "HUSEYIN.OZTURK@MICROSOFT.COM", "AQAAAAEAACcQAAAAEAJWx45bDSFVrUdXgLxNNgXx8/TfSSlhDawgXvl8hdOjZs9v4hNhGAXxh6mHGIKKxw==", "+905233749205", false, "Model", null, "5VJAYH3FNTWQ038WOJWKILLJVNQBETZK", 3, "Öztürk", new Guid("61eec2d6-0ef9-4517-bc18-84a205ee0fad"), false, null, "huseyin.ozturk@microsoft.com" },
                    { new Guid("7604f0b7-5032-47ec-a246-eda6388047a7"), 0, new DateTime(2065, 3, 4, 10, 59, 21, 645, DateTimeKind.Local).AddTicks(3527), null, new Guid("52b2624e-c2bb-4388-8ca5-0b1883500992"), "eacf9cf3-6de5-4409-9206-9e7b3bb8467c", new DateTime(2023, 5, 10, 10, 59, 21, 645, DateTimeKind.Local).AddTicks(3526), null, "ahmet.ozturk@outlook.com", false, "6040371012", "/images/UserPhotos/defaultuser.jpg", false, null, "Ahmet", "AHMET.OZTURK@OUTLOOK.COM", "AHMET.OZTURK@OUTLOOK.COM", "AQAAAAEAACcQAAAAEH4WqS2GC6TT0X8an+uMFY2iwfUD4Q+4J1KlAqZTpkBvk/1MlK8PW2I2PCSes/lWCA==", "+905199946077", false, "Policeman/Policewoman", null, "CM7OCOBKOWLXVLCZWZZ2R5BYNF07HTS7", 3, "Öztürk", new Guid("fdad674c-4956-40cc-8adb-5dac84d9240f"), false, null, "ahmet.ozturk@outlook.com" },
                    { new Guid("8ae10070-462a-4545-8f91-57064334a5f4"), 0, new DateTime(2059, 2, 21, 10, 59, 21, 673, DateTimeKind.Local).AddTicks(5390), null, new Guid("b8939d8f-e18b-469f-b9bb-917f3468bbbd"), "ddf5254e-72a1-4e15-ac2f-8a313eb96b7e", new DateTime(2023, 5, 10, 10, 59, 21, 673, DateTimeKind.Local).AddTicks(5389), null, "ahmet.ozturk@microsoft.com", false, "7747516726", "/images/UserPhotos/defaultuser.jpg", false, null, "Ahmet", "AHMET.OZTURK@MICROSOFT.COM", "AHMET.OZTURK@MICROSOFT.COM", "AQAAAAEAACcQAAAAEBkIf2rq8tu/1D6L/uVlNyvwiSnPzXXYE36j6wmtlT22xQJm48IPezey1UigCp79vA==", "+905176537789", false, "Dancer", null, "4IZIC02AD6NAMLP6YD7NVMTCCIE76SJB", 3, "Öztürk", new Guid("468ba217-c0dd-4318-9ae1-9b185b66f1c2"), false, null, "ahmet.ozturk@microsoft.com" },
                    { new Guid("97b3e779-2ad1-40a0-882f-e59d74f09243"), 0, new DateTime(2053, 7, 8, 10, 59, 21, 576, DateTimeKind.Local).AddTicks(4825), null, new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), "55fdad41-c4ff-4e88-8e4a-f177e7f40986", new DateTime(2023, 5, 10, 10, 59, 21, 576, DateTimeKind.Local).AddTicks(4822), null, "test1@test.com", false, "6234353860", "/images/UserPhotos/defaultuser.jpg", false, null, "Mehmet", "TEST1@TEST.COM", "TEST1@TEST.COM", "AQAAAAEAACcQAAAAECVpgvipb9Wc3Lf++5YEqFHaUhU3KbWI4OmZeyqDcQ5joMOqaWlqgYF0FBM46xplHA==", "+905816032042", false, "Lifeguard", null, "EGFQTA7GVVG50ZMJP88W14XVZ3NXTPXL", 1, "Çelik", new Guid("6aa8e93d-cff1-44bb-86eb-2aaca322a3e4"), false, null, "test1@test.com" },
                    { new Guid("9ce6fa87-a6b5-44f0-a5cc-16629f80a745"), 0, new DateTime(2061, 10, 26, 10, 59, 21, 588, DateTimeKind.Local).AddTicks(1187), null, new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), "8c4552ca-cbe8-4497-93f1-de4b5533fbaa", new DateTime(2023, 5, 10, 10, 59, 21, 588, DateTimeKind.Local).AddTicks(1183), null, "test3@test.com", false, "6083881284", "/images/UserPhotos/defaultuser.jpg", false, null, "İbrahim", "TEST3@TEST.COM", "TEST3@TEST.COM", "AQAAAAEAACcQAAAAEA3kZMXdkoXwb8pkzS+8Nqp/a5xXWo/+jTVBelwqMqLpdT97+rI5VEpTZKHy1Ir10A==", "+905417900323", false, "Dentist", null, "O1TJWOSGB1IZ5TA1RW825NTG76FGEXWD", 1, "Yıldız", new Guid("5531095b-e912-4cd1-8374-68b9da72fc38"), false, null, "test3@test.com" },
                    { new Guid("9ed5089c-9058-4779-a414-540e4ef15299"), 0, new DateTime(2043, 8, 17, 10, 59, 21, 582, DateTimeKind.Local).AddTicks(2313), null, new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), "2d87ea4d-11b9-4c07-8140-a8db48ea332d", new DateTime(2023, 5, 10, 10, 59, 21, 582, DateTimeKind.Local).AddTicks(2311), null, "test2@test.com", false, "7336178162", "/images/UserPhotos/defaultuser.jpg", false, null, "Hüseyin", "TEST2@TEST.COM", "TEST2@TEST.COM", "AQAAAAEAACcQAAAAEAD31lS6uVqYsGWT7JgLKBqjWv2jyhJG+dQrZERsHQXZ52l+QQtyle/k3b64U8y1/A==", "+905545142235", false, "Traffic warden", null, "B1V6KFS1WA526GVJL74I6S3Y1YVCB47R", 1, "Kaya", new Guid("2b9f6c95-bd09-4ab7-bc1c-8e9bbd2931d5"), false, null, "test2@test.com" },
                    { new Guid("ae9c039e-cf83-4561-9d26-d27561f67c99"), 0, new DateTime(2041, 12, 26, 10, 59, 21, 599, DateTimeKind.Local).AddTicks(5328), null, new Guid("e63470f7-522e-4209-a3dc-056593a063f6"), "8ab7de84-940e-45dd-bb05-f60bbf850fbe", new DateTime(2023, 5, 10, 10, 59, 21, 599, DateTimeKind.Local).AddTicks(5326), null, "ismail.demir@hotmail.com", false, "5637614394", "/images/UserPhotos/defaultuser.jpg", false, null, "İsmail", "ISMAIL.DEMIR@HOTMAIL.COM", "ISMAIL.DEMIR@HOTMAIL.COM", "AQAAAAEAACcQAAAAEOgjb49xWUg0MKhqcQCx/URi7fokGxOyjOKAyejaFu+lc2aFDR6rLEjbcTHOSTzOSg==", "+905857363360", false, "Traffic warden", null, "XXZ3MMBRDZHVMEFW0FSHBPNVGOSKPG1E", 3, "Demir", new Guid("dbbbb3e5-5cb7-4221-a27e-499768e3f74e"), false, null, "ismail.demir@hotmail.com" },
                    { new Guid("c67d9cbf-7f4e-4953-ab2d-bfb5c323b823"), 0, new DateTime(2054, 2, 1, 10, 59, 21, 639, DateTimeKind.Local).AddTicks(7337), null, new Guid("c1b583df-72e3-4461-a136-f304c55a155a"), "eee3ae28-d844-4116-a7bf-0fa1dd4f4317", new DateTime(2023, 5, 10, 10, 59, 21, 639, DateTimeKind.Local).AddTicks(7336), null, "hasan.yildiz@outlook.com", false, "7778588246", "/images/UserPhotos/defaultuser.jpg", false, null, "Hasan", "HASAN.YILDIZ@OUTLOOK.COM", "HASAN.YILDIZ@OUTLOOK.COM", "AQAAAAEAACcQAAAAEBwPwkg+UEL+K8K8uEx2RZSHvPN+WHX9nqh0i+cyK+tD7gTMsA10cq3rIlMQ8cPESA==", "+905285654457", false, "Real estate agent", null, "C1JHT9RXPW44DS5F1FQSKP11CCXAMAA0", 3, "Yıldız", new Guid("ea37ae29-0fcf-46a7-8b78-d92095d5a8be"), false, null, "hasan.yildiz@outlook.com" },
                    { new Guid("cbc386c4-c86b-4009-86dc-7ab16996bc0a"), 0, new DateTime(2059, 6, 4, 10, 59, 21, 634, DateTimeKind.Local).AddTicks(292), null, new Guid("975b516d-ca6c-4e65-906b-b6376c7aeb1a"), "6ef1293e-5be8-4807-b0e8-73b56b3b00c0", new DateTime(2023, 5, 10, 10, 59, 21, 634, DateTimeKind.Local).AddTicks(290), null, "ibrahim.demir@yahoo.com", false, "8031313706", "/images/UserPhotos/defaultuser.jpg", false, null, "İbrahim", "IBRAHIM.DEMIR@YAHOO.COM", "IBRAHIM.DEMIR@YAHOO.COM", "AQAAAAEAACcQAAAAEPwL1YhCDCnORF01O8S7jxBQRf9Cs71wCvXssE8XYOp3KjazejBVIV/9xvQ13Q8gdQ==", "+905663474789", false, "Politician", null, "SLEJ2MF9BIA973M7NBR7TJI3N8XS77XP", 3, "Demir", new Guid("deca627e-b731-46b0-81a1-87925d62ed61"), false, null, "ibrahim.demir@yahoo.com" },
                    { new Guid("dac58c81-03f6-4eaa-8e22-0ed5c00e6b3e"), 0, new DateTime(2069, 4, 9, 10, 59, 21, 667, DateTimeKind.Local).AddTicks(9179), null, new Guid("71db8e77-31db-4b05-93f3-8391aba50655"), "ad0b692b-c169-40b6-b2a4-a96eae97af76", new DateTime(2023, 5, 10, 10, 59, 21, 667, DateTimeKind.Local).AddTicks(9177), null, "hasan.yildiz@google.com", false, "1254358446", "/images/UserPhotos/defaultuser.jpg", false, null, "Hasan", "HASAN.YILDIZ@GOOGLE.COM", "HASAN.YILDIZ@GOOGLE.COM", "AQAAAAEAACcQAAAAECHbU8rmWf06IYoyp7SsmhzaMZRz560yVMZGA8O2pOcA18+tyfBIHVQ4NnSGtRdDyw==", "+905904892165", false, "Artist", null, "GKSF0QOMLYVNXUSPFLJMS56ZA3XAU56P", 3, "Yıldız", new Guid("e1cdf8fa-0d15-46e6-8239-25d18502d861"), false, null, "hasan.yildiz@google.com" },
                    { new Guid("e273428b-2ff3-4a73-9cf0-292699c5d3bc"), 0, new DateTime(2069, 8, 14, 10, 59, 21, 593, DateTimeKind.Local).AddTicks(8189), null, new Guid("55a97588-97c1-4088-b180-63ff2ec78e3e"), "b63690d6-39ed-48ea-a3e0-02b040e7dc8d", new DateTime(2023, 5, 10, 10, 59, 21, 593, DateTimeKind.Local).AddTicks(8187), null, "test4@test.com", false, "3755873592", "/images/UserPhotos/defaultuser.jpg", false, null, "Hüseyin", "TEST4@TEST.COM", "TEST4@TEST.COM", "AQAAAAEAACcQAAAAENQ9y8sjGZ4+jDWtL+FkPZWgc1rNRig0471JmP3HWOFJ7HJcdUL0j21c0YSm2+nm8A==", "+905835435766", false, "Busdriver", null, "55YYQSTRE1A1RD5614I2OHGCM1AYPWXM", 1, "Şahin", new Guid("012a5b9c-9d64-4013-92e5-830a0b586c26"), false, null, "test4@test.com" },
                    { new Guid("ef846b45-24c4-45c5-b87b-d84a9958ed05"), 0, new DateTime(2066, 4, 29, 10, 59, 21, 651, DateTimeKind.Local).AddTicks(506), null, new Guid("7295f361-d22f-47d4-a242-408119c1eb6b"), "b20b501f-87f2-4e20-aebe-3329532dcb70", new DateTime(2023, 5, 10, 10, 59, 21, 651, DateTimeKind.Local).AddTicks(504), null, "yusuf.yildiz@google.com", false, "6467814792", "/images/UserPhotos/defaultuser.jpg", false, null, "Yusuf", "YUSUF.YILDIZ@GOOGLE.COM", "YUSUF.YILDIZ@GOOGLE.COM", "AQAAAAEAACcQAAAAECYf89DGnZF3DmyqVaD09VNO/EtL1eKKFi4n1UjqGlURm4+kwdx01MsOOUTu+Dk0SA==", "+905687019787", false, "Taxi driver", null, "EXZ1MTX5RQTUA0OXAGUMM6ECHYP1L37Z", 3, "Yıldız", new Guid("1935b337-e6d1-4a62-991a-c2af101d01fd"), false, null, "yusuf.yildiz@google.com" },
                    { new Guid("f0c2c53c-61d6-4ebc-944d-ee8706919bf6"), 0, new DateTime(2071, 9, 8, 10, 59, 21, 679, DateTimeKind.Local).AddTicks(1671), null, new Guid("850b038c-2e4f-4035-9bdb-fa264da09a84"), "79f460e3-1133-4e7c-adeb-8907edd24a5e", new DateTime(2023, 5, 10, 10, 59, 21, 679, DateTimeKind.Local).AddTicks(1670), null, "hasan.ozdemir@yahoo.com", false, "2327030142", "/images/UserPhotos/defaultuser.jpg", false, null, "Hasan", "HASAN.OZDEMIR@YAHOO.COM", "HASAN.OZDEMIR@YAHOO.COM", "AQAAAAEAACcQAAAAEMjh/XOQr/tds53O0XcnpSUGIIYUq2928VGiBI+4EAmqbdP74Qwzl363Sf5WF3JozQ==", "+905258874836", false, "Librarian", null, "M4EBNRT8RH3MUTRD45GMEHQIYLCPYF9H", 3, "Özdemir", new Guid("ef57645b-0cd3-4b57-8a2d-c75ca508b780"), false, null, "hasan.ozdemir@yahoo.com" },
                    { new Guid("f66ec653-fcda-4994-a3ca-91c145065a0b"), 0, new DateTime(2067, 10, 31, 10, 59, 21, 622, DateTimeKind.Local).AddTicks(5547), null, new Guid("8a1f69b4-98fa-43d0-8f47-75ac79553f5b"), "f1506422-cbe1-4aa2-8de7-213878d1c142", new DateTime(2023, 5, 10, 10, 59, 21, 622, DateTimeKind.Local).AddTicks(5546), null, "ali.celik@hotmail.com", false, "1843088394", "/images/UserPhotos/defaultuser.jpg", false, null, "Ali", "ALI.CELIK@HOTMAIL.COM", "ALI.CELIK@HOTMAIL.COM", "AQAAAAEAACcQAAAAEKubSpHGVjLmN3qQx2/lrqruBtNa7ptFwzreI+/e3R1ebMfENafTdpP8gvjUDytxJQ==", "+905160012281", false, "Bricklayer", null, "T9TN89G646QOEWXWVF3G4DQSBJDHCZ3W", 3, "Çelik", new Guid("22747ab2-6406-4781-b80e-be01523c6706"), false, null, "ali.celik@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("efca208f-a077-42a8-ae00-2ddb74878a3d"), new Guid("2bf1ad77-d254-403d-84a5-fda515a10451") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("2df709e8-9478-4247-aeb5-608d0c273224") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("4375c33f-37b5-450b-b9c0-642d9c7515a0") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("48da37b6-0065-4ae9-ae7c-dbf4d00ee742") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("53d9a0b1-89f6-4587-9dc7-a6d36232e35c") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("5dc08428-481b-44f3-be90-cca3f1d5225d") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("6f101a49-324b-40b5-a7eb-7612dd104a4f") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("7604f0b7-5032-47ec-a246-eda6388047a7") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("8ae10070-462a-4545-8f91-57064334a5f4") },
                    { new Guid("efca208f-a077-42a8-ae00-2ddb74878a3d"), new Guid("97b3e779-2ad1-40a0-882f-e59d74f09243") },
                    { new Guid("efca208f-a077-42a8-ae00-2ddb74878a3d"), new Guid("9ce6fa87-a6b5-44f0-a5cc-16629f80a745") },
                    { new Guid("efca208f-a077-42a8-ae00-2ddb74878a3d"), new Guid("9ed5089c-9058-4779-a414-540e4ef15299") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("ae9c039e-cf83-4561-9d26-d27561f67c99") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("c67d9cbf-7f4e-4953-ab2d-bfb5c323b823") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("cbc386c4-c86b-4009-86dc-7ab16996bc0a") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("dac58c81-03f6-4eaa-8e22-0ed5c00e6b3e") },
                    { new Guid("efca208f-a077-42a8-ae00-2ddb74878a3d"), new Guid("e273428b-2ff3-4a73-9cf0-292699c5d3bc") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("ef846b45-24c4-45c5-b87b-d84a9958ed05") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("f0c2c53c-61d6-4ebc-944d-ee8706919bf6") },
                    { new Guid("f7e3193a-5710-447e-a5d8-a80b0464e77b"), new Guid("f66ec653-fcda-4994-a3ca-91c145065a0b") }
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
                name: "IX_Companies_SectorId",
                table: "Companies",
                column: "SectorId");

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
