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
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), "Z5YCW3E1WBHLI0E2JVY5OG1INWSVEHH6", "Company Administrator", "COMPANY ADMINITRATOR" },
                    { new Guid("c84fe7b8-0faf-4ce8-a9b1-bf79ca13f4ef"), "A1XK9GYZIZVYZ8J81PXKZMY66DER4QQ8", "Site Administrator", "SITE ADMINITRATOR" },
                    { new Guid("ef2589c9-5df1-4b63-9386-78fa77c6548e"), "AUUFU53FZEQCW484ZOILDZPKU3FX099B", "Personal", "PERSONAL" }
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("04d18c2d-3f35-4513-8137-c2f74dc559db"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3575), null, "Ahşap Teknolojisi", 1, null },
                    { new Guid("0d8d0c2c-3e5f-47ff-9643-4c6c75cf8842"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3686), null, "Turizm, Konaklama, Yiyecek-İçecek Hizmetleri", 1, null },
                    { new Guid("1599f4fe-c9a4-4d81-93b7-2d045f28ea4e"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3608), null, "Gıda", 1, null },
                    { new Guid("2cdf4eeb-b80c-452c-9ad1-49a5784d2b46"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3606), null, "Enerji", 1, null },
                    { new Guid("5363f863-ea59-428e-b7a7-2124b8021af7"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3613), null, "Kimya, Petrol, Lastik ve PLastik", 1, null },
                    { new Guid("548960be-90e6-46d5-a083-6c04a24adbe9"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3610), null, "İnşaat", 1, null },
                    { new Guid("5b8c24b3-b253-45e1-a2f9-7b935771b046"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3612), null, "İş ve Yönetimi", 1, null },
                    { new Guid("5ecf8def-3e51-47f8-81a1-4ddf83337776"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3620), null, "Metal", 1, null },
                    { new Guid("6e6dd653-37fd-4519-9134-edbb833af2a9"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3688), null, "Ulaştırma, Lojistik ve Haberleşme", 1, null },
                    { new Guid("79d32acb-bc39-45a9-a727-efc1c80a2984"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3630), null, "Ticaret (Satış ve Pazarlama)", 1, null },
                    { new Guid("7bedc93f-cc35-4712-b811-99e3e870d52b"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3631), null, "Toplumsal ve Kişisel Hizmetler", 1, null },
                    { new Guid("8e087fc6-334c-400b-b4ce-3f70bc3a247f"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3616), null, "Kültür, Sanat ve Tasarım", 1, null },
                    { new Guid("9623805e-fc71-4efe-b3fb-06442bdeffcd"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3589), null, "Bilişim", 1, null },
                    { new Guid("aed9d524-e583-43ee-bf61-6fc17c9d9066"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3618), null, "Maden", 1, null },
                    { new Guid("b39f528e-17e1-408a-bf6d-be8a2790fc64"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3622), null, "Sağlık ve Sosyal Hizmetler", 1, null },
                    { new Guid("b3c08117-9375-4646-acc9-ba4572a9d41f"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3601), null, "Eğitim", 1, null },
                    { new Guid("b4778589-33b8-405e-86b0-d154b6a593f3"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3619), null, "Medya, İletişim ve Yayıncılık", 1, null },
                    { new Guid("b807be61-0c4a-43d2-83b0-8b72e3960de7"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3607), null, "Finans", 1, null },
                    { new Guid("c32c886d-5e19-437d-bebd-6f2cac8a9c3d"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3629), null, "Tekstil, Hazır Giyim, Deri", 1, null },
                    { new Guid("cac2c0f3-85b3-4c29-8cee-1124f5c36cf0"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3602), null, "Elektrik ve Elektronik", 1, null },
                    { new Guid("cb0eccf8-63bb-43b0-84a4-67b11d308092"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3591), null, "Cam, Çimento ve ToprakÇevre", 1, null },
                    { new Guid("e52e613e-ad94-487c-be77-8b6f20d1aacb"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3626), null, "Tarım, Avcılık ve Balıkçılık", 1, null },
                    { new Guid("fa08ed8d-2a62-4123-bdd7-93418e67b79d"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3624), null, "Spor ve Rekreasyon", 1, null },
                    { new Guid("fa2b9e96-f0a2-4ce9-a9c0-7c95541db457"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3621), null, "Otomotiv", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "NumberOfEmployees", "PhoneNumber", "SectorId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("05b92701-8e5d-4656-8bae-ee69eb66f9ff"), new DateTime(2023, 5, 10, 10, 48, 12, 880, DateTimeKind.Local).AddTicks(4029), null, "info@sahinkomanditsirketi.com", "Şahin Kollektif Şirketi", 18, "+905154604279", new Guid("cac2c0f3-85b3-4c29-8cee-1124f5c36cf0"), 3, null },
                    { new Guid("0c43feb8-ea84-4393-9fe4-af14de8994fd"), new DateTime(2023, 5, 10, 10, 48, 12, 892, DateTimeKind.Local).AddTicks(572), null, "info@yildizanonimsirketi.com", "Yıldız Kollektif Şirketi", 22, "+905253607477", new Guid("5ecf8def-3e51-47f8-81a1-4ddf83337776"), 3, null },
                    { new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3807), null, "ikapp@ikapp.com", "IKApp A.Ş.", 5, "+905482221320", new Guid("9623805e-fc71-4efe-b3fb-06442bdeffcd"), 1, null },
                    { new Guid("241169a0-9d72-4629-92d6-173979a92656"), new DateTime(2023, 5, 10, 10, 48, 12, 851, DateTimeKind.Local).AddTicks(4534), null, "info@celikanonimsirketi.com", "Çelik Limited Şirketi", 69, "+905983146706", new Guid("b3c08117-9375-4646-acc9-ba4572a9d41f"), 3, null },
                    { new Guid("3420a046-4338-4b87-99a8-affbc8f4c27b"), new DateTime(2023, 5, 10, 10, 48, 12, 874, DateTimeKind.Local).AddTicks(5902), null, "info@sahinkomanditsirketi.com", "Şahin Anonim Şirketi", 60, "+905170723157", new Guid("e52e613e-ad94-487c-be77-8b6f20d1aacb"), 3, null },
                    { new Guid("535d41d9-5c74-42d8-a721-71dd0b377718"), new DateTime(2023, 5, 10, 10, 48, 12, 839, DateTimeKind.Local).AddTicks(8132), null, "info@demirlimitedsirketi.com", "Demir Komandit Şirketi", 59, "+905579360912", new Guid("548960be-90e6-46d5-a083-6c04a24adbe9"), 3, null },
                    { new Guid("7417da31-5571-410b-a1f0-ca47f74f5ebe"), new DateTime(2023, 5, 10, 10, 48, 12, 903, DateTimeKind.Local).AddTicks(9314), null, "info@yildizlimitedsirketi.com", "Yıldız Anonim Şirketi", 72, "+905679330176", new Guid("fa2b9e96-f0a2-4ce9-a9c0-7c95541db457"), 3, null },
                    { new Guid("82a85e13-7afc-4d2e-bee3-eabce739f7d3"), new DateTime(2023, 5, 10, 10, 48, 12, 845, DateTimeKind.Local).AddTicks(6272), null, "info@ozturkkooperatifsirketi.com", "Öztürk Kooperatif Şirketi", 91, "+905825968587", new Guid("8e087fc6-334c-400b-b4ce-3f70bc3a247f"), 3, null },
                    { new Guid("8c7efd2a-ff5f-4b58-9b03-3b3fc2f5bf30"), new DateTime(2023, 5, 10, 10, 48, 12, 822, DateTimeKind.Local).AddTicks(1010), null, "info@sahinanonimsirketi.com", "Şahin Kollektif Şirketi", 77, "+905484495581", new Guid("b3c08117-9375-4646-acc9-ba4572a9d41f"), 3, null },
                    { new Guid("8e937242-f573-4367-8fc3-51a93a05c9b0"), new DateTime(2023, 5, 10, 10, 48, 12, 833, DateTimeKind.Local).AddTicks(8552), null, "info@aydinanonimsirketi.com", "Aydın Anonim Şirketi", 42, "+905921181612", new Guid("fa2b9e96-f0a2-4ce9-a9c0-7c95541db457"), 3, null },
                    { new Guid("8eef585c-7eb8-4a1e-b322-a63945428234"), new DateTime(2023, 5, 10, 10, 48, 12, 898, DateTimeKind.Local).AddTicks(161), null, "info@yildirimkollektifsirketi.com", "Yıldırım Anonim Şirketi", 3, "+905276432923", new Guid("9623805e-fc71-4efe-b3fb-06442bdeffcd"), 3, null },
                    { new Guid("940927fb-be4f-4db9-af96-c8eabeaebece"), new DateTime(2023, 5, 10, 10, 48, 12, 868, DateTimeKind.Local).AddTicks(8865), null, "info@yilmazkooperatifsirketi.com", "Yılmaz Kooperatif Şirketi", 35, "+905583690914", new Guid("6e6dd653-37fd-4519-9134-edbb833af2a9"), 3, null },
                    { new Guid("a74a6c66-878c-4d52-afea-98f39bb35322"), new DateTime(2023, 5, 10, 10, 48, 12, 827, DateTimeKind.Local).AddTicks(9579), null, "info@yildirimkooperatifsirketi.com", "Yıldırım Kollektif Şirketi", 95, "+905455145949", new Guid("1599f4fe-c9a4-4d81-93b7-2d045f28ea4e"), 3, null },
                    { new Guid("b6bd551b-7dce-4d3f-8ec4-5b548495313e"), new DateTime(2023, 5, 10, 10, 48, 12, 863, DateTimeKind.Local).AddTicks(510), null, "info@sahinkomanditsirketi.com", "Şahin Kollektif Şirketi", 12, "+905668545854", new Guid("fa08ed8d-2a62-4123-bdd7-93418e67b79d"), 3, null },
                    { new Guid("b8e3345b-0f0e-417e-a9ec-308d15423984"), new DateTime(2023, 5, 10, 10, 48, 12, 886, DateTimeKind.Local).AddTicks(2613), null, "info@ozturkanonimsirketi.com", "Öztürk Limited Şirketi", 46, "+905689471587", new Guid("cb0eccf8-63bb-43b0-84a4-67b11d308092"), 3, null },
                    { new Guid("e57f5738-24cd-4c46-ac59-1f4182313c3e"), new DateTime(2023, 5, 10, 10, 48, 12, 857, DateTimeKind.Local).AddTicks(1859), null, "info@aydinkollektifsirketi.com", "Aydın Limited Şirketi", 64, "+905137966452", new Guid("5ecf8def-3e51-47f8-81a1-4ddf83337776"), 3, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("030b8def-f2bb-4ea5-950e-f0599b81af48"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3949), null, "Construction Foreman", 1, null },
                    { new Guid("03addab9-b0f8-4901-bd2d-9d97df0ab1b7"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3826), null, "Sales Associate", 1, null },
                    { new Guid("09d2c1c5-8cea-47d6-8422-3eba2c1b0247"), new Guid("82a85e13-7afc-4d2e-bee3-eabce739f7d3"), new DateTime(2023, 5, 10, 10, 48, 12, 845, DateTimeKind.Local).AddTicks(6276), null, "Regional Sales Manager", 1, null },
                    { new Guid("0bff4efa-c6aa-4fe6-a9cf-b42eeeaf213e"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3951), null, "Project Manager", 1, null },
                    { new Guid("1040a0f1-4903-48a4-a44d-134ee6805556"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3871), null, "Pharmacy Technician", 1, null },
                    { new Guid("195189e0-3252-4f56-a1a7-bb6acfcbe23d"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3820), null, "VP of Sales", 1, null },
                    { new Guid("1ac05bed-a6e5-4540-8f3f-3a9f0e3ecaed"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3947), null, "Server/Host/Hostess", 1, null },
                    { new Guid("1d39842b-e647-4ab2-b1c0-dcedf6744115"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3944), null, "Concierge", 1, null },
                    { new Guid("20d9847f-9b29-4c36-8cd7-d34c47a0ec00"), new Guid("8eef585c-7eb8-4a1e-b322-a63945428234"), new DateTime(2023, 5, 10, 10, 48, 12, 898, DateTimeKind.Local).AddTicks(167), null, "Teaching Assistant", 1, null },
                    { new Guid("230f5951-023b-4d61-9de3-d8a878b8e1ed"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3824), null, "Regional Sales Manager", 1, null },
                    { new Guid("23d1cbad-26ab-421c-8cba-8b948a7c29b9"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3855), null, "Director of Business Operations", 1, null },
                    { new Guid("29934169-07bd-4a7c-a033-80ad7b4fbeec"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3956), null, "Project Appraisal Engineer", 1, null },
                    { new Guid("30d61fb4-74d9-4b77-a58b-f01f6bd496b7"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3957), null, "Inspector", 1, null },
                    { new Guid("36239132-4ead-4cbb-97ec-4d8c4722bc1a"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3844), null, "Risk Analyst", 1, null },
                    { new Guid("3841b49d-6272-4836-8801-4c0162106c04"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3864), null, "Systems Administrator", 1, null },
                    { new Guid("3a5f0361-c53c-46de-9555-b5d720474865"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3868), null, "Other Industries:", 1, null },
                    { new Guid("3c5a8542-7d90-47ad-b85f-88e525dcb2ee"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3876), null, "Occupational Therapy Aide", 1, null },
                    { new Guid("43cdfa1c-2e0d-4cde-9d89-d5a79d211866"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3860), null, "Director of Information Security", 1, null },
                    { new Guid("4447169c-6914-4a52-a633-831901ac2dac"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3833), null, "Sr. Marketing Manager", 1, null },
                    { new Guid("44f87a15-4af0-4c2a-8e60-1b958223041c"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3836), null, "Marketing Coordinator", 1, null },
                    { new Guid("50295a2f-46f3-4c7c-8898-2993d205bea6"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3869), null, "Registered Nurse", 1, null },
                    { new Guid("50629c42-ba6f-4ba0-8f38-3ec2f8df882a"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3867), null, "Data Analyst", 1, null },
                    { new Guid("591281d2-3587-41e4-8f6a-6607fc1892d0"), new Guid("b8e3345b-0f0e-417e-a9ec-308d15423984"), new DateTime(2023, 5, 10, 10, 48, 12, 886, DateTimeKind.Local).AddTicks(2621), null, "Data Analyst", 1, null },
                    { new Guid("5acb5aa1-c616-48ee-abe3-eb265eb92b91"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3838), null, "VP of Finance", 1, null },
                    { new Guid("5d7924d9-7db4-494f-839d-19ac45cf94f0"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3845), null, "VP of Client Services", 1, null },
                    { new Guid("5f9b2aed-1dd1-41d8-8969-467230e9f575"), new Guid("e57f5738-24cd-4c46-ac59-1f4182313c3e"), new DateTime(2023, 5, 10, 10, 48, 12, 857, DateTimeKind.Local).AddTicks(1864), null, "Inspector", 1, null },
                    { new Guid("62fb92fc-f66d-4e5c-a5d1-188425cef2d6"), new Guid("8e937242-f573-4367-8fc3-51a93a05c9b0"), new DateTime(2023, 5, 10, 10, 48, 12, 833, DateTimeKind.Local).AddTicks(8558), null, "Hotel Receptionist", 1, null },
                    { new Guid("6369fd79-7ae2-4c0b-b7ee-1d355552c918"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3882), null, "Registrar", 1, null },
                    { new Guid("6753701e-9096-4dce-adf7-3130d37353b3"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3832), null, "Marketing Director", 1, null },
                    { new Guid("75793833-0638-455d-a2fe-b87ae28e1e41"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3858), null, "Sr. Manager of HR", 1, null },
                    { new Guid("7962f1c9-ea52-4314-9faf-6ecc2a904766"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3946), null, "Front Desk Associate", 1, null },
                    { new Guid("7ef4e021-d51a-4a54-8150-882d663d9dd5"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3884), null, "Teacher", 1, null },
                    { new Guid("81fb693e-f74a-426a-8ac2-cd6bd277840c"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3825), null, "Sales Representative", 1, null },
                    { new Guid("842f5c23-7f58-4c2b-87e7-be72032bb074"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3950), null, "Safety Director", 1, null },
                    { new Guid("843f0e75-521a-47db-9235-b73d59e1ca59"), new Guid("7417da31-5571-410b-a1f0-ca47f74f5ebe"), new DateTime(2023, 5, 10, 10, 48, 12, 903, DateTimeKind.Local).AddTicks(9319), null, "Sales Associate", 1, null },
                    { new Guid("877b0eab-9803-4dbd-85d1-762394a65f1a"), new Guid("05b92701-8e5d-4656-8bae-ee69eb66f9ff"), new DateTime(2023, 5, 10, 10, 48, 12, 880, DateTimeKind.Local).AddTicks(4032), null, "Customer Service Representative", 1, null },
                    { new Guid("87f7d925-19c9-4bb2-a50c-16b671311839"), new Guid("0c43feb8-ea84-4393-9fe4-af14de8994fd"), new DateTime(2023, 5, 10, 10, 48, 12, 892, DateTimeKind.Local).AddTicks(576), null, "Sr. Manager of HR", 1, null },
                    { new Guid("88f1a485-da27-4335-b3d8-b05658a1090a"), new Guid("3420a046-4338-4b87-99a8-affbc8f4c27b"), new DateTime(2023, 5, 10, 10, 48, 12, 874, DateTimeKind.Local).AddTicks(5906), null, "Data Analyst", 1, null },
                    { new Guid("8b02b62c-d8a1-4acf-b67d-e167a8a7cfa3"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3839), null, "Procurement Director", 1, null },
                    { new Guid("8fe8d552-bd28-4f89-a815-6513d629569f"), new Guid("940927fb-be4f-4db9-af96-c8eabeaebece"), new DateTime(2023, 5, 10, 10, 48, 12, 868, DateTimeKind.Local).AddTicks(8870), null, "CMO (Chief Marketing Officer)", 1, null },
                    { new Guid("9cacf8d9-3e9e-4c18-92d5-a0e6996977a6"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3875), null, "Clinical Laboratory Technician", 1, null },
                    { new Guid("a1ee13c5-0f52-4842-84fa-3cf7701f9642"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3831), null, "CMO (Chief Marketing Officer)", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("a5f82483-c9db-4b4c-a48b-36d4e54c00eb"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3835), null, "Marketing Analyst", 1, null },
                    { new Guid("a7fe6901-4eff-49a8-9c2b-008d51c850b7"), new Guid("b6bd551b-7dce-4d3f-8ec4-5b548495313e"), new DateTime(2023, 5, 10, 10, 48, 12, 863, DateTimeKind.Local).AddTicks(520), null, "Director of Business Operations", 1, null },
                    { new Guid("a956debf-139d-48e1-940b-fee3a44e0f69"), new Guid("8c7efd2a-ff5f-4b58-9b03-3b3fc2f5bf30"), new DateTime(2023, 5, 10, 10, 48, 12, 822, DateTimeKind.Local).AddTicks(1033), null, "Teaching Assistant", 1, null },
                    { new Guid("acbe3865-c0df-4850-b604-3d4f77dd37ec"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3881), null, "Principal", 1, null },
                    { new Guid("b4a59c5b-f013-485d-bfb6-5cc7d5c2b7f0"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3879), null, "Administrator", 1, null },
                    { new Guid("b9c52638-4db4-43de-acd7-a934f313e330"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3847), null, "Account Manager", 1, null },
                    { new Guid("bb2cafa2-0738-4674-90bb-21647448fb59"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3850), null, "Customer Service Representative", 1, null },
                    { new Guid("bd446787-d011-481d-953a-d0653d140a95"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3873), null, "Physical Therapist", 1, null },
                    { new Guid("c26df384-7929-4b9d-9a1a-48e5c1762a00"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3863), null, "Full Stack Developer", 1, null },
                    { new Guid("c3f201c9-2e96-48ed-90e4-cc7b82adfbba"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3940), null, "General Manager", 1, null },
                    { new Guid("c87e59f5-7513-4f2a-815f-faeb3b152f3f"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3861), null, "Software Engineer I, II, III", 1, null },
                    { new Guid("cd22a1ea-2f04-463b-b75d-f0f2644693d1"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3843), null, "Credit Analyst", 1, null },
                    { new Guid("ce984ee3-0f82-47ef-ad35-2c34fe455ccb"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3948), null, "Hotel Receptionist", 1, null },
                    { new Guid("cee81027-7a80-4dea-9b0a-22f6fab083e6"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3840), null, "Investment Analyst", 1, null },
                    { new Guid("d262f3ab-5d5a-4cb6-809b-2048609d871b"), new Guid("a74a6c66-878c-4d52-afea-98f39bb35322"), new DateTime(2023, 5, 10, 10, 48, 12, 827, DateTimeKind.Local).AddTicks(9583), null, "School Counselor", 1, null },
                    { new Guid("d35867e4-93d4-408e-8abe-badc517dd097"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3859), null, "HR Analyst", 1, null },
                    { new Guid("d5d02c10-e576-4aa1-a4a4-8e31ea742a79"), new Guid("241169a0-9d72-4629-92d6-173979a92656"), new DateTime(2023, 5, 10, 10, 48, 12, 851, DateTimeKind.Local).AddTicks(4539), null, "Director of Business Operations", 1, null },
                    { new Guid("d6f05981-2eb7-41b0-a313-eeadd02136fb"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3848), null, "Customer Success Manager", 1, null },
                    { new Guid("df585796-32b9-4d84-b2fd-bb97be5c1742"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3874), null, "Nursing Assistant", 1, null },
                    { new Guid("dfce2dcc-b139-4fe8-a331-c914d3b5e9fb"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3953), null, "Contract Administrator", 1, null },
                    { new Guid("e1e884e6-5dec-477b-b1d6-f311dbf59025"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3857), null, "Operations Supervisor", 1, null },
                    { new Guid("e3bc519a-75be-43ea-b96f-3df19078f554"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3941), null, "Guest Services Supervisor", 1, null },
                    { new Guid("e7bcea7c-ba1c-4a10-af3a-c8bea1d8653c"), new Guid("535d41d9-5c74-42d8-a721-71dd0b377718"), new DateTime(2023, 5, 10, 10, 48, 12, 839, DateTimeKind.Local).AddTicks(8140), null, "Teacher", 1, null },
                    { new Guid("e93f293d-1a45-411f-9e1b-f595af58d781"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3851), null, "Support Specialist", 1, null },
                    { new Guid("ee0f4aa7-e0cb-4827-9a4b-b38223e8d29d"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3852), null, "COO (Chief Operating Officer)", 1, null },
                    { new Guid("f6c6b4d5-a8a5-40ef-a765-7f7bd999c22f"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3938), null, "Teaching Assistant", 1, null },
                    { new Guid("fbbd4fd4-856b-43ec-88d0-a4c2bfe84e19"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3822), null, "National Sales Director", 1, null },
                    { new Guid("ff1d42bf-2d8b-407a-af23-0e127816574d"), new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(3883), null, "School Counselor", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "BloodGroup", "CompanyId", "ConcurrencyStamp", "CreateDate", "DeleteDate", "Email", "EmailConfirmed", "IdentityNumber", "ImagePath", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profession", "SecondName", "SecurityStamp", "Status", "Surname", "TitleId", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("0477ccd5-bff4-4b0c-a0c0-9be8b79caa95"), 0, new DateTime(2049, 1, 13, 10, 48, 12, 816, DateTimeKind.Local).AddTicks(91), null, new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), "b1169301-2fd0-4223-b02e-6449914f556a", new DateTime(2023, 5, 10, 10, 48, 12, 816, DateTimeKind.Local).AddTicks(89), null, "yusuf.demir@outlook.com", false, "5614813716", "/images/UserPhotos/defaultuser.jpg", false, null, "Yusuf", "YUSUF.DEMIR@OUTLOOK.COM", "YUSUF.DEMIR@OUTLOOK.COM", "AQAAAAEAACcQAAAAEGGjyvGBPUyGuS0CyoY9E4FErCfxO2WQUm/wfwAh56UjyP/cvcGMWXOyov7sZXSkNA==", "+905245023860", false, "Artist", null, "HI0DIWBWI65IKL8HVDQXC01ANX0338GS", 1, "Demir", new Guid("7ef4e021-d51a-4a54-8150-882d663d9dd5"), false, null, "yusuf.demir@outlook.com" },
                    { new Guid("0aa6b735-83a3-46d6-826e-96589ce07fd4"), 0, new DateTime(2052, 9, 18, 10, 48, 12, 798, DateTimeKind.Local).AddTicks(2623), null, new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), "354046e3-deff-44a1-b864-623478d0b048", new DateTime(2023, 5, 10, 10, 48, 12, 798, DateTimeKind.Local).AddTicks(2621), null, "yusuf.yildiz@yandex.com", false, "7625131768", "/images/UserPhotos/defaultuser.jpg", false, null, "Yusuf", "YUSUF.YILDIZ@YANDEX.COM", "YUSUF.YILDIZ@YANDEX.COM", "AQAAAAEAACcQAAAAENfE4LVheJwVoGF0aFo5Aak/hS/yTd7xKtJmMOCwRTjIOcMNvIMRuIkJbTFOFXV4Hw==", "+905441578594", false, "Dancer", null, "2IBDCKXK3B1DGIDWPVEN0WG0RKRHS3FM", 1, "Yıldız", new Guid("bd446787-d011-481d-953a-d0653d140a95"), false, null, "yusuf.yildiz@yandex.com" },
                    { new Guid("39d2c68b-7211-4587-b27d-5205964ed156"), 0, new DateTime(2050, 2, 28, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(4011), null, new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), "0b85874d-16ad-4930-814c-599cce305cd2", new DateTime(2023, 5, 10, 10, 48, 12, 792, DateTimeKind.Local).AddTicks(4009), null, "mustafa.sahin@google.com", false, "1080263482", "/images/UserPhotos/defaultuser.jpg", false, null, "Mustafa", "MUSTAFA.SAHIN@GOOGLE.COM", "MUSTAFA.SAHIN@GOOGLE.COM", "AQAAAAEAACcQAAAAEEV/ZgXntycdkwju8U8JeDB5xQ8svHKOkQH+6AjoVFPROkkspXGVTfHepCsfQEOYXA==", "+905259060873", false, "Bricklayer", null, "IZN4IRGBTOX3PHUQLMABMJHOMUE8NW6M", 1, "Şahin", new Guid("a5f82483-c9db-4b4c-a48b-36d4e54c00eb"), false, null, "mustafa.sahin@google.com" },
                    { new Guid("3d10c1ab-e59b-44d6-9a98-2781db1d42de"), 0, new DateTime(2076, 2, 1, 10, 48, 12, 892, DateTimeKind.Local).AddTicks(581), null, new Guid("0c43feb8-ea84-4393-9fe4-af14de8994fd"), "a66388af-1668-41d0-883e-a86591d87d74", new DateTime(2023, 5, 10, 10, 48, 12, 892, DateTimeKind.Local).AddTicks(580), null, "mustafa.yildiz@yandex.com", false, "1802245754", "/images/UserPhotos/defaultuser.jpg", false, null, "Mustafa", "MUSTAFA.YILDIZ@YANDEX.COM", "MUSTAFA.YILDIZ@YANDEX.COM", "AQAAAAEAACcQAAAAEEG5caYAr6TfSlFG7cs02SjxTMZJRWexbo6qAFJbgh2cKhHDa3SDE40/W9KbJNb5yg==", "+905214005410", false, "Dancer", null, "J4XZF0WWHW7B80Y352W43QSFDP2KEA7G", 3, "Yıldız", new Guid("87f7d925-19c9-4bb2-a50c-16b671311839"), false, null, "mustafa.yildiz@yandex.com" },
                    { new Guid("4aec7e0d-be9c-41b2-b1ff-f04088c8ecb7"), 0, new DateTime(2058, 5, 2, 10, 48, 12, 851, DateTimeKind.Local).AddTicks(4547), null, new Guid("241169a0-9d72-4629-92d6-173979a92656"), "53d17c06-2ec8-4fcd-b5a6-979837bd48fa", new DateTime(2023, 5, 10, 10, 48, 12, 851, DateTimeKind.Local).AddTicks(4546), null, "yusuf.celik@yahoo.com", false, "5007018304", "/images/UserPhotos/defaultuser.jpg", false, null, "Yusuf", "YUSUF.CELIK@YAHOO.COM", "YUSUF.CELIK@YAHOO.COM", "AQAAAAEAACcQAAAAEGr82zXAlbCWLilyQPUB2rpZ84FaXncS9eJ6Zz8mO25dXTjFW47hlKqxIATOtt/eFg==", "+905319211514", false, "Librarian", null, "YD3D3B3JS0Y5PRXWY996M2NNEP8M7CPC", 3, "Çelik", new Guid("d5d02c10-e576-4aa1-a4a4-8e31ea742a79"), false, null, "yusuf.celik@yahoo.com" },
                    { new Guid("56fa3184-be5c-4fdf-bfd3-b30297017428"), 0, new DateTime(2069, 3, 19, 10, 48, 12, 857, DateTimeKind.Local).AddTicks(1869), null, new Guid("e57f5738-24cd-4c46-ac59-1f4182313c3e"), "bc28ecb3-3b72-4b80-8323-a762650b7002", new DateTime(2023, 5, 10, 10, 48, 12, 857, DateTimeKind.Local).AddTicks(1868), null, "ibrahim.aydin@google.com", false, "2503312646", "/images/UserPhotos/defaultuser.jpg", false, null, "İbrahim", "IBRAHIM.AYDIN@GOOGLE.COM", "IBRAHIM.AYDIN@GOOGLE.COM", "AQAAAAEAACcQAAAAEOCG5qMLZFtbQ4FeZ/rcD8dNwiEq6F5rSbAoveiFXSmPeX17UYPzBIg/x964W5nn2g==", "+905315079973", false, "Veterinary doctor(Vet)", null, "DEF0YLG7LDHHOKB4YTJOD7Y5RPBTVQKZ", 3, "Aydın", new Guid("5f9b2aed-1dd1-41d8-8969-467230e9f575"), false, null, "ibrahim.aydin@google.com" },
                    { new Guid("6383f107-1df3-40ad-afb0-b23b43afa722"), 0, new DateTime(2042, 6, 7, 10, 48, 12, 804, DateTimeKind.Local).AddTicks(2382), null, new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), "6c0618fc-264e-4cef-92bf-b1ace4b77399", new DateTime(2023, 5, 10, 10, 48, 12, 804, DateTimeKind.Local).AddTicks(2377), null, "osman.celik@outlook.com", false, "3748037192", "/images/UserPhotos/defaultuser.jpg", false, null, "Osman", "OSMAN.CELIK@OUTLOOK.COM", "OSMAN.CELIK@OUTLOOK.COM", "AQAAAAEAACcQAAAAENjBArhPPzzTQkQ09B4DvPtEQxWkUlWzJ2rggQt7eqRmb+QGys4Gh1G0KEAwy8o1nQ==", "+905990283662", false, "Fisherman", null, "T0SN0OC70JJ104ZE9FPLIEPX5KCPC89G", 1, "Çelik", new Guid("a5f82483-c9db-4b4c-a48b-36d4e54c00eb"), false, null, "osman.celik@outlook.com" },
                    { new Guid("7de6cd16-11fa-42b1-b8c0-58d20b689816"), 0, new DateTime(2046, 8, 19, 10, 48, 12, 880, DateTimeKind.Local).AddTicks(4037), null, new Guid("05b92701-8e5d-4656-8bae-ee69eb66f9ff"), "a5e55e97-1317-443c-b1db-7d4c90f5e829", new DateTime(2023, 5, 10, 10, 48, 12, 880, DateTimeKind.Local).AddTicks(4036), null, "osman.sahin@outlook.com", false, "3141224894", "/images/UserPhotos/defaultuser.jpg", false, null, "Osman", "OSMAN.SAHIN@OUTLOOK.COM", "OSMAN.SAHIN@OUTLOOK.COM", "AQAAAAEAACcQAAAAEHrOwrwyTi9OwtGY+UQA/0XN7+QlpocY+Xkr+ED8zPv3O4g1OW/Xk0u3u2jw7mb2Ow==", "+905728110165", false, "Photographer", null, "99GK5TWPLMRG9346KJ886AAHZNRD662C", 3, "Şahin", new Guid("877b0eab-9803-4dbd-85d1-762394a65f1a"), false, null, "osman.sahin@outlook.com" },
                    { new Guid("89cec818-f48b-40ed-9dd5-b6506c20951a"), 0, new DateTime(2064, 12, 6, 10, 48, 12, 833, DateTimeKind.Local).AddTicks(8565), null, new Guid("8e937242-f573-4367-8fc3-51a93a05c9b0"), "06296c3d-c442-46a1-b6b8-32918de3890a", new DateTime(2023, 5, 10, 10, 48, 12, 833, DateTimeKind.Local).AddTicks(8564), null, "ahmet.aydin@hotmail.com", false, "3263527378", "/images/UserPhotos/defaultuser.jpg", false, null, "Ahmet", "AHMET.AYDIN@HOTMAIL.COM", "AHMET.AYDIN@HOTMAIL.COM", "AQAAAAEAACcQAAAAEL0mOXJ4GxYgbEhuD7jj5QO2UB3vUpvuz9LI28cm50HqjvunDj9rCKb7zkks5LJI1A==", "+905432699784", false, "Plumber", null, "0656G4PZSRGA62SEEEJPOUNCT4QBNBPC", 3, "Aydın", new Guid("62fb92fc-f66d-4e5c-a5d1-188425cef2d6"), false, null, "ahmet.aydin@hotmail.com" },
                    { new Guid("8e153450-b8b4-45d8-a0d6-aa4a5d7bc169"), 0, new DateTime(2074, 12, 22, 10, 48, 12, 822, DateTimeKind.Local).AddTicks(1046), null, new Guid("8c7efd2a-ff5f-4b58-9b03-3b3fc2f5bf30"), "2b042afb-64c7-450b-8d43-88c3e774e9b3", new DateTime(2023, 5, 10, 10, 48, 12, 822, DateTimeKind.Local).AddTicks(1043), null, "ibrahim.sahin@yahoo.com", false, "2832441790", "/images/UserPhotos/defaultuser.jpg", false, null, "İbrahim", "IBRAHIM.SAHIN@YAHOO.COM", "IBRAHIM.SAHIN@YAHOO.COM", "AQAAAAEAACcQAAAAEHivRTAkfoG83P87NsgVaQXMgukpdZ3n8BvSfDi4B35pcaqJSzmMAJ0kOuxEYvu4cA==", "+905856678124", false, "Accountant", null, "PBNWGY09LSW7Z1TZ8UCL4WLRKA9UQUTJ", 3, "Şahin", new Guid("a956debf-139d-48e1-940b-fee3a44e0f69"), false, null, "ibrahim.sahin@yahoo.com" },
                    { new Guid("a49390ba-5b8b-445b-bbc4-a76f5e69bb3f"), 0, new DateTime(2066, 7, 27, 10, 48, 12, 839, DateTimeKind.Local).AddTicks(8146), null, new Guid("535d41d9-5c74-42d8-a721-71dd0b377718"), "af0c7736-2398-48ec-b292-b9e4112618b6", new DateTime(2023, 5, 10, 10, 48, 12, 839, DateTimeKind.Local).AddTicks(8144), null, "yusuf.demir@yahoo.com", false, "1615658428", "/images/UserPhotos/defaultuser.jpg", false, null, "Yusuf", "YUSUF.DEMIR@YAHOO.COM", "YUSUF.DEMIR@YAHOO.COM", "AQAAAAEAACcQAAAAEHUFdSpXFNz7iY8oBlGGJFIscmb10Y281c08++x6KKrlI8y4/JEwCgODWkvGZGwQww==", "+905269164157", false, "Lawyer", null, "1PXSWI290CJMCAWG2UK3X13J91EJU5C0", 3, "Demir", new Guid("e7bcea7c-ba1c-4a10-af3a-c8bea1d8653c"), false, null, "yusuf.demir@yahoo.com" },
                    { new Guid("c641568a-f7d3-41f1-a16f-be578fe369f2"), 0, new DateTime(2071, 4, 14, 10, 48, 12, 863, DateTimeKind.Local).AddTicks(525), null, new Guid("b6bd551b-7dce-4d3f-8ec4-5b548495313e"), "072e46bb-45ae-47d6-9158-338e10618695", new DateTime(2023, 5, 10, 10, 48, 12, 863, DateTimeKind.Local).AddTicks(524), null, "mustafa.sahin@microsoft.com", false, "2104068810", "/images/UserPhotos/defaultuser.jpg", false, null, "Mustafa", "MUSTAFA.SAHIN@MICROSOFT.COM", "MUSTAFA.SAHIN@MICROSOFT.COM", "AQAAAAEAACcQAAAAED3+XXhVDfqpqnqPLUWgswMV6Nznw986FjqpHVSoXZTOPkRs9Pw2jwRlBuFXMtUwTg==", "+905579126356", false, "Librarian", null, "7547PV5GE4VCYFXPPTCWBT31RK7IB3W0", 3, "Şahin", new Guid("a7fe6901-4eff-49a8-9c2b-008d51c850b7"), false, null, "mustafa.sahin@microsoft.com" },
                    { new Guid("d18706c1-c134-4f78-83e1-7cf8d0b09a21"), 0, new DateTime(2054, 10, 5, 10, 48, 12, 903, DateTimeKind.Local).AddTicks(9324), null, new Guid("7417da31-5571-410b-a1f0-ca47f74f5ebe"), "5df8631d-aa56-4aae-8c8e-1fb3eb323ba3", new DateTime(2023, 5, 10, 10, 48, 12, 903, DateTimeKind.Local).AddTicks(9323), null, "ismail.yildiz@yahoo.com", false, "4840530734", "/images/UserPhotos/defaultuser.jpg", false, null, "İsmail", "ISMAIL.YILDIZ@YAHOO.COM", "ISMAIL.YILDIZ@YAHOO.COM", "AQAAAAEAACcQAAAAEMFk64AB8zDywAMcUlsBtvRaUKgJZI86MqmbQMLyMtakwA7uguURk4fX6/cmigloMQ==", "+905254818378", false, "Hairdresser", null, "F38FMN2VTIXSUD0MWYMFVDK6DSG7BGFW", 3, "Yıldız", new Guid("843f0e75-521a-47db-9235-b73d59e1ca59"), false, null, "ismail.yildiz@yahoo.com" },
                    { new Guid("d486ecfb-b7b5-4742-9f40-443477556153"), 0, new DateTime(2058, 7, 31, 10, 48, 12, 898, DateTimeKind.Local).AddTicks(178), null, new Guid("8eef585c-7eb8-4a1e-b322-a63945428234"), "fd8c1bc7-a0da-43bd-8202-75f411d31218", new DateTime(2023, 5, 10, 10, 48, 12, 898, DateTimeKind.Local).AddTicks(176), null, "huseyin.yildirim@microsoft.com", false, "4627017434", "/images/UserPhotos/defaultuser.jpg", false, null, "Hüseyin", "HUSEYIN.YILDIRIM@MICROSOFT.COM", "HUSEYIN.YILDIRIM@MICROSOFT.COM", "AQAAAAEAACcQAAAAEG5wxl8xfAivu+zizq7JyuWUNhaUO92dFxvv68DH4Dfo3Afyu4/isTgmZgo3WCgqww==", "+905832029653", false, "Taxi driver", null, "EH987WPVL7DSNN5PIPS7YKSPGWYOS63J", 3, "Yıldırım", new Guid("20d9847f-9b29-4c36-8cd7-d34c47a0ec00"), false, null, "huseyin.yildirim@microsoft.com" },
                    { new Guid("e94333f7-fc57-405e-ae7e-b8beb32d8992"), 0, new DateTime(2056, 4, 1, 10, 48, 12, 845, DateTimeKind.Local).AddTicks(6280), null, new Guid("82a85e13-7afc-4d2e-bee3-eabce739f7d3"), "fc335012-6ab0-45a1-ab03-03ed650dce04", new DateTime(2023, 5, 10, 10, 48, 12, 845, DateTimeKind.Local).AddTicks(6279), null, "hasan.ozturk@outlook.com", false, "6823637218", "/images/UserPhotos/defaultuser.jpg", false, null, "Hasan", "HASAN.OZTURK@OUTLOOK.COM", "HASAN.OZTURK@OUTLOOK.COM", "AQAAAAEAACcQAAAAEPsp77nXXWqJD4UfBDhZ75Xm6zIwVIOowDpGmHhh85agWFFx3PQRzCZL73GqhDPDzQ==", "+905488966545", false, "Policeman/Policewoman", null, "NACYB3DUBAF37S97MQGNSGUK9WA0VLU4", 3, "Öztürk", new Guid("09d2c1c5-8cea-47d6-8422-3eba2c1b0247"), false, null, "hasan.ozturk@outlook.com" },
                    { new Guid("ea5b1748-620b-49c9-988d-b4eec19b67b6"), 0, new DateTime(2062, 11, 7, 10, 48, 12, 810, DateTimeKind.Local).AddTicks(1998), null, new Guid("1e9bf361-dab5-4de0-91b5-3e3c303b250a"), "ba38b27b-95b6-48e7-9eb7-1b6287ee8082", new DateTime(2023, 5, 10, 10, 48, 12, 810, DateTimeKind.Local).AddTicks(1996), null, "ibrahim.celik@yahoo.com", false, "8168011050", "/images/UserPhotos/defaultuser.jpg", false, null, "İbrahim", "IBRAHIM.CELIK@YAHOO.COM", "IBRAHIM.CELIK@YAHOO.COM", "AQAAAAEAACcQAAAAEAJgrl4wguVdPciq2NBB3N5MnVBeC/AiJgpP+Ui1ybrunFL716fGckTXxqP9SOvBCw==", "+905612483636", false, "Farmer", null, "RQQS2PL3GXYWES2BUBSVI6ETO7NUXTK3", 1, "Çelik", new Guid("e93f293d-1a45-411f-9e1b-f595af58d781"), false, null, "ibrahim.celik@yahoo.com" },
                    { new Guid("ee703a77-7c16-48c6-bc2f-db1a788393cb"), 0, new DateTime(2058, 10, 14, 10, 48, 12, 874, DateTimeKind.Local).AddTicks(5915), null, new Guid("3420a046-4338-4b87-99a8-affbc8f4c27b"), "f39d7975-211b-4c9e-add6-4a689fc1d809", new DateTime(2023, 5, 10, 10, 48, 12, 874, DateTimeKind.Local).AddTicks(5914), null, "yusuf.sahin@outlook.com", false, "2687243530", "/images/UserPhotos/defaultuser.jpg", false, null, "Yusuf", "YUSUF.SAHIN@OUTLOOK.COM", "YUSUF.SAHIN@OUTLOOK.COM", "AQAAAAEAACcQAAAAEJdXJHVeNUvosjslJR47tW6nkAEpU5WXA7AZjKkjpUZM3cuo/kMfkG5srIA/FkZBDg==", "+905698786626", false, "Engineer", null, "764J1URZZS2FV7RB38PBTRPDGQH586NG", 3, "Şahin", new Guid("88f1a485-da27-4335-b3d8-b05658a1090a"), false, null, "yusuf.sahin@outlook.com" },
                    { new Guid("ee79a705-6435-4e31-8dfa-9449049a3f92"), 0, new DateTime(2061, 2, 15, 10, 48, 12, 886, DateTimeKind.Local).AddTicks(2630), null, new Guid("b8e3345b-0f0e-417e-a9ec-308d15423984"), "3ecfb0af-96ce-499a-9cdf-8e32537220d5", new DateTime(2023, 5, 10, 10, 48, 12, 886, DateTimeKind.Local).AddTicks(2628), null, "osman.ozturk@google.com", false, "6102087644", "/images/UserPhotos/defaultuser.jpg", false, null, "Osman", "OSMAN.OZTURK@GOOGLE.COM", "OSMAN.OZTURK@GOOGLE.COM", "AQAAAAEAACcQAAAAEHlmn4ldM8/cYym7dzkDjdX5vo/wjfNZ9XbeB1plXw+7I7Yucp6lcutESambkbTQcQ==", "+905120423978", false, "Factory worker", null, "TKK7VLKW3HTD5KEOSQN8F64LV51B1MSP", 3, "Öztürk", new Guid("591281d2-3587-41e4-8f6a-6607fc1892d0"), false, null, "osman.ozturk@google.com" },
                    { new Guid("f6987ece-f047-4c92-8d99-aafc6497e6f3"), 0, new DateTime(2061, 7, 15, 10, 48, 12, 827, DateTimeKind.Local).AddTicks(9591), null, new Guid("a74a6c66-878c-4d52-afea-98f39bb35322"), "54928b34-9d39-4a96-bd92-d1d074f7a70e", new DateTime(2023, 5, 10, 10, 48, 12, 827, DateTimeKind.Local).AddTicks(9590), null, "osman.yildirim@yahoo.com", false, "3370487248", "/images/UserPhotos/defaultuser.jpg", false, null, "Osman", "OSMAN.YILDIRIM@YAHOO.COM", "OSMAN.YILDIRIM@YAHOO.COM", "AQAAAAEAACcQAAAAEFQ85o/6DgUNcXeR5m7I/TsbQcFamH8z75IBGxzSDO/J4quSsIPXUXwpD3TTHFOL+Q==", "+905685069241", false, "Painter", null, "LZE36HYIC3W3T1KYZ97U578346P5YDMW", 3, "Yıldırım", new Guid("d262f3ab-5d5a-4cb6-809b-2048609d871b"), false, null, "osman.yildirim@yahoo.com" },
                    { new Guid("fbf9920b-fc81-4e25-8235-175042afe54e"), 0, new DateTime(2075, 2, 1, 10, 48, 12, 868, DateTimeKind.Local).AddTicks(8876), null, new Guid("940927fb-be4f-4db9-af96-c8eabeaebece"), "54f7e2da-a193-4a0d-b0b3-25401896b61d", new DateTime(2023, 5, 10, 10, 48, 12, 868, DateTimeKind.Local).AddTicks(8874), null, "mehmet.yilmaz@outlook.com", false, "1525001082", "/images/UserPhotos/defaultuser.jpg", false, null, "Mehmet", "MEHMET.YILMAZ@OUTLOOK.COM", "MEHMET.YILMAZ@OUTLOOK.COM", "AQAAAAEAACcQAAAAEDWRq/waPZQlnZ77u0RSARPL/UnjqT1XioFmb9307Bowb28m+jFZQGWOQl3KKAjgYA==", "+905682410749", false, "Taxi driver", null, "3ZAPPORCB8LFIJIEQCAR5LPVIUD4LW56", 3, "Yılmaz", new Guid("8fe8d552-bd28-4f89-a815-6513d629569f"), false, null, "mehmet.yilmaz@outlook.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("c84fe7b8-0faf-4ce8-a9b1-bf79ca13f4ef"), new Guid("0477ccd5-bff4-4b0c-a0c0-9be8b79caa95") },
                    { new Guid("c84fe7b8-0faf-4ce8-a9b1-bf79ca13f4ef"), new Guid("0aa6b735-83a3-46d6-826e-96589ce07fd4") },
                    { new Guid("c84fe7b8-0faf-4ce8-a9b1-bf79ca13f4ef"), new Guid("39d2c68b-7211-4587-b27d-5205964ed156") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("3d10c1ab-e59b-44d6-9a98-2781db1d42de") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("4aec7e0d-be9c-41b2-b1ff-f04088c8ecb7") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("56fa3184-be5c-4fdf-bfd3-b30297017428") },
                    { new Guid("c84fe7b8-0faf-4ce8-a9b1-bf79ca13f4ef"), new Guid("6383f107-1df3-40ad-afb0-b23b43afa722") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("7de6cd16-11fa-42b1-b8c0-58d20b689816") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("89cec818-f48b-40ed-9dd5-b6506c20951a") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("8e153450-b8b4-45d8-a0d6-aa4a5d7bc169") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("a49390ba-5b8b-445b-bbc4-a76f5e69bb3f") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("c641568a-f7d3-41f1-a16f-be578fe369f2") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("d18706c1-c134-4f78-83e1-7cf8d0b09a21") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("d486ecfb-b7b5-4742-9f40-443477556153") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("e94333f7-fc57-405e-ae7e-b8beb32d8992") },
                    { new Guid("c84fe7b8-0faf-4ce8-a9b1-bf79ca13f4ef"), new Guid("ea5b1748-620b-49c9-988d-b4eec19b67b6") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("ee703a77-7c16-48c6-bc2f-db1a788393cb") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("ee79a705-6435-4e31-8dfa-9449049a3f92") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("f6987ece-f047-4c92-8d99-aafc6497e6f3") },
                    { new Guid("49081fb1-5f2c-4be4-a792-b7dae95bc9f0"), new Guid("fbf9920b-fc81-4e25-8235-175042afe54e") }
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
