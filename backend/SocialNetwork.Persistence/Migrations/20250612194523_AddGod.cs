using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FilmMatch.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, "User", "USER" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, "Admin", "ADMIN" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, "God", "GOD" }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0b27972c-b3df-4ae4-9138-2e90c749d139"),
                column: "ImageUrl",
                value: "/images/category/militant.webp");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("31f80f2a-9426-41e2-93f7-7f12180722a1"),
                column: "ImageUrl",
                value: "/images/category/thriller.webp");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4b4974f1-8ea2-43f1-998f-d3a1cfb9d1c3"),
                column: "ImageUrl",
                value: "/images/category/drama.webp");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5dbd7a97-f0a1-4f4e-91c7-244cbab17eec"),
                column: "ImageUrl",
                value: "/images/category/comedy.webp");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d0bfe76e-0f12-4fcd-94aa-3be4f90d79e1"),
                column: "ImageUrl",
                value: "/images/category/fantasy.webp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0b27972c-b3df-4ae4-9138-2e90c749d139"),
                column: "ImageUrl",
                value: "https://avatars.mds.yandex.net/get-kinopoisk-image/4771096/2a0000017e39d1cfcb48b5f4fe5a81e8b9f4/1920x");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("31f80f2a-9426-41e2-93f7-7f12180722a1"),
                column: "ImageUrl",
                value: "https://avatars.mds.yandex.net/get-kinopoisk-image/4771096/2a0000017e39d1cfcb48b5f4fe5a81e8b9f4/1920x");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4b4974f1-8ea2-43f1-998f-d3a1cfb9d1c3"),
                column: "ImageUrl",
                value: "https://avatars.mds.yandex.net/get-kinopoisk-image/4771096/2a0000017e39d1cfcb48b5f4fe5a81e8b9f4/1920x");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5dbd7a97-f0a1-4f4e-91c7-244cbab17eec"),
                column: "ImageUrl",
                value: "https://avatars.mds.yandex.net/get-kinopoisk-image/4771096/2a0000017e39d1cfcb48b5f4fe5a81e8b9f4/1920x");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d0bfe76e-0f12-4fcd-94aa-3be4f90d79e1"),
                column: "ImageUrl",
                value: "https://avatars.mds.yandex.net/get-kinopoisk-image/4771096/2a0000017e39d1cfcb48b5f4fe5a81e8b9f4/1920x");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "User", "USER" },
                    { "2", null, "Admin", "ADMIN" }
                });
        }
    }
}
