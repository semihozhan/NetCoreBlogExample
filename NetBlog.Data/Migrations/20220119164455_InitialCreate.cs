using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetBlog.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "VARBINARY(500)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    SeoAuthor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SeoTags = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ArticleID = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedByName", "CreatedOn", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedOn", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 874, DateTimeKind.Local).AddTicks(9066), "c# Category", true, false, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 874, DateTimeKind.Local).AddTicks(9079), "c#", "c#" },
                    { 2, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 874, DateTimeKind.Local).AddTicks(9094), "c++ Category", true, false, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 874, DateTimeKind.Local).AddTicks(9096), "c++", "c++" },
                    { 3, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 874, DateTimeKind.Local).AddTicks(9100), "Javascript Category", true, false, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 874, DateTimeKind.Local).AddTicks(9102), "Javascript", "Javascript" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedByName", "CreatedOn", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedOn", "Name", "Note" },
                values: new object[] { 1, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 878, DateTimeKind.Local).AddTicks(8407), "Tüm Haklara Sahip", true, false, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 878, DateTimeKind.Local).AddTicks(8421), "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedByName", "CreatedOn", "Description", "Email", "Firstname", "IsActive", "IsDeleted", "Lastname", "ModifiedByName", "ModifiedOn", "Note", "PasswordHash", "Picture", "RoleID", "username" },
                values: new object[] { 1, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 889, DateTimeKind.Local).AddTicks(8023), "First User", "semihozhan@yandex.com", "Semih", true, false, "Ozhan", "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 889, DateTimeKind.Local).AddTicks(8034), "Admin", new byte[] { 101, 49, 48, 97, 100, 99, 51, 57, 52, 57, 98, 97, 53, 57, 97, 98, 98, 101, 53, 54, 101, 48, 53, 55, 102, 50, 48, 102, 56, 56, 51, 101 }, "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU", 1, "semihozhan" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedOn", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedOn", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 1, 1, 1, "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.", "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 871, DateTimeKind.Local).AddTicks(5166), new DateTime(2022, 1, 19, 19, 44, 54, 871, DateTimeKind.Local).AddTicks(6665), true, false, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 871, DateTimeKind.Local).AddTicks(5597), "C# 9.0 ve .net 5", "Admin", "C# 9.0 ve .net 5", "C# 9.0,.net 5", "default.jpg", "C# 9.0 ve .net 5", 1, 130 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedOn", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedOn", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 2, 2, 1, "Yinelenen bir sayfa içeriğinin okuyucunun dikkatini dağıttığı bilinen bir gerçektir. Lorem Ipsum kullanmanın amacı, sürekli 'buraya metin gelecek, buraya metin gelecek' yazmaya kıyasla daha dengeli bir harf dağılımı sağlayarak okunurluğu artırmasıdır. Şu anda birçok masaüstü yayıncılık paketi ve web sayfa düzenleyicisi, varsayılan mıgır metinler olarak Lorem Ipsum kullanmaktadır. Ayrıca arama motorlarında 'lorem ipsum' anahtar sözcükleri ile arama yapıldığında henüz tasarım aşamasında olan çok sayıda site listelenir. Yıllar içinde, bazen kazara, bazen bilinçli olarak (örneğin mizah katılarak), çeşitli sürümleri geliştirilmiştir.", "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 871, DateTimeKind.Local).AddTicks(7499), new DateTime(2022, 1, 19, 19, 44, 54, 871, DateTimeKind.Local).AddTicks(7505), true, false, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 871, DateTimeKind.Local).AddTicks(7502), "C# 9.0 ve .net 5", "Admin", "C++ 9.0 ve .net 5", "C++ 9.0,.net 5", "default.jpg", "C++ 9.0 ve .net 5", 1, 140 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedOn", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedOn", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 3, 3, 1, "Javascript Yinelenen bir sayfa içeriğinin okuyucunun dikkatini dağıttığı bilinen bir gerçektir. Lorem Ipsum kullanmanın amacı, sürekli 'buraya metin gelecek, buraya metin gelecek' yazmaya kıyasla daha dengeli bir harf dağılımı sağlayarak okunurluğu artırmasıdır. Şu anda birçok masaüstü yayıncılık paketi ve web sayfa düzenleyicisi, varsayılan mıgır metinler olarak Lorem Ipsum kullanmaktadır. Ayrıca arama motorlarında 'lorem ipsum' anahtar sözcükleri ile arama yapıldığında henüz tasarım aşamasında olan çok sayıda site listelenir. Yıllar içinde, bazen kazara, bazen bilinçli olarak (örneğin mizah katılarak), çeşitli sürümleri geliştirilmiştir.", "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 871, DateTimeKind.Local).AddTicks(7512), new DateTime(2022, 1, 19, 19, 44, 54, 871, DateTimeKind.Local).AddTicks(7516), true, false, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 871, DateTimeKind.Local).AddTicks(7514), "Javascript", "Admin", "Javascript ES2019", "Javascript", "default.jpg", "Javascript ES2019", 1, 70 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleID", "CreatedByName", "CreatedOn", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedOn", "Note", "Text" },
                values: new object[] { 1, 1, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 876, DateTimeKind.Local).AddTicks(9458), true, false, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 876, DateTimeKind.Local).AddTicks(9472), "Javascript", "C# comment example" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleID", "CreatedByName", "CreatedOn", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedOn", "Note", "Text" },
                values: new object[] { 2, 2, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 876, DateTimeKind.Local).AddTicks(9485), true, false, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 876, DateTimeKind.Local).AddTicks(9487), "Javascript", "C++ comment example" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleID", "CreatedByName", "CreatedOn", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedOn", "Note", "Text" },
                values: new object[] { 3, 3, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 876, DateTimeKind.Local).AddTicks(9491), true, false, "InitialCreate", new DateTime(2022, 1, 19, 19, 44, 54, 876, DateTimeKind.Local).AddTicks(9492), "Javascript", "Javascript comment example" });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleID",
                table: "Comments",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_username",
                table: "Users",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
