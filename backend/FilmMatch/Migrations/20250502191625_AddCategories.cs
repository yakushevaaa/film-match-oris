using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FilmMatch.Migrations
{
    /// <inheritdoc />
    public partial class AddCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    ImageAlt = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    ImageAlt = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryId", "ImageAlt", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, 0, "Боевик", "https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/3e7efab5-e108-4de0-99b6-8196d6cf7c21/1920x", "Боевик" },
                    { 2, 0, "Детектив", "https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/3e7efab5-e108-4de0-99b6-8196d6cf7c21/1920x", "Детектив" },
                    { 3, 0, "Комедия", "https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/3e7efab5-e108-4de0-99b6-8196d6cf7c21/1920x", "Комедия" },
                    { 4, 0, "Мелодрама", "https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/3e7efab5-e108-4de0-99b6-8196d6cf7c21/1920x", "Мелодрама" },
                    { 5, 0, "Трагедия", "https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/3e7efab5-e108-4de0-99b6-8196d6cf7c21/1920x", "Трагедия" }
                });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Description", "ImageAlt", "ImageUrl", "Title" },
                values: new object[] { 1, "", "", "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Films");
        }
    }
}
