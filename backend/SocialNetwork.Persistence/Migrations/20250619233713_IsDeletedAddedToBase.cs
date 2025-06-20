using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmMatch.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IsDeletedAddedToBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserLikedFilm",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserFriends",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserDislikedFilm",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserBookmarkedFilm",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FriendRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Films",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0b27972c-b3df-4ae4-9138-2e90c749d139"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("31f80f2a-9426-41e2-93f7-7f12180722a1"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4b4974f1-8ea2-43f1-998f-d3a1cfb9d1c3"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5dbd7a97-f0a1-4f4e-91c7-244cbab17eec"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d0bfe76e-0f12-4fcd-94aa-3be4f90d79e1"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: new Guid("8cb06d19-68aa-4ce3-a1a6-76e48d9f4d55"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: new Guid("a1e7b5fa-34df-4bc4-902e-cdfb10dcf001"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: new Guid("afde4eeb-c0b7-404f-aad0-0d188fe9a921"),
                column: "IsDeleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserLikedFilm");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserFriends");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserDislikedFilm");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserBookmarkedFilm");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");
        }
    }
}
