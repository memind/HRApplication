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
                    { new Guid("451747c1-45d5-4f1f-a00a-e56c5c7b1d74"), "XHSSY0TI1UZ6AL0E7STK1PUKNCKF88LB", "Personal", "PERSONAL" },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), "Z4N6XVFY893N85R4ASH31DLLWBZZYF8I", "Company Administrator", "COMPANY ADMINISTRATOR" },
                    { new Guid("dd59a103-db7f-4dff-aead-c7832328af56"), "4L7DHBDND3VQIXGHEX54CT1QZ7WG6ONN", "Site Administrator", "SITE ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("027703bc-c9ba-454b-a58e-c90873d50312"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6753), null, "Metal", 1, null },
                    { new Guid("089f61ae-6a71-41e6-9f6a-b874fc660176"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6764), null, "Ticaret (Satış ve Pazarlama)", 1, null },
                    { new Guid("0e47ba62-811c-4a5f-8696-6756f6ae4694"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6730), null, "Cam, Çimento ve ToprakÇevre", 1, null },
                    { new Guid("11805b22-108e-4eef-82bf-98323191473d"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6760), null, "Spor ve Rekreasyon", 1, null },
                    { new Guid("14e34f21-709a-4e7a-9953-83455e7eb0cf"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6731), null, "Eğitim", 1, null },
                    { new Guid("1f689906-d592-4caf-bd7d-fb9c38a0de4f"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6756), null, "Sağlık ve Sosyal Hizmetler", 1, null },
                    { new Guid("201fe1a0-5eed-44d9-b9af-259576068b48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6737), null, "Enerji", 1, null },
                    { new Guid("2f9265e5-fc95-4288-a811-7a79a329fbe3"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6767), null, "Turizm, Konaklama, Yiyecek-İçecek Hizmetleri", 1, null },
                    { new Guid("341657f2-b7e2-4626-8e42-1656e726f52e"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6768), null, "Ulaştırma, Lojistik ve Haberleşme", 1, null },
                    { new Guid("3ebc660e-1594-4e9c-beab-1dd3c3e485dd"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6761), null, "Tarım, Avcılık ve Balıkçılık", 1, null },
                    { new Guid("3ffd7cb7-16da-43d7-9da7-404cb6950120"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6740), null, "Gıda", 1, null },
                    { new Guid("4e0e4fb6-7339-467f-9600-4570f83123ff"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6704), null, "Ahşap Teknolojisi", 1, null },
                    { new Guid("51e11be5-b5ea-49e2-8c47-873429ca287c"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6741), null, "İnşaat", 1, null },
                    { new Guid("548615b0-3b6a-40d6-81cc-e3269f176762"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6747), null, "Kimya, Petrol, Lastik ve PLastik", 1, null },
                    { new Guid("6d2fd800-6c3a-4875-9e12-20b1a3dfce63"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6738), null, "Finans", 1, null },
                    { new Guid("730d8e43-a0ec-4bdc-bb82-7f77c1fbdda1"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6746), null, "İş ve Yönetimi", 1, null },
                    { new Guid("7e8de850-9c36-467b-ab01-f34d0f2607c3"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6733), null, "Elektrik ve Elektronik", 1, null },
                    { new Guid("85cdadfd-32db-4884-85a3-cd683d8a787e"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6728), null, "Bilişim", 1, null },
                    { new Guid("879ee182-4aac-4ec7-bed0-accb171b44d2"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6750), null, "Maden", 1, null },
                    { new Guid("b06b9cde-adb7-4582-b4c5-d77d150cec7d"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6749), null, "Kültür, Sanat ve Tasarım", 1, null },
                    { new Guid("d6b4c1fe-ea9e-43b4-a9d5-aca59cca5c26"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6752), null, "Medya, İletişim ve Yayıncılık", 1, null },
                    { new Guid("ec97eb4d-a910-438f-8178-7338f61b26e0"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6766), null, "Toplumsal ve Kişisel Hizmetler", 1, null },
                    { new Guid("f00820f6-6c7c-4b9f-b097-fbd9f2211f06"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6754), null, "Otomotiv", 1, null },
                    { new Guid("f82484b5-7b1a-4b2a-87f4-dc371eede3cd"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6763), null, "Tekstil, Hazır Giyim, Deri", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "NumberOfEmployees", "PhoneNumber", "SectorId", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("036e34c7-64cb-4e3f-a3f9-6d6de28b0436"), new DateTime(2023, 5, 11, 9, 35, 34, 85, DateTimeKind.Local).AddTicks(3174), null, "info@ozturkkollektifsirketi.com", "Öztürk Kollektif Şirketi", 82, "+905352706233", new Guid("7e8de850-9c36-467b-ab01-f34d0f2607c3"), 3, null },
                    { new Guid("0b31c53a-2ef5-4872-9401-6969416aa560"), new DateTime(2023, 5, 11, 9, 35, 34, 14, DateTimeKind.Local).AddTicks(1997), null, "info@yildizkooperatifsirketi.com", "Yıldız Kooperatif Şirketi", 66, "+905986051274", new Guid("f00820f6-6c7c-4b9f-b097-fbd9f2211f06"), 3, null },
                    { new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6877), null, "ikapp@ikapp.com", "IKApp A.Ş.", 5, "+905431590896", new Guid("85cdadfd-32db-4884-85a3-cd683d8a787e"), 1, null },
                    { new Guid("27181829-9cd2-4eb0-bc9f-557c57afa7a5"), new DateTime(2023, 5, 11, 9, 35, 34, 31, DateTimeKind.Local).AddTicks(8206), null, "info@ozturkanonimsirketi.com", "Öztürk Anonim Şirketi", 21, "+905364176748", new Guid("f00820f6-6c7c-4b9f-b097-fbd9f2211f06"), 3, null },
                    { new Guid("64a61aa1-89f7-48f3-a2f8-379eec32b3dc"), new DateTime(2023, 5, 11, 9, 35, 34, 37, DateTimeKind.Local).AddTicks(6889), null, "info@yildizkomanditsirketi.com", "Yıldız Komandit Şirketi", 75, "+905245637873", new Guid("1f689906-d592-4caf-bd7d-fb9c38a0de4f"), 3, null },
                    { new Guid("67ab1fa5-de44-4fac-a459-617ce25cf18e"), new DateTime(2023, 5, 11, 9, 35, 34, 43, DateTimeKind.Local).AddTicks(5557), null, "info@ozturklimitedsirketi.com", "Öztürk Limited Şirketi", 56, "+905472699854", new Guid("51e11be5-b5ea-49e2-8c47-873429ca287c"), 3, null },
                    { new Guid("6df6813a-f8d8-4df7-ae41-1165136511eb"), new DateTime(2023, 5, 11, 9, 35, 34, 56, DateTimeKind.Local).AddTicks(492), null, "info@yildizkomanditsirketi.com", "Yıldız Komandit Şirketi", 94, "+905464204859", new Guid("6d2fd800-6c3a-4875-9e12-20b1a3dfce63"), 3, null },
                    { new Guid("790ffc62-8d24-4460-a27d-12032728aed3"), new DateTime(2023, 5, 11, 9, 35, 34, 8, DateTimeKind.Local).AddTicks(2771), null, "info@ozdemirkooperatifsirketi.com", "Özdemir Kooperatif Şirketi", 31, "+905977264693", new Guid("3ebc660e-1594-4e9c-beab-1dd3c3e485dd"), 3, null },
                    { new Guid("7c197d87-b380-44d7-9741-cf73e220ba09"), new DateTime(2023, 5, 11, 9, 35, 34, 49, DateTimeKind.Local).AddTicks(8145), null, "info@demiranonimsirketi.com", "Demir Anonim Şirketi", 87, "+905968719491", new Guid("879ee182-4aac-4ec7-bed0-accb171b44d2"), 3, null },
                    { new Guid("8a01feda-c82e-4360-9c79-ac38fd84bff9"), new DateTime(2023, 5, 11, 9, 35, 34, 61, DateTimeKind.Local).AddTicks(9025), null, "info@ozdemirkooperatifsirketi.com", "Özdemir Kooperatif Şirketi", 52, "+905604296860", new Guid("7e8de850-9c36-467b-ab01-f34d0f2607c3"), 3, null },
                    { new Guid("910a74e9-dee9-49eb-bd72-b706f4b2dae0"), new DateTime(2023, 5, 11, 9, 35, 34, 73, DateTimeKind.Local).AddTicks(5959), null, "info@celikkomanditsirketi.com", "Çelik Komandit Şirketi", 30, "+905162000735", new Guid("11805b22-108e-4eef-82bf-98323191473d"), 3, null },
                    { new Guid("a4fb67e1-40f7-4f2d-8ae6-a84fdf0d426c"), new DateTime(2023, 5, 11, 9, 35, 34, 91, DateTimeKind.Local).AddTicks(154), null, "info@ozturkkooperatifsirketi.com", "Öztürk Kooperatif Şirketi", 65, "+905633549186", new Guid("14e34f21-709a-4e7a-9953-83455e7eb0cf"), 3, null },
                    { new Guid("acad92f8-f2bf-49b5-a0ef-10126da14184"), new DateTime(2023, 5, 11, 9, 35, 34, 67, DateTimeKind.Local).AddTicks(7534), null, "info@yilmazkooperatifsirketi.com", "Yılmaz Kooperatif Şirketi", 60, "+905778416167", new Guid("027703bc-c9ba-454b-a58e-c90873d50312"), 3, null },
                    { new Guid("c86e9ea3-d642-49ef-b903-2677b06c27a0"), new DateTime(2023, 5, 11, 9, 35, 34, 25, DateTimeKind.Local).AddTicks(9293), null, "info@kayakomanditsirketi.com", "Kaya Komandit Şirketi", 3, "+905895688938", new Guid("7e8de850-9c36-467b-ab01-f34d0f2607c3"), 3, null },
                    { new Guid("df71d666-fcee-4ab9-87ab-d6e0615999cf"), new DateTime(2023, 5, 11, 9, 35, 34, 20, DateTimeKind.Local).AddTicks(681), null, "info@kayaanonimsirketi.com", "Kaya Anonim Şirketi", 51, "+905238474308", new Guid("879ee182-4aac-4ec7-bed0-accb171b44d2"), 3, null },
                    { new Guid("e216856a-6205-4a09-b5a2-8a9d1d524360"), new DateTime(2023, 5, 11, 9, 35, 34, 79, DateTimeKind.Local).AddTicks(4228), null, "info@sahinkomanditsirketi.com", "Şahin Komandit Şirketi", 45, "+905151872925", new Guid("7e8de850-9c36-467b-ab01-f34d0f2607c3"), 3, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("00528c90-5e87-46ec-9188-d4a235967920"), new Guid("0b31c53a-2ef5-4872-9401-6969416aa560"), new DateTime(2023, 5, 11, 9, 35, 34, 14, DateTimeKind.Local).AddTicks(2039), null, "Sales Associate", 1, null },
                    { new Guid("008bcb91-496c-4ca9-aead-0a547c4a86f1"), new Guid("036e34c7-64cb-4e3f-a3f9-6d6de28b0436"), new DateTime(2023, 5, 11, 9, 35, 34, 85, DateTimeKind.Local).AddTicks(3180), null, "Front Desk Associate", 1, null },
                    { new Guid("00f39cae-bbe6-45c5-ae4b-8b354063093e"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7011), null, "Safety Director", 1, null },
                    { new Guid("0854e1db-35b8-482f-aaa0-590d38ef29fc"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6960), null, "COO (Chief Operating Officer)", 1, null },
                    { new Guid("0d0898bc-28ba-4c6f-98fc-bd7ed650adc5"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6966), null, "HR Analyst", 1, null },
                    { new Guid("0db87f4b-24e4-4479-a95c-66001f94d330"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6906), null, "Sales Associate", 1, null },
                    { new Guid("0f168fa1-413e-4702-97dc-9f909c761916"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6953), null, "Customer Success Manager", 1, null },
                    { new Guid("152827ce-5112-4b3c-b32b-3a3ce3ebd3ac"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6909), null, "CMO (Chief Marketing Officer)", 1, null },
                    { new Guid("170dba08-b306-46e5-b74b-c7d23e5001bf"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6962), null, "Director of Business Operations", 1, null },
                    { new Guid("1cc0dabe-6649-4d90-bc4f-fd92269400c1"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6965), null, "Sr. Manager of HR", 1, null },
                    { new Guid("1ec51b05-98ac-42d2-9034-5ec7adb88d94"), new Guid("6df6813a-f8d8-4df7-ae41-1165136511eb"), new DateTime(2023, 5, 11, 9, 35, 34, 56, DateTimeKind.Local).AddTicks(497), null, "Risk Analyst", 1, null },
                    { new Guid("214e6d52-646d-47b5-b443-7b9dd2d0e27c"), new Guid("7c197d87-b380-44d7-9741-cf73e220ba09"), new DateTime(2023, 5, 11, 9, 35, 34, 49, DateTimeKind.Local).AddTicks(8170), null, "Guest Services Supervisor", 1, null },
                    { new Guid("28bc1084-73d1-4e45-8efc-895b737165b9"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6973), null, "Full Stack Developer", 1, null },
                    { new Guid("2a54579f-92e1-4f35-b4e4-4b48ae7e697e"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7008), null, "Hotel Receptionist", 1, null },
                    { new Guid("2b58e2d8-3dc6-451f-bb6b-46117d875fcd"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6969), null, "Software Engineer I, II, III", 1, null },
                    { new Guid("2b72b1b3-010d-4e6d-b6e8-20505332c4e2"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6911), null, "Marketing Director", 1, null },
                    { new Guid("2e034aa6-3352-4648-8976-802eb64b28ec"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6894), null, "VP of Sales", 1, null },
                    { new Guid("33004240-6b2f-440c-b8e7-a09f6d89649b"), new Guid("8a01feda-c82e-4360-9c79-ac38fd84bff9"), new DateTime(2023, 5, 11, 9, 35, 34, 61, DateTimeKind.Local).AddTicks(9033), null, "Teaching Assistant", 1, null },
                    { new Guid("3303e12d-a1af-481b-8359-c9a1364b2985"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6916), null, "Marketing Coordinator", 1, null },
                    { new Guid("334832c2-d284-449f-89ce-ac3c17d397c8"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6996), null, "Teacher", 1, null },
                    { new Guid("39ca5a8b-4232-4ffc-9c9e-35a559e5877b"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6955), null, "Customer Service Representative", 1, null },
                    { new Guid("3ba9e159-4122-4ad6-be41-f288110fbcfa"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6963), null, "Operations Supervisor", 1, null },
                    { new Guid("4fe08181-bff4-44f1-b801-03922b0a654d"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6914), null, "Marketing Analyst", 1, null },
                    { new Guid("53f40433-0a52-44b2-9a8d-166d18367516"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6993), null, "Registrar", 1, null },
                    { new Guid("5522c410-dbe2-462f-8e87-6d5d1d9449c0"), new Guid("910a74e9-dee9-49eb-bd72-b706f4b2dae0"), new DateTime(2023, 5, 11, 9, 35, 34, 73, DateTimeKind.Local).AddTicks(5962), null, "Full Stack Developer", 1, null },
                    { new Guid("5b3966b5-24c9-43c4-9366-d26996d502a3"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6991), null, "Principal", 1, null },
                    { new Guid("5fe9307e-c52a-46b2-9cce-e5ceb595fe28"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6978), null, "Registered Nurse", 1, null },
                    { new Guid("60ee3640-2bbc-4af7-a83f-f02b4740de05"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7016), null, "Contract Administrator", 1, null },
                    { new Guid("61205078-dfd6-42b2-87a0-e83eb49ecc16"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6988), null, "Occupational Therapy Aide", 1, null },
                    { new Guid("62f7b4e6-4034-46a2-a645-102b15b1a756"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6974), null, "Systems Administrator", 1, null },
                    { new Guid("661e16ca-063d-4ace-980c-361410fca613"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6968), null, "Director of Information Security", 1, null },
                    { new Guid("6852d6b5-c4d5-4837-b5fa-50fdca7242a9"), new Guid("790ffc62-8d24-4460-a27d-12032728aed3"), new DateTime(2023, 5, 11, 9, 35, 34, 8, DateTimeKind.Local).AddTicks(2779), null, "Teaching Assistant", 1, null },
                    { new Guid("7067a817-a98c-459e-b631-7412a02a6fd9"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7009), null, "Construction Foreman", 1, null },
                    { new Guid("73308397-5391-4b37-9f73-3527297c50ba"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6987), null, "Clinical Laboratory Technician", 1, null },
                    { new Guid("7d3c9a07-9658-4b03-8e37-11eab7345502"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6980), null, "Pharmacy Technician", 1, null },
                    { new Guid("7e1e95b1-7033-4359-a249-4f871f8764a0"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7006), null, "Server/Host/Hostess", 1, null },
                    { new Guid("7fef34e8-d9a4-4ea3-9a6b-dd15d620b3b0"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6982), null, "Physical Therapist", 1, null },
                    { new Guid("81177806-7278-45b9-939a-bd14679d30c2"), new Guid("e216856a-6205-4a09-b5a2-8a9d1d524360"), new DateTime(2023, 5, 11, 9, 35, 34, 79, DateTimeKind.Local).AddTicks(4232), null, "Full Stack Developer", 1, null },
                    { new Guid("82ad5882-cd55-4f30-84d5-9a83501305eb"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6984), null, "Nursing Assistant", 1, null },
                    { new Guid("8360c5e9-ab87-46b1-a3f4-e3baceca298c"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7005), null, "Front Desk Associate", 1, null },
                    { new Guid("86a0ed4c-f17b-44a8-8992-f29c9a795082"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6977), null, "Other Industries:", 1, null },
                    { new Guid("8763469a-9b54-4df6-94ef-24afc6369fee"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6959), null, "Support Specialist", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CompanyId", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("87b19be8-4534-4c6c-8d4a-a69d95764396"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6994), null, "School Counselor", 1, null },
                    { new Guid("8db85304-b891-430e-aa1b-7064132b7b56"), new Guid("a4fb67e1-40f7-4f2d-8ae6-a84fdf0d426c"), new DateTime(2023, 5, 11, 9, 35, 34, 91, DateTimeKind.Local).AddTicks(156), null, "Data Analyst", 1, null },
                    { new Guid("93dd06bd-8807-4ce2-a1ce-c62e79c6e215"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7014), null, "Project Manager", 1, null },
                    { new Guid("94cecfb3-c050-45f9-a4b4-1b29b0152d58"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6918), null, "VP of Finance", 1, null },
                    { new Guid("950facce-0519-42bd-be40-09d1a6f3f4f3"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6923), null, "Investment Analyst", 1, null },
                    { new Guid("9b3c1327-0260-48b5-9bd1-cb29514dfb63"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6929), null, "Account Manager", 1, null },
                    { new Guid("a4bf8a84-401e-4056-b497-1cdc64660086"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6926), null, "Risk Analyst", 1, null },
                    { new Guid("ac35b919-4ad7-4007-83d5-006d79ea9f1e"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6997), null, "Teaching Assistant", 1, null },
                    { new Guid("ad97e104-1854-4472-b989-d0c43663911e"), new Guid("df71d666-fcee-4ab9-87ab-d6e0615999cf"), new DateTime(2023, 5, 11, 9, 35, 34, 20, DateTimeKind.Local).AddTicks(685), null, "Data Analyst", 1, null },
                    { new Guid("b10300c9-f418-4457-b1ca-2159baa99d9d"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6975), null, "Data Analyst", 1, null },
                    { new Guid("b1c4068a-6c67-41ac-8da2-b7644ad78613"), new Guid("67ab1fa5-de44-4fac-a459-617ce25cf18e"), new DateTime(2023, 5, 11, 9, 35, 34, 43, DateTimeKind.Local).AddTicks(5562), null, "Software Engineer I, II, III", 1, null },
                    { new Guid("b38676d7-6f75-4797-8069-0bbaddab7a11"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6921), null, "Procurement Director", 1, null },
                    { new Guid("b79a3d77-5766-46c2-854f-29662c06bbf6"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7017), null, "Project Appraisal Engineer", 1, null },
                    { new Guid("cd333a63-0c2b-48bd-996d-ece26fa88792"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6904), null, "Sales Representative", 1, null },
                    { new Guid("d06e90e9-2be2-4f83-978b-9ce0d03fa775"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7000), null, "General Manager", 1, null },
                    { new Guid("d4011f2e-38a8-4bc6-961d-bb7e733d29b1"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7003), null, "Concierge", 1, null },
                    { new Guid("d49b0a32-eb83-45b6-a9c3-e572314561da"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6990), null, "Administrator", 1, null },
                    { new Guid("d4d4b64a-f6a4-4c27-8ebb-93de587ca3b4"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6901), null, "Regional Sales Manager", 1, null },
                    { new Guid("d835af41-ec70-4e35-acea-8654fa8dec8e"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6927), null, "VP of Client Services", 1, null },
                    { new Guid("db779e39-77d4-40a7-9200-bd490196c7b2"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6912), null, "Sr. Marketing Manager", 1, null },
                    { new Guid("db9da90c-1074-4dd0-b07d-f2b88383527c"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7002), null, "Guest Services Supervisor", 1, null },
                    { new Guid("e2092c2c-15c2-419a-8460-3bd7b728bc0f"), new Guid("64a61aa1-89f7-48f3-a2f8-379eec32b3dc"), new DateTime(2023, 5, 11, 9, 35, 34, 37, DateTimeKind.Local).AddTicks(6898), null, "Inspector", 1, null },
                    { new Guid("e32c482d-eb66-44c1-b3df-9037032a986d"), new Guid("27181829-9cd2-4eb0-bc9f-557c57afa7a5"), new DateTime(2023, 5, 11, 9, 35, 34, 31, DateTimeKind.Local).AddTicks(8217), null, "Customer Success Manager", 1, null },
                    { new Guid("eb209307-66af-4e92-afbb-78c600293e2c"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6899), null, "National Sales Director", 1, null },
                    { new Guid("ec07587f-9de6-4756-a302-4e802201d630"), new Guid("acad92f8-f2bf-49b5-a0ef-10126da14184"), new DateTime(2023, 5, 11, 9, 35, 34, 67, DateTimeKind.Local).AddTicks(7538), null, "Sales Representative", 1, null },
                    { new Guid("ed771faa-7989-4840-b654-936a983a64c7"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(6924), null, "Credit Analyst", 1, null },
                    { new Guid("f15b8616-dd03-4187-ad67-4e837c2b8fb1"), new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7019), null, "Inspector", 1, null },
                    { new Guid("fb4113de-f198-4fe1-88f2-e309ca66e518"), new Guid("c86e9ea3-d642-49ef-b903-2677b06c27a0"), new DateTime(2023, 5, 11, 9, 35, 34, 25, DateTimeKind.Local).AddTicks(9309), null, "Account Manager", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "BloodGroup", "CompanyId", "ConcurrencyStamp", "CreateDate", "DeleteDate", "Email", "EmailConfirmed", "IdentityNumber", "ImagePath", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profession", "SecondName", "SecurityStamp", "Status", "Surname", "TitleId", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("0a2ded2a-d239-48aa-9208-fd680a848f59"), 0, new DateTime(2049, 1, 18, 9, 35, 34, 67, DateTimeKind.Local).AddTicks(7546), null, new Guid("acad92f8-f2bf-49b5-a0ef-10126da14184"), "96a056c5-d09b-437d-80ba-6ce11ceeed33", new DateTime(2023, 5, 11, 9, 35, 34, 67, DateTimeKind.Local).AddTicks(7545), null, "ibrahim.yilmaz@yandex.com", false, "4373144488", "/images/UserPhotos/defaultuser.jpg", false, null, "İbrahim", "IBRAHIM.YILMAZ@YANDEX.COM", "IBRAHIM.YILMAZ@YANDEX.COM", "AQAAAAEAACcQAAAAECOwsJHzJo4f4+Om4hn5dRjswfXNZZT49SR7XgDt45JiCp6bXNA4218e+WeKJFQVfg==", "+905219702621", false, "Hairdresser", null, "6VLL3AS0OAL19I3Q0JSFEVETWQV84LML", 3, "Yılmaz", new Guid("ec07587f-9de6-4756-a302-4e802201d630"), false, null, "ibrahim.yilmaz@yandex.com" },
                    { new Guid("1c84649d-9aca-45db-967d-27b04b30ef85"), 0, new DateTime(2062, 11, 11, 9, 35, 34, 20, DateTimeKind.Local).AddTicks(694), null, new Guid("df71d666-fcee-4ab9-87ab-d6e0615999cf"), "515da689-5f27-407a-8fc2-17ddf69c4207", new DateTime(2023, 5, 11, 9, 35, 34, 20, DateTimeKind.Local).AddTicks(692), null, "ahmet.kaya@hotmail.com", false, "6820278744", "/images/UserPhotos/defaultuser.jpg", false, null, "Ahmet", "AHMET.KAYA@HOTMAIL.COM", "AHMET.KAYA@HOTMAIL.COM", "AQAAAAEAACcQAAAAEA5GW4KqhoVKlKgWtwo7jxxEPnOmxbQ+6fcfssg50yz6wQ4uo1c6osoVcRnHMoiWYg==", "+905939936886", false, "Chef/Cook", null, "CSKPQ6BT1BXE5VF52TMDMAH5BCJVFBJ4", 3, "Kaya", new Guid("ad97e104-1854-4472-b989-d0c43663911e"), false, null, "ahmet.kaya@hotmail.com" },
                    { new Guid("26709fad-a014-457c-919b-b634174b0c7d"), 0, new DateTime(2041, 8, 24, 9, 35, 34, 56, DateTimeKind.Local).AddTicks(503), null, new Guid("6df6813a-f8d8-4df7-ae41-1165136511eb"), "a85442a1-cc47-427a-888f-e8b2c4c99bc5", new DateTime(2023, 5, 11, 9, 35, 34, 56, DateTimeKind.Local).AddTicks(501), null, "yusuf.yildiz@google.com", false, "5575601762", "/images/UserPhotos/defaultuser.jpg", false, null, "Yusuf", "YUSUF.YILDIZ@GOOGLE.COM", "YUSUF.YILDIZ@GOOGLE.COM", "AQAAAAEAACcQAAAAEDM9y9cDlkgKwcoJ6Q8kCyXdWT/AO5GjZgoRTUBvLtBfi8XKb/3fl8NFhb9NLRf5aQ==", "+905384694729", false, "Teacher", null, "FOS0GMPER8AWHN0QCMWAORI4FGC6PZ17", 3, "Yıldız", new Guid("1ec51b05-98ac-42d2-9034-5ec7adb88d94"), false, null, "yusuf.yildiz@google.com" },
                    { new Guid("28e06592-35f7-4387-ac28-4fa7c2b65ad7"), 0, new DateTime(2054, 5, 4, 9, 35, 34, 25, DateTimeKind.Local).AddTicks(9316), null, new Guid("c86e9ea3-d642-49ef-b903-2677b06c27a0"), "4ba4c122-0d59-4895-94c7-522b93073691", new DateTime(2023, 5, 11, 9, 35, 34, 25, DateTimeKind.Local).AddTicks(9314), null, "yusuf.kaya@outlook.com", false, "2818153626", "/images/UserPhotos/defaultuser.jpg", false, null, "Yusuf", "YUSUF.KAYA@OUTLOOK.COM", "YUSUF.KAYA@OUTLOOK.COM", "AQAAAAEAACcQAAAAEKjEVw/3a3VGJPGOrznB1tFSgwvILKG91iB7gKVPWGXpox0N6zT6yyV+xHZcFxX3kQ==", "+905214666722", false, "Model", null, "5MKDGOLCIQP6LDXHXUJEAMJTKRBMADXO", 3, "Kaya", new Guid("fb4113de-f198-4fe1-88f2-e309ca66e518"), false, null, "yusuf.kaya@outlook.com" },
                    { new Guid("2cbc8181-9594-451a-ae3c-fd7c1201fa5f"), 0, new DateTime(2059, 3, 18, 9, 35, 34, 8, DateTimeKind.Local).AddTicks(2786), null, new Guid("790ffc62-8d24-4460-a27d-12032728aed3"), "5c1ca733-2b2c-4710-ab40-8cb4fbc33ef8", new DateTime(2023, 5, 11, 9, 35, 34, 8, DateTimeKind.Local).AddTicks(2784), null, "ahmet.ozdemir@yandex.com", false, "3518670550", "/images/UserPhotos/defaultuser.jpg", false, null, "Ahmet", "AHMET.OZDEMIR@YANDEX.COM", "AHMET.OZDEMIR@YANDEX.COM", "AQAAAAEAACcQAAAAEPqwbeY0YY2OlOb8NQqBfXdmmM+CWmS1fSpmtKniy9y2dpNPcQERig0Z+rwq66CQjA==", "+905771173244", false, "Pharmacist", null, "OMO6BFISILFWWLRVSZJPTUAWYE08IFZM", 3, "Özdemir", new Guid("6852d6b5-c4d5-4837-b5fa-50fdca7242a9"), false, null, "ahmet.ozdemir@yandex.com" },
                    { new Guid("33cb8551-77d7-43ff-b305-2d68f1307f71"), 0, new DateTime(2067, 10, 6, 9, 35, 34, 2, DateTimeKind.Local).AddTicks(4275), null, new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), "d786d67d-fd28-456b-9ffc-34c320b0b655", new DateTime(2023, 5, 11, 9, 35, 34, 2, DateTimeKind.Local).AddTicks(4272), null, "test5@test.com", false, "3574887620", "/images/UserPhotos/defaultuser.jpg", false, null, "Mustafa", "TEST5@TEST.COM", "TEST5@TEST.COM", "AQAAAAEAACcQAAAAEFV2Zor2RdjGFOKEf2eLsGTgBQPbZUIqBghhmC4VBAARg9cUOeyaMiSyv8ti4mkk4w==", "+905248156946", false, "Farmer", null, "EN8CBZ7D6BSRGTWHY3TBF12JC67V4IG1", 1, "Aydın", new Guid("87b19be8-4534-4c6c-8d4a-a69d95764396"), false, null, "test5@test.com" },
                    { new Guid("80570c85-e7b9-425b-b67c-8a7ee4b5ab4a"), 0, new DateTime(2072, 5, 5, 9, 35, 33, 996, DateTimeKind.Local).AddTicks(5732), null, new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), "419ed43b-284c-49a7-9d17-857648bce653", new DateTime(2023, 5, 11, 9, 35, 33, 996, DateTimeKind.Local).AddTicks(5725), null, "test4@test.com", false, "7136624830", "/images/UserPhotos/defaultuser.jpg", false, null, "Osman", "TEST4@TEST.COM", "TEST4@TEST.COM", "AQAAAAEAACcQAAAAEGAisXwsbMddvlCTgn5u5H5u8t8FOLt89sgzszr0MB+Yp9ju6zSOkuV7ZBwXRp8GRw==", "+905132561053", false, "Postman", null, "20Z4D9S88HXYHLQTIPTZMTEEN8XN0CX2", 1, "Yıldız", new Guid("0854e1db-35b8-482f-aaa0-590d38ef29fc"), false, null, "test4@test.com" },
                    { new Guid("94c2bcdc-7dbf-4cf8-a053-eb5534f867f6"), 0, new DateTime(2070, 10, 4, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7037), null, new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), "08098c22-b26f-4629-b4ce-27adde9129c0", new DateTime(2023, 5, 11, 9, 35, 33, 978, DateTimeKind.Local).AddTicks(7032), null, "test1@test.com", false, "6372803762", "/images/UserPhotos/defaultuser.jpg", false, null, "İsmail", "TEST1@TEST.COM", "TEST1@TEST.COM", "AQAAAAEAACcQAAAAEFKlWGP/UFsrgwUFqFC0YHoHgQ3zzKvdeN2xsK0XqZZmrSLUHAYhKwQlWw8dOCkHHQ==", "+905405842315", false, "Lifeguard", null, "KVB4ARB4FMSDL688FU6VJSN1SOXOGK65", 1, "Demir", new Guid("eb209307-66af-4e92-afbb-78c600293e2c"), false, null, "test1@test.com" },
                    { new Guid("9f120828-9658-4c2d-9d1e-2d21eb3ce8b7"), 0, new DateTime(2051, 12, 26, 9, 35, 34, 43, DateTimeKind.Local).AddTicks(5570), null, new Guid("67ab1fa5-de44-4fac-a459-617ce25cf18e"), "12081f42-7ada-42de-921d-f1bb4e47d6e2", new DateTime(2023, 5, 11, 9, 35, 34, 43, DateTimeKind.Local).AddTicks(5569), null, "mehmet.ozturk@outlook.com", false, "8062486352", "/images/UserPhotos/defaultuser.jpg", false, null, "Mehmet", "MEHMET.OZTURK@OUTLOOK.COM", "MEHMET.OZTURK@OUTLOOK.COM", "AQAAAAEAACcQAAAAENnn4eHWwjk0opGtUj7fAmYQ7VFR0qwRAnNFBNce9tDtiAHA+8p9KXXy6MQpCZM7qg==", "+905417913920", false, "Shop assistant", null, "CHHIZ3Y9Y67LPF1492TVCMXOZ488BENJ", 3, "Öztürk", new Guid("b1c4068a-6c67-41ac-8da2-b7644ad78613"), false, null, "mehmet.ozturk@outlook.com" },
                    { new Guid("a5981db3-b0d3-4e5d-8fa4-80da7416f86d"), 0, new DateTime(2061, 9, 19, 9, 35, 33, 990, DateTimeKind.Local).AddTicks(5102), null, new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), "1eb4fd75-6903-4b17-bf08-fc7b4bb6d7a5", new DateTime(2023, 5, 11, 9, 35, 33, 990, DateTimeKind.Local).AddTicks(5097), null, "test3@test.com", false, "4032013890", "/images/UserPhotos/defaultuser.jpg", false, null, "Mehmet", "TEST3@TEST.COM", "TEST3@TEST.COM", "AQAAAAEAACcQAAAAEJIT5w1iNfXs+XG1YS9PHYJDjcKWa0fVqQNYW2YzeKx9yw7efrXNHHzIsVOXwOICGA==", "+905618280647", false, "Factory worker", null, "0NJFZZJ3T2C2R06EE6TPCK68HNFYF5ZD", 1, "Yıldız", new Guid("db779e39-77d4-40a7-9200-bd490196c7b2"), false, null, "test3@test.com" },
                    { new Guid("b5f9cace-bfd5-4554-bd6d-90b118e90a75"), 0, new DateTime(2075, 11, 6, 9, 35, 34, 91, DateTimeKind.Local).AddTicks(162), null, new Guid("a4fb67e1-40f7-4f2d-8ae6-a84fdf0d426c"), "eaf080d2-23e7-4731-9b09-9283c58828af", new DateTime(2023, 5, 11, 9, 35, 34, 91, DateTimeKind.Local).AddTicks(161), null, "ali.ozturk@yahoo.com", false, "6656737460", "/images/UserPhotos/defaultuser.jpg", false, null, "Ali", "ALI.OZTURK@YAHOO.COM", "ALI.OZTURK@YAHOO.COM", "AQAAAAEAACcQAAAAEENtv+uDCpZZwjnje0EXCpENCIOwRtXrDgNilO+viPa+PoYtXTkvb6TPQfK594KlRQ==", "+905229246010", false, "Receptionist", null, "YWJS9W7KE1PJ4J4SALYUDL1BYR39ZI0A", 3, "Öztürk", new Guid("8db85304-b891-430e-aa1b-7064132b7b56"), false, null, "ali.ozturk@yahoo.com" },
                    { new Guid("bf819692-436f-4569-82b8-9efdd2c7205e"), 0, new DateTime(2071, 10, 3, 9, 35, 34, 61, DateTimeKind.Local).AddTicks(9046), null, new Guid("8a01feda-c82e-4360-9c79-ac38fd84bff9"), "9d5e11ea-d2f3-44e8-ba32-afca12e48e51", new DateTime(2023, 5, 11, 9, 35, 34, 61, DateTimeKind.Local).AddTicks(9041), null, "yusuf.ozdemir@yandex.com", false, "6056085718", "/images/UserPhotos/defaultuser.jpg", false, null, "Yusuf", "YUSUF.OZDEMIR@YANDEX.COM", "YUSUF.OZDEMIR@YANDEX.COM", "AQAAAAEAACcQAAAAEHAF3gch56KeTGEMYU1K18ums6BIVjlq0vhjCGyxeaueYjUeUY0u5AVOaCVF/dLI7w==", "+905753590521", false, "Postman", null, "0B7LJ0ZM4X9B55MC1VZH6382NAKRC031", 3, "Özdemir", new Guid("33004240-6b2f-440c-b8e7-a09f6d89649b"), false, null, "yusuf.ozdemir@yandex.com" },
                    { new Guid("c62b4d8f-6e77-4820-a095-e752e93d6ff2"), 0, new DateTime(2048, 1, 14, 9, 35, 34, 31, DateTimeKind.Local).AddTicks(8227), null, new Guid("27181829-9cd2-4eb0-bc9f-557c57afa7a5"), "621a4241-7f6e-4d3c-bf35-3923af912405", new DateTime(2023, 5, 11, 9, 35, 34, 31, DateTimeKind.Local).AddTicks(8225), null, "huseyin.ozturk@hotmail.com", false, "3886336850", "/images/UserPhotos/defaultuser.jpg", false, null, "Hüseyin", "HUSEYIN.OZTURK@HOTMAIL.COM", "HUSEYIN.OZTURK@HOTMAIL.COM", "AQAAAAEAACcQAAAAEL7ncYbUMFx5tThlX7i+LSEL41RJeBB0lagAxcjYDlbefASI1AoNaXTaXfVGJ+tw5g==", "+905456437073", false, "Secretary", null, "CHLKM6IA5V8NT1TVE7DFVFOMS549V94W", 3, "Öztürk", new Guid("e32c482d-eb66-44c1-b3df-9037032a986d"), false, null, "huseyin.ozturk@hotmail.com" },
                    { new Guid("c9841234-80c1-4a6f-ba91-9687f9a60ef3"), 0, new DateTime(2067, 1, 13, 9, 35, 34, 49, DateTimeKind.Local).AddTicks(8180), null, new Guid("7c197d87-b380-44d7-9741-cf73e220ba09"), "2b13be58-0810-43be-80e7-c7c8645cae19", new DateTime(2023, 5, 11, 9, 35, 34, 49, DateTimeKind.Local).AddTicks(8178), null, "ibrahim.demir@google.com", false, "6257064730", "/images/UserPhotos/defaultuser.jpg", false, null, "İbrahim", "IBRAHIM.DEMIR@GOOGLE.COM", "IBRAHIM.DEMIR@GOOGLE.COM", "AQAAAAEAACcQAAAAEPmZCaxtMiANfBAy3kpn3qHD22jQ0zdW0ykmUPjDpGTRP3gnZE5VU9nfchH/3/ExAQ==", "+905757210527", false, "Businessman", null, "656BZ9HXZISJHFFUH1PQLY2F3LCXDF29", 3, "Demir", new Guid("214e6d52-646d-47b5-b443-7b9dd2d0e27c"), false, null, "ibrahim.demir@google.com" },
                    { new Guid("cbfc66cc-6a8f-4e72-a5fd-69049955c23b"), 0, new DateTime(2045, 1, 13, 9, 35, 34, 85, DateTimeKind.Local).AddTicks(3186), null, new Guid("036e34c7-64cb-4e3f-a3f9-6d6de28b0436"), "b6c7af36-0e76-4285-bfc1-a6ddd7182121", new DateTime(2023, 5, 11, 9, 35, 34, 85, DateTimeKind.Local).AddTicks(3184), null, "ali.ozturk@yandex.com", false, "2358608248", "/images/UserPhotos/defaultuser.jpg", false, null, "Ali", "ALI.OZTURK@YANDEX.COM", "ALI.OZTURK@YANDEX.COM", "AQAAAAEAACcQAAAAEMTVLDIqGh5ziimh9tn9SKU9CHgTPPTiNTZhITyihIK7sz9BJIVOnPzJX7J2qvZhzA==", "+905433387488", false, "Politician", null, "YHWGHUQ86E57L9BSSHZ2KZMK7LK7O3M3", 3, "Öztürk", new Guid("008bcb91-496c-4ca9-aead-0a547c4a86f1"), false, null, "ali.ozturk@yandex.com" },
                    { new Guid("d052f77a-5cd9-4132-b741-42c1cb144af9"), 0, new DateTime(2042, 9, 20, 9, 35, 34, 73, DateTimeKind.Local).AddTicks(5966), null, new Guid("910a74e9-dee9-49eb-bd72-b706f4b2dae0"), "2ec071e7-0184-4cd0-b59a-c8358d885720", new DateTime(2023, 5, 11, 9, 35, 34, 73, DateTimeKind.Local).AddTicks(5966), null, "ismail.celik@microsoft.com", false, "8547524498", "/images/UserPhotos/defaultuser.jpg", false, null, "İsmail", "ISMAIL.CELIK@MICROSOFT.COM", "ISMAIL.CELIK@MICROSOFT.COM", "AQAAAAEAACcQAAAAEBcucec+Rm/URJBS3hkVqP4wX3FBH58W4wjmRu1z7tz3dWcRZ3/jmrS7rkFowyAT0Q==", "+905670294952", false, "Taxi driver", null, "VCYB68GTAGP29NSTH0UXKOQV81KLC029", 3, "Çelik", new Guid("5522c410-dbe2-462f-8e87-6d5d1d9449c0"), false, null, "ismail.celik@microsoft.com" },
                    { new Guid("d5dd2363-8a11-4fd2-b6ec-91635b09e25d"), 0, new DateTime(2044, 7, 24, 9, 35, 34, 14, DateTimeKind.Local).AddTicks(2054), null, new Guid("0b31c53a-2ef5-4872-9401-6969416aa560"), "18d26e1f-1579-4088-915c-03b1dd6f93d7", new DateTime(2023, 5, 11, 9, 35, 34, 14, DateTimeKind.Local).AddTicks(2050), null, "yusuf.yildiz@yandex.com", false, "4061886542", "/images/UserPhotos/defaultuser.jpg", false, null, "Yusuf", "YUSUF.YILDIZ@YANDEX.COM", "YUSUF.YILDIZ@YANDEX.COM", "AQAAAAEAACcQAAAAENmjLBr1bWYFhyDJ7FYEPeIFgJ/eQXy5zSWlGb5mjTWpHdDmD1uBPG9/Eap3H6PSTw==", "+905999012820", false, "Postman", null, "R32ALKDGSKGXT0WQ4PQQ7WWM9I7GNFJ9", 3, "Yıldız", new Guid("00528c90-5e87-46ec-9188-d4a235967920"), false, null, "yusuf.yildiz@yandex.com" },
                    { new Guid("f2186b6c-4294-4448-8419-1c7b4520a964"), 0, new DateTime(2060, 4, 15, 9, 35, 33, 984, DateTimeKind.Local).AddTicks(6310), null, new Guid("0c7e4e02-c78a-410c-a4a9-1b32a7f2df48"), "2c252d56-e5f1-42e3-ae9a-895f6989f858", new DateTime(2023, 5, 11, 9, 35, 33, 984, DateTimeKind.Local).AddTicks(6307), null, "test2@test.com", false, "3410213442", "/images/UserPhotos/defaultuser.jpg", false, null, "Mustafa", "TEST2@TEST.COM", "TEST2@TEST.COM", "AQAAAAEAACcQAAAAEAho0E0ZSvV6LSRfGE68E7dKbfxn8BcoGBuUCTZBeJIRIRpvdtPctqVBqsnyGixThA==", "+905663216354", false, "Photographer", null, "5TLUSFEC6Y21H94B3UPLRLWF310TH2EL", 1, "Öztürk", new Guid("0d0898bc-28ba-4c6f-98fc-bd7ed650adc5"), false, null, "test2@test.com" },
                    { new Guid("f2d17da7-c782-4594-ba43-49f1112e6c34"), 0, new DateTime(2073, 7, 30, 9, 35, 34, 79, DateTimeKind.Local).AddTicks(4237), null, new Guid("e216856a-6205-4a09-b5a2-8a9d1d524360"), "b34ba154-4337-4a29-a843-6a6837eb7551", new DateTime(2023, 5, 11, 9, 35, 34, 79, DateTimeKind.Local).AddTicks(4235), null, "ali.sahin@google.com", false, "6388205248", "/images/UserPhotos/defaultuser.jpg", false, null, "Ali", "ALI.SAHIN@GOOGLE.COM", "ALI.SAHIN@GOOGLE.COM", "AQAAAAEAACcQAAAAEJcu2LvvBaHlAPF9gyhrKXL6F9ETlo3TXfW/4sGAQnOMyvUs7LNHsBXieGiEhFLuiA==", "+905367541559", false, "Newsreader", null, "L4YTUBCZWFK3BR8ZWL744O0RYSUJOJQA", 3, "Şahin", new Guid("81177806-7278-45b9-939a-bd14679d30c2"), false, null, "ali.sahin@google.com" },
                    { new Guid("ffb83a0d-8c70-4456-b277-ff418a592c63"), 0, new DateTime(2067, 3, 9, 9, 35, 34, 37, DateTimeKind.Local).AddTicks(6904), null, new Guid("64a61aa1-89f7-48f3-a2f8-379eec32b3dc"), "cf85c15a-17e8-4255-b151-52470bc372a0", new DateTime(2023, 5, 11, 9, 35, 34, 37, DateTimeKind.Local).AddTicks(6902), null, "ibrahim.yildiz@outlook.com", false, "5505477788", "/images/UserPhotos/defaultuser.jpg", false, null, "İbrahim", "IBRAHIM.YILDIZ@OUTLOOK.COM", "IBRAHIM.YILDIZ@OUTLOOK.COM", "AQAAAAEAACcQAAAAEK5nwEzPxHG/FzeGu7bQ+zvxogtr0aE89YMJDc2MYPdt7pUtU/RiKJnH9OylvC7Gmg==", "+905996248517", false, "Receptionist", null, "N716H16S3B611H6JVRZ8TVVEYA1ONQJS", 3, "Yıldız", new Guid("e2092c2c-15c2-419a-8460-3bd7b728bc0f"), false, null, "ibrahim.yildiz@outlook.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("0a2ded2a-d239-48aa-9208-fd680a848f59") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("1c84649d-9aca-45db-967d-27b04b30ef85") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("26709fad-a014-457c-919b-b634174b0c7d") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("28e06592-35f7-4387-ac28-4fa7c2b65ad7") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("2cbc8181-9594-451a-ae3c-fd7c1201fa5f") },
                    { new Guid("dd59a103-db7f-4dff-aead-c7832328af56"), new Guid("33cb8551-77d7-43ff-b305-2d68f1307f71") },
                    { new Guid("dd59a103-db7f-4dff-aead-c7832328af56"), new Guid("80570c85-e7b9-425b-b67c-8a7ee4b5ab4a") },
                    { new Guid("dd59a103-db7f-4dff-aead-c7832328af56"), new Guid("94c2bcdc-7dbf-4cf8-a053-eb5534f867f6") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("9f120828-9658-4c2d-9d1e-2d21eb3ce8b7") },
                    { new Guid("dd59a103-db7f-4dff-aead-c7832328af56"), new Guid("a5981db3-b0d3-4e5d-8fa4-80da7416f86d") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("b5f9cace-bfd5-4554-bd6d-90b118e90a75") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("bf819692-436f-4569-82b8-9efdd2c7205e") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("c62b4d8f-6e77-4820-a095-e752e93d6ff2") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("c9841234-80c1-4a6f-ba91-9687f9a60ef3") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("cbfc66cc-6a8f-4e72-a5fd-69049955c23b") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("d052f77a-5cd9-4132-b741-42c1cb144af9") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("d5dd2363-8a11-4fd2-b6ec-91635b09e25d") },
                    { new Guid("dd59a103-db7f-4dff-aead-c7832328af56"), new Guid("f2186b6c-4294-4448-8419-1c7b4520a964") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("f2d17da7-c782-4594-ba43-49f1112e6c34") },
                    { new Guid("d3b0cd8e-3a2e-4d23-abf0-3c8c7e16ab61"), new Guid("ffb83a0d-8c70-4456-b277-ff418a592c63") }
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
