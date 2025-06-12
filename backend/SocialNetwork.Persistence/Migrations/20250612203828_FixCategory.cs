using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmMatch.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0b27972c-b3df-4ae4-9138-2e90c749d139"),
                column: "ImageUrl",
                value: "http://localhost:5210/images/category/action.png");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("31f80f2a-9426-41e2-93f7-7f12180722a1"),
                column: "ImageUrl",
                value: "http://localhost:5210/images/category/thriller.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4b4974f1-8ea2-43f1-998f-d3a1cfb9d1c3"),
                column: "ImageUrl",
                value: "http://localhost:5210/images/category/drama.png");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5dbd7a97-f0a1-4f4e-91c7-244cbab17eec"),
                column: "ImageUrl",
                value: "http://localhost:5210/images/category/comedy.jpg");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d0bfe76e-0f12-4fcd-94aa-3be4f90d79e1"),
                column: "ImageUrl",
                value: "http://localhost:5210/images/category/fantasy.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
