using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondExam.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDefinition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "UserCredentials",
                columns: table => new
                {
                    CredentialsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCredentials", x => x.CredentialsID);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    MaterialTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialId);
                    table.ForeignKey(
                        name: "FK_Materials_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId");
                    table.ForeignKey(
                        name: "FK_Materials_Types_MaterialTypeId",
                        column: x => x.MaterialTypeId,
                        principalTable: "Types",
                        principalColumn: "TypeId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CredentialsID = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_UserCredentials_CredentialsID",
                        column: x => x.CredentialsID,
                        principalTable: "UserCredentials",
                        principalColumn: "CredentialsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextReview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DigitReview = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "AuthorDescription", "AuthorName" },
                values: new object[,]
                {
                    { 1, "Polish author", "Jan Kowalski" },
                    { 2, "Famous russian scientist", "Wolodia Puszkin" },
                    { 3, "Inventor of tens", "Nacomi Tachata" },
                    { 4, "Has so many publications", "Daniel Nowak" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeDefinition", "TypeName" },
                values: new object[,]
                {
                    { 1, "Video tutorial is a video material that focuses mostly on guiding step-by-step in dedicated topic", "Video tutorial" },
                    { 2, "Article is an article on web page", "Article" },
                    { 3, "What book is, everybody knows", "Book" },
                    { 4, "Sample code", "Code snippet" }
                });

            migrationBuilder.InsertData(
                table: "UserCredentials",
                columns: new[] { "CredentialsID", "Login", "Password" },
                values: new object[,]
                {
                    { 1, "admin", "admin" },
                    { 2, "user", "user" }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "MaterialId", "AuthorId", "CreatedDate", "MaterialDescription", "MaterialLocation", "MaterialTitle", "MaterialTypeId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2009, 7, 4, 8, 15, 11, 16, DateTimeKind.Unspecified).AddTicks(790), "How to avoid nulls in C#", "Internet", "How to avoid Nulls", 1 },
                    { 2, 2, new DateTime(2004, 12, 26, 0, 24, 16, 94, DateTimeKind.Unspecified).AddTicks(2403), "Basic of C#", "Library", "C# for dummies", 3 },
                    { 3, 3, new DateTime(1998, 4, 1, 17, 33, 29, 610, DateTimeKind.Unspecified).AddTicks(3173), "Collection of best practices", "Internet", "Best practices in code", 2 },
                    { 4, 4, new DateTime(2004, 1, 30, 22, 47, 4, 352, DateTimeKind.Unspecified).AddTicks(7247), "How to seed database", "Internet", "Seeding Database", 4 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CredentialsID", "Role" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 2, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "DigitReview", "MaterialId", "ReviewReference", "TextReview" },
                values: new object[,]
                {
                    { 1, 9, 1, "https://localhost:5001/1", "Good material" },
                    { 2, 10, 1, "https://localhost:5001/1", "Great material" },
                    { 3, 2, 1, "https://localhost:5001/1", "poor material" },
                    { 4, 9, 2, "https://localhost:5001/2", "Good material" },
                    { 5, 7, 3, "https://localhost:5001/3", "Good material" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_AuthorId",
                table: "Materials",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MaterialTypeId",
                table: "Materials",
                column: "MaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MaterialId",
                table: "Reviews",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CredentialsID",
                table: "Users",
                column: "CredentialsID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "UserCredentials");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
